---
id: PLAN-PARALELO
tipo: index
estado: vigente
titulo: Plan de ejecucion paralela — proximas sesiones
tags: [index]
---

# Plan de ejecucion paralela — proximas sesiones

> Objetivo: ejecutar tareas independientes en paralelo (en distintas
> sesiones) para avanzar rapido sin bloquearnos.

## Codificacion de tareas

`P<n>_<categoria>_<slug>` — donde n es el orden propuesto y categoria
es la del INDEX.

## Wave 1 — base de conocimiento (ejecutables en paralelo, no se bloquean)

- [x] `P01_META_INDEX-HUMAN-READABLE` — convencion + arbol maestro (HECHO 29-abr)
- [x] `P02_DATA_DDL-FUNCIONAL-PRODUCTO-BODEGA` — diccionario funcional inicial (HECHO 29-abr)
- [x] `P03_CLIENT_FINGERPRINT-MAMPA` — fingerprint completo MAMPA (HECHO 29-abr)
- [x] `P04_CLIENT_FINGERPRINT-KILLIOS` — fingerprint K7 con la misma plantilla
- [x] `P05_CLIENT_FINGERPRINT-BECOFARMA` — fingerprint BECOFARMA (incluye H29)
- [x] `P06_CLIENT_FINGERPRINT-BYB` — fingerprint B&B
- [x] `P07_CLIENT_FINGERPRINT-CEALSA` — fingerprint CEALSA-QAS

## Wave 2 — depende del fingerprint completo

- [ ] `P08_DATA_PERFILES-PRODUCTO-CROSS-CLIENTE` — agrupar productos
      por tupla `(control_lote, control_vence, control_lp, control_peso)`
      cross-cliente. Detectar perfiles tipicos.
- [ ] `P09_PROC_FLUJOS-DE-DATOS` — para cada predicado del lenguaje
      interno, documentar flujo real de tablas observado en cada cliente
      con queries de validacion.
- [ ] `P10_FEAT_MATRIZ-FINAL-CON-FINGERPRINTS` — completar
      `funcionalidades-por-cliente.md` con datos de los 7 fingerprints.

## Wave 3 — depende de la matriz final

- [ ] `P11_ARCH_ADR-001-WEBAPI-MODELO-A` — armar ADR completo si Erik
      confirma Modelo A (consumidor de outbox).
- [~] `P12_PARAM_INVENTARIO-CAPABILITY-FLAGS` (parcial: K7/BECOFARMA/MAMPA hechos en L-019) — extraer todos los
      capability flags por cliente desde `i_nav_config_enc` y similares.
- [ ] `P13_DATA_SEED-CLIENTE-LIMPIO` — diseñar seed base limpia para
      bootstrap de clientes nuevos. Plantilla: tablas a vaciar +
      tablas con catalogos + flags por defecto.

## Wave 4 — automatizacion

- [ ] `P14_TEST_GENERADOR-SET-PRUEBAS` — script que dado
      `(cliente, predicado)` genere set de pruebas concreto basado en
      el fingerprint del cliente.
- [ ] `P15_DIAG_FINGERPRINT-DIFF` — script que compare fingerprints
      A vs B y muestre diff humano-readable.

## Tareas dependientes de Erik (bloquean Wave 3 y posteriores)

- [ ] H29: validar con cliente BECOFARMA si conoce el corte de marzo
- [ ] H26 followup: decidir log unificado vs segmentado en WebAPI
- [ ] ADR-001: confirmar Modelo A vs B vs C
- [ ] Q-CAPABILITY-FLAG-VERIF: ¿donde vive el flag de verificacion?
- [ ] Q-EC2-DESFASE: calendario de sync EC2 vs productivas
- [ ] Q-MAMPA-ERP: que ERP usa MAMPA
- [ ] Q-MAMPA-CEDIS-PANTALLA-LEGACY: por que bodega 21 tiene pantallas =0
- [ ] H06-H11 ratificacion del Bloque A
