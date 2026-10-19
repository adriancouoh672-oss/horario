# Registro de decisiones

Las decisiones se registran antes de convertirse en dependencias difíciles de cambiar.

## DEC-0001. Desarrollo por etapas

- Fecha: 2026-10-18
- Estado: aceptada
- Decisión: ejecutar una sola etapa por encargo y detenerse con evidencia y reporte.
- Motivo: reduce riesgo, hace visible el aprendizaje y permite que el responsable comprenda y revise el trabajo.
- Consecuencia: las etapas 1 a 8 no están autorizadas por el inicio de la etapa 0.

## DEC-0002. Reglas deterministas para disponibilidad

- Fecha: 2026-10-18
- Estado: aceptada como dirección del producto
- Decisión: el motor de disponibilidad usará reglas explícitas en C# y no dependerá de IA generativa.
- Motivo: los conflictos y sugerencias deben ser explicables, repetibles y probables automáticamente.
- Consecuencia: la lógica se mantendrá separada de MAUI y de la persistencia.

## DEC-0003. Plataformas iniciales

- Fecha: 2026-10-18
- Estado: aceptada como alcance
- Decisión: orientar la primera entrega a Android y Windows.
- Motivo: corresponde al escenario del usuario inicial.
- Evidencia de viabilidad general: la documentación oficial de .NET MAUI permite compilar Android y Windows desde Windows; el equipo cumple el requisito general de sistema operativo.
- Pendiente: comprobar compilación y ejecución reales de cada plataforma en la etapa 2.

## DEC-0004. Tecnología candidata

- Fecha: 2026-10-18
- Estado: propuesta, no fijada
- Decisión candidata: C#/.NET MAUI con XAML, SQLite local y, en una entrega posterior, ASP.NET Core y SignalR.
- Motivo: permite compartir modelos y reglas entre Android y Windows, conservar integraciones específicas por plataforma y seguir la arquitectura de la guía.
- Pendiente: elegir versiones objetivo y paquetes solo después de una prueba mínima reproducible y de consultar las tablas de versiones vigentes.

## DEC-0005. Persistencia local antes que sincronización

- Fecha: 2026-10-18
- Estado: aceptada como secuencia
- Decisión: crear primero una base local funcional; la API y sincronización se incorporarán únicamente después de estabilizar reglas y persistencia.
- Motivo: reduce variables y permite demostrar valor sin depender de alojamiento ni conexión.

## DEC-0006. Datos ficticios para demostraciones

- Fecha: 2026-10-18
- Estado: aceptada
- Decisión: capturas, pruebas y demostraciones usarán clientes y servicios ficticios.
- Motivo: proteger privacidad y producir evidencia reproducible.

## DEC-0007. Sin cronograma de desarrollo ni precio prematuros

- Fecha: 2026-10-18
- Estado: aceptada
- Decisión: no inventar un cronograma semanal de desarrollo ni fijar precio comercial hasta conocer fecha, rúbrica, experiencia, alcance de venta y costos operativos.
- Aclaración: esta decisión no elimina ni pospone la vista semanal de la agenda del producto, que permanece en REQ-A-008 y en la entrega A.
- Motivo: los datos del cronograma escolar y del precio siguen pendientes y no deben inventarse; la agenda semanal sí es una función definida del producto.

## DEC-0008. Una sola regla de disponibilidad

- Fecha: 2026-10-18
- Estado: aceptada como requisito de diseño
- Decisión: sugerencias y validación de guardado usarán la misma definición de ocupación, horario y conflicto.
- Motivo: evita que el sistema recomiende un espacio que después acepte o rechace con reglas diferentes.
- Consecuencia: toda sugerencia se revalida al confirmar; si dejó de estar libre, no se guarda ni se mueve otro compromiso.

## DEC-0009. Copia histórica del servicio en la cita

- Fecha: 2026-10-18
- Estado: aceptada como requisito funcional
- Decisión: al crear una cita se copian duración, preparación, limpieza, precio y moneda del servicio.
- Motivo: una modificación posterior del catálogo no debe reescribir el historial ni cambiar disponibilidad ya comprometida.
- Consecuencia: las citas nuevas usan el catálogo vigente; las existentes conservan su copia.

## DEC-0010. Sin movimientos automáticos

- Fecha: 2026-10-18
- Estado: aceptada como regla transversal
- Decisión: ningún compromiso se moverá, acortará, cancelará ni reemplazará sin una acción explícita del usuario.
- Motivo: una sugerencia es una opción, no autorización para reorganizar la agenda.
- Consecuencia: ante conflictos o cambios de horario, el sistema informa y espera una decisión.

## Revisión técnica asistida de DEC-0011 a DEC-0017

- Fecha de registro: 2026-10-19
- Resultado: las propuestas fueron contrastadas documentalmente con REQ-A-001 a REQ-A-009, revisadas en las tres capturas vigentes y recorridas mediante interacciones simuladas reportadas por Codex.
- Alcance de la evidencia: revisión técnica asistida del prototipo en navegador; no es aprobación del responsable, prueba con usuarios ni validación física en Android o Windows.
- Contradicciones: no se encontraron contradicciones entre DEC-0011 a DEC-0017 y los requisitos vigentes. Se corrigió únicamente una formulación en `docs/DISENO.md` que llamaba “resuelta” a la presentación de zona horaria; debe entenderse como propuesta técnica pendiente de aprobación.
- Estado común: las siete decisiones continúan como propuestas. La aprobación del responsable y la validación en teléfono real después de implementar una versión ejecutable siguen pendientes.

## DEC-0011. Datos mínimos de clientes y servicios

- Fecha: 2026-10-18
- Estado: propuesta revisada técnicamente en E1-T03; aprobación del responsable pendiente
- Decisión: cliente con nombre obligatorio y teléfono/notas opcionales. Servicio con nombre, duración, precio, moneda, preparación y limpieza.
- Motivo: reducir captura innecesaria sin perder la información requerida por citas, precios y disponibilidad.
- Consecuencia: los formularios distinguen campos obligatorios y opcionales; no se inventan más datos personales.

## DEC-0012. Tareas sin horario

- Fecha: 2026-10-18
- Estado: propuesta revisada técnicamente en E1-T03; aprobación del responsable pendiente
- Decisión: una tarea sin fecha y hora no ocupa disponibilidad.
- Motivo: un pendiente no representa por sí mismo un intervalo reservado.
- Consecuencia: las tareas sin horario aparecen en una lista de pendientes, no dentro de los cálculos de conflicto.

## DEC-0013. Parámetros iniciales de sugerencias

- Fecha: 2026-10-18
- Estado: propuesta revisada técnicamente en E1-T03; aprobación del responsable pendiente
- Decisión: buscar hasta tres opciones en intervalos de 15 minutos y con horizonte inicial de 14 días.
- Aclaración: la granularidad se aplica a la búsqueda automática; no impide registrar manualmente una hora válida distinta.
- Consecuencia: el diseño explica el periodo de búsqueda y permite ampliar el horizonte en una evolución posterior si se aprueba.

## DEC-0014. Presentación de la agenda

- Fecha: 2026-10-18
- Estado: propuesta revisada técnicamente en E1-T03; aprobación del responsable pendiente
- Decisión: semana desde lunes, configurable; canceladas ocultas por defecto con filtro; preparación y limpieza visibles y diferenciadas.
- Motivo: reducir ruido sin ocultar historial y hacer comprensible la ocupación real.
- Consecuencia: la semana móvil usa selector horizontal de días y detalle del día seleccionado; el escritorio aprovecha una cuadrícula semanal.

## DEC-0015. Zona horaria y compromisos entre fechas

- Fecha: 2026-10-18
- Estado: propuesta revisada técnicamente en E1-T03; aprobación del responsable pendiente
- Decisión: proponer la zona horaria del dispositivo y pedir confirmación. Inicio y fin conservan fecha para admitir medianoche.
- Motivo: evitar ambigüedad temporal y permitir compromisos que terminen al día siguiente.
- Consecuencia: Ajustes muestra la zona confirmada y Nueva cita enseña fecha de fin cuando cambia el día.

## DEC-0016. Archivo sin pérdida de historial

- Fecha: 2026-10-18
- Estado: propuesta revisada técnicamente en E1-T03; aprobación del responsable pendiente
- Decisión: archivar conserva el historial y oculta clientes o servicios de las selecciones nuevas por defecto.
- Motivo: impedir pérdida de referencias históricas sin saturar los formularios.
- Consecuencia: Clientes y Servicios ofrecen filtro para archivados; la reactivación queda pendiente de confirmar.

## DEC-0017. Lenguaje visual adaptable

- Fecha: 2026-10-18
- Estado: propuesta revisada técnicamente en E1-T03; aprobación del responsable pendiente
- Decisión: compartir jerarquía, colores semánticos, componentes y vocabulario entre Android y Windows, adaptando navegación y densidad a cada plataforma.
- Motivo: mantener coherencia sin forzar una interfaz idéntica en teléfono y escritorio.
- Consecuencia: Android usa navegación compacta y objetivos táctiles; Windows usa navegación lateral y mayor espacio para la agenda.

## DEC-0018. Naturaleza de los bocetos

- Fecha: 2026-10-18
- Estado: aceptada para E1-T02
- Decisión: los archivos visuales son prototipos estáticos de revisión, no una aplicación web ni evidencia de funciones implementadas.
- Motivo: validar diseño antes de programar la aplicación C#/.NET MAUI.
- Consecuencia: no contienen lógica de negocio, persistencia, servidor, sincronización ni promesas de funciones futuras.

## Referencias oficiales consultadas

- Plataformas compatibles: https://learn.microsoft.com/dotnet/maui/supported-platforms?view=net-maui-10.0
- Instalación y configuración: https://learn.microsoft.com/dotnet/maui/get-started/installation
- SQLite local: https://learn.microsoft.com/dotnet/maui/data-cloud/database-sqlite?view=net-maui-10.0
- SignalR: https://learn.microsoft.com/aspnet/core/signalr/introduction?view=aspnetcore-10.0

Estas referencias respaldan decisiones generales, no prueban que el proyecto compile en este equipo.
