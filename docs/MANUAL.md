# Manual breve — paquete Release `afac4de`

## Localizar el paquete conservado

- Carpeta de trabajo: `artifacts/release/android/` y `artifacts/release/windows/`.
- Copia conservada: `artifacts/entrega-afac4de-20261019.zip`.
- APK: `android/mx.tiempoenorden.app-Signed.apk`.
- Windows: ejecutar desde la carpeta completa `windows/`, no copiar solo el `.exe`.

Los binarios están fuera de Git por la regla del repositorio que excluye resultados de compilación. La copia ZIP contiene las carpetas `android` y `windows` de esta entrega.

## Instalación Android

1. Copia `mx.tiempoenorden.app-Signed.apk` al teléfono.
2. Autoriza la instalación desde ese origen si Android lo solicita y abre la aplicación.

Las pruebas anteriores en el AVD comprobaron una compilación de desarrollo y el primer hito de persistencia. La instalación del **APK Release final** se intentó el 2026-10-19, pero ADB mantuvo el AVD en estado `offline`; por ello esta APK Release no se marca como instalada ni recorrida. La prueba en teléfono físico también sigue pendiente.

## Abrir Windows exactamente

Esta publicación es x64, sin MSIX y framework-dependent (`WindowsPackageType=None`). Requiere Windows 10 1809/x64 o posterior y se abre así desde PowerShell:

```powershell
$packageRoot = 'C:\Users\adria\OneDrive\Documents\ChatGPT\Tiempo en orden\artifacts\release\windows'
Set-Location -LiteralPath $packageRoot
.\TiempoEnOrden.App.exe
```

Antes de abrirla en otro equipo, instala y comprueba estas dependencias x64:

1. **.NET Desktop Runtime 10** (la publicación pide `Microsoft.NETCore.App` 10.0.x).
2. **Windows App Runtime 1.8, canal estable, x64**, compatible con `Microsoft.WindowsAppSDK 1.8.260529003`. La carpeta incluye `Microsoft.WindowsAppRuntime.Bootstrap.dll`, que inicializa la dependencia, pero no sustituye el runtime MSIX instalado en el equipo.
3. **Microsoft Visual C++ Redistributable x64** vigente.

Comprobación de Windows App Runtime:

```powershell
Get-AppxPackage 'Microsoft.WindowsAppRuntime.1.8*' |
  Select-Object Name, Version, Architecture, PackageFullName
```

En el equipo de empaquetado se observó `Microsoft.WindowsAppRuntime.1.8` x64 versión `8000.994.2142.0`; esto no demuestra que esté instalado en otro equipo. Descarga el instalador x64 de Windows App Runtime desde la página oficial de [Windows App SDK](https://learn.microsoft.com/windows/apps/windows-app-sdk/downloads) y el redistribuible desde [Visual C++](https://learn.microsoft.com/cpp/windows/latest-supported-vc-redist). La guía oficial para aplicaciones WinUI sin empaquetar confirma que el runtime de Windows App SDK debe distribuirse o instalarse aparte. [Microsoft Learn](https://learn.microsoft.com/windows/apps/windows-app-sdk/deploy-unpackaged-apps)

El recorrido visual manual de Windows está pendiente: que el `.exe` arranque no equivale a que todas las pantallas estén aprobadas.

## Alcance funcional y límites

- Esta versión admite **citas de negocio cuya preparación, servicio y limpieza caben en una jornada del mismo día**.
- No promete ni acepta citas nocturnas de negocio que crucen medianoche. El requisito futuro conserva fechas de inicio/fin, pero esa capacidad no está implementada en el paquete `afac4de`.
- Tiempo personal, clientes, servicios, citas, horario, descansos, conflictos, sugerencias y cancelación son locales al dispositivo.
- Las sugerencias nunca mueven ni guardan una cita sin una acción posterior de la persona.
- El respaldo se crea en el almacenamiento privado interno de la aplicación. Solo restaura el respaldo más reciente de ese mismo almacenamiento; reemplaza los datos actuales tras confirmación, no tiene deshacer, no se comparte, no se sincroniza, no se exporta para otro equipo y no es un respaldo en nube.
- No hay cuentas, nube, pagos, servidor, recordatorios coordinados ni reprogramación automática.

## Recorrido manual Release — 10 minutos (pendiente)

Usa una fecha futura `D` entre lunes y sábado. En Ajustes conserva o define jornada 09:00–18:00 y descanso 14:00–15:00. Este recorrido debe ejecutarse sobre el paquete Release y registrar cada resultado en `docs/PRUEBAS-ENTREGA.md`; **no está aprobado todavía**.

| Minuto | Acción y comprobación esperada |
| --- | --- |
| 0–1 | Abre el paquete Release. En **Clientes**, crea `Ana Release` y confirma que aparece en la lista. |
| 1–2 | En **Servicios**, crea `Corte Release`: 30 min, preparación 5 min, limpieza 5 min, MXN 250. |
| 2–3 | Crea una cita para `D` a las 10:00 con Ana y Corte. En Agenda confirma preparación 09:55–10:00, servicio 10:00–10:30 y limpieza 10:30–10:35. |
| 3–4 | Cierra completamente la aplicación, ábrela de nuevo y confirma cliente, servicio y cita. |
| 4–5 | Edita la cita, agrega la nota `editada Release` y guarda. Confirma que conserva la cita. |
| 5–6 | Inicia otra cita del mismo servicio a las 10:15. Confirma que no se guarda por cruce con la ocupación existente. |
| 6–7 | En ese borrador conflictivo, elige una alternativa sugerida. Confirma que solo cambia fecha/hora del borrador; descártalo sin guardar. |
| 7–8 | En Ajustes elige **Crear respaldo local**. Vuelve a Agenda, cancela la cita y confirma que desaparece de la vista normal; activa **Mostrar canceladas** y confírmala en historial. |
| 8–9 | En Ajustes elige **Restaurar último respaldo**, confirma y verifica que la cita activa con la nota editada reaparece. |
| 9–10 | Anota plataforma, versión de Windows/Android, dependencias instaladas, resultado de cada paso y cualquier captura real. |

No elijas una hora nocturna ni una que cruce medianoche para este recorrido: esa capacidad no pertenece a esta versión.
