# Manual breve — Tiempo en Orden

## Instalar

### Android

1. Copia `artifacts/release/android/mx.tiempoenorden.app-Signed.apk` al teléfono.
2. Ábrelo y autoriza la instalación desde ese origen si Android lo solicita.
3. Abre **Tiempo en Orden**.

El APK se comprobó en el AVD API 36. La prueba en teléfono físico sigue pendiente, por lo que esta instalación debe considerarse una validación manual solicitada al responsable.

### Windows

1. Descomprime el contenido de `artifacts/release/windows/` en una carpeta local.
2. Ejecuta `TiempoEnOrden.App.exe`.

El paquete de Windows es una publicación sin empaquetar para Windows x64 y requiere el runtime .NET 10 Desktop instalado. La aplicación se compiló y arrancó, pero el recorrido visual manual de Windows sigue pendiente.

## Recorrido de uso

1. En **Más > Clientes**, escribe el nombre y elige **Guardar cliente**.
2. En **Más > Servicios**, indica nombre, duración, precio, preparación y limpieza; elige **Guardar servicio**.
3. En **Inicio** o **Agenda**, elige **Nueva cita**, selecciona cliente y servicio, ajusta fecha/hora y guarda.
4. Consulta los segmentos preparación, servicio y limpieza en **Agenda**. Todos ocupan tiempo.
5. Para cambiar una cita, elige **Editar** en Agenda, modifica el borrador y usa **Guardar cambios**. Para eliminarla de la disponibilidad, usa **Cancelar cita** y confirma.
6. En **Tiempo personal**, guarda una actividad o bloqueo. En **Ajustes**, guarda jornada, descanso y días laborales.
7. Si un horario se cruza con una cita, descanso, actividad personal o jornada, no se guarda. Elige una de las alternativas sugeridas o ajusta el borrador manualmente.
8. En **Ajustes**, usa **Crear respaldo local**. **Restaurar último respaldo** reemplaza los datos actuales tras pedir confirmación.

## Límites de esta entrega

- Los respaldos son privados y locales al dispositivo; no se transfieren ni sincronizan.
- Las citas canceladas quedan ocultas de forma predeterminada. Activa **Mostrar canceladas** para ver el historial.
- No existen cuentas, nube, pagos, recordatorios ni reprogramación automática.
- No se ha validado todavía el recorrido completo en Windows ni en un teléfono Android real.
