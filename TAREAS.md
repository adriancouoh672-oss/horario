# Plan de tareas

Cada tarea debe cerrarse con evidencia en `AVANCES.md`. Una etapa posterior permanece bloqueada hasta recibir autorización explícita y cumplir la dependencia indicada.

## Etapa 0: preparar el proyecto

### E0-T01. Leer guía e inspeccionar repositorio

- Estado: completada
- Dependencia: ninguna
- Resultado: guía leída; repositorio y archivos existentes inventariados.
- Evidencia: entrada de etapa 0 en `AVANCES.md`.

### E0-T02. Diagnosticar entorno

- Estado: completada
- Dependencia: E0-T01
- Resultado: sistema operativo, Git, .NET, MAUI, herramientas Android y herramientas Windows registradas sin instalar componentes.
- Evidencia: comandos y resultados en `AVANCES.md`.

### E0-T03. Crear documentación base

- Estado: completada
- Dependencia: E0-T01
- Resultado: `AGENTS.md`, `PROYECTO.md`, `TAREAS.md`, `AVANCES.md`, `docs/DECISIONES.md` y `docs/REQUISITOS.md` coherentes.
- Evidencia: revisión de archivos y enlaces internos.

### E0-T04. Validar y cerrar etapa 0

- Estado: completada
- Dependencia: E0-T02 y E0-T03
- Criterio de cierre: documentación existe, separa alcance actual y futuro, registra bloqueos y propone una siguiente tarea pequeña.

## Etapa 1: requisitos y bocetos

Estado: completada el 2026-10-19. E1-T01, E1-T02 y E1-T03 cerradas; el diseño y DEC-0011 a DEC-0017 fueron aprobados por el responsable. La validación en teléfono real se conserva como comprobación posterior a la implementación.

### E1-T01. Refinar historias y criterios

- Estado: completada
- Requisitos: REQ-A-001 a REQ-A-009
- Dependencia: etapa 0 completada y autorización explícita recibida. Las respuestas aún desconocidas se conservaron como decisiones pendientes, sin inventarlas.
- Resultado: cada requisito A registra usuario y necesidad, historia, prioridad, dependencias, flujo principal, alternativas, validaciones, mensajes, criterios Dado/Cuando/Entonces, ejemplo ficticio y dudas.
- Evidencia: `docs/REQUISITOS.md`, decisiones DEC-0007 a DEC-0010 y entrada E1-T01 de `AVANCES.md`.
- Criterio de cierre: reglas críticas trazables y comprobables; entregas B/C siguen posteriores; no se creó código ni bocetos.

### E1-T02. Diseñar flujos y bocetos

- Estado: completada
- Requisitos: REQ-A-001 a REQ-A-009
- Dependencia: E1-T01
- Alcance alineado: seis flujos; Inicio, Agenda y Nueva cita detallados para Android/Windows; Clientes, Servicios y Ajustes secundarios; sistema visual, estados, accesibilidad, trazabilidad y preguntas de revisión.
- Resultado: especificación en `docs/DISENO.md` y prototipo visual estático en `docs/prototipos/tiempo-en-orden-bocetos.html`.
- Evidencia: DEC-0011 a DEC-0017, entonces provisionales y aprobadas al cerrar E1-T03; naturaleza del prototipo DEC-0018; revisión visual y entrada E1-T02 en `AVANCES.md`.
- Criterio de cierre: prototipos navegables y revisados visualmente, sin lógica de negocio ni apariencia de funciones B/C.

### E1-T03. Revisar con el responsable

- Estado: completada el 2026-10-19.
- Dependencia: E1-T02
- Alcance confirmado: revisar y ajustar los bocetos existentes de la entrega A, comprobar su adaptación visual en navegador y registrar observaciones sin iniciar MAUI ni la etapa 2.
- Evidencia: `docs/DISENO.md`, capturas en `docs/prototipos/capturas/`, aprobación explícita del responsable y entradas E1-T03 de `AVANCES.md`.
- Criterio de cierre: cumplido; flujo y decisiones aprobados. La validación en teléfono real no se confunde con el cierre del diseño y continúa pendiente.

## Etapa 2: base ejecutable

Estado: cierre de plataforma pendiente. E2-T01, E2-T02 y E2-T03 completadas; E2-T04 ejecutada con la validación interactiva de Windows y la validación en teléfono real pendientes. DEC-0022 autoriza continuar la versión local sin considerar aprobados esos pendientes.

### E2-T01. Elegir versiones objetivo y requisitos del entorno

- Estado: completada el 2026-10-19.
- Requisitos relacionados: REQ-A-001 a REQ-A-009 como base ejecutable compartida para Android y Windows.
- Dependencia: etapa 1 aprobada.
- Resultado: combinación candidata de .NET 10/MAUI 10, API de compilación y mínimos de cliente definidos; entorno instalado vuelto a inspeccionar; faltantes y preparación documentados sin instalar herramientas ni crear solución.
- Evidencia: DEC-0019, DEC-0020 y entrada E2-T01 en `AVANCES.md`.
- Criterio de cierre: versiones de compilación y mínimos de cliente separados, documentación oficial vigente registrada y comprobación mínima asignada a E2-T02.

### E2-T02. Crear solución mínima y separación de lógica/interfaz

- Estado: completada el 2026-10-19.
- Requisitos relacionados: REQ-A-001 a REQ-A-009.
- Dependencia: E2-T01 completada y autorización explícita para preparar el entorno y crear la solución.
- Resultado: entorno CLI preparado, solución mínima creada con `TiempoEnOrden.App` y `TiempoEnOrden.Core`, y combinación de DEC-0019 confirmada para compilación.
- Evidencia: `global.json`, `TiempoEnOrden.sln`, proyectos bajo `src/`, `README.md`, DEC-0019 a DEC-0021 y entrada E2-T02 en `AVANCES.md`.
- Criterio de cierre: cumplido; Core, Android y Windows restauraron y compilaron por separado sin errores. No se ejecutó la aplicación.

### E2-T03. Implementar navegación y pantallas con datos ficticios

- Estado: completada el 2026-10-19.
- Requisitos relacionados: REQ-A-001 a REQ-A-009, únicamente en presentación y navegación; no implementa todavía la persistencia ni las reglas funcionales dependientes de etapas posteriores.
- Dependencia: E2-T02 completada y diseño aprobado en E1-T03.
- Resultado: Inicio, Agenda diaria/semanal, Nueva cita, Tiempo personal, Clientes, Servicios y Ajustes implementados en XAML con MVVM sencillo, navegación adaptable y fuente ficticia compartida en memoria.
- Evidencia: código bajo `src/TiempoEnOrden.App`, README y entrada E2-T03 en `AVANCES.md`; compilaciones separadas Android/Windows y arranque de proceso con ventana principal en Windows.
- Criterio de cierre: cumplido para implementación y compilación. La herramienta de revisión no pudo seleccionar la ventana WinUI, por lo que capturas y recorrido visual automatizado quedan pendientes y no se sustituyen por el prototipo HTML.

### E2-T04. Ejecutar y validar Windows y Android

- Estado: ejecutada el 2026-10-19 con pendientes de plataforma; la etapa 2 permanece en curso.
- Requisitos relacionados: REQ-A-001 a REQ-A-009, solo para la demostración navegable implementada en E2-T03.
- Dependencia: E2-T03 completada.
- Resultado: Windows compiló y abrió una ventana receptiva, pero su recorrido quedó bloqueado porque la herramienta no expuso la ventana WinUI de forma seleccionable. Android compiló, se instaló y completó los recorridos en un AVD API 36. Se corrigieron la confirmación al descartar después de una creación contextual y la presentación de fechas en español.
- Evidencia: `docs/PRUEBAS-E2.md` y capturas MAUI reales bajo `docs/pruebas-e2/`.
- Criterio de cierre: parcialmente cumplido. Android en emulador está aprobado; Windows requiere el recorrido manual del responsable y Android requiere validación en teléfono real. No se inició la etapa 3.

## Etapa 3: datos y agenda local

Estado: autorizada con prioridad de entrega; depende de la base compilable de E2 y conserva pendientes de plataforma.

- E3-T01/E3-T02 — bloque 1: completado el 2026-10-19. Modelo persistente, proyecto Infrastructure, SQLite e inicialización separada de la demostración. Evidencia: verificación de escritura/lectura tras reabrir conexión y compilaciones Android/Windows sin errores.
- E3-T03A — bloque 2: completado el 2026-10-19. Clientes y servicios con guardado real. Evidencia: crear, consultar y conservar tras reabrir SQLite; interfaz conectada al repositorio local.
- E3-T03B — bloque 3: completado el 2026-10-19. Crear, consultar, editar y cancelar citas con valores históricos del servicio. Evidencia: verificador SQLite y recorrido real en el AVD tras cierre forzado y reapertura.
- E3-T03C — bloque 4: completado el 2026-10-19. Tiempo personal, horario laboral y descansos persistentes e integrados en Inicio/Agenda. Evidencia: alta/configuración, cierre, reapertura y consulta automatizada.
- E3-T04: completado el 2026-10-19. Agenda diaria/semanal alimentada por citas, tiempo personal y descansos reales; filtros y contadores usan esa misma fuente.

## Etapa 4: motor de disponibilidad

Estado: autorizada después de completar y comprobar la etapa 3.

- E4-T01 — bloque 5: completado el 2026-10-19. Intervalos y cruces en Core, incluyendo preparación, limpieza, descanso, horario, tiempo personal, canceladas y exclusión de la propia cita al editar.
- E4-T02/E4-T03 — bloque 6: completado el 2026-10-19. Sugerencias deterministas de hasta tres espacios, cada 15 minutos y hasta 14 días, usando exactamente las reglas del guardado.
- E4-T04: completado el 2026-10-19. Pruebas de escenarios base, márgenes, límites contiguos, cancelación, autoexclusión y validez de todas las sugerencias.

## Mantenimiento autorizado posterior a la entrega

- M1 — completado el 2026-10-19. Acceso explícito a cualquier cita desde Agenda y aclaración entre catálogo de servicios, Nueva cita y tiempo personal. Requisitos: REQ-A-001 a REQ-A-003. Dependencia: autorización explícita del responsable. Evidencia: DEC-0028, compilaciones Android/Windows y verificador SQLite. Pendiente: recorrido manual en paquete Release.
- M2 — completado el 2026-10-20. Edición/archivo de clientes y edición/eliminación de tiempo personal. Requisitos: REQ-A-003 y REQ-A-004. Evidencia: DEC-0029, verificador SQLite y compilación Windows. Pendiente: recorrido manual.
- M3 — completado el 2026-10-21. Corrección de **Ver agenda** y presentación de una sola tarjeta por cita, conservando preparación y limpieza como ocupación. Requisitos: REQ-A-001, REQ-A-006 y REQ-A-008. Dependencia: versión local funcional y reporte del responsable. Evidencia: DEC-0030, verificador, compilaciones Android/Windows y publicación Windows M3. Pendiente: recorrido manual en la interfaz.

## Etapa 5: cuenta y sincronización

Estado: no iniciada; depende de entrega A comprobada y decisión de alojamiento.

- E5-T01: diseñar API, autorización y política de conflictos.
- E5-T02: implementar persistencia central y endpoints.
- E5-T03: implementar cola local, reintentos y sincronización.
- E5-T04: validar desconexión, reconexión y dos dispositivos reales o declarar lo pendiente.

## Etapa 6: recordatorios coordinados

Estado: no iniciada; depende de sincronización estable.

- E6-T01: implementar preferencia manual y adaptadores de plataforma.
- E6-T02: coordinar confirmación y aviso de respaldo.
- E6-T03: probar permisos, reinicio, segundo plano y desconexión por plataforma.

## Etapa 7: calidad y piloto

Estado: bloque de entrega local autorizado; las pruebas de usuarios y piloto real siguen sin inventarse.

- E7-T01: revisar accesibilidad, navegación y estados.
- E7-T02 — bloque 7: completado el 2026-10-19. Respaldo/restauración de la base local comprobados mediante modificación posterior y recuperación.
- E7-T03: revisar seguridad y medir rendimiento con volumen registrado.
- E7-T04: preparar y registrar piloto real en `docs/PILOTO.md`.

## Etapa 8: entrega y propuesta comercial

Estado: bloque de entrega autorizado después de comprobar respaldo/restauración; no depende de inventar un piloto.

- E8-T01 — bloque 8: completado el 2026-10-19. APK Release local y publicación Windows x64 sin empaquetar generados; instalación y límites documentados.
- E8-T02 — bloque 8: completado el 2026-10-19. Manual breve, guía técnica, trazabilidad y matriz de pruebas preparados.
- E8-T03 — bloque 8: completado el 2026-10-19. Guion y recorrido de demostración basados solo en funciones comprobadas y pendientes declarados.
- E8-CIERRE — completado el 2026-10-19. Documentación de `afac4de` corregida para diferenciar pruebas de desarrollo y APK Release, límites de horario, respaldo interno y dependencias Windows. El recorrido Release de 10 minutos permanece no ejecutado.
- E8-T04: redactar propuesta comercial sin promesas ni precio no validado.
