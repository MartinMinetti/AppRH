window.onload = CargarPaginaReturn();

function CargarPaginaReturn() {
    SearchHouseReturnTemp();
}

function AddHouseReturn() {
    //  console.log("Guardo la Casa"); 
    var houseID = $('#HouseID').val();

    $.ajax({
        type: "POST",
        url: '../../Returns/AddHouseReturn',
        data: { HouseID : houseID },
        success: function (resultado){
            if (resultado == true) {
                $('#staticBackdrop').modal('hide');
                

                SearchHouseReturnTemp();

                location.href = "../../Returns/Create";
            }
            else {
                alert("No se pudo agregar la casa, intente nuevamente.");
                console.log();
            }
        },
        error: function (result){
            console.log("Error debido a: " + result);

        },
    })
}


function CancelReturn() {
    //   console.log("Cancelar Alquiler");

    $.ajax({
        type: "POST",
        url: '../../Returns/CancelReturn',
        data: {},
        success: function(resultado){
            if (resultado == true) {
                location.href = "../../Returns/Index";
            }
        },
        error(result){

        },
    })
}


function SearchHouseReturnTemp() {
    // console.log ("Buscar Pelicula.");

    $("#tableHouseReturn").empty();

    $.ajax({
        type: "GET",
        url: '../../Returns/SearchHouseReturnTemp',
        data: {},
        success: function(ListadoHouseReturnTemp){
            // console.log (ListadoHouseReturnTemp); 
            $.each(ListadoHouseReturnTemp, function(index, item){
                $("#tableHouseReturn").append(
                "<tr>" +
                    "<th>" + item.houseName + "</th>" +
                    "<th>" +
                    "<button class='btn btn-danger' onclick='QuitarHouseReturn(" + item.houseID + ");'> Quitar</button>" +
                    "</th>" +
                "</tr>"
                )

            });
            
        },
        error(result){

        },
    })
}


function SearchReturnHouse(ReturnID) {
    // console.log ("Buscar Casa.");

    $("#tablereturndetail").empty();

    $.ajax({
        type: "POST",
        url: '../../Returns/SearchReturnHouse',
        data: { ReturnID : ReturnID},
        success: function(ListadoHouseReturn){
            // console.log (ListadoHouseReturn); 
            $.each(ListadoHouseReturn, function(index, item){
                $("#tablereturndetail").append(
                "<tr>" +
                    "<th>" + item.houseName + "</th>" +
                "</tr>"
                )

            });
            
        },
        error(result){

        },
    })
}


function QuitarHouseReturn(id) {
    // console.log("Quitar Casa");

    $.ajax({
        type: "POST",
        url: '../../Returns/QuitarHouseReturn',
        data: {HouseID : id},
        success: function(resultado){
            if (resultado == true) {
                location.href = "../../Returns/Create";
            }
        },
        error(result){

        },
    })
}

