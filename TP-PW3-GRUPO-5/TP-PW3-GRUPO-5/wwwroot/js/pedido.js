//Filtrado por nombre ,estado y eliminados
const btnEliminar = document.getElementById('eliminado');
const cliente = document.getElementById('cliente');
const estado = document.getElementById('estado');
const ultimosDosMeses = document.getElementById('ultimosDosMeses');

function GenerarTabla(resultados) {

    var table = $('#example').DataTable();
    table.destroy();

    listado = '';

    resultados.forEach(resultado => {
        listado += `<tr><td>
                ${resultado.Cliente} - #${resultado.NroPedido}
                </td>
                <td>
                ${resultado.Estado}
                </td>
                <td>
                ${resultado.UltimaModificacion}
                </td>
                <td>
                <a class="me-3" href="/pedido/detallePedido?accion=ver" data-toggle="tooltip"
                    title="Ver"><i class="fas fa-eye"></i></a>
                <a class="me-3" href="/pedido/detallePedido?accion=editar" data-toggle="tooltip"
                    title="Editar"><i class="fas fa-edit"></i></a>`;
        if (!resultado.seBorro) {
            listado += `
            <a href = "#" data-toggle="tooltip" title = "Borrar" >
                <i class="fas fa-trash-alt"></i>
            </a > `;
        } else {
            listado += `<i title = "Eliminado" class="fas fa-ban" ></i > `;
        }
        listado += `</td ></tr >`;
    })
    $('#tbody').html(listado);

};

//Obtener resultados filtrados

function ObtenerResultados() {
    let data = {
        Cliente: document.getElementById('cliente').value,
        Estado: document.getElementById('estado').value,
        Eliminado: document.getElementById('eliminado').checked,
        UltimosDosMeses: document.getElementById('ultimosDosMeses').checked
    }

    fetch('https://localhost:44344/pedido/ObtenerFiltros', {
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
    $('.select2Pedido').select2();
});

////Paginacion


function Paginacion() {

    $('#example').DataTable({
        ordering : false,
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



