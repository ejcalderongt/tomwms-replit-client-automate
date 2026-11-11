# Validación local de la suite de La Cumbre

<!-- #EJC20261111_CUMBRE_NETWORK_DIAGNOSTICS -->

`validation.json` resume las dos sesiones sintéticas aprobadas. Sus rutas `session` son relativas a la raíz del Brain y apuntan a extractos saneados en `synthetic-evidence/`. Se conservan las mediciones de loopback, resumen e incidentes; se excluyen inventarios de red de la PC, configuraciones privadas, sesiones fallidas previas y archivos temporales. La evidencia de preparación no contiene mediciones de La Cumbre.

`validate_suite.py` permite repetir las pruebas con Python 3, Windows PowerShell 5.1 y PowerShell 7. Usa un servidor HTTP local y puertos loopback. La salida nueva incluye inventario LOCAL: no publicar recursivamente toda esta carpeta. Los resultados nuevos deben revisarse y normalizar sus rutas antes de sustituir la evidencia publicada.

`claims.json` conserva procedencia de las decisiones y resultados locales; permanecen draft por no declarar validación independiente ni fuentes ya commiteadas. Los hashes se verifican con `tools/brain-quality/provenance.py`.

Límites de validación: ver `tools/network-diagnostics/VALIDACION.md`. Restauración en otra PC: `tools/network-diagnostics/RESTAURAR-OTRA-PC.md`.
