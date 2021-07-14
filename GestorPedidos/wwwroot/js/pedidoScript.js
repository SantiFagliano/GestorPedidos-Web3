﻿$(document).ready(function () {
    $('#articulos-select').select2();
    $('#articulos-select2').select2();
    $('#articulos-select3').select2();

    $('#table_id_pedidos').dataTable({
        "order": [[2, "asc"]]
    });

    $(function () {
        $("#flexCheckCheckedPedido").on('change', function () {
            if ($(this).is(':checked')) {
                location.href = "https://localhost:44322/Pedido/ListarEntregados";
            }
            else {
                location.reload();
            }
        });

        $("#flexCheckDefaultDosMeses").on('change', function () {
            if ($(this).is(':checked')) {
                location.href = "https://localhost:44322/Pedido/ListarUltimosDosMeses";
            }
            else {
                location.reload();
            }
        });
    });
});
