let appMenu = {
	data: () => ({
		menu: [],
		baseUrl: baseUrl,
		termino: '',
		bMenu: true
	}),
	created() {
		this.getMenu();
	},
	methods: {
		getMenu() {
			this.bMenu = true;
			const url = `${this.baseUrl}/mnt/menu/usuario`;
			axios.get(url)
				.then((res) => {
					this.menu = res.data.lista
				}).catch((error) => {
					notificarError(error)
				}).finally(() => {
					this.bMenu = false;
				})
		},
		getOpciones(padre = null) {
			let aux = this.menu.filter(o => o.idPortalMenuNavigation.padre == padre).map(mo => mo.idPortalMenuNavigation)
			let tmp = []

			if (aux) {
				aux.forEach(e => {
					let agregar = true;

					tmp.forEach(o => {
						if (o.idPortalMenu == e.idPortalMenu) {
							agregar = false
                        }
					})
					if (agregar) {
						tmp.push(e)
                    }
				})

				tmp.forEach(op => {
					op.opciones = this.getOpciones(op.idPortalMenu);
				})
			}

			return tmp;
		}
	},
	computed: {
		lista() {
			return this.getOpciones(null);
		},
    }
}

let App = Vue.createApp(appMenu);

App.component('menu-opciones', {
	template: '#menu-opciones',
	props: {
		lista: {
			type: Array,
			required: false
		},
		ide: {
			required: true
        }
	},
	data: () => ({
		baseUrl: baseUrl
	}),
	methods: {
		toggleOpciones(id='') {
			$(`#menu-opciones-${id}`).toggle()
        }
    }
})

App.mount('#sidebar-wrapper');