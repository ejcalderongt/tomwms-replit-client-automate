const baseUrl = window.location.origin + "";

$(document).on("click", "#menuKpp", function (e) {
	e.preventDefault()
	$("#wrapper").toggleClass("toggled")
})

function verOpcionesMenu(args) {
	var menu = args.querySelector('.knavul')
	$(menu).toggle('blind')
}

function notificar(tipo, mensaje) {
	switch (tipo) {
		case false:
			toastr.error(mensaje)
			break;

		case 0:
			toastr.error(mensaje)
			break;

		case true:
			toastr.success(mensaje)
			break;

		case 1:
			toastr.success(mensaje)
			break;

		case 2:
			toastr.warning(mensaje)
			break;

		case 3:
			toastr.info(mensaje)
			break;
	}
}

function notificarError(mensaje) {
	toastr.error(mensaje)
}

function notificarExito(mensaje) {
	toastr.success(mensaje)
}

function notificarAvertencia(mensaje) {
	toastr.warning(mensaje)
}

function notificarInfo(mensaje) {
	toastr.info(mensaje)
}

function cargando(id, opc = 1) {
	let contenedor = document.getElementById(id)
	let spin = `
	<div class="spinner-border" role="status">
		<span class="sr-only">Loading...</span>
	</div>`;

	if (opc == 1) {
		contenedor.innerHTML = "<p class='text-center'>" + spin + "</p>";
	}

	if (opc == 2) {
		contenedor.innerHTML = "<tr><td class='text-center' colspan='100%'>" + spin + "</td></tr>";
	}
}
function formatoFecha(fecha, formato) {
	let aux = fecha.split("T")

	if (aux[1]) {
		let aux2 = aux[1].split(".");
		if (!aux2[1]) {
			fecha = fecha + ".001"
		}
	}

	var f = new Date(fecha)
	var ani = f.getFullYear();
	var mes = parseInt(f.getMonth()) + 1;

	if (mes <= 9) {
		mes = "0" + mes
	}

	var dia = parseInt(f.getDay()) <= 9 ? "0" + f.getDay() : f.getDay();
	var hor = parseInt(f.getHours()) <= 9 ? "0" + f.getHours() : f.getHours();
	var min = parseInt(f.getMinutes()) <= 9 ? "0" + f.getMinutes() : f.getMinutes();
	var seg = parseInt(f.getSeconds()) <= 9 ? "0" + f.getSeconds() : f.getSeconds();
	var fechaFormato = "";

	switch (formato) {
		case 1:
			fechaFormato = `${dia}/${mes}/${ani}`;
			break;
		case 2:
			fechaFormato = `${ani}-${mes}-${dia}`;
			break;
		case 3:
			fechaFormato = `${dia}/${mes}/${ani} ${hor}:${min}:${seg}`;
			break;
		case 4:
			fechaFormato = `${ani}-${mes}-${dia} ${hor}:${min}:${seg}`;
			break;
		case 5:
			fechaFormato = `${dia}/${mes}/${ani} ${hor}:${min}`;
			break;
		default:
			fechaFormato = `${dia}/${mes}/${ani} ${hor}:${min}:${seg}`;
			break;
	}

	return fechaFormato;
}