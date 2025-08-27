let AppRptExistencias = Vue.createApp({
    mixins: [MxReporteDev],
    name: 'page-rpt-existencias',
    data: () => ({
        url: "rpt/existencias",
        fileName: "Existencias",
        idDataGrid: "grid-existencias",
        columnas: [
            {
                dataField: "fecha_ingreso",
                dataType: "datetime",
                format: "dd/MM/yyyy HH:mm",
                caption: "Fecha"
            },
            { dataField: "codigo_Producto", caption: "Código" },
            { dataField: "codigo_Barra", caption: "Código de barras" },
            { dataField: "nombre_Producto", caption: "Producto" },
            { dataField: "umBas", caption: "UMBas" },
            { dataField: "nombre_Presentacion", caption: "Presentación" },
            { dataField: "cantidadUmBas", caption: "Cantidad" },
            {
                dataField: "fecha_Vence",
                dataType: "date",
                format: "dd/MM/yyyy",
                caption: "Vencimiento"
            },
            { dataField: "lote", caption: "Lote" },
            { dataField: "lic_plate", caption: "Licplate" },
            { dataField: "nombreTipoProducto", caption: "Tipo de producto" }
        ]
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

AppRptExistencias.mount('#page-rpt-existencias');