let Ropciones = {
	template: '#rama-opciones',
	name: 'rama-opciones',
	props: {
		datos: {
			type: Array,
			required: true
		}
	},
	methods: {
		editMenu(item) {
			this.$emit('edit-menu', item);
		},
		addMenu(item) {
			this.$emit('add-menu', item);
		}
	}
}
let AppMntMenu = Vue.createApp({
	mixins: [MxAccion],
	data: () => ({
		nombre: 'mnt/menu',
		listaBuscar: "nombre",
		fopcion: {},
		buscarAlGuardar: true
	}),
	methods: {
		getOpciones(padre = null) {
			let opciones = this.lista.filter(e => e.padre == padre && e.activo == 1)
			if (opciones.length > 0) {
				opciones.forEach(o => {
					o.opciones = this.getOpciones(o.idPortalMenu)
				})
			}
			return opciones;
		},
		editMenu(item) {
			this.form = item
			this.reg = item.idPortalMenu
		},
		addMenu(item) {
			this.limpiar()
			this.fopcion = item
			this.form.padre = item.idPortalMenu
		},
		limpiar() {
			this.form = {}
			this.fopcion = {}
			this.reg = ""
		}
	},
	computed: {
		filtrada() {
			return this.getOpciones()
		}
	}
})

AppMntMenu.component('rama-opciones', Ropciones); 

AppMntMenu.mount("#page-mante-menu")