# Local Brain tools

<!-- #EJC20261027_BRAIN_INTEGRATION -->

Resolve `BrainRoot` using `../scripts/resolve-wms-paths.ps1`; the current
manifest is `tools/brain-quality/workspace.json`, with code roots in
`source-roots.json`. Explicit `-BrainRoot` or `WMS_BRAIN_ROOT` supports a moved
checkout. Do not assume old Replit/OpenClaw paths still exist.

From BrainRoot, use `python tools/brain-quality/brain.py "<question>"` for
local BM25 retrieval with provenance. Client/branch filters are strict over
declared metadata; missing scope is unknown, not universal. `bm25-rerank`
is an optional deeper retrieval mode requiring the isolated semantic runtime
and `--reranker-runtime`. Its inference is materially slower than BM25.
The search policy and measured comparisons live in `tools/brain-quality/search-policy.json`.

For possible dependencies or impact, query `tools/brain-quality/graph.py`;
read BrainRoot/AGENTS.md for rebuild/query examples and coverage limits.
Query selected symbols rather than loading the full graph into model context.
Hashes and source inventory must still match. Syntax linkage is not execution
or deployment proof, and alternative DDL locations can conflict.

When specialist-agent work is authorized, use `specialist-contracts.json` and
`agent_workflow.py` for code, SQL, functional, coordinator and verifier roles.
Keep independent discovery separate from synthesis, bound evidence follow-ups,
and preserve migration/QA/eligibility constraints. Verify narratives as well
as listed claims; a literal citation match is not semantic support.
Ordinary questions do not require all specialists.

Historical Git citations must carry the immutable `git_ref`, hash and span;
never validate another branch's hash against the current working file.
Unknown token/dollar costs remain unknown. Evaluation scores apply to the
tested sample and do not establish general reasoning superiority.
