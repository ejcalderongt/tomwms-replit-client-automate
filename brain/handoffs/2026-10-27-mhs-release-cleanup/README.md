# #EJC20261027_MHS_RELEASE

Publicado por instrucción explícita de Erik el 2026-10-27 (America/Guatemala).
Tag Git anotado común: EJC20261027-MHS-EMPAQUE.
Todos los mensajes de commit incluyen #EJC20261027.

## Revisiones publicadas

| Repo | Rama | Commit |
|---|---|---|
| TOMWMS_BOF / Azure DevOps | dev_2028_merge | 9a493e5194ecb7481428a2b7eacabd76614c50ba |
| DBA / GitHub ejcalderongt/DBA | codex/mhs-empaque-20261027 | 59557ee24f297a7c0ed5492f27a80e3b2edfb6d3 |
| tomwms-replit-client-automate / GitHub | wms-brain | cierre documental identificado por el mismo tag |

Ramas y tags de BOF/DBA comprobados con git ls-remote: el tag anotado apunta
exactamente al commit de la rama publicado. Sin force push.
Azure DevOps no soportó --atomic: push normal de rama y tag completado y verificado.

## Contenido

- BOF: ocho archivos. Persistencia segura de flags/estado/fechas de empaque;
  reciclaje de LP completas y adicionales; tab Empaque - X Lic con maestro/detalle
  de solo lectura, colores y vista simplificada hasta Operadores; recursos y
  corrección del patrón inválido de .gitignore.
- DBA: seis archivos de migración aditiva, rollback condicionado y consultas.
  trans_licencia_empaque.sql ya existe idéntico en upstream; no se duplica.
- Brain: handoffs de 2026-10-26 y scripts de validación trasladados fuera de BOF.
  Logs, XML/PNG de pruebas y respaldo local del antiguo README no se publican.
- Brain bootstrap previamente versionado en BOF se mantiene sin cambios.

## Seguridad y conservación

Conn.ini y los dos archivos previos de diseño de Impresion_Recepcion quedan
modificados localmente, fuera del commit. Escaneo de staging sin claves del
Conn.ini, binarios ni logs. No se descartan cambios previos de otros módulos.

DBA master tenía cinco commits locales y 46 remotos divergentes. Se creó un
worktree del MISMO repositorio desde origin/master, se aplicó el parche exacto
preparado y se publicó únicamente esa entrega en codex/mhs-empaque-20261027.
No se mezclan ni reescriben esos cinco commits anteriores. Master local conserva
su historia y trabajo pendiente; no es la rama publicada para esta entrega.
Worktree: C:/Users/yejc2/source/repos/DBA_mhs_empaque_20261027.
Origin DBA normalizado a HTTPS canónico (SSH fallaba por known_hosts).

## Validación y despliegue

Compilaciones BOF y WSHHRN correctas; pruebas de DLL/DevExpress, expansión vacía,
formatos, rollback y preservación de preparación descritas en los handoffs:

- ../2026-10-26-picking-etiquetas-empaque/README.md
- ../2026-10-26-mhs-tarimas-completas/README.md
- ../2026-10-26-picking-visor-empaque/README.md

Esta publicación no despliega aplicaciones ni migra producción. La columna
EsRecicladaRecepcion debe existir antes de usar el DAL nuevo en otra base;
solo MHS-DEV fue migrada durante implementación. Los scripts de validación
son fixtures DEV con rollback, no datos operativos ni tareas para producción.
Brain remoto de indexación continúa sin token disponible; Git del Brain sí se publica.
