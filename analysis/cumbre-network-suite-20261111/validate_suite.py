"""Local synthetic checks only. Never contacts customer or production endpoints."""
import csv
import http.server
import json
import os
from pathlib import Path
import shutil
import socket
import subprocess
import threading
import time

ROOT = Path(__file__).resolve().parents[2]
SUITE = ROOT / 'tools' / 'network-diagnostics'
OUT = Path(__file__).resolve().parent


class Handler(http.server.BaseHTTPRequestHandler):
    def log_message(self, *_):
        pass

    def do_GET(self):
        status = 503 if self.path == '/bad' else 200
        if self.path == '/slow':
            time.sleep(2)
        body = b'<soap:Fault>test</soap:Fault>' if self.path == '/fault' else b'<Result>OK</Result>'
        self.send_response(status)
        self.send_header('Content-Length', str(len(body)))
        self.end_headers()
        try:
            self.wfile.write(body)
        except (ConnectionError, OSError):
            pass

    def do_POST(self):
        self.rfile.read(int(self.headers.get('Content-Length', 0)))
        self.do_GET()


def read_csv(path):
    with path.open(encoding='utf-8-sig', newline='') as file:
        return list(csv.DictReader(file))


server = http.server.ThreadingHTTPServer(('127.0.0.1', 0), Handler)
server.daemon_threads = True
threading.Thread(target=server.serve_forever, daemon=True).start()
port = server.server_port
with socket.socket() as closed:
    closed.bind(('127.0.0.1', 0))
    closed_port = closed.getsockname()[1]
results = []
try:
    for engine in ('powershell.exe', 'pwsh.exe'):
        exe = shutil.which(engine)
        assert exe, f'{engine} unavailable'
        env = os.environ.copy()
        if engine == 'powershell.exe':
            # Python inherits PS7's module path; launch Windows PS as it starts natively.
            env['PSModulePath'] = os.pathsep.join([str(Path(os.environ['WINDIR'])/'System32/WindowsPowerShell/v1.0/Modules'),
                                                   str(Path(os.environ['ProgramFiles'])/'WindowsPowerShell/Modules')])
        case = OUT / engine.replace('.exe', '')
        case.mkdir(exist_ok=True)
        (case / 'body.xml').write_text('<Envelope/>', encoding='utf-8')
        (case / 'headers.txt').write_text('Content-Type: text/xml\n', encoding='utf-8')
        http = []
        for name, endpoint in [('ok','ok'), ('bad','bad'), ('fault','fault'), ('missing','ok'), ('slow','slow'), ('post','ok')]:
            target = dict(name=name, url=f'http://127.0.0.1:{port}/{endpoint}', method='GET',
                          approvedReadOnly=True, expectedStatus=[200], expectedContains='Result')
            if name == 'missing':
                target['expectedContains'] = 'ABSENT_MARKER'
            if name == 'post':
                target.update(method='POST', bodyFile='body.xml', headersFile='headers.txt')
            http.append(target)
        config = dict(site='LOCAL SYNTHETIC TEST', scenario=engine, durationSeconds=5, intervalSeconds=1,
                      timeoutSeconds=1, icmp=[dict(name='loopback',host='127.0.0.1')], dns=[],
                      tcp=[dict(name='open',host='127.0.0.1',port=port),dict(name='closed',host='127.0.0.1',port=closed_port)],
                      http=http, sql=dict(enabled=True,server=f'tcp:127.0.0.1,{closed_port}',database='synthetic',
                      authentication='Windows',encrypt=True,trustServerCertificate=False,intervalSeconds=5))
        cfg = case / 'config.json'
        cfg.write_text(json.dumps(config), encoding='utf-8')
        args = [exe, '-NoProfile', '-File', str(SUITE/'Run-Diagnostics.ps1'), '-ConfigPath', str(cfg), '-OutputRoot', str(case/'evidence')]
        run = subprocess.run(args, capture_output=True, text=True, timeout=70, env=env)
        (case/'console.txt').write_text(run.stdout+'\n'+run.stderr, encoding='utf-8')
        assert run.returncode == 0, (engine, run.stdout, run.stderr)
        folder = sorted((case/'evidence').iterdir(), key=lambda p:p.stat().st_mtime)[-1]
        workers = read_csv(folder/'workers.csv')
        assert len(workers) == 10 and all(w['state']=='Completed' for w in workers), workers
        expected = {'ok':'http_ok','bad':'unexpected_http_status','fault':'soap_fault',
                    'missing':'expected_content_missing','slow':'curl_28','post':'http_ok'}
        for name,status in expected.items():
            rows = read_csv(folder/f'http-{name}.csv')
            assert rows and all(r['status']==status for r in rows), (engine,name,rows)
            assert all((r['success']=='True') == (name in ('ok','post')) for r in rows), name
            if name in ('ok','post'):
                assert all(float(r['ttfbMs'])>=0 and float(r['totalMs'])>0 for r in rows)
        assert all(r['success']=='True' for r in read_csv(folder/'tcp-open.csv'))
        assert all(r['success']=='False' for r in read_csv(folder/'tcp-closed.csv'))
        assert all(r['success']=='False' for r in read_csv(folder/'sql-sql.csv'))
        assert all(r['status'].startswith('sql_') for r in read_csv(folder/'sql-sql.csv')), read_csv(folder/'sql-sql.csv')
        assert all(r['success']=='True' for r in read_csv(folder/'icmp-loopback.csv'))
        summary = read_csv(folder/'summary.csv')
        assert any(r['target']=='slow' and float(r['failurePercent'])==100 and r['samples']=='0' for r in summary)
        assert any(r['target']=='ok' and r['metric']=='ttfbMs' for r in summary)
        mark = subprocess.run([exe,'-NoProfile','-File',str(SUITE/'Mark-Incident.ps1'),'-Run',str(folder),
                              '-Phase','start','-Operation','test-01'],capture_output=True,text=True,timeout=15,env=env)
        assert mark.returncode==0 and read_csv(folder/'incidents.csv')[0]['operation']=='test-01'
        # Synthetic DMV file must not be misclassified as a probe CSV.
        (folder/'sql-test-requests.snapshot.csv').write_text('"session_id","status"\n"99","running"\n',encoding='utf-8')
        summarize = subprocess.run([exe,'-NoProfile','-File',str(SUITE/'Summarize.ps1'),'-Run',str(folder)],capture_output=True,text=True,timeout=15,env=env)
        assert summarize.returncode==0, summarize.stderr
        # No accidental secrets/config/response bodies in evidence.
        assert not (folder/'config.json').exists()
        assert not list(folder.glob('*.xml'))
        config['http'][0]['approvedReadOnly']=False
        cfg.write_text(json.dumps(config),encoding='utf-8')
        denied=subprocess.run(args,capture_output=True,text=True,timeout=15,env=env)
        assert denied.returncode!=0 and 'approvedReadOnly' in denied.stderr
        results.append(dict(engine=engine,session=str(folder),status='passed',
                            checks=['HTTP success','HTTP 503','SOAP Fault','content mismatch','HTTP timeout',
                                    'POST read-only','ICMP','TCP open/refused','SQL connection failure',
                                    'percentiles and failure denominator','incident marker','snapshot exclusion',
                                    'unapproved HTTP rejected']))
        print(engine+' passed',flush=True)
finally:
    server.shutdown()
(OUT/'validation.json').write_text(json.dumps(results,indent=2),encoding='utf-8')
