$(document).ready(function () {
    setInterval(CargarNuevosDispositivos, 1500);
});

function PlaySound(path) {
    var audio = new Audio(path);
    audio.play();
}

function LoadPage() {
    $("#div_load").show();
    $("#div_content").hide();
}

function LoadPageHide() {
    $("#div_load").hide();
    $("#div_content").show();
}

function keyPressInteger(sender, args) {
    var text = sender.get_value() + args.get_keyCharacter();
    if (!text.match('^[0-9]+$'))
        args.set_cancel(true);
}

function AlertGO(TextMess, URL) {
    swal({
        title: "Mensaje del Sistema",
        text: TextMess,
        type: 'success',
        showCancelButton: false,
        confirmButtonColor: "#428bca",
        confirmButtonText: "Aceptar",
        closeOnConfirm: false,
        allowEscapeKey: false
    },
       function () {
           location.href = URL;
       });
}

function ModalShow(modal) {
    LoadPageHide();
    $(modal).modal('show');
    return true;
}

function ModalCloseGlobal(modal) {
    $(modal).modal('hide');
}

//VALIDA QUE SOLO SEAN NUMEROS ENTEROS REALES
function validarEnteros(e) {
    k = (document.all) ? e.keyCode : e.which;
    if (k == 8 || k == 0) return true;
    patron = /[0-9\s\t]/;
    n = String.fromCharCode(k);
    return patron.test(n);
}

function keyPressInteger(sender, args) {
    var text = sender.get_value() + args.get_keyCharacter();
    if (!text.match('^[0-9]+$')) {
        args.set_cancel(true);
    }
}

//valida un correo
function validarEmail(Object) {
    var valor = Object.value;
    if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(valor)) {
        return (true)
    }
    alert("La dirección de email " + valor + " es incorrecta.")
    return (false)
}

function ValidateUF(Object, size_max) {
    var size = (Object.files[0].size) / 1000000;
    if (size > size_max) {
        Object.value = "";
        swal("Mensaje del sistema", "El tamaño maximo para la subida de archivos es " + size_max + " mb.", "error");
        return false;
    } else {
        return true;
    }
}

function ConfirmFotoPerfil(msg) {
    if (confirm(msg)) {
        $("#lnksubiendofotoperfil").show();
        $("#lnksubirfotoperfil").hide();
        return true;
    } else {
        return false;
    }
}

function ConfirmGuardaConfig(msg) {
    if (confirm(msg)) {
        $("#lnkcargandotermina22").show();
        $("#lnkguardarconfiguracion22").hide();
        return true;
    } else {
        return false;
    }
}

function ShowNewDevice() {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": true,
        "progressBar": true,
        "positionClass": "toast-top-full-width",
        "preventDuplicates": true,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "500000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }

    toastr.options.onclick = function () {
        ModalShow("#myModalUserInfo");
    };
    Command: toastr["warning"]("Se detecto un nuevo dispositivo conectado. Da clic sobre este mensaje para ver mas opciones.", "Nuevo Inicio de Sesión")
    PlaySound('dist/sounds/info.mp3');
}

function CargarNuevosDispositivos() {
    $.ajax({
        url: 'Service.asmx/checaItem',
        contentType: "application/json; charset=utf-8",
        type: "POST",
        dataType: "json",
        success: function (response) {
            var mensaje = response.d;
            if (mensaje != "") {
                document.getElementById('lnkactualizar').click();
                ShowNewDevice();
            }
        },
        error: function (result, status, err) {
            console.log("error", result.responseText);
        }
    });
}

function CloseDeviceMP(id_usuario_sesion, command) {
    var myHidden = document.getElementById('hdfid_usuario_sesion');
    myHidden.value = id_usuario_sesion;
    var commando = document.getElementById('hdfcommand');
    commando.value = command;
    var msg = command == "cerrar"
        ? "¿Desea cerrar sesión en este dispositivo. Tenga en cuenta que podria perderse información no guardada?" :
       "¿Desea bloquear el inicio de sesión en este dispositivo. Tenga en cuenta que podria perderse información no guardada?";
    if (confirm(msg)) {
        document.getElementById('btncerrarsesion').click();
    }

    return false;
}

function LoadSinc(msg) {
    if (confirm(msg)) {
        return true;
    } else {
        return false;
    }
}
