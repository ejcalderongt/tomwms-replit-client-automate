let AppRptMovimientos = Vue.createApp({
	mixins: [MxReporteDev],
    name: 'page-rpt-movimientos',
    data: () => ({
        url: "rpt/movimientos",
		fileName: "Movimientos",
		idDataGrid: "grid-movimientos",
		columnas: [
			{
				dataField: "fecha",
				dataType: "datetime",
				format: "dd/MM/yyyy HH:mm",
				caption: "Fecha"
			},
			{ dataField: "codigo", caption: "Código" },
			{ dataField: "codigoBarra", caption: "Código de barras" },
			{ dataField: "producto", caption: "Producto" },
			{ dataField: "umBas", caption: "UMBas"},
			{ dataField: "presentacion", caption: "Presentación"},
			{ dataField: "cantidad", caption: "Cantidad" },
			{
				dataField: "fecha_Vence",
				dataType: "date",
				format: "dd/MM/yyyy",
				caption: "Vencimiento"
			},
			{ dataField: "lote", caption: "Lote" },
			{ dataField: "lic_Plate", caption: "Licplate" },
			{ dataField: "ubicOrigen", caption: "Origen" },
			{ dataField: "ubicDestino", caption: "Destino" },
			{ dataField: "tipoTarea", caption: "Tipo de tarea"}
		],
		dxExtraOptions: {}
	}),
	created() {
		this.bform.bodega = ''
		this.getCatalogo(['bodegas']);
	},
	computed: {
		buscandoBodegas() {
			return this.cBuscando.indexOf('bodegas') >= 0;
		}
	}
})

AppRptMovimientos.mount("#page-rpt-movimientos")