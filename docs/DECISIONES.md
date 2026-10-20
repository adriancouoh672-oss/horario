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
- Pendiente: comprobar ejecución real de cada plataforma en una tarea posterior; E2-T02 confirmó la compilación de ambas.

## DEC-0004. Tecnología candidata

- Fecha: 2026-10-18
- Estado: aceptada como base tecnológica; versiones de DEC-0019 confirmadas por compilación mínima en E2-T02
- Decisión: C#/.NET MAUI con XAML, SQLite local y, en una entrega posterior, ASP.NET Core y SignalR.
- Motivo: permite compartir modelos y reglas entre Android y Windows, conservar integraciones específicas por plataforma y seguir la arquitectura de la guía.
- Pendiente: comprobar ejecución y comportamiento en cada plataforma en tareas posteriores; la compilación mínima quedó confirmada en E2-T02.

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
- Estado: combinación confirmada para compilación en E2-T02; ejecución pendiente
- Decisión: usar .NET 10 LTS y .NET MAUI 10 para Android y Windows. Preparar el SDK .NET `10.0.113` de la banda 10.0.1xx, que incluye el runtime de seguridad `10.0.13`, y el servicio estable .NET MAUI `10.0.110`. La instalación actual `10.0.102`/MAUI `10.0.20` se aprovecha como base, pero no se fija porque está por detrás de las correcciones de seguridad y mantenimiento vigentes.
- Android para compilar: `net10.0-android`, Android SDK Platform 36, Build-Tools `36.0.0` y Microsoft OpenJDK 21 LTS. El paquete Android exacto lo resolverá el conjunto estable de cargas de trabajo de .NET 10; no se fijará manualmente fuera de ese conjunto.
- Android para el cliente: mínimo Android 7.0, API 24. Aunque .NET 10 todavía admite API 21, la documentación recomienda API 24 para evitar fallos de ejecución relacionados con desugaring.
- Windows para compilar: `net10.0-windows10.0.19041.0`, `Microsoft.Windows.SDK.BuildTools 10.0.26100.8249` y `Microsoft.WindowsAppSDK 1.8.260529003`, resueltos transitivamente por MAUI sin sobrescribirlos manualmente.
- Windows para el cliente: mínimo Windows 10 versión 1809, compilación `10.0.17763.0`; las API posteriores deberán protegerse con comprobaciones de versión.
- Justificación: .NET 10 es LTS activo hasta el 2028-11-14; API 36 es estable y es la plataforma de compilación predeterminada de .NET 10 para Android; Windows App SDK conserva compatibilidad hasta Windows 10 1809.
- Evidencia: .NET SDK `10.0.113`, runtime `10.0.13`, manifiesto MAUI `10.0.110`, .NET for Android `36.1.118`, Microsoft OpenJDK `21.0.12.1`, Android Platform 36 y Build-Tools `36.0.0`. Core, Android y Windows compilaron por separado con cero errores y cero advertencias.
- Consecuencia: la combinación queda confirmada para compilación. Esto no demuestra ejecución en Android o Windows ni validación en teléfono real.

## DEC-0020. Preparación del entorno para E2-T02

- Fecha: 2026-10-19
- Estado: preparación de CLI completada en E2-T02; herramientas opcionales de ejecución pendientes
- Herramientas reutilizables: Windows x64 versión mostrada como 25H2, compilación 26200; .NET SDK `10.0.102`; carga `maui` con MAUI `10.0.20` y .NET for Android `36.1.43`; Visual Studio Code `1.140.0`; Android Studio 2026.1; Android SDK Platform 35, 36 y 37.0; Build-Tools `36.0.0`; Platform-Tools `37.0.1`; emulador `37.1.11`; aproximadamente 380 GB libres.
- Componentes preparados: SDK .NET `10.0.113` en `%USERPROFILE%\.dotnet`; carga MAUI `10.0.110`; Microsoft OpenJDK `21.0.12.1` en `%USERPROFILE%\.jdks\jdk-21.0.12.1+1`; variables de usuario `DOTNET_ROOT`, `JAVA_HOME`, `ANDROID_HOME` y `ANDROID_SDK_ROOT`; entradas no duplicadas de .NET, Java, `platform-tools` y `emulator` en el `PATH` de usuario.
- Componentes reutilizados: Android SDK existente en `C:\Users\adria\AppData\Local\Android\Sdk`, Platform 36, Build-Tools `36.0.0` y Platform-Tools `37.0.1`.
- Herramientas Windows efectivas: la restauración obtuvo `Microsoft.Windows.SDK.BuildTools 10.0.26100.8249` y `Microsoft.WindowsAppSDK 1.8.260529003`; no fue necesario instalar Windows Kits ni Visual Studio para la compilación CLI.
- Incompatibilidad observada: Android Studio incluye JDK 25, pero no se elige porque .NET 10 documenta JDK 21. `sdkmanager` 12 puede enumerar los paquetes, aunque avisa que solo entiende XML hasta versión 3 frente a repositorios con XML 4.
- Método de instalación: el intento de instalación de .NET por `winget` requirió elevación y no pudo completarse de forma desatendida. Se usó el instalador oficial `dotnet-install.ps1` en el perfil del usuario. Microsoft OpenJDK se instaló desde el ZIP oficial tras comprobar su SHA-256 (`192441A9D27DA813BADA974BB88B4CF64D37A9589ED37F204374D411CA5CE07F`).
- Alternativa de IDE: si se prefiere Visual Studio, .NET 10 requiere Visual Studio 2026 versión 18.0 o posterior con la carga de desarrollo .NET MAUI. No es necesario instalarlo para probar primero la ruta ya disponible de CLI y VS Code.
- Elementos no bloqueantes para compilar y todavía pendientes: extensiones oficiales de C# y MAUI para VS Code, AVD o teléfono conectado, y confirmación de Modo de desarrollador. Solo se necesitan para mejorar la edición o ejecutar/desplegar en tareas posteriores. La directiva de rutas largas permanece sin cambios porque las compilaciones no la requirieron.
- Consecuencia: el entorno CLI queda preparado y reproducible. No se reemplazó el `PATH`, no se duplicaron entradas y se conservaron las instalaciones previas.

## DEC-0021. Estructura mínima de la solución

- Fecha: 2026-10-19
- Estado: aceptada para la base ejecutable de E2-T02
- Decisión: la solución contiene únicamente `TiempoEnOrden.App`, aplicación MAUI con XAML dirigida a Android y Windows, y `TiempoEnOrden.Core`, biblioteca `net10.0` independiente de MAUI. App referencia Core; Core no referencia App.
- Motivo: establecer una frontera verificable entre interfaz y lógica compartida sin crear capas, API o infraestructura antes de necesitarlas.
- Alcance actual: la única pantalla muestra el nombre del producto y la leyenda `Base de desarrollo`. `ProductInfo` contiene solo la identidad compartida del producto; no implementa reglas de negocio.
- Consecuencia: persistencia, disponibilidad, conflictos, navegación completa, servidor, sincronización y recordatorios continúan fuera de E2-T02.

## DEC-0022. Excepción de prioridad por entrega del 20 de octubre

- Fecha: 2026-10-19
- Estado: autorizada por el responsable
- Decisión: avanzar con la versión local funcional aunque la revisión manual de Windows y la validación en teléfono Android real de E2-T04 continúen pendientes.
- Motivo: priorizar una entrega local comprobable para el 20 de octubre.
- Condiciones: los pendientes de plataforma permanecen visibles y no se consideran aprobados; Android se comprueba en el AVD existente; Windows no recibe nuevos intentos repetidos de automatización y se entrega un recorrido manual.
- Secuencia autorizada: persistencia SQLite; clientes y servicios; citas; tiempo personal y configuración laboral; cruces; sugerencias; respaldo/restauración; paquete y documentación.
- Límites: no incluir nube, cuentas, pagos ni recordatorios coordinados; no presentar simulaciones como funciones reales.

## DEC-0023. Persistencia SQLite local

- Fecha: 2026-10-19
- Estado: aceptada para la versión local
- Decisión: crear `TiempoEnOrden.Infrastructure`, dependiente de Core y no de MAUI, y usar `sqlite-net-pcl` estable `1.11.285`. La aplicación suministra la ruta de datos de cada plataforma e Infrastructure implementa repositorios, inicialización y respaldo.
- Motivo: mantener SQLite separada de la presentación y de los datos ficticios, conservar Core independiente y seguir la guía vigente de .NET MAUI para bases locales.
- Consecuencia: App referencia Infrastructure; Infrastructure referencia Core; Core no referencia ninguna de las dos. Los datos persistentes usan identificadores estables y las citas copian los valores históricos del servicio.
- Fuente oficial: https://learn.microsoft.com/dotnet/maui/data-cloud/database-sqlite?view=net-maui-10.0
- Paquete estable comprobado: https://www.nuget.org/packages/sqlite-net-pcl/1.11.285

## DEC-0024. Regla única de disponibilidad local

- Fecha: 2026-10-19
- Estado: aceptada para la versión local
- Decisión: representar cada cita mediante un intervalo ocupado desde el inicio de preparación hasta el fin de limpieza y considerar cruce solo cuando dos intervalos se superponen; los extremos que únicamente se tocan son compatibles.
- La misma comprobación valida jornada laboral, descanso, tiempo personal y citas activas. Una cita cancelada no ocupa disponibilidad y, al editar, se excluye el identificador de la propia cita.
- Motivo: hacer comprobables REQ-A-006, REQ-A-007 y REQ-A-008 y evitar diferencias entre guardado y sugerencias.
- Consecuencia: el guardado rechaza un cruce y no mueve ningún compromiso. Las sugerencias del bloque siguiente deben invocar este mismo servicio, no duplicar sus reglas.

## DEC-0025. Respaldo local controlado por la persona

- Fecha: 2026-10-19
- Estado: aceptada para la versión local
- Decisión: crear copias de SQLite dentro del almacenamiento privado de la aplicación y permitir restaurar únicamente el respaldo local más reciente, tras confirmación explícita.
- Motivo: ofrecer recuperación verificable para la entrega sin introducir nube, cuentas, permisos de almacenamiento compartido o sincronización.
- Consecuencia: la restauración reemplaza los datos locales actuales y recarga Ajustes. No es una exportación transferible entre dispositivos ni una copia de seguridad en la nube.

## DEC-0026. Límite de citas de negocio del paquete `afac4de`

- Fecha: 2026-10-19
- Estado: limitación documentada para cierre de entrega; no reemplaza el requisito de producto pendiente.
- Decisión: presentar el paquete Release como compatible únicamente con citas de negocio cuya ocupación completa cabe en un día laboral. No se admiten citas nocturnas que crucen medianoche.
- Motivo: `AvailabilityService` compara la ocupación contra una única jornada fechada y rechaza preparación o limpieza que salgan de esa fecha.
- Consecuencia: REQ-A-001 conserva la expectativa de inicio/fin con fecha, pero la capacidad de cruzar medianoche queda explícitamente pendiente y no se demuestra.

## DEC-0027. Dependencias de la publicación Windows sin empaquetar

- Fecha: 2026-10-19
- Estado: comprobada mediante configuración y contenido de publicación; instalación en otro equipo pendiente.
- Decisión: la carpeta Windows se distribuye completa como publicación x64 framework-dependent. Además de .NET Desktop Runtime 10 x64, requiere Windows App Runtime 1.8 estable x64 compatible con `Microsoft.WindowsAppSDK 1.8.260529003` y Visual C++ Redistributable x64.
- Evidencia: `WindowsPackageType=None`, `TiempoEnOrden.App.runtimeconfig.json`, `TiempoEnOrden.App.deps.json` y los binarios bootstrapper incluidos.
- Consecuencia: no afirmar que basta instalar .NET Desktop ni que la carpeta es autosuficiente. La verificación de dependencias e instalación es parte del recorrido manual Release.

## Referencias oficiales consultadas

- Política y descargas de .NET 10: https://dotnet.microsoft.com/platform/support/policy y https://dotnet.microsoft.com/download/dotnet/10.0
- Instalación oficial por script de .NET: https://learn.microsoft.com/dotnet/core/tools/dotnet-install-script
- Descarga oficial de Microsoft Build of OpenJDK 21: https://learn.microsoft.com/java/openjdk/download
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

Estas referencias respaldan las decisiones y distinguen herramientas de compilación de mínimos de cliente. E2-T02 confirmó la combinación mediante compilaciones separadas de Core, Android y Windows; la ejecución en cada plataforma continúa pendiente.
