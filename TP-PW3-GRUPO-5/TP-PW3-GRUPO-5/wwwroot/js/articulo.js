//Filtrado por nombre ,email y eliminados
const btnEliminar = document.getElementById('eliminado');
const inpNombre = document.getElementById('descripcion');
const inpEmail = document.getElementById('codigo');

function GenerarTabla(resultados) {

    var table = $('#example').DataTable();
    table.destroy();

    listado = '';
    let fechaActual = new Date();
    fechaActual = fechaActual.getDate();

    resultados.forEach(resultado => {
        listado += `<tr><td>
                ${resultado.Descripcion}
                </td>
                <td>
                ${resultado.Codigo}
                </td>
                <td>
                <a class="me-3" href="/Articulo/DetalleArticulo/ver/${resultado.IdArticulo}" data-toggle="tooltip"
                    title="Ver"><i class="fas fa-eye"></i></a>
                <a class="me-3" href="/Articulo/DetalleArticulo/editar/${resultado.IdArticulo}" data-toggle="tooltip"
                    title="Editar"><i class="fas fa-edit"></i></a>`;
        if (resultado.BorradoPor == null) {
            listado += `
            <a onclick="eliminarArticulo(${resultado.IdArticulo},'${resultado.Descripcion}')" data-toggle="tooltip" title="Borrar">
                <i class="fas fa-trash-alt"></i>
            </a>`;
        } else {
            listado += `<i title="Eliminado" class="fas fa-ban"></i>`;
        }
        listado += `</td></tr>`;
    })
    $('#tbody').html(listado);

};

//Obtener resultados filtrados

function ObtenerResultados() {
    let data = {
        Descripcion: document.getElementById('descripcion').value,
        Codigo: document.getElementById('codigo').value,
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
    $('.select2Articulo').select2();
});

////Paginacion


function Paginacion() {

    $('#example').DataTable({
        ordering: false,
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

function eliminarArticulo(id, descripcion) {
    fetch('https://localhost:44344/articulo/ConsultarEstadoPedidos', {
        method: 'POST',
        headers: {
            'Content-type': 'application/json'
        },
        body: JSON.stringify(id)
    })
        .then(response =>
            response.json()
        )
        .then(data => {
            if (data) {
                swal({
                    title: `¿Desea eliminar al artículo (${descripcion})?`,
                    text: " Tenga en cuenta que se encuentra en pedidos no eliminados. Al eliminar al artículo, también se eliminará de sus pedidos.",
                    icon: "warning",
                    buttons: ["Cancelar", "Aceptar"],
                    dangerMode: true,
                })
                    .then((willDelete) => {
                        if (willDelete) {
                            window.location.href = `/articulo/EliminarArticulo/${id}`
                        }
                    });
            } else {
                swal({
                    title: "¿Desea eliminar?",
                    text: "¿Confirma que desea eliminar?",
                    icon: "warning",
                    buttons: ["Cancelar", "Aceptar"],
                    dangerMode: true,
                })
                    .then((willDelete) => {
                        if (willDelete) {
                            window.location.href = `/articulo/EliminarArticulo/${id}`
                        }
                    });
            }
        })



}




