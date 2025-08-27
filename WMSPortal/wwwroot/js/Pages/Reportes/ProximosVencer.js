let AppRptExistencias = Vue.createApp({
    mixins: [MxReporteDev],
    name: 'page-rpt-existencias',
    data: () => ({
        url: "rpt/proximos-a-vencer",
        fileName: "Proximos a vencer",
        idDataGrid: "grid-proximos-a-vencer",
        bform: {
            bodega: "",
            dias_tolerancia: 365
        },
        columnas: [
            { dataField: "codigo", caption: "Código" },
            { dataField: "nombre", caption: "Nombre" },
            { dataField: "um", caption: "UM" },
            { dataField: "presentacion", caption: "Presentacion" },
            { dataField: "lote", caption: "Lote" },
            { dataField: "estado", caption: "Estado" },
            { dataField: "palletid", caption: "Licencia" },
            {
                dataField: "cantpres", caption: "Cant Pres", format: {
                    type: "fixedPoint",
                    precision: 2
                }},
            {
                dataField: "cantumbas", caption: "Cant UM Bas", format: {
                    type: "fixedPoint",
                    precision: 2
                } },
            {
                dataField: "fecha_ingreso",
                dataType: "datetime",
                format: "dd/MM/yyyy HH:mm",
                caption: "Fecha de ingreso"
            },
            {
                dataField: "fecha_vence",
                dataType: "datetime",
                format: "dd/MM/yyyy",
                caption: "Fecha de vencimiento"
            },
            { dataField: "ubic", caption: "Ubicacion" },
            { dataField: "tolerancia_dias", caption: "Dias de tolerancia" }
        ],
        dxExtraOptions: {
            summary: {
                totalItems: [{
                    column: "cantpres",
                    summaryType: "sum",
                    valueFormat: "#,##0.00",
                    displayFormat: "Total: {0}"

                }, {
                    column: "cantumbas",
                    summaryType: "sum",
                    valueFormat: "#,##0.00",
                    displayFormat: "Total: {0}"
                }]
            }

        }
    }),
    created() {
        this.getCatalogo(['bodegas']);
    },
    computed: {
        buscandoBodegas() {
            return this.cBuscando.indexOf('bodegas') >= 0;
        }
    }
})

AppRptExistencias.mount('#page-rpt-existencias');