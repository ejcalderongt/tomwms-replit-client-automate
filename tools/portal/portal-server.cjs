// #EJC20261023_CAREMED_PUBLICACION: React compilado y proxy a WMSWebAPI local.
const http = require('node:http');
const fs = require('node:fs');
const path = require('node:path');
const root = path.join(__dirname, 'react');
const uiPort = Number(process.env.PORTAL_UI_PORT || 5174);
const apiPort = Number(process.env.PORTAL_API_PORT || 5197);
const types = {'.html':'text/html; charset=utf-8','.js':'text/javascript; charset=utf-8','.css':'text/css; charset=utf-8','.json':'application/json','.png':'image/png','.jpg':'image/jpeg','.svg':'image/svg+xml','.ico':'image/x-icon','.woff':'font/woff','.woff2':'font/woff2'};
http.createServer((req,res)=>{
  const url = new URL(req.url, 'http://localhost');
  if(url.pathname.startsWith('/api/') || url.pathname.startsWith('/kpi/')) {
    const upstreamPath = req.url.replace(/^\/kpi\//,'/api/');
    const proxy = http.request({hostname:'127.0.0.1',port:apiPort,path:upstreamPath,method:req.method,headers:{...req.headers,host:'127.0.0.1:'+apiPort}}, upstream=>{
      res.writeHead(upstream.statusCode,upstream.headers);upstream.pipe(res);
    });
    proxy.setTimeout(120000,()=>proxy.destroy(new Error('Timeout')));
    proxy.on('error',()=>{if(!res.headersSent)res.writeHead(502,{'content-type':'application/json'});res.end('{"mensaje":"API local no disponible"}');});
    req.pipe(proxy);return;
  }
  if(req.method!=='GET' && req.method!=='HEAD'){res.writeHead(405);res.end();return;}
  let file;
  try {file=path.resolve(root,'.'+decodeURIComponent(url.pathname));} catch {res.writeHead(400);res.end();return;}
  if(file!==root && !file.startsWith(root+path.sep)){res.writeHead(403);res.end();return;}
  if(!fs.existsSync(file) || !fs.statSync(file).isFile()){
    if(path.extname(url.pathname)){res.writeHead(404);res.end();return;}
    file=path.join(root,'index.html');
  }
  res.writeHead(200,{'content-type':types[path.extname(file)]||'application/octet-stream','cache-control':path.basename(file)==='index.html'?'no-cache':'public, max-age=3600'});
  if(req.method==='HEAD'){res.end();return;}
  fs.createReadStream(file).pipe(res);
}).listen(uiPort,'127.0.0.1',()=>console.log('Portal React local disponible'));
