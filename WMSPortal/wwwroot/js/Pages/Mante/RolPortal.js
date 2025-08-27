let AppMntMenu = Vue.createApp({
	mixins: [MxAccion],
	data: () => ({
		nombre: 'mnt/rol-portal',
		listaBuscar: "nombre",
		buscarAlGuardar: false,
		rol: {},
		modal: null,
		bAgregado:false,
		bDisponible: false,
		lAgregado: [],
		lDisponible: [],
		keyPk: 'idPortalRol'
	}),
	mounted() {
		this.modal = new bootstrap.Modal(document.getElementById('modal-acceso'))
	},
	methods: {
		accesoMenu(rol) {
			this.rol = rol;
			this.modal.show();
			this.buscarMenu();
		},
		buscarMenu(tipo = null) {
			if (tipo == 1 || (tipo == null || tipo == undefined)) {
				this.bDisponible = true;
				this.lDisponible = []
				axios
					.get(`${baseUrl}/mnt/menu/buscar`)
					.then(r => {
						this.bDisponible = false;
						this.lDisponible = r.data.lista;
					})
					.catch(e => {
						this.bDisponible = false;
						notificarError("Buscar menu: " + e);
					});
			}

			if (tipo == 2 || (tipo == null || tipo == undefined)) {
				this.bAgregado = true;
				this.lAgregado = []
				axios
					.get(`${baseUrl}/mnt/menu-rol/buscar`, { params: { rol_id: this.rol.idPortalRol } })
					.then(r => {
						this.bAgregado = false;
						this.lAgregado = r.data.lista;
					})
					.catch(e => {
						this.bAgregado = false;
						notificarError("Buscar permisos: " + e);
					});
			}
		},
		getOpciones(padre = null) {
			let tmp = this.lAgregado.filter(o => o.idPortalMenuNavigation.padre == padre).map(mo => mo.idPortalMenuNavigation);

			if (tmp) {
				tmp.forEach(op => {
					op.opciones = this.getOpciones(op.idPortalMenu);
				})
			}

			return tmp;
		},
		agregarOpcion(item) {
			if (confirm("¿Está seguro de agregar el permiso?")) {
				axios
					.post(`${baseUrl}/mnt/menu-rol/guardar/${item.idPortalMenu}/${this.rol.idPortalRol}`)
					.then(r => {
						notificar(r.data.exito, r.data.mensaje);
						if (r.data.exito && r.data.linea) {
							this.lAgregado.push(r.data.linea);
						}
					}).catch(e => {
						notificarError("Agregar permiso: " + e);
					}).finally(() => {
					});
			}
		},
		eliminarOpcion(item) {
			console.log(item)
			if (confirm("¿Está seguro de eliminar el permiso?")) {
				axios
					.post(`${baseUrl}/mnt/menu-rol/eliminar/${item.idPortalMenu}/${this.rol.idPortalRol}`)
					.then(r => {
						notificar(r.data.exito, r.data.mensaje);
						if (r.data.exito) {
							let tmp = this.lAgregado.filter(rmo => rmo.idPortalRol == this.rol.idPortalRol && rmo.idPortalMenu == item.idPortalMenu)

							if (tmp && tmp.length) {
								tmp.forEach(del => {
									this.lAgregado.splice(this.lAgregado.indexOf(del), 1)
								})
                            }
						}
					}).catch(e => {
						notificarError("Eliminar permiso: " + e);
					}).finally(() => {
					});
			}
		}
	},
	computed: {
		listaPermitidos() {
			let tmp = [];

			if (this.lAgregado) {
				tmp = this.lAgregado.map(i => i.idPortalMenuNavigation.idPortalMenu)
			}

			return tmp;
		},
		listaAgregadas() {
			return this.getOpciones(null);
		},
		listaDisponibles() {
			return this.lDisponible.filter(o => o.padre == null)
        }
    }
})

AppMntMenu.component("rama-opciones-agregadas", {
	template: '#rama-opciones-agregadas',
	name: 'rama-opciones-agregadas',
	props: {
		datos: {
			type: Array,
			required: true
		}
	},
	methods: {
		eliminarOpcion(item) {
			this.$emit('eliminar-opcion', item);
		}
	}
})

AppMntMenu.component("rama-opciones-disponibles", {
	template: '#rama-opciones-disponibles',
	name: 'rama-opciones-disponibles',
	props: {
		datos: {
			type: Array,
			required: true
		},
		permitidos: {
			type: Array,
			required: true
        }
	},
	methods: {
		agregarOpcion(item) {
			this.$emit('agregar-opcion', item);
		}
	}
})

AppMntMenu.mount("#page-mante-rol-portal")