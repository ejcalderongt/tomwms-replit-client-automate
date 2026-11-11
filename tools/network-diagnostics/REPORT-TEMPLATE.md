# La Cumbre — diagnóstico de latencia

<!-- #EJC20261111_CUMBRE_NETWORK_DIAGNOSTICS -->

Estado inicial: pendiente de mediciones en sitio. Este documento no contiene resultados reales.

## Respuesta principal

Tramo observado con demora: UNKNOWN. Causa raíz: no determinada. Próxima prueba discriminadora: pendiente.

## Contexto verificable

- Sesión/fecha/UTC y zona local:
- Operador/agente/PC:
- Contexto Brain: revisión, archivos y hashes; cambios locales:
- Versiones desplegadas HH/BOF/WS y fuente de verificación:
- Topología saneada: PC/HH → AP/switch/VLAN → WS → SQL; local/remoto/VPN/proxy:
- Conexión cable/Wi-Fi, interfaz/ruta, zona/SSID/BSSID/banda/canal:
- Relojes y desfases PC/HH/AP/IIS/SQL:
- Operación/método exacto, lectura confirmada, tamaño de solicitud/respuesta y datos/carga comparable:
- Cobertura: qué tráfico y servidores se pudieron observar; permisos faltantes:

## Línea de tiempo

| UTC | Operación/dispositivo/zona | Duración observada | Métricas coincidentes | Evidencia ruta/fila/frame |
|---|---|---|---|---|
| pendiente | | | | |

## Comparación

| Escenario/tramo | Muestras/éxitos/fallos/timeouts | p50 | p95 | p99 | Máximo | Limitaciones |
|---|---|---|---|---|---|---|
| pendiente | | | | | | |

Unidad ms. Percentiles sobre éxitos; describir aparte fallos y sus duraciones. Mostrar conexión SQL y SELECT 1 separados; HTTP DNS/TCP/TLS/TTFB/descarga. Contrastar operación completa y HTTP de negocio frente a pruebas básicas.

## Hipótesis y prueba siguiente

| ID | Hipótesis/tramo | A favor | En contra | Confianza/estado | Prueba que la distingue |
|---|---|---|---|---|---|
| H01 | | | | sin evaluar | |

## Acciones propuestas

| Prioridad | Acción concreta | Responsable sugerido | Evidencia que la justifica | Validación posterior | Requiere autorización de cambio |
|---|---|---|---|---|---|
| pendiente | | | | | |

## Brain y procedencia

Separar fact/hypothesis/decision; limitar al cliente/versión realmente observados. No completar el template como verified hasta tener evidencia. Registro de ejemplo estructural (NO es un hallazgo):

```json
{
  "id":"CUMBRE-NET-PENDIENTE-001",
  "kind":"hypothesis",
  "statement":"Pendiente de formular a partir de evidencia",
  "sources":[],
  "clients":["CUMBRE"],
  "branches":[],
  "validated_at":null,
  "status":"draft"
}
```

Para cada fuente agregar path, commit (null si evidencia no commiteada), sha256 y procedencia. Mantener PCAP/secretos fuera de la publicación; derivar extracto saneado. Enumerar pendientes sin resolver, cobertura faltante y condiciones necesarias para validar la causa.
