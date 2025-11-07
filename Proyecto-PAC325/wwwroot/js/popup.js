document.addEventListener("DOMContentLoaded", function () {
    $(document).on("click", ".btn-modal", function () {
        const titulo = $(this).data("titulo");
        let contenido = $(this).data("contenido");
        contenido = JSON.stringify(contenido, null, 2);

        $("#modalTitulo").text(titulo);
        $("#modalContenido").text(contenido);

        $("#popup").css("display", "flex");
    });

    $("#cerrar").on("click", function (e) {
        $("#popup").css("display", "none");
    });
});