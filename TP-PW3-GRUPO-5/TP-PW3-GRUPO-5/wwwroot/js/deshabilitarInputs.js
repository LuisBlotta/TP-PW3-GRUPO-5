$(document).ready(function () {
    $("#myForm :input").prop("disabled", true);
    $("#btnEditar").hide();
    $(".btnEditar").hide();

    setTimeout(function () {
        $(".btnQuitar").hide();
    }, 50);

});

