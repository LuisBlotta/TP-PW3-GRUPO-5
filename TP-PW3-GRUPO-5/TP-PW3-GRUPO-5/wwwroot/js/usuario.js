﻿//Filtrado por nombre ,email y eliminados
const btnEliminar = document.getElementById('eliminado');
const inpNombre = document.getElementById('nombre');
const inpEmail = document.getElementById('email');

function generarTabla(resultados) {

    var table = $('#example').DataTable();
    table.destroy();

    listado = '';

    resultados.forEach(resultado => {
        listado += `<tr><td>
                ${resultado.Nombre}
                </td>
                <td>
                ${resultado.Apellido}
                </td>
                <td>
                ${resultado.Email}
                </td>
                <td>
                <a class="me-3" href="/usuario/detalleUsuario?accion=ver" data-toggle="tooltip"
                    title="Ver"><i class="fas fa-eye"></i></a>
                <a class="me-3" href="/usuario/detalleUsuario?accion=editar" data-toggle="tooltip"
                    title="Editar"><i class="fas fa-edit"></i></a>
                <a href="#" data-toggle="tooltip" title="Borrar">
                    <i class="fas fa-trash-alt"></i>
                </a>
                </td>        
        </tr>`
    })
    $('#tbody').html(listado);

};

//Obtener resultados filtrados

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
            paginacion();
        })
}


// Select2
$(document).ready(function () {
    $('.js-example-basic-single').select2();
});

////Paginacion


function paginacion() {

    $('#example').DataTable({
        retrieve: true,
        destroy: true,
        "language": {
            "paginate": {
                "first": "Primera",
                "last": "Ultima",
                "next": "Siguiente",
                "previous": "Anterior"
            },
            "lengthMenu": "Mostrar _MENU_ resultados por página",
            "zeroRecords": "No se encontraron resultados",
            "info": "Mostrando página  _PAGE_ de _PAGES_",
            "infoEmpty": "Sin resultados",
            "infoFiltered": "(filtered from _MAX_ total records)"
        }
    });
    $('#example_filter').hide();
}

$(document).ready(function () {
    paginacion();
});
