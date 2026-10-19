# Tiempo en Orden

Aplicación .NET MAUI de una agenda personal y de negocio para Android y Windows. E2-T03 incorpora navegación y pantallas XAML de la entrega A con datos ficticios en memoria; todavía no implementa persistencia, motor de disponibilidad, servidor, sincronización ni recordatorios.

## Estructura

- `src/TiempoEnOrden.App`: aplicación .NET MAUI con XAML para Android y Windows.
- `src/TiempoEnOrden.Core`: biblioteca C# compartida e independiente de MAUI.

La aplicación depende de `Core`; `Core` no depende de la aplicación.

## Pantallas disponibles en la demostración

- Inicio con próxima cita, compromisos del día y tarea sin horario.
- Agenda diaria y semanal con navegación por fecha, filtros y canceladas.
- Nueva cita como borrador visual; no guarda ni comprueba conflictos.
- Tiempo personal con altas únicamente en memoria.
- Clientes y Servicios con creación contextual que conserva el borrador de cita.
- Ajustes de demostración.

La muestra usa el viernes 6 de noviembre de 2026 a las 09:20 como fecha y hora ficticias. Todas las pantallas señalan que los datos pueden desaparecer al reiniciar.

## Requisitos de compilación

- .NET SDK 10.0.113, fijado por `global.json`.
- Carga de trabajo MAUI 10.0.110.
- Microsoft OpenJDK 21.0.12.1.
- Android SDK Platform 36, Build Tools 36.0.0 y Platform Tools.
- Microsoft.Windows.SDK.BuildTools 10.0.26100.8249, restaurado de forma transitiva por la versión elegida de MAUI.

Variables de usuario esperadas en Windows:

```powershell
$env:DOTNET_ROOT = "$env:USERPROFILE\.dotnet"
$env:JAVA_HOME = "$env:USERPROFILE\.jdks\jdk-21.0.12.1+1"
$env:ANDROID_HOME = "$env:LOCALAPPDATA\Android\Sdk"
$env:ANDROID_SDK_ROOT = $env:ANDROID_HOME
```

Después de una instalación nueva, abrir otra terminal para que se refleje el `PATH`. Si `dotnet` todavía apunta a otro SDK, se puede usar temporalmente `& "$env:USERPROFILE\.dotnet\dotnet.exe"` en los comandos siguientes.

## Preparación reproducible

```powershell
dotnet workload install maui
dotnet workload update
dotnet --info
dotnet workload list
java -version
```

## Restaurar y compilar

Biblioteca compartida:

```powershell
dotnet restore .\src\TiempoEnOrden.Core\TiempoEnOrden.Core.csproj
dotnet build .\src\TiempoEnOrden.Core\TiempoEnOrden.Core.csproj -c Debug --no-restore
```

Android:

```powershell
dotnet restore .\src\TiempoEnOrden.App\TiempoEnOrden.App.csproj
dotnet build .\src\TiempoEnOrden.App\TiempoEnOrden.App.csproj -f net10.0-android -c Debug --no-restore
```

Windows:

```powershell
dotnet restore .\src\TiempoEnOrden.App\TiempoEnOrden.App.csproj
dotnet build .\src\TiempoEnOrden.App\TiempoEnOrden.App.csproj -f net10.0-windows10.0.19041.0 -c Debug --no-restore
```

Estos comandos solo restauran y compilan. No ejecutan la aplicación ni sustituyen las pruebas posteriores en un teléfono Android o en una sesión de Windows.

## Ejecutar en Windows

```powershell
dotnet run --project .\src\TiempoEnOrden.App\TiempoEnOrden.App.csproj -f net10.0-windows10.0.19041.0 --no-build --no-launch-profile
```

E2-T03 confirmó que el proceso crea una ventana principal titulada `Tiempo en Orden`. La inspección automatizada de su contenido quedó pendiente porque la herramienta de captura no expuso esa ventana como seleccionable. Android no se ejecutó: se preparará en E2-T04 sin instalar un emulador durante E2-T03.
