//Filtrado por nombre ,email y eliminados
const btnEliminar = document.getElementById('eliminado');
const inpNombre = document.getElementById('descripcion');
const inpEmail = document.getElementById('codigo');

function GenerarTabla(resultados) {

    var table = $('#example').DataTable();
    table.destroy();

    listado = '';

    resultados.forEach(resultado => {
        listado += `<tr><td>
                ${resultado.Descripcion}
                </td>
                <td>
                ${resultado.Codigo}
                </td>
                <td>
                <a class="me-3" href="/articulo/detalleArticulo?accion=ver" data-toggle="tooltip"
                    title="Ver"><i class="fas fa-eye"></i></a>
                <a class="me-3" href="/articulo/detalleArticulo?accion=editar" data-toggle="tooltip"
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

function ObtenerResultados() {
    var codigo = document.getElementById('codigo').value;

    if (codigo == "") {
        codigo = null;
    }

    let data = {
        Descripcion: document.getElementById('descripcion').value,
        Codigo: codigo,
        Eliminado: document.getElementById('eliminado').checked
    }

    fetch('https://localhost:44344/articulo/ObtenerFiltros', {
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
            GenerarTabla(data);
            Paginacion();
        })
}


// Select2
$(document).ready(function () {
    $('.js-example-basic-single').select2();
});

////Paginacion


function Paginacion() {

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
    Paginacion();
    ObtenerResultados();
});



