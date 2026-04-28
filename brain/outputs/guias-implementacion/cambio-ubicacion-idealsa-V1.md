---
output_type: guia-implementacion
audience: programador
client: idealsa
version: V1
status: draft
supersedes: cambio-ubicacion-idealsa-V0.md
authored_by: agente-brain
authored_at: 2026-04-28T21:00:00-03:00
ratificacion_pendiente_de:
  - Erik Calderon (PrograX24)
  - Carol (CKFK/KKKL): aclarar E1 sentido del operador, E2 significado de "A y B iguales C y D iguales"
---

# Guia de implementacion V1 - Cambio de ubicacion 100% restrictivo (Idealsa)

> **Audiencia**: programador que va a implementar el modulo en BOF.NET, WebService y la nueva WebAPI .NET 10.
>
> **Cambios respecto a V0**:
> 1. Stack corregido: WebAPI **.NET 10** (no .NET 8). El .NET 8 esta reservado para el bridge brain<->WMS y NO se toca en este sprint.
> 2. Flujo HH corregido: la HH **NO** habla REST directo a la WebAPI. La HH consume el WebService existente, y el WebService se modifica para internamente delegar la validacion al endpoint REST de la WebAPI .NET 10.

---

## 0 - Contexto y alcance

### Cliente y escenario
- **Cliente destino**: Idealsa (tenant existente, no BD nueva).
- **Estado actual de Idealsa**: NO tiene reglas de ubicacion configuradas (`regla_ubic_enc.rows = 0` para `IdEmpresa = <ID_IDEALSA>`).
- **Cliente de referencia**: Killios (`TOMWMS_KILLIOS_PRD`) tiene 1.102 filas en `ubicaciones_por_regla` y un set funcional de reglas - sirve como patron de poblacion.
- **Hito de la consulta**: Carol pide que el cambio de ubicacion (en BOF y en HH) se valide contra la regla de ubicacion **siempre**, con mensaje 100% restrictivo, salvo dos excepciones (E1 downgrade rotacion + E2 mismo producto).

### Stack confirmado
| Componente | Stack | Rol en este modulo |
|---|---|---|
| **BOF** | .NET (VB) - Forms | Cliente directo de la WebAPI .NET 10 (consume REST). |
| **HH** | Android (Java/Kotlin) | Sigue consumiendo el WebService existente, sin cambios de su lado. |
| **WebService** | (legacy, existente) | Se MODIFICA: internamente delega validacion y ejecucion al endpoint REST de la WebAPI .NET 10. Misma interfaz hacia la HH. |
| **WebAPI nueva** | **.NET 10** | Hospeda la logica de regla de ubicacion. Endpoints `/cambio-ubicacion/validar` y `/cambio-ubicacion/ejecutar`. |
| **Bridge brain<->WMS** | .NET 8 | **No se toca en este sprint.** Reservado para intercambio con el brain. |
| **Base de datos** | SQL Server (3 tenants: Killios, B&B, Cealsa - Idealsa entra como nuevo tenant en una existente) | Modelo `regla_ubic_*` ya formalizado. |

### Decisiones arquitectonicas tomadas en esta guia
| # | Decision | Valor | Justificacion |
|---|---|---|---|
| D1 | Modo cuando ubicacion no tiene regla asignada | **Bloquear** (modo restrictivo total) | Coherente con el espiritu "100% restrictivo" de Carol. En Idealsa siempre habra regla, pero el codigo debe ser defensivo. |
| D2 | Override por usuario/rol | **Ninguno** | Carol explicito: "mensaje 100% restrictivo". Sin permiso especial, sin supervisor. |
| D3 | Catalogo de mensajes | Tabla `mensaje_regla` (hoy 10 filas, sin enlace a `regla_ubic_*`) - se cablea via cambio de schema minimo (ver pieza 6). |
| D4 | Donde valida | **WebAPI .NET 10 server-side** obligatorio. BOF llama directo. HH llama via WebService que delega. |
| D5 | Bootstrap de regla en Idealsa | SQL seed parametrizado tomando estructura de Killios como referencia. |
| D6 | Aplicabilidad por cliente | Activable por flag bit en `dbo.cliente`. Default OFF, ON para Idealsa. |
| D7 | Stack WebAPI | **.NET 10** (no .NET 8) | El .NET 8 esta reservado al bridge brain<->WMS. |
| D8 | Flujo HH | HH -> WebService (existente, modificado) -> WebAPI .NET 10 | La HH no cambia su contrato. El WebService se vuelve un proxy delgado para enforcement. |

### Lo que esta fuera de scope de esta guia
- Reglas para clientes distintos de Idealsa (Killios mantiene su comportamiento legacy).
- Migracion del codigo viejo de BOF (esta guia define el TARGET, no el path de migracion).
- Cambios en el contrato HH<->WebService (la HH queda intacta).
- Aclaraciones de Carol sobre E1 y E2: marcadas como `TODO-CAROL` en el codigo.

---

## 1 - Bootstrap: crear las reglas de ubicacion para Idealsa

### 1.1 Plan de poblacion

Para que el enforcement funcione necesitamos, en orden:

1. Una fila en `regla_ubic_enc` por cada combinacion `(IdEmpresa, IdBodega)` de Idealsa que vaya a usar enforcement.
2. Detalles de la regla en al menos `regla_ubic_det_ir` (por indice de rotacion) y `regla_ubic_det_pe` (por estado producto = solo DISPONIBLE).
3. Asignacion de cada ubicacion activa de Idealsa a la regla via `regla_ubicacion`.
4. Refrescar (o repoblar) la tabla materializada `ubicaciones_por_regla`.
5. Poblar `mensaje_regla` con los textos a mostrar (ver pieza 6).

### 1.2 SQL seed parametrico

> **Importante**: este script es plantilla. Reemplazar `@IdEmpresaIdealsa` por el valor real del tenant Idealsa. Antes de correrlo en QAS o PRD, consultar con Erik los `IdBodega` que entran en scope.

```sql
DECLARE @IdEmpresaIdealsa INT = <ID_IDEALSA>;       -- pedir a Erik
DECLARE @UsuarioSeed NVARCHAR(25) = N'SEED_BRAIN';
DECLARE @Fecha DATETIME = GETDATE();

-- Paso 1: regla cabecera por cada bodega activa de Idealsa
INSERT INTO dbo.regla_ubic_enc (IdEmpresa, IdBodega, Nombre, Activo, user_agr, fec_agr)
SELECT @IdEmpresaIdealsa, b.IdBodega,
       CONCAT(N'Regla restrictiva default - ', b.descripcion),
       1, @UsuarioSeed, @Fecha
FROM dbo.bodega b
WHERE b.IdEmpresa = @IdEmpresaIdealsa
  AND b.activo = 1
  AND NOT EXISTS (
      SELECT 1 FROM dbo.regla_ubic_enc rue
      WHERE rue.IdEmpresa = @IdEmpresaIdealsa AND rue.IdBodega = b.IdBodega
  );

-- Paso 2: detalle por indice de rotacion (acepta todos los indices que tienen sentido)
-- La excepcion E1 (downgrade) la implementa el motor en runtime, no se modela aqui.
INSERT INTO dbo.regla_ubic_det_ir (IdReglaUbicacionEnc, IdIndiceRotacion, Activo, user_agr, fec_agr)
SELECT rue.IdReglaUbicacionEnc, ir.IdIndiceRotacion, 1, @UsuarioSeed, @Fecha
FROM dbo.regla_ubic_enc rue
CROSS JOIN dbo.indice_rotacion ir
WHERE rue.IdEmpresa = @IdEmpresaIdealsa
  AND ir.Activo = 1
  AND NOT EXISTS (
      SELECT 1 FROM dbo.regla_ubic_det_ir d
      WHERE d.IdReglaUbicacionEnc = rue.IdReglaUbicacionEnc
        AND d.IdIndiceRotacion = ir.IdIndiceRotacion
  );

-- Paso 3: detalle por estado producto (solo DISPONIBLE)
INSERT INTO dbo.regla_ubic_det_pe (IdReglaUbicacionEnc, IdProductoEstado, Activo, user_agr, fec_agr)
SELECT rue.IdReglaUbicacionEnc, pe.IdProductoEstado, 1, @UsuarioSeed, @Fecha
FROM dbo.regla_ubic_enc rue
CROSS JOIN dbo.producto_estado pe
WHERE rue.IdEmpresa = @IdEmpresaIdealsa
  AND pe.NomEstado IN (N'DISPONIBLE')
  AND NOT EXISTS (SELECT 1 FROM dbo.regla_ubic_det_pe d
                  WHERE d.IdReglaUbicacionEnc = rue.IdReglaUbicacionEnc
                    AND d.IdProductoEstado = pe.IdProductoEstado);

-- Paso 4: asignar ubicaciones a la regla de su bodega
INSERT INTO dbo.regla_ubicacion (IdUbicacion, IdReglaUbicacionEnc, IdBodega)
SELECT bu.IdUbicacion, rue.IdReglaUbicacionEnc, bu.IdBodega
FROM dbo.bodega_ubicacion bu
JOIN dbo.regla_ubic_enc rue ON rue.IdBodega = bu.IdBodega AND rue.IdEmpresa = @IdEmpresaIdealsa
WHERE bu.activo = 1
  AND ISNULL(bu.bloqueada, 0) = 0
  AND ISNULL(bu.ubicacion_merma, 0) = 0
  AND NOT EXISTS (
      SELECT 1 FROM dbo.regla_ubicacion ru
      WHERE ru.IdUbicacion = bu.IdUbicacion AND ru.IdReglaUbicacionEnc = rue.IdReglaUbicacionEnc
  );

-- Paso 5: refrescar la tabla materializada (ejecutar el SP que la repuebla, si existe;
-- de lo contrario, truncate + recompute desde un job nocturno).
-- TODO-DEV: confirmar con DBA si ubicaciones_por_regla se actualiza por trigger o por job.
```

### 1.3 Verificacion post-seed

```sql
-- Esperado: una fila por bodega activa de Idealsa
SELECT COUNT(*) AS reglas FROM dbo.regla_ubic_enc WHERE IdEmpresa = <ID_IDEALSA>;

-- Esperado: una fila por (regla x indice_rotacion activa)
SELECT COUNT(*) AS detalles_ir FROM dbo.regla_ubic_det_ir d
JOIN dbo.regla_ubic_enc rue ON rue.IdReglaUbicacionEnc = d.IdReglaUbicacionEnc
WHERE rue.IdEmpresa = <ID_IDEALSA>;

-- Esperado: ~ todas las ubicaciones activas, no bloqueadas, no merma de Idealsa
SELECT COUNT(*) AS asignaciones FROM dbo.regla_ubicacion ru
JOIN dbo.regla_ubic_enc rue ON rue.IdReglaUbicacionEnc = ru.IdReglaUbicacionEnc
WHERE rue.IdEmpresa = <ID_IDEALSA>;
```

---

## 2 - Modelo de dominio en .NET 10

> Asume Clean Architecture o Vertical Slices con MediatR + FluentValidation. Si el stack final es otro, ajustar nombres pero mantener responsabilidades.
>
> Las APIs de C# usadas (records, primary constructors, async, EF Core / Dapper) son compatibles con .NET 10.

### 2.1 Entidades de dominio

```csharp
// Domain/Entities/CambioUbicacionRequest.cs
public sealed record CambioUbicacionRequest(
    int IdProducto,
    int IdProductoBodega,
    int IdStock,
    int IdUbicacionOrigen,
    int IdUbicacionDestino,
    int IdBodega,
    int IdEmpresa,
    string UsuarioOperador,
    string OrigenLlamada      // "BOF" / "WS_HH" / "API" - para auditoria
);

// Domain/Entities/ResultadoValidacion.cs
public sealed record ResultadoValidacion(
    bool Permitido,
    string CodigoMotivo,        // R0_NO_REGLA, R0_PRODUCTO_NO_CUMPLE, E1_DOWNGRADE_OK, E2_MISMO_PRODUCTO_OK, LEGACY_PASSTHROUGH
    int? IdMensajeRegla,
    string MensajeUsuario,
    string DetalleTecnico       // para auditoria, no se muestra al usuario
);
```

### 2.2 Servicio principal

```csharp
// Domain/Services/IReglaUbicacionService.cs
public interface IReglaUbicacionService
{
    Task<ResultadoValidacion> ValidarAsync(CambioUbicacionRequest req, CancellationToken ct);
}

// Application/Services/ReglaUbicacionService.cs
public sealed class ReglaUbicacionService(
    IReglaUbicacionRepository repo,
    IMensajeReglaRepository msgRepo,
    IClienteFlagsRepository flagsRepo) : IReglaUbicacionService
{
    public async Task<ResultadoValidacion> ValidarAsync(CambioUbicacionRequest req, CancellationToken ct)
    {
        // Paso 0 - flag de cliente: si NO tiene enforcement strict, passthrough legacy
        var enforce = await flagsRepo.GetEnforceReglaUbicacionStrictAsync(req.IdEmpresa, ct);
        if (!enforce)
            return Permitir("LEGACY_PASSTHROUGH", regla: null, "Cliente sin enforcement strict.");

        // Paso 1 - traer regla asignada a la ubicacion destino
        var regla = await repo.GetReglaPorUbicacionAsync(req.IdUbicacionDestino, req.IdBodega, ct);
        if (regla is null)
            return await NegarAsync("R0_NO_REGLA", regla: null, req, ct);

        // Paso 2 - chequear que el producto cumple los detalles de la regla (R0)
        var producto = await repo.GetProductoConRotacionAsync(req.IdProducto, ct);
        var ubicDestino = await repo.GetUbicacionConRotacionAsync(req.IdUbicacionDestino, ct);

        var cumpleR0 = await repo.ProductoCumpleReglaAsync(producto, regla, ct);
        if (cumpleR0)
            return Permitir("R0_OK", regla, "Cumple regla.");

        // Paso 3 - si NO cumple R0, evaluar E1 (downgrade rotacion)
        if (EvaluarE1(producto, ubicDestino))
            return Permitir("E1_DOWNGRADE_OK", regla, "Permitido por excepcion downgrade rotacion.");

        // Paso 4 - evaluar E2 (mismo producto en origen y destino)
        if (await EvaluarE2Async(req, ct))
            return Permitir("E2_MISMO_PRODUCTO_OK", regla, "Permitido por excepcion consolidacion mismo SKU.");

        return await NegarAsync("R0_PRODUCTO_NO_CUMPLE", regla, req, ct);
    }

    private static bool EvaluarE1(ProductoConRotacion p, UbicacionConRotacion u)
    {
        // TODO-CAROL: confirmar el sentido del operador.
        //   Lectura A: producto MAS RAPIDO (IndicePrioridad menor) puede ir a ubicacion MAS LENTA (IndicePrioridad mayor)
        //              -> return p.IndicePrioridad < u.IndicePrioridad;
        //   Lectura B: producto numericamente MAYOR puede ir a ubicacion numericamente MENOR
        //              -> return p.IndicePrioridad > u.IndicePrioridad;
        // Hasta aclarar, NO permitir E1 (ser conservador).
        return false;
    }

    private async Task<bool> EvaluarE2Async(CambioUbicacionRequest req, CancellationToken ct)
    {
        // TODO-CAROL: confirmar significado de "A y B iguales C y D iguales".
        //   Lectura b: el SKU en origen y destino debe ser el mismo, MAS coincidencia de
        //              atributos (lote, vencimiento, presentacion, calidad).
        //   Lectura c: coordenadas A,B coinciden en una posicion y C,D en otra.
        // Hasta aclarar, exigir como minimo: mismo IdProducto en ambas ubicaciones.
        var stockOrigen = await repo.GetStockEnUbicacionAsync(req.IdUbicacionOrigen, ct);
        var stockDestino = await repo.GetStockEnUbicacionAsync(req.IdUbicacionDestino, ct);
        return stockOrigen.Any(s => s.IdProducto == req.IdProducto)
            && stockDestino.Any(s => s.IdProducto == req.IdProducto);
    }

    private async Task<ResultadoValidacion> NegarAsync(string codigo, ReglaUbicacion? regla, CambioUbicacionRequest req, CancellationToken ct)
    {
        var msg = await msgRepo.GetMensajePorReglaAsync(regla?.IdReglaUbicacionEnc, codigo, ct);
        return new ResultadoValidacion(
            Permitido: false,
            CodigoMotivo: codigo,
            IdMensajeRegla: msg?.IdMensajeRegla,
            MensajeUsuario: msg?.Texto ?? "El producto no puede ubicarse en esta posicion segun la regla de ubicacion.",
            DetalleTecnico: $"req={req.IdProducto}@{req.IdUbicacionDestino} regla={regla?.IdReglaUbicacionEnc}"
        );
    }

    private static ResultadoValidacion Permitir(string codigo, ReglaUbicacion? regla, string detalle)
        => new(Permitido: true, CodigoMotivo: codigo, IdMensajeRegla: null,
               MensajeUsuario: string.Empty, DetalleTecnico: detalle);
}
```

### 2.3 Repositorio - consultas SQL claves

```csharp
// Infrastructure/Repositories/ReglaUbicacionRepository.cs
// Query 1: regla asignada a una ubicacion en una bodega
public async Task<ReglaUbicacion?> GetReglaPorUbicacionAsync(int idUbicacion, int idBodega, CancellationToken ct)
{
    const string sql = @"
        SELECT TOP 1 rue.IdReglaUbicacionEnc, rue.Nombre, rue.IdEmpresa, rue.IdBodega, rue.Activo
        FROM dbo.regla_ubicacion ru
        JOIN dbo.regla_ubic_enc rue ON rue.IdReglaUbicacionEnc = ru.IdReglaUbicacionEnc
        WHERE ru.IdUbicacion = @IdUbicacion
          AND ru.IdBodega = @IdBodega
          AND rue.Activo = 1
        ORDER BY rue.IdReglaUbicacionEnc DESC;"; // si hay varias activas, queda priorizacion para regla_ubic_prio
    // ... ejecutar
}

// Query 2: el producto cumple la regla (chequea TODOS los detalles activos por categoria)
public async Task<bool> ProductoCumpleReglaAsync(ProductoConRotacion p, ReglaUbicacion regla, CancellationToken ct)
{
    const string sql = @"
        SELECT
          (SELECT COUNT(*) FROM dbo.regla_ubic_det_ir   WHERE IdReglaUbicacionEnc=@R AND Activo=1 AND IdIndiceRotacion=@IdRot)   AS m_ir,
          (SELECT COUNT(*) FROM dbo.regla_ubic_det_tr   WHERE IdReglaUbicacionEnc=@R AND Activo=1 AND IdTipoRotacion=@IdTipoRot) AS m_tr,
          (SELECT COUNT(*) FROM dbo.regla_ubic_det_pe   WHERE IdReglaUbicacionEnc=@R AND Activo=1 AND IdProductoEstado=@IdEst)   AS m_pe,
          (SELECT COUNT(*) FROM dbo.regla_ubic_det_pp   WHERE IdReglaUbicacionEnc=@R AND Activo=1 AND IdPresentacion=@IdPres)    AS m_pp,
          (SELECT COUNT(*) FROM dbo.regla_ubic_det_prop WHERE IdReglaUbicacionEnc=@R AND Activo=1 AND IdPropietario=@IdProp)     AS m_prop,
          (SELECT COUNT(*) FROM dbo.regla_ubic_det_tp   WHERE IdReglaUbicacionEnc=@R AND Activo=1 AND IdProductoTipo=@IdTipo)    AS m_tp,
          -- counts de detalles activos (para diferenciar 'sin filtro' vs 'no aplica')
          (SELECT COUNT(*) FROM dbo.regla_ubic_det_ir   WHERE IdReglaUbicacionEnc=@R AND Activo=1) AS t_ir,
          (SELECT COUNT(*) FROM dbo.regla_ubic_det_tr   WHERE IdReglaUbicacionEnc=@R AND Activo=1) AS t_tr,
          (SELECT COUNT(*) FROM dbo.regla_ubic_det_pe   WHERE IdReglaUbicacionEnc=@R AND Activo=1) AS t_pe,
          (SELECT COUNT(*) FROM dbo.regla_ubic_det_pp   WHERE IdReglaUbicacionEnc=@R AND Activo=1) AS t_pp,
          (SELECT COUNT(*) FROM dbo.regla_ubic_det_prop WHERE IdReglaUbicacionEnc=@R AND Activo=1) AS t_prop,
          (SELECT COUNT(*) FROM dbo.regla_ubic_det_tp   WHERE IdReglaUbicacionEnc=@R AND Activo=1) AS t_tp;";
    // Logica: para cada categoria de detalle, si t_X = 0 -> no hay filtro (pasa).
    //         Si t_X > 0 -> exige m_X >= 1 (el producto matchea al menos un detalle activo).
}
```

### 2.4 Validacion con FluentValidation (entrada del endpoint)

```csharp
public sealed class CambioUbicacionRequestValidator : AbstractValidator<CambioUbicacionRequest>
{
    public CambioUbicacionRequestValidator()
    {
        RuleFor(x => x.IdProducto).GreaterThan(0);
        RuleFor(x => x.IdUbicacionOrigen).GreaterThan(0);
        RuleFor(x => x.IdUbicacionDestino).GreaterThan(0)
            .NotEqual(x => x.IdUbicacionOrigen).WithMessage("Origen y destino deben ser distintos.");
        RuleFor(x => x.IdBodega).GreaterThan(0);
        RuleFor(x => x.IdEmpresa).GreaterThan(0);
        RuleFor(x => x.UsuarioOperador).NotEmpty();
        RuleFor(x => x.OrigenLlamada).Must(o => o is "BOF" or "WS_HH" or "API")
            .WithMessage("OrigenLlamada debe ser BOF, WS_HH o API.");
    }
}
```

---

## 3 - Endpoints WebAPI .NET 10

### 3.1 Especificacion

| Endpoint | Verbo | Proposito | Idempotente |
|---|---|---|---|
| `/api/cambio-ubicacion/validar` | POST | Solo valida (read-only). Util para que el cliente preview el resultado antes de tocar el boton. | Si |
| `/api/cambio-ubicacion/ejecutar` | POST | Vuelve a validar y persiste el cambio si pasa. Devuelve el mismo `ResultadoValidacion` + `IdMovimiento` si OK. | No (genera mov) |

### 3.2 Contratos

```json
// Request (ambos endpoints)
{
  "idProducto": 12345,
  "idProductoBodega": 67890,
  "idStock": 11111,
  "idUbicacionOrigen": 222,
  "idUbicacionDestino": 333,
  "idBodega": 1,
  "idEmpresa": 5,
  "usuarioOperador": "carol.r",
  "origenLlamada": "BOF"
}

// Response (validar)
{
  "permitido": false,
  "codigoMotivo": "R0_PRODUCTO_NO_CUMPLE",
  "idMensajeRegla": 7,
  "mensajeUsuario": "El producto AGUA MINERAL 1.5L no puede ubicarse en P-A-03-02: la ubicacion requiere rotacion A y el producto es rotacion C.",
  "detalleTecnico": "req=12345@333 regla=42"
}

// Response (ejecutar - caso OK)
{
  "permitido": true,
  "codigoMotivo": "E2_MISMO_PRODUCTO_OK",
  "idMensajeRegla": null,
  "mensajeUsuario": "",
  "detalleTecnico": "req=12345@333 regla=42",
  "idMovimiento": 998877
}
```

### 3.3 Handlers (MediatR)

```csharp
public sealed record ValidarCambioUbicacionCommand(CambioUbicacionRequest Req) : IRequest<ResultadoValidacion>;

public sealed class ValidarCambioUbicacionHandler(IReglaUbicacionService svc)
    : IRequestHandler<ValidarCambioUbicacionCommand, ResultadoValidacion>
{
    public Task<ResultadoValidacion> Handle(ValidarCambioUbicacionCommand cmd, CancellationToken ct)
        => svc.ValidarAsync(cmd.Req, ct);
}

public sealed record EjecutarCambioUbicacionCommand(CambioUbicacionRequest Req) : IRequest<EjecutarResult>;

public sealed class EjecutarCambioUbicacionHandler(
    IReglaUbicacionService svc,
    IMovimientoRepository mov,
    IUnitOfWork uow) : IRequestHandler<EjecutarCambioUbicacionCommand, EjecutarResult>
{
    public async Task<EjecutarResult> Handle(EjecutarCambioUbicacionCommand cmd, CancellationToken ct)
    {
        var validacion = await svc.ValidarAsync(cmd.Req, ct);
        if (!validacion.Permitido)
            return new EjecutarResult(validacion, IdMovimiento: null);

        await using var tx = await uow.BeginTransactionAsync(ct);
        var idMov = await mov.PersistirCambioAsync(cmd.Req, validacion.CodigoMotivo, ct);
        await tx.CommitAsync(ct);
        return new EjecutarResult(validacion, IdMovimiento: idMov);
    }
}
```

### 3.4 Auditoria

Cada llamada a `/ejecutar` (OK o NEG) deja registro en una tabla nueva o en logs estructurados. Usa `OrigenLlamada` para distinguir BOF / HH (via WS) / API directa:

```sql
CREATE TABLE dbo.audit_cambio_ubicacion (
  IdAudit              BIGINT IDENTITY PRIMARY KEY,
  Fecha                DATETIME NOT NULL DEFAULT GETDATE(),
  Usuario              NVARCHAR(50) NOT NULL,
  IdProducto           INT NOT NULL,
  IdUbicacionOrigen    INT NOT NULL,
  IdUbicacionDestino   INT NOT NULL,
  IdBodega             INT NOT NULL,
  Permitido            BIT NOT NULL,
  CodigoMotivo         NVARCHAR(40) NOT NULL,
  IdReglaUbicacionEnc  INT NULL,
  MensajeUsuario       NVARCHAR(500) NULL,
  Origen               NVARCHAR(20) NOT NULL  -- 'BOF' / 'WS_HH' / 'API'
);
```

---

## 4 - Integracion con BOF .NET (VB)

> **TODO-DEV**: identificar el Form actual que maneja "Cambio de ubicacion". Habitualmente vive bajo `frmInventario*` o `frmMovimiento*`. Buscar por el texto del label / boton "Cambio de ubicacion" o "Reubicar".
>
> El BOF llama directo a la WebAPI .NET 10 - no pasa por el WebService.

### 4.1 Patron de integracion

```vbnet
' Antes de persistir, llamar al endpoint de validacion (sincronico).
Private Async Function btnGuardar_Click() As Task
    Dim req = New CambioUbicacionRequest With {
        .IdProducto = Me.IdProductoSeleccionado,
        .IdUbicacionOrigen = Me.UbicOrigen,
        .IdUbicacionDestino = Me.UbicDestino,
        .IdBodega = SesionActual.IdBodega,
        .IdEmpresa = SesionActual.IdEmpresa,
        .UsuarioOperador = SesionActual.Usuario,
        .OrigenLlamada = "BOF"
    }

    Dim res = Await ApiClient.PostAsync(Of ResultadoValidacion)("/api/cambio-ubicacion/validar", req)

    If Not res.Permitido Then
        MessageBox.Show(res.MensajeUsuario, "Cambio no permitido", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        Return ' NO ofrecer override - el mensaje es 100% restrictivo
    End If

    ' Si paso la validacion, ejecutar
    Dim ejec = Await ApiClient.PostAsync(Of EjecutarResult)("/api/cambio-ubicacion/ejecutar", req)
    If ejec.Permitido Then
        MessageBox.Show("Cambio realizado.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Me.RefrescarGrilla()
    Else
        ' caso raro: paso validar pero fallo ejecutar (race condition de regla)
        MessageBox.Show(ejec.MensajeUsuario, "Cambio no permitido", MessageBoxButtons.OK, MessageBoxIcon.Stop)
    End If
End Function
```

### 4.2 Que hay que TOCAR en BOF

- **Form de cambio de ubicacion** (TODO-DEV: nombre del Form): reemplazar el SQL inline o llamada al SP por la llamada al endpoint REST de la WebAPI .NET 10.
- **DAL legacy**: dejar el metodo viejo deprecated por una release, pero sin invocaciones nuevas.
- **Capa de comunicacion HTTP**: si el BOF no tiene `ApiClient`, agregar uno con `HttpClient` + `System.Text.Json`. Single instance via `Lazy(Of HttpClient)`.
- **Manejo de errores de red**: si la API esta caida, NO permitir el cambio (fail-closed). Mostrar mensaje "no se pudo validar la regla, intente nuevamente".

### 4.3 Que NO hay que tocar

- La logica de impresion de etiquetas, refresco de stock, etc. - sigue como esta.
- El form de configuracion de reglas de ubicacion - en este sprint solo se consume, no se modifica.

---

## 5 - Integracion con HH (via WebService existente)

> **CLAVE**: la HH NO se modifica. El cambio vive en el **WebService existente** que se vuelve un proxy delgado hacia la WebAPI .NET 10.
>
> Flujo: `HH (Android, sin cambios) -> WebService (modificado internamente) -> WebAPI .NET 10 -> SQL`

### 5.1 Diagrama del flujo

```
+--------+      SOAP/legacy      +--------------+    HTTP REST     +-----------------+
|  HH    | --------------------> |  WebService  | --------------> | WebAPI .NET 10  |
| (sin   |                       | (proxy nuevo |                 | /cambio-ubicac/ |
| cambio)|                       |  comportam.) |                 |  validar +      |
+--------+                       +--------------+                 |  ejecutar       |
                                                                  +--------+--------+
                                                                            |
                                                                            v
                                                                       SQL Server
                                                                       (regla_ubic_*)
```

### 5.2 Que hay que TOCAR en el WebService

Los metodos del WebService que hoy resuelven el cambio de ubicacion (TODO-DEV: identificar nombres exactos, suelen llamarse `MoverStock`, `CambiarUbicacion`, `ReubicarProducto` o similar) deben ser modificados para:

1. **NO ejecutar SQL directo** ni llamar a SPs legacy de cambio de ubicacion.
2. Construir un `CambioUbicacionRequest` desde los parametros que recibe del HH.
3. Llamar via HTTP al endpoint `/api/cambio-ubicacion/ejecutar` de la WebAPI .NET 10.
4. Mapear la respuesta de la WebAPI al formato que la HH espera (mantener compatibilidad con el contrato actual del WebService).

```csharp
// Pseudocodigo del metodo del WebService (sintaxis exacta depende de WCF/ASMX/etc.)
public ReubicarRespuestaLegacy ReubicarProductoHH(ReubicarPeticionLegacy peticion)
{
    var req = new CambioUbicacionRequest(
        IdProducto: peticion.IdProducto,
        IdProductoBodega: peticion.IdProductoBodega,
        IdStock: peticion.IdStock,
        IdUbicacionOrigen: peticion.UbicOrigen,
        IdUbicacionDestino: peticion.UbicDestino,
        IdBodega: peticion.IdBodega,
        IdEmpresa: peticion.IdEmpresa,
        UsuarioOperador: peticion.UsuarioHH,
        OrigenLlamada: "WS_HH"   // <-- importante para auditoria
    );

    // Llamar al endpoint de la WebAPI .NET 10
    var resp = _httpClient.PostAsJsonAsync("/api/cambio-ubicacion/ejecutar", req).Result;
    var ejec = resp.Content.ReadFromJsonAsync<EjecutarResult>().Result;

    // Mapear al contrato legacy que espera la HH
    return new ReubicarRespuestaLegacy
    {
        Exitoso = ejec.Validacion.Permitido,
        Mensaje = ejec.Validacion.MensajeUsuario,
        IdMovimiento = ejec.IdMovimiento ?? 0,
        CodigoMotivo = ejec.Validacion.CodigoMotivo  // si la HH puede mostrarlo
    };
}
```

### 5.3 Que NO se toca en la HH

- El codigo Android queda **intacto**. No hay release de HH para este sprint.
- El contrato del WebService hacia la HH se mantiene (mismos parametros de entrada y salida del metodo).
- La UX de la HH (mensaje de error, vibracion, bip) sigue como esta - simplemente ahora el mensaje de error vendra del catalogo `mensaje_regla` via la WebAPI.

### 5.4 Modo offline en HH

> **TODO-CAROL**: confirmar si la HH puede operar offline para cambio de ubicacion.

Recomendacion del brain: **modo online obligatorio para cambio de ubicacion**. La regla restrictiva 100% es incompatible con confirmar localmente y sincronizar despues - habria un periodo donde el usuario cree que esta hecho y al sync el server lo niega, dejando inventario inconsistente. Si la HH ya tiene logica offline para esta operacion, el WebService tiene que ser estricto y rechazar peticiones que vengan en lote sin garantia de validacion en linea.

### 5.5 Configuracion del cliente HTTP en el WebService

- **Timeout**: 5 segundos a la WebAPI. Si timeout, el WebService devuelve a la HH el mensaje "no se pudo validar, reintente" (fail-closed).
- **Retry**: solo en errores de red transitorios (connection reset). NO retry en respuestas 4xx ni 5xx con cuerpo de validacion.
- **Url base de la WebAPI**: en archivo de configuracion del WebService. Distintos valores por ambiente (DEV/QAS/PRD).
- **Logging**: log de cada llamada a la WebAPI con `OrigenLlamada=WS_HH` y latencia, para detectar problemas de performance.

---

## 6 - Cableado de mensajes (cambio de schema minimo)

### 6.1 Estado actual y gap

`mensaje_regla` (10 filas, modify 2016-07-22) tiene columnas: `IdMensajeRegla, Nombre, fec_agr, user_agr, fec_mod, user_mod, activo`.

Problemas:
1. No hay columna `Texto` ni `Plantilla` - solo `Nombre`. Hay que decidir si `Nombre` ES el texto o agregar una columna nueva.
2. Solo es referenciada por `propietario_reglas_enc` - **NO esta enlazada a `regla_ubic_*`**.

### 6.2 Cambio de schema propuesto

```sql
-- Agregar columna de plantilla de texto (con placeholders)
ALTER TABLE dbo.mensaje_regla
ADD Plantilla NVARCHAR(500) NULL;

-- Cablear mensaje a regla_ubic_enc (mensaje default cuando esa regla rechaza)
ALTER TABLE dbo.regla_ubic_enc
ADD IdMensajeReglaDefault INT NULL,
    CONSTRAINT FK_regla_ubic_enc_mensaje_regla
    FOREIGN KEY (IdMensajeReglaDefault) REFERENCES dbo.mensaje_regla(IdMensajeRegla);
```

### 6.3 Catalogo inicial sugerido

```sql
INSERT INTO dbo.mensaje_regla (Nombre, Plantilla, activo, fec_agr, user_agr)
VALUES
 (N'RELOC_PRODUCTO_NO_CUMPLE',
  N'El producto {Producto} no puede ubicarse en {Ubicacion}: la regla "{Regla}" exige {RequisitoFaltante}.',
  1, GETDATE(), N'SEED_BRAIN'),
 (N'RELOC_NO_HAY_REGLA',
  N'La ubicacion {Ubicacion} no tiene regla de ubicacion asignada. No se permite recibir productos hasta que se configure.',
  1, GETDATE(), N'SEED_BRAIN'),
 (N'RELOC_UBIC_BLOQUEADA',
  N'La ubicacion {Ubicacion} esta bloqueada. No se puede mover stock hacia ella.',
  1, GETDATE(), N'SEED_BRAIN');
```

### 6.4 Render de placeholders

El `IMensajeReglaRepository.GetMensajePorReglaAsync(...)` devuelve la `Plantilla`. El servicio aplica un render simple (sustitucion de `{Token}` por valor) antes de devolverla en `ResultadoValidacion.MensajeUsuario`. No hay logica de localizacion - todo en espanol.

---

## 7 - Tests obligatorios

> Implementar como xUnit + Testcontainers (SQL Server) o como integration tests contra una BD de test sembrada con el seed de la pieza 1.

| # | Caso | Setup | Esperado |
|---|---|---|---|
| T1 | Producto cumple regla -> permitido | Producto rotacion A, ubicacion con regla que acepta rotacion A | `Permitido=true, CodigoMotivo=R0_OK` |
| T2 | Producto NO cumple regla, sin excepciones -> bloqueado | Producto rotacion C, ubicacion con regla que solo acepta A y B, distinto SKU en destino | `Permitido=false, CodigoMotivo=R0_PRODUCTO_NO_CUMPLE` |
| T3 | Ubicacion sin regla asignada -> bloqueado | Ubicacion existente pero sin fila en `regla_ubicacion` | `Permitido=false, CodigoMotivo=R0_NO_REGLA` |
| T4 | E1 downgrade rotacion (placeholder) | Producto rotacion A, ubicacion con regla que solo acepta C | Pendiente de aclaracion Carol - hoy bloqueado |
| T5 | E2 mismo producto en ambas ubicaciones | Stock del mismo IdProducto en origen y destino, regla del destino no acepta | `Permitido=true, CodigoMotivo=E2_MISMO_PRODUCTO_OK` |
| T6 | Origen = destino -> rechazado por validador de entrada | Mismo IdUbicacion en origen y destino | HTTP 400 desde FluentValidation |
| T7 | Race condition: validar OK pero ejecutar bloquea | Cambiar la regla entre validar y ejecutar | `ejecutar` devuelve permitido=false sin generar movimiento |
| T8 | Endpoint caido -> cliente fail-closed | Detener WebAPI .NET 10 | BOF muestra error y NO permite cambio. WebService devuelve a HH error que NO permite cambio. |
| T9 | Cliente sin enforcement (Killios) | `cliente.enforce_regla_ubicacion_strict = 0` | `Permitido=true, CodigoMotivo=LEGACY_PASSTHROUGH` |
| T10 | Llamada desde WS_HH | `OrigenLlamada=WS_HH` con request valido | Auditoria registra `Origen=WS_HH` |

---

## 8 - Activacion por cliente (rollout)

### 8.1 Flag por cliente

> Agregar una decima columna bit en `dbo.cliente` (hoy hay 9 columnas bit).

```sql
ALTER TABLE dbo.cliente
ADD enforce_regla_ubicacion_strict BIT NOT NULL DEFAULT 0;

-- Activar para Idealsa
UPDATE dbo.cliente
SET enforce_regla_ubicacion_strict = 1
WHERE IdCliente = <ID_IDEALSA>;
```

### 8.2 Logica en el servicio

Ya esta integrada en `ReglaUbicacionService.ValidarAsync` (paso 0 del flujo): si el flag esta OFF, devuelve `LEGACY_PASSTHROUGH` y permite el cambio sin tocar reglas. Killios queda en passthrough durante el rollout para no romper su operativa actual. Idealsa estrena enforcement desde el primer cambio de ubicacion.

---

## 9 - Checklist de PR (para revisor)

- [ ] Seed SQL ejecutado en QAS de Idealsa y verificado con las 3 queries de la pieza 1.3.
- [ ] `regla_ubic_enc.IdMensajeReglaDefault` creada y poblada con un mensaje generico para cada regla nueva.
- [ ] `cliente.enforce_regla_ubicacion_strict = 1` para Idealsa, `= 0` para todos los demas (verificar Killios sigue operando).
- [ ] Tests T1-T10 pasan en CI.
- [ ] Form de BOF actualizado y probado manualmente con un caso bloqueado y uno permitido (BOF -> WebAPI directa).
- [ ] Metodo del WebService modificado y probado con la HH real (HH -> WebService -> WebAPI). Verificar caso bloqueado y permitido.
- [ ] La HH no requirio cambios de codigo ni release.
- [ ] Endpoint `/validar` y `/ejecutar` documentados en OpenAPI/Swagger de la WebAPI .NET 10.
- [ ] Auditoria `audit_cambio_ubicacion` recibe filas con `Origen=BOF` y `Origen=WS_HH` correctamente.
- [ ] Mensajes en `mensaje_regla` revisados con Carol antes del go-live.
- [ ] `TODO-CAROL` resueltos o explicitamente diferidos a un sprint siguiente con ticket asociado.
- [ ] El bridge brain<->WMS (.NET 8) NO fue tocado en este sprint.

---

## 10 - Lo que queda fuera de scope (para sprints siguientes)

1. **E1 (downgrade rotacion)**: hoy queda como `false` hasta aclarar el sentido del operador con Carol.
2. **E2 (significado completo de "A y B iguales C y D iguales")**: hoy se exige solo "mismo IdProducto en ambas ubicaciones" como minimo defensivo.
3. **Modo offline en HH**: por ahora online obligatorio. Si la HH ya tiene logica offline para reubicacion, hay que diseniar el comportamiento esperado.
4. **Migracion de Killios al enforcement strict**: requiere primero auditar sus reglas existentes.
5. **Reglas dinamicas por temporada / campania**: el modelo soporta `Activo bit` pero no hay UI para activar/desactivar reglas masivamente.
6. **Notificaciones a supervisor cuando hay rechazos repetidos**: util para detectar reglas mal configuradas.
7. **Reescritura completa del WebService**: este sprint solo modifica metodos de cambio de ubicacion. Una migracion total del WebService a la WebAPI .NET 10 es una decision arquitectonica mayor que excede esta consulta.

---

## Cross-references

- Card cliente: `wms-brain-client/questions/Q-015/question.md`
- Inbox event: `brain/_inbox/20260428-2000-H12-...json`
- Proposal: `brain/_proposals/P3-2026-04-28-RELOC-RULE-STRICT.md`
- Documento explicativo para Carol: `brain/outputs/respuestas-cliente/consulta-carol-reubicacion.md`
- Version anterior obsoleta: `brain/outputs/guias-implementacion/cambio-ubicacion-idealsa-V0.md`
- Snapshot de evidencia: wms-db-brain (rama del mismo repo) commit del 27-abr-2026.

## Ratificacion pendiente

- Erik Calderon (PrograX24): aprobar/rechazar/diferir esta guia.
- Carol (CKFK/KKKL): aclarar E1 (sentido del operador) y E2 (significado de "A y B iguales C y D iguales") + confirmar modo offline en HH.
- DBA: confirmar como se pobla/refresca `ubicaciones_por_regla`.
