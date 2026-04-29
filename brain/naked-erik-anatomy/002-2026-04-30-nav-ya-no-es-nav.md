# 002 — 2026-04-30 — NAV ya no es NAV

> Carolina respondio. Once preguntas. Una de ellas reescribio el brain entero.

---

Hay momentos donde uno entiende algo del codigo y siente que avanzo. Y hay
momentos donde uno entiende UNA cosa y se da cuenta que estuvo leyendo mal
todo el codigo durante semanas. Hoy fue del segundo tipo.

La pregunta era inocente: en `CEALSAMI3/Clases Interface Sync/` hay clases
con prefijo `clsSyncNav<X>`. Erik me habia dicho clarito: "NAV es solo BYB".
Entonces, ¿que hacen estas clases con prefijo Nav en el modulo de CEALSA?
Era un misterio operativo, una contradiccion que arrastraba desde Wave 6.1.

Carolina respondio. Y la respuesta es de esas que hacen pisar el freno de
golpe:

> "NAV significa lo que indicas, porque fue la primera Interface que Erik
> desarrollo para TOMWMS, pero resulto que luego al crear las nuevas
> Interfaces a Erik le resulto funcional manejar las mismas tablas y por
> tanto las mismas clases y nos quedamos con ese nombre el cual ya no lo
> identificamos con NAV sino que para nosotros es sinonimo de ERP o de
> Interface."

Tres minutos de leer eso despacio. Y despues recorrer mentalmente el
brain entero.

`i_nav_config_enc`. **No es de NAV.** Es la tabla de configuracion por
interface ERP, sea NAV, SAP, custom de ARITEC, o cualquiera.

`i_nav_transacciones_out`. **No es de NAV.** Es la outbox de transacciones
hacia el ERP, sea cual sea.

Las columnas con sufijo `_NC`, `_nc`, `_nav`. **No son de NAV.** Son sufijos
historicos heredados.

Los binarios `NavSync.exe`. Estos SI son historicamente de NAV pero el
**naming** se generalizo: en clientes nuevos con SAP, el ejecutable es
`SAPBOSync<Cliente>.exe` pero la tabla destino sigue siendo
`i_nav_config_enc`.

Las clases `clsSyncNav<X>` en CEALSAMI3. **No son de NAV.** Son las clases
de sync de productos / categorias / grupos / tablas de conversion para
el ERP que ARITEC desarrollo para CEALSA. Que se va a migrar a Odoo.
Pero el nombre va a quedar siendo Nav porque nadie lo va a renombrar
ahora.

Hay algo casi poetico en esto. El primer cliente del WMS pago el peaje de
darle nombre a la infraestructura. Y como Erik tiene la disciplina de no
duplicar codigo cuando puede reusar (uno de los principios de Wave 6.1),
las tablas y clases del primer cliente terminaron sirviendo a todos los
demas, conservando el nombre original. Fueron diez anos despues que el
nombre dejo de significar algo. Y ahora "Nav" es ruido. Es como esas
calles que se llaman "Avenida del Tren" en ciudades donde el tren ya no
pasa, y nadie se acuerda por que se llamaba asi pero todos saben donde es.

---

Lo que me deja pensando es esto: cada vez que veo un nombre en codigo
legacy, tengo que preguntarme si el nombre sigue significando lo que
parece significar, o si es ruido historico. La regla que estaba aplicando
"si dice Nav es de Microsoft Dynamics NAV" era falsa de raiz. Y la unica
razon por la que la creia es porque aplique sentido comun de naming sin
verificar.

Hay un patron mas grande aca. Cuando Erik dijo "MI3 = Modulo de
Integracion con Terceros" en Wave 8, fue otra revelacion del mismo tipo:
una sigla cuyo significado se habia perdido del lenguaje cotidiano del
equipo. Carolina hoy refuerza el patron: las cosas en este WMS tienen
nombres que contaron una historia y luego dejaron de contarla.

Si tuviera que agregar un principio Erik a la lista de Wave 6.1, seria
este: **"Naming primer cliente queda como generico"**. Es el corolario
historico de "duplicacion obligada como oportunidad". Cuando duplicar es
caro y reusar es facil, los nombres sobreviven a su semantica. Y el que
viene despues a leer el codigo paga el precio de ese reuso.

---

Otras cinco respuestas de Carolina hoy:

- El cambio mayor de 2028 es **IDENTITY autoincremental**, no
  funcionalidad. Errores de concurrencia donde no se insertaban
  recepciones ni reservas. Y la causa raiz era hacer `MAX(id)+1` en
  aplicacion. Es el tipo de bug que existe durante anos antes de
  hacerse visible. Llego un dia donde MAMPA tenia tantos operadores
  simultaneos en HH que el `MAX(id)+1` colisionaba constantemente y se
  perdian recepciones. Ese fue el limite. Y de ese limite nacio el WMS
  2028.

- La rama `dev_2025` no fue alias muerta. Fue donde se gesto la
  funcionalidad de **talla x color para MAMPA**. Cuando el feature
  termino, se mergeo a `dev_2028_merge`. La rama quedo congelada como
  fosil. Wave 6.2 me hizo creer que era duplicada porque el SHA visible
  coincidia con `dev_2023_estable`, pero esa coincidencia es engañosa:
  el contenido propio de `dev_2025` ya esta en `dev_2028_merge`.

- `master` es **release oficial congelada**. No se trabaja ahi. El
  default de GitHub es engañoso. Las ramas de trabajo son
  `dev_2023_estable` y `dev_2028_merge`.

- **CLARISPHARMA es un cliente** que no estaba en mi mapa. Va con
  Becofarma en la cola de migracion 2028. Probablemente farmacia. Es
  una alegria descubrir clientes nuevos despues de creer que ya tenia
  el inventario completo.

- **MERCOSAL** acaba de pasar BD a Erik. Probable El Salvador,
  probable holding IDEALSA. Lo que confirma una hipotesis de Wave 7:
  el holding IDEALSA tiene mas filiales que las dos que veiamos.
  MERHONSA + MERCOPAN + MERCOSAL = Honduras + Panama + El Salvador. La
  arquitectura corporativa centroamericana del holding empieza a
  emerger.

- Y un dato lateral pero importante: el **bug de implosion** que
  permitia tener la misma licencia en distintas ubicaciones con
  distintos estados. La rama `dev_2028_Cumbre` esta arreglando esto.
  Cuando se mergee, llega a todos los clientes 2028. Pero MAMPA YA
  esta en prod sin el fix. Es deuda silenciosa: cuantos LPs corruptos
  hay en MAMPA hoy.

---

El brain crece tres dimensiones a la vez con cada respuesta de Carolina.
La primera dimension es factica (tal cosa significa tal cosa). La segunda
es revisionista (lo que creia ya no es asi). La tercera es generativa
(esta respuesta abre tres preguntas nuevas).

Once respuestas, diez aprendizajes nuevos, once preguntas nuevas. El
gradiente es positivo: por cada pregunta cerrada se abren nuevas, pero
las nuevas son MAS especificas y mas faciles de cerrar. Eso es lo que
Wave 0 buscaba: hacer que el brain converja en hipotesis cada vez mas
afiladas, no en confusiones cada vez mas amplias.

---

Carolina escribe con autoridad serena. No dice "creo que" ni "no estoy
segura". Dice "es asi" y suelta el dato. Cuando no sabe (Q-CEALSA-ARITEC-
ERP-NOMBRE) lo dice claro: "no recuerdo su nombre". Esa precision es
ORO. Es la diferencia entre alguien que conoce el sistema y alguien que
lee documentacion sobre el sistema.

Y hay otra cosa que aprecio: Carolina respeta el formato del cuestionario.
Numera, cita la pregunta, responde en el espacio correcto. Eso permite
procesar las respuestas sin ambiguedad. Si trabajara en estructuras
sueltas (audio, llamada, brain dump) seria 5 veces mas lento extraer la
misma informacion. Hay un valor enorme en hacer las cosas bien
formateadas, y Carolina lo entiende.

---

Mañana viene mas. Carolina respondio bloques 1 y 2. Quedan los bloques
3 (Portal CEALSA / DMS), 4 (stock_jornada), 5 (DALCore / generador), 6
(multi-tenancy 3PL), 7 (capabilities), 8 (Web BOF), 9 (bonus), 10 (Wave
6.2 quick wins), 11 (Wave 7 holding + implosion), 12 (Wave 8 reserva +
MI3), 13 (Wave 9 casos naturales). Mas el bloque 14 que acabo de abrir
en Wave 10. Hay material para semanas.

Y ya no leo `clsSyncNavProducto` igual que ayer. Hoy se que ese "Nav" no
significa lo que parece significar.

— el agente
