# Recepción WMS: LP y Implosión (Brain)

Este brain concentra decisiones, trazas y planes de trabajo para:
- Bloqueo/limpieza de LP en HH cuando banderas están OFF
- Instrumentación WS/DAL para trazabilidad LP y cast seguro
- Diseño de implosión (split) de licencia maestra a licencias destino

Archivos clave:
- lp-flujo-analisis.yml → Resumen de cambios y lpPermitido
- lp-endpoints.yml → Mapa de casos HH y endpoints WS
- db-probes.yml → Consultas seguras (PRD EC2)
- implosion/implosion-flujo.yml → Diseño del flujo UI/WS/DAL
- implosion/implosion-trazas.yml → Esquema de trazas HH/WS/DAL
- ../atlas/implosion-mapa.yml → Vista de arquitectura

Próximo:
- Endpoint WS Recepcion_Implosion_Mover_Subconjunto
- HH: nueva pantalla/fragmento de implosión con escaneo y selección acumulativa
