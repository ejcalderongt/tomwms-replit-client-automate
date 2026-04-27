const https = require('https');
const PAT = process.env.AZURE_DEVOPS_PAT;
const AUTH = 'Basic ' + Buffer.from(':' + PAT).toString('base64');
const ORG = 'ejcalderon0892';
const PROJECT = process.argv[2] || 'TOMWMS_BOF';

function get(path) {
  return new Promise((res, rej) => {
    https.request({
      method: 'GET', hostname: 'dev.azure.com', path,
      headers: { 'Authorization': AUTH, 'Accept': 'application/json', 'User-Agent': 'replit-brain' }
    }, r => { let d=''; r.on('data',c=>d+=c); r.on('end', () => res({ status: r.statusCode, body: d })); }).on('error', rej).end();
  });
}

(async () => {
  console.log(`=== Repos en proyecto ${PROJECT} ===`);
  const r = await get(`/${ORG}/${PROJECT}/_apis/git/repositories?api-version=7.0`);
  if (r.status !== 200) { console.error('HTTP', r.status, r.body.slice(0,300)); process.exit(1); }
  const j = JSON.parse(r.body);
  for (const repo of (j.value || [])) {
    console.log(`  - ${repo.id}  | name="${repo.name}"  | defaultBranch=${repo.defaultBranch}`);
  }
  
  console.log(`\n=== Branches del primer repo ===`);
  const repo0 = j.value[0];
  if (repo0) {
    const br = await get(`/${ORG}/${PROJECT}/_apis/git/repositories/${repo0.id}/refs?filter=heads&api-version=7.0`);
    if (br.status === 200) {
      const bj = JSON.parse(br.body);
      bj.value.forEach(b => console.log(`  - ${b.name}`));
    }
  }
})();
