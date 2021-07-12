var articulos = new Array();

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

function CargarTabla(articulos) {
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
                <a onclick="quitarArticulo(${posicion})">Quitar</a>
                </td>                  
        </tr>`
            posicion++;
        })
    }

    $('#tbody').html(listado);

};

//Enviar Formulario

function MandarForm(accion) {
    let selectCliente = document.getElementById("cliente");
    let idCliente = Number.parseInt(selectCliente.value);

    if (articulos.length > 0 && !Number.isNaN(idCliente)) {
        let nombreCl = selectCliente.options[selectCliente.selectedIndex].text;
        nombreCl = nombreCl.split("#")[0];


        let data = {
            IdCliente: document.getElementById('cliente').value,
            Articulos: articulos,
            Accion: accion,
            Comentarios: document.getElementById('comentarios').value
        }

        fetch('https://localhost:44344/Pedido/NuevoPedido', {
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
                if (data.Accion == "guardar") {
                    window.location.href = '/Pedido/Index';
                }
                window.location.href = '/Pedido/NuevoPedido';
            })
    } else {
        let error = "Debe seleccionar un cliente y al menos un articulo.";
        $('#errorSelect').html(error);
    }

}

function ResetForm() {
    articulos = new Array();
    document.getElementById("cliente").selectedIndex = 0;
    document.getElementById("articulo").selectedIndex = 0;
    $('.select2NuevoPedido').select2();
    document.getElementById("cantidad").value = "";
    document.getElementById("comentarios").value = "";
    $('#errorSelect').html("");
    CargarTabla(articulos);
}

function quitarArticulo(posicion) {
    articulos.splice(posicion, 1);
    articulos.sort(Compare);
    CargarTabla(articulos);
}
// Select2
$(document).ready(function () {
    $('.select2NuevoPedido').select2();
});

$(document).ready(function () {
    CargarTabla(articulos);
});



