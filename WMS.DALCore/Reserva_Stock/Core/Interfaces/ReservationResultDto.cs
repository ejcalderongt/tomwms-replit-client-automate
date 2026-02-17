using System;
using System.Collections.Generic;
using System.Linq;
using WMS.EntityCore.Stock;
using WMSWebAPI.Be;

namespace WMS.StockReservation.Core.Interfaces
{
    public enum ReservationStatus
    {
        Success = 0,
        Partial = 1,
        Failed = 2
    }

    public enum ReservationFailureCode
    {
        NONE = 0,
        NO_STOCK = 1,
        LOT_NOT_FOUND = 2,
        LOCATION_RESTRICTED_NO_STOCK = 3,
        PRODUCT_STATE_REQUIRED_NO_STOCK = 4,
        PICKING_ZONE_REQUIRED_NO_STOCK = 5,
        NON_PICKING_ZONE_REQUIRED_NO_STOCK = 6,
        RECEPTION_LOCATION_NOT_ALLOWED = 7,
        ALL_STOCK_EXPIRED = 8,
        ZONE_PRIORITY_CONFLICT = 9,
        PRODUCT_NOT_FOUND = 10,
        INVALID_QUANTITY = 11,
        STORAGE_CONDITION_MISMATCH = 12,
        MANUFACTURING_DATE_INVALID = 13
    }

    public class ReservationFailureReason
    {
        public ReservationFailureCode Code { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? LoteNo { get; set; }
        public int? IdUbicacion { get; set; }
        public int? IdProductoEstado { get; set; }
        public string? ZoneName { get; set; }
        public double? QuantityAffected { get; set; }

        public static ReservationFailureReason Create(
            ReservationFailureCode code,
            string message,
            double? qty = null)
        {
            return new ReservationFailureReason
            {
                Code = code,
                Message = message,
                QuantityAffected = qty
            };
        }
    }

    public class ReservationResultDto
    {
        public ReservationStatus Status { get; set; }
        public double RequestedQuantity { get; set; }
        public double ReservedQuantity { get; set; }
        public double PendingQuantity { get; set; }
        public int ReservationCount { get; set; }

        public List<ReservationFailureReason> FailureReasons { get; set; } = new();
        public List<clsBeStock_res> Reservations { get; set; } = new();

        public bool UsedPickingZone { get; set; }
        public bool UsedNonPickingZone { get; set; }
        public bool UsedSpecificLot { get; set; }
        public bool UsedExplosion { get; set; }

        public string StatusMessage => Status switch
        {
            ReservationStatus.Success => "Reserva completada exitosamente.",
            ReservationStatus.Partial => $"Reserva parcial: {ReservedQuantity:F2} de {RequestedQuantity:F2} unidades.",
            ReservationStatus.Failed => "No se pudo reservar stock. " + GetPrimaryFailureMessage(),
            _ => "Estado desconocido."
        };

        public string GetPrimaryFailureMessage()
        {
            if (FailureReasons.Count == 0)
                return "Sin detalles adicionales.";

            return FailureReasons.First().Message;
        }

        public List<string> GetAllFailureMessages()
        {
            return FailureReasons.Select(r => $"[{r.Code}] {r.Message}").ToList();
        }

        public static ReservationResultDto FromContext(
            ReservationContext context,
            List<ReservationFailureReason>? reasons = null)
        {
            var requested = context.OriginalRequestedQuantity;
            var pending = context.PendingQuantity;
            var reserved = requested - pending;

            var status = reserved >= requested - 0.000001
                ? ReservationStatus.Success
                : reserved > 0.000001
                    ? ReservationStatus.Partial
                    : ReservationStatus.Failed;

            var dto = new ReservationResultDto
            {
                Status = status,
                RequestedQuantity = requested,
                ReservedQuantity = reserved,
                PendingQuantity = pending,
                ReservationCount = context.CreatedReservations?.Count ?? 0,
                Reservations = context.CreatedReservations ?? new List<clsBeStock_res>(),
                FailureReasons = reasons ?? context.FailureReasons ?? new List<ReservationFailureReason>(),
                UsedPickingZone = context.UsedPickingZone,
                UsedNonPickingZone = context.UsedNonPickingZone,
                UsedSpecificLot = context.HasSpecificLot,
                UsedExplosion = context.ExplosionModeEnabled
            };

            if (status == ReservationStatus.Partial && dto.FailureReasons.Count == 0)
            {
                dto.FailureReasons.Add(ReservationFailureReason.Create(
                    ReservationFailureCode.NO_STOCK,
                    $"Stock insuficiente. Disponible: {reserved:F2}, Pendiente: {pending:F2}",
                    pending));
            }

            if (status == ReservationStatus.Failed && dto.FailureReasons.Count == 0)
            {
                dto.FailureReasons.Add(ReservationFailureReason.Create(
                    ReservationFailureCode.NO_STOCK,
                    "No hay stock disponible para este producto.",
                    requested));
            }

            return dto;
        }

        public ReservationResult ToLegacyResult()
        {
            return new ReservationResult
            {
                Success = Status == ReservationStatus.Success || Status == ReservationStatus.Partial,
                Reservations = Reservations,
                RemainingQuantity = PendingQuantity,
                Messages = GetAllFailureMessages()
            };
        }
    }
}
