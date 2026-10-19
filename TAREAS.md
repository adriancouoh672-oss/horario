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

Estado: en curso. E1-T01 y E1-T02 completadas; E1-T03 tiene ajustes implementados y revisión del responsable pendiente.

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
- Evidencia: decisiones provisionales DEC-0011 a DEC-0017, naturaleza del prototipo DEC-0018, revisión visual y entrada E1-T02 en `AVANCES.md`.
- Criterio de cierre: prototipos navegables y revisados visualmente, sin lógica de negocio ni apariencia de funciones B/C.

### E1-T03. Revisar con el responsable

- Estado: en curso. Ajustes visuales y comprobaciones del prototipo completados; falta la revisión y comentarios del responsable.
- Dependencia: E1-T02
- Alcance confirmado: revisar y ajustar los bocetos existentes de la entrega A, comprobar su adaptación visual en navegador y registrar observaciones sin iniciar MAUI ni la etapa 2.
- Evidencia: `docs/DISENO.md`, capturas en `docs/prototipos/capturas/` y entrada E1-T03 de `AVANCES.md`.
- Criterio de cierre: flujo de creación/corrección de cita comprensible y comentarios registrados.

## Etapa 2: base ejecutable

Estado: no iniciada; depende de etapa 1 aprobada.

- E2-T01: elegir versiones objetivo y registrar la decisión.
- E2-T02: crear solución mínima y separación de lógica/interfaz.
- E2-T03: implementar navegación con datos ficticios identificados.
- E2-T04: compilar y ejecutar en cada destino disponible, documentando los pendientes.

## Etapa 3: datos y agenda local

Estado: no iniciada; depende de base ejecutable.

- E3-T01: diseñar modelo y migraciones.
- E3-T02: implementar persistencia SQLite.
- E3-T03: implementar operaciones locales de clientes, servicios, citas y tiempo personal.
- E3-T04: completar agenda diaria/semanal y pruebas de persistencia.

## Etapa 4: motor de disponibilidad

Estado: no iniciada; depende de datos locales estables.

- E4-T01: implementar intervalos y cruces en lógica independiente.
- E4-T02: implementar sugerencias deterministas.
- E4-T03: integrar conflicto y búsqueda en Nueva cita.
- E4-T04: ejecutar pruebas de los escenarios base y límites.

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

Estado: no iniciada; depende de funciones centrales verificadas.

- E7-T01: revisar accesibilidad, navegación y estados.
- E7-T02: implementar y comprobar respaldo/restauración.
- E7-T03: revisar seguridad y medir rendimiento con volumen registrado.
- E7-T04: preparar y registrar piloto real en `docs/PILOTO.md`.

## Etapa 8: entrega y propuesta comercial

Estado: no iniciada; depende del piloto y correcciones prioritarias.

- E8-T01: generar paquetes posibles y documentar instalación.
- E8-T02: preparar manual, guía técnica y trazabilidad.
- E8-T03: preparar demostración de cinco minutos basada en funciones reales.
- E8-T04: redactar propuesta comercial sin promesas ni precio no validado.
