let MxAccion = {
	name: 'accion',
	data: () => ({
		verForm: true,
		nombre: '',
		btnGuardar: false,
		inicio: true,
		reg: '',
		form: {},
		bform: {},
		lista: [],
		listaFiltrar: null,
		listaTermino: null,
		autoBuscar: true,
		inicioArray: false,
		buscarAlGuardar: false,
		keyPk: "id"
	}),
	created() {
		if (this.autoBuscar) {
			this.buscar();
		}
	},
	methods: {
		guardar() {
			this.btnGuardar = true;
			let data = new FormData();
			
			for (let i in this.form) {
				data.append(i, this.form[i])
			}
			axios
				.post(`${baseUrl}/${this.nombre}/guardar/${this.reg}`, data)
				.then(r => {
					notificar(r.data.exito, r.data.mensaje);
					if (r.data.exito) {
						if (this.buscarAlGuardar) {
							this.buscar();
						} else {
							if (r.data.linea) {
								if (this.reg == '') {
									if (this.inicioArray) {
										this.lista.unshift(r.data.linea);
									} else {
										this.lista.push(r.data.linea);
									}
								} else {
									let tmp = this.lista.filter(e => {
										return e.id == this.reg
									})[0];

									if (tmp) {
										let key = this.lista.indexOf(tmp);
										for (let i in this.lista[key]) {
											this.lista[key][i] = r.data.linea[i];
										}
									}
								}
							}
						}

						this.limpiar();
					}
				}).catch(e => {
					notificarError("Guardar: "+e );
				}).finally(() => {
					this.btnGuardar = false;
				});
		},
		buscar() {
			this.inicio = true;
			axios
				.get(`${baseUrl}/${this.nombre}/buscar`, { params: this.bform })
				.then(r => {
					this.inicio = false;
					this.lista = r.data.lista;
				})
				.catch(e => {
					this.inicio = false;
					notificarError("Buscar: "+e);
				});
		},
		limpiar() {
			this.reg = '';
			this.form = {};
		},
		editar(idx) {
			let tmp = this.filtrada[idx];

			this.reg = tmp[this.keyPk];
			this.form = {};

			for (let i in tmp) {
				this.form[i] = tmp[i];
			}

			this.verForm = true;
		}
	},
	computed: {
		filtrada() {
			return this.lista.filter(obj => {
				if (this.listaFiltrar === null) {
					return true;
				} else {
					if (this.listaTermino === null) {
						return true;
					} else {
						return obj[this.listaFiltrar].toLowerCase().includes(this.listaTermino.toLowerCase());
					}
				}
			});
		}
	}
}