const https = require('https');
const PAT = process.env.AZURE_DEVOPS_PAT;
const AUTH = 'Basic ' + Buffer.from(':' + PAT).toString('base64');
const ORG = 'ejcalderon0892';
const PROJECT = 'TOMWMS_BOF';
const REPO = '1a06eb98-9b7d-49e0-8086-bca97e309315';
const BRANCH = 'dev_2028_merge';
const TARGET = 'clsLnStock_res';

function req(method, host, path, bodyObj) {
  return new Promise((resolve, reject) => {
    const body = bodyObj ? JSON.stringify(bodyObj) : null;
    const opts = {
      method, hostname: host, path,
      headers: {
        'Authorization': AUTH,
        'Accept': 'application/json',
        'Content-Type': 'application/json',
        'User-Agent': 'replit-brain'
      }
    };
    if (body) opts.headers['Content-Length'] = Buffer.byteLength(body);
    const r = https.request(opts, res => {
      let d=''; res.on('data', c=>d+=c);
      res.on('end', () => resolve({ status: res.statusCode, body: d }));
    });
    r.on('error', reject);
    if (body) r.write(body);
    r.end();
  });
}

(async () => {
  console.log('=== Code Search: clsLnStock_res ===');
  const search = await req('POST', 'almsearch.dev.azure.com',
    `/${ORG}/_apis/search/codesearchresults?api-version=7.0`, {
      searchText: TARGET,
      $top: 50,
      filters: { Project: [PROJECT], Repository: ['TOMWMS_BOF'], Branch: [BRANCH] },
      includeFacets: false
    });
  console.log('  HTTP', search.status);
  if (search.status === 200) {
    const j = JSON.parse(search.body);
    console.log('  Resultados:', j.count);
    (j.results || []).slice(0, 20).forEach(r => {
      console.log(`    - ${r.path}  | branch=${r.versions?.[0]?.branchName || '-'}  | matches=${r.matches?.content?.length || 0}`);
    });
  } else {
    console.log('  body:', search.body.slice(0, 400));
  }
})();
