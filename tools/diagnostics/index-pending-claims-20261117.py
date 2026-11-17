"""#CKFK20261117_BRAIN_AFIRMACIONES: preserve pending historical assertions as declarations."""
from pathlib import Path
import json,hashlib,re

root=Path(__file__).resolve().parents[2]
out=root/'analysis/20261117-brain-pending'
inv=json.loads((out/'inventory.json').read_text('utf-8'))
repo=next(r for r in inv['repositories'] if r['id']=='brain')
indexed=[];omissions=[]
def walk(data):
    if isinstance(data,list):
        for v in data:yield from walk(v)
    elif isinstance(data,dict):
        if isinstance(data.get('statement'),str):yield data
        else:
            for v in data.values():
                if isinstance(v,(list,dict)):yield from walk(v)
for entry in repo['entries']:
    raw=entry['path'];p=root/raw
    if entry['category']!='knowledge-evidence-or-log' or not raw.endswith('.json'):continue
    if not any(word in p.name.lower() for word in ['claim','afirmacion','provenance']):continue
    if any(part in {'baseline','tests','bof-byb','hh','bof','evidencia','evidence','runtime','node_modules'} for part in p.relative_to(root).parts):continue
    if raw.startswith('analysis/20261117-brain-pending/'):continue
    try:
        if p.stat().st_size>2_000_000:
            omissions.append({'path':raw,'reason':'size; file remains in inventory'});continue
        blob=p.read_bytes();data=json.loads(blob.decode('utf-8-sig'))
        rows=list(walk(data))
        if not rows:continue
        source={'path':raw,'commit':None,'sha256':hashlib.sha256(blob).hexdigest(),
                'checkout_commit':repo['head'],'content_state':'historical-local-declaration'}
        kept=[]
        for row in rows:
            text=row['statement']
            if re.search(r'(?i)(password\s*=|clave_bd\s*=|contrase[nñ]a\s*[:=]|user\s*id\s*=|bearer\s+)',text):
                omissions.append({'path':raw,'id':row.get('id'),'reason':'potential sensitive assertion content; not copied'});continue
            kept.append({k:row[k] for k in ['id','kind','statement','clients','branches','status','validated_at','supersedes','limitations'] if k in row})
        indexed.append({'source':source,'review_status':'declarations-indexed-not-revalidated',
                        'declared_claims':kept})
    except (OSError,UnicodeError,ValueError) as e:
        omissions.append({'path':raw,'reason':type(e).__name__})
result={'tag':'#CKFK20261117_BRAIN_AFIRMACIONES','status':'draft',
        'policy':'Preserve declared scope/status as historical source data. Missing clients/branches remain unknown. No operational claim independently verified by this index.',
        'documents':indexed,'omissions':omissions}
(out/'historical-claims-index.json').write_text(json.dumps(result,ensure_ascii=False,indent=2),'utf-8')
print(json.dumps({'documents':len(indexed),'declared_claims':sum(len(x['declared_claims']) for x in indexed),'omissions':len(omissions)}))
