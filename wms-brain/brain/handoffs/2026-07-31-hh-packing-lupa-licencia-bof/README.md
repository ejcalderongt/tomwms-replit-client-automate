---
slug: 2026-07-31-hh-packing-lupa-licencia-bof
estado: aplicado_local_sin_commit
fecha: "2026-07-31"
owner: hh
clientes: [MHS, CORE]
ramas: [dev_2026_estable, dev_2028_merge]
tags: [packing, hh, lupa, licencia_bof, callback, branch_port, regresion]
freshness_days: 90
---

# HH Packing - lupa bloqueada y licencia de empaque BOF

## Sintoma

Al entrar a preparacion de packing y pulsar la lupa, el control podia quedar
deshabilitado y el operador ya no podia empacar productos.

## Causa

El listener ejecutaba `btnBuscars.setEnabled(false)` antes de llamar
`procesarLicencia()`. Con `txtLP` vacio, el metodo retornaba sin lanzar una
peticion y sin callback capaz de reactivar la lupa.

## Solucion federada

- La lupa ya no se deshabilita de forma incondicional en el listener.
- Una busqueda vacia converge en `verLista()` y muestra pendientes.
- El bloqueo se conserva solo durante la validacion asincrona real de una
  licencia de empaque BOF.
- `licenciaEmpaqueBofSeleccionada` evita que `Load()` reemplace una licencia
  BOF valida por la resolucion automatica.

## Trazabilidad de ramas

- Cambio funcional de licencia BOF: `c189d431` en `dev_2026_estable`.
- Fix de referencia: `9760ae35` en `dev_2028_merge`.
- Destino local corregido: `dev_2026_estable`.
- Archivo unico: `frm_preparacion_packing.java`.
- Patch-id comun: `8551ba65c2863348569df1a533517bd41d06e67c`.

## Validacion

- Android Java: `BUILD SUCCESSFUL` en `:app:compileDebugJavaWithJavac`.
- Higiene: `git diff --check` sin hallazgos.
- Encoding: UTF-8 sin BOM, CRLF, sin mojibake.
- Regresion: no se importaron diferencias de `frm_Packing.java`, layout,
  implosion, talla/color ni configuracion entre ramas.

## Riesgo residual

Probar en dispositivo el flujo SOAP completo con lupa vacia, producto/LP
legado, licencia generada en BOF, insercion de packing y finalizacion.

## Gobernanza de identidad

No se exige que Carol se identifique para analizar, modelar o curar conocimiento
sin efectos destructivos. Una identidad declarada en chat no autoriza commits,
pushes, secretos, escrituras productivas ni acciones destructivas.

