let AppRptOcupacionBodega = Vue.createApp({
    mixins: [MxReporte],
    data: () => ({
        lista: [],
        url: "rpt/ocupacion-bodega",
        buscando: false,
		fileName: "Ocupación bodega",
        pOcupado: 0,
        autoBuscar: false,
        bform: {
            bodega: ''
        },
        msgInicio: true
    }),
    created() {
        this.getCatalogo(['bodegas']);
    },
    methods: {
		buscar() {
            this.buscando = true;
            this.msgInicio = false;

			axios.get(`${baseUrl}/${this.url}/buscar`, { params: this.bform })
				.then(r => {
					if (r.data.exito) {
						let tmp = r.data.lista

						this.lista = [
							{ nombre: "Ocupadas", valor: tmp.ubicacionesOcupadas},
							{ nombre: "Vacias", valor: tmp.ubicacionesVacias}
						];
						let total = tmp.ubicacionesOcupadas + tmp.ubicacionesVacias;
						if (total > 0) {
							this.pOcupado = (tmp.ubicacionesOcupadas * 100) / total;
						} else {
							this.pOcupado = 100;
                        }

                        setTimeout(() => {
                            this.indicador();
                            this.grafica()
                        })
					} else {
						notificarError(r.data.mensaje);
					}
				}).catch(err => {
					notificarError(err);
				}).finally(() => {
					this.buscando = false;
				})
		},
        indicador() {
            porcentaje = this.pOcupado;

            $("#indicador").dxCircularGauge({
                scale: {
                    startValue: 0, endValue: 100,
                    tick: {
                        color: "#536878"
                    },
                    tickInterval: 10,
                    label: {
                        indentFromTick: 3
                    }
                },
                rangeContainer: {
                    offset: 10,
                    ranges: [
                        { startValue: 0, endValue: 33.33, color: "#77DD77" },
                        { startValue: 33.33, endValue: 66.66, color: "#E6E200" },
                        { startValue: 66.66, endValue: 100, color: "#92000A" }
                    ]
                },
                title: {
                    text: "Porcentaje de ocupación de bodega",
                    font: { size: 28 },
                    subtitle: numeral(porcentaje).format('0.000')+ '%'
                },
                tooltip: {
                    enabled: true,
                    customizeTooltip: function (arg) {
                        console.log(arg.valueText)
                        return {
                            text: numeral(parseFloat(arg.valueText)).format('0.000') + '%'
                        };
                    },
                    font: {
                        color: "#000000",
                        size: 20
                    }
                },
                "export": {
                    enabled: true
                },
                value: porcentaje
            });
        },
        grafica() {
            let valores = this.lista;

            $("#grafica").dxChart({
                dataSource: valores,
                palette: "light",
                title: {
                    text: "Gráfico de ocupación de bodega",
                },
                commonSeriesSettings: {
                    type: "bar",
                    valueField: "valor",
                    argumentField: "nombre",
                    ignoreEmptyPoints: true
                },
                seriesTemplate: {
                    nameField: "nombre"
                },
                tooltip: {
                    enabled: true,
                    shared: true,
                    customizeTooltip: function (info) {
                        return {
                            html: `
							    <div>
								    <div class='tooltip-header'><b>Ubicaciones ${info.argumentText.toLowerCase()}</b></div>
								    <div class='tooltip-body'>
									    <div class='series-name'>${numeral(info.points[0].valueText).format('0,0,0.00')}</div>
								    </div>
							    </div>`
                        };
                    }
                },
            });
        }
    },
    computed: {
        buscandoBodegas() {
            return this.cBuscando.indexOf('bodegas') >= 0;
        }
    }
})


AppRptOcupacionBodega.mount('#page-rpt-ocupacion-bodega')