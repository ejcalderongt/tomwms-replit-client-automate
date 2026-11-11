---
tipo: runbook
clientes: [CUMBRE]
estado: preparado-pendiente-validacion-en-sitio
---

# La Cumbre — suite de diagnóstico en sitio

<!-- #EJC20261111_CUMBRE_NETWORK_DIAGNOSTICS -->

Entrada operativa: [guía de despliegue](../../tools/network-diagnostics/README.md).
Prompt para el agente en la PC del técnico: [PROMPT-AGENTE.md](../../tools/network-diagnostics/PROMPT-AGENTE.md).

Encargo del 11 de noviembre de 2026: investigar lentitud recurrente reportada en La Cumbre con una PC conectada directamente a su red. El reporte de señales Wi-Fi correctas no es todavía una validación de calidad de enlace. Ubicación real de WS/SQL, versiones desplegadas, procesos y dispositivos afectados quedan por relevar en sitio.

Este es un procedimiento preparado, no evidencia de una causa raíz. El paquete contiene inventario local, sondas independientes ICMP/TCP/DNS/HTTP/SOAP/SQL, marcadores de operación, captura Wireshark acotada, análisis de indicadores y observación opcional de servidor. No cambia WMS ni infraestructura. No incluye secretos/configuración productiva ni instala componentes en La Cumbre de forma remota.

Generar paquete portable con `tools/network-diagnostics/Export-Deployment.ps1 -Destination <carpeta-nueva>`. Configuración privada y evidencias están excluidas por allowlist. El ZIP incluye contexto Brain mínimo con HEAD, estado local y hashes; no representa el Brain completo ni sincronización remota.

En sitio conservar hechos/hipótesis/decisiones y evidencia en `analysis/cumbre-network/<fecha>/`; integrar conclusiones revisadas en un handoff con procedencia y alcance. PCAP y material privado permanecen restringidos. No commit/push implícito. No atribuir causa por señal, ping, SELECT 1 o retransmisiones aisladas.

Validación local de la suite: `analysis/cumbre-network-suite-20261111/validation.json` y sesiones sintéticas asociadas. Consultar sus resultados y límites antes de declarar compatibilidad. Wireshark/Npcap y SQL exitoso requieren verificación en destino si no fueron ejercitados localmente.
