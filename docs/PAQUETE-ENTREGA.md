# Paquete de entrega local

Generado el 2026-10-19. Los binarios no se versionan porque el repositorio excluye resultados de compilación; están disponibles en el espacio de trabajo bajo `artifacts/release/`.

| Plataforma | Archivo o carpeta | Tipo | Comprobación |
| --- | --- | --- | --- |
| Android | `android/mx.tiempoenorden.app-Signed.apk` | APK Release local, 31,995,039 bytes | SHA-256 `4E669D0BCBD9F7C497026ED2527A472FA7239FFA1670F92F14DACFF4B2642399` |
| Windows | `windows/` | Publicación Release sin empaquetar, x64, 155,960,412 bytes | Ejecutable SHA-256 `31C00BB3D39F4BEB020E27730FDEE33FBCC7CAA79EED20C4BE87C9DAA48982E4` |

## Cómo se generaron

```powershell
& "$env:USERPROFILE\.dotnet\dotnet.exe" publish .\src\TiempoEnOrden.App\TiempoEnOrden.App.csproj -f net10.0-android -c Release --no-restore -p:AndroidPackageFormats=apk -p:EmbedAssembliesIntoApk=true -o .\artifacts\release\android
& "$env:USERPROFILE\.dotnet\dotnet.exe" publish .\src\TiempoEnOrden.App\TiempoEnOrden.App.csproj -f net10.0-windows10.0.19041.0 -c Release --no-restore -p:WindowsPackageType=None -p:PublishReadyToRun=false -o .\artifacts\release\windows
```

El APK usa la firma de desarrollo local. Antes de distribución pública se requiere una clave de firma de lanzamiento que no se incluye en este repositorio. Consulta [MANUAL.md](MANUAL.md) para instalar y [PRUEBAS-ENTREGA.md](PRUEBAS-ENTREGA.md) para el estado de validación real.
