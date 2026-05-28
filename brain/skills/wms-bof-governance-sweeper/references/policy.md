---
tipo: other
autores: [erik]
---
# BOF Governance Policy

`#EJC20260527`

## Goal

Find high-value BOF improvement candidates without changing runtime behavior.

## Safe scope

- Read-only static analysis on `*.vb`.
- Candidate-level findings with evidence (file + line).
- Prioritization by risk and optimization payoff.

## Out of scope for automatic changes

- Refactors that alter public method contracts.
- SQL semantics rewrites.
- State-machine logic modifications.

## Priority heuristics

1. Potential business-rule conflict in operational flows.
2. Heavy query patterns affecting latency.
3. Roundtrip-heavy hotspots.
4. Duplication and dead-code candidates.
