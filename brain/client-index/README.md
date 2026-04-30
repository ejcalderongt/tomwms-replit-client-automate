# client-index

Índice por cliente con su perfil de BD, particularidades, mapeos custom, outliers conocidos y registro de revisiones/hallazgos. Permite arrancar cualquier diagnóstico futuro sin re-aprender lo que ya descubrimos en el cliente.

## Estructura de un yml por cliente

Cada archivo `<slug>.yml` (ej: `killios.yml`) tiene estas secciones:

```yaml
cliente: <Nombre legible>
slug: <slug minúsculas>

bd:
  host_env: WMS_DB_HOST
  user_env: WMS_DB_USER
  password_env: WMS_<SLUG>_DB_PASSWORD
  database_env: WMS_<SLUG>_DB_NAME
  driver: pymssql
  read_only: true

modulos:
  stock_jornada: false   # parámetro de configuración del cliente
  hh_android: true
  bof_vbnet: true

mapeos_custom:
  tipos_tarea:           # solo si difieren del glosario base
    7: NombreCustom
  motivos_ajuste:
    1: "Error en digitación"
    2: "Falla de sistema"
  roles_usuario:
    18: Auditoría

usuarios_relevantes:
  - id: 13
    nombre: Mario Santizo
    rol: Operador BOF
  - id: 20
    nombre: Heidi López
    rol: BOF

factores_default:
  caja_um_promedio_estimado: 5_to_12
  presentaciones_comunes:
    - { nombre: Caja5, factor: 5 }

periodos_clave:
  inicio_operacion: 2025-11-30
  gaps_conocidos:
    - tabla: trans_ajuste_enc
      desde: 2023-04-01
      hasta: 2025-11-01
      descripcion: "2.5 años sin ajustes manuales — posible cambio de proceso/versión"

outliers_conocidos:
  - tabla: trans_ajuste_det
    pk: { idajustedet: 638 }
    descripcion: "4.8B UM mayo-2021, observación 'sa' — excluir de agregados"

revisiones:
  - fecha: 2026-04-30
    caso: CP-013-killios-wms164
    rama_brain: brain/debuged-cases/CP-013-killios-wms164/
    hallazgos:
      - "Bug dañado_picking sin descuento (>320k UM fantasma transversales)"
      - "Cronicidad >5 años (1653 ajustes manuales históricos)"
      - "Outlier corrupto idajustedet=638"
      - "WMS164/BG2512: BD sobra 14 cajas exactas"
    waves:
      - { id: 18, descripcion: "Trazabilidad full WMS164" }
      - { id: 19, descripcion: "Cuantificación dañados Killios completo" }
      - { id: 20, descripcion: "Desglose stock real BG2512 + factor caja" }
      - { id: 21, descripcion: "Cruce histórico ajustes manuales" }
```

## Programa relacionado

`replicate.py` lee un yml y permite re-correr hallazgos de revisiones anteriores rápido. Uso:

```bash
# Listar revisiones del cliente
python replicate.py killios --list

# Re-correr un hallazgo (re-ejecuta queries clave del caso)
python replicate.py killios --caso CP-013-killios-wms164

# Smoke test de la conexión
python replicate.py killios --smoke
```

## Cómo agregar un cliente nuevo

1. Cargar las env vars del cliente: `WMS_<SLUG>_DB_PASSWORD`, `WMS_<SLUG>_DB_NAME`.
2. Crear `<slug>.yml` con la plantilla de arriba.
3. Correr smoke test: `python replicate.py <slug> --smoke`.
4. Documentar las particularidades que encuentres a medida que investigues.
5. Cada caso cerrado para ese cliente agrega una entrada en `revisiones` con la rama del brain y los hallazgos.
