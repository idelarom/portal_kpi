$(document).ready(function () {
    IniciarWidgets();
});

function ViewEvent()
{

}


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

//VARIABLES GLOBALES

//Regresa el usuario en sesion
function User()
{
    return $('#ContentPlaceHolder1_hdf_usuario').val();
}

//Regresa el numero de empleado del usuario en sesion
function NumEmpleado() {
    return $('#ContentPlaceHolder1_hdf_numempleado').val();;
}

//Regresa si el usuario en sesion puede ver todos los empleados(permiso)
function VerTodosEmpleados() {
    return $('#ContentPlaceHolder1_hdf_ver_Todos_empleados').val();
}

//array que contiene las ejecuciones ajax
var xhrRequests = [];

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
   , zIndex: 10 // The z-index (defaults to 2000000000)
   , className: 'spinner' // The CSS class to assign to the spinner
   , top: '45%' // Top position relative to parent
   , left: '50%' // Left position relative to parent
   , shadow: true // Whether to render a shadow
   , hwaccel: true // Whether to use hardware acceleration
   , position: 'absolute' // Element positioning
};

//declaramos los objetos de tipo load desde el inicio (load oscuro)
var opts2 = {
    lines: 13 // The number of lines to draw
   , length: 28 // The length of each line
   , width: 14 // The line thickness
   , radius: 42 // The radius of the inner circle
   , scale: .8 // Scales overall size of the spinner
   , corners: 1 // Corner roundness (0..1)
   , color: '#000' // #rgb or #rrggbb or array of colors
   , opacity: 0.1 // Opacity of the lines
   , rotate: 0 // The rotation offset
   , direction: 1 // 1: clockwise, -1: counterclockwise
   , speed: 1 // Rounds per second
   , trail: 60 // Afterglow percentage
   , fps: 20 // Frames per second when using setTimeout() as a fallback for CSS
   , zIndex: 10 // The z-index (defaults to 2000000000)
   , className: 'spinner' // The CSS class to assign to the spinner
   , top: '45%' // Top position relative to parent
   , left: '50%' // Left position relative to parent
   , shadow: true // Whether to render a shadow
   , hwaccel: true // Whether to use hardware acceleration
   , position: 'absolute' // Element positioning
};

//funcion que termina todas las ejecuciones ajax guardadas en un array
function CloseAjax(url) {
    $.each(xhrRequests, function (idx, jqXHR) {
        console.log("remove ajax each");
        jqXHR.abort();
        jqXHR = null;
    });
    window.location.href = url;
}

//FUNCION QUE VERIFICA QUE WIDGETS ESTAN LIGADOS AL USUARIO E INICIALIZA LAS FUNCIONES
function IniciarWidgets() {
    ////verificamos si los divs estan en la lista, si estan cargamos su informacion mediante ajax
    var usuario = $('#ContentPlaceHolder1_hdf_usuario').val();
    //guardamos esta ejecucion en un array
    var ajax_ejecutados = [];
    var call = $.ajax({
        url: 'inicio.aspx/getDivs',
        contentType: "application/json; charset=utf-8",
        type: "POST",
        dataType: "json",
        data: "{usuario:'" + usuario + "'}",
        success: function (response) {
            var bono = JSON.parse(response.d);
            for (indice = 0; indice < bono.length; indice++) {
                var div = bono[indice].nombre_codigo;
                if ((div == "dashboard_kpi_ind" || div == "desglo_dashboard_kpi_ind") && jQuery.inArray("CargarDashboardbonosIndividual", ajax_ejecutados) == -1) {
                    
                    ajax_ejecutados.push("CargarDashboardbonosIndividual");
                    CargarDashboardbonosIndividual();
                } else if (div == "dashboard_kpi" && jQuery.inArray("CargarDashboardbonos", ajax_ejecutados) == -1) {
                    ajax_ejecutados.push("CargarDashboardbonos");
                    CargarDashboardbonos();
                }
                else if ((div == "Performance_ing_ind" || div == "desglo_performance_ing_ind") && jQuery.inArray("CargarPerformanceIngenieriaIndividual", ajax_ejecutados) == -1) {
                    ajax_ejecutados.push("CargarPerformanceIngenieriaIndividual");
                    CargarPerformanceIngenieriaIndividual();
                }
                else if (div == "performance_ing" && jQuery.inArray("CargarPerformanceIngenieria", ajax_ejecutados) == -1) {
                    ajax_ejecutados.push("CargarPerformanceIngenieria");
                    CargarPerformanceIngenieria();
                }
            }
        },
        error: function (result, status, err) {
            console.log("error", result.responseText);
        }
    });
    xhrRequests.push(call);
}

//WIDGET DE DAHSBOARD BONOS INDIVIDUAL
function CargarDashboardbonosIndividual() {
    //load de dashboard bonos
    var target = document.getElementById('dashboard_kpi_ind');
    var spinner = new Spinner(opts).spin(target);
    var target2 = document.getElementById('desglo_dashboard_kpi_ind');
    var spinner2 = new Spinner(opts2).spin(target2);

    var usuario = User();// $('#ContentPlaceHolder1_hdf_usuario').val();
    //guardamos esta ejecucion en un array
    var call = $.ajax({
        url: 'reporte_dashboard_bonos_kpi.aspx/GetDashboardBonosValues_Individual',
        contentType: "application/json; charset=utf-8",
        type: "POST",
        dataType: "json",
        data: "{lista_usuarios:'" + usuario + "',usuario:'" + usuario + "'}",
        success: function (response) {
            var bono = JSON.parse(response.d);
            if (bono.length > 0) {
                var bono_porcentaje = bono[0]._Total_Final.replace(' %', '');
                var preventa = bono[0]._Preventa;
                var implementacion = bono[0]._Implementación;
                var soporte = bono[0]._Soporte;
                var kpi = bono[0].KPI_Individual;
                var kpig = bono[0].KPI_Grupo;
                var compromisos = bono[0]._Cump;
                var total_preventa = bono[0].Preventa;
                var total_implemetacion = bono[0].Implementacion;
                var total_soporte = bono[0].Soporte;
                $("#bono_trimestral").text(bono[0].Total_Final);
                $("#dashboard_bonos_totalpreventa").text(total_preventa);
                $("#dashboard_bonos_totalimp").text(total_implemetacion);
                $("#dashboard_bonos_totalsoporte").text(total_soporte);
                $("#dashboard_bonos_preventa").text(preventa);
                $("#dashboard_bonos_imp").text(implementacion);
                $("#dashboard_bonos_soporte").text(soporte);
                $("#dashboard_bonos_kpi").text(kpi);
                $("#dashboard_bonos_kpig").text(kpig);
                $("#dashboard_bonos_compromisos").text(compromisos);
                $("#progress_bar_bono_kpi_ind").css("width", Math.round(bono_porcentaje  > 100 ? 100: bono_porcentaje) + "%");
                $("#progress_bono_kpi_ind").text(bono_porcentaje + " % alcanzado");
            }
            spinner.stop();
            spinner2.stop();
        },
        error: function (result, status, err) {
            console.log("error", result.responseText);
            spinner.stop();
            spinner2.stop();
        }
    });
    xhrRequests.push(call);
}

//WIDGET DE DASHBOARD BONOS GRUPAL
function CargarDashboardbonos() {
    //load de dashboard bonos
    var target = document.getElementById('dashboard_kpi');
    var spinner = new Spinner(opts2).spin(target);
    var usuario = User();
    var num_empleado = NumEmpleado();
    var ver_Todos_empleados = VerTodosEmpleados(); //$('#ContentPlaceHolder1_hdf_ver_Todos_empleados').val();
    //guardamos esta ejecucion en un array
    var call = $.ajax({
        url: 'reporte_dashboard_bonos_kpi.aspx/GetDashboardBonosValues',
        contentType: "application/json; charset=utf-8",
        type: "POST",
        dataType: "json",
        data: "{num_empleado:'" + num_empleado + "',usuario:'" + usuario + "', ver_todos_empleados:'" + ver_Todos_empleados + "'}",
        success: function (response) {
            var bono = JSON.parse(response.d);
            if (bono.length > 0) {
                var cadena = '';
                for (indice = 0; indice < bono.length; indice++) {
                    cadena = cadena + bono[indice].Login + ",";
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
            } else {
                $('#link_dashboard_kpi').attr('href', 'reporte_dashboard_bonos_kpi.aspx');
            }
            spinner.stop();
        },
        error: function (result, status, err) {
            console.log("error", result.responseText);
            spinner.stop();
        }
    });
    xhrRequests.push(call);
}


//WIDGET DE PERFORMANCE INGENIERIA INDIVIDUAL
function CargarPerformanceIngenieriaIndividual() {
    //load de dashboard bonos
    var target = document.getElementById('Performance_ing_ind');
    var spinner = new Spinner(opts).spin(target);
    var target2 = document.getElementById('desglo_performance_ing_ind');
    var spinner2 = new Spinner(opts2).spin(target2);

    var usuario = User();// $('#ContentPlaceHolder1_hdf_usuario').val();
    //guardamos esta ejecucion en un array
    var call = $.ajax({
        url: 'reporte_performance_ingenieria_netdiario.aspx/GetPerformanceIngenieria_Individual',
        contentType: "application/json; charset=utf-8",
        type: "POST",
        dataType: "json",
        data: "{lista_usuarios:'" + usuario + "',usuario:'" + usuario + "'}",
        success: function (response) {
            var Performance = JSON.parse(response.d);
            if (Performance.length > 0) {

                var Soporte = Performance[0].Soporte;
                var Porcentaje_Soporte = Performance[0]._Soporte;
                var Preventa = Performance[0].Preventa;
                var Porcentaje_Preventa = Performance[0]._Preventa;
                var Administrativas = Performance[0].Administrativas;
                var Porcentaje_Administrativas = Performance[0]._Administrativas;
                var Implementacion = Performance[0].Implementacion;
                var Porcentaje_Implementacion = Performance[0]._Implementacion;
                var Total_Horas = Performance[0].Total_Horas;
                var Porcentaje_Total_Horas = Performance[0]._Total_Horas;

                $("#horas_Semanal").text(Total_Horas);
                $("#performance_ing_preventa").text(Porcentaje_Preventa + " %");
                $("#performance_ing_totalpreventa").text(Preventa + " hrs");
                $("#performance_ingenieria_imp").text(Porcentaje_Implementacion + " %");
                $("#performance_ingenieria_totalimp").text(Implementacion + " hrs");
                $("#performance_ingenieria_sop").text(Porcentaje_Soporte + " %");
                $("#performance_ingenieria_totalsop").text(Soporte + " hrs");
                $("#performance_ingenieria_admin").text(Porcentaje_Administrativas + " %");
                $("#performance_ingenieria_totaladmin").text(Administrativas + " hrs");
                $("#performance_ingenieria_th").text(Porcentaje_Total_Horas + " %");
                $("#performance_ingenieria_totaladhr").text(Total_Horas + " hrs");
                $("#progress_bar_performance_ing_ind").css("width", Math.round(Porcentaje_Total_Horas > 100 ? 100 : Porcentaje_Total_Horas) + "%");
                $("#progress_performance_ing_ind").text(Porcentaje_Total_Horas + " % alcanzado");
            }
            spinner.stop();
            spinner2.stop();
        },
        error: function (result, status, err) {
            console.log("error", result.responseText);
            spinner.stop();
            spinner2.stop();
        }
    });
    xhrRequests.push(call);
}

//WIDGET DE PERFORMANCE INGENIERIA GRUPAL
function CargarPerformanceIngenieria() {
    //load de dashboard bonos
    var target = document.getElementById('performance_ing');
    var spinner = new Spinner(opts2).spin(target);
    var usuario = User();
    var num_empleado = NumEmpleado();
    var ver_Todos_empleados = VerTodosEmpleados(); //$('#ContentPlaceHolder1_hdf_ver_Todos_empleados').val();
    //guardamos esta ejecucion en un array
    var call = $.ajax({
        url: 'reporte_performance_ingenieria_netdiario.aspx/GetPerformanceIngenieriaValues',
        contentType: "application/json; charset=utf-8",
        type: "POST",
        dataType: "json",
        data: "{num_empleado:'" + num_empleado + "',usuario:'" + usuario + "', ver_todos_empleados:'" + ver_Todos_empleados + "'}",
        success: function (response) {
            var performance = JSON.parse(response.d);
            if (performance.length > 0) {
                var cadena = '';
                for (indice = 0; indice < performance.length; indice++) {
                    cadena = cadena + performance[indice].Login + ",";
                    $('#table_performance_ing').find('tbody').append('' + '<tr><td>' +
                        performance[indice].Empleado + '</td><td style="text-align: center;">' +
                        performance[indice].Soporte + " hrs/" + performance[indice]._Soporte + " %" + '</td><td style="text-align: center;">' +
                        performance[indice].Preventa + " hrs/" + performance[indice]._Preventa + " %" + '</td><td style="text-align: center;">' +
                        performance[indice].Administrativas + " hrs/" + performance[indice]._Administrativas + " %" + '</td><td style="text-align: center;">' +
                        performance[indice].Implementacion + " hrs/" + performance[indice]._Implementacion + " %" + '</td></tr>');
                }
                if (cadena.length > 1) {
                    cadena = cadena.substring(0, cadena.length - 1);
                    cadena = btoa(cadena);
                    $('#link_dashboard_kpi').attr('href', 'reporte_performance_ingenieria_netdiario.aspx?list=' + cadena)
                } else {
                    $('#link_dashboard_kpi').attr('href', 'reporte_performance_ingenieria_netdiario.aspx?list=' + cadena)
                }
                Init('#table_dashboard_kpi');
            } else {
                $('#link_dashboard_kpi').attr('href', 'reporte_performance_ingenieria_netdiario.aspx');
            }
            spinner.stop();
        },
        error: function (result, status, err) {
            console.log("error", result.responseText);
            spinner.stop();
        }
    });
    xhrRequests.push(call);
}