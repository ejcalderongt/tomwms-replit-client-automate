namespace WMS.StockReservation.Infrastructure
{
    /// <summary>
    /// Factory para construir el pipeline y la cadena de handlers dinámicamente.
    /// NO usa DI container (compatibilidad con VB.NET legacy).
    /// Target: ~120 líneas
    /// </summary>
    public class ServiceFactory : IServiceFactory
    {
        public IReservationPipeline CreateReservationPipeline()
        {
            var logger = new ReservationLogger();

            var steps = new IPipelineStep[]
            {
                new ValidationStep(logger),
                new EntityLoadingStep(logger),
                new StockQueryStep(logger),
                new DateCalculationStep(logger),
                new ReservationLoopStep(this, logger),  // ✅ Pasa factory para re-construcción
                new PostProcessingStep(logger)
            };

            return new PipelineExecutor(steps, logger);
        }

        /// <summary>
        /// Construye dinámicamente la cadena de handlers según el punto de inicio.
        /// CRÍTICO: Se llama en CADA iteración del loop para permitir re-entry.
        /// </summary>
        public IReservationHandler BuildHandlerChain(
            int startingPoint,
            ReservationContext context,
            IReservationLogger logger)
        {
            // Determinar qué handlers incluir según el modo y configuración
            var handlers = new List<IReservationHandler>();

            // Si hay explosión o UMBas, solo usar ese handler
            if (context.IsExplosionModeEnabled || context.IsUMBasModeEnabled)
            {
                handlers.Add(new UMBasExplosionHandler(logger));
            }
            else
            {
                // Construcción normal según starting point
                switch (startingPoint)
                {
                    case 1: // INICIAR_EN_1 (pallets completos)
                        if (context.Configuration.Conservar_Zona_Picking_Clavaud)
                        {
                            handlers.Add(new CompletePackagesHandler(logger));
                            handlers.Add(new IncompletePackagesHandler(logger));
                        }
                        handlers.Add(new PickingZoneHandler(logger));
                        handlers.Add(new NonPickingZoneHandler(logger));
                        break;

                    case 2: // INICIAR_EN_2 (pallets incompletos)
                        if (context.Configuration.Conservar_Zona_Picking_Clavaud)
                        {
                            handlers.Add(new IncompletePackagesHandler(logger));
                        }
                        handlers.Add(new PickingZoneHandler(logger));
                        handlers.Add(new NonPickingZoneHandler(logger));
                        break;

                    case 3: // INICIAR_EN_3 (picking zone)
                        handlers.Add(new PickingZoneHandler(logger));
                        handlers.Add(new NonPickingZoneHandler(logger));
                        break;

                    case 4: // INICIAR_EN_4 (non-picking zones)
                        handlers.Add(new NonPickingZoneHandler(logger));
                        break;

                    default: // 0 o desconocido: cadena completa
                        if (context.Configuration.Conservar_Zona_Picking_Clavaud)
                        {
                            handlers.Add(new CompletePackagesHandler(logger));
                            handlers.Add(new IncompletePackagesHandler(logger));
                        }
                        handlers.Add(new PickingZoneHandler(logger));
                        handlers.Add(new NonPickingZoneHandler(logger));
                        break;
                }
            }

            // Encadenar handlers
            if (handlers.Count == 0)
            {
                throw new InvalidOperationException(
                    $"No handlers configurados para startingPoint={startingPoint}");
            }

            for (int i = 0; i < handlers.Count - 1; i++)
            {
                handlers[i].SetNext(handlers[i + 1]);
            }

            return handlers[0];
        }
    }
}
