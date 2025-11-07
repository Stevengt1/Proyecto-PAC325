// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$("#cerrar-modal").on("click", function () {
    $("#mensaje").fadeOut();

    // Modal del SINPE
    document.addEventListener("DOMContentLoaded", function () {
        const abrirModal = document.getElementById("abrirModal");
        const cerrarModal = document.getElementById("cerrarModal");
        const modal = document.getElementById("modalSinpe");

        if (abrirModal && cerrarModal && modal) {
            abrirModal.addEventListener("click", () => {
                modal.style.display = "block";
            });

            cerrarModal.addEventListener("click", () => {
                modal.style.display = "none";
            });

            window.addEventListener("click", (e) => {
                if (e.target === modal) {
                    modal.style.display = "none";
                }
            });
        }
    });
