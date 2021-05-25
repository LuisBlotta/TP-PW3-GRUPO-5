﻿var articulos = new Array();

function AgregarArticulo() {
    let codigo = Number.parseInt(document.getElementById("articulo").value);
    let selectArticulo = document.getElementById("articulo");
    let descripcion = selectArticulo.options[selectArticulo.selectedIndex].text;
    descripcion = descripcion.split("#")[0];
    let cantidad = Number.parseInt(document.getElementById("cantidad").value);

    if (!Number.isNaN(codigo) && !Number.isNaN(cantidad)) {
        let articulo = {
            Codigo: codigo,
            Descripcion: descripcion,
            Cantidad: cantidad
        };

        if (articulos.length != 0) {

            if (BuscarArticulo(codigo)) {
                let posicion = articulos.findIndex(o => o.Codigo == codigo);
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
function BuscarArticulo(codigo) {
    let seEncontro = false;
    articulos.forEach(a => {
        if (a.Codigo == codigo) {
            seEncontro = true;
        }
    });
    return seEncontro;
}

function CargarTabla(articulos) {
    listado = '';
    if (articulos.length == 0) {
        listado = `<tr class="text-center">
                   <td colspan = "4" >No se cargaron articulos</td >
                   </tr >`;
    } else {
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
                <a>Quitar</a>
                </td>                  
        </tr>`
        })
    }

    $('#tbody').html(listado);

};

//Enviar Formulario

function MandarForm(accion) {

    let selectCliente = document.getElementById("cliente");
    let nombreCl = selectCliente.options[selectCliente.selectedIndex].text;
    nombreCl = nombreCl.split("#")[0];


    let data = {
        NumeroCliente: document.getElementById('cliente').value,
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
            MostrarMensaje(data.Mensaje,nombreCl);
            ResetForm();
        })
}

function MostrarMensaje(mensaje, nombreCl) {
    let html = `<div class="alert alert-success alert-dismissible "><label>Pedido de ${nombreCl} nro. 123 creado con exito</label>
        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
    </div>`
    $('#alert').html(html);
}

function ResetForm() {
    articulos = new Array();
    document.getElementById("cliente").value = "";
    document.getElementById("articulo").value = "";
    document.getElementById("cantidad").value = "";
    document.getElementById("comentarios").value = "";
    CargarTabla(articulos);
}
// Select2
$(document).ready(function () {
    $('.js-example-basic-single').select2();
});

$(document).ready(function () {
    $('.alert').hide();
    CargarTabla(articulos);
});



