# Diseño de Tiempo en Orden

Estado: especificación visual provisional ajustada en E1-T03. Los bocetos son material de revisión; no son una aplicación web ni prueban funciones implementadas. La aplicación de producción prevista continúa siendo C#/.NET MAUI. La revisión del responsable sigue pendiente.

## Alcance de este diseño

Se diseñan los recorridos y pantallas de la entrega A local para Android y Windows. No se muestran cuenta, sincronización, avisos coordinados, servidor, pagos, reservas públicas ni otras funciones de las entregas B/C.

E1-T02 en `TAREAS.md` ya pedía bocetos móvil/escritorio de Inicio, Agenda, Nueva cita, Clientes, Servicios y Ajustes. Este encargo amplía el criterio de cierre al exigir seis flujos, sistema visual, estados, trazabilidad y prototipos revisados. No existe contradicción; se alinea la descripción de la tarea con este alcance más preciso.

## Principios

1. **Disponibilidad comprensible:** mostrar servicio, preparación y limpieza como partes distintas de una sola ocupación.
2. **Control del usuario:** informar antes de cambiar; nunca mover ni cancelar compromisos automáticamente.
3. **Misma regla, misma explicación:** un espacio sugerido se valida con las mismas reglas al guardar.
4. **Personal y negocio juntos, pero distinguibles:** texto, icono y color; nunca color solamente.
5. **Adaptación por plataforma:** jerarquía compartida, navegación y densidad propias de Android y Windows.
6. **Estados honestos:** vacío, error, conflicto y éxito son diferentes. Un error no se representa como agenda vacía.
7. **Entrega A visible:** ninguna pantalla sugiere cuenta, nube, sincronización o recordatorios coordinados.

## Arquitectura de navegación

### Android

- Barra inferior: `Inicio`, `Agenda` y `Más`.
- Acciones `Nueva cita` y `Tiempo personal` visibles desde Inicio; `Nueva cita` permanece visible en Agenda.
- `Más` abre Clientes, Servicios, Ajustes y Tareas.
- Formularios en una sola columna, con secciones y acciones persistentes al final del contenido.
- Objetivos táctiles mínimos propuestos de 44 × 44 px y separación suficiente entre acciones destructivas.

### Windows

- Navegación lateral persistente: Inicio, Agenda, Clientes, Servicios, Tareas y Ajustes. `Estados` no es una sección del producto: está fuera de este menú como control de revisión del prototipo.
- `Nueva cita` como acción principal destacada en la barra lateral y encabezado contextual.
- Área central para agenda; panel lateral de detalle o formulario cuando el ancho lo permite.
- Las operaciones mantienen contexto: crear cliente o servicio abre un panel secundario y vuelve a Nueva cita con la selección aplicada.

## Flujos

### FLUJO-01. Crear una cita con cliente y servicio

Requisitos: REQ-A-001, REQ-A-002, REQ-A-003, REQ-A-005, REQ-A-006 y REQ-A-009.

1. Desde Inicio o Agenda, elegir `Nueva cita`.
2. Seleccionar cliente existente o continuar sin cliente.
3. Seleccionar servicio activo.
4. Capturar fecha y hora de inicio; mostrar fecha y hora de fin calculadas.
5. Mostrar ocupación completa: preparación, servicio y limpieza.
6. Validar horario laboral, descansos, bloqueos y compromisos.
7. Confirmar `Guardar cita`.
8. Mostrar operación completada y acceso a `Ver en agenda`.

Alternativas: datos inválidos conservan el formulario; fallo de guardado no abandona la pantalla; si termina al día siguiente, la fecha de fin se presenta explícitamente.

### FLUJO-02. Detectar conflicto, buscar alternativas y elegir una

Requisitos: REQ-A-001, REQ-A-005, REQ-A-006 y REQ-A-007.

1. Al validar una hora, mostrar un bloque de conflicto con el intervalo ocupado y el compromiso que cruza.
2. Mantener los datos capturados y ofrecer `Buscar espacios`.
3. Buscar en saltos de 15 minutos, hasta 14 días, sin alterar compromisos.
4. Mostrar hasta tres opciones con hora de servicio, ocupación total y razón de validez.
5. El usuario selecciona una opción.
6. Volver al formulario con la nueva hora y revalidar al guardar.
7. Si la opción dejó de estar libre, permanecer en el formulario y actualizar alternativas.

### FLUJO-03. Registrar y proteger tiempo personal

Requisitos: REQ-A-004, REQ-A-005, REQ-A-006 y REQ-A-009.

1. En Agenda, elegir `Agregar tiempo` y tipo `Actividad personal` o `Bloqueo`.
2. Capturar título, fecha, inicio y fin.
3. Validar cruces; una actividad puede quedar fuera del horario laboral.
4. Confirmar y mostrarla con etiqueta e icono personal.
5. El intervalo deja de aparecer en sugerencias.

Una tarea sin horario se guarda en `Pendientes` y no ocupa disponibilidad.

### FLUJO-04. Editar o cancelar una cita

Requisitos: REQ-A-001, REQ-A-006, REQ-A-008 y REQ-A-009.

1. Abrir una cita desde Inicio o Agenda.
2. Elegir `Editar` o `Cancelar cita`.
3. Al editar, excluir la cita actual por identificador y validar los cambios.
4. Al cancelar, mostrar confirmación y explicar que el horario quedará libre.
5. Tras confirmar, conservar historial y ocultarla de la agenda normal.
6. El filtro `Mostrar canceladas` permite consultarla con estado explícito.

### FLUJO-05. Crear cliente o servicio sin perder Nueva cita

Requisitos: REQ-A-001, REQ-A-002 y REQ-A-003.

1. Desde el selector correspondiente, elegir `Crear cliente` o `Crear servicio`.
2. Abrir hoja modal en Android o panel lateral en Windows.
3. Guardar los campos mínimos.
4. Cerrar el panel secundario.
5. Volver al formulario de cita conservando fecha, hora y demás datos.
6. Seleccionar automáticamente el registro recién creado.

Si falla el guardado secundario, el formulario de cita permanece intacto.

### FLUJO-06. Consultar agenda diaria y semanal

Requisitos: REQ-A-004, REQ-A-005 y REQ-A-008.

1. Abrir Agenda en vista diaria.
2. Cambiar entre `Día` y `Semana`.
3. Aplicar filtros `Todo`, `Negocio`, `Personal` y `Mostrar canceladas`.
4. Abrir el detalle de un compromiso.

En Android, la semana utiliza una tira horizontal de siete días y muestra debajo el detalle del día seleccionado; así conserva legibilidad sin reducir controles. En Windows, una cuadrícula semanal usa el ancho disponible y permite abrir detalles en un panel.

## Pantallas principales

### Inicio

Contenido compartido:

- Saludo corto, fecha y hora ficticia actual de la muestra (`09:20`), coherentes con una próxima cita a las `10:00`.
- Próxima cita con cliente, servicio, hora de atención y ocupación total.
- Compromisos del día en orden cronológico.
- Tareas sin horario en sección separada.
- Acciones `Nueva cita` y `Tiempo personal`.
- Distinción textual entre compromisos `Pasado` y `Próximo`.

Android apila tarjetas y mantiene navegación inferior. Windows presenta resumen y agenda del día en dos columnas dentro del área principal.

Estados: sin próxima cita, día vacío, carga fallida y operación completada mediante confirmación breve no bloqueante.

### Agenda

- Selector Día/Semana.
- Navegación por fecha y acción `Hoy`.
- Filtros por tipo y opción de canceladas.
- Leyenda persistente: `Negocio`, `Personal`, `Preparación/Limpieza` y `Cancelada`.
- Preparación y limpieza se muestran como intervalos separados, con etiqueta, hora y fondos sencillos diferentes.

Android muestra semana compacta por días seleccionables y lista temporal del día. Windows muestra cuadrícula semanal completa y panel de detalle.

### Nueva cita

- Cliente opcional, con creación contextual.
- Servicio obligatorio, con creación contextual.
- Inicio con fecha y hora.
- Fin calculado con fecha y hora.
- Zona horaria confirmada visible como `Ciudad de México`; `America/Mexico_City` se conserva como identificador interno.
- Resumen de ocupación: preparación + servicio + limpieza.
- Acciones `Descartar cambios` y `Guardar cita`. Si el formulario fue modificado, descartar exige confirmación. `Cancelar cita` se reserva para una cita ya registrada.

Estados incorporados:

- **Inválido:** campos conservados, resumen superior y mensajes junto a cada campo.
- **Conflicto:** explicación del intervalo, compromiso que cruza y acción `Buscar espacios`.
- **Alternativas:** hasta tres opciones con ocupación completa.
- **Alternativa aplicada:** actualiza fecha y hora solo tras elegirla y recuerda que se revalidará al guardar.
- **Guardado fallido:** mensaje persistente y acción `Reintentar`; no afirmar éxito.
- **Completado:** confirmación con `Ver en agenda` y `Crear otra`.

## Pantallas secundarias

### Clientes

- Lista y búsqueda por nombre.
- Formulario: nombre obligatorio; teléfono y notas opcionales.
- Estado `Activo` o `Archivado` expresado con texto e icono.
- Archivados ocultos por defecto; filtro para consultarlos.

### Servicios

- Lista con nombre, duración total de atención, precio y estado.
- Formulario: nombre, duración, precio, moneda, preparación y limpieza.
- Vista previa de ocupación total.
- Archivo conserva historial; archivados no aparecen por defecto en Nueva cita.

### Ajustes

- Horario laboral y descansos.
- Zona horaria propuesta por el dispositivo y confirmación explícita.
- Inicio de semana: lunes como valor inicial, configurable.
- Preferencias de agenda: mostrar canceladas desactivado por defecto.
- No incluye cuenta, nube, sincronización ni recordatorios coordinados.

## Sistema visual provisional

### Tipografía

- Familia de sistema: Segoe UI en Windows y Roboto en Android, con alternativa sans-serif.
- Título de pantalla: 28/34, peso seminegrita.
- Encabezado de sección: 20/26, peso seminegrita.
- Cuerpo y controles: 16/24.
- Metadatos: 14/20; nunca menor a 12 en el prototipo.

### Color

- Fondo: gris azulado muy claro; superficies blancas o equivalentes en modo oscuro.
- Primario: azul petróleo para acciones principales y selección.
- Negocio: azul, acompañado por icono de maletín y etiqueta `Negocio`.
- Personal: morado, acompañado por icono de persona y etiqueta `Personal`.
- Preparación: ámbar suave con etiqueta y horario; limpieza: azul claro con etiqueta y horario. Son intervalos separados y no dependen solo del color.
- Error/conflicto: rojo oscuro con icono de alerta y explicación.
- Éxito: verde oscuro con icono de confirmación y texto.

El color siempre se combina con texto, icono, forma o patrón. Contraste objetivo: WCAG AA para texto y controles.

### Espaciado y forma

- Escala: 4, 8, 12, 16, 24 y 32 px.
- Radio de controles y superficies: 10–14 px.
- Controles táctiles: al menos 44 px de alto.
- Una acción primaria por grupo; acciones destructivas separadas y rotuladas.

### Componentes

- Navegación lateral y navegación inferior.
- Encabezado de pantalla.
- Botón primario, secundario, de texto y destructivo.
- Campo, selector, fecha/hora, filtro, pestañas Día/Semana.
- Tarjeta de próxima cita y fila de agenda.
- Bloque temporal con margen visual.
- Banner de conflicto, error y éxito.
- Estado vacío con acción contextual.
- Hoja modal móvil y panel lateral de escritorio.

## Datos ficticios comunes

- Cliente: Ana López, teléfono `555-010-2040`.
- Cliente: Luis Pérez, teléfono `555-010-2075`.
- Servicio: Corte clásico, MXN 250, 45 min, 10 min de preparación y 5 min de limpieza.
- Servicio: Afeitado, MXN 180, 30 min, 10 min de preparación y 10 min de limpieza.
- Personal: Clase nocturna, 18:30–20:00.
- Tarea sin horario: Comprar insumos.
- Zona horaria ficticia del dispositivo: `Ciudad de México`, con `America/Mexico_City` como identificador interno y confirmación explícita. Esta muestra no supone que todas las personas de México usen esa zona.

## Trazabilidad entre pantallas y requisitos

| Pantalla | Requisitos principales | Evidencia visual |
| --- | --- | --- |
| Inicio | REQ-A-001, REQ-A-004, REQ-A-008 | Próxima cita, día, tarea sin horario y Nueva cita |
| Agenda | REQ-A-001, REQ-A-004, REQ-A-005, REQ-A-008 | Día/semana, filtros, tipos y márgenes |
| Nueva cita | REQ-A-001, REQ-A-002, REQ-A-003, REQ-A-005, REQ-A-006, REQ-A-007 | Formulario, ocupación, conflicto y alternativas |
| Clientes | REQ-A-003, REQ-A-009 | Campos mínimos, activos/archivados |
| Servicios | REQ-A-002, REQ-A-009 | Duración, precio, moneda y márgenes |
| Ajustes | REQ-A-005, REQ-A-008, REQ-A-009 | Horario, descanso, semana y zona horaria |
| Estados para revisión | REQ-A-001, REQ-A-006, REQ-A-008, REQ-A-009 | Control externo al menú para revisar vacío, inválido, conflicto, fallo y éxito |

## Prototipo visual

Abrir `docs/prototipos/tiempo-en-orden-bocetos.html`. Los controles externos cambian plataforma, muestran los estados simulados de Nueva cita, abren los estados de revisión y amplían el texto. Dentro del producto solo permanecen navegación, vista Día/Semana y acciones propias del recorrido. No guarda datos ni ejecuta reglas reales.

## Revisión y ajustes E1-T03

### Alcance registrado y estado

`TAREAS.md` registraba E1-T03 como revisión con el responsable, dependiente de E1-T02, con cierre cuando el flujo de creación/corrección resulte comprensible y existan comentarios. El encargo actual autoriza los ajustes y la comprobación del prototipo. No equivale a aprobación final: la tarea queda en curso hasta recibir comentarios del responsable y la etapa 1 no se marca como terminada.

### Ajustes implementados

- La muestra usa `09:20`, saludo de mañana y próxima cita a las `10:00`; el resumen indica `3 compromisos pendientes` y no afirma que el sistema haya comprobado que todo está en orden.
- Inicio diferencia un compromiso pasado de los próximos mediante texto, orden temporal y tratamiento visual.
- La tarjeta de próxima cita en Windows conserva su altura natural y ya no se estira para igualar la columna de compromisos.
- Preparación y limpieza se presentan como dos intervalos con horas, etiquetas y fondos simples; la ocupación de la cita continúa siendo la suma de preparación, servicio y limpieza.
- Se sustituyeron las letras `N`, `P` y `M` por etiquetas completas e iconos de negocio, personal y tiempo de margen.
- Inicio incorpora acceso directo a `Tiempo personal` y un formulario visual de actividad/bloqueo.
- `Estados para revisión` se retiró de la navegación del producto y se movió a los controles externos del prototipo.
- Nueva cita permite abrir formularios contextuales de cliente o servicio sin eliminar lo capturado. La opción ficticia elegida vuelve al formulario y conserva fecha y hora.
- Elegir una sugerencia actualiza la hora ficticia, presenta `Alternativa aplicada` y mantiene visible que las mismas reglas se ejecutarán al guardar. Ningún compromiso se mueve por esa acción.
- Se añadió control `Texto +` y un foco visible de 3 px para revisión por teclado.

### Comprobaciones del prototipo en navegador

Estas son comprobaciones visuales y de interacción del prototipo HTML en Chrome; no son pruebas físicas de Android ni Windows ni validan una aplicación MAUI.

| Comprobación | Tamaños revisados | Resultado observado |
| --- | --- | --- |
| Inicio, Agenda semanal y Nueva cita móvil | 360, 390 y 430 px | Sin desbordamiento horizontal ni controles recortados; navegación y acciones principales visibles. |
| Inicio, Agenda semanal y Nueva cita de escritorio | 1280 y 1440 px | Sin desbordamiento horizontal; cuadrícula semanal legible y tarjeta de próxima cita sin estiramiento. |
| Texto ampliado | Inicio móvil en 360, 390 y 430 px | Reflujo sin recortes ni desbordamiento horizontal. |
| Teclado y foco | Secuencia inicial mediante `Tab` | Control enfocado con contorno sólido visible de 3 px; se conserva el orden nativo del documento. |
| Creación contextual | Cliente dentro de Nueva cita | Panel visible, hora `17:20` conservada y cliente ficticio nuevo seleccionado al volver. |
| Semana pequeña | Vista móvil | Tira de siete días y detalle solo del día seleccionado; no se muestra la cuadrícula de escritorio. |
| Modo oscuro | Windows simulado por navegador | Superficie y texto adaptados; fondo `rgb(24, 35, 41)` y texto `rgb(237, 245, 245)` en la ventana revisada. |
| Conflicto, alternativa y error | Nueva cita | Mensajes diferenciados visibles; la alternativa modifica el formulario únicamente al seleccionarla y el fallo no afirma éxito. |
| Ejecución del prototipo | Consola y página | Cero errores registrados durante el recorrido automatizado. |

### Capturas de revisión

Las seis capturas de la primera revisión fueron sustituidas por las tres capturas vigentes de la segunda revisión, enumeradas más abajo.

### Pendiente de revisión del responsable

- Confirmar si la jerarquía de Inicio prioriza correctamente próxima cita, acciones y agenda del día.
- Confirmar si preparación y limpieza se comprenden como tiempo ocupado separado del servicio.
- Confirmar si la semana móvil y el mensaje de conflicto son suficientemente claros en un teléfono real.
- Confirmar o ajustar las decisiones provisionales DEC-0011 a DEC-0017.
- Resolver las preguntas funcionales todavía abiertas: motivo de cancelación, reactivación de archivados y representación de citas entre fechas.

### Segunda revisión de capturas E1-T03

- **Semana móvil:** el contador y la lista se generan desde la misma colección ficticia de tres compromisos. Ahora aparecen Corte con Ana, Descanso 14:00–15:00 y Clase nocturna 18:30–20:00.
- **Semana de escritorio:** se sustituyeron las filas de horas desiguales por una escala continua de 09:00 a 20:00, con 60 px por hora y desplazamiento vertical calculado en minutos. La cita del martes ocupa 09:50–10:50 y divide proporcionalmente preparación (10 min), servicio (45 min) y limpieza (5 min). Un resumen textual conserva los horarios cuando un segmento es demasiado pequeño para contener texto.
- **Formulario:** `Cancelar` se sustituyó por `Descartar cambios`. Un formulario sin modificaciones vuelve a Inicio; si existe una modificación, aparece la confirmación `¿Descartar los cambios?`. `Cancelar cita` continúa reservado al flujo de una cita registrada.
- **Zona horaria:** se muestra `Ciudad de México` como nombre comprensible y `America/Mexico_City` como identificador interno. La propuesta se atribuye al dispositivo actual y requiere confirmación; no se generaliza a todos los usuarios de México.
- **Demostración:** Formulario, Inválido, Conflicto, Alternativa aplicada, Fallo y Completado se movieron fuera de la ventana del producto bajo la etiqueta `Acciones simuladas del prototipo`.
- Se conservaron el diseño general, el recorrido de alternativas y los requisitos existentes. No se agregó una función del producto.

Capturas vigentes de esta revisión:

- `docs/prototipos/capturas/semana-movil-corregida-e1-t03.png`
- `docs/prototipos/capturas/semana-escritorio-corregida-e1-t03.png`
- `docs/prototipos/capturas/formulario-actualizado-e1-t03.png`

La revisión del responsable continúa pendiente y la etapa 2 permanece sin iniciar.

## Preguntas para E1-T03

1. ¿La diferencia entre preparación, servicio y limpieza se entiende sin explicación adicional?
2. ¿La semana móvil permite decidir rápidamente qué día revisar?
3. ¿Inicio debe mostrar primero próxima cita o compromisos del día?
4. ¿La creación contextual de cliente/servicio debe ser hoja o pantalla completa en Android?
5. ¿Los mensajes de conflicto explican suficiente información sin resultar técnicos?
6. ¿Debe pedirse motivo de cancelación?
7. ¿Se permitirá reactivar clientes y servicios archivados?
8. ¿Cómo se mostrará una cita que comienza un día y termina al siguiente en la semana?
9. Resuelta en esta revisión: mostrar ciudad comprensible e identificador interno (`Ciudad de México` y `America/Mexico_City`).
10. ¿La densidad visual y tamaño de controles son adecuados en el teléfono real disponible?
