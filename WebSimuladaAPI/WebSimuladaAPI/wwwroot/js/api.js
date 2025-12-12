$("#formConsulta").submit(function (e) {
    e.preventDefault();

    var telefono = $("#telefonoCaja").val();
    var token = JWT_TOKEN;

    $.ajax({
        url: `https://localhost:7275/api/Sinpe/${telefono}`,
        type: "GET",
        headers: {
            "Authorization": "Bearer " + token
        },
        success: function (data) {
            $("#tablaSinpe").empty();
            data.forEach(s => {
                $("#tablaSinpe").append(`
                    <tr>
                        <td>${s.idSinpe}</td>
                        <td>${s.telefonoOrigen}</td>
                        <td>${s.telefonoDestinatario}</td>
                        <td>${s.monto}</td>
                        <td>${s.fechaDeRegistro}</td>
                        <td>${s.estado}</td>
                    </tr>
                `);
            });
        },
        error: function (err) {
            alert("Error consultando SINPE: " + err.responseText);
        }
    });
});

$("#formSync").submit(function (e) {
    e.preventDefault();

    var id = $("#idSinpeSync").val();
    var token = JWT_TOKEN;

    $.ajax({
        url: `https://localhost:7275/api/Sinpe/${id}`,
        type: "POST",
        headers: {
            "Authorization": "Bearer " + token
        },
        success: function (data) {
            $("#resultadoSync").val(JSON.stringify(data, null, 2));
        },
        error: function (err) {
            $("#resultadoSync").val("Error: " + err.responseText);
        }
    });
});

