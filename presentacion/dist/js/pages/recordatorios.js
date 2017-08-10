//declaramos los objetos de tipo load desde el inicio (load oscuro)
var opts2 = {
    lines: 13 // The number of lines to draw
   , length: 28 // The length of each line
   , width: 14 // The line thickness
   , radius: 42 // The radius of the inner circle
   , scale: .7 // Scales overall size of the spinner
   , corners: 1 // Corner roundness (0..1)
   , color: '#000' // #rgb or #rrggbb or array of colors
   , opacity: 0.1 // Opacity of the lines
   , rotate: 0 // The rotation offset
   , direction: 1 // 1: clockwise, -1: counterclockwise
   , speed: 1 // Rounds per second
   , trail: 60 // Afterglow percentage
   , fps: 20 // Frames per second when using setTimeout() as a fallback for CSS
   , zIndex: 2e9 // The z-index (defaults to 2000000000)
   , className: 'spinner' // The CSS class to assign to the spinner
   , top: '45%' // Top position relative to parent
   , left: '50%' // Left position relative to parent
   , shadow: true // Whether to render a shadow
   , hwaccel: true // Whether to use hardware acceleration
   , position: 'absolute' // Element positioning
};

function User() { return "IDELAROM"; }

function GetRecordsToday() {
    var usuario = User();

    var call = $.ajax({
        url: 'recordatorios.aspx/GetRecordsToday',
        contentType: "application/json; charset=utf-8",
        type: "POST",
        dataType: "json",
        data: "{user:'" + usuario + "'}",
        success: function (response) {
            var bono = JSON.parse(response.d);
            if (bono.length > 0) {
                console.log("response", bono);
            }
        },
        error: function (result, status, err) {
            console.log("error", result.responseText);
        }
    });
}

function ConfirmwidgetProyectoModal(msg) {
    if (confirm(msg)) {
        $("#ContentPlaceHolder1_lnkcargando").show();
        $("#ContentPlaceHolder1_lnkguardar").hide();
        return true;
    } else {
        return false;
    }
}

function ConfirmEntregableDelete(msg) {
    if (confirm(msg)) {
        $("#ContentPlaceHolder1_load_items").show();
        return ReturnPrompMsg(msg);
    } else {

        $("#ContentPlaceHolder1_load_items").hide();
        return false;
    }
}

function ReturnPrompMsg() {
    var motivo = prompt("Motivo de Eliminación", "");
    if (motivo != null) {
        if (motivo != '') {
            var myHidden = document.getElementById('ContentPlaceHolder1_hdfmotivos');
            myHidden.value = motivo;
            return true;
        } else {
            alert('ES NECESARIO EL MOTIVO DE LA ELIMINACIÓN.');
            ReturnPrompMsg();
            return false;
        }
    } else {
        return false;
    }
}

function EditRecordatorios(id_rec) {
    var myHidden = document.getElementById('ContentPlaceHolder1_hdfid_rec');
    myHidden.value = id_rec;
    document.getElementById('ContentPlaceHolder1_btnedit').click();
    return true;
}

function sconfirm(msg) {
    if (confirm(msg)) {
        $("#ContentPlaceHolder1_load_items").show();
        return true;
    } else {
        return false;
    }
}