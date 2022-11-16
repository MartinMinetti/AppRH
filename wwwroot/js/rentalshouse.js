window.onload = CargarPagina();

function CargarPagina() {
    SearchTempHouse();
}


function AddHouseTemp() {
    console.log("Guardo la Casa"); 
    var houseID = $('#HouseID').val();

    $.ajax({
        type: "POST",
        url: '../../Rentals/AddHouseTemp',
        data: { HouseID : houseID },
        success: function (resultado){
            if (resultado == true) {
                $('#staticBackdrop').modal('hide');
                

                SearchTempHouse();

                location.href = "../../Rentals/Create";
            }
            else {
                alert("No se pudo agregar la Pelicula, intente nuevamente.");
                console.log();
            }
        },
        error: function (result){
            console.log("Error debido a: " + result);

        },
    })
}



function CancelRental() {
    // console.log("Cancelar Alquiler");

    $.ajax({
        type: "POST",
        url: '../../Rentals/CancelRental',
        data: {},
        success: function(resultado){
            if (resultado == true) {
                location.href = "../../Rentals/Index";
            }
        },
        error(result){

        },
    })
}


function SearchTempHouse() {
    // console.log ("Buscar casa.");

    $("#tablesHouses").empty();

    $.ajax({
        type: "GET",
        url: '../../Rentals/SearchTempHouse',
        data: {},
        success: function(ListadoHouseTemp){
            // console.log (ListadoHouseTemp); 
            $.each(ListadoHouseTemp, function(index, item){
                $("#tablesHouses").append(
                "<tr>" +
                    "<th>" + item.houseName + "</th>" +
                    "<th>" +
                    "<button class='btn btn-danger' onclick='QuitarHouse(" + item.houseID + ");'> Quitar</button>" +
                    "</th>" +
                "</tr>"
                )

            });
            
        },
        error(result){

        },
    })
}

function SearchRentalHouse (rentalID) {
    // console.log ("Buscar House.");

    $("#tablesrentaldetail").empty();

    $.ajax({
        type: "GET",
        url: '../../Rentals/SearchRentalHouse',
        data: { RentalID : rentalID },
        success: function(ListadoHouse){
            // console.log (ListadoHouse); 
            $.each(ListadoHouse, function(index, item){
                $("#tablesrentaldetail").append(
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


function QuitarHouse(id) {
    // console.log("Quitar House");

    $.ajax({
        type: "POST",
        url: '../../Rentals/QuitarHouse',
        data: { HouseID : id },
        success: function(resultado){
            if (resultado == true) {
                location.href = "../../Rentals/Create";
            }
        },
        error(result){

        },
    })
}


// function OneHouse(){
//     $.ajax({
//         type: "POST",
//         url: '../../Rentals/OneHouse',
//         data: {},
//         success: function(resultado){
//             if (resultado == true) {
                
//             }
//         },
//         error(result){

//         },
//      })
// }

