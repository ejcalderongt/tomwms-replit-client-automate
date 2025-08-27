let MxReporte = {
	name: "MxReporte",
	mixins: [MxCatalogo],
	data: () => ({
		lista: [],
		url: "",
		bform: {
			fdel: null,
			fal: null
		},
		buscando: false,
		fileName: "Datos",
		autoBuscar: true
	}),
	created() {
		if (this.autoBuscar) {
			this.setFechas();
			this.buscar();
		}
	},
	methods: {
		buscar() {
			this.buscando = true;
			axios.get(`${baseUrl}/${this.url}/buscar`, { params: this.bform })
				.then(r => {
					if (r.data.exito) {
						this.lista = r.data.lista
					} else {
						notificarError(r.data.mensaje);
					}
				}).catch(err => {
					notificarError(err);
				}).finally(() => {
					this.buscando = false;
				})
		},
		setFechas() {
			let fecha = new Date().toJSON();
			this.bform.fdel = fecha.slice(0, 8) + "01";
			this.bform.fal = fecha.slice(0, 10);
		},
		
		formatoFecha(fecha, formato) {
			return formatoFecha(fecha, formato);
		},
		formato(numero, formato = '0') {
			return numeral(numero).format(formato)
		},
		exportExcel(fn, dl) {
			var elt = this.$refs["tabla-datos"];
			var wb = XLSX.utils.table_to_book(elt, { sheet: this.fileName || "Hoja 1" });
			return dl ?
				XLSX.write(wb, { bookType: "xlsx", bookSST: true, type: 'base64' }) :
				XLSX.writeFile(wb, (this.fileName || 'Datos') + '.xlsx');
		}
	},
	computed: {
		filtrada() {
			return this.lista;
		}
	}
}