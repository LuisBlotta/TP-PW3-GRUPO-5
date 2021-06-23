var articulos = new Array();

// Select2
$(document).ready(function () {
    $('.select2Pedido').select2();
    ObtenerResultados();

});

function AgregarArticulo() {
    let codigo = document.getElementById("articulo").value;
    let selectArticulo = document.getElementById("articulo");
    let descripcion = selectArticulo.options[selectArticulo.selectedIndex].text;
    descripcion = descripcion.split("#")[0];
    let cantidad = Number.parseInt(document.getElementById("cantidad").value);

    if (codigo != "" && !Number.isNaN(cantidad)) {
        let articulo = {
            Codigo: codigo,
            Descripcion: descripcion,
            Cantidad: cantidad
        };

        if (articulos.length != 0) {
            let posicion = articulos.findIndex(o => o.Codigo == codigo);
            if (posicion != -1) {
                let articuloBuscado = articulos[posicion];
                let cantidadNueva = articuloBuscado.Cantidad;
                articulo.Cantidad += cantidadNueva;

                articulos.splice(posicion, 1);
                if (articulo.Cantidad > 0) {
                    articulos.push(articulo);
                    $('#errorSelect').html("");
                }
                articulos.sort(Compare);
                CargarTabla(articulos);
            } else {
                if (cantidad > 0) {
                    articulos.push(articulo);
                    $('#errorSelect').html("");
                } else {
                    $('#errorSelect').html("Ingrese una cantidad valida");
                }
                articulos.sort(Compare);
                CargarTabla(articulos);
            }

        } else {
            if (cantidad > 0) {
                articulos.push(articulo);
                $('#errorSelect').html("");
            } else {
                $('#errorSelect').html("Ingrese una cantidad valida");
            }
            articulos.sort(Compare);
            CargarTabla(articulos);

        }

    } else {
        let error = "Debe seleccionar un articulo y una cantidad.";
        $('#errorSelect').html(error);
    }
}
function Compare(a, b) {
    if (a.Codigo > b.Codigo) return 1;
    if (a.Codigo < b.Codigo) return -1;

    return 0;
}

function CargarTabla(data) {
    articulos = data;
    articulos.sort(Compare);
    listado = '';
    if (articulos.length == 0) {
        listado = `<tr class="text-center">
                   <td colspan = "4" >No se cargaron articulos</td >
                   </tr >`;
    } else {
        let posicion = 0;
        articulos.forEach(articulo => {
            listado += `<tr><td>
                ${articulo.Descripcion}
                </td>
                <td>
                ${articulo.Codigo}
                </td>
                <td>
                ${articulo.Cantidad}
                </td>
                <td>
                <a onclick="QuitarArticulo(${posicion})">Quitar</a>
                </td>                  
        </tr>`
            posicion++;
        })
    }

    $('#tbody').html(listado);

};


function QuitarArticulo(posicion) {
    articulos.splice(posicion, 1);
    articulos.sort(Compare);
    CargarTabla(articulos);
}
function MandarForm(accion) {
    let idPedido = Number.parseInt(document.getElementById("IdPedido").value);
    let idEstado = Number.parseInt(document.getElementById("IdEstado").value);
    let comentarios = document.getElementById("comentarios").value;
    if (accion == "entregado") {
        var data = {
            Comentarios: comentarios,
            IdPedido: idPedido,
            EstadoPedido: idEstado
        }
    } else {
        var data = {
            Comentarios: comentarios,
            IdPedido: idPedido,
            EstadoPedido: idEstado,
            Articulos: articulos
        }
    }

    fetch('https://localhost:44344/pedido/EditarPedido', {
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
            window.location.href = data;
        })
}


function ObtenerResultados() {
    let data = Number.parseInt(document.getElementById("IdPedido").value);
    fetch('https://localhost:44344/pedido/ObtenerDetallePedido', {
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
            CargarTabla(data);
        })

}
