---
tipo: other
ramas_afectadas: [dev_2028_merge]
autores: [erik]
---
# Aplicar export Excel limpio — REEMPLAZO DIRECTO de archivos

**Cuando usar este metodo**: si `git apply` te falla con "patch does not
apply" porque tu clone tiene commits posteriores que cambiaron el
contexto del Designer.

**Base sobre la que se generaron los archivos**:
- Repo: TOMWMS_BOF
- Rama: `dev_2028_merge`
- Commit base: `a72ce6b` (HEAD de origin/dev_2028_merge al 2026-05-12)
- Hash SHA256 archivos modificados: ver al final.

## Que hacer

### Opcion 1 — Descargar via curl (recomendado)

```bash
cd /tu/clone/TOMWMS_BOF
git checkout dev_2028_merge
git pull origin dev_2028_merge

# Backup local por las dudas
cp TOMIMSV4/TOMIMSV4/Transacciones/Ajustes/frmAjusteStock.vb \
   TOMIMSV4/TOMIMSV4/Transacciones/Ajustes/frmAjusteStock.vb.bak
cp TOMIMSV4/TOMIMSV4/Transacciones/Ajustes/frmAjusteStock.Designer.vb \
   TOMIMSV4/TOMIMSV4/Transacciones/Ajustes/frmAjusteStock.Designer.vb.bak

# Reemplazar
curl -L -o TOMIMSV4/TOMIMSV4/Transacciones/Ajustes/frmAjusteStock.vb \
  "https://raw.githubusercontent.com/ejcalderongt/tomwms-replit-client-automate/wms-brain/wms-brain/brain/code-changes/AJUSTE-STOCK-EXPORT-EXCEL/full-files/frmAjusteStock.vb"

curl -L -o TOMIMSV4/TOMIMSV4/Transacciones/Ajustes/frmAjusteStock.Designer.vb \
  "https://raw.githubusercontent.com/ejcalderongt/tomwms-replit-client-automate/wms-brain/wms-brain/brain/code-changes/AJUSTE-STOCK-EXPORT-EXCEL/full-files/frmAjusteStock.Designer.vb"

# Verificar diff esperado
git diff --stat -- TOMIMSV4/TOMIMSV4/Transacciones/Ajustes/frmAjusteStock.vb \
                   TOMIMSV4/TOMIMSV4/Transacciones/Ajustes/frmAjusteStock.Designer.vb
```

Diff esperado aproximado:
- `frmAjusteStock.vb`: +285 lineas (region nuevo + 4 imports)
- `frmAjusteStock.Designer.vb`: +13 / -2 lineas

### Opcion 2 — Descargar manual desde el navegador

Ir a:
- https://github.com/ejcalderongt/tomwms-replit-client-automate/tree/wms-brain/wms-brain/brain/code-changes/AJUSTE-STOCK-EXPORT-EXCEL/full-files

Click en cada archivo, boton "Download raw file", y los pegas en
`TOMIMSV4/TOMIMSV4/Transacciones/Ajustes/` reemplazando los originales.

## IMPORTANTE: si tu clone tiene commits mas nuevos que `a72ce6b`

Estos archivos completos se generaron sobre `a72ce6b`. Si tenes commits
posteriores que tocaron `frmAjusteStock.vb` o su Designer, vas a perder
esos cambios al reemplazar.

Para chequear si hay riesgo:

```bash
git log --oneline a72ce6b..HEAD -- \
  TOMIMSV4/TOMIMSV4/Transacciones/Ajustes/frmAjusteStock.vb \
  TOMIMSV4/TOMIMSV4/Transacciones/Ajustes/frmAjusteStock.Designer.vb
```

- Si el output es vacio → seguro, reemplaza tranquilo.
- Si hay commits → NO reemplaces. Avisame y armamos un patch a 3 vias o
  un script anchor-based que aplique solo los 7 cambios respetando lo tuyo.

## Despues del reemplazo

1. Abrir `TOMWMS.sln` en Visual Studio.
2. Build del proyecto WMS (TOMIMSV4). Verificar 0 errores.
3. Probar manual: abrir un Ajuste con detalle, click "Exportar Excel"
   en el ribbon, elegir destino y formato.
4. Si todo OK, commit a `dev_2028_merge`:
   ```
   git add TOMIMSV4/TOMIMSV4/Transacciones/Ajustes/frmAjusteStock.vb \
           TOMIMSV4/TOMIMSV4/Transacciones/Ajustes/frmAjusteStock.Designer.vb
   git commit -m "#EJCRP feat(TOMIMSV4/Ajustes): exportar Excel limpio en frmAjusteStock"
   git push origin dev_2028_merge
   ```

## Hashes SHA256 para verificacion

Se publican junto a los archivos en `full-files/SHA256SUMS.txt`.
