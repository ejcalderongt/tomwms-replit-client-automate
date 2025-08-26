let MxCatalogo = {
    name: 'catalogo',
    data: () => ({
        cat: {
            bodegas: []
        },
        args: {},
        cBuscando: [],
    }),
	methods: {
		getCatalogo(lista = []) {
			if (lista.length) {
				lista.forEach(opcion => {
					this.cat[opcion] = [];
					this.cBuscando.push(opcion);

					axios.get(`${baseUrl}/catalogo/buscar/${opcion}`, { params: { args: this.args[opcion] } })
					.then(r => {
						if (r.status == 200) {
							this.cat[opcion] = r.data;
						} else {
							notificarError(r.statusText);
                        }
					})
					.catch(e => {
						notificar(0, e);
					})
					.finally(() => {
						setTimeout(() => {
						this.cBuscando.splice(opcion, 1);
						}, 5000)
					});
                })
			}
		}
	}
}