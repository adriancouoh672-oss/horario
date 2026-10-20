# Guía técnica — Entrega local

## Arquitectura

| Proyecto | Responsabilidad | Dependencias |
| --- | --- | --- |
| `TiempoEnOrden.Core` | Modelos y regla de disponibilidad | Ninguna dependencia de MAUI o SQLite |
| `TiempoEnOrden.Infrastructure` | SQLite, mapeo y copias de seguridad | Core, `sqlite-net-pcl 1.11.285` |
| `TiempoEnOrden.App` | XAML, MVVM, navegación y ruta local de datos | Core e Infrastructure |
| `TiempoEnOrden.Verification` | Verificación de integración ejecutable | Core e Infrastructure |

La ruta de SQLite se obtiene desde `FileSystem.AppDataDirectory`. Core no depende de App. Cada cita copia los datos relevantes del servicio para preservar su historia cuando el catálogo cambia.

## Disponibilidad

`AvailabilityService` forma el intervalo ocupado desde inicio de preparación hasta fin de limpieza. Rechaza cruces con jornada, descanso, tiempo personal y citas activas; las canceladas se excluyen y la cita editada se autoexcluye por ID. Usa intervalos semiabiertos: dos intervalos contiguos no se cruzan.

La misma clase valida guardar y calcula sugerencias, con máximo de tres resultados, saltos de 15 minutos y 14 días de horizonte. Aplicar una sugerencia solo cambia el borrador.

## Respaldo

Infrastructure cierra la conexión antes de copiar o restaurar la base. La interfaz guarda copias en el directorio privado `respaldos` y solo restaura la más reciente después de confirmar. No hay respaldo externo ni cifrado adicional en esta versión.

## Comandos de calidad

```powershell
& "$env:USERPROFILE\.dotnet\dotnet.exe" run --project .\tests\TiempoEnOrden.Verification\TiempoEnOrden.Verification.csproj
& "$env:USERPROFILE\.dotnet\dotnet.exe" build .\src\TiempoEnOrden.App\TiempoEnOrden.App.csproj -f net10.0-android --no-restore
& "$env:USERPROFILE\.dotnet\dotnet.exe" build .\src\TiempoEnOrden.App\TiempoEnOrden.App.csproj -f net10.0-windows10.0.19041.0 --no-restore
```

Consulta [PRUEBAS-ENTREGA.md](PRUEBAS-ENTREGA.md) para distinguir compilación, AVD, pruebas automatizadas y pendientes de pruebas físicas.
