---
id: db-brain-sp-set-clientes-rec
type: db-sp
title: dbo.SET_CLIENTES_REC
schema: dbo
name: SET_CLIENTES_REC
kind: sp
modify_date: 2018-04-13
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.SET_CLIENTES_REC`

| Atributo | Valor |
|---|---|
| Tipo | SQL_STORED_PROCEDURE |
| Schema modify_date | 2018-04-13 |

## Consume

- `cliente`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE PROCEDURE [dbo].[SET_CLIENTES_REC]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

update cliente set es_bodega_recepcion = 1 where IdCliente IN (1,4,11,18,3)
END
```
