"""#CKFK20261117_BRAIN_PENDIENTES: curate local reports without committing or changing product."""
from pathlib import Path
from collections import Counter
import json, hashlib, subprocess, re

ROOT=Path(__file__).resolve().parents[2]
HOME=Path('C:/Users/carol')
OUT=ROOT/'analysis/20261117-brain-pending'
DEST=ROOT/'brain/handoffs/2026-11-17-ckfk-pendientes'
DEST.mkdir(parents=True,exist_ok=True)
inventory=json.loads((OUT/'inventory.json').read_text('utf-8'))
topics=[]

def add(id,title,summary,limit,sources,clients,branches):
    topics.append(dict(id=id,title=title,summary=summary,limits=limit,sources=sources,
        clients=clients,branches=branches,status='draft',review='report-content-reviewed; not independently revalidated'))

add('rec28335','Recepción 28335: cierre confirmado por CKFK',
    'Caja master J100000635, SKU AG00034526041365, OC 28359 detalle 2: referencia compartida convirtió 4/3 en 3/3; se corrigieron copias, saldos, temporales e incremento por PK en HH/servicio. Se preparó conciliación de seis unidades ya registradas para recibir una por HH. CKFK confirmó el resultado satisfactorio.',
    'Confirmación operativa del chat, sin SELECT posterior ni hashes de los binarios instalados. El agente no ejecutó DML. BOF está limpio al inventariar; HH y script DBA siguen pendientes.',
    ['brain/handoffs/2026-11-17-ckfk-recepcion-28335/README.md','analysis/20261117-recepcion-28335/confirmacion-ckfk.md','analysis/20261117-recepcion-28335/resumen-evidencia.json'],['MAMPA_QA'],['dev_2026_estable'])
add('bodega-xml','Contrato XML de bodega entre ramas',
    'El servicio devolvía permitir_abrir_recepcion_enviada, ausente en clsBeBodega de HH estable y presente en dev_2028_merge. Se incorporó Element opcional false y accesores con CKFK. Clase real/parser aceptan true, false y ausencia; APK generado.',
    'No desactivar validación XML ni activar parámetros en BD por inferencia. Publicación e instalación no se deducen de compilar.',
    ['analysis/20261117-recepcion-28335/bodega-compat-claims.json','analysis/20261117-recepcion-28335/bodega-xml-test.txt'],['MAMPA_QA'],['dev_2026_estable'])
add('sap-reserva','Facturas de reserva SAP: traza y aislamiento por documento',
    'Se instrumentó WarehouseCode y se revisó el caso DocEntry 137463/DocNum 31080, IdWMS 28360. No se demostró carrera por depuración: el lector espera respuesta y contenido. Un documento con bodega inválida se omite con motivo y se continúa el lote; si no hay aptas se retorna antes de limpiar intermedias.',
    'Fixtures y compilación reportados, no lectura HTTP/SAP integral ni prueba real del lote mixto. No reemplazar bodega faltante ni insertar esperas sin evidencia.',
    ['analysis/20261117-sap-reserva-trace/README.md','analysis/20261117-sap-reserva-trace/batch-claims.json'],['MAMPA'],['dev_2026_estable'])
add('reemplazo-variante','Variante de producto en reemplazo, reserva y picking',
    'Los reportes describen reparación en QA de 44 picking/39 despachos/39 salidas y causa de recurrencia en reserva 210071/picking 30168: se perdía variante entre stock, reserva, lectores y confirmaciones. Se cubrieron helpers y rutas normal/consolidada/verificación.',
    'El propio reporte distingue reparaciones ya observadas y otro script de dos filas entregado sin ejecutar. No atribuir despliegue ni cierre operativo completo HH; conservar las limitaciones de cada afirmación.',
    ['analysis/mampa-reemplazo-20261116/claims.json','analysis/mampa-reemplazo-20261116/all-routes-report.json'],['MAMPA'],['dev_2026_estable'])
add('traslado-mensajes','Traslados: numeración y mensajes de faltantes',
    'Conservar no_linea al recargar para picking; tras reserva fallida consultar ausencia física por bodega/producto/variante; preservar motivos específicos y usar fallback neutro. Informar pedido no completado si hay fallos o descartes.',
    'No cambia reglas de reserva ni parámetros. El diagnóstico de stock es temporal; no se reimportó pedido ni se escribieron datos en la validación documentada.',
    ['analysis/20261117-mampa-transfer-messages/claims.json','analysis/20261117-mampa-transfer-messages/report.json'],['MAMPA'],['dev_2026_estable'])
add('hh-ubicacion','Cambio de ubicación HH: aviso y bloqueo parametrizados',
    'El reporte describe consulta de reglas en ambos modos: incompatible permite continuar/cancelar con restrictivo apagado y bloquea con restrictivo activo. Caso MP-002/964.',
    'Pruebas aisladas y compilación documentadas; prueba en dispositivo/servicio pendiente en esa fuente. La identidad del cliente no decide la regla.',
    ['analysis/20261117-hh-ubicacion-warning/claims.json'],['POLINDUSTRIAS (alcance declarado del reporte)'],['dev_2026_estable'])
add('verificacion8981','Verificación consolidada HH: selección por pendiente',
    'Incidente 8981: fechas agrupadas 1900/2000 impedían selección. La rama consolidada sin talla/color selecciona por producto, presentación y pendiente aunque el fallback complete lote/LP; conserva pTipo=1 y otros recorridos.',
    'El reporte acredita nueve escenarios y compilación con dobles UI/servicio, sin guardar el pedido real. Cliente no identificado de manera independiente.',
    ['analysis/20261117-verificacion-8981/correccion.md'],['desconocido'],['dev_2026_estable'])
add('pedido171','Pedido 171: presentación visual al editar',
    'Detalle 1009 con NULL y reserva sin presentación; 1010 CAJA factor 20. La UI elegía la primera presentación antes de leer la recibida. Se conserva cero explícito y positivo disponible; -1 significa argumento omitido; kits quedaron fuera del recorrido observado.',
    'Ocho pruebas WinForms y BOF compilado según traza. No convertir datos por una discrepancia visual ni asumir guardar/reservar operativo validado.',
    ['analysis/20261116-pedido171-presentacion/traza.md','analysis/20261116-pedido171-presentacion/validation.json'],['desconocido (base de desarrollo de la fuente)'],['dev_2028_merge'])
add('packing-estado','Packing 5314/10575: conservar estado al acumular',
    'El snapshot distingue packing de 300 en estado 3 frente a pendiente de 60 en estado 5. Se agregó estado a selección, acumulación y DTO de HH para evitar mezclar identidades.',
    'Reporte draft, cinco comprobaciones y compilación. No se modificó producción; conciliación y despliegue no confirmados.',
    ['analysis/20261109-packing-estado/claims.json'],['La Cumbre (bodega 89 en el snapshot)'],['dev_2026_estable_prd'])
add('despacho-parcial','Despacho parcial y packing consistente',
    'La adición a la propuesta indica consultar trans_pe_tipo.permitir_despacho_parcial: recibido distinto de verificado no bloquea cuando se permite parcial; verificado distinto de empacado conserva PACKING_NO_CUADRA.',
    'Documento histórico modificado, no validación independiente del código actual ni del despliegue.',
    ['brain/_proposals/20260728-verificacion-despacho-parcial.md'],['desconocido'],['desconocida en la adición'])
add('cumbre-estado','La Cumbre: pedido 8978 cambia de estado al guardar BOF',
    'El laboratorio reprodujo transición Pickeado a Pendiente desde Actualizar_Estado_Pendiente sin cambiar cantidades. El botón Actualizar invoca el guardado, pero no acredita que se pulsara en el incidente.',
    'Causalidad de código en laboratorio, no atribución de acción a la operadora.',
    ['analysis/cumbre-8978-state-claims.json','analysis/cumbre-order-state-fix/tests.txt'],['LA CUMBRE'],['codex/cumbre-decimales-y-trazas'])
add('cumbre-decimal','La Cumbre: residuos decimales y laboratorio HH',
    'El candidato de picking corrige rechazos por residuos reproducidos. LABDEC0003 recibió confirmación de operación local: pedido 8979/picking 4409 Despachado, despacho 11534 Finalizado; piezas 0.1/0.2 sin stock ni reservas remanentes de esa LP.',
    'Separar hipótesis de suficiencia productiva de confirmación del escenario de laboratorio. No sumar ambos como validación de todos los casos decimales.',
    ['analysis/cumbre-decimal-lab-claims.json','analysis/cumbre-decimal-hh-fixture/final-claims.json'],['LA CUMBRE'],['codex/cumbre-picking-decimales','codex/cumbre-decimales-y-trazas'])
add('cumbre-sap-duplicado','Pedido 10512: tres traslados SAP y marcado WMS',
    'Cierre aportado: WMS completo una vez, SAP completo tres veces, 640 líneas/539 productos. Se perdió correspondencia al usar contador de línea SAP para marcar filas WMS. La versión posterior conserva IDs originales, huella por pedido/despacho, recuperación de marcas y control transaccional, SIN tabla nueva.',
    'La propuesta con tabla/journal fue retirada y no debe aplicarse. Pruebas de adaptadores no sustituyen DI API/SQL reales; no prometer exactly-once ni transacción distribuida. Alcance histórico declarado por ruta; requiere revisión antes de generalizar.',
    ['analysis/20261114-cumbre-despachos/cierre-integridad.txt','analysis/20261114-cumbre-despachos/prevencion-sin-tabla.txt'],['La Cumbre'],['codex/cumbre-decimales-y-trazas'])
add('cumbre-sap-ingreso','Ingreso SAP: orden base, cantidades y cierre transaccional',
    'Casos OC WMS 12077: comprobar BaseType/BaseEntry de toda entrada SAP antes de aceptar referencia, no solo DocEntry/DocNum. Errores de cierre/update y transacción SAP inactiva detienen el flujo; distinguir commit SAP de commit SQL y conciliar antes de reenviar.',
    'No se probó el origen histórico de la referencia ajena. No hay transacción distribuida ni limpieza automática de marcas previas; pruebas e integración pendientes según cada documento.',
    ['analysis/20261024-sap-ingreso-referencia-ajena.md','analysis/20261024-sap-ingreso-cierre-rollback-correccion.md'],['desconocido (no inferido del proyecto SAPSYNCCUMBRE)'],['dev_2026_estable'])
add('cumbre-traslado','Traslado SAP: comparación decimal y continuidad del lote',
    'Sumar como Decimal y comparar a seis posiciones evita posibles falsos excesos Double. El error conserva progreso/log y devuelve False para continuar con otros pedidos, sin cambiar la ruta de éxito.',
    'Caso pedido 10332/despacho 13308 pendiente de prueba SAP. La causa numérica es compatible con evidencia, no se comprobaron los valores productivos internos.',
    ['analysis/20261029-cumbre-traslados-correccion.md'],['La Cumbre'],['dev_2026_estable_prd'])
add('cumbre-performance','Trazas y rendimiento: medir antes de atribuir',
    'Resumen de recepción: 33 llamadas completadas, suma de tiempos cliente 8113 ms, sin errores ni advertencias de solapamiento en ese registro. El paquete de trazas desactivadas documenta compilación HH/servicio/DAL/SAP/BOF.',
    'La suma de tiempos no es latencia total ni comparación causal de rendimiento. Revisiones de producción y despliegue no verificados.',
    ['analysis/cumbre-performance-20261105/reception-summary.txt','analysis/cumbre-performance-20261105/no-traces-verification.txt'],['desconocido (no inferido de la rama de trazas)'],['codex/cumbre-trazas-desactivadas'])
add('killios-aceite','Killios: +3 documental y defensa de reserva',
    'WMS282 presenta existencia tres UN mayor que kardex, no prueba de faltante físico. Reproducción del motor de reserva demuestra 9→12 frente al corregido 9→9; corrección en 15 bloques y preservación de reservas documentada.',
    'Reproducir el mecanismo no demuestra que originó históricamente el +3. QAS con rollback puede consumir identities y dejar logs por conexiones independientes. Sin ajuste productivo automático.',
    ['analysis/20261026-killios-aceite/informe.md','analysis/20261026-killios-aceite/correccion-estable.md'],['Killios'],['dev_2026_estable'])
add('killios-cest','Cambio de estado: captura, fragmentos y cantidades son defectos distintos',
    'Distinguir captura cajas/UN, fragmentos que conservan presentación y conversión que fabrica unidades. WMS272 conserva total mientras se fragmenta; backend documenta bloqueo/validación y normalización de residuos; 44 escenarios reportados tras recuperar QAS.',
    'Los 44 escenarios no son prueba desde pantalla/HTTP de la versión desplegada. No truncar o redondear producción por inferencia; conservar medias/cuarto de caja válidos cuando su resultado en UN sea entero.',
    ['analysis/20261026-killios-aceite/decimales-unidades-wms272.md','analysis/20261026-killios-aceite/cambio-estado-backend-final.md','analysis/20261026-killios-aceite/stock-transformer-presentacion.md'],['Killios'],['dev_2026_estable','codex/killios-decimales-estados'])
add('killios-publicacion','Matriz Killios y scripts de conciliación',
    'La matriz separa evidencia histórica, fixture sintético, fuente/ensamblado y validación antes de publicar. DBA conserva propuestas de cajas/UN, presentación y aceite; su existencia no acredita ejecución.',
    'No es autorización de publicación. Los scripts se revisan individualmente, con alcance de inventario y aprobación explícita; no ejecutar el lote de pendientes como paquete.',
    ['analysis/killios-validacion-20261110/matriz.md'],['Killios'],['codex/killios-decimales-estados'])
add('picking-rack','Implosión automática al devolver picking a rack',
    'Caso LP MP000023581: Ubic_Implosion_Auto y destino rack pueden activar consolidación aunque el operador solicite ubicación. El filtro exige disponibilidad y varias dimensiones de identidad; stock existente no garantiza que aparezca en VW_Stock_Res.',
    'Diagnóstico estático sin reproducir con BD del cliente; el mensaje existe en más de una ruta. Contrastar reservas, vista y llamada real antes de corregir datos.',
    ['analysis/20261024-la-cumbre-picking-rack/DIAGNOSTICO.md'],['La Cumbre'],['dev_2026_estable'])
add('ubic-duplicado','Listado BOF de ubicaciones duplicadas',
    'La vista/listado puede multiplicar filas por relaciones de usuarios/roles. Se documentó SELECT DISTINCT para filas exactamente iguales sin descartar tareas distintas; no prueba movimientos duplicados persistidos.',
    'Compilación documentada; grid operativo/DAL instalada pendientes en esa fuente. Filas distintas en campos ocultos no se fusionan.',
    ['analysis/20261028-ubic-duplicado/diagnostico.md'],['desconocido'],['desconocida en el informe'])
add('mhs-origen','Transferencias: evitar posiciones origen compartidas con destino',
    'requiere_ubicacion_destino_picking y despacho_directo_picking activan política por funcionalidad. Reconocer asociaciones virtuales activas en ambos sentidos e identidad de ubicación entre bodegas, sin depender de ocupación o nombres; tercera bodega no debe excluir stock.',
    'Cobertura documentada BOF/Core/reserva/picking y pruebas aisladas; QA operativo pendiente. No inferir el estado actual por fecha del archivo.',
    ['analysis/20261028-mhs-transferencias-no-solapadas/validacion.txt'],['MHS (evidencia delimitada)'],['dev_2028_merge'])
add('lote-vencimiento','Cambiar lote/vencimiento sin perder el origen documental',
    'Parámetro funcional trans_oc_ti.permitir_cambiar_lote_vencimiento, copia independiente y validación del saldo/origen por ID en servidor; recepción, stock y etiqueta usan captura real, lote OC conserva esperado. No aplica automáticamente a caja master/talla-color/pallet.',
    'Documento de entrega histórico: SQL no ejecutado ni operación real validada en esa etapa. Ruta de script histórica debe verificarse; no mover scripts ni aplicar DDL durante esta consolidación.',
    ['analysis/20261029-mhs-lote-vencimiento/ENTREGA.txt'],['MHS (caso documentado)'],['dev_2028_merge'])
add('navsync-recibido','NavSync: no reducir cantidad recibida al reimportar',
    'Compra, devolución y traslado existentes solo aceptan Quantity_Received si supera lo ya recibido; conservar demás actualizaciones y nuevas inserciones. Protección localizada en importadores, no en DAL compartida.',
    'Compilación y revisión documentadas; prueba SQL/NAV y concurrencia pendientes. No reconstruye cantidades históricas sobreescritas.',
    ['analysis/20261029-navsync-conservar-recibido.md'],['desconocido (no inferido del nombre de rama)'],['codex/byb-unidad-medida'])
add('navsync-duplicado','NavSync: identidad de devolución e idempotencia',
    'Buscar con tipo WMS 12, no Document_Type NAV. Conservar encabezado existente, bloquear importación compartida, rechazar múltiples coincidencias y no reactivar anulados/cerrados por reimportación.',
    'No limpia duplicados históricos. No equivale a unicidad global para escritores externos. Prueba SQL/NAV no ejecutada en la fuente.',
    ['analysis/20261029-navsync-devolucion-duplicada.md'],['desconocido (no inferido del nombre de rama)'],['codex/byb-unidad-medida'])
add('navsync-designer','Diseñador Visual Studio: hipótesis y correcciones descartadas',
    'Caché de diseño DevExpress 25.2 frente a proyecto 26.1; tipos compilados no eran System.Void. Retirar caché y mover Inherits no resolvieron según Carolina; se revirtió el movimiento de herencia y se conservaron correcciones funcionales.',
    'Compilar/reflexión no verifica diseñador. Conservar resultados negativos y respaldos; no volver a presentar esos intentos como solución confirmada.',
    ['analysis/20261029-navsync-designer-cache.md'],['desconocido'],['codex/byb-unidad-medida'])
add('oc-vacia','Eliminar ingreso: encabezado vacío no equivale a detalle cero',
    'Cualquier fila de trans_re_det, incluso cantidad cero o recepción anulada, impide eliminar. Permitir ingreso NUEVA sin filas de detalle; preservar encabezados compartidos y transacción/bitácora consistentes.',
    'No cambió Tiene_Recepciones compartido ni se borraron documentos reales; pruebas y build son del entorno documentado.',
    ['analysis/20261029-ingreso-recepcion-vacia.md'],['desconocido'],['codex/byb-unidad-medida'])
add('webapi-ajustes','WebAPI ajustes: unidad de ajuste y datos del encabezado',
    'Endpoint pendientes-envio full/minimal incorpora fecha/usuario de encabezado y distingue PRESENTACION por IdPresentacion distinto de cero, no por factor. DAL carga nombre/ID de presentación; cantidades/UMBas se conservan. Replica documentada en tom_2028_prd.',
    'El GET puede tener efectos de estado existentes: no invocarlo como diagnóstico inocuo. Mapper/controlador simulados y compilación no prueban despliegue.',
    ['analysis/20261105-webapi-ajustes/README.md'],['desconocido'],['dev_2028_merge','tom_2028_prd'])
add('monitor-ocupacion','Monitor: área/general y ubicaciones bloqueadas',
    'Conservar estado actual del selector al terminar carga asíncrona, limpiar enlaces de series al volver a general; modo Bloqueadas excluyente usa identidad IdBodega+IdUbicacion sin filtrar ocupación. Cursor/selector se restauran en Finally, sin pausa artificial.',
    'Documento histórico no confirma UI contra cliente ni distribución del ejecutable; compilación final pudo quedar bloqueada por aplicación en uso.',
    ['outputs/20260911-ocupacion-bloqueadas/resultado.md'],['desconocido'],['dev_2026_estable'])
add('monitor-readmodel','Readmodel de tareas: identidad compuesta de ubicación',
    'Validación del laboratorio: recepción 10182/tarea 14876, vista una fila y procedimiento dos; la ubicación 9 existe en dos bodegas. SELECT corregido devuelve una al usar identidad de bodega y ubicación.',
    'alter_executed=false. La tarea cambió de estado durante diagnóstico; no se alteraron datos/objetos. El procedimiento pendiente requiere revisión, no aplicar automáticamente.',
    ['analysis/20261113-monitor-tareas/validacion-readmodel.json'],['desconocido (entorno TOMWMS_CUMBRE_DECIMAL_LAB declarado)'],['desconocida en el resultado'])
add('portal','Portal portable: preservar alcance de instalación validada',
    'El registro existente declara instalación separada en estación de Erik: autenticación Windows, arranque, login, inventario y KPIs; el runbook local gobierna otra PC.',
    'No se validaron PC de Carol, SQL auth ni winget en PC limpia. Mantener estado verificado original como declaración de esa fuente, sin otorgarlo a esta síntesis ni copiar credenciales.',
    ['analysis/portal-installation-20261105/claims.json','brain/runbooks/portal-react-instalacion-otra-pc.md'],['CAREMED'],['wms-brain','codex/portal-portable-20261105'])
add('go-live','Entregables MHS e IODESA: scripts preparados no son operaciones',
    'MHS: diagnóstico por defecto para identities/correlativos. IODESA: DOWN conserva IDs actuales pero no recupera IDs originales ya reasignados por un UP sin respaldo.',
    'Revisar cada script, respaldar/probar y autorizar explícitamente. No interpretar inventario de scripts como prueba de ejecución ni como paquete listo para aplicar.',
    ['deliverables/MHS_go_live/README.md','deliverables/IODESA_identity_down/README.md'],['MHS','IODESA'],['desconocida en los entregables'])
add('gobierno','Gobierno pendiente: firma, funcionalidad, rutas y SQL primero',
    'AGENTS pendiente incorpora CKFK para Carolina, reglas por funcionalidad sin identidad de empresa, carpetas habituales sin worktrees automáticos y script concreto antes de DML. Se conserva historia y autoría ajena; no se hace commit/push en esta actualización.',
    'La autorización para documentar no autoriza publicar, mezclar HH/BOF, modificar datos ni adoptar experimentos como política estable.',
    ['AGENTS.md'],['desconocido (regla de trabajo local, no regla de cliente)'],['wms-brain'])

# All pending paths stay in inventory. These case summaries supplement rather
# than silently promote unreviewed files or historical claims to current facts.
manifest={}
brain_head=next(x['head'] for x in inventory['repositories'] if x['id']=='brain')
for t in topics:
    for raw in t['sources']:
        p=ROOT/raw
        if not p.is_file():raise FileNotFoundError(raw)
        manifest[raw]={'path':raw,'commit':None,'sha256':hashlib.sha256(p.read_bytes()).hexdigest(),
            'content_state':'local-snapshot-not-attributed','checkout_commit':brain_head}

# Read current product bytes only for this bounded list. Do not copy contents.
product=[]
for rid in ['hh','bof','dba']:
    repo=next(x for x in inventory['repositories'] if x['id']==rid)
    paths=[x['path'] for x in repo['entries'] if x['state']!='??']
    if rid=='bof':paths=['TOMIMSV4/DAL/Transacciones/Recepcion/Recepcion_Encabezado/clsLnTrans_re_enc_Partial.vb']
    if rid=='dba':paths += ['20261117_recuperar_producto_recepcion_28335.sql']
    for raw in paths:
        p=Path(repo['path'])/raw
        if not p.is_file():continue
        product.append({'repository':rid,'path':raw,'commit':None,'checkout_commit':repo['head'],
            'sha256':hashlib.sha256(p.read_bytes()).hexdigest(),
            'content_state':'committed-at-clean-checkout' if rid=='bof' else 'pending-snapshot',
            'client_scope':'not-inferred-from-file-name'})

(OUT/'topics.json').write_text(json.dumps(topics,ensure_ascii=False,indent=2),'utf-8')
(OUT/'sources.json').write_text(json.dumps({'tag':'#CKFK20261117_BRAIN_PENDIENTES','knowledge_sources':list(manifest.values()),'product_sources':product},ensure_ascii=False,indent=2),'utf-8')
claims=[]
for t in topics:
    claims.append({'id':'CKFK20261117-PENDING-'+t['id'].upper(),'kind':'fact',
        'statement':'La evidencia local documenta: '+t['summary']+' Limite: '+t['limits'],
        'sources':[manifest[s] for s in t['sources']], 'clients':t['clients'],'branches':t['branches'],
        'validated_at':None,'status':'draft','evidence_level':t['review']})
(OUT/'claims.json').write_text(json.dumps(claims,ensure_ascii=False,indent=2),'utf-8')

lines=['---','title: "CKFK: todo el trabajo local pendiente de commit"','kind: fact','clients: []',
       'branches: [wms-brain, dev_2026_estable, codex/mhs-despacho-directo-picking, main]',
       'status: draft','validated_at: null','---','',
       '# Trabajo local pendiente de commit: consolidación CKFK','',
       '<!-- #CKFK20261117_BRAIN_PENDIENTES -->','',
       'Solicitud de Carolina: incorporar todo lo trabajado y aprendido pendiente de commit, no solo recepción 28335. Corte local: 2026-11-17. No se hicieron commits, push, cambios de rama, movimientos de archivos fuente ni cambios de datos durante esta consolidación.','',
       'Se inventariaron **siete repositorios conocidos**. El inventario es exhaustivo de las entradas que Git reportó en ese corte; las síntesis siguientes cubren la evidencia local consultada. Un archivo sin seguimiento no acredita autoría, validación, vigencia ni autorización para incluirlo en un commit. Los archivos nuevos creados por esta consolidación son una adición posterior al corte.','',
       'La firma **CKFK** identifica esta consolidación solicitada por Carolina. No se reescribieron firmas históricas ni se atribuyeron a Carolina cambios ajenos. Las notas que dicen «sin commit» describen su momento original; el estado actual se toma del inventario, sin confundirlo con despliegue.','',
       '## Estado por repositorio','',
       '| Repositorio | Rama / estado | HEAD del corte | Entradas pendientes | Modificadas/versionadas |','|---|---|---|---:|---:|']
for r in inventory['repositories']:
    if 'head' in r:lines.append(f"| {r['id']} | `{r['branch'] or 'HEAD separado'}` | `{r['head']}` | {r['count']} | {r['tracked_pending']} |")
lines += ['', 'BOF ya está limpio en este corte. Eso no elimina las notas o evidencias aún sin commit en Brain ni demuestra que los binarios se publicaron. HH conserva 9 archivos versionados modificados; DBA 2, además de los scripts sin seguimiento. Los clones históricos de db-brain y brain-client están en HEAD separado y limpios. Exchange conserva un registro histórico sin seguimiento; no se usó como transporte.','',
          '## Cómo se conservó todo el alcance','',
          '- [Inventario completo por archivo](../../../analysis/20261117-brain-pending/inventory.json): incluye código, evidencia, entregables, cachés, dependencias y rutas privadas identificadas sin leer sus cuerpos.',
          '- [Cobertura de cada grupo](cobertura.md): todos los grupos tienen un destino documental o una clasificación explícita de revisión pendiente.',
          '- [Catálogo de temas](../../../analysis/20261117-brain-pending/topics.json), [fuentes y hashes](../../../analysis/20261117-brain-pending/sources.json) y [afirmaciones draft](../../../analysis/20261117-brain-pending/claims.json).',
          '- [Índice de afirmaciones históricas pendientes](../../../analysis/20261117-brain-pending/historical-claims-index.json): 65 declaraciones en 31 documentos, conservando sus ID, alcance, estado declarado y límites. Indexar una afirmación anterior no vuelve a validarla.',
          '- Dependencias, respaldos, volcados de error, compilaciones y documentos binarios quedan inventariados; no se promueven a reglas de producto ni se prepara un commit masivo con ellos. Credenciales/configuraciones privadas no se copiaron. No se borró ni movió nada.',
          '- Grupos sin informe suficiente quedan como **registrados, no interpretados**. Sus nombres no se usan para inventar cliente, rama, causa, ejecución de un script o cierre.',
          '', '## Cambios actuales de HH adicionales a los incidentes','',
          'El diff revisado incorpora versión visible **8.9.78** en build.gradle y appGlobals, y fecha visible **17-11-2026** en strings.xml; versionCode sigue en 8966. WebService.java (2 mensajes) y frm_consulta_stock.java (3 mensajes) cambian «internet» por «red», sin cambios de conectividad en esos hunks. Esto describe el código pendiente, no certifica un APK distribuido con esa versión.','',
          'También están registrados CantidadCambioEstado.java, dialog_editar_lote_vencimiento.xml, automation/run-test-hh.ps1 y RFIDAPI3Library, además de logs de error. No se asume que todo archivo nuevo de HH sea parte del cambio de recepción. El script histórico de diagnóstico bajo HH requiere revisión de ubicación conforme al gobierno del Brain; no se movió automáticamente.','',
          '## Otros materiales pendientes conservados','',
          'La cobertura por archivo conserva también los artefactos de verificación por unidad B&B, comparación de ramas estables, calendario MHS, evidencias XML de despacho/packing, scripts de alias/destino MHS del 12 de noviembre, trazas de stock de La Cumbre, diagramas MI3/Mercosal, herramientas de unificación local, release notes y documentos de rollback. Cuando solo hay scripts, capturas, datos o binarios y no un informe con alcance suficiente, el registro se limita a su existencia: no inventa un diagnóstico, prueba aprobada, ejecución SQL o cierre. ROAD conserva su carpeta separada y no se mezcla con reglas de TOMWMS.','',
          'Los archivos pendientes de herramientas brain-quality incluyen diagnóstico de decimales/presentaciones, reapertura de recepción, packing, laboratorios y validadores locales. Siguen enlazados desde el inventario. No se cambia el método de recuperación predeterminado, no se promueven experimentos ni se reejecutan indexaciones de modelos por documentar estas herramientas.','',
          '## Temas y aprendizajes recuperados','']
for t in topics:
    lines += [f"### {t['title']}",'',t['summary'],'', '**Validación y límites:** '+t['limits'],'',
              '**Alcance declarado en las fuentes:** '+', '.join(t['clients'])+'; ramas: '+', '.join('`'+x+'`' for x in t['branches'])+'.','',
              '**Fuentes:** '+', '.join(f'[{Path(s).name}](../../../{s})' for s in t['sources'])+'.','']
lines += ['## Política para revisar y publicar después','',
          'Separar commits de Brain, HH, BOF y DBA. Conservar los cambios ajenos. Revisar la categoría de cada archivo antes de staging; este registro no decide automáticamente qué se publicará. No ejecutar scripts SQL como efecto de registrar su existencia. No sustituir una autorización explícita por la confirmación de otro caso.','',
          'Todos los nuevos registros permanecen draft y con validated_at nulo. El cierre de 28335 está confirmado por la operadora; el resto conserva los límites de sus propias fuentes. Los informes históricos pueden tener hashes de fuentes hoy cambiadas: no reinterpretarlos como prueba de los bytes actuales ni invalidar su historia reescribiéndola. El manifiesto nuevo conserva el hash de cada informe consultado y los HEAD actuales de código por separado.','',
          'No se volvió a ejecutar ninguna batería de producto ni se consultó producción para escribir esta síntesis. La verificación de esta actualización consiste en procedencia, cobertura, enlaces y recuperación local del catálogo.']
(DEST/'README.md').write_text('\n'.join(lines)+'\n','utf-8')

coverage=['# Cobertura completa del corte local','', '<!-- #CKFK20261117_BRAIN_COBERTURA -->','',
          'Cada entrada está en inventory.json. Esta tabla agrupa rutas para navegación, no convierte nombres de carpetas en hechos operativos. Las carpetas auxiliares no se eliminaron.','',
          '| Repositorio / grupo | Entradas | Tratamiento |','|---|---:|---|']
coverage_data=[]
prefixes={}
for t in topics:
    for s in t['sources']:
        parts=s.split('/'); prefix='/'.join(parts[:2]) if len(parts)>2 else s
        prefixes.setdefault(prefix,set()).add(t['id'])
for repo in inventory['repositories']:
    groups={}
    for e in repo.get('entries',[]):
        p=e['path']; parts=p.split('/'); key='/'.join(parts[:2]) if len(parts)>2 else p
        groups.setdefault(key,[]).append(e)
    for group,entries in sorted(groups.items()):
        categories=Counter(x['category'] for x in entries)
        linked=sorted(prefixes.get(group,[])) if repo['id']=='brain' else []
        if linked:disposition='Síntesis de fuentes: '+', '.join(linked)+'; demás archivos: evidencia auxiliar registrada'
        elif all(x['category']=='runtime-generated-or-cache' for x in entries):disposition='Dependencias/generados/respaldo: registrados sin validación funcional'
        elif any(x['category']=='private-or-config-content-not-read' for x in entries):disposition='Contenido privado/configuración: no consultado ni copiado'
        elif repo['id'] in {'hh','dba'}:disposition='Cambio o artefacto pendiente: inventariado; no autorizado para publicar/ejecutar por este registro'
        else:disposition='Registrado sin interpretación funcional; revisar fuentes antes de afirmar causa, vigencia o ejecución'
        coverage.append(f"| {repo['id']}: `{group}` | {len(entries)} | {disposition} |")
        coverage_data.append({'repository':repo['id'],'group':group,'count':len(entries),'categories':dict(categories),'topics':linked,'disposition':disposition})
(DEST/'cobertura.md').write_text('\n'.join(coverage)+'\n','utf-8')
(OUT/'coverage.json').write_text(json.dumps(coverage_data,ensure_ascii=False,indent=2),'utf-8')
print(json.dumps({'topics':len(topics),'knowledge_sources':len(manifest),'product_sources':len(product),
                  'inventoried_entries':sum(r.get('count',0) for r in inventory['repositories']),
                  'coverage_entries':sum(x['count'] for x in coverage_data),'groups':len(coverage_data)},ensure_ascii=False))
