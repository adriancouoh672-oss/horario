using TiempoEnOrden.Core.Models;

namespace TiempoEnOrden.Core.Data;

public interface ILocalDataRepository
{
    string DatabasePath { get; }

    Task InitializeAsync();

    Task<IReadOnlyList<ClientRecord>> GetClientsAsync(bool includeArchived = false);
    Task<ClientRecord?> GetClientAsync(Guid id);
    Task SaveClientAsync(ClientRecord client);

    Task<IReadOnlyList<ServiceRecord>> GetServicesAsync(bool includeArchived = false);
    Task<ServiceRecord?> GetServiceAsync(Guid id);
    Task SaveServiceAsync(ServiceRecord service);

    Task<IReadOnlyList<AppointmentRecord>> GetAppointmentsAsync(DateTime? from = null, DateTime? to = null, bool includeCancelled = false);
    Task<AppointmentRecord?> GetAppointmentAsync(Guid id);
    Task SaveAppointmentAsync(AppointmentRecord appointment);

    Task<IReadOnlyList<PersonalTimeRecord>> GetPersonalTimesAsync(DateTime? from = null, DateTime? to = null);
    Task<PersonalTimeRecord?> GetPersonalTimeAsync(Guid id);
    Task SavePersonalTimeAsync(PersonalTimeRecord personalTime);
    Task DeletePersonalTimeAsync(Guid id);

    Task<WorkScheduleSettings> GetWorkScheduleAsync();
    Task SaveWorkScheduleAsync(WorkScheduleSettings settings);

    Task<string> CreateBackupAsync(string destinationPath);
    Task RestoreBackupAsync(string sourcePath);
    Task CloseAsync();
}

