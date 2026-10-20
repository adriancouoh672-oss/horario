# Pruebas de ejecución de la etapa 2

Fecha de ejecución: 2026-10-19.

Alcance: aplicación MAUI de demostración de E2-T03. No se probaron persistencia, disponibilidad, conflictos, servidor, sincronización ni recordatorios porque todavía no están implementados. Los resultados `aprobado` se limitan al entorno y recorrido indicados.

## Entornos

- Windows 25H2, build 26200.9457, aplicación sin empaquetar `net10.0-windows10.0.19041.0`.
- Android Emulator 37.1.11 con un único AVD `TiempoEnOrden_API_36`, Pixel 5, Google APIs, Android 16/API 36, x86_64 y aceleración WHPX.
- No se encontró un teléfono Android conectado o autorizado. La validación en teléfono real permanece pendiente.

## Resultados

| Plataforma | Entorno | Recorrido | Resultado | Evidencia | Pendiente |
| --- | --- | --- | --- | --- | --- |
| Windows | Windows 25H2, compilación x64 sin empaquetar | Compilar el destino Windows | aprobado | `dotnet build ... -f net10.0-windows10.0.19041.0`: 0 advertencias, 0 errores | Ninguno para compilación. |
| Windows | Proceso local `TiempoEnOrden.App` | Arrancar e identificar proceso y ventana | aprobado | Proceso receptivo, título `Tiempo en Orden` y manejador principal no nulo; se cerró al terminar | El arranque no demuestra navegación. |
| Windows | Herramienta de control de ventanas | Captura e interacción con la ventana WinUI | bloqueado | La herramienta encontró el proceso, pero informó `windows: []`; el mismo manejador apareció atribuido a otra aplicación, por lo que no se interactuó con ella | Ejecutar el recorrido manual descrito abajo y registrar capturas MAUI reales. |
| Windows | Aplicación MAUI | Inicio, navegación, agenda, filtros, borrador contextual, descarte, tiempo personal, texto ampliado, foco y reinicio | no ejecutado | No hubo una ventana seleccionable para interacción segura | Validación manual del responsable. |
| Android | AVD `TiempoEnOrden_API_36` | Compilar, instalar y arrancar | aprobado | APK de depuración con ensamblados incluidos; `adb install -r`: `Success`; actividad enfocada y PID activo | Repetir en teléfono real cuando esté disponible. |
| Android | AVD API 36 | Inicio: fecha, próxima cita y contador/lista | aprobado | [captura de Inicio](pruebas-e2/android-inicio.png); jerarquía observada con `3 compromisos pendientes` | Teléfono real pendiente. |
| Android | AVD API 36 | Abrir Agenda y cambiar día, semana y filtros | aprobado | [captura de semana](pruebas-e2/android-agenda-semana.png); Día mostró 4 filas, Semana las mismas 4, Negocio 1 compromiso y semana siguiente Personal 0 | Teléfono real pendiente. |
| Android | AVD API 36 | Navegar por Inicio, Agenda, Más, Clientes, Servicios, Tiempo personal y Ajustes | aprobado | Textos de cada pantalla observados mediante la jerarquía nativa de Android | Teléfono real pendiente. |
| Android | AVD API 36 | Modificar borrador; crear cliente y servicio contextuales; regresar | aprobado | Cliente y servicio nuevos quedaron seleccionados; fecha `6 nov 2026`, hora `10:00` y notas modificadas se conservaron al regresar; [captura de Nueva cita](pruebas-e2/android-nueva-cita.png) | Las altas son solo memoria de demostración. |
| Android | AVD API 36 | Descartar un borrador modificado | aprobado | [captura de confirmación](pruebas-e2/android-confirmar-descarte.png); se corrigió el indicador de cambios para no perder la confirmación al volver de una creación contextual | Teléfono real pendiente. |
| Android | AVD API 36 | Agregar tiempo personal de demostración | aprobado | Mensaje observado: `Agregado solo a los datos ficticios en memoria. Se perderá al reiniciar.` | No es persistencia real. |
| Android | AVD API 36 | Teclado abierto y botón Atrás | aprobado | `mInputShown=true`; [captura con teclado](pruebas-e2/android-cita-teclado.png); Atrás cerró el teclado conservando el texto y también regresó desde pantallas secundarias | Teléfono real pendiente. |
| Android | AVD API 36, escala de fuente 1.3 | Texto ampliado, desplazamiento, foco y controles | aprobado | [captura de texto ampliado](pruebas-e2/android-texto-ampliado-foco.png) y [captura de foco por teclado](pruebas-e2/android-foco-teclado.png); desplazamiento vertical mantuvo accesibles las acciones | Comprobar gestos y tamaño del dispositivo real. |
| Android | AVD API 36 | Reiniciar y restablecer la demostración | aprobado | Tras `am force-stop` y arranque en frío: Inicio volvió a 3 pendientes y el día siguiente, filtro Personal, volvió a 0; la escala se restauró a 1.0 | Teléfono real pendiente. |
| Android | Teléfono físico | Todos los recorridos anteriores | no ejecutado | `adb devices -l` no mostró un teléfono conectado antes de crear el AVD | Conectar y autorizar un teléfono compatible; no declarar aprobado hasta ejecutar allí. |

## Correcciones realizadas durante E2-T04

1. El indicador de cambios del borrador ya no se reinicia al volver de crear un cliente o servicio contextual. La confirmación de descarte vuelve a mostrarse.
2. La cultura de presentación se fija en `es-MX`, de modo que las fechas de la interfaz permanecen en español aunque el dispositivo use otro idioma. La zona horaria continúa siendo una decisión separada.

## Recorrido manual pendiente en Windows

1. Ejecutar el comando de Windows del `README.md` y confirmar que la ventana muestra Inicio, tres compromisos pendientes y la próxima cita.
2. Recorrer Inicio, Agenda, Clientes, Servicios, Ajustes, Nueva cita y Tiempo personal con la navegación lateral.
3. En Agenda, cambiar Día/Semana, fecha, filtros y visibilidad de canceladas; revisar la escala temporal uniforme.
4. Modificar el borrador, crear cliente y servicio contextuales, volver y confirmar que los datos se conservan.
5. Elegir `Descartar cambios` y comprobar que aparece la confirmación.
6. Agregar tiempo personal de demostración, reiniciar y comprobar el restablecimiento.
7. Aumentar el tamaño del texto de Windows, recorrer con Tab/Mayús+Tab y comprobar desplazamiento, foco visible y ausencia de recortes.
8. Registrar cada resultado como aprobado, fallido, bloqueado o no ejecutado y adjuntar capturas reales de MAUI.

