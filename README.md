# Tiempo en Orden

Aplicación .NET MAUI de una agenda personal y de negocio para Android y Windows. E2-T04 validó la demostración en un emulador Android y confirmó el arranque de Windows; todavía no implementa persistencia, motor de disponibilidad, servidor, sincronización ni recordatorios.

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

E2-T04 volvió a confirmar que el proceso crea una ventana receptiva titulada `Tiempo en Orden`. La herramienta de captura no expuso esa ventana WinUI como seleccionable, por lo que el recorrido de Windows continúa pendiente de comprobación manual. La lista exacta está en `docs/PRUEBAS-E2.md`.

## Ejecutar en Android con el AVD preparado

E2-T04 creó un único AVD reutilizable llamado `TiempoEnOrden_API_36` con Google APIs, API 36 y x86_64. No hay que crear otro para repetir la prueba.

```powershell
& "$env:LOCALAPPDATA\Android\Sdk\emulator\emulator.exe" -avd TiempoEnOrden_API_36
& "$env:LOCALAPPDATA\Android\Sdk\platform-tools\adb.exe" devices -l
dotnet build .\src\TiempoEnOrden.App\TiempoEnOrden.App.csproj -f net10.0-android -c Debug --no-restore `
  -p:EmbedAssembliesIntoApk=true
& "$env:LOCALAPPDATA\Android\Sdk\platform-tools\adb.exe" install -r `
  .\src\TiempoEnOrden.App\bin\Debug\net10.0-android\mx.tiempoenorden.app-Signed.apk
```

La instalación directa necesita `EmbedAssembliesIntoApk=true`; un APK de depuración preparado para Fast Deployment no contiene por sí solo los ensamblados auxiliares. Las capturas y resultados están en `docs/PRUEBAS-E2.md`. La ejecución en AVD no sustituye la validación pendiente en un teléfono real.
