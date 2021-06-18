// Select2
$(document).ready(function () {
    $('.select2Pedido').select2();
    ObtenerResultados();
});


function ObtenerResultados() {
    let data = Number.parseInt(document.getElementById("IdPedido").value);

    fetch('https://localhost:44344/pedido/ObtenerPedido', {
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
            // LLEGA LA INFO AHORA HAY QUE REALIZAR LAS FUNCIONES !!!!!!
        })
}


