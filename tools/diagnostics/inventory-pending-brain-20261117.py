"""#CKFK20261117_BRAIN_PENDIENTES: read-only Git inventory, no source bodies or secrets."""
from pathlib import Path
import subprocess, json, hashlib, collections

HOME = Path('C:/Users/carol')
BRAIN = HOME/'Documents/wms-brain'
OUT = BRAIN/'analysis/20261117-brain-pending'
EXCHANGE = HOME/'Documents/Codex/2026-06-12/clona-este-repositorio-contextualiza-y-analiza'
REPOS = [('brain', BRAIN), ('hh', HOME/'StudioProjects/TOMHH2025'),
         ('bof', HOME/'source/repos/TOMWMS_BOF'), ('dba', HOME/'source/repos/DBA'),
         ('exchange', EXCHANGE/'tomwms-replit-client-automate-fresh'),
         ('db-brain', EXCHANGE/'tomwms-db-brain'), ('brain-client', EXCHANGE/'tomwms-wms-brain-client')]

def git(repo, *args):
    return subprocess.run(['git','-c',f'safe.directory={repo.as_posix()}', '-C',str(repo),*args],
                          capture_output=True, check=True).stdout

def category(path):
    parts=path.split('/')
    if any(p in {'.tmp','.codex-tmp','.codex-build','.codex-finalizer','node_modules','__pycache__','bin','obj','build','runtime','baseline','tests'} for p in parts):
        return 'runtime-generated-or-cache'
    if path.lower().endswith(('.ini','.config','.env','.keystore','.jks','.pfx','.pem')) or 'credential' in path.lower():
        return 'private-or-config-content-not-read'
    if path.lower().endswith(('.rar','.zip','.dll','.exe','.apk','.jar','.aar','.docx','.pptx','.pdf','.png','.jpg','.mp4','.hprof','.dmp')):
        return 'binary-or-deliverable'
    if path.lower().endswith(('.java','.vb','.cs','.xml','.gradle','.sql','.ps1','.py','.js','.mjs','.cjs','.ts','.tsx')):
        return 'source-script-or-query'
    if path.lower().endswith(('.md','.yml','.yaml','.json','.txt','.log','.csv','.tsv')):
        return 'knowledge-evidence-or-log'
    return 'other'

reports=[]
for label,repo in REPOS:
    if not repo.exists():
        reports.append({'id':label,'path':str(repo),'state':'missing'});continue
    try:
        raw=git(repo,'status','--porcelain=v1','-z','--untracked-files=all').decode('utf-8').split('\0')
        rows=[];i=0
        while i<len(raw):
            item=raw[i];i+=1
            if not item:continue
            state,path=item[:2],item[3:]
            old=None
            if 'R' in state or 'C' in state:
                old=raw[i];i+=1
            row={'state':state,'path':path,'category':category(path),'content_review':'not-reviewed'}
            if old:row['old_path']=old
            # Inventory names only. Hash the selected evidence separately; avoid
            # opening private files, external symlink targets or cloud placeholders.
            rows.append(row)
        report={'id':label,'path':str(repo),'branch':git(repo,'branch','--show-current').decode().strip(),
                'head':git(repo,'rev-parse','HEAD').decode().strip(),'entries':rows,
                'count':len(rows),'categories':dict(collections.Counter(r['category'] for r in rows)),
                'tracked_pending':sum(r['state']!='??' for r in rows),
                'recent_commits':git(repo,'log','-5','--format=%h %s').decode('utf-8').splitlines()}
        reports.append(report)
        print(json.dumps({k:report[k] for k in ['id','branch','count','tracked_pending','categories']},ensure_ascii=False),flush=True)
    except subprocess.CalledProcessError:reports.append({'id':label,'path':str(repo),'state':'git-unavailable'})
OUT.mkdir(parents=True,exist_ok=True)
result={'tag':'#CKFK20261117_BRAIN_PENDIENTES','snapshot_date':'2026-11-17','scope':'seven known local repositories; pending Git entries at start of inventory; no remote queries', 'repositories':reports}
(OUT/'inventory.json').write_text(json.dumps(result,ensure_ascii=False,indent=2),'utf-8')
for r in reports:
    print(json.dumps({k:r[k] for k in ['id','branch','head','count','tracked_pending','categories','state'] if k in r},ensure_ascii=False))
    if r.get('id') in {'hh','bof','dba'}:
        for f in r.get('entries',[]):print('  '+f['state']+' '+f['path'])
    if r.get('id')=='brain':
        groups=collections.Counter('/'.join(x['path'].split('/')[:2]) for x in r['entries'])
        print('BRAIN GROUPS '+json.dumps(groups.most_common(),ensure_ascii=False))
