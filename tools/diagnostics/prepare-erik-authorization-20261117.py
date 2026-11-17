"""#CKFK20261117_ERIK_AUTORIZACION: exact review manifest; never stage/commit/push."""
from pathlib import Path
import subprocess,json,re,hashlib,csv,collections

ROOT=Path(__file__).resolve().parents[2]
OUT=ROOT/'analysis/20261117-autorizacion-erik'
OUT.mkdir(exist_ok=True)
REPOS={'Brain':ROOT,'HH':Path('C:/Users/carol/StudioProjects/TOMHH2025'),
       'BOF':Path('C:/Users/carol/source/repos/TOMWMS_BOF'),'DBA':Path('C:/Users/carol/source/repos/DBA')}

def git(repo,*args,check=True):
    return subprocess.run(['git','-c',f'safe.directory={repo.as_posix()}','-C',str(repo),*args],capture_output=True,check=check)
def gs(repo,*args):return git(repo,*args,check=False).stdout.decode('utf-8',errors='replace').strip()

hh={
'app/src/main/java/com/dts/classes/Transacciones/OrdenCompra/Trans_oc_det/clsBeTrans_oc_det.java':('HH-1','Permite clonar el detalle OC para que 4/3 conserve dos cantidades independientes.','Recepción: replay del fragmento real; compilación Android; cierre 28335 comunicado por CKFK.'),
'app/src/main/java/com/dts/tom/Transacciones/Recepcion/frm_list_rec_prod.java':('HH-1','Construye copias por lote, calcula saldo, omite completos y valida identidad funcional.','Replay 4/3 y 3/4, OC intacta, saldo 1, completos y variante incorrecta.'),
'app/src/main/java/com/dts/tom/Transacciones/Recepcion/frm_recepcion_datos.java':('HH-1','Limpia la lista y entrega temporales negativos distintos para detalle y stock_rec.','Compilación y pruebas de identidad/cantidades en el servicio con datos en memoria.'),
'app/src/main/java/com/dts/classes/Mantenimientos/Bodega/clsBeBodega.java':('HH-1','Agrega permitir_abrir_recepcion_enviada como XML opcional false y sus accesores.','Parser real sobre clase compilada: true, false y campo ausente.'),
'app/src/main/java/com/dts/base/WebService.java':('HH-2','Dos mensajes sustituyen internet por red; no cambia la lógica de conectividad en el diff.','Revisión de diff y compilación; sin nueva prueba de red.'),
'app/src/main/java/com/dts/tom/Transacciones/ConsultaStock/frm_consulta_stock.java':('HH-2','Tres mensajes sustituyen internet por red.','Revisión de diff y compilación; sin cambiar predicados ni callbacks.'),
'app/build.gradle':('HH-3','versionName 8.9.77 → 8.9.78; versionCode permanece 8966.','assembleDebug del estado actual; no publicación de APK release.'),
'app/src/main/java/com/dts/base/appGlobals.java':('HH-3','Versión mostrada 8.9.77 → 8.9.78.','Revisión del campo version y compilación actual.'),
'app/src/main/res/values/strings.xml':('HH-3','Fecha visible 16-11-2026 → 17-11-2026.','Compilación/paquetizado de recursos Android.')}

brain_seeds={
'AGENTS.md','brain/_index/ATLAS.md',
'brain/handoffs/2026-11-17-ckfk-pendientes/README.md',
'brain/handoffs/2026-11-17-ckfk-pendientes/cobertura.md',
'brain/handoffs/2026-11-17-ckfk-recepcion-28335/README.md',
'brain/learnings/CKFK20261117-caja-master-saldos-identidad.md',
'brain/handoffs/2026-05-22-codex-performance-bof-hh/PACKING-HH-GRAFO-TRAZA-FINA-2026-05-26.yml',
'brain/handoffs/2026-05-22-codex-performance-bof-hh/PICKING-HH-GRAFO-TRAZA-FINA-2026-05-26.yml'}
sources=json.loads((ROOT/'analysis/20261117-brain-pending/sources.json').read_text('utf-8'))
brain_seeds.update(s['path'] for s in sources['knowledge_sources'])
hist=json.loads((ROOT/'analysis/20261117-brain-pending/historical-claims-index.json').read_text('utf-8'))
brain_seeds.update(row['source']['path'] for row in hist['documents'])
for p in (ROOT/'analysis/20261117-brain-pending').iterdir():
    if p.is_file() and p.suffix in {'.json','.log'}:brain_seeds.add(p.relative_to(ROOT).as_posix())
case_files=['diagnostico.md','entrega.md','confirmacion-ckfk.md','resumen-evidencia.json',
            'fix-manifest.json','ReplayCajaMaster.java','replay.txt','regression.log',
            'TestBodegaXml.java','bodega-xml-test.txt','hh-build.log','hh-apk-build.log',
            'hh-bodega-compat-build.log','bof-build.log','sql-parse.json','recovery-guards.json',
            'recovery-guards-current.json','recovery-guard-selects.sql','diagnostico.sql']
brain_seeds.update('analysis/20261117-recepcion-28335/'+p for p in case_files)
for name in ['read-recepcion-28335.ps1','test-recepcion-28335.py','test-recepcion-28335-template.vb',
             'inventory-pending-brain-20261117.py','consolidate-pending-brain-20261117.py',
             'index-pending-claims-20261117.py','prepare-erik-authorization-20261117.py']:
    brain_seeds.add('tools/diagnostics/'+name)
# Include pending textual Markdown link targets, not binary packages or broad raw dumps.
queue=list(brain_seeds)
while queue:
    raw=queue.pop();p=ROOT/raw
    if not p.is_file() or p.suffix!='.md':continue
    for ref in re.findall(r'\]\(([^)]+)\)',p.read_text('utf-8-sig',errors='replace')):
        if re.match(r'\w+://',ref):continue
        q=(p.parent/ref.split('#')[0]).resolve()
        if not q.is_relative_to(ROOT) or not q.is_file() or q.suffix not in {'.md','.txt','.json','.yml','.yaml'}:continue
        rel=q.relative_to(ROOT).as_posix()
        if rel not in brain_seeds:brain_seeds.add(rel);queue.append(rel)

def status(repo):
    parts=git(repo,'status','--porcelain=v1','-z','--untracked-files=all').stdout.decode('utf-8').split('\0')
    rows=[];i=0
    while i<len(parts):
        s=parts[i];i+=1
        if not s:continue
        row={'state':s[:2],'path':s[3:]}
        if any(c in s[:2] for c in 'RC'):row['old_path']=parts[i];i+=1
        rows.append(row)
    return rows

def describe_brain(raw):
    if raw=='AGENTS.md':return 'Firma CKFK; reglas por funcionalidad; carpetas habituales; script SQL antes de DML.'
    if raw=='brain/_index/ATLAS.md':return 'Enlaces de navegación al catálogo de pendientes y cierre 28335.'
    if '20260728-verificacion-despacho-parcial' in raw:return 'Adición documentada de despacho parcial y control de packing.'
    if 'traza-003-sapsyncmampa-interface' in raw:return 'Referencia a la instrumentación y omisión por factura de reserva sin bodega.'
    if 'PACKING-HH-GRAFO-TRAZA-FINA' in raw:return 'Nota CKFK20261020: filtro por producto conserva resumen de LP packing y separa LP de stock/del agrupador; antecedente, no prueba operativa nueva.'
    if 'PICKING-HH-GRAFO-TRAZA-FINA' in raw:return 'Nota CKFK20261020: cierre por servicio y reconciliación DAL, nunca solo por lista vacía en HH; antecedente, no validación nueva.'
    if raw.startswith('brain/handoffs/2026-11-17-ckfk-pendientes/'):return 'Catálogo de 33 temas y cobertura exacta del inventario local.'
    if raw.startswith('brain/handoffs/2026-11-17-ckfk-recepcion-28335/'):return 'Causa, correcciones, conciliación y cierre confirmado por CKFK; límites explícitos.'
    if raw.startswith('brain/learnings/CKFK20261117'):return 'Criterios aprendidos: copia, saldo, identidad de lote y compatibilidad XML.'
    if raw.startswith('analysis/20261117-brain-pending/'):return 'Inventario, fuentes, declaraciones históricas o comprobaciones de la consolidación.'
    if raw.startswith('analysis/20261117-recepcion-28335/'):return 'Evidencia acotada, pruebas o instrucciones del caso 28335; no acredita DML del agente.'
    if raw.startswith('tools/diagnostics/'):return 'Herramienta reproducible del Brain; no ejecutada para modificar datos en esta solicitud.'
    return 'Informe o declaración histórica que respalda un tema del catálogo; conserva su alcance y pendientes.'

dba_titles={
'bodega_asociacion_virtual':'Ajustes del script existente de asociaciones/constraints; revisión DBA pendiente.',
'usp_wms_principal02_tareas_readmodel_v1':'Condición adicional de bodega al relacionar ubicación en el readmodel; antecedente SELECT, sin ALTER aplicado por este trabajo.',
'20260803_mhs_espacios_virtuales_capacidad_tramo.sql':'Espacios virtuales y capacidad de tramo.',
'20260826_mampa_i_nav_ejecucion_det_error_talla_color.down.sql':'Reversa de campos de error/talla-color en ejecución de interfaz.',
'20260826_mampa_i_nav_ejecucion_det_error_talla_color.sql':'Campos de error/talla-color en ejecución de interfaz.',
'20260930_mhs_ubicacion_destino_picking.rollback.sql':'Rollback de ubicación destino de picking.',
'20260930_mhs_ubicacion_destino_picking.sql':'Ubicación destino de picking.',
'20260930_mhs_ubicacion_destino_picking.validacion.sql':'Validación de ubicación destino de picking.',
'20261001_agregar_destino_picking_dos_tablas.sql':'Campos de destino de picking en dos tablas.',
'20261012_crear_dos_tipos_salida_mhs.sql':'Tipos de salida: revisar parámetros y alcance; no activar reglas por empresa.',
'20261019_mhs_corregir_lote_licencia_preimpresion.sql':'Conciliación de lote/LP de preimpresión.',
'20261023_01_cumbre_trans_ajuste_det_origen.sql':'Origen de detalle de ajuste.',
'20261023_02_cumbre_trans_ajuste_det_borrador_origen.sql':'Origen de detalle borrador de ajuste.',
'20261023_03_cumbre_ajustes_origen_validacion.sql':'Validación de origen en ajustes.',
'20261023_cumbre_recuperar_origen_ajuste_1270.sql':'Recuperación específica del origen del ajuste 1270.',
'20261023_cumbre_reporte_ingresos_ubicaciones.rollback.sql':'Rollback del reporte de ingresos/ubicaciones.',
'20261023_cumbre_reporte_ingresos_ubicaciones.sql':'Reporte de ingresos/ubicaciones.',
'20261027_ckfk_killios_cajas_y_un_LEEME.md':'Instrucciones de revisión de cajas y unidades.',
'20261027_ckfk_killios_consultar_presentacion_no_multiplo.sql':'Consulta de presentación no múltiplo.',
'20261027_ckfk_killios_consultar_un_decimales.sql':'Consulta de unidades decimales.',
'20261027_ckfk_killios_correcciones_LEEME.md':'Alcance e instrucciones de correcciones de inventario.',
'20261027_ckfk_killios_corregir_decimales_wms272.sql':'Conciliación específica de decimales WMS272.',
'20261027_ckfk_killios_corregir_presentaciones_cajas_y_un.sql':'Conciliación de presentaciones de cajas/unidades.',
'20261027_ckfk_killios_corregir_presentaciones_no_multiplos.sql':'Conciliación de presentaciones no múltiplos.',
'20261027_ckfk_killios_corregir_sobrante3_wms282.sql':'Propuesta específica del sobrante documental de tres UN WMS282.',
'20261027_ckfk_killios_presentaciones_LEEME.md':'Instrucciones y límites de corrección de presentaciones.',
'20261028_ckfk_killios_aceite_buen_estado_LEEME.md':'Instrucciones de conciliación de aceite en buen estado.',
'20261028_ckfk_killios_aceite_menos3_buen_estado.sql':'Propuesta específica de ajuste de tres UN de aceite.',
'20261030_mhs_pedido_confirmar_packing_vista.sql':'Vista de confirmación de pedido/packing.',
'20261117_recuperar_producto_recepcion_28335.sql':'Concilia cinco filas sin sumar unidades; deja una para HH. Recuperación ya confirmada por CKFK; conservar como histórico, NO reejecutar.'}

states={};proposed=[];held=[];existing_dependencies=[]
for name,repo in REPOS.items():
    branch=gs(repo,'branch','--show-current');up=gs(repo,'rev-parse','--abbrev-ref','--symbolic-full-name','@{upstream}')
    remote=re.sub(r'(?<=://)[^/@]+@','',gs(repo,'remote','get-url','origin'))
    rows=status(repo);pending={r['path']:r for r in rows}
    states[name]={'path':str(repo),'branch':branch,'head':gs(repo,'rev-parse','HEAD'),'upstream':up or None,
                  'upstream_commit_cached':gs(repo,'rev-parse',up) if up else None,
                  'push_target_proposed':up or 'origin/'+branch,'origin_sanitized':remote,
                  'cached_behind_ahead':gs(repo,'rev-list','--left-right','--count',up+'...HEAD') if up else None,
                  'staged_paths_before':gs(repo,'diff','--cached','--name-only').splitlines(),
                  'pending_count':len(rows)}
    if name=='Brain':
        for p in sorted(brain_seeds):
            if p not in pending:existing_dependencies.append({'repository':name,'path':p,'state':'already-tracked-or-not-pending'});continue
            proposed.append({'repository':name,'batch':'BRAIN-1','path':p,'state':pending[p]['state'],
                             'change':describe_brain(p),'tests':'33 registros de procedencia, enlaces, cobertura y búsqueda local; informes históricos conservan sus propias limitaciones.'})
    elif name=='HH':
        for p,(batch,change,tests) in hh.items():
            if p not in pending:raise RuntimeError('HH selection drift: '+p)
            proposed.append({'repository':name,'batch':batch,'path':p,'state':pending[p]['state'],'change':change,'tests':tests})
    elif name=='DBA':
        p='20261117_recuperar_producto_recepcion_28335.sql'
        if p not in pending:raise RuntimeError('DBA selection drift')
        proposed.append({'repository':name,'batch':'DBA-1','path':p,'state':pending[p]['state'],
                         'change':dba_titles[p],'tests':'ScriptDom: 0 errores. 11 precondiciones contrastadas ANTES de recuperación. CKFK confirmó el cierre; no prueba posterior SQL del agente.'})
    selected={p['path'] for p in proposed if p['repository']==name}
    for row in rows:
        if row['path'] in selected:continue
        p=row['path'];why='Fuera del alcance propuesto; conserva inventario y requiere selección/revisión separada.'
        if any(x in p.split('/') for x in ['node_modules','build','bin','obj','runtime','.codex-build','.codex-tmp','.tmp','.codex-finalizer','baseline','tests']):why='Dependencia, generado, caché o respaldo; no proponer en este paquete.'
        elif re.search(r'(?i)(secrets|credential|\.env$|\.ini$|\.config$|\.keystore$)',p):why='Privado/configuración: no incluir ni copiar su contenido.'
        elif re.search(r'(?i)(hs_err|replay_pid|\.rar$|\.apk$|\.dll$|\.exe$)',p):why='Volcado/binario/paquete local; fuera del commit de código/documentación propuesto.'
        elif name=='HH':why='Auxiliar histórico no integrado en este diff; sin referencias Java observadas para el helper/layout, o automatización local. Revisión separada.'
        elif name=='DBA':why='Lote histórico independiente: faltan revisión y autorización específicas. No solicitar ejecución por incluirlo en inventario.'
        elif row['state']!='??':
            d=gs(repo,'diff','--',p);lines=[x[1:] for x in d.splitlines() if x.startswith(('+','-')) and not x.startswith(('+++','---'))]
            if not any(x.strip() for x in lines):why='Solo línea vacía/formato; no mezclar con los cambios sustantivos.'
        held.append({'repository':name,'path':p,'state':row['state'],'reason':why,
                     'change':dba_titles.get(p,'No se atribuye un cambio funcional sin revisión de contenido.')})

suspects=[]
for row in proposed:
    p=REPOS[row['repository']]/row['path'];blob=p.read_bytes()
    row['sha256']=hashlib.sha256(blob).hexdigest();row['bytes']=len(blob)
    row['branch']=states[row['repository']]['branch'];row['push_target_proposed']=states[row['repository']]['push_target_proposed']
    # High-confidence literal credentials only; do not print matching content.
    text=blob.decode('utf-8-sig',errors='replace')
    for n,line in enumerate(text.splitlines(),1):
        if re.search(r'''(?i)(?:password|pwd|clave_bd_wms)\s*=\s*['"](?!\$|<|\{)[^'"\r\n]{5,}['"]''',line):
            suspects.append({'repository':row['repository'],'path':row['path'],'line':n,'rule':'literal-secret-assignment-review'})

brain=REPOS['Brain'];up=states['Brain']['upstream']
commits=[]
for line in gs(brain,'log','--reverse','--format=%H%x09%s',up+'..HEAD').splitlines():
    sha,subject=line.split('\t',1)
    files=gs(brain,'diff-tree','--no-commit-id','--name-status','-r',sha).splitlines()
    commits.append({'commit':sha,'subject':subject,'files_against_parent':files})
net=gs(brain,'diff','--name-status',up,'HEAD').splitlines()
manifest={'tag':'#CKFK20261117_ERIK_AUTORIZACION','date':'2026-11-17','requester':'CKFK',
          'purpose':'Review/authorization only. No stage, commit, push, branch switch, deployment or SQL execution.',
          'remote_comparison':'local cached remote-tracking refs only; no fetch or live remote verification',
          'repositories':states,'proposed_files':proposed,'held_files':held,'unchanged_dependencies':existing_dependencies,
          'brain_preexisting_outgoing_commits_cached':commits,'brain_net_paths_HEAD_vs_cached_upstream':net,
          'literal_secret_scan_candidates':suspects,
          'generated_review_files_not_in_publication_scope':['analysis/20261117-autorizacion-erik/README.md','analysis/20261117-autorizacion-erik/manifest.json','analysis/20261117-autorizacion-erik/archivos-propuestos.tsv','analysis/20261117-autorizacion-erik/archivos-fuera.tsv']}
(OUT/'manifest.json').write_text(json.dumps(manifest,ensure_ascii=False,indent=2),'utf-8')
for filename,rows,fields in [('archivos-propuestos.tsv',proposed,['repository','branch','push_target_proposed','batch','state','path','change','tests','sha256','bytes']),
                             ('archivos-fuera.tsv',held,['repository','state','path','change','reason'])]:
    with (OUT/filename).open('w',encoding='utf-8-sig',newline='') as f:
        w=csv.DictWriter(f,fieldnames=fields,delimiter='\t',extrasaction='ignore');w.writeheader();w.writerows(rows)

count=collections.Counter(r['repository'] for r in proposed)
lines=['# Solicitud de autorización a Erik — CKFK','',
       '<!-- #CKFK20261117_ERIK_AUTORIZACION -->','',
       'Corte: 17 de noviembre de 2026. Propuesta para revisión; **sin staging, commit ni push realizados**. No solicita desplegar ni ejecutar SQL. No se cambió ninguna rama.','',
       '## Alcance concreto propuesto','',
       f"- Brain: **{count['Brain']} archivos pendientes**, documentación y evidencia seleccionada de la consolidación completa (33 temas/65 declaraciones históricas), más **{len(commits)} commits anteriores de historia** que un push de esta rama también alcanzaría según la referencia local.",
       '- HH: **9 archivos versionados**, en tres commits sugeridos: recepción/compatibilidad XML; mensajes de red; versión/fecha.',
       '- DBA: **1 script histórico del caso 28335**. Versionarlo no autoriza reejecutarlo: la recuperación ya fue confirmada por CKFK.',
       '- BOF: **0 archivos nuevos para commit y 0 commits por delante de su referencia local**. La corrección del servicio ya está en el HEAD actual; no proponer un commit duplicado.',
       '- Los demás pendientes se enumeran exactamente en archivos-fuera.tsv. No se incluyen automáticamente scripts antiguos, dependencias, binarios, copias de fuentes, configuraciones o cachés.','',
       'La selección es una recomendación para autorización expresa. Si se quiere publicar también otro lote histórico DBA o un auxiliar HH, deberá aprobarse su lista por separado.','',
       '## Repositorios y destinos','',
       '| Repositorio | Carpeta | Rama actual | Destino solicitado | HEAD | Atraso / adelanto local |','|---|---|---|---|---|---|']
for name,s in states.items():
    drift=s['cached_behind_ahead'].replace('\t',' / ') if s['cached_behind_ahead'] else 'sin upstream'
    lines.append(f"| {name} | `{s['path']}` | `{s['branch']}` | `{s['push_target_proposed']}` | `{s['head']}` | {drift} |")
lines += ['', 'Orígenes configurados (sin credenciales en URL):','']
for name,s in states.items():lines.append(f"- {name}: `{s['origin_sanitized']}`.")
lines += ['', '**DBA no tiene upstream configurado.** `origin/'+states['DBA']['branch']+'` es el destino propuesto para que Erik lo apruebe; no se creó una rama remota ni tracking. Las comparaciones son contra referencias locales almacenadas: antes de ejecutar un push autorizado se debe comprobar que origen/rama/HEAD no hayan cambiado. No se hizo fetch en esta preparación.','',
          '## Brain: commits anteriores que también quedarían comprendidos','',
          '**Hay 9 commits de historia local y diferencia neta de archivos entre HEAD y la referencia local de origin: '+str(len(net))+'.** No son nueve cambios de contenido todavía ausentes por necesidad: el árbol actual ya coincide con el upstream almacenado. Sí son commits distintos y deben constar en la autorización de push.','',
          '| Commit completo | Mensaje |','|---|---|']
for c in commits:lines.append(f"| `{c['commit']}` | {c['subject']} |")
for c in commits:
    lines += ['', '### Archivos de '+c['commit'][:8],'']
    if c['files_against_parent']:lines += ['```text',*c['files_against_parent'],'```']
    else:lines.append('Commit de merge/historia; no aporta una lista de cambios por la comparación simple con padre. El delta neto del rango está registrado en manifest.json.')
lines += ['', '## HH: archivos y pruebas exactos','',
          '| Lote sugerido | Archivo | Cambio | Prueba / alcance |','|---|---|---|---|']
for row in proposed:
    if row['repository']=='HH':lines.append(f"| {row['batch']} | `{row['path']}` | {row['change']} | {row['tests']} |")
lines += ['', 'No se proponen CantidadCambioEstado.java, dialog_editar_lote_vencimiento.xml ni RFIDAPI3Library: no se observaron referencias Java al helper o a los IDs del diálogo en esta rama; el AAR RFID usado por app/build.gradle ya está versionado en app/libs. Esta comprobación estática no certifica todos los usos dinámicos. La compilación se ejecutó en el checkout habitual, que conserva esos auxiliares; no se creó un checkout limpio ni se movieron archivos.','',
          '## Pruebas que acompañan la solicitud','',
          '- **Estado HH actual:** assembleDebug --offline satisfactorio, incluido versionName 8.9.78. Evidencia: `analysis/20261117-brain-pending/authorization-hh-build.log`.',
          '- **Recepción:** repetición de los fragmentos reales HH/servidor con fixtures en memoria; 4/3 y 3/4, OC original, saldo 1, completos, identidad y rechazo de exceso, NaN, variantes, LP, PK/temporales duplicados y reintentos obsoletos. Evidencia: `authorization-regression.log`. No simula bloqueo/rollback SQL real.',
          '- **XML bodega actual:** parser real con la clase compilada, campo true/false/ausente. Evidencia: `authorization-bodega-xml.log`.',
          '- **Operación 28335:** cierre comunicado por CKFK en el chat. Sin nueva lectura SQL posterior ni identificación criptográfica de binarios instalados.',
          '- **Brain:** 33 registros de procedencia sin inválidos; 63 enlaces sin faltantes; cobertura de 24,110 entradas/433 grupos del corte anterior; búsqueda BM25 local encuentra cierre y catálogo. Las 65 declaraciones históricas no fueron revalidadas como hechos actuales.',
          '- **DBA-1:** parseo ScriptDom con 0 errores y 11 precondiciones contrastadas ANTES de la recuperación. Son pruebas históricas del estado anterior, no autorización ni condición actual para volver a ejecutar.',
          '- **BOF:** compilación WSHHRN/DAL del cambio de recepción documentada en el caso; no se recompiló ni se modificó BOF para preparar esta lista. No se extiende ese resultado a todos los commits posteriores del repositorio.','',
          '## Brain: lista exacta para el nuevo commit propuesto','',
          'Los hashes, estado Git y tamaño de cada archivo están en archivos-propuestos.tsv y manifest.json. Informes y afirmaciones históricos se conservan con sus límites; no se propone marcar todo como validado ni cambiar sus firmas.','',
          '| Archivo | Cambio / papel en el paquete |','|---|---|']
for row in proposed:
    if row['repository']=='Brain':lines.append(f"| `{row['path']}` | {row['change']} |")
lines += ['', '## DBA: los 30 pendientes, sin ocultar los lotes históricos','',
          'La descripción de los lotes fuera del paquete orienta su revisión por nombre/artefacto; no certifica que estén aplicados ni correctos. Solo DBA-1 está propuesto en esta solicitud.','',
          '| Estado | Archivo exacto | Alcance | Disposición |','|---|---|---|---|']
all_dba=[r for r in proposed if r['repository']=='DBA']+[r for r in held if r['repository']=='DBA']
for row in sorted(all_dba,key=lambda x:x['path']):
    disposition='PROPUESTO DBA-1; histórico, no reejecutar' if row in proposed else 'FUERA; revisión y autorización separadas'
    lines.append(f"| `{row['state']}` | `{row['path']}` | {row['change']} | {disposition} |")
lines += ['', '## Excluidos y comprobaciones de integridad','',
          f"Se conservan **{len(held)} entradas pendientes fuera de la selección**, detalladas por repositorio/ruta/motivo en [archivos-fuera.tsv](archivos-fuera.tsv). No se borraron, descartaron ni movieron. El inventario de siete repositorios anterior también conserva exchange/db-brain/brain-client; no se propone push de esos repositorios históricos.",
          '', 'Los archivos de esta solicitud (README, manifiesto y TSV) son material de revisión local y no amplían automáticamente el lote BRAIN-1. El manifiesto registra los bytes seleccionados; si cambian, hay que actualizar la lista antes de pedir o usar la autorización.',
          '', f"Comprobación de asignaciones literales de credenciales en la selección: **{len(suspects)} candidatos a revisión**. Es una búsqueda acotada, no una garantía de ausencia de secretos ni sustituto de la revisión de Erik. No se copiaron archivos privados de configuración ni definiciones SQL productivas al informe.",
          '', '## Texto para enviar a Erik','',
          f"Erik, solicito tu autorización para preparar commits separados y hacer push de los lotes de este informe: BRAIN-1 ({count['Brain']} archivos), HH-1/HH-2/HH-3 (9 archivos) y DBA-1 (1 script histórico). Las ramas/destinos y los hashes están en el manifiesto. El push de Brain también incluiría los 9 commits de historia local enumerados, aunque el árbol HEAD actual coincida con la referencia local de origin. BOF está limpio y no requiere otro commit/push según esa referencia. Los otros pendientes quedan fuera. Las pruebas actuales de HH/recepción/XML y las del Brain están adjuntas; la recuperación 28335 ya fue confirmada por mí. Esta solicitud no autoriza ejecutar SQL ni desplegar. ¿Autorizas este alcance y el destino propuesto de DBA, que hoy no tiene upstream? — CKFK",
          '', '**Hasta recibir autorización explícita: no staging, commit ni push.**']
(OUT/'README.md').write_text('\n'.join(lines)+'\n','utf-8')
print(json.dumps({'proposed':dict(count),'held':len(held),'brain_existing_commits':len(commits),
                  'brain_net_delta':len(net),'secret_scan_candidates':len(suspects),'report':str(OUT/'README.md')},ensure_ascii=False))
