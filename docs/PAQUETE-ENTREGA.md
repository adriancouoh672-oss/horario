# Paquete de entrega local

Generado el 2026-10-19. Los binarios no se versionan porque el repositorio excluye resultados de compilación; están disponibles en el espacio de trabajo bajo `artifacts/release/`.

La copia conservada del cierre es `artifacts/entrega-afac4de-20261019.zip`, con SHA-256 `F8C0507A93C11159A1A45B4A76BD7B93311C4E791595B327D42F986E3D1CF78F` y 120,159,876 bytes.

| Plataforma | Archivo o carpeta | Tipo | Comprobación |
| --- | --- | --- | --- |
| Android | `android/mx.tiempoenorden.app-Signed.apk` | APK Release local, 31,995,039 bytes | SHA-256 `4E669D0BCBD9F7C497026ED2527A472FA7239FFA1670F92F14DACFF4B2642399` |
| Windows | `windows/` | Publicación Release sin empaquetar, x64, 155,960,412 bytes | Ejecutable SHA-256 `31C00BB3D39F4BEB020E27730FDEE33FBCC7CAA79EED20C4BE87C9DAA48982E4` |

## Dependencias efectivas de Windows

`TiempoEnOrden.App.csproj` fija `WindowsPackageType=None`. Por tanto, no es MSIX ni una distribución autosuficiente: se debe conservar la carpeta `windows` completa y el equipo destino necesita:

- Windows 10 versión 1809/x64 o posterior.
- .NET Desktop Runtime 10 x64 (`Microsoft.NETCore.App` 10.0.x según el archivo `TiempoEnOrden.App.runtimeconfig.json`).
- Windows App Runtime 1.8 estable x64, compatible con `Microsoft.WindowsAppSDK 1.8.260529003` observado en `TiempoEnOrden.App.deps.json`.
- Microsoft Visual C++ Redistributable x64 vigente.

La carpeta publicada contiene `Microsoft.WindowsAppRuntime.Bootstrap.dll`, `Microsoft.WindowsAppRuntime.Bootstrap.Net.dll` y `Microsoft.WindowsAppRuntime.dll`; estos inicializan la dependencia, pero no equivalen al runtime Windows App SDK instalado. La inspección del equipo de empaquetado encontró Windows App Runtime 1.8 x64 `8000.994.2142.0`; no se infiere su presencia en otros equipos. Consulta las instrucciones exactas de comprobación e instalación en [MANUAL.md](MANUAL.md).

## Cómo se generaron

```powershell
& "$env:USERPROFILE\.dotnet\dotnet.exe" publish .\src\TiempoEnOrden.App\TiempoEnOrden.App.csproj -f net10.0-android -c Release --no-restore -p:AndroidPackageFormats=apk -p:EmbedAssembliesIntoApk=true -o .\artifacts\release\android
& "$env:USERPROFILE\.dotnet\dotnet.exe" publish .\src\TiempoEnOrden.App\TiempoEnOrden.App.csproj -f net10.0-windows10.0.19041.0 -c Release --no-restore -p:WindowsPackageType=None -p:PublishReadyToRun=false -o .\artifacts\release\windows
```

El APK usa la firma de desarrollo local. Antes de distribución pública se requiere una clave de firma de lanzamiento que no se incluye en este repositorio. Las pruebas anteriores del AVD corresponden a una compilación de desarrollo; la instalación del APK Release final quedó bloqueada con ADB `offline`. Consulta [MANUAL.md](MANUAL.md) para instalar y [PRUEBAS-ENTREGA.md](PRUEBAS-ENTREGA.md) para el estado de validación real.
