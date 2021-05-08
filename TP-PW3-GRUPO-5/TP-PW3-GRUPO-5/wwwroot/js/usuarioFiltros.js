
const btnEliminar = document.getElementById('eliminado');
const inpNombre = document.getElementById('nombre');
const inpEmail = document.getElementById('email');

    //btnEliminar.addEventListener('click', () => {
    //    obtenerResultados();
    //});

    //inpNombre.addEventListener('change', () => {
    //    obtenerResultados();
    //});

    //inpEmail.addEventListener('change', () => {
    //    obtenerResultados();
    //});

    function generarTabla(resultados){

        listado = '';

        resultados.forEach(resultado => {
            listado += `<tr><td>
                ${resultado.Nombre}
                </td>
                <td>
                ${resultado.Email}
                </td>
                <td>
                ${resultado.Apellido}
                </td>
                <td class="btn-reserva">
                <a class='w3-button w3-round-xlarge w3-green reserva' href='facturacionPorCliente.php?id=${resultado.Id}'>Ver</a>
                </td></tr>`
        })
        $('#tbody').html(listado);

    };

    function obtenerResultados() {

        let data = {
            Nombre: document.getElementById('nombre').value,
            Email: document.getElementById('email').value,
            Eliminado: true
        }

        fetch('https://localhost:44344/usuario/ObtenerFiltros', {
            method: 'POST',
            headers: {
                'Content-type': 'application/json'
            },
            body: JSON.stringify(data)
        })
            .then(response =>
                response.json()

            )
            .then(data => {
                generarTabla(data)
            })
}


// Select2
$(document).ready(function () {
    $('.js-example-basic-single').select2();
});
