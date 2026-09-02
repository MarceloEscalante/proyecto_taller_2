# Criterios de aceptación - Iteración 1

## Objetivo de la iteración

Construir la base visual y arquitectónica de la aplicación de escritorio Mod Parts sobre Avalonia UI. La entrega debe permitir recorrer una experiencia coherente de login, shell, dashboards y módulos principales, dejando preparados los puntos de extensión para incorporar posteriormente los RF-01 a RF-08 sin refactorizaciones mayores.

Esta iteración no implementa reglas de negocio, persistencia ni autenticación real. Todo contenido operativo debe presentarse como placeholder, estado vacío o dato de diseño; no se deben mostrar datos reales ni simulaciones complejas.

## Alcance

- Capa de Presentación y su organización por vistas, viewmodels, componentes, layouts, navegación, recursos, estilos y assets.
- Shell principal con header, sidebar y área de contenido.
- Navegación visual local entre las pantallas incluidas.
- Vista de login completamente diseñada, sin conexión ni validación contra usuarios.
- Dashboards visuales diferenciados para Administrador, Vendedor y Depósito.
- Vistas placeholder para Productos, Categorías, Clientes, Ventas, Inventario, Usuarios y Reportes.
- Sistema de diseño visual común para controles y estados de la aplicación.
- Preparación visual para que los permisos dependan del rol en una iteración futura.

## Fuera de alcance

- Autenticación, autorización y manejo real de sesión.
- Validación real de email, contraseña, credenciales, permisos o estado de usuario.
- Conexión con Supabase, PostgreSQL, Entity Framework Core, SDK externo o cualquier API.
- Servicios, repositorios, modelos persistentes, lógica de negocio y reglas RN-01 a RN-07.
- ABM de productos, categorías, clientes o usuarios.
- Registro de ventas, descuentos, apertura de caja o actualización de stock.
- Consultas, filtros y exportación de reportes.
- Datos reales, seed de base de datos o mock data compleja.
- Facturación electrónica, pagos, compras online, envíos, modo offline transaccional y múltiples cajas.
- Implementación de las restricciones de seguridad de datos del ERS, como hashing de contraseñas o SSL/TLS; quedan para la capa correspondiente.

## Pantallas incluidas

| Pantalla | Propósito en esta iteración | Acceso visual esperado |
| --- | --- | --- |
| Login | Presentar el punto de entrada de la aplicación | Ruta inicial |
| Dashboard Administrador | Mostrar la estructura del panel global | Rol Administrador futuro |
| Dashboard Vendedor | Mostrar la estructura del panel de ventas propias | Rol Vendedor futuro |
| Dashboard Depósito | Mostrar la estructura del panel de inventario | Rol Depósito futuro |
| Productos | Reservar el área del catálogo | Administrador |
| Categorías | Reservar el área de clasificación del catálogo | Administrador, como submódulo de Productos |
| Clientes | Reservar el área de gestión de clientes | Administrador y Vendedor |
| Ventas | Reservar el área de ventas presenciales | Administrador y Vendedor |
| Inventario | Reservar el área de movimientos y control de stock | Administrador y Depósito |
| Usuarios | Reservar el área de administración de usuarios | Administrador |
| Reportes | Reservar el área de consultas con grilla | Según alcance del rol |

La selección de un rol durante esta iteración, si se ofrece para recorrer las variantes, debe ser una ayuda de demostración visual y no una autenticación. No se deben guardar credenciales ni inferir permisos reales.

## Componentes incluidos

- Shell de aplicación.
- Header con identificación del sistema, contexto de la vista y espacio para acciones futuras.
- Sidebar con navegación agrupada por módulos.
- Breadcrumb reutilizable.
- Título de página y encabezado de sección.
- Tarjeta KPI placeholder.
- Empty State reutilizable con mensaje y espacio para una acción futura, cuando corresponda.
- Campos de texto, campo de contraseña, selector visual y estados de error de formulario.
- Botones primario, secundario y de acción contextual.
- Contenedor reservado para DataGrid.
- Contenedor de modal o diálogo futuro, sin operaciones persistentes.
- Recursos de colores, tipografía, espaciados, iconos y estilos compartidos.

## Criterios de aceptación funcionales

### CA-F01 - Inicio y shell

- Dado que se inicia la aplicación, cuando termina la carga inicial, entonces se muestra una ventana de escritorio con la vista de Login o la ruta inicial definida, sin excepción visible ni pantalla sin diseñar.
- Dado que se accede a una pantalla interna, cuando la vista se renderiza, entonces conserva el mismo shell compuesto por header, sidebar y área de contenido.
- Dado que se cambia de módulo desde el sidebar, cuando se selecciona una opción habilitada, entonces se muestra la vista correspondiente sin cerrar la aplicación ni perder el layout común.
- Dado que una opción todavía no tiene comportamiento de negocio, cuando se accede a ella, entonces se muestra su placeholder diseñado y no un error, una vista en blanco o una operación simulada.

### CA-F02 - Navegación y crecimiento futuro

- La navegación debe identificar rutas o destinos independientes para Login, los tres dashboards y cada módulo incluido.
- El estado de navegación debe permitir agregar nuevos destinos sin duplicar el shell ni acoplar cada vista a una implementación de base de datos.
- El shell debe admitir que el conjunto de opciones del sidebar se determine posteriormente por rol. En esta iteración basta con definir y visualizar los tres conjuntos de menú.
- La opción activa del sidebar y el breadcrumb deben reflejar la vista actualmente mostrada.
- La navegación visual no debe afirmar que un usuario está autenticado ni debe conceder permisos reales.

### CA-F03 - Login visual

- La vista contiene, como mínimo, los campos Email y Contraseña, el botón Ingresar y una jerarquía visual clara.
- El campo Contraseña presenta el contenido enmascarado a nivel visual.
- El botón Ingresar tiene estado normal, hover, presionado y deshabilitado o no disponible cuando la interfaz lo requiera.
- La vista incluye un estado de error visible y consistente para credenciales inválidas, sin revelar qué dato habría fallado. El estado es únicamente demostrativo y no se dispara contra un backend.
- La vista incluye estados visuales para campos vacíos o incompletos, sin ejecutar validación real ni persistir información.
- La composición del login es utilizable con teclado y mouse, mantiene contraste suficiente y no recorta controles en una ventana de escritorio razonable.

### CA-F04 - Dashboards por rol

- Existe una composición diferenciada para Administrador, Vendedor y Depósito, cada una con título identificable.
- Cada dashboard contiene tarjetas KPI placeholder, un área de contenido principal y al menos un estado vacío consistente.
- El Dashboard Administrador reserva visualmente indicadores para ventas totales del día y stock bajo global.
- El Dashboard Vendedor reserva visualmente un indicador para ventas propias del día.
- El Dashboard Depósito reserva visualmente indicadores o alertas para stock bajo y control de inventario.
- Ningún dashboard muestra números, nombres de clientes, productos, ventas o alertas provenientes de datos reales o mock complejos.
- El contenido se reorganiza sin solaparse cuando cambia el tamaño de la ventana dentro de los tamaños de escritorio soportados.

### CA-F05 - Sidebar por rol

Los menús deben quedar definidos visualmente de la siguiente manera:

| Rol | Opciones |
| --- | --- |
| Administrador | Dashboard, Productos, Clientes, Ventas, Inventario, Usuarios, Reportes |
| Vendedor | Dashboard, Clientes, Ventas, Reportes |
| Depósito | Dashboard, Inventario |

- Cada opción visible tiene etiqueta legible, estado activo y destino visual identificable.
- Categorías debe estar accesible como submódulo visual de Productos, aunque no aparezca como opción de primer nivel en la tabla del ERS.
- Los menús de roles no deben mezclarse: una variante no debe mostrar opciones exclusivas de otra.
- La restricción de menú es visual en esta iteración; el bloqueo de acceso real queda fuera de alcance.

### CA-F06 - Pantallas placeholder

Para Productos, Categorías, Clientes, Ventas, Inventario, Usuarios y Reportes:

- La ruta existe y se puede abrir desde el destino visual correspondiente.
- La pantalla muestra un título específico del módulo.
- La pantalla muestra un breadcrumb coherente con el shell y, para Categorías, su relación con Productos.
- Existe un área reservada para el contenido futuro, con dimensiones y jerarquía que no parezcan una vista rota.
- Existe un Empty State común, con mensaje de ausencia de contenido y sin datos inventados.
- El placeholder no muestra formularios operativos, grillas con registros ficticios ni botones que aparenten guardar, eliminar o consultar información real.
- Reportes reserva explícitamente el espacio para una futura DataGrid y filtros de fecha, pero no ejecuta consultas.

### CA-F07 - Preparación arquitectónica de presentación

- La estructura de presentación separa conceptualmente `Views`, `ViewModels`, `Components`, `Layouts`, `Navigation`, `Resources`, `Styles` y `Assets`.
- Las vistas de módulo dependen de componentes y layouts compartidos cuando el comportamiento visual es común.
- La navegación y el shell no contienen acceso a datos ni reglas de negocio.
- La estructura permite incorporar posteriormente servicios de la capa de Lógica de Negocio y adaptadores de Acceso a Datos sin mover la responsabilidad visual de las vistas.
- La preparación de roles se representa mediante destinos y menú configurables, sin implementar autorización.

## Criterios de aceptación visuales

### Sistema de diseño

| Elemento | Criterio |
| --- | --- |
| Paleta | Fondo principal neutro claro, superficies diferenciadas, texto de alto contraste y un color de acción sobrio. Los colores de éxito, advertencia y error se reservan para estados semánticos. Evitar una paleta dominada por un único tono. |
| Tipografía | Familia sans serif legible y consistente, con jerarquías explícitas para título, subtítulo, etiqueta, cuerpo y ayuda. El tamaño debe ser apropiado para una aplicación de escritorio, no para una landing page. |
| Espaciado | Escala coherente basada en múltiplos regulares; los contenidos no deben quedar pegados a los bordes ni generar espacios arbitrarios entre controles. |
| Bordes | Radios discretos y consistentes, con separadores y contornos suficientes para distinguir superficies sin recargar la interfaz. |
| Inputs | Etiqueta visible, estado de foco, placeholder cuando aporte contexto, estado de error y tamaño estable. El texto debe caber sin recortes. |
| Botones | Jerarquía clara entre acción primaria y secundaria, estados interactivos visibles, texto legible e iconos solo cuando aporten reconocimiento. |
| DataGrid | Contenedor preparado para encabezados, filas, estado vacío y desplazamiento; sin registros ficticios ni columnas arbitrarias que aparenten datos reales. |
| Cards KPI | Tamaño estable, título breve, espacio para un valor futuro, indicador de tendencia opcional como placeholder y estado vacío reconocible. |
| Modales | Superficie diferenciada, título, área de contenido, cierre visible y acciones separadas. No deben ejecutar operaciones en esta iteración. |

### Consistencia y usabilidad

- Todas las vistas usan el mismo lenguaje visual para títulos, breadcrumbs, espaciado, Empty State y acciones.
- Los estados normal, hover, foco, deshabilitado, error y vacío son distinguibles sin depender únicamente del color.
- No existen textos, controles o iconos superpuestos, cortados o ilegibles en tamaños de escritorio razonables.
- La interfaz es navegable mediante teclado y mouse en los controles principales.
- Los módulos se perciben como software empresarial: sobrio, claro, operativo y minimalista, sin adornos que compitan con la información.
- Las etiquetas y mensajes de la interfaz son consistentes con el dominio del ERS y no prometen funcionalidades que todavía no existen.

## Trazabilidad con el ERS

| Elemento de esta iteración | Requisitos del ERS preparados |
| --- | --- |
| Login y estados de error | RF-01 |
| Dashboards y estados vacíos | RF-02 |
| Productos y Categorías | RF-03 |
| Ventas | RF-04 |
| Inventario | RF-05 |
| Clientes | RF-06 |
| Usuarios y roles futuros | RF-07 |
| Reportes y DataGrid reservada | RF-08 |
| Shell, estilos, navegación y estructura por capas | Secciones 3.1, 3.2 y requisitos no funcionales de mantenibilidad y portabilidad |

La trazabilidad indica preparación visual y arquitectónica, no implementación del requisito funcional completo.

## Riesgos

| Riesgo | Impacto | Mitigación en esta iteración |
| --- | --- | --- |
| Confundir una navegación demostrativa con autorización real | Alto | Etiquetar la variante de rol como demostración y dejar la autorización para RF-01. |
| Diseñar placeholders que condicionen la lógica futura | Medio | Definir componentes reutilizables y áreas de contenido sin contratos de datos concretos. |
| Inconsistencia entre módulos | Medio | Centralizar recursos, estilos, shell, breadcrumbs y Empty State. |
| Ambigüedad sobre Categorías | Medio | Tratarla como submódulo visual de Productos y mantener una ruta propia. |
| Datos inventados interpretados como comportamiento implementado | Alto | Usar placeholders y estados vacíos; no mostrar registros ni KPIs con valores ficticios. |
| Layout inutilizable en escritorios pequeños | Medio | Probar redimensionamiento y evitar posiciones absolutas que produzcan solapamientos. |
| Refactorización al incorporar capas posteriores | Medio | Mantener navegación y presentación independientes de servicios, repositorios y modelos persistentes. |

## Checklist final de validación

### Ejecución y navegación

- [ ] La aplicación inicia correctamente sin excepción visible.
- [ ] La vista inicial es el Login diseñado.
- [ ] Se puede recorrer el shell y volver al Dashboard desde el sidebar.
- [ ] Cada destino del menú abre su placeholder correspondiente.
- [ ] La variante visual de cada rol muestra únicamente el menú definido para ese rol.
- [ ] La opción activa y el breadcrumb se actualizan al cambiar de vista.

### Login

- [ ] Están presentes Email, Contraseña e Ingresar.
- [ ] La contraseña se muestra enmascarada.
- [ ] Se pueden visualizar estados de error y campos incompletos.
- [ ] No se realiza autenticación, persistencia ni llamada de red.

### Dashboards y módulos

- [ ] Existen los tres dashboards por rol.
- [ ] Cada dashboard tiene título, KPI placeholder y Empty State.
- [ ] Existen placeholders para Productos, Categorías, Clientes, Ventas, Inventario, Usuarios y Reportes.
- [ ] Cada placeholder tiene título, breadcrumb y área reservada.
- [ ] Reportes reserva una DataGrid y filtros futuros sin consultar datos.
- [ ] No se muestran datos reales ni mock data compleja.

### Diseño y arquitectura

- [ ] El shell, controles y estados usan estilos compartidos.
- [ ] La paleta, tipografía, espaciado, bordes e inputs son consistentes.
- [ ] Los estados visuales principales son distinguibles y legibles.
- [ ] No hay vistas en blanco, controles cortados ni elementos superpuestos.
- [ ] La organización de presentación separa Views, ViewModels, Components, Layouts, Navigation, Resources, Styles y Assets.
- [ ] No se agregaron servicios, repositorios, conexión a base de datos, lógica de negocio ni autenticación real.

## Condición de aprobación

La Iteración 1 se aprueba cuando todos los ítems del checklist son verificables en la aplicación y no quedan pantallas sin diseño dentro del alcance. La aprobación confirma la base visual y arquitectónica; no implica que RF-01 a RF-08 estén implementados.