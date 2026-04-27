const https = require('https');
const fs = require('fs');
const PAT = process.env.AZURE_DEVOPS_PAT;
const AUTH = 'Basic ' + Buffer.from(':' + PAT).toString('base64');
const ORG = 'ejcalderon0892';
const PROJECT = process.argv[2];
const REPO = process.argv[3];
const BRANCH = process.argv[4];
const FILEPATH = process.argv[5];
const OUT = process.argv[6];

const apiPath = `/${ORG}/${PROJECT}/_apis/git/repositories/${REPO}/items?path=${encodeURIComponent(FILEPATH)}&versionDescriptor.version=${BRANCH}&versionDescriptor.versionType=branch&includeContent=true&api-version=7.0`;

https.request({
  method: 'GET', hostname: 'dev.azure.com', path: apiPath,
  headers: { 'Authorization': AUTH, 'Accept': 'text/plain', 'User-Agent': 'replit-brain' }
}, r => {
  let chunks = [];
  r.on('data', c => chunks.push(c));
  r.on('end', () => {
    const buf = Buffer.concat(chunks);
    if (r.statusCode !== 200) {
      console.error('HTTP', r.statusCode, buf.toString().slice(0,300));
      process.exit(1);
    }
    fs.writeFileSync(OUT, buf);
    console.log(`  OK  ${FILEPATH} -> ${OUT}  (${buf.length} bytes)`);
  });
}).on('error', e => { console.error(e); process.exit(1); }).end();
