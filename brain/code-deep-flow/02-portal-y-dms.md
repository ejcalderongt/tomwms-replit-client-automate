# 02 — Portal CEALSA, DMS, generador de código y la trinidad DALCore/EntityCore/AppGlobalCore

**Tipo de doc**: Arquitectura macro (no traza de invariante)  
**Autor de la historia**: Erik Calderón (2026-04-28, Wave 6.1)  
**Validaciones de campo**: SQL CEALSA QAS + grep en `dev_2028_merge` BOF

> **Disclaimer ramas**: cuando se cita código, refiere a `dev_2028_merge` salvo aclaración. Para 4 de 5 clientes en producción (BECO/K7/BYB/CEALSA), la rama corriendo es `dev_2023_estable` — la trinidad `WMS.DALCore + WMS.EntityCore + WMS.AppGlobalCore` **NO existe en 2023** y por lo tanto no aplica a producción real (excepto MAMPA, que está en pruebas integrales 2028).

---

## 1. El origen del problema — portal CEALSA

### 1.1 Caso de uso

CEALSA es **3PL** (third-party logistics) en Guatemala. Como almacén fiscal, está regulado por:
- **SAT** (Superintendencia de Administración Tributaria)
- **SIB** (probable, Erik no confirmó)
- **Aseguradora** del depósito

De ahí surge la funcionalidad **`stock_jornada`** del WMS: corte/snapshot diario de cuánta, cuál y qué mercancía estuvo en qué bodegas. Es el invariante regulatorio.

Después surge una segunda necesidad — **comercial, no regulatoria**:
- Los **propietarios** de mercancía (clientes de CEALSA) quieren ver **su inventario en tiempo cuasi-real** (entre cortes diarios).
- La **gerencia / dirección logística** también quiere visibilidad para aprovechar la inteligencia del WMS.

### 1.2 Restricción arquitectónica madre

> *"WMS se ejecuta en un servidor on-premise dado que generalmente no se necesita exponer un servidor de WMS en capa 3."*

El WMS vive **on-prem**. El portal necesita estar **en internet**. ¿Cómo conectar los dos sin romper la seguridad ni los costos?

---

## 2. Restricciones de diseño

| Restricción | Implicación |
|---|---|
| BD on-prem (capa 3) | No exponer directo a internet |
| Tamaño BD: 10+ GB en CEALSA PRD (1.6 GB QAS validado por SQL) | Replicar todo a cloud → costo prohibitivo |
| Solo ~10 propietarios "pagantes" del servicio portal | Filtrar por propietario reduce 80x el dato |
| Multi-cliente futuro (no solo CEALSA) | Diseñar para múltiples WMS-source en un cloud-target |
| Mantenimiento limitado (recurso monetario, tiempo, factor humano) | Aplicar **ley del mínimo esfuerzo** |
| Erik: aversión a herramientas que pierden control de debug | Descartar SQL Replication, ORM modernos, etc. |

---

## 3. Decisión arquitectónica madre — "como es arriba es abajo"

> *"En el principio, no había nada... como hacíamos para exponer la data discriminada hacia un portal que necesita estar publicado en internet."*

### 3.1 Las dos opciones consideradas

**Opción A — BD raw en cloud sin estructura WMS**:
- Solo tablas planas con datos consultables
- Ventaja: ahorra dependencias estructurales
- Desventaja: si después se quiere historial de movimientos, detalle de ingreso, etc. → reconstruir todo

**Opción B — Replicar la transacción manteniendo estructura WMS idéntica** ← **ELEGIDA**:
- "Como es afuera es adentro, como es arriba es abajo, como era on-premise era on-the-cloud"
- Ventaja: integridad estructural, facilidad de evolución, no pierde nada
- Desventaja: requiere replicar la cadena completa de relaciones por transacción

### 3.2 Lo que implica la Opción B

> *"Si hoy se recibía un nuevo propietario, se creaba el propietario, acuerdos comerciales (relativos al ERP del cliente), se importan a WMS vía cealsasync, se asociaba el proveedor a la bodega."*

Cada transacción debe replicarse con **toda su cadena de FKs**:
```
Propietario → AcuerdosComerciales → CEALSAsync → Producto/Bodega/Asociaciones → 
Estado-por-propietario → Recepción/Pedido/Picking/Despacho → stock + stock_jornada
```

El cloud-target debe poder insertar todo esto en orden, fila por fila.

---

## 4. DMS — Data Management System (Efren)

### 4.1 Ubicación en el repo (validada)

```
TOMWMS_BOF/DMS/
├─ Api/
│  ├─ ApiConfig.vb
│  └─ ApiService.vb
├─ Clases/
│  ├─ BaseDatos.vb
│  ├─ IMS.vb
│  ├─ clsBD.vb
│  ├─ clsHelper.vb
│  ├─ clsTabla.vb
│  └─ m_Global.vb
└─ App.config
```

Es proyecto VB.NET separado dentro del solution `TOMWMS_BOF`.

### 4.2 Diseño según Erik

> *"Crear un EXE que corre en el server-side de WMS. WMS por arquitectura tiene que tener un license-server, eso está definido en la base de datos y se valida contra una variable al inicio de sesión. Entonces en el server se instala y ejecuta el WMS de forma parametrizada (horario/días/frecuencia ejecución)."*

**Características**:
- EXE server-side (aprovecha la infra de license-server existente)
- Parametrizado por horario / días / frecuencia
- Replica data on-prem → cloud manteniendo estructura idéntica
- **NO usa bulk insert / `INSERT INTO ... SELECT` / inline batch**
- **SÍ usa inserción fila por fila controlada**

### 4.3 ¿Por qué no bulk insert?

> *"Cuando algo falla en la sincronización, por debugar la fila exacta en la que falla la sincronización, es un principio que siempre persigo, tener control total sobre los objetos y los datos, por eso evito utilizar un ORM."*

**Principio Erik**: control total sobre objetos y datos > performance del bulk insert.  
La fila por fila permite:
- Identificar exactamente cuál registro falló y por qué FK
- Reintentar solo el que falló
- Evitar transacciones grandes que rollback completo ante el primer error
- Logging discriminado por fila

### 4.4 Trade-off aceptado

- **Costo**: throughput menor (cada `INSERT` cuesta su round-trip)
- **Beneficio**: debuggability total + recuperación granular
- **Cuándo es razonable**: cargas no-masivas (10 propietarios × N transacciones/día), donde el debugging vale más que el throughput

---

## 5. Generador de código — el patrón BE / LN-base / LN-partial

### 5.1 La automatización más temprana

> *"Creé un generador de código para VB.NET y para C#, quizá esa fue de las primeras automatizaciones."*

**Mecánica**: el generador "se pega a la base de datos del programador" (introspección de schema) y genera:

```
Por cada tabla:
├─ clsBe<Tabla>.vb          ← Entity (BE = Business Entity), autogenerada 100%
│  └─ Una propiedad por columna
│
├─ clsLn<Tabla>.vb          ← Logic Name (CRUD base), autogenerada
│  ├─ Cargar()              ← SELECT por PK
│  ├─ Insert()              ← INSERT
│  ├─ Update()              ← UPDATE
│  ├─ Delete()              ← DELETE
│  └─ GetAll()              ← SELECT *
│
└─ clsLn<Tabla>.partial.vb  ← Partial class, manual
   └─ Métodos ad-hoc del negocio (queries específicas, lógica de dominio)
```

### 5.2 Por qué partial classes

VB.NET (y C#) permiten dividir una clase en múltiples archivos con `Partial Class`. El patrón explota eso:
- **Archivo autogenerado**: el regenerador puede sobrescribirlo cuando cambia el schema sin tocar código manual
- **Archivo partial**: el código manual nunca se pierde

### 5.3 Drift del patrón (Erik admite)

> *"Aunque en la práctica muchas clases base tienen métodos o funciones que se escriben en ellas."*

La disciplina del patrón se rompió: hay clases `Ln<Tabla>` que tienen métodos manuales en el archivo "autogenerado" en lugar de en el `.partial.vb`. Esto rompe la regenerabilidad — si se regenera, se pierde código manual.

**Q-* abierta** — `Q-LN-DRIFT-AUDIT`: ¿qué % de las clases LN tienen drift (código manual en archivo autogenerado)? ¿hay marcadores en código para detectar?

### 5.4 Implicación arquitectónica

Este patrón es la **plantilla universal de mantenimiento** del WMS:
- Un mantenimiento (Producto, Bodega, Cliente, etc.) = una tabla + clsBe + clsLn + clsLn.partial + form WinForms
- Crear un mantenimiento nuevo = generar las 3 clases + escribir el form que las usa
- Esto explica por qué la cantidad de archivos `clsBe*.vb` y `clsLn*.vb` en `WMS.DAL` es enorme y simétrica

---

## 6. Origen de DALCore / EntityCore / AppGlobalCore — la duplicación obligada

### 6.1 La intuición tecnológica

> *"La tendencia tecnológica me indicaba (mi intuición) debía elegir .NET Core como nueva arqui para la interface de WMS para futuras integraciones con otros sistemas, para tener webhooks y otras funcionalidades."*

Erik decidió que el WebAPI debía ir en .NET Core (hoy .NET 5+/6+/8+). Esto chocó con un problema técnico:

### 6.2 El problema

> *"Mis clases no son compatibles con .NET Core, y si las hago en .NET Core no son compatibles con mi core WMS."*

- **WMS.DAL legacy** = VB.NET Framework (4.x, no .NET Core)
- WinForms BOF = VB.NET Framework
- WSHHRN = VB.NET Framework
- **El nuevo WebAPI** = .NET Core C#

No hay forma de usar las DLLs legacy desde un proyecto .NET Core (binarios incompatibles).

### 6.3 La decisión: duplicación obligada

> *"Me veo en la dura necesidad de mantener lo mismo pero duplicado, eso implicaba replicar todo, bueno, casi todo, porque me daba la oportunidad de colocar en el WebAPI métodos y procesos limpios y depurados con los años, entonces desde esa perspectiva el sistema podría tener una reingeniería gradual."*

Por eso nacen los tres componentes Core (validados en `dev_2028_merge`, NO existen en `dev_2023_estable`):

```
WMS.AppGlobalCore/        ← utilidades base (5 archivos)
├─ clsInsert.cs
├─ clsUpdate.cs
├─ clsPublic.cs
├─ DataRecordExtensions.cs
└─ FormatoFechas.cs

WMS.EntityCore/           ← clases Be (Entity), portadas de WMS.Entity
├─ Acuerdos/clsBeTrans_acuerdoscomerciales_*.cs
├─ Cliente/clsBeCliente.cs, clsBeCliente_3pl.cs, clsBeClientesMi3.cs
├─ Cambio_Ubicacion/clsBeTrans_ubic_hh_*.cs
└─ ... (estructura paralela a WMS.Entity legacy)

WMS.DALCore/              ← clases Ln (CRUD + lógica), portadas de WMS.DAL
├─ Acuerdos/clsLnTrans_acuerdoscomerciales_*.cs
├─ Ajustes/clsLnTrans_ajuste_*.cs
├─ Centro_Costo/clsLnCentro_costo.cs
├─ Cliente/clsLnCliente.cs, clsLnCliente_bodega.cs, clsLnCliente_tiempos.cs
├─ Datos_Maestros/clsLnArancel.cs, clsLnBodega.cs, clsLnBodega_area.cs, clsLnBodega_muelles.cs
└─ ... (estructura paralela a WMS.DAL legacy)
```

### 6.4 La oportunidad oculta

La duplicación es trabajo extra, pero Erik la justifica como **oportunidad de reingeniería gradual**:
- En el port a .NET Core se pueden meter "métodos y procesos limpios y depurados con los años"
- El comportamiento que en VB.NET legacy era subóptimo (Erik tiene años de feedback de producción) se corrige al reescribir
- Es una migración + cleanup combinados

### 6.5 La visión a futuro

> *"El siguiente paso es llevar a la web lo que hoy se ejecuta en Windows Forms."*

DALCore/EntityCore son **preparación** para esto:
- Hoy WinForms BOF usa WMS.DAL/WMS.Entity (VB.NET Framework)
- Mañana un BOF Web (¿Blazor? ¿Razor Pages? ¿API + SPA?) usaría WMS.DALCore/WMS.EntityCore (.NET Core)
- En la transición conviven ambas capas: WinForms con DAL legacy, WebAPI/WebApp con DALCore

### 6.6 Quién invoca DALCore/EntityCore

> *"Por eso nacieron DALCore, EntityCore, para poder recibir transacciones desde nuestro propio WMS, o desarrollos que hagamos con ERPs de terceros mediante nuestros propios WebAPIs."*

**Consumidores actuales**:
1. **WMSWebAPI** (canal B2B-only, MHS = primer caso)
2. **DMS** (probable — pendiente verificar en código si DMS usa AppGlobalCore o sus propias clases)
3. **Futuros desarrollos web** (ej: portal CEALSA cuando madure)

**No usado por**: WSHHRN, BOF WinForms, MI3 — ellos siguen con WMS.DAL/Entity legacy en VB.NET Framework.

---

## 7. Modelo multi-tenant de propietarios — estados por propietario

### 7.1 El concepto Erik

> *"El estado estaba pensado por propietario para que por ejemplo si los productos de un consignatario son VIGENTE y BUEN ESTADO pero para otro solo es BUEN ESTADO, tuviera un criterio de exclusión que le dé mayor tropicalización al software."*

**Tropicalización** = adaptación de un producto a las particularidades locales / del cliente. En el contexto del WMS = capacidad de cada propietario de definir su propio set de estados aceptables sobre el mismo producto físico.

### 7.2 Tablas relevantes (validadas en CEALSA QAS)

```
propietarios                    ← maestro de propietarios (3PL clientes)
propietario_bodega              ← qué bodegas usa cada propietario
propietario_destinatario        ← destinos válidos por propietario
propietario_reglas_enc + det    ← reglas de negocio por propietario
producto_estado                 ← maestro general de estados
producto_estado_ubic            ← combinaciones permitidas estado × ubicación
```

### 7.3 Q-* abiertas

- **Q-PROPIETARIO-ESTADO-MODELO**: ¿hay tabla puente `propietario_producto_estado` o `propietario_estado_permitido`? Las 6 tablas listadas no la tienen aparente — el modelo puede ser más implícito (vía `propietario_reglas_*`).
- **Q-PROPIETARIO-AGNOSTICO**: clientes no-3PL (BECO, BYB, K7) — ¿usan `propietarios` con un único propietario "default"? ¿o ignoran la tabla?

---

## 8. stock_jornada — el invariante regulatorio (validado por SQL)

### 8.1 Quién lo usa hoy

| Cliente | stock_jornada filas | Lectura |
|---|---:|---|
| BECO | 0 | Tabla existe pero no usada |
| K7 | 0 | Tabla existe pero no usada |
| BYB | 0 | Tabla existe pero no usada |
| **MAMPA** | **21.883** | **Activo** |
| **CEALSA** | (presente, no contado) | **Activo** (origen del concepto) |

**Hallazgo crítico**: `stock_jornada` no es exclusivo CEALSA. **MAMPA también lo usa** — ¿porque MAMPA también es 3PL? ¿porque está en `dev_2028_merge` y allí viene activo por defecto? Pendiente confirmar con Erik.

### 8.2 Estructura (25+ columnas)

La tabla replica TODA la traza transaccional del stock:

| Columna | Significado |
|---|---|
| `IdStockJornada`, `IdJornadaSistema` | PK + FK a la jornada (corte) |
| `Fecha` | Fecha del corte |
| `IdBodega`, `IdUbicacion`, `IdUbicacion_anterior` | Localización física |
| `IdPropietarioBodega` | **Propietario** dueño del stock (multi-tenancy) |
| `IdProductoBodega`, `IdProductoEstado`, `IdPresentacion`, `IdUnidadMedida` | Producto + estado + presentación |
| `IdRecepcionEnc/Det`, `IdPedidoEnc`, `IdPickingEnc`, `IdDespachoEnc` | **Cadena transaccional completa** |
| `lote`, `lic_plate`, `serial` | Identificadores de unidad |
| `cantidad`, `uds_lic_plate`, `no_bulto` | Cantidades |
| `fecha_ingreso`, `fecha_vence` | Vigencia |

**Esto materializa exactamente el principio "como arriba es abajo"**: la jornada no es un agregado, es una **réplica completa de cada fila de stock con su contexto transaccional**.

### 8.3 Tablas auxiliares de la jornada

- `jornada_sistema` — encabezado de la jornada (cuándo se cerró, quién la cerró)
- `stock_jornada_consecutivo` — control del numerador correlativo
- `stock_jornada_desfase` — diferencias / ajustes detectados
- `stock_jornada_fecha_consecutiva` — control de continuidad temporal
- `stock_jornada_temporal`, `tmp_stock_jornada` — staging
- `pendiente_jornada` — jornadas no cerradas

### 8.4 Q-* abiertas

- **Q-STOCK-JORNADA-PROCESO**: ¿qué SP / componente cierra la jornada? ¿es batch nocturno? ¿manual?
- **Q-STOCK-JORNADA-CONSUMER**: ¿quién consume el resultado? ¿reporte para SAT/SIB? ¿el portal? ¿ambos?
- **Q-STOCK-JORNADA-DESFASE**: ¿cómo se calcula `stock_jornada_desfase`? ¿qué pasa cuando hay desfase?
- **Q-STOCK-JORNADA-MAMPA**: ¿por qué MAMPA tiene 21.883 filas? ¿está activo por la rama 2028 o porque MAMPA también es regulado?

---

## 9. cealsasync y el "Nav" residual — observación contradictoria

### 9.1 Estructura del módulo

```
TOMWMS_BOF/CEALSAMI3/
├─ CEALSASYNC.sln
├─ CELSASYNC.vbproj                         ← typo: "CELSA" sin la A intermedia
├─ AppGlobal/
└─ Clases Interface Sync/
   ├─ clsInterfaceBase.vb
   ├─ Cliente/clsSyncERPCliente.vb
   ├─ Lotes/clsSyncLotes.vb
   ├─ Pedido_Compra/clsSyncERPAcuerdosComerciales.vb
   ├─ Producto/
   │  ├─ clsSyncNavProducto.vb              ← prefijo "Nav"!
   │  ├─ CategoriasProductos/clsSyncNavCategoriasProducto.vb
   │  └─ GruposProductos/clsSyncNavGruposProducto.vb
   └─ Tabla_Conversion/clsSyncNavTablaConversion.vb
```

### 9.2 La contradicción

Erik (Wave 6.1): *"Es muy específico para BYB, de momento solo BYB tiene NAV."*

Pero el módulo CEALSAMI3 (sync de CEALSA con su ERP MercaERP/IMS4) usa **clases con prefijo `Nav`**:
- `clsSyncNavProducto`
- `clsSyncNavCategoriasProducto`
- `clsSyncNavGruposProducto`
- `clsSyncNavTablaConversion`

**Hipótesis a validar con Erik**:
1. **Copy-paste de un BYBSync original**: el módulo CEALSA fue construido copiando un sync NAV (BYB) y nunca se renombró el prefijo. Las clases ya no consumen NAV, solo IMS4/MercaERP.
2. **CEALSA tiene NAV adicional**: además de su ERP principal, CEALSA usa NAV para algún módulo (categorías, grupos, tabla conversión).
3. **`Nav` no es Microsoft Dynamics NAV**: es prefijo histórico que significa otra cosa (¿Navegación? ¿Navision genérico?). Improbable, pero hay que descartar.

**Q-NAV-PREFIJO-CEALSA** — abierta.

---

## 10. Visión de migración — BOF WinForms → Web

### 10.1 La declaración

> *"El siguiente paso es llevar a la web lo que hoy se ejecuta en Windows Forms."*

### 10.2 Implicaciones

1. **DALCore + EntityCore + AppGlobalCore son inversión preparatoria** para esta migración.
2. **WMSWebAPI puede expandirse** de B2B-only a también backend de la nueva web BOF.
3. **El generador de código probablemente necesita una tercera variante** (genera UI Web — Blazor / Razor / SPA components) además de las dos actuales (VB.NET y C#).
4. **WSHHRN seguirá existiendo** para HH Android (no se va a web la HH).
5. **MI3 puede sobrevivir o ser absorbida** por WMSWebAPI con adaptadores ERP.

### 10.3 Q-* abiertas

- **Q-WEB-BOF-STACK**: ¿qué tecnología web tiene Erik en mente? Blazor Server / Blazor WASM / React + .NET API / Razor Pages?
- **Q-WEB-BOF-TIMELINE**: ¿después de cerrar la migración 2023→2028 de todos los clientes? ¿en paralelo?
- **Q-GENERADOR-WEB-VARIANTE**: ¿hay que extender el generador a UI Web?

---

## 11. Q-* nuevas que abre este capítulo

### Sobre el DMS
- **Q-DMS-USA-DALCORE**: ¿el DMS (`DMS/Clases/*.vb`) usa `AppGlobalCore`/`DALCore`/`EntityCore`, o tiene sus propias clases?
- **Q-DMS-DESTINO-CLOUD**: ¿qué tipo de target en cloud? ¿SQL Server en VPS? ¿Azure SQL? ¿containers?
- **Q-DMS-PROPIETARIO-FILTER**: ¿dónde se configura qué propietarios replican? ¿tabla `propietarios.replicar_a_cloud`? ¿config en App.config?

### Sobre el portal CEALSA
- **Q-PORTAL-STACK**: ¿qué tecnología? ¿está construido? ¿cuál es el repo?
- **Q-PORTAL-AUTH**: ¿cómo autentica al propietario? ¿usuarios CEALSA-side o propietario-side?
- **Q-PORTAL-MULTITENANCY-DECISION**: ¿se decidió 1-BD-por-cliente o 1-BD-compartida-multi-tenant? Estado actual de la decisión.

### Sobre el generador
- **Q-GENERADOR-UBICACION**: ¿dónde vive el código del generador? ¿es app standalone? ¿Efren mantiene?
- **Q-GENERADOR-INPUTS**: ¿cómo se especifica el contrato de generación? ¿plantillas T4? ¿código manual de templates?
- **Q-LN-DRIFT-AUDIT**: % de clases LN con código manual en archivo autogenerado.

### Sobre DALCore vs DAL legacy
- **Q-DALCORE-PARIDAD**: ¿qué % del DAL legacy fue portado a DALCore?
- **Q-DALCORE-CONSUMERS**: ¿solo WMSWebAPI? ¿DMS también? ¿algún componente más?
- **Q-DALCORE-COMPORTAMIENTO**: ¿hay diferencias semánticas con DAL legacy? Ej: nuevo manejo de excepciones, nuevo logging, nueva validación.

### Sobre stock_jornada
- **Q-STOCK-JORNADA-PROCESO**: SP / componente que cierra jornada
- **Q-STOCK-JORNADA-CONSUMER**: quién consume (SAT/SIB/portal/auditoría)
- **Q-STOCK-JORNADA-DESFASE**: cómo se detectan diferencias y qué se hace
- **Q-STOCK-JORNADA-MAMPA**: ¿por qué MAMPA también lo usa?

### Sobre el repo CEALSA vacío
- **Q-CEALSA-REPO**: el repo `CEALSA` en Azure DevOps existe pero está vacío (0 MB, sin ramas) — ¿iba a ser el portal? ¿el DMS específico? ¿se canceló?

### Sobre propietarios y tropicalización
- **Q-PROPIETARIO-ESTADO-MODELO**: tabla puente exacta entre propietario y estados aceptables
- **Q-PROPIETARIO-AGNOSTICO**: cómo manejan propietarios los clientes no-3PL (BECO/BYB/K7)
- **Q-NAV-PREFIJO-CEALSA**: ver §9.2

---

## 12. Cierre — principios arquitectónicos confirmados Wave 6.1

1. **Control total > performance**: Erik prefiere fila-por-fila debuggeable a bulk insert opaco. Aplica al DMS, al WSHHRN, y a la decisión de no usar ORM.

2. **"Como es arriba es abajo"**: la BD cloud-target replica la estructura on-prem en lugar de simplificarla. La integridad estructural se prioriza sobre la economía de espacio (parcial — se filtra por propietario).

3. **Duplicación obligada como oportunidad**: cuando .NET Framework y .NET Core no pueden compartir DLLs, se duplica con cleanup. La duplicación se justifica por el cleanup que permite.

4. **Generación de código como base de mantenibilidad**: el patrón BE / LN-base / LN-partial es la plantilla universal. El generador es la primera automatización del WMS.

5. **Tropicalización por propietario**: el modelo multi-tenant 3PL no es tabla de tenant, es **reglas de negocio por propietario** (`propietario_reglas_*` + estados aceptables por propietario).

6. **Reingeniería gradual con coexistencia**: WMS.DAL legacy + WMS.DALCore .NET Core conviven. WSHHRN + WMSWebAPI conviven. La migración no es big-bang.

7. **Infra existente como apalancamiento**: el DMS aprovecha el license-server que ya corre server-side, no monta infra nueva.
