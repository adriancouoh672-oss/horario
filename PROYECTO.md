# Tiempo en Orden

## Resumen

Tiempo en Orden es una agenda personal y de negocio para Android y Windows. Su objetivo es ayudar a una persona que trabaja por citas a conocer su disponibilidad real, reservar tiempo personal y laboral, detectar conflictos y recibir recordatorios en sus dispositivos.

El nombre del producto es provisional. La propuesta se validará con usuarios y con la rúbrica escolar antes de afirmar que existe demanda comercial.

## Usuario inicial propuesto

Trabajador independiente que atiende por citas, por ejemplo un barbero o estilista, utiliza computadora y teléfono y necesita coordinar trabajo, clases, descansos y compromisos personales.

Esta definición es una hipótesis. Debe contrastarse mediante entrevistas reales con 3 a 5 posibles usuarios; no se inventarán respuestas ni testimonios.

## Problema

Las citas del negocio y los compromisos personales suelen registrarse en lugares distintos. Esto dificulta conocer la disponibilidad, respetar tiempos de preparación y limpieza, evitar cruces y recordar una cita al cambiar de dispositivo.

## Propuesta de valor

Una agenda que encuentra horarios compatibles con la vida personal y laboral y recuerda los compromisos en los dispositivos configurados. El motor inicial se basará en reglas deterministas, no en IA generativa.

## Alcance progresivo

### Entrega A: base local

- Aplicación para Android y Windows.
- Agenda, clientes, servicios, actividades personales y tareas.
- Horario laboral, descansos y bloqueos.
- Detección de conflictos y búsqueda de hasta tres espacios válidos.
- Persistencia local.

### Entrega B: versión conectada

- Una cuenta utilizable en ambos dispositivos.
- Sincronización, trabajo sin conexión y recuperación.
- Conflictos visibles y resolubles, sin sobrescritura silenciosa.
- Recordatorios coordinados con confirmación compartida.

### Entrega C: piloto comercial

- Instaladores que el entorno permita generar.
- Documentación técnica y de usuario.
- Respaldo y restauración comprobados.
- Pruebas con usuarios y registro de resultados reales.
- Demostración y propuesta comercial ajustadas a las funciones terminadas.

## Fuera de la primera entrega

Reservas públicas, lista de espera, reorganización automática de tareas, varios empleados, cobros en línea, WhatsApp automático, calendarios externos, voz, mapas e iOS. Solo se evaluarán después de validar utilidad, costo y mantenimiento.

## Arquitectura candidata

La guía propone C# y .NET MAUI con XAML para Android y Windows, SQLite para datos locales, y posteriormente una API ASP.NET Core y SignalR. Es una dirección, no una autorización para implementar todos los componentes ni una versión técnica fijada.

Organización candidata cuando comience el código:

- `src/TiempoEnOrden.App`: vistas, ViewModels e integraciones de plataforma.
- `src/TiempoEnOrden.Core`: modelos y reglas sin dependencia de interfaz.
- `src/TiempoEnOrden.Infrastructure`: persistencia y, más adelante, sincronización.
- `src/TiempoEnOrden.Api`: solo al autorizar la entrega conectada.
- `tests`: pruebas de reglas, persistencia y sincronización.
- `docs`: requisitos, decisiones, diseño y manuales.

La estructura podrá simplificarse si la primera solución ejecutable demuestra que menos proyectos son suficientes.

## Estado al cerrar la etapa 0

- Repositorio Git local creado, todavía sin commits.
- No existe código de aplicación.
- Documentación base creada.
- Entorno Windows y herramientas diagnosticados; existen dependencias parciales para MAUI y Android, pero ninguna plataforma se ha compilado ni ejecutado.

## Información pendiente del responsable

- Fecha de entrega y rúbrica escolar.
- Evidencias exigidas por el profesor.
- Qué significa “vender” para la materia: venta real, propuesta o demostración.
- Experiencia actual del responsable con C# y .NET.
- Teléfono Android disponible y versión de Android.
- Usuario o cliente objetivo concreto.
- Presupuesto para alojamiento y mantenimiento.
- Restricciones de datos personales, respaldo y publicación.

No se elaborará un cronograma semanal de desarrollo ni se fijará precio sin estos datos. Esta restricción no elimina la vista semanal de la agenda incluida en la entrega A.
