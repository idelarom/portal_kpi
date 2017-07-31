
$(document).ready(function () {
    IniciarWidgets();
});
//variables globales

//declaramos los objetos de tipo load desde el inicio
var opts = {
    lines: 13 // The number of lines to draw
   , length: 28 // The length of each line
   , width: 14 // The line thickness
   , radius: 42 // The radius of the inner circle
   , scale: .45 // Scales overall size of the spinner
   , corners: 1 // Corner roundness (0..1)
   , color: '#fff' // #rgb or #rrggbb or array of colors
   , opacity: 0.5 // Opacity of the lines
   , rotate: 0 // The rotation offset
   , direction: 1 // 1: clockwise, -1: counterclockwise
   , speed: 1 // Rounds per second
   , trail: 60 // Afterglow percentage
   , fps: 20 // Frames per second when using setTimeout() as a fallback for CSS
   , zIndex: 2e9 // The z-index (defaults to 2000000000)
   , className: 'spinner' // The CSS class to assign to the spinner
   , top: '45%' // Top position relative to parent
   , left: '50%' // Left position relative to parent
   , shadow: false // Whether to render a shadow
   , hwaccel: false // Whether to use hardware acceleration
   , position: 'absolute' // Element positioning
};

//declaramos los objetos de tipo load desde el inicio (load oscuro)
var opts2 = {
    lines: 13 // The number of lines to draw
   , length: 28 // The length of each line
   , width: 14 // The line thickness
   , radius: 42 // The radius of the inner circle
   , scale: .45 // Scales overall size of the spinner
   , corners: 1 // Corner roundness (0..1)
   , color: '#000' // #rgb or #rrggbb or array of colors
   , opacity: 0.5 // Opacity of the lines
   , rotate: 0 // The rotation offset
   , direction: 1 // 1: clockwise, -1: counterclockwise
   , speed: 1 // Rounds per second
   , trail: 60 // Afterglow percentage
   , fps: 20 // Frames per second when using setTimeout() as a fallback for CSS
   , zIndex: 2e9 // The z-index (defaults to 2000000000)
   , className: 'spinner' // The CSS class to assign to the spinner
   , top: '45%' // Top position relative to parent
   , left: '50%' // Left position relative to parent
   , shadow: false // Whether to render a shadow
   , hwaccel: false // Whether to use hardware acceleration
   , position: 'absolute' // Element positioning
};

function ModalClose() {
    $('#myModalExcel').modal('hide');
}

function Init(table) {
    $(table).DataTable({
        "paging": true,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": true,
        "language": {
            "sProcessing": "Procesando...",
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": "No se encontraron resultados",
            "sEmptyTable": "Ningún dato disponible en esta tabla",
            "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
            "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
            "sInfoPostFix": "",
            "sSearch": "Buscar:",
            "sUrl": "",
            "sInfoThousands": ",",
            "sLoadingRecords": "Cargando...",
            "oPaginate": {
                "sFirst": "Primero",
                "sLast": "Último",
                "sNext": "Siguiente",
                "sPrevious": "Anterior"
            },
            "oAria": {
                "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                "sSortDescending": ": Activar para ordenar la columna de manera descendente"
            }
        }
    });
}

function IniciarWidgets() {
    ////verificamos si los divs estan en la lista, si estan cargamos su informacion mediante ajax
    var usuario = $('#ContentPlaceHolder1_hdf_usuario').val();
    $.ajax({
        url: 'inicio.aspx/getDivs',
        contentType: "application/json; charset=utf-8",
        type: "POST",
        dataType: "json",
        data: "{usuario:'" + usuario + "'}",
        success: function (response) {
            var bono = JSON.parse(response.d);
            for (indice = 0; indice < bono.length; indice++) {
                var div = bono[indice].nombre_codigo;
                if (div == "dashboard_kpi_ind") {
                    CargarDashboardbonosIndividual();
                } else if (div == "dashboard_kpi") {
                    CargarDashboardbonos();
                }
            }
            //spinner.stop();
        },
        error: function (result, status, err) {
            console.log("error", result.responseText);
            //spinner.stop();
        }
    });
}
//WIDGET DE DAHSBOARD BONOS INDIVIDUAL
function CargarDashboardbonosIndividual() {
    //load de dashboard bonos
    var target = document.getElementById('dashboard_kpi_ind');
    var spinner = new Spinner(opts).spin(target);

    var usuario = $('#ContentPlaceHolder1_hdf_usuario').val();
    var num_empleado = $('#ContentPlaceHolder1_hdf_numempleado').val();
    var ver_Todos_empleados = $('#ContentPlaceHolder1_hdf_ver_Todos_empleados').val();
    $.ajax({
        url: 'reporte_dashboard_bonos_kpi.aspx/GetDashboardBonosValues_Individual',
        contentType: "application/json; charset=utf-8",
        type: "POST",
        dataType: "json",
        data: "{lista_usuarios:'" + usuario + "',usuario:'" + usuario + "'}",
        success: function (response) {
            var bono = JSON.parse(response.d);
            if (bono.length > 0)
            {
                console.log("bono", bono[0].Total_Final);
                var bono_porcentaje = bono[0]._Total_Final.replace(' %', '');
                $("#bono_trimestral").text(bono[0].Total_Final);
                $("#progress_bar_bono_kpi_ind").css("width", Math.round(bono_porcentaje) + "%");
                $("#progress_bono_kpi_ind").text(bono_porcentaje + " % alcanzado");
            }
            spinner.stop();
        },
        error: function (result, status, err) {
            console.log("error", result.responseText);
            spinner.stop();
        }
    });
}

//WIDGET DE DASHBOARD BONOS GRUPAL
function CargarDashboardbonos() {
    //load de dashboard bonos
    var target = document.getElementById('dashboard_kpi');
    var spinner = new Spinner(opts2).spin(target);


    var usuario = $('#ContentPlaceHolder1_hdf_usuario').val();
    var num_empleado = $('#ContentPlaceHolder1_hdf_numempleado').val();
    var ver_Todos_empleados = $('#ContentPlaceHolder1_hdf_ver_Todos_empleados').val();
    console.log("nem", num_empleado);
    $.ajax({
        url: 'reporte_dashboard_bonos_kpi.aspx/GetDashboardBonosValues',
        contentType: "application/json; charset=utf-8",
        type: "POST",
        dataType: "json",
        data: "{num_empleado:'" + num_empleado + "',usuario:'" + usuario + "', ver_todos_empleados:'" + ver_Todos_empleados + "'}",
        success: function (response) {
            var bono = JSON.parse(response.d);
            if (bono.length > 0)
            {
                var cadena = '';
                for (indice = 0; indice < bono.length; indice++)
                {
                    cadena =cadena + bono[indice].Login+",";
                    $('#table_dashboard_kpi').find('tbody').append('' + '<tr><td>' +
                        bono[indice].Nombre + '</td><td style="text-align: center;">' +
                        bono[indice].Monto_Bono + '</td><td style="text-align: center;">' +
                        bono[indice].Total_Final + '</td><td style="text-align: center;">' +
                        bono[indice]._Total_Final + '</td></tr>');
                }
                if (cadena.length > 1) {
                    cadena = cadena.substring(0, cadena.length - 1);
                    cadena = btoa(cadena);
                    $('#link_dashboard_kpi').attr('href', 'reporte_dashboard_bonos_kpi.aspx?list=' + cadena)
                } else {
                    $('#link_dashboard_kpi').attr('href', 'reporte_dashboard_bonos_kpi.aspx?list=' + cadena)
                }
                Init('#table_dashboard_kpi');
            }                
            spinner.stop();
        },
        error: function (result, status, err) {
            console.log("error", result.responseText);
            spinner.stop();
        }
    });
}