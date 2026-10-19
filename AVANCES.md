# Registro de avances

## 2026-10-18 — Etapa 0: preparar el proyecto

### ETAPA Y TAREA

E0-T01 a E0-T04. Diagnosticar la carpeta y el entorno, convertir la propuesta en documentación trazable y dejar preparada la siguiente etapa sin comenzar el producto.

### RESULTADO

La etapa 0 quedó completada. Se documentaron producto, alcance A/B/C, reglas funcionales, criterios de aceptación, decisiones, dependencias y tareas.

Todavía no están disponibles pantallas, aplicación ejecutable, persistencia, servidor, sincronización ni recordatorios. No se creó código de producción y no se instaló software.

### ARCHIVOS

- `AGENTS.md`: reglas para futuros cambios y pruebas.
- `PROYECTO.md`: visión, alcance, arquitectura candidata y datos pendientes.
- `TAREAS.md`: tareas por etapa con estados y dependencias.
- `AVANCES.md`: evidencia y reporte de cierre.
- `docs/DECISIONES.md`: decisiones aceptadas, propuestas y pendientes.
- `docs/REQUISITOS.md`: requisitos numerados y criterios de aceptación.

### DIAGNÓSTICO DEL ENTORNO

- Equipo: THUNDEROBOT 911 Plus, arquitectura x64.
- Sistema: Windows 11 Pro, versión 10.0.26200, compilación 26200.
- PowerShell: 7.6.5.
- Git: 2.55.0.windows.5. El repositorio existe en la carpeta, no tiene commits y al iniciar no contenía archivos de proyecto.
- .NET SDK: 10.0.102; runtimes 10.0.2 de .NET, ASP.NET Core y Windows Desktop.
- Carga de trabajo MAUI: instalada, manifiesto 10.0.20/10.0.100.
- Plantillas: `dotnet new list maui` reconoce la plantilla de aplicación MAUI y plantillas XAML/C#.
- No existe `global.json`; aún no se ha fijado SDK del repositorio.
- Visual Studio: no detectado por `vswhere`; solo se encontró SQL Server Management Studio 22.
- MSBuild independiente: no está en `PATH`. El Windows SDK tampoco fue detectado en su ubicación/registro habitual.
- Visual Studio Code: 1.140.0. No se detectaron las extensiones oficiales C# Dev Kit o .NET MAUI; solo `leo-labs.dotnet@1.3.0` coincidió con el filtro.
- Android Studio: instalado; producto 2026.1.3, compilación `AI-261.26222.65.2613.15948027`.
- Android SDK: existe en `%LOCALAPPDATA%\\Android\\Sdk`, con plataformas 35, 36 y 37.0 y build-tools 36.0.0.
- ADB: 37.0.1 disponible por ruta absoluta. No había dispositivo físico conectado.
- Emulador Android: binario instalado, pero no hay AVD ni imágenes de sistema instaladas.
- Java: Android Studio incluye JBR/JDK 25.0.2, pero `java`, `javac`, `JAVA_HOME`, `ANDROID_HOME`, `adb` y `emulator` no están configurados en el entorno de la terminal.
- NuGet: `nuget.org` habilitado.

La presencia de SDK, plantillas y paquetes indica viabilidad parcial, pero no equivale a una compilación. La compatibilidad concreta de JDK, Android SDK, .NET MAUI y Windows SDK se validará al elegir versiones y crear la base mínima en la etapa 2. No se instalarán componentes sin un encargo explícito.

### VALIDACIÓN

Comprobaciones ejecutadas:

- `git status --short --branch`: repositorio sin commits; solo apareció el directorio temporal usado para leer la guía.
- `dotnet --info`: SDK 10.0.102 y runtimes 10.0.2 detectados.
- `dotnet workload list`: carga `maui` instalada.
- `dotnet new list maui`: plantillas MAUI disponibles.
- `vswhere -all -products * -format json`: ninguna instalación de Visual Studio; SSMS 22 sí detectado.
- inspección de Android SDK: plataformas y herramientas presentes.
- `adb devices -l`: comando ejecutado, cero dispositivos conectados.
- `emulator -list-avds`: comando ejecutado, cero dispositivos virtuales configurados.
- `code --version` y `code --list-extensions --show-versions`: VS Code detectado; extensión oficial MAUI no detectada.

No se ejecutó compilación ni aplicación porque la etapa 0 no crea código. Android y Windows permanecen **no probados**.

### PRUEBA MANUAL

No corresponde una prueba funcional en esta etapa. Revisión manual sugerida:

1. Leer `PROYECTO.md` y confirmar que usuario, problema y entregas reflejan la intención.
2. Leer `docs/REQUISITOS.md` y marcar ambigüedades de vocabulario o alcance.
3. Responder las preguntas pendientes antes o durante la etapa 1.

Resultado esperado: el responsable puede explicar qué se construirá primero, qué queda para después y qué datos faltan.

### PROBLEMAS

- Falta fecha de entrega, rúbrica, evidencia escolar requerida y definición de venta.
- No se conoce el nivel de experiencia del responsable con C#.
- No se ha identificado teléfono Android para pruebas.
- Android carece de AVD y no hay dispositivo conectado.
- Las rutas de Java y Android no están configuradas para la terminal.
- No se detectaron Visual Studio, Windows SDK ni extensión oficial .NET MAUI para VS Code.
- No se ha comprobado compilación en Android ni Windows.

### DECISIONES

- Trabajar una etapa por encargo y detenerse al cerrarla.
- Usar reglas deterministas y separadas de la interfaz.
- Mantener .NET MAUI/SQLite/API/SignalR como dirección técnica, sin fijar versiones ni crear componentes futuros todavía.
- Usar datos ficticios en pruebas y demostraciones.

### SIGUIENTE TAREA

E1-T01: refinar historias de usuario y criterios verificables con las respuestas del responsable. Dependencia: autorización explícita de etapa 1 y revisión de los pendientes. Criterio de cierre: requisitos A numerados, vocabulario acordado y preguntas abiertas visibles, sin comenzar código de producción.

## 2026-10-18 — E1-T01: refinar historias y criterios

### ETAPA Y TAREA

Etapa 1, tarea E1-T01. Convertir REQ-A-001 a REQ-A-009 en comportamientos claros y comprobables sin crear bocetos ni código.

### RESULTADO

E1-T01 quedó completada. Cada requisito de la entrega A incluye usuario y necesidad, historia de usuario, prioridad, dependencias, flujo principal, alternativas, validaciones, mensajes provisionales, criterios Dado/Cuando/Entonces, ejemplo ficticio y decisiones pendientes.

Se definió un vocabulario provisional para cita, actividad personal, bloqueo, descanso, tarea y horario laboral. Se reforzaron como reglas transversales la ocupación por preparación y limpieza, liberación al cancelar, exclusión de la propia cita al editar, copia histórica del servicio, reglas compartidas entre sugerencia y guardado, y prohibición de movimientos automáticos.

REQ-B-001 a REQ-B-004 y REQ-C-001 a REQ-C-004 permanecen documentados como entregas posteriores y no fueron ampliados hacia implementación.

No se programó, no se instaló software y no se crearon bocetos.

### ARCHIVOS

- `docs/REQUISITOS.md`: requisitos A refinados, reglas globales, vocabulario y dudas.
- `PROYECTO.md`: aclaración entre cronograma de desarrollo y vista semanal del producto.
- `TAREAS.md`: E1-T01 completada; E1-T02 y E1-T03 no iniciadas.
- `docs/DECISIONES.md`: aclaración de DEC-0007 y nuevas DEC-0008 a DEC-0010.
- `AVANCES.md`: evidencia y cierre de E1-T01.

### VALIDACIÓN

- Revisión estructural de los nueve requisitos A para comprobar todas las secciones solicitadas.
- Conteo de identificadores: 17 en total, incluidos los 9 requisitos A, sin duplicados.
- Conteo de estructura: cada una de las nueve secciones solicitadas aparece exactamente una vez en cada requisito A; se registraron 45 criterios Dado/Cuando/Entonces.
- Búsqueda explícita de reglas críticas: preparación, limpieza, cancelación, autoexclusión al editar, copia histórica, regla compartida y ausencia de movimientos automáticos.
- Revisión de estados para confirmar que E1-T02, E1-T03 y etapas 2 a 8 siguen sin iniciar.

La validación es documental. No existe software que compilar o ejecutar en esta tarea.

### PRUEBA MANUAL

1. Revisar cada historia y confirmar que describe la operación esperada.
2. Recorrer los ejemplos ficticios y verificar que preparación y limpieza cambian la ocupación.
3. Confirmar que cancelar libera el horario y editar no genera conflicto consigo misma.
4. Confirmar que la vista semanal forma parte del producto.
5. Revisar las dudas pendientes antes de dibujar pantallas.

Resultado esperado: los comportamientos pueden transformarse en flujos y bocetos sin inventar reglas adicionales.

### PROBLEMAS E INCONSISTENCIAS

- DEC-0007 decía “no crear calendario semanal”, frase que podía contradecir REQ-A-008. Se corrigió: se pospone el cronograma semanal de desarrollo, no la vista semanal de la agenda.
- TAREAS.md indicaba que E1-T01 dependía de respuestas del responsable. La autorización actual permite refinar lo conocido; los datos ausentes se conservaron como dudas y no se inventaron.
- El comportamiento de tareas sin hora sigue sin definirse.
- Siguen pendientes campos mínimos, granularidad de búsqueda, primer día de la semana, zona horaria inicial y casos que cruzan medianoche.
- El público inicial continúa como hipótesis; no existen entrevistas ni validación comercial registradas.

### DECISIONES

- Mantener la vista semanal dentro de la entrega A.
- Compartir reglas entre sugerencias y guardado y revalidar al confirmar.
- Copiar valores del servicio a la cita para conservar historia.
- No mover compromisos sin acción explícita.
- Mantener mensajes y vocabulario visual como provisionales hasta E1-T02/E1-T03.

### SIGUIENTE TAREA

E1-T02: diseñar flujos y bocetos de Inicio, Agenda diaria/semanal, Nueva cita, Clientes, Servicios y Ajustes. Dependencia: autorización explícita y revisión de las dudas de E1-T01. Criterio de cierre: bocetos comparables para móvil y escritorio y especificación en `docs/DISENO.md`, sin comenzar código de producción.

## 2026-10-18 — E1-T02: flujos y bocetos Android/Windows

### ETAPA Y TAREA

Etapa 1, tarea E1-T02. Diseñar una propuesta visual profesional y revisable para la entrega A antes de comenzar la aplicación MAUI.

### RESULTADO

E1-T02 quedó completada. Se documentaron seis flujos: crear cita; resolver conflicto mediante alternativas; proteger tiempo personal; editar/cancelar; crear cliente o servicio sin perder el formulario; consultar agenda diaria/semanal.

Se diseñaron Inicio, Agenda y Nueva cita con detalle para Android y Windows. Se agregaron bocetos secundarios de Clientes, Servicios y Ajustes, además de estados vacío, formulario inválido, conflicto, fallo de guardado, operación completada y cita cancelada.

El prototipo permite cambiar plataforma, pantalla, vista Día/Semana y estado del formulario. Es estático y está rotulado como prototipo: no implementa reglas, almacenamiento, servidor ni sincronización.

La descripción original de E1-T02 ya incluía las seis pantallas solicitadas, pero no detallaba flujos, estados, sistema visual, trazabilidad ni revisión visual. Se amplió la descripción para alinearla con este encargo; no se encontró contradicción de alcance.

### ARCHIVOS

- `docs/DISENO.md`: navegación, seis flujos, pantallas, sistema visual, estados, trazabilidad y preguntas.
- `docs/prototipos/tiempo-en-orden-bocetos.html`: prototipo visual interactivo y estático.
- `docs/REQUISITOS.md`: decisiones provisionales incorporadas sin cambiar identificadores.
- `docs/DECISIONES.md`: DEC-0011 a DEC-0018.
- `TAREAS.md`: E1-T02 completada; E1-T03 no iniciada.
- `AVANCES.md`: evidencia y cierre de E1-T02.

### VALIDACIÓN

- El prototipo se renderizó localmente en Chrome mediante automatización de navegador, sin descargar ni instalar herramientas.
- Se revisaron visualmente Inicio Android/Windows, Agenda semanal Android/Windows, conflicto en Nueva cita, Clientes/Servicios, Ajustes, estados y modo oscuro de Windows.
- La primera cuadrícula semanal de Windows perdió su estructura al alternar vistas; se corrigió la regla de visualización y se volvió a renderizar.
- Resultado final del navegador: cero errores de consola o de página.
- Medición final: ancho del prototipo 1180 px y ancho desplazable 1180 px; no se detectó desbordamiento horizontal en el ancho de revisión.
- Se comprobaron siete días en escritorio y selector de siete días con detalle seleccionado en móvil.
- Se comprobaron etiquetas, color y patrón para distinguir negocio, personal y márgenes.
- Se verificó que el prototipo no contiene cuenta, nube, sincronización, servidor ni recordatorios coordinados.

No se compiló ni ejecutó una aplicación MAUI porque E1-T02 produce diseño, no código de producción.

### PRUEBA MANUAL

1. Abrir `docs/prototipos/tiempo-en-orden-bocetos.html` en un navegador.
2. Cambiar entre Android y Windows.
3. Recorrer Inicio, Agenda, Nueva cita, Clientes/Servicios, Ajustes y Estados.
4. En Agenda, alternar Día/Semana y comprobar la semana móvil.
5. En Nueva cita, alternar Formulario, Inválido, Conflicto, Fallo y Completado.
6. Comparar los bocetos con la trazabilidad de `docs/DISENO.md`.

Resultado esperado: las decisiones visuales pueden revisarse sin confundir el prototipo con funcionalidad terminada.

### PROBLEMAS E INCONSISTENCIAS

- No hubo contradicción entre las decisiones provisionales del encargo y los requisitos; resolvieron dudas abiertas de E1-T01.
- La reactivación de registros archivados continúa pendiente.
- La zona horaria propuesta necesita revisión de lenguaje: nombre técnico, ciudad comprensible o ambos.
- La cita que cruza medianoche tiene modelo de datos propuesto, pero falta acordar su representación semanal.
- El prototipo se revisó en tamaños simulados; la comodidad táctil debe comprobarse en el teléfono real durante una etapa posterior.
- No se realizaron entrevistas ni pruebas con usuarios; el público inicial sigue siendo hipótesis.

### DECISIONES

- Cliente: nombre obligatorio; teléfono y notas opcionales.
- Servicio: nombre, duración, precio, moneda, preparación y limpieza.
- Tareas sin horario no ocupan disponibilidad.
- Sugerencias: tres opciones, intervalos de 15 minutos y 14 días; captura manual no limitada.
- Semana desde lunes configurable; canceladas ocultas por defecto con filtro.
- Márgenes visibles; zona del dispositivo propuesta y confirmada; inicio/fin incluyen fecha.
- Archivo conserva historial y oculta registros de selecciones nuevas.
- Todas estas decisiones son provisionales y se revisarán en E1-T03.

### SIGUIENTE TAREA

E1-T03: revisar los flujos y bocetos con el responsable y registrar correcciones. Dependencia: autorización explícita y respuestas a las preguntas de `docs/DISENO.md`. Criterio de cierre: recorrido de creación/corrección comprensible, decisiones provisionales confirmadas o ajustadas y observaciones reales registradas. No comenzar código de producción.

## 2026-10-18 — E1-T03: revisión y ajustes del diseño

### ETAPA Y TAREA

Etapa 1, tarea E1-T03. Revisar y ajustar los bocetos existentes de la entrega A, comprobar su adaptación en navegador y preparar evidencia para la revisión del responsable. El alcance registrado en `TAREAS.md` coincide con una revisión posterior a E1-T02; se precisó que los ajustes ya están implementados y que la aprobación del responsable sigue pendiente.

### RESULTADO

Se ajustaron Inicio, Agenda semanal y Nueva cita para Android y Windows simulados en navegador. La muestra usa una hora ficticia coherente (`09:20`), diferencia pasado y próximo, sustituye el resumen no comprobable por `3 compromisos pendientes`, separa preparación y limpieza, agrega acceso directo a tiempo personal y retira `Estados` del menú del producto.

La tarjeta de próxima cita en Windows conserva su altura natural. Las categorías usan iconos y etiquetas completas en lugar de letras. La creación contextual de cliente/servicio conserva el formulario de cita. El conflicto muestra el cruce sin mover datos y una alternativa solo modifica la hora cuando el usuario la elige; el guardado queda descrito como una revalidación con las mismas reglas.

E1-T03 no se declara completada porque todavía no existen comentarios ni aprobación del responsable. La etapa 1 continúa en curso. No se programó MAUI, no se instaló software y no se inició la etapa 2.

### ARCHIVOS

- `docs/prototipos/tiempo-en-orden-bocetos.html`: prototipo visual ajustado.
- Se generaron seis capturas para la primera revisión; después fueron sustituidas por las tres capturas corregidas registradas en la entrada siguiente para evitar conservar evidencia visual obsoleta.
- `docs/DISENO.md`: alcance, ajustes, matriz de comprobaciones y pendientes.
- `TAREAS.md`: E1-T03 en curso con revisión del responsable pendiente.
- `AVANCES.md`: esta evidencia.

### VALIDACIÓN

- Se renderizó el prototipo localmente y se recorrió en Chrome mediante automatización ya disponible, sin instalar herramientas.
- Se revisaron anchos móviles de 360, 390 y 430 px y anchos de escritorio de 1280 y 1440 px.
- En Inicio, Agenda semanal y Nueva cita no se detectaron controles recortados ni desbordamiento horizontal en esos tamaños.
- Con `Texto +` en Inicio móvil no se detectaron recortes ni desbordamiento horizontal en 360, 390 y 430 px.
- La navegación móvil muestra acciones principales sin que un panel flotante las tape; los formularios contextuales participan del flujo de la página.
- El recorrido inicial mediante teclado mostró foco sólido visible de 3 px y conserva el orden nativo.
- Al abrir y guardar un cliente contextual, la hora ficticia `17:20` permaneció en Nueva cita y el cliente nuevo quedó seleccionado.
- La semana móvil muestra la tira de siete días y solo el detalle del día seleccionado; la cuadrícula completa queda reservada para escritorio.
- Se comprobó adaptación al modo oscuro mediante preferencia simulada del navegador.
- Se comprobaron mensajes visibles de conflicto, alternativa aplicada y guardado fallido.
- Resultado de ejecución: cero errores de consola o de página.
- Las seis capturas se revisaron visualmente después de regenerarlas con el archivo actualizado.

Estas comprobaciones corresponden al prototipo en navegador. No son pruebas físicas de Android o Windows y no demuestran lógica de negocio, persistencia ni comportamiento MAUI.

### PROBLEMAS E INCONSISTENCIAS

- El alcance registrado de E1-T03 decía `Revisar con el responsable`, pero no distinguía ajustes técnicos de aprobación. Se aclaró: los ajustes y comprobaciones están hechos; el cierre depende de comentarios reales del responsable.
- La ocupación ficticia del conflicto mostraba `18:20–19:25`, incompatible con 45 minutos de servicio, 10 de preparación y 5 de limpieza. Se corrigió a `18:20–19:20`.
- La cuadrícula semanal de escritorio podía aparecer debajo de la semana móvil por precedencia de estilos. Se corrigió y se volvió a comprobar.
- Las decisiones DEC-0011 a DEC-0017 siguen provisionales. No se sustituyó ningún requisito ni se cambió ningún identificador.
- No se realizaron entrevistas, pruebas con usuarios ni pruebas en dispositivos físicos. El público inicial sigue siendo una hipótesis.

### DECISIONES

- No se creó una decisión nueva: los cambios son correcciones de presentación y comprobación coherentes con REQ-A-001 a REQ-A-009 y DEC-0008, DEC-0010, DEC-0014, DEC-0017 y DEC-0018.
- `Estados para revisión` pertenece al marco del prototipo y no a la navegación de la entrega A.
- La revisión automatizada y visual en navegador se registra como simulación del prototipo, nunca como prueba de plataforma.

### PRUEBA MANUAL PROPUESTA

1. Abrir `docs/prototipos/tiempo-en-orden-bocetos.html` en un navegador.
2. Comparar Android y Windows en Inicio y confirmar que `09:20`, el saludo y la cita de `10:00` son coherentes.
3. Abrir Agenda, seleccionar Semana y revisar preparación, servicio, limpieza y tiempo personal.
4. Abrir Nueva cita, elegir Conflicto y seleccionar una alternativa; confirmar que ningún otro compromiso cambia y que el formulario se actualiza solo tras la elección.
5. Abrir `Crear cliente` y `Crear servicio`, volver y comprobar que fecha y hora permanecen.
6. Activar `Texto +`, navegar con `Tab` y revisar modo oscuro del navegador.
7. Registrar comentarios reales sobre jerarquía, lenguaje, densidad y claridad.

Resultado esperado: el responsable puede recorrer creación y corrección de cita, comprender la semana móvil y registrar ajustes concretos sin confundir el prototipo con una aplicación terminada.

### SIGUIENTE TAREA

Continuar E1-T03 únicamente cuando el responsable entregue comentarios sobre estos bocetos. Criterio de cierre: comentarios reales registrados, decisiones provisionales confirmadas o corregidas y recorrido de creación/corrección aceptado. E2-T01 permanece bloqueada hasta que la etapa 1 se cierre explícitamente.

## 2026-10-18 — E1-T03: segunda revisión de capturas

### ETAPA Y TAREA

Continuación exclusiva de E1-T03 para corregir Semana móvil, Semana de escritorio y Nueva cita según la revisión de las seis capturas anteriores. No se inició MAUI ni la etapa 2.

### RESULTADO

- La semana móvil obtiene contador y filas de una misma colección ficticia. Muestra tres compromisos: Corte con Ana, Descanso 14:00–15:00 y Clase nocturna 18:30–20:00.
- La semana de escritorio usa una escala uniforme de 09:00 a 20:00. Cada hora ocupa 60 px y los eventos se desplazan verticalmente por minutos desde las 09:00.
- La cita del martes ocupa 60 minutos de 09:50 a 10:50, con segmentos proporcionales de 10 minutos de preparación, 45 de servicio y 5 de limpieza. Los horarios completos también aparecen en texto para no depender del tamaño de los segmentos.
- Nueva cita usa `Descartar cambios`. Si un campo fue modificado, muestra confirmación antes de abandonar el formulario. `Cancelar cita` queda reservado para citas ya registradas.
- La zona horaria se presenta como `Ciudad de México`; `America/Mexico_City` se conserva como identificador interno. El texto explica que la propuesta proviene del dispositivo actual y no se generaliza a todas las personas de México.
- Formulario, Inválido, Conflicto, Alternativa aplicada, Fallo y Completado se movieron fuera de la ventana del producto y están rotulados como `Acciones simuladas del prototipo`.

La revisión del responsable continúa pendiente. No se agregó lógica de negocio, persistencia ni una función nueva del producto.

### ARCHIVOS

- `docs/prototipos/tiempo-en-orden-bocetos.html`
- `docs/prototipos/capturas/semana-movil-corregida-e1-t03.png`
- `docs/prototipos/capturas/semana-escritorio-corregida-e1-t03.png`
- `docs/prototipos/capturas/formulario-actualizado-e1-t03.png`
- `docs/DISENO.md`
- `AVANCES.md`

### VALIDACIÓN

- Chrome local mediante automatización disponible; cero errores de consola o página.
- Semana móvil: contador `3`, tres filas y títulos `Corte · Ana`, `Descanso` y `Clase nocturna` obtenidos de la misma colección ficticia.
- Escala de escritorio: tres separaciones consecutivas de `60 px` entre horas.
- Cita de escritorio: desplazamiento `50 px` desde las 09:00, altura total `60 px` y segmentos medidos de `10`, `45` y `5 px`.
- Controles de estado dentro de la ventana del producto: `0`; etiqueta externa encontrada: `Acciones simuladas del prototipo`.
- Confirmación de descarte visible después de modificar la hora y elegir `Descartar cambios`.
- Nombre visible `Ciudad de México` e identificador `America/Mexico_City` presentes.
- Desbordamiento horizontal medido: `0 px` en las tres capturas.
- Las tres capturas se inspeccionaron visualmente. Estas comprobaciones corresponden al prototipo en navegador, no a pruebas físicas de Android o Windows.

### PROBLEMAS E INCONSISTENCIAS

- La semana móvil anunciaba tres compromisos y mostraba dos. Se eliminó la duplicación manual entre contador y filas.
- La semana de escritorio agrupaba 09:00, 10:00, 14:00 y 18:00 en filas visualmente iguales. Se sustituyó por una escala continua para evitar duraciones aparentes incorrectas.
- `Cancelar` era ambiguo en una cita nueva. Se sustituyó por `Descartar cambios` con confirmación condicionada a modificaciones.
- La zona técnica era el único nombre visible. Se añadió el nombre comprensible sin convertirlo en una regla general para México.

### DECISIONES

- DEC-0011 a DEC-0017 conservan su texto y estado provisional.
- La presentación de zona horaria responde la pregunta visual sobre nombre técnico o comprensible: se muestran ambos, con funciones distintas.
- No se registró aprobación del responsable ni se cerró E1-T03.

### PRUEBA MANUAL PROPUESTA

1. Abrir el prototipo y seleccionar Android, Agenda y Semana; confirmar que contador y lista muestran los mismos tres compromisos.
2. Seleccionar Windows y revisar que horas y eventos conservan una escala vertical uniforme.
3. Abrir Nueva cita, elegir `Formulario` en los controles externos, modificar un campo y pulsar `Descartar cambios`; comprobar la confirmación.
4. Revisar el texto `Ciudad de México` y el identificador interno.

### SIGUIENTE TAREA

Mantener E1-T03 pendiente hasta recibir comentarios reales del responsable sobre estas tres capturas y sobre DEC-0011 a DEC-0017. No iniciar E2-T01.

## 2026-10-19 — E1-T03: registro de revisión técnica asistida

### ETAPA Y TAREA

Continuación documental de E1-T03. Se registraron DEC-0011 a DEC-0017 como propuestas resultantes de revisión técnica asistida, sin atribuirlas a una aprobación del responsable. No se inició la etapa 2.

### RESULTADO

- Revisión documental realizada: se contrastaron requisitos, decisiones y diseño sin cambiar identificadores.
- Diseño revisado mediante las tres capturas vigentes de E1-T03.
- Pruebas de interacción reportadas por Codex: recorridos simulados del prototipo HTML en navegador, no pruebas de una aplicación implementada.
- Aprobación del responsable pendiente.
- Validación en teléfono real pendiente de una versión ejecutable posterior a la autorización de implementación.
- No se regeneraron ni modificaron bocetos o capturas.

### ARCHIVOS

- `docs/REQUISITOS.md`
- `docs/DECISIONES.md`
- `docs/DISENO.md`
- `AVANCES.md`

### VALIDACIÓN

- Se verificó la conservación de los identificadores de requisitos y decisiones.
- Se comprobó la relación explícita de DEC-0011 a DEC-0017 con los requisitos de la entrega A.
- Se revisó que los cambios de esta continuación sean únicamente documentales.
- No se ejecutaron pruebas físicas de Android o Windows ni se declaró validación en teléfono real.

### PROBLEMAS E INCONSISTENCIAS

- No se encontraron contradicciones entre DEC-0011 a DEC-0017 y los requisitos vigentes.
- `docs/DISENO.md` calificaba como “resuelta” la presentación comprensible de la zona horaria. Esa redacción podía confundirse con aprobación; se corrigió a propuesta técnica documentada con aprobación pendiente.

### DECISIONES

- DEC-0011 a DEC-0017 conservan contenido e identificadores y pasan a describirse como propuestas revisadas técnicamente en E1-T03.
- La revisión técnica asistida, la aprobación del responsable y la validación física se registran como evidencias diferentes.

### PRUEBA MANUAL PROPUESTA

El responsable puede revisar la tabla de trazabilidad en `docs/REQUISITOS.md`, el texto íntegro de DEC-0011 a DEC-0017 y la matriz de evidencia en `docs/DISENO.md`, y después registrar aprobación, rechazo o ajustes sin inferirlos de esta revisión.

### SIGUIENTE TAREA

Continuar E1-T03 únicamente al recibir comentarios reales del responsable. Criterio de cierre: registrar su aprobación o correcciones y mantener separada la futura validación en teléfono real. E2-T01 permanece bloqueada.

## 2026-10-19 — Aprobación de E1-T03 y cierre de la etapa 1

### ETAPA Y TAREA

E1-T03 y etapa 1. El responsable declaró: “Apruebo el diseño y las decisiones revisadas en E1-T03”.

### RESULTADO

- E1-T03 queda completada.
- La etapa 1 queda completada.
- El diseño y DEC-0011 a DEC-0017 quedan aprobados por el responsable.
- Se mantiene pendiente la validación en teléfono real; la aprobación del diseño no demuestra comportamiento de una aplicación implementada.

### ARCHIVOS

- `docs/REQUISITOS.md`
- `docs/DECISIONES.md`
- `docs/DISENO.md`
- `TAREAS.md`
- `AVANCES.md`

### VALIDACIÓN Y PENDIENTES

Se revisó que la aprobación no se describa como prueba física, prueba con usuarios ni ejecución MAUI. La futura validación en teléfono real requerirá una versión ejecutable y se registrará por separado.

## 2026-10-19 — E2-T01: versiones objetivo y requisitos del entorno

### ETAPA Y TAREA

Etapa 2, tarea E2-T01. Elegir versiones objetivo, volver a diagnosticar el entorno y documentar su preparación sin instalar herramientas ni crear la solución.

### RESULTADO

E2-T01 queda completada documentalmente. Se eligió como combinación candidata .NET 10 LTS con .NET MAUI 10, Android API 36/JDK 21 y Windows SDK estable 10.0.26100.x. Se separaron los objetivos de compilación de los mínimos del cliente:

| Destino | Compilación candidata | Mínimo del cliente |
| --- | --- | --- |
| Android | `net10.0-android`, SDK Platform 36, Build-Tools 36.0.0, Microsoft OpenJDK 21 | Android 7.0, API 24 |
| Windows | `net10.0-windows10.0.19041.0`, Windows SDK estable 10.0.26100.x y Windows App SDK transitivo de MAUI | Windows 10 1809, build 17763 |

La versión preparada para fijar es SDK .NET `10.0.113` con runtime `10.0.13` y .NET MAUI `10.0.110`. La instalación actual `10.0.102`/runtime `10.0.2`/MAUI `10.0.20` es de la misma base y se aprovecha, pero requiere servicio por correcciones de seguridad, no una actualización motivada solo por novedad. La combinación permanece candidata hasta compilar ambos destinos en E2-T02.

### DIAGNÓSTICO REAL DEL EQUIPO

- Windows x64, versión mostrada como 25H2, build 26200.9457; 16 GB de RAM aproximados y 380 GB libres.
- .NET SDK `10.0.102`, MSBuild `18.0.7`, runtimes `10.0.2`.
- Carga `maui` instalada: manifiesto MAUI `10.0.20/10.0.100`; .NET for Android `36.1.43`.
- Plantillas MAUI disponibles.
- Visual Studio Code `1.140.0` instalado, sin las extensiones oficiales C#, C# Dev Kit ni .NET MAUI.
- No existe Visual Studio completo; `vswhere` solo encontró SQL Server Management Studio 22.
- Android Studio 2026.1 instalado con JDK 25 integrado. No existe JDK 21 configurado y `java`/`javac` no están en `PATH`.
- Android SDK en `C:\Users\adria\AppData\Local\Android\Sdk`: Platform 35, 36 y 37.0; Build-Tools 36.0.0; Platform-Tools 37.0.1; emulador 37.1.11; Command-line Tools 12.0; NDK 28.2.13676358.
- `ANDROID_HOME`, `ANDROID_SDK_ROOT` y `JAVA_HOME` no están definidos.
- No hay AVD y `adb devices -l` no encontró teléfono conectado.
- No existe `C:\Program Files (x86)\Windows Kits\10`; no se encontró Windows SDK instalado.
- El hipervisor está presente. El estado de Modo de desarrollador no quedó confirmado porque las claves consultadas no existían.
- `LongPathsEnabled` vale 0.

### COMANDOS Y COMPROBACIONES EJECUTADAS

- `Get-ComputerInfo` y lectura de `HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion`: sistema, versión y build.
- `dotnet --info`, `dotnet --list-sdks`, `dotnet workload list` y `dotnet new list maui`: SDK, runtimes, cargas y plantillas.
- `vswhere -all -products * -format json`: ausencia de Visual Studio completo.
- `code --version` y `code --list-extensions --show-versions`: editor y extensiones.
- Ejecución directa de `java.exe` y `javac.exe` incluidos en Android Studio: JDK 25.0.2, no elegido como JDK del proyecto.
- Ejecución directa de `adb version`, `emulator -version`, `sdkmanager --version` y `sdkmanager --list_installed`: inventario Android. `sdkmanager` devolvió una advertencia por XML de repositorio versión 4, pero enumeró los paquetes.
- `emulator -list-avds` y `adb devices -l`: sin AVD ni dispositivo conectado.
- Inspección de Windows Kits, variables de entorno, Modo de desarrollador, rutas largas, virtualización y espacio libre.

Estas comprobaciones diagnostican herramientas; no constituyen compilación, ejecución ni prueba de Android o Windows.

### DOCUMENTACIÓN OFICIAL CONSULTADA

- Política y descarga de .NET 10: LTS activo hasta 2028-11-14; el parche de seguridad vigente es runtime 10.0.13 con SDK 10.0.113 o 10.0.402.
- .NET MAUI 10 y .NET for Android: API 36, JDK 21 y API 24 recomendada como mínimo.
- Plataformas MAUI: Windows 10 1809 o posterior y Android API 24 para la línea documentada actual.
- Android Developers: Android 16/API 36 estable y Build-Tools 36.x.
- Compatibilidad de Visual Studio: .NET 10 requiere Visual Studio 2026 18.0 o posterior cuando se usa ese IDE.
- Windows SDK: familia 26100 todavía compatible; el SDK 19041 está fuera de soporte, aunque el TFM puede conservar el contrato 19041 y el cliente mínimo 17763.
- Windows App SDK: compatible hacia Windows 10 1809 y separado del SDK usado para compilar.

Las URL exactas quedaron registradas en `docs/DECISIONES.md`.

### COMPONENTES FALTANTES Y PREPARACIÓN PROPUESTA

1. Instalar SDK .NET 10.0.113 x64 y actualizar la carga estable MAUI 10 dentro de la banda 10.0.1xx.
2. Instalar Microsoft OpenJDK 21 x64 y definir `JAVA_HOME` a su ruta efectiva.
3. Definir `ANDROID_HOME=C:\Users\adria\AppData\Local\Android\Sdk`; agregar `platform-tools` y `emulator` a `PATH`.
4. Instalar Windows SDK estable 10.0.26100.x.
5. Instalar las extensiones oficiales C#, C# Dev Kit y .NET MAUI en VS Code. Visual Studio 2026 18.0+ con carga MAUI queda como alternativa, no como duplicación obligatoria.
6. Confirmar o habilitar Modo de desarrollador antes de desplegar Windows.
7. Crear un AVD API 36 x64 o conectar el teléfono real únicamente cuando corresponda ejecutar Android.
8. En E2-T02, crear la solución mínima, restaurar y compilar Android y Windows por separado; confirmar o corregir DEC-0019 con la evidencia real.

Ninguno de estos pasos se ejecutó en E2-T01.

### PROBLEMAS Y BLOQUEOS

- El JDK 25 incluido con Android Studio no es la versión elegida y no está configurado para .NET; falta OpenJDK 21.
- Falta el Windows SDK, por lo que la compilación Windows no se da por disponible.
- El SDK .NET instalado contiene runtime 10.0.2, anterior al parche de seguridad vigente 10.0.13.
- No hay dispositivo ni AVD para ejecución Android; esto no bloquea una compilación inicial.
- Falta confirmar si Command-line Tools 12 puede mantenerse durante la preparación; solo se actualizará si la validación o restauración lo exige.
- No se comprobó compilación mínima porque crear la solución corresponde a E2-T02.

### DECISIONES

- DEC-0019 registra versiones objetivo y mínimos del cliente.
- DEC-0020 registra el diagnóstico, faltantes y preparación exacta.
- No se instalaron herramientas, no se creó código y no se inició E2-T02.

### PRUEBA MANUAL PROPUESTA

Después de autorizar y preparar el entorno, E2-T02 deberá registrar `dotnet --info`, `dotnet workload list`, versiones del JDK y SDK Android, restauración y una compilación mínima independiente para Android y Windows. No se considerará confirmada la combinación si solo compila uno de los dos destinos.

### SIGUIENTE TAREA

E2-T02: preparar los componentes faltantes autorizados, crear la solución mínima y confirmar la combinación candidata mediante compilaciones separadas. Dependencia: E2-T01 completada y autorización explícita. Criterio de cierre: solución mínima con reglas separadas de interfaz y resultados reales de compilación para Android y Windows, o bloqueos documentados. No iniciada en este encargo.
