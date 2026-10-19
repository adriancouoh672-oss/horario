# Instrucciones de trabajo para Tiempo en Orden

## Propósito

Este repositorio contiene una agenda personal y de negocio para Android y Windows. El trabajo se realiza por etapas pequeñas, verificables y documentadas. No se debe iniciar una etapa posterior sin una solicitud explícita del responsable.

## Documentos que se leen antes de trabajar

Antes de modificar código o documentación, leer en este orden:

1. `AGENTS.md`
2. `PROYECTO.md`
3. `docs/REQUISITOS.md`
4. `docs/DECISIONES.md`
5. `TAREAS.md`
6. `AVANCES.md`

Si los documentos se contradicen, detener el cambio que dependa de la contradicción, registrarla en `AVANCES.md` y pedir una decisión al responsable.

## Reglas de implementación

- Usar C# para la lógica del producto y XAML para la interfaz cuando comience la implementación .NET MAUI.
- Separar reglas de agenda, disponibilidad y conflictos de las vistas y de las integraciones específicas de Android o Windows.
- Mantener modelos y reglas compartidos entre plataformas. Encapsular notificaciones, permisos y otras funciones del sistema operativo detrás de interfaces.
- No mover ni modificar compromisos de forma automática sin una acción explícita del usuario.
- No mezclar datos de demostración con datos persistentes reales.
- No incluir contraseñas, tokens, claves, datos personales reales, binarios ni resultados de compilación en el repositorio.
- No agregar servidor, sincronización, recordatorios coordinados ni selección automática de dispositivo antes de que la etapa correspondiente sea autorizada.
- Consultar documentación oficial vigente antes de fijar versiones, paquetes o requisitos de plataforma. Registrar la elección en `docs/DECISIONES.md`.
- Preservar trabajo existente y evitar capas, dependencias o abstracciones que todavía no resuelvan una necesidad comprobada.

## Verificación y evidencia

- Cada requisito funcional debe tener identificador y criterio de aceptación en `docs/REQUISITOS.md`.
- Cada tarea debe indicar requisito relacionado, dependencia y evidencia esperada en `TAREAS.md`.
- Al cerrar una tarea, ejecutar las comprobaciones posibles y registrar comandos y resultados reales en `AVANCES.md`.
- Distinguir con precisión entre: creado, compilado, ejecutado, probado automáticamente, probado manualmente, simulado y pendiente.
- No declarar Android o Windows como probado si solo se verificó la otra plataforma.
- Las pruebas automáticas de reglas no sustituyen pruebas físicas de permisos, notificaciones, segundo plano, reinicio o sincronización entre dispositivos.

## Cierre de cada encargo

Actualizar `AVANCES.md` con:

- etapa y tarea;
- resultado y funciones aún no disponibles;
- archivos creados o modificados;
- validaciones ejecutadas y plataforma;
- prueba manual propuesta;
- problemas o bloqueos;
- decisiones tomadas;
- siguiente tarea pequeña, con dependencia y criterio de cierre.

Detenerse al completar la etapa autorizada.
