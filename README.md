# Tiempo en Orden

Agenda local para Android y Windows creada con .NET MAUI. La entrega actual guarda datos en SQLite en el dispositivo: clientes, servicios, citas, tiempo personal y horario laboral. No tiene nube, cuentas, pagos, servidor, sincronización ni recordatorios coordinados.

## Funciones disponibles

- Crear clientes y servicios; los servicios conservan duración, precio y márgenes para futuras citas.
- Crear, consultar, editar y cancelar citas. Cancelar conserva el historial y libera disponibilidad.
- Consultar Agenda diaria y semanal, con filtros de negocio, personal y canceladas.
- Proteger tiempo personal, configurar jornada, descanso y días laborales.
- Detectar cruces considerando preparación, servicio y limpieza; no compara una cita consigo misma al editar.
- Proponer hasta tres horarios alternativos en intervalos de 15 minutos y un horizonte de 14 días. Elegir una opción solo modifica el borrador: la persona debe guardar la cita.
- Crear y restaurar el último respaldo local, tras confirmación.

Los datos son locales a cada dispositivo. Cambiar un servicio no modifica las citas existentes y ningún compromiso se mueve de forma automática.

## Requisitos de compilación

- .NET SDK 10.0.113, fijado por `global.json`.
- Carga MAUI 10.0.110.
- Microsoft OpenJDK 21.0.12.1.
- Android SDK Platform 36, Build Tools 36.0.0 y Platform Tools.
- Para Windows: `net10.0-windows10.0.19041.0`; las herramientas SDK se restauran transitivamente.

La información de entorno y las decisiones están en `docs/DECISIONES.md`.

## Compilar y verificar

En una terminal donde `dotnet --version` resuelva 10.0.113:

```powershell
dotnet restore .\TiempoEnOrden.sln
dotnet run --project .\tests\TiempoEnOrden.Verification\TiempoEnOrden.Verification.csproj
dotnet build .\src\TiempoEnOrden.App\TiempoEnOrden.App.csproj -f net10.0-android -c Debug --no-restore
dotnet build .\src\TiempoEnOrden.App\TiempoEnOrden.App.csproj -f net10.0-windows10.0.19041.0 -c Debug --no-restore
```

El verificador ejercita persistencia tras reabrir, edición/cancelación, historial, horario, tiempo personal, conflictos, sugerencias y respaldo/restauración. No sustituye las pruebas manuales de interfaz ni un teléfono real.

Si la terminal usa el SDK global anterior, usar temporalmente:

```powershell
& "$env:USERPROFILE\.dotnet\dotnet.exe" --version
```

## Ejecutar

Windows:

```powershell
dotnet run --project .\src\TiempoEnOrden.App\TiempoEnOrden.App.csproj -f net10.0-windows10.0.19041.0 --no-launch-profile
```

Android con el AVD existente `TiempoEnOrden_API_36`:

```powershell
& "$env:LOCALAPPDATA\Android\Sdk\emulator\emulator.exe" -avd TiempoEnOrden_API_36
& "$env:LOCALAPPDATA\Android\Sdk\platform-tools\adb.exe" devices -l
& "$env:USERPROFILE\.dotnet\dotnet.exe" build .\src\TiempoEnOrden.App\TiempoEnOrden.App.csproj -f net10.0-android -c Debug -p:EmbedAssembliesIntoApk=true
& "$env:LOCALAPPDATA\Android\Sdk\platform-tools\adb.exe" install -r .\src\TiempoEnOrden.App\bin\Debug\net10.0-android\mx.tiempoenorden.app-Signed.apk
```

Una APK con ensamblados incluidos es necesaria para la instalación directa por ADB. El AVD no reemplaza un teléfono real.

## Entrega y documentación

- [Manual breve](docs/MANUAL.md)
- [Guía técnica](docs/GUIA-TECNICA.md)
- [Guion de exposición](docs/GUION-EXPOSICION.md)
- [Matriz de pruebas de entrega](docs/PRUEBAS-ENTREGA.md)
- [Paquete de entrega](docs/PAQUETE-ENTREGA.md)
- [Pruebas originales de E2](docs/PRUEBAS-E2.md)

Los paquetes generados se colocan en `artifacts/release/` y se excluyen del control de versiones. Para abrir el paquete Release de Windows y comprobar sus dependencias efectivas (incluidos Windows App Runtime y Visual C++), seguir las instrucciones exactas de [MANUAL.md](docs/MANUAL.md). El APK Release final se generó, pero su instalación en el AVD quedó bloqueada; no se presenta como probado. La versión actual admite citas de negocio dentro de una jornada del mismo día, no citas que crucen medianoche.
