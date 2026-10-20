# Guion de exposición — paquete Release `afac4de`

Duración sugerida: 6 a 8 minutos. Este guion describe capacidades implementadas y límites del paquete; el recorrido Release manual continúa pendiente de resultados del responsable.

1. **Propósito y alcance.** “Tiempo en Orden es una agenda local para un trabajador independiente. Guarda datos en el dispositivo; no incluye cuentas, nube, pagos ni recordatorios.”
2. **Catálogos.** En Clientes crea una persona ficticia. En Servicios crea un servicio con duración, preparación, limpieza, precio y moneda. Aclara que una cita conserva esos valores aunque el catálogo cambie después.
3. **Cita dentro de la jornada.** Crea una cita de negocio en un día laboral, por ejemplo a las 10:00. Muestra en Agenda los tres segmentos: preparación, servicio y limpieza.
4. **Persistencia local.** Cierra por completo la aplicación, vuelve a abrirla y confirma cliente, servicio y cita. Esta secuencia fue comprobada por el verificador de SQLite y antes en el AVD con una compilación de desarrollo; no afirmar que el APK Release final ya la completó en un dispositivo.
5. **Edición, conflicto y alternativas.** Edita la nota de la cita. Intenta otra dentro de su ocupación; explica que no se guarda. Muestra las sugerencias: elegir una solo actualiza el borrador y exige guardar explícitamente.
6. **Cancelación y agenda.** Cancela la cita y enseña que libera disponibilidad; activa el filtro de canceladas para mostrar el historial.
7. **Jornada y tiempo protegido.** Enseña Tiempo personal y Ajustes. Explica que descanso, jornada y días laborales se comprueban antes de guardar una cita. Esta versión solo admite citas de negocio que caben en una jornada del mismo día: no demostrar ni prometer citas nocturnas que crucen medianoche.
8. **Respaldo y cierre.** Crea un respaldo local y explica que se guarda dentro del almacenamiento privado del dispositivo. Restaurar el último respaldo reemplaza los datos actuales tras confirmación; no es nube, exportación, sincronización ni copia entre equipos.

Cierre sugerido: “La entrega tiene lógica local, persistencia y validación automatizada. Quedan pendientes el recorrido manual del paquete Release en Windows, su instalación Release en Android y la prueba en un teléfono real.”

No presentar como comprobados: instalación del APK Release final, navegación manual completa de Windows, prueba en teléfono real, respaldo transferible, sincronización ni soporte nocturno que cruce medianoche.
