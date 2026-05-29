---
id: PLAYBOOK-FIX-CP014
tipo: cp-open
estado: vigente
titulo: PLAYBOOK-FIX CP-014 — Remediacion stock fantasma WMS62 Killios
ramas: [dev_2023_estable, dev_2028_merge]
hereda_de: CP-013/PLAYBOOK-FIX.md
tags: [cp-open]
---

# PLAYBOOK-FIX CP-014 — Remediacion stock fantasma WMS62 Killios

> Este playbook **hereda integramente** las secciones §A-§H de
> `CP-013/PLAYBOOK-FIX.md`. Solo se documentan aqui los **deltas
> especificos** de WMS62 / variante REEMP_BE_PICK / Killios.

---

## A'. Patch funcional (extension al patch §A de CP-013)

CP-013 §A propone capturar el `dañado_picking=1` desde backoffice.
Este caso agrega:

1. **Capturar el cambio manual `IdProductoEstado: 1 → 8`** (BUEN ESTADO
   → REEMPACAR) en el grid de gestion de stock (TOMIMSV4 forms).
   Mismo combo de destino obligatorio (MERMA / DEVOLUCION / DESCARTE).
2. **Capturar el flujo HH REEMP_BE_PICK / REEMP_ME_PICK** (TipoTarea
   25/26): cuando el operador reemplaza un pallet durante picking, el
   sistema debe:
   - Generar `trans_movimientos` con `IdTipoTarea=17 (AJCANTN)` por la
     cantidad de la matricula vieja.
   - O bien, si la matricula vieja queda fisicamente, dejarla como
     `IdProductoEstado=8 (REEMPACAR)` Y generar un AJCANTN simbolico
     para que el kardex SAP reciba la senal.

## B'. Reconciliacion retroactiva especifica WMS62 bodega 1

> Pre-requisito: validacion fisica por Carolina + Zulma de las 27
> lineas listadas en `traza-001-stock-fantasma.md`.

### B'.1. Caso minimo — solo las 10 cajas reportadas hoy

Aplicar AJCANTN -60 UM sobre la(s) linea(s) que Carolina identifique
como sobrante. Plantilla SQL en `traza-001-stock-fantasma.md` §D.3.

### B'.2. Caso ampliado — limpiar las 364 cajas REEMPACAR/MAL ESTADO

Si Carolina determina que ningun pallet REEMPACAR esta fisicamente:
aplicar AJCANTN sobre los 8 IdStock danado=true (suma 2.184 UM = 364
cajas). Esto:
- Reduce stock vivo bodega 1 a 92,83 cajas (solo lo sano + reservado).
- Deja saldo WMS muy por debajo del kardex SAP — requeriria reverso de
  alguna recepcion historica.

**No recomendado**. Probable que solo algunas REEMPACAR esten faltantes.
Validar caso por caso.

### B'.3. Caso "alineamiento total con SAP"

Eliminar 60 UM (las 10 cajas reportadas) y dejar el resto del stock como
esta. Esto deja:
- Stock WMS bodega 1 = 446,83 cajas = kardex SAP.
- Las matriculas REEMPACAR siguen vivas pero con un AJCANTN simbolico
  que SAP ya conoce.

**Recomendado**.

## C'. Validacion post-fix

Despues del ajuste, correr las queries READ-ONLY de
`traza-001-stock-fantasma.md` §C:

| Query | Resultado esperado |
|---|---|
| C.1 listado fresco | <27 lineas o iguales pero alguna con cantidad reducida |
| C.2 total bodega 1 | **2.681 UM = 446,83 cajas** (= kardex SAP) |
| C.4 saldo SAP reconstruido | Sin cambios (2.681 UM) — el AJCANTN compensatorio fue contabilizado |

Y monitorear durante 7 dias que no se generen nuevas lineas con
`IdProductoEstado=8` o nuevas matriculas sin contraparte en
`trans_movimientos`.

## D'. Si el fix de codigo de BUG-001 ya esta aplicado a `dev_2028_merge`

Validar con Carolina si el release que tiene Killios esta arriba de la
version donde se aplico el patch CP-013. Si si: este caso es post-fix y
revela que el patch no cubre el escenario REEMP_BE_PICK. Extender el
patch.

Si no: este caso es pre-fix. Aplicar B'.3 como remediacion puntual y
esperar al release con el fix.

---

## Cross-links

- `CP-013/PLAYBOOK-FIX.md` (playbook madre §A-§H)
- `traza-001-stock-fantasma.md` (datos forenses CP-014)
- `wms-known-issues/BUG-001-danado-picking-no-resta-inventario/INDEX.md`
- `INFORME-CAROLINA.md` (donde buscar el bug en codigo)
