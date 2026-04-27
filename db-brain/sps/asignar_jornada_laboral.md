---
id: db-brain-sp-asignar-jornada-laboral
type: db-sp
title: dbo.asignar_jornada_laboral
schema: dbo
name: asignar_jornada_laboral
kind: sp
modify_date: 2024-09-18
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.asignar_jornada_laboral`

| Atributo | Valor |
|---|---|
| Tipo | SQL_STORED_PROCEDURE |
| Schema modify_date | 2024-09-18 |
| Parámetros | 1 |

## Parámetros

| # | Nombre | Tipo | Out |
|---:|---|---|:-:|
| 1 | `@JornadaLaboral` | `int` |  |

## Consume

- `bodega`
- `jornada_laboral`
- `operador`
- `operador_bodega`
- `operador_jornada_laboral`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE PROCEDURE asignar_jornada_laboral
@JornadaLaboral as integer
as
declare @IdOperador as integer
declare @IdEmpresa as integer
declare @fetch_outer_cursor int
declare @fetch_inner_cursor int

 DECLARE operadorcursor CURSOR 
FOR SELECT IdOperador FROM operador

OPEN operadorcursor

FETCH NEXT FROM operadorcursor INTO @IdOperador

WHILE @@FETCH_STATUS = 0
    BEGIN
			print '---------------------------------'
            print ' inicia iteracion de operador: ' + STR(@IdOperador)

            declare @IdBodega as integer
            DECLARE bodegacursor CURSOR 
                FOR SELECT IdBodega FROM bodega
            OPEN bodegacursor
            FETCH NEXT FROM bodegacursor INTO @IdBodega      
            
            WHILE @@FETCH_STATUS = 0
                BEGIN

			if EXISTS (select *from operador_bodega where IdOperador=@IdOperador and IdBodega=@IdBodega and activo=1)
			begin
					   print ' inicia iteracion de bodega: ' + STR(@IdBodega)

						if @JornadaLaboral = 0 --no hay jornada especifica, se insertan todas
							begin
								insert into operador_jornada_laboral
								select ROW_NUMBER() OVER(ORDER BY o.IdOperador ASC) + ISNULL((select max(IdOperadorJornadaLaboral) FROM operador_jornada_laboral),0) AS IdOperadorJornadaLaboral,
								o.IdOperador, j.IdBodega, j.IdJornada, j.activo, 1, getdate(),1,getdate()
								from operador o cross join jornada_laboral j
								where j.IdBodega =@IdBodega and o.IdOperador=@IdOperador
								order by o.IdOperador
							end
						else --si hay una jornada especifica, se inserta únicamente esa.
							begin
								insert into operador_jornada_laboral
								select ROW_NUMBER() OVER(ORDER BY o.IdOperador ASC) + ISNULL((select max(IdOperadorJornadaLaboral) FROM operador_jornada_laboral),0) AS IdOperadorJornadaLaboral,
								o.IdOperador, j.IdBodega, j.IdJornada, j.activo, 1, getdate(),1,getdate()
								from operador o cross join jornada_laboral j
								where j.IdBodega =@IdBodega and j.IdJornada = @JornadaLaboral	and o.IdOperador=@IdOperador
							end
			end

                      FETCH NEXT FROM bodegacursor INTO @IdBodega      
                END
            CLOSE bodegacursor;
            DEALLOCATE bodegacursor;
            FETCH NEXT FROM operadorcursor INTO @IdOperador
    END
CLOSE operadorcursor;
DEALLOCATE operadorcursor;
```
