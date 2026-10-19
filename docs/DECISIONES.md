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
- Estado: aceptada como base tecnológica; versiones candidatas en DEC-0019 pendientes de compilación mínima
- Decisión: C#/.NET MAUI con XAML, SQLite local y, en una entrega posterior, ASP.NET Core y SignalR.
- Motivo: permite compartir modelos y reglas entre Android y Windows, conservar integraciones específicas por plataforma y seguir la arquitectura de la guía.
- Pendiente: comprobar la combinación de versiones de DEC-0019 mediante la solución mínima de E2-T02.

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

## Revisión y aprobación de DEC-0011 a DEC-0017

- Fecha de registro: 2026-10-19
- Resultado: las propuestas fueron contrastadas documentalmente con REQ-A-001 a REQ-A-009, revisadas en las tres capturas vigentes y recorridas mediante interacciones simuladas reportadas por Codex.
- Alcance de la evidencia: revisión técnica asistida del prototipo en navegador, seguida de aprobación explícita del responsable el 2026-10-19. No es prueba con usuarios ni validación física en Android o Windows.
- Contradicciones: no se encontraron contradicciones entre DEC-0011 a DEC-0017 y los requisitos vigentes. Antes de la aprobación se corrigió una formulación en `docs/DISENO.md` que llamaba “resuelta” a la presentación de zona horaria; desde el 2026-10-19 forma parte de la decisión aprobada.
- Estado común: las siete decisiones fueron aprobadas por el responsable el 2026-10-19. La validación en teléfono real después de implementar una versión ejecutable sigue pendiente.

## DEC-0011. Datos mínimos de clientes y servicios

- Fecha: 2026-10-18
- Estado: aprobada por el responsable el 2026-10-19
- Decisión: cliente con nombre obligatorio y teléfono/notas opcionales. Servicio con nombre, duración, precio, moneda, preparación y limpieza.
- Motivo: reducir captura innecesaria sin perder la información requerida por citas, precios y disponibilidad.
- Consecuencia: los formularios distinguen campos obligatorios y opcionales; no se inventan más datos personales.

## DEC-0012. Tareas sin horario

- Fecha: 2026-10-18
- Estado: aprobada por el responsable el 2026-10-19
- Decisión: una tarea sin fecha y hora no ocupa disponibilidad.
- Motivo: un pendiente no representa por sí mismo un intervalo reservado.
- Consecuencia: las tareas sin horario aparecen en una lista de pendientes, no dentro de los cálculos de conflicto.

## DEC-0013. Parámetros iniciales de sugerencias

- Fecha: 2026-10-18
- Estado: aprobada por el responsable el 2026-10-19
- Decisión: buscar hasta tres opciones en intervalos de 15 minutos y con horizonte inicial de 14 días.
- Aclaración: la granularidad se aplica a la búsqueda automática; no impide registrar manualmente una hora válida distinta.
- Consecuencia: el diseño explica el periodo de búsqueda y permite ampliar el horizonte en una evolución posterior si se aprueba.

## DEC-0014. Presentación de la agenda

- Fecha: 2026-10-18
- Estado: aprobada por el responsable el 2026-10-19
- Decisión: semana desde lunes, configurable; canceladas ocultas por defecto con filtro; preparación y limpieza visibles y diferenciadas.
- Motivo: reducir ruido sin ocultar historial y hacer comprensible la ocupación real.
- Consecuencia: la semana móvil usa selector horizontal de días y detalle del día seleccionado; el escritorio aprovecha una cuadrícula semanal.

## DEC-0015. Zona horaria y compromisos entre fechas

- Fecha: 2026-10-18
- Estado: aprobada por el responsable el 2026-10-19
- Decisión: proponer la zona horaria del dispositivo y pedir confirmación. Inicio y fin conservan fecha para admitir medianoche.
- Motivo: evitar ambigüedad temporal y permitir compromisos que terminen al día siguiente.
- Consecuencia: Ajustes muestra la zona confirmada y Nueva cita enseña fecha de fin cuando cambia el día.

## DEC-0016. Archivo sin pérdida de historial

- Fecha: 2026-10-18
- Estado: aprobada por el responsable el 2026-10-19
- Decisión: archivar conserva el historial y oculta clientes o servicios de las selecciones nuevas por defecto.
- Motivo: impedir pérdida de referencias históricas sin saturar los formularios.
- Consecuencia: Clientes y Servicios ofrecen filtro para archivados; la reactivación queda pendiente de confirmar.

## DEC-0017. Lenguaje visual adaptable

- Fecha: 2026-10-18
- Estado: aprobada por el responsable el 2026-10-19
- Decisión: compartir jerarquía, colores semánticos, componentes y vocabulario entre Android y Windows, adaptando navegación y densidad a cada plataforma.
- Motivo: mantener coherencia sin forzar una interfaz idéntica en teléfono y escritorio.
- Consecuencia: Android usa navegación compacta y objetivos táctiles; Windows usa navegación lateral y mayor espacio para la agenda.

## DEC-0018. Naturaleza de los bocetos

- Fecha: 2026-10-18
- Estado: aceptada para E1-T02
- Decisión: los archivos visuales son prototipos estáticos de revisión, no una aplicación web ni evidencia de funciones implementadas.
- Motivo: validar diseño antes de programar la aplicación C#/.NET MAUI.
- Consecuencia: no contienen lógica de negocio, persistencia, servidor, sincronización ni promesas de funciones futuras.

## DEC-0019. Versiones objetivo de la base ejecutable

- Fecha: 2026-10-19
- Estado: combinación candidata; requiere compilación mínima en E2-T02
- Decisión: usar .NET 10 LTS y .NET MAUI 10 para Android y Windows. Preparar el SDK .NET `10.0.113` de la banda 10.0.1xx, que incluye el runtime de seguridad `10.0.13`, y el servicio estable .NET MAUI `10.0.110`. La instalación actual `10.0.102`/MAUI `10.0.20` se aprovecha como base, pero no se fija porque está por detrás de las correcciones de seguridad y mantenimiento vigentes.
- Android para compilar: `net10.0-android`, Android SDK Platform 36, Build-Tools `36.0.0` y Microsoft OpenJDK 21 LTS. El paquete Android exacto lo resolverá el conjunto estable de cargas de trabajo de .NET 10; no se fijará manualmente fuera de ese conjunto.
- Android para el cliente: mínimo Android 7.0, API 24. Aunque .NET 10 todavía admite API 21, la documentación recomienda API 24 para evitar fallos de ejecución relacionados con desugaring.
- Windows para compilar: `net10.0-windows10.0.19041.0`, Windows SDK estable de la familia `10.0.26100.x` o posterior compatible y Windows App SDK transitivo de la versión MAUI elegida, sin sobrescribirlo manualmente al crear la solución.
- Windows para el cliente: mínimo Windows 10 versión 1809, compilación `10.0.17763.0`; las API posteriores deberán protegerse con comprobaciones de versión.
- Justificación: .NET 10 es LTS activo hasta el 2028-11-14; API 36 es estable y es la plataforma de compilación predeterminada de .NET 10 para Android; Windows App SDK conserva compatibilidad hasta Windows 10 1809.
- Consecuencia: E2-T02 debe crear una solución mínima, restaurar y compilar por separado Android y Windows antes de convertir esta combinación candidata en confirmada.

## DEC-0020. Preparación del entorno para E2-T02

- Fecha: 2026-10-19
- Estado: diagnóstico completado; preparación pendiente y no ejecutada
- Herramientas reutilizables: Windows x64 versión mostrada como 25H2, compilación 26200; .NET SDK `10.0.102`; carga `maui` con MAUI `10.0.20` y .NET for Android `36.1.43`; Visual Studio Code `1.140.0`; Android Studio 2026.1; Android SDK Platform 35, 36 y 37.0; Build-Tools `36.0.0`; Platform-Tools `37.0.1`; emulador `37.1.11`; aproximadamente 380 GB libres.
- Componentes faltantes o no configurados: Microsoft OpenJDK 21; `JAVA_HOME`; `ANDROID_HOME`; Windows SDK/Windows Kits; extensiones oficiales C#, C# Dev Kit y .NET MAUI para Visual Studio Code; dispositivo Android o AVD; confirmación de Modo de desarrollador. No hay una instalación completa de Visual Studio, solo SSMS y el instalador.
- Incompatibilidad observada: Android Studio incluye JDK 25, pero no se elige porque .NET 10 documenta JDK 21. `sdkmanager` 12 puede enumerar los paquetes, aunque avisa que solo entiende XML hasta versión 3 frente a repositorios con XML 4.
- Preparación exacta autorizable antes de E2-T02: instalar SDK .NET `10.0.113` x64 y actualizar la carga estable MAUI 10 dentro de la banda 10.0.1xx; instalar Microsoft OpenJDK 21 x64; definir `JAVA_HOME` a su carpeta real; definir `ANDROID_HOME` como `C:\Users\adria\AppData\Local\Android\Sdk`; agregar `%ANDROID_HOME%\platform-tools` y `%ANDROID_HOME%\emulator` a `PATH`; instalar Windows SDK estable `10.0.26100.x`; instalar las extensiones oficiales de C#, C# Dev Kit y .NET MAUI en VS Code; comprobar o habilitar Modo de desarrollador antes de desplegar Windows.
- Alternativa de IDE: si se prefiere Visual Studio, .NET 10 requiere Visual Studio 2026 versión 18.0 o posterior con la carga de desarrollo .NET MAUI. No es necesario instalarlo para probar primero la ruta ya disponible de CLI y VS Code.
- Elementos no bloqueantes para compilar: no hay AVD ni teléfono conectado. Se necesitará uno para ejecutar Android más adelante, no para la primera compilación. La directiva de rutas largas está desactivada y se vigilará en E2-T02 antes de cambiarla.
- Consecuencia: no se instaló ni configuró nada en E2-T01. Los pasos se ejecutarán únicamente con una autorización posterior; E2-T02 registrará versiones efectivas y resultados reales.

## Referencias oficiales consultadas

- Política y descargas de .NET 10: https://dotnet.microsoft.com/platform/support/policy y https://dotnet.microsoft.com/download/dotnet/10.0
- Plataformas compatibles: https://learn.microsoft.com/dotnet/maui/supported-platforms?view=net-maui-10.0
- Novedades de .NET MAUI 10, API 36, JDK 21 y mínimo recomendado: https://learn.microsoft.com/dotnet/maui/whats-new/dotnet-10?view=net-maui-10.0
- Instalación y configuración: https://learn.microsoft.com/dotnet/maui/get-started/installation y https://learn.microsoft.com/dotnet/android/getting-started/installation/dependencies
- Versiones estables de .NET MAUI: https://github.com/dotnet/maui/releases
- Android SDK 36 estable: https://developer.android.com/tools/releases/platforms y https://developer.android.com/about/versions/16/setup-sdk
- Compatibilidad entre .NET 10 y Visual Studio: https://learn.microsoft.com/dotnet/core/install/windows
- Windows SDK y relación entre SDK, Windows App SDK y sistema cliente: https://learn.microsoft.com/windows/apps/windows-sdk/ y https://learn.microsoft.com/windows/apps/get-started/versioning-overview
- Modo de desarrollador: https://learn.microsoft.com/windows/apps/get-started/developer-mode-features-and-debugging
- SQLite local: https://learn.microsoft.com/dotnet/maui/data-cloud/database-sqlite?view=net-maui-10.0
- SignalR: https://learn.microsoft.com/aspnet/core/signalr/introduction?view=aspnetcore-10.0

Estas referencias respaldan las decisiones y distinguen herramientas de compilación de mínimos de cliente. La compatibilidad real de la combinación candidata todavía no está probada en este equipo.
