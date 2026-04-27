const https = require('https');
const PAT = process.env.AZURE_DEVOPS_PAT;
if (!PAT) { console.error('Falta AZURE_DEVOPS_PAT'); process.exit(1); }
const AUTH = 'Basic ' + Buffer.from(':' + PAT).toString('base64');

function get(host, path) {
  return new Promise((resolve, reject) => {
    const req = https.request({
      method: 'GET', hostname: host, path,
      headers: { 'Authorization': AUTH, 'Accept': 'application/json', 'User-Agent': 'replit-brain' }
    }, res => {
      let d = ''; res.on('data', c => d += c);
      res.on('end', () => resolve({ status: res.statusCode, body: d, headers: res.headers }));
    });
    req.on('error', reject); req.end();
  });
}

(async () => {
  // Probamos varias orgs candidatas
  const orgs = ['ejcalderon0892', 'PrograX24', 'prograx24', 'ejcalderon', 'TOMWMS'];
  for (const org of orgs) {
    console.log(`\n=== Probando org: ${org} ===`);
    const r = await get('dev.azure.com', `/${org}/_apis/projects?api-version=7.0`);
    console.log(`  HTTP ${r.status} ${r.headers['content-type'] || ''}`);
    if (r.status === 200) {
      try {
        const j = JSON.parse(r.body);
        console.log(`  Projects (${j.count || j.value?.length || 0}):`);
        (j.value || []).forEach(p => console.log(`    - ${p.id} | name="${p.name}" | url=${p.url}`));
      } catch(e) { console.log('  raw:', r.body.slice(0, 300)); }
      break;
    } else if (r.status === 203 || r.status === 401) {
      console.log('  (auth/redirect; primer fragmento del body)');
      console.log(' ', r.body.slice(0, 200).replace(/\n/g,' '));
    } else if (r.status === 302) {
      console.log('  redirect a:', r.headers.location);
    } else {
      console.log('  body fragment:', r.body.slice(0, 200));
    }
  }
})();
