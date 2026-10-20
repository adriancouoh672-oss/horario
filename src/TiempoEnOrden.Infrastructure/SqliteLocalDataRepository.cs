using SQLite;
using TiempoEnOrden.Core.Data;
using TiempoEnOrden.Core.Models;
using TiempoEnOrden.Infrastructure.Entities;

namespace TiempoEnOrden.Infrastructure;

public sealed class SqliteLocalDataRepository : ILocalDataRepository
{
    private readonly SemaphoreSlim _gate = new(1, 1);
    private SQLiteAsyncConnection? _connection;
    private bool _initialized;

    public SqliteLocalDataRepository(string databasePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(databasePath);
        DatabasePath = databasePath;
    }

    public string DatabasePath { get; }

    public async Task InitializeAsync()
    {
        await _gate.WaitAsync();
        try
        {
            if (_initialized)
            {
                return;
            }

            Directory.CreateDirectory(Path.GetDirectoryName(DatabasePath)!);
            _connection = CreateConnection();
            await _connection.CreateTablesAsync(
                CreateFlags.None,
                typeof(ClientEntity),
                typeof(ServiceEntity),
                typeof(AppointmentEntity),
                typeof(PersonalTimeEntity),
                typeof(WorkScheduleEntity));

            if (await _connection.Table<WorkScheduleEntity>().CountAsync() == 0)
            {
                await _connection.InsertAsync(ToEntity(WorkScheduleSettings.Default));
            }

            _initialized = true;
        }
        finally
        {
            _gate.Release();
        }
    }

    public async Task<IReadOnlyList<ClientRecord>> GetClientsAsync(bool includeArchived = false)
    {
        var connection = await GetConnectionAsync();
        var rows = await connection.Table<ClientEntity>().OrderBy(item => item.Name).ToListAsync();
        return rows.Where(item => includeArchived || !item.IsArchived).Select(ToRecord).ToList();
    }

    public async Task<ClientRecord?> GetClientAsync(Guid id)
    {
        var connection = await GetConnectionAsync();
        var row = await connection.FindAsync<ClientEntity>(id.ToString("D"));
        return row is null ? null : ToRecord(row);
    }

    public async Task SaveClientAsync(ClientRecord client)
    {
        Validate(client);
        var connection = await GetConnectionAsync();
        await connection.InsertOrReplaceAsync(ToEntity(client));
    }

    public async Task<IReadOnlyList<ServiceRecord>> GetServicesAsync(bool includeArchived = false)
    {
        var connection = await GetConnectionAsync();
        var rows = await connection.Table<ServiceEntity>().OrderBy(item => item.Name).ToListAsync();
        return rows.Where(item => includeArchived || !item.IsArchived).Select(ToRecord).ToList();
    }

    public async Task<ServiceRecord?> GetServiceAsync(Guid id)
    {
        var connection = await GetConnectionAsync();
        var row = await connection.FindAsync<ServiceEntity>(id.ToString("D"));
        return row is null ? null : ToRecord(row);
    }

    public async Task SaveServiceAsync(ServiceRecord service)
    {
        Validate(service);
        var connection = await GetConnectionAsync();
        await connection.InsertOrReplaceAsync(ToEntity(service));
    }

    public async Task<IReadOnlyList<AppointmentRecord>> GetAppointmentsAsync(DateTime? from = null, DateTime? to = null, bool includeCancelled = false)
    {
        var connection = await GetConnectionAsync();
        var rows = await connection.Table<AppointmentEntity>().OrderBy(item => item.StartTicks).ToListAsync();
        return rows
            .Where(item => includeCancelled || item.Status != (int)AppointmentStatus.Cancelled)
            .Select(ToRecord)
            .Where(item => (!from.HasValue || item.OccupiedEnd > from.Value) && (!to.HasValue || item.OccupiedStart < to.Value))
            .ToList();
    }

    public async Task<AppointmentRecord?> GetAppointmentAsync(Guid id)
    {
        var connection = await GetConnectionAsync();
        var row = await connection.FindAsync<AppointmentEntity>(id.ToString("D"));
        return row is null ? null : ToRecord(row);
    }

    public async Task SaveAppointmentAsync(AppointmentRecord appointment)
    {
        Validate(appointment);
        var connection = await GetConnectionAsync();
        await connection.InsertOrReplaceAsync(ToEntity(appointment));
    }

    public async Task<IReadOnlyList<PersonalTimeRecord>> GetPersonalTimesAsync(DateTime? from = null, DateTime? to = null)
    {
        var connection = await GetConnectionAsync();
        var rows = await connection.Table<PersonalTimeEntity>().OrderBy(item => item.StartTicks).ToListAsync();
        return rows.Select(ToRecord)
            .Where(item => (!from.HasValue || item.End > from.Value) && (!to.HasValue || item.Start < to.Value))
            .ToList();
    }

    public async Task SavePersonalTimeAsync(PersonalTimeRecord personalTime)
    {
        Validate(personalTime);
        var connection = await GetConnectionAsync();
        await connection.InsertOrReplaceAsync(ToEntity(personalTime));
    }

    public async Task<WorkScheduleSettings> GetWorkScheduleAsync()
    {
        var connection = await GetConnectionAsync();
        var row = await connection.FindAsync<WorkScheduleEntity>(1);
        return row is null ? WorkScheduleSettings.Default : ToRecord(row);
    }

    public async Task SaveWorkScheduleAsync(WorkScheduleSettings settings)
    {
        Validate(settings);
        var connection = await GetConnectionAsync();
        await connection.InsertOrReplaceAsync(ToEntity(settings));
    }

    public async Task<string> CreateBackupAsync(string destinationPath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(destinationPath);
        await CheckpointAndCloseAsync();
        Directory.CreateDirectory(Path.GetDirectoryName(destinationPath)!);
        File.Copy(DatabasePath, destinationPath, true);
        return destinationPath;
    }

    public async Task RestoreBackupAsync(string sourcePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sourcePath);
        if (!File.Exists(sourcePath))
        {
            throw new FileNotFoundException("No existe el respaldo indicado.", sourcePath);
        }

        await CheckpointAndCloseAsync();
        File.Copy(sourcePath, DatabasePath, true);
        await InitializeAsync();
    }

    public async Task CloseAsync() => await CheckpointAndCloseAsync();

    private SQLiteAsyncConnection CreateConnection() => new(
        DatabasePath,
        SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);

    private async Task<SQLiteAsyncConnection> GetConnectionAsync()
    {
        await InitializeAsync();
        return _connection!;
    }

    private async Task CheckpointAndCloseAsync()
    {
        await _gate.WaitAsync();
        try
        {
            if (_connection is not null)
            {
                await _connection.CloseAsync();
                _connection = null;
            }

            _initialized = false;
        }
        finally
        {
            _gate.Release();
        }
    }

    private static ClientEntity ToEntity(ClientRecord value) => new()
    {
        Id = value.Id.ToString("D"), Name = value.Name, Phone = value.Phone, Notes = value.Notes, IsArchived = value.IsArchived
    };

    private static ClientRecord ToRecord(ClientEntity value) => new(Guid.Parse(value.Id), value.Name, value.Phone, value.Notes, value.IsArchived);

    private static ServiceEntity ToEntity(ServiceRecord value) => new()
    {
        Id = value.Id.ToString("D"), Name = value.Name, DurationMinutes = value.DurationMinutes,
        PreparationMinutes = value.PreparationMinutes, CleanupMinutes = value.CleanupMinutes,
        Price = value.Price, Currency = value.Currency, IsArchived = value.IsArchived
    };

    private static ServiceRecord ToRecord(ServiceEntity value) => new(
        Guid.Parse(value.Id), value.Name, value.DurationMinutes, value.PreparationMinutes,
        value.CleanupMinutes, value.Price, value.Currency, value.IsArchived);

    private static AppointmentEntity ToEntity(AppointmentRecord value) => new()
    {
        Id = value.Id.ToString("D"), ClientId = value.ClientId?.ToString("D"), ClientName = value.ClientName,
        ServiceId = value.ServiceId.ToString("D"), ServiceName = value.ServiceName, StartTicks = value.Start.Ticks,
        DurationMinutes = value.DurationMinutes, PreparationMinutes = value.PreparationMinutes,
        CleanupMinutes = value.CleanupMinutes, Price = value.Price, Currency = value.Currency,
        Notes = value.Notes, Status = (int)value.Status
    };

    private static AppointmentRecord ToRecord(AppointmentEntity value) => new(
        Guid.Parse(value.Id), string.IsNullOrWhiteSpace(value.ClientId) ? null : Guid.Parse(value.ClientId),
        value.ClientName, Guid.Parse(value.ServiceId), value.ServiceName, new DateTime(value.StartTicks),
        value.DurationMinutes, value.PreparationMinutes, value.CleanupMinutes, value.Price, value.Currency,
        value.Notes, (AppointmentStatus)value.Status);

    private static PersonalTimeEntity ToEntity(PersonalTimeRecord value) => new()
    {
        Id = value.Id.ToString("D"), Title = value.Title, Kind = (int)value.Kind,
        StartTicks = value.Start.Ticks, EndTicks = value.End.Ticks
    };

    private static PersonalTimeRecord ToRecord(PersonalTimeEntity value) => new(
        Guid.Parse(value.Id), value.Title, (PersonalTimeKind)value.Kind,
        new DateTime(value.StartTicks), new DateTime(value.EndTicks));

    private static WorkScheduleEntity ToEntity(WorkScheduleSettings value) => new()
    {
        Id = 1,
        WorkStartTicks = value.WorkStart.Ticks,
        WorkEndTicks = value.WorkEnd.Ticks,
        BreakStartTicks = value.BreakStart.Ticks,
        BreakEndTicks = value.BreakEnd.Ticks,
        WorkingDaysMask = value.WorkingDays.Aggregate(0, (mask, day) => mask | (1 << (int)day))
    };

    private static WorkScheduleSettings ToRecord(WorkScheduleEntity value) => new(
        new TimeSpan(value.WorkStartTicks),
        new TimeSpan(value.WorkEndTicks),
        new TimeSpan(value.BreakStartTicks),
        new TimeSpan(value.BreakEndTicks),
        Enum.GetValues<DayOfWeek>().Where(day => (value.WorkingDaysMask & (1 << (int)day)) != 0).ToHashSet());

    private static void Validate(ClientRecord value)
    {
        if (value.Id == Guid.Empty || string.IsNullOrWhiteSpace(value.Name))
            throw new ArgumentException("El cliente requiere identificador y nombre.");
    }

    private static void Validate(ServiceRecord value)
    {
        if (value.Id == Guid.Empty || string.IsNullOrWhiteSpace(value.Name) || value.DurationMinutes <= 0 ||
            value.PreparationMinutes < 0 || value.CleanupMinutes < 0 || value.Price < 0 || string.IsNullOrWhiteSpace(value.Currency))
            throw new ArgumentException("El servicio contiene valores inválidos.");
    }

    private static void Validate(AppointmentRecord value)
    {
        if (value.Id == Guid.Empty || value.ServiceId == Guid.Empty || value.DurationMinutes <= 0 ||
            value.PreparationMinutes < 0 || value.CleanupMinutes < 0)
            throw new ArgumentException("La cita contiene valores inválidos.");
    }

    private static void Validate(PersonalTimeRecord value)
    {
        if (value.Id == Guid.Empty || string.IsNullOrWhiteSpace(value.Title) || value.End <= value.Start)
            throw new ArgumentException("El tiempo personal contiene valores inválidos.");
    }

    private static void Validate(WorkScheduleSettings value)
    {
        if (value.WorkEnd <= value.WorkStart || value.BreakEnd <= value.BreakStart || value.WorkingDays.Count == 0)
            throw new ArgumentException("El horario laboral contiene valores inválidos.");
    }
}

