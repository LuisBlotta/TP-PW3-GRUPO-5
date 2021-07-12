//Filtrado por nombre ,email y eliminados
const btnEliminar = document.getElementById('eliminado');
const inpNombre = document.getElementById('nombre');
const inpEmail = document.getElementById('numero');

function GenerarTabla(resultados) {

    var table = $('#example').DataTable();
    table.destroy();

    listado = '';

    resultados.forEach(resultado => {
        listado += `<tr><td>
                ${resultado.Nombre}
                </td>
                <td>
                ${resultado.Numero}
                </td>
                <td>
                ${resultado.Telefono}
                </td>
                <td>
                <a class="me-3" href="/cliente/detalleCliente/ver/${resultado.IdCliente}" data-toggle="tooltip"
                    title="Ver"><i class="fas fa-eye"></i></a>
                <a class="me-3" href="/cliente/detalleCliente/editar/${resultado.IdCliente}" data-toggle="tooltip"
                    title="Editar"><i class="fas fa-edit"></i></a>`;
        if (resultado.BorradoPor == null) {
            listado += `
            <a onclick="eliminarCliente(${resultado.IdCliente},'${(resultado.Nombre).replace(/['"]+/g, '')}')" data-toggle="tooltip" title="Borrar">
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
    var numero = document.getElementById('numero').value;

    if (numero == "") {
        numero = null;
    }

    let data = {
        Nombre: document.getElementById('nombre').value,
        Numero: numero,
        Eliminado: document.getElementById('eliminado').checked
    }

    fetch('https://localhost:44344/cliente/ObtenerFiltros', {
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
    $('.select2Cliente').select2();
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

function eliminarCliente(id, nombre) {
    fetch('https://localhost:44344/cliente/ConsultarEstadoPedidos', {
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
                    title: `¿Desea eliminar al cliente (${nombre})?`,
                    text: " Tenga en cuenta que posee pedidos no eliminados. Al eliminar al cliente, también se eliminarán sus pedidos",
                    icon: "warning",
                    buttons: ["Cancelar", "Aceptar"],
                    dangerMode: true,
                })
                    .then((willDelete) => {
                        if (willDelete) {
                            window.location.href = `/Cliente/EliminarCliente/${id}`
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
                            window.location.href = `/Cliente/EliminarCliente/${id}`
                        }
                    });
            }
        })

            
        
}
