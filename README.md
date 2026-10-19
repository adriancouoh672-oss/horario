# Tiempo en Orden

Base de desarrollo de una agenda personal y de negocio para Android y Windows. Esta solución todavía no implementa la agenda, persistencia, disponibilidad, servidor, sincronización ni recordatorios.

## Estructura

- `src/TiempoEnOrden.App`: aplicación .NET MAUI con XAML para Android y Windows.
- `src/TiempoEnOrden.Core`: biblioteca C# compartida e independiente de MAUI.

La aplicación depende de `Core`; `Core` no depende de la aplicación.

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
