# Matriz de pruebas de la entrega local

Fecha: 2026-10-19. Los resultados distinguen evidencia automatizada, emulador y pendientes físicos.

| Área | Entorno | Resultado | Evidencia | Pendiente |
| --- | --- | --- | --- | --- |
| SQLite, catálogos y citas | Verificador .NET | aprobado | Crea cliente, servicio y cita; cierra/reabre; edita, cancela e inspecciona historial | Interfaz Windows manual |
| Tiempo personal y horario | Verificador .NET | aprobado | Guarda actividad, horario, descanso y días; cierra/reabre | Interfaz Windows manual |
| Conflictos y márgenes | Verificador .NET | aprobado | Preparación, limpieza, descanso, jornada, personal, cancelación, autoexclusión y extremos contiguos | Recorrido visual manual |
| Sugerencias | Verificador .NET | aprobado | Tres máximas; 15 minutos; 14 días; cada una vuelve a validarse con la regla de guardado | Recorrido visual manual |
| Respaldo/restauración | Verificador .NET | aprobado | Crea copia, agrega dato posterior, restaura y verifica recuperación | Recuperación manual en teléfono real |
| Compilación Android | .NET 10/MAUI 10, API 36 | aprobado | `dotnet build -f net10.0-android`: 0 advertencias, 0 errores | Teléfono real |
| Compilación Windows | .NET 10/MAUI 10, Windows x64 | aprobado | `dotnet build -f net10.0-windows10.0.19041.0`: 0 advertencias, 0 errores | Recorrido manual de interfaz |
| Ejecución Android | AVD `TiempoEnOrden_API_36` | pruebas anteriores aprobadas para compilación de desarrollo y primer hito; APK Release final bloqueada | Instalación, arranque, creación/persistencia tras cierre, edición y cancelación observadas antes con desarrollo. La pasada Release del 19-oct dejó ADB `offline` tras 90 s | Instalar y recorrer el APK Release; conflicto, sugerencia y respaldo; teléfono real |
| Ejecución Windows | Windows 25H2 | bloqueado para interacción | Proceso y ventana arrancaron; la herramienta no expuso WinUI seleccionable | Responsable debe seguir el recorrido Release de MANUAL.md y registrar resultado |
| Teléfono Android real | Teléfono autorizado | no ejecutado | No había teléfono conectado | Conectar, autorizar ADB, instalar APK y repetir el manual |

No se presentan pruebas del prototipo HTML como evidencia de la aplicación MAUI.

## Recorrido Release manual pendiente

El recorrido exacto de 10 minutos está en [MANUAL.md](MANUAL.md). Su estado es **no ejecutado** hasta recibir los resultados del responsable; no se debe cambiar a aprobado con la evidencia de compilación, el verificador o el AVD anterior.
