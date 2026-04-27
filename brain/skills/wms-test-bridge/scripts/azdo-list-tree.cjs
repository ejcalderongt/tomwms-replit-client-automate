const https = require('https');
const PAT = process.env.AZURE_DEVOPS_PAT;
const AUTH = 'Basic ' + Buffer.from(':' + PAT).toString('base64');
const ORG = 'ejcalderon0892';
const PROJECT = 'TOMWMS_BOF';
const REPO = '1a06eb98-9b7d-49e0-8086-bca97e309315';
const BRANCH = 'dev_2028_merge';

function get(path) {
  return new Promise((res, rej) => {
    https.request({
      method: 'GET', hostname: 'dev.azure.com', path,
      headers: { 'Authorization': AUTH, 'Accept': 'application/json', 'User-Agent': 'replit-brain' }
    }, r => { let d=''; r.on('data',c=>d+=c); r.on('end', () => res({ status: r.statusCode, body: d })); }).on('error', rej).end();
  });
}

(async () => {
  const path = `/${ORG}/${PROJECT}/_apis/git/repositories/${REPO}/items?recursionLevel=Full&versionDescriptor.version=${BRANCH}&versionDescriptor.versionType=branch&api-version=7.0`;
  console.log('GET', path);
  const r = await get(path);
  console.log('HTTP', r.status, 'body length:', r.body.length);
  if (r.status !== 200) { console.log(r.body.slice(0,500)); process.exit(1); }
  const j = JSON.parse(r.body);
  const items = j.value || [];
  console.log('Total items:', items.length);
  
  // Buscar archivos que contengan clsLnStock_res
  const matches = items.filter(it => /clsLnStock_res/i.test(it.path));
  console.log('\n=== Matches clsLnStock_res ===');
  matches.forEach(m => console.log(`  ${m.gitObjectType.padEnd(8)} ${m.path}  (objectId=${m.objectId?.slice(0,8) || '-'})`));
  
  // Bonus: cualquier *.vb que tenga "Test" o "QA" en el nombre
  console.log('\n=== Otros archivos con Test/QA ===');
  items.filter(it => it.gitObjectType === 'blob' && /\.vb$/i.test(it.path) && /(Test|QA|Caso|Pruebas)/i.test(it.path))
       .slice(0, 30)
       .forEach(m => console.log(`  ${m.path}`));
  
  // Guardar lista completa para referencia
  require('fs').writeFileSync('/tmp/azdo-fetch/tree.txt', items.map(i => `${i.gitObjectType}\t${i.path}`).join('\n'));
  console.log('\nÁrbol completo guardado en /tmp/azdo-fetch/tree.txt');
})();
