let MxReporteDev = {
	name: "MxReporteDExpr",
	mixins: [MxCatalogo],
	data: () => ({
		url: "",
		fileName: "Datos",
		lista: [],
		bform: {
			fdel: null,
			fal: null
		},
		buscando: false,
		excelFields: {},
		gridObj: null,
		columnas: [],
		dxExtraOptions: {},
		idDataGrid: "content-datagrid",
		autoBuscar: true,
		pdfOptions: {
			orientation: "landscape",
			margin: {
				left: .25,
				right: .25
			}
		}
	}),
	created() {
		this.setFechas();

		if (this.autoBuscar) {
			this.buscar();
        }
    },
	methods: {
		setFechas() {
			let fecha = new Date().toJSON();
			this.bform.fdel = fecha.slice(0, 8) + "01";
			this.bform.fal = fecha.slice(0, 10);
		},
		buscar() {
			this.buscando = true;
			axios.get(`${baseUrl}/${this.url}/buscar`, { params: this.bform })
				.then(r => {
					this.lista = []
					if (r.data.exito) {
						this.lista = r.data.lista
					} else {
						notificarError(r.data.mensaje);
					}
					this.dxGrid();
				}).catch(err => {
					notificarError(err);
				}).finally(() => {
					this.buscando = false;
				})
		},
		dxGrid() {
			if (this.gridObj) {
				$(`#${this.idDataGrid}`).dxDataGrid({
					dataSource: this.lista,
				});
			} else {
				this.gridObj = $(`#${this.idDataGrid}`).dxDataGrid({
					dataSource: this.lista,
					columns: this.columnas,
					wordWrapEnabled: true,
					allowColumnResizing: true,
					columnResizingMode: "widget",
					showRowLines: true,
					rowAlternationEnabled: true,
					showBorders: true,
					filterRow: {
						visible: true,
						applyFilter: "auto"
					},
					headerFilter: {
						visible: true
					},
					pager: {
						allowedPageSizes: [5, 10, 20, 50],
						showPageSizeSelector: true,
						showNavigationButtons: true
					},
					paging: {
						pageSize: 25,
					},
					...this.dxExtraOptions
				}).dxDataGrid("instance");
			}
		},
		exportarExcel() {
			try {
			var workbook = new ExcelJS.Workbook();
			var worksheet = workbook.addWorksheet(this.fileName);

			DevExpress.excelExporter.exportDataGrid({
				component: this.gridObj,
				worksheet: worksheet,
				autoFilterEnabled: true
			}).then(() => {
				workbook.xlsx.writeBuffer().then((buffer) => {
					saveAs(new Blob([buffer], { type: 'application/octet-stream' }), `${this.fileName}.xlsx`);
				});
			});

			} catch (e) {
				notificarError(e.getMessage())
            }
		},
		exportarPDF() {
			var doc = new jsPDF(this.pdfOptions);

			DevExpress.pdfExporter.exportDataGrid({
				jsPDFDocument: doc,
				component: this.gridObj
			}).then(() => {
				doc.save(`${this.fileName}.pdf`);
			});
					}
	}
}