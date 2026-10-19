# Requisitos de Tiempo en Orden

Estado: requisitos de la entrega A refinados en E1-T01. Describen comportamiento esperado, no funcionalidad implementada. El público inicial sigue siendo una hipótesis pendiente de entrevistas reales.

## Convenciones

- `Confirmado por guía`: forma parte del alcance propuesto y fue convertido en comportamiento comprobable.
- `Hipótesis`: debe validarse con usuarios, rúbrica o decisión explícita.
- Prioridad `P0`: necesaria para que la entrega A cumpla su propósito básico.
- Prioridad `P1`: importante, pero admite una primera demostración limitada si la dependencia queda declarada.
- Los textos entre comillas son mensajes provisionales; deberán revisarse en los bocetos y pruebas de usabilidad.
- Los ejemplos usan datos ficticios y no constituyen entrevistas, ventas ni fechas del proyecto.

## Vocabulario funcional provisional

- **Cita:** compromiso de negocio con inicio y fin; puede tener servicio y cliente. Su ocupación incluye preparación y limpieza.
- **Actividad personal:** compromiso personal con intervalo reservado; puede estar fuera del horario laboral y ocupa disponibilidad.
- **Bloqueo:** intervalo que el usuario marca expresamente como no disponible.
- **Descanso:** intervalo no laborable dentro de la configuración del horario; impide citas de negocio.
- **Tarea:** pendiente personal. Provisionalmente, una tarea sin fecha y hora no reserva tiempo ni afecta disponibilidad.
- **Horario laboral:** intervalos en los que pueden ubicarse servicios de negocio. No limita actividades personales.

## Reglas globales de la entrega A

1. Ningún compromiso se crea, mueve, acorta, cancela o sustituye sin una acción explícita del usuario.
2. La validación de guardado y las sugerencias usan una sola definición de disponibilidad.
3. Preparación y limpieza ocupan tiempo aunque no se muestren como una cita independiente.
4. Una cita cancelada conserva su registro, pero deja de ocupar disponibilidad.
5. Al editar una cita, esa misma cita se excluye de la comparación mediante su identificador estable.
6. Duración, preparación, limpieza, precio y moneda se copian a la cita; cambios posteriores del servicio no alteran citas existentes.
7. Los intervalos son contiguos, pero no se cruzan, cuando el fin de uno coincide exactamente con el inicio del otro.
8. La vista semanal de la agenda pertenece al producto. Lo que se pospone es inventar un cronograma semanal de desarrollo sin fecha ni rúbrica.

## Integración de propuestas técnicas de E1-T03

DEC-0011 a DEC-0017 se contrastaron con estos requisitos como resultado de una revisión técnica asistida. No se modificaron identificadores ni se detectaron contradicciones funcionales. El responsable aprobó estas decisiones y el diseño el 2026-10-19. La aprobación no sustituye la validación del producto implementado.

| Decisión | Requisitos relacionados | Alcance integrado |
| --- | --- | --- |
| DEC-0011 | REQ-A-001, REQ-A-002, REQ-A-003 | Datos mínimos de cliente y servicio usados por el formulario de cita. |
| DEC-0012 | REQ-A-004 | Las tareas sin horario no reservan disponibilidad. |
| DEC-0013 | REQ-A-007 | Hasta tres sugerencias, búsqueda cada 15 minutos y horizonte inicial de 14 días, sin limitar el registro manual. |
| DEC-0014 | REQ-A-006, REQ-A-008 | Semana desde lunes configurable, canceladas ocultas por defecto y márgenes diferenciados. |
| DEC-0015 | REQ-A-001, REQ-A-008, REQ-A-009 | Zona horaria propuesta por el dispositivo y confirmada; inicio y fin conservan fecha. |
| DEC-0016 | REQ-A-002, REQ-A-003, REQ-A-009 | Archivar conserva historial y excluye el registro de nuevas selecciones por defecto. |
| DEC-0017 | REQ-A-004, REQ-A-008 | Lenguaje visual compartido con adaptación a Android y Windows. |

Evidencia disponible en E1-T03: revisión documental realizada, diseño revisado mediante las tres capturas vigentes, pruebas de interacción del prototipo en navegador reportadas por Codex y aprobación explícita del responsable el 2026-10-19. Permanece pendiente la validación en un teléfono real, que requiere una versión ejecutable.

## Requisitos funcionales de la entrega A

### REQ-A-001. Gestionar citas

**Usuario y necesidad:** trabajador independiente que atiende por citas y necesita registrar y corregir compromisos de negocio sin perder control de su disponibilidad.

**Historia:** Como trabajador independiente, quiero crear, consultar, editar y cancelar citas, para mantener una agenda de trabajo correcta y saber qué tiempo sigue disponible.

**Prioridad y dependencias:** P0. Depende de REQ-A-002 para copiar datos del servicio, de REQ-A-006 para validar conflictos y de REQ-A-009 para conservar datos. El cliente de REQ-A-003 es opcional.

**Flujo principal:**

1. El usuario inicia una cita nueva.
2. Selecciona fecha y hora de inicio, servicio y, opcionalmente, cliente. Inicio y fin conservan fecha para admitir compromisos que cruzan medianoche.
3. El sistema calcula fin y ocupación total con preparación y limpieza.
4. El sistema valida datos, horario laboral, descansos, bloqueos y cruces.
5. El usuario confirma el guardado.
6. La cita aparece en la agenda con identificador estable y valores históricos del servicio.

**Alternativas:**

- Si falta un dato obligatorio, el sistema conserva lo capturado y señala el campo.
- Si existe conflicto, no guarda y permite corregir datos o solicitar espacios con REQ-A-007.
- Al editar, la cita actual se excluye del conjunto de conflictos.
- Al cancelar, se solicita confirmación; el registro cambia a cancelado y libera su intervalo.
- Si el fin calculado cae en el día siguiente, se muestra la fecha completa antes de confirmar.

**Validaciones y mensajes:** fin posterior al inicio; duración efectiva positiva; servicio válido; fecha y hora de inicio y fin válidas. Mensajes provisionales: “Revisa los datos indicados”, “Este horario entra en conflicto con otro compromiso” y “La cita fue cancelada y el horario volvió a estar disponible”.

**Criterios de aceptación:**

- Dado un servicio válido y un horario disponible, cuando el usuario confirma la cita, entonces se guarda y aparece en la agenda.
- Dada una cita existente, cuando el usuario abre su detalle, modifica un dato válido y confirma, entonces se conserva el mismo identificador y se muestran los datos actualizados.
- Dada una cita existente sin otros cruces, cuando el usuario la edita sin cambiar su horario, entonces la cita no se considera conflicto consigo misma.
- Dada una cita activa, cuando el usuario confirma su cancelación, entonces queda marcada como cancelada y su ocupación deja de bloquear horarios.
- Dado un conflicto, cuando el usuario intenta guardar, entonces la cita no se guarda ni se mueve automáticamente y se explica el motivo.
- Dada una cita guardada, cuando la aplicación se reinicia, entonces reaparece con el mismo estado y valores históricos.

**Ejemplo ficticio:** Ana López solicita “Corte clásico” para el 6 de noviembre de 2026 a las 17:00. El servicio dura 60 minutos, con 10 de preparación y 10 de limpieza; la ocupación validada es de 16:50 a 18:10. Ana es un personaje ficticio.

**Dudas o decisiones pendientes:** definir si cancelar requiere motivo y cómo resumir visualmente citas que abarcan dos fechas.

### REQ-A-002. Gestionar servicios

**Usuario y necesidad:** trabajador independiente que ofrece distintos servicios y necesita reutilizar duración, márgenes y precio al agendar.

**Historia:** Como trabajador independiente, quiero administrar un catálogo de servicios, para crear citas consistentes sin volver a capturar sus características cada vez.

**Prioridad y dependencias:** P0. Depende de REQ-A-009. Es dependencia de REQ-A-001, REQ-A-006 y REQ-A-007.

**Flujo principal:** el usuario crea un servicio con nombre, duración, preparación, limpieza, precio y moneda; revisa los datos y guarda. Puede consultar y editar el catálogo.

**Alternativas:** si el servicio ya fue usado, editar el catálogo solo afecta citas futuras. Un servicio archivado no se ofrece por defecto para nuevas citas, pero sigue visible en las existentes y puede consultarse mediante filtro.

**Validaciones y mensajes:** nombre requerido; duración mayor que cero; preparación y limpieza iguales o mayores que cero; precio no negativo; moneda requerida cuando hay precio. Mensajes provisionales: “Indica una duración mayor que cero” y “El cambio se aplicará solo a citas nuevas”.

**Criterios de aceptación:**

- Dado un servicio con datos válidos, cuando el usuario guarda, entonces queda disponible para nuevas citas.
- Dada una duración cero o negativa, cuando el usuario intenta guardar, entonces el sistema rechaza el servicio y señala la duración.
- Dada una cita creada con un servicio, cuando el usuario cambia después la duración, márgenes o precio del catálogo, entonces la cita conserva los valores copiados al crearla.
- Dado un servicio previamente utilizado, cuando se archiva, entonces no desaparece de las citas históricas.

**Ejemplo ficticio:** “Corte clásico” cuesta MXN 250, dura 45 minutos, requiere 10 de preparación y 5 de limpieza. Una cita conserva esos valores aunque el catálogo cambie después a MXN 280 y 50 minutos.

**Dudas o decisiones pendientes:** tratamiento de precio vacío, moneda inicial sugerida y posibilidad de reactivar servicios archivados.

### REQ-A-003. Gestionar clientes con datos mínimos

**Usuario y necesidad:** trabajador independiente que necesita reconocer a quién atenderá sin almacenar información innecesaria.

**Historia:** Como trabajador independiente, quiero registrar datos mínimos de clientes y asociarlos opcionalmente con citas, para identificar mis compromisos sin duplicar información.

**Prioridad y dependencias:** P1. Depende de REQ-A-009; es opcional para REQ-A-001.

**Flujo principal:** el usuario crea un cliente, captura nombre obligatorio y, opcionalmente, teléfono y notas; guarda y posteriormente puede seleccionarlo en una cita.

**Alternativas:** una cita puede guardarse sin cliente. Un cliente archivado no se ofrece por defecto para nuevas citas, pero conserva historial y puede consultarse mediante filtro; nunca se borran silenciosamente sus relaciones.

**Validaciones y mensajes:** nombre requerido; teléfono y notas opcionales; formato de teléfono validado solo si se captura; advertencia de duplicado exacto sin bloquear homónimos. Mensajes provisionales: “Indica el nombre del cliente” y “Archivar conserva sus citas y lo oculta de nuevas selecciones”.

**Criterios de aceptación:**

- Dado un cliente con datos mínimos válidos, cuando el usuario guarda, entonces queda disponible para asociarlo con una cita.
- Dada una cita nueva, cuando el usuario no selecciona cliente, entonces puede continuar si los demás datos son válidos.
- Dado un cliente asociado con citas, cuando el usuario intenta eliminarlo, entonces no se eliminan las citas ni se pierde su referencia histórica.
- Dado un cliente archivado, cuando se consulta una cita histórica, entonces sigue identificable.

**Ejemplo ficticio:** se registra “Luis Pérez” con teléfono ficticio `555-010-2040` y se asocia a una cita. El dato no corresponde a una persona real.

**Dudas o decisiones pendientes:** normalización de teléfono, búsqueda, confirmación de duplicados y posibilidad de reactivar clientes archivados.

### REQ-A-004. Registrar tiempo personal

**Usuario y necesidad:** persona que combina trabajo y vida personal y necesita impedir que una cita ocupe un compromiso personal.

**Historia:** Como usuario, quiero registrar actividades personales, tareas y bloqueos, para ver mi disponibilidad real junto con el trabajo.

**Prioridad y dependencias:** P0 para actividades y bloqueos con intervalo; P1 para tareas sin horario. Depende de REQ-A-006 y REQ-A-009.

**Flujo principal:** el usuario elige tipo personal, captura título, fecha, inicio y fin, revisa y guarda. El intervalo aparece en la agenda y ocupa disponibilidad.

**Alternativas:** una actividad personal puede estar fuera del horario laboral. Si entra en conflicto, se informa y el usuario decide corregir o cancelar la operación; el sistema no mueve ningún compromiso. Una tarea sin horario se muestra como pendiente y no ocupa disponibilidad.

**Validaciones y mensajes:** título identificable; fin posterior al inicio para elementos con intervalo. Mensajes provisionales: “La actividad personal se cruza con otro compromiso” y “Nada se moverá hasta que elijas qué cambiar”.

**Criterios de aceptación:**

- Dada una actividad personal fuera del horario laboral, cuando el usuario la guarda sin otro cruce, entonces se acepta y aparece en la agenda.
- Dado un bloqueo personal, cuando se buscan espacios para un servicio, entonces su intervalo no se ofrece.
- Dado un cruce con una cita, cuando el usuario intenta guardar una actividad, entonces se muestra el conflicto y ningún compromiso se mueve automáticamente.
- Dada una actividad guardada, cuando se reinicia la aplicación, entonces conserva datos y estado.
- Dados elementos personales y de negocio, cuando se muestran juntos, entonces pueden distinguirse por texto, icono o etiqueta además del color.
- Dada una tarea sin fecha y hora, cuando se calcula disponibilidad, entonces la tarea no bloquea ningún intervalo.

**Ejemplo ficticio:** “Clase nocturna” se registra de 18:30 a 20:00, aunque el horario laboral termine a las 18:00; ese intervalo no puede sugerirse para una cita.

**Dudas o decisiones pendientes:** definir estados de tarea, orden de pendientes y diferencia visual exacta entre actividad y bloqueo.

### REQ-A-005. Configurar horario laboral y descansos

**Usuario y necesidad:** trabajador independiente que necesita limitar servicios a sus horas reales de atención y proteger descansos.

**Historia:** Como trabajador independiente, quiero configurar horarios laborales y descansos por día, para que las citas y sugerencias respeten cuándo atiendo.

**Prioridad y dependencias:** P0. Depende de REQ-A-009; alimenta REQ-A-001, REQ-A-006 y REQ-A-007.

**Flujo principal:** el usuario define uno o más intervalos laborales por día y descansos dentro de ellos, revisa la configuración y guarda.

**Alternativas:** puede marcar un día sin trabajo. Si cambia el horario y ya existen citas fuera del nuevo intervalo, se advierte; las citas existentes no se mueven ni cancelan automáticamente.

**Validaciones y mensajes:** intervalos válidos y no invertidos; descansos dentro del periodo configurado o señalados como inválidos; cruces de configuración explicados. Mensajes provisionales: “El descanso debe estar dentro del horario laboral” y “Hay citas existentes fuera del nuevo horario; no se modificarán”.

**Criterios de aceptación:**

- Dado un horario de 09:00 a 18:00, cuando se intenta crear un servicio fuera de ese intervalo, entonces no se guarda sin corrección explícita.
- Dado un descanso de 14:00 a 15:00, cuando se buscan espacios, entonces no se ofrece un intervalo que lo ocupe total o parcialmente.
- Dada una actividad personal fuera del horario laboral, cuando se guarda, entonces el horario laboral no la rechaza por ese motivo.
- Dadas citas existentes, cuando el usuario cambia el horario laboral, entonces las citas no se mueven y se muestran las afectadas.

**Ejemplo ficticio:** lunes de 09:00 a 18:00 con descanso de 14:00 a 15:00. Un servicio que ocuparía de 13:50 a 14:50 se rechaza por invadir el descanso.

**Dudas o decisiones pendientes:** definir múltiples turnos por día, copia de horarios entre días, excepciones por fecha y cambios de horario estacional.

### REQ-A-006. Detectar conflictos

**Usuario y necesidad:** trabajador independiente que necesita evitar citas incompatibles y entender exactamente qué intervalo está ocupado.

**Historia:** Como usuario, quiero que el sistema detecte conflictos con reglas claras, para no aceptar compromisos que no puedo cumplir.

**Prioridad y dependencias:** P0. Depende de datos de REQ-A-001, REQ-A-002, REQ-A-004 y REQ-A-005. Debe usar identificadores de REQ-A-009.

**Flujo principal:** al crear o editar un compromiso, el sistema calcula su ocupación, compara contra intervalos activos y configuración, y devuelve válido o una lista explicable de conflictos.

**Alternativas:** citas canceladas se excluyen; la cita editada se excluye por identificador; intervalos contiguos se aceptan; ante conflicto no se modifica ningún registro automáticamente.

**Validaciones y mensajes:** regla de cruce `inicioA < finB` y `finA > inicioB`; ocupación de cita desde `inicio - preparación` hasta `fin del servicio + limpieza`. Mensaje provisional: “Este horario ocupa 10:50–12:10 y se cruza con ‘Clase’ a las 12:00”.

**Criterios de aceptación:**

- Dada una cita de servicio de 10:00 a 10:30 con 10 minutos de limpieza, cuando se intenta iniciar otra a las 10:35, entonces existe conflicto porque la ocupación termina a las 10:40.
- Dada esa misma cita, cuando otra inicia exactamente a las 10:40 y no hay otra restricción, entonces los intervalos son contiguos y no existe cruce.
- Dada una cita con 10 minutos de preparación, cuando otro compromiso termina después del inicio de esa preparación, entonces existe conflicto.
- Dada una cita cancelada, cuando se valida un nuevo compromiso en su intervalo, entonces la cita cancelada no bloquea el horario.
- Dada una cita existente, cuando se edita conservando su horario, entonces se excluye a sí misma y no produce un falso conflicto.
- Dado cualquier conflicto, cuando se informa al usuario, entonces ningún compromiso cambia hasta que el usuario ejecute una acción explícita.

**Ejemplo ficticio:** “Afeitado” inicia a las 11:00, dura 30 minutos, requiere 10 de preparación y 10 de limpieza; ocupa 10:50–11:40. Una actividad a las 11:35 entra en conflicto.

**Dudas o decisiones pendientes:** acordar precisión interna menor a 15 minutos, si fuera necesaria, y presentación cuando existen varios conflictos simultáneos.

### REQ-A-007. Sugerir espacios disponibles

**Usuario y necesidad:** trabajador independiente que necesita encontrar rápidamente alternativas válidas cuando una hora no cabe.

**Historia:** Como trabajador independiente, quiero recibir hasta tres horarios disponibles y explicados, para elegir una alternativa sin calcular manualmente todos los márgenes.

**Prioridad y dependencias:** P0 como diferenciador de la entrega A. Depende de REQ-A-002, REQ-A-005 y REQ-A-006.

**Flujo principal:** el usuario selecciona servicio y fecha inicial; el sistema busca en intervalos de 15 minutos durante un horizonte inicial de hasta 14 días, evalúa disponibilidad en orden determinista y muestra hasta tres resultados con ocupación y explicación. El usuario elige uno y confirma; el sistema vuelve a validarlo antes de guardar.

**Alternativas:** si no hay espacios se informa sin crear ni mover compromisos. Si un espacio deja de estar disponible antes de guardar, se rechaza y se ofrecen opciones actualizadas.

**Validaciones y mensajes:** periodo válido; servicio válido; misma regla de ocupación y conflictos que REQ-A-006. Mensajes provisionales: “No encontramos espacios válidos en este periodo” y “Este horario ya no está disponible; elige otra opción”.

**Criterios de aceptación:**

- Dado un servicio y un periodo con más de tres espacios, cuando se solicita búsqueda, entonces se muestran como máximo tres en orden determinista.
- Dado un espacio que invade preparación, limpieza, descanso, bloqueo o compromiso activo, cuando se calculan sugerencias, entonces ese espacio no se ofrece.
- Dada una sugerencia, cuando se muestra, entonces explica al menos hora de servicio y ocupación total.
- Dada una sugerencia seleccionada, cuando el usuario confirma, entonces el guardado ejecuta las mismas reglas de disponibilidad nuevamente.
- Dado que la sugerencia dejó de estar libre, cuando se confirma, entonces no se sobrescribe ni mueve nada y se solicita elegir otra opción.
- Dada la búsqueda automática, cuando se evalúan candidatos, entonces avanzan en intervalos de 15 minutos y no exceden el horizonte inicial de 14 días.
- Dada una hora capturada manualmente que no coincide con un intervalo de 15 minutos, cuando cumple las reglas, entonces puede validarse y guardarse; la granularidad limita la búsqueda, no la captura manual.

**Ejemplo ficticio:** para “Corte clásico” de 45 minutos, 10 de preparación y 5 de limpieza, se muestran 09:10, 11:30 y 16:00, indicando que cada opción reserva 60 minutos totales.

**Dudas o decisiones pendientes:** preferencia por horas tempranas frente a mejor aprovechamiento de huecos y posibilidad de ampliar manualmente el horizonte.

### REQ-A-008. Consultar agenda diaria y semanal

**Usuario y necesidad:** persona que coordina vida y negocio y necesita comprender sus compromisos de hoy y de la semana.

**Historia:** Como usuario, quiero consultar mi agenda por día y por semana, con filtros y próxima cita, para planear mi tiempo personal y laboral.

**Prioridad y dependencias:** P0. Depende de REQ-A-001, REQ-A-004 y REQ-A-009; utiliza datos de servicios y horarios para contexto.

**Flujo principal:** el usuario abre la agenda, ve el día actual, cambia entre vista diaria y semanal, navega entre fechas y puede filtrar personal o negocio. Desde un elemento abre su detalle.

**Alternativas:** si no hay elementos se muestra estado vacío; si falla la lectura se ofrece explicación y reintento; un filtro sin resultados no se confunde con pérdida de datos.

**Validaciones y mensajes:** orden cronológico estable; semana iniciada en lunes con preferencia configurable; canceladas ocultas por defecto y disponibles mediante filtro; márgenes visibles y diferenciados; elementos distinguibles sin depender solo del color. Mensajes provisionales: “No hay compromisos para este día”, “El filtro actual no tiene resultados” y “No pudimos cargar la agenda; intenta de nuevo”.

**Criterios de aceptación:**

- Dados compromisos personales y de negocio en un día, cuando se abre la vista diaria, entonces aparecen ordenados y diferenciados.
- Dados compromisos en varios días, cuando se abre la vista semanal, entonces cada uno aparece en el día correspondiente.
- Dada una próxima cita activa, cuando se consulta el resumen, entonces se identifica correctamente sin contar citas canceladas.
- Dado el filtro “Personal”, cuando se aplica, entonces se ocultan elementos de negocio sin borrarlos.
- Dada una agenda vacía, cuando se abre, entonces se muestra un estado vacío y no un error.
- Dado un error de lectura, cuando ocurre, entonces se muestra un estado de error distinto del estado vacío.
- Dada la configuración inicial, cuando se abre la vista semanal, entonces la semana comienza en lunes; si el usuario cambia la preferencia, la vista respeta el nuevo inicio.
- Dada una cita cancelada, cuando se abre la agenda sin filtros especiales, entonces no aparece; al activar “Mostrar canceladas”, aparece identificada como cancelada.
- Dada una cita con preparación o limpieza, cuando se consulta en la agenda, entonces los márgenes son visibles y distinguibles del tiempo de servicio.

**Ejemplo ficticio:** el lunes muestra “Corte de Luis” a las 10:00 y “Clase nocturna” a las 18:30; la vista semanal permite ver ambos junto con los demás días.

**Dudas o decisiones pendientes:** confirmar la representación visual propuesta, contenido exacto del resumen y densidad aceptable de la semana móvil durante E1-T03.

### REQ-A-009. Persistir datos localmente

**Usuario y necesidad:** usuario de un dispositivo que necesita conservar agenda y configuración después de cerrar o reiniciar la aplicación, aun sin conexión.

**Historia:** Como usuario, quiero que mis datos se guarden localmente, para continuar usando la agenda y recuperar el mismo estado al volver a abrirla.

**Prioridad y dependencias:** P0 y transversal. Da soporte a REQ-A-001 a REQ-A-008. No depende de cuenta ni sincronización de la entrega B.

**Flujo principal:** tras una acción confirmada, el sistema guarda el registro y su estado; al iniciar, carga datos y configuración local con sus identificadores y valores históricos.

**Alternativas:** si falla el guardado, se informa y no se presenta la operación como completada. Si falla la carga, se muestra error sin fingir agenda vacía. Las migraciones y recuperación detalladas se diseñarán en etapas posteriores.

**Validaciones y mensajes:** identificadores estables; inicio y fin con fecha; zona horaria del dispositivo propuesta y confirmada por el usuario; decimales y moneda en formatos definidos; estado de cancelación persistente. Mensajes provisionales: “Confirma tu zona horaria: America/Mexico_City”, “No pudimos guardar los cambios; inténtalo de nuevo” y “No pudimos cargar tus datos locales”.

**Criterios de aceptación:**

- Dada una cita guardada, cuando la aplicación se cierra y vuelve a abrir, entonces conserva identificador, fechas, estado y copia histórica del servicio.
- Dado un cliente, servicio, actividad, bloqueo u horario guardado, cuando se reinicia, entonces el registro reaparece sin duplicarse.
- Dada una cita cancelada, cuando se reinicia, entonces conserva el estado cancelado y no ocupa disponibilidad.
- Dado un error de escritura, cuando ocurre, entonces el sistema no confirma falsamente el guardado.
- Dados datos locales sin conexión, cuando se abre la entrega A, entonces siguen disponibles sin requerir una cuenta.
- Dada la primera configuración, cuando se detecta la zona horaria del dispositivo, entonces se propone al usuario y no queda confirmada hasta que este la acepta o cambia.

**Ejemplo ficticio:** se guardan el servicio “Corte clásico”, el cliente ficticio “Luis Pérez”, una cita y un descanso. Tras reiniciar, reaparecen una sola vez y la cita conserva precio y duración originales.

**Dudas o decisiones pendientes:** elegir tecnología y estrategia de migraciones en etapas posteriores, detectar cambios de zona horaria, copias de seguridad y recuperación ante corrupción.

## Requisitos de la entrega B — posteriores

No forman parte de E1-T01 ni de la implementación actual.

### REQ-B-001. Cuenta y autorización

Una cuenta podrá utilizar sus datos en Android y Windows sin leer ni modificar datos de otra cuenta.

### REQ-B-002. Sincronización recuperable

Los cambios locales se sincronizarán con reintentos idempotentes, versiones de registro y marcas de eliminación. La desconexión no perderá cambios y los conflictos quedarán visibles.

### REQ-B-003. Tiempo coordinado

El servidor almacenará instantes en UTC y la zona necesaria para mostrarlos. La disponibilidad se revalidará en el servidor antes de aceptar un cambio sincronizado.

### REQ-B-004. Recordatorios coordinados

El usuario configurará avisos por dispositivo. La confirmación o cancelación se coordinará entre dispositivos y podrá existir un aviso de respaldo documentado.

Estado de REQ-B-001 a REQ-B-004: documentados para una entrega posterior; no autorizados.

## Requisitos de la entrega C — posteriores

No forman parte de E1-T01 ni de la implementación actual.

### REQ-C-001. Respaldo y restauración

Formato versionado, validación, vista previa y política explícita de importación y duplicados. La restauración se comprobará en un entorno de prueba.

### REQ-C-002. Accesibilidad y calidad

Navegación adaptada, controles utilizables, contraste, texto ajustable, modo oscuro y estados de error, carga y vacío coherentes.

### REQ-C-003. Entrega reproducible

Versión, requisitos, instalación, desinstalación, limitaciones, manual, guía técnica, evidencia y trazabilidad entre requisito, implementación y prueba.

### REQ-C-004. Piloto real

De 3 a 5 personas completarán recorridos definidos por el responsable y se registrarán observaciones reales, sin inventar testimonios ni generalizar éxito comercial.

Estado de REQ-C-001 a REQ-C-004: documentados para una entrega posterior; no autorizados.

## Seguimiento posterior al diseño aprobado

- Confirmar o corregir el público inicial mediante entrevistas reales.
- El diseño móvil/escritorio y DEC-0011 a DEC-0017 fueron aprobados el 2026-10-19.
- Validar accesibilidad, densidad e interacción en un teléfono real cuando exista una versión ejecutable.
- Resolver reactivación de registros archivados, tratamiento de varias zonas horarias y representación de citas que cruzan fechas.
