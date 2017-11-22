$(document).ready(function () {
    setInterval(CargarNuevosDispositivos, 1500);
    //setInterval(CargarNotificaciones, 5000);

});

function BlockUI() {
    $.blockUI({
        message: '<h3>Cargando</h1>',
        css: {
            border: 'none',
            padding: '15px',
            left: '25%',
            width:'50%',
            backgroundColor: '#000',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            opacity: .5,
            color: '#fff'
        }
    });
}

function UnBlockUI() {
    $.unblockUI();
}
function control_clear(control) {
    var valor = control.value;

    if (valor.length > 0) {
        control.value = "";
    }
}
//DEVUELVE NOTIFICACION DE ESCRITORIO
function NotificationDesktop(Mensaje, title) {
    PlaySound('dist/sounds/notification.mp3');
    // Let's check if the browser supports notifications
    if (!("Notification" in window)) {
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
        Command: toastr["info"](Mensaje, title);
    } else {
        var options = {
            body: Mensaje,
            icon: "img/logo_login.png",
            dir: "ltr",
            tag: "PENDIENTE"
        };
        var notification = new Notification(title, options);
        notification.onclick = function () {
            notification.close();
        };
        setTimeout(function () {
            notification.close();
        }, 8000);
    }
}


function ValidateRange(Object, val_min, val_max, error_mess) {
    // It's a number

    if (Object.value != "") {
        numValue = parseFloat(Object.value);
        min = parseFloat(val_min);
        max = parseFloat(val_max);
        if (numValue < min || numValue > max) {
            Object.value = "1";
            swal({
                title: "Mensaje del Sistema",
                text: error_mess,
                type: 'error',
                showCancelButton: false,
                confirmButtonColor: "#428bca",
                confirmButtonText: "Aceptar",
                closeOnConfirm: false, allowEscapeKey: false
            });
        }
    }
}

//VALIDA QUE SOLO SEAN NUMEROS ENTEROS REALES
function validarEnteros(e) {
    k = (document.all) ? e.keyCode : e.which;
    if (k == 8 || k == 0) return true;
    patron = /[0-9\s\t]/;
    n = String.fromCharCode(k);
    return patron.test(n);
}

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
        url: '../../Pages/Common/Service.asmx/checaItem',
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


function CargarNotificaciones() {
    var usuario = $("#hdf_mp_usuario").val();
    $.ajax({
        url: '../../Pages/Common/Service.asmx/GetAvisos',
        contentType: "application/json; charset=utf-8",
        type: "POST",
        dataType: "json",
        data: "{usuario:'" + usuario + "'}",
        success: function (response) {
            var notificaciones = JSON.parse(response.d);
            $("#menu_notificaciones").empty();
            $('#btn_not').removeAttr('onclick');
            if (notificaciones.length > 0) {
                NotificacionDesktop("Nuevas notificaciones.", "Tiene " + notificaciones.length + " notificacion(es)","img/logo_login.png")
                PlaySound('dist/sounds/notification.mp3');
                $("#numero_notificaciones").show(); 
                $("#numero_notificaciones").text(notificaciones.length);
                $("#lblnumero_notificaciones").text(notificaciones.length);
                
                for (indice = 0; indice < notificaciones.length; indice++) {
                    var notificacion = notificaciones[indice].notificacion;
                    var url = notificaciones[indice].url;
                    var icono = notificaciones[indice].icono;
                    var id_notificacion = notificaciones[indice].id_notificacion;

                    //$("#menu_notificaciones").append('<li style="cursor:pointer;"><a href="#" aria-hidden="true"><i class="' + (icono == null ? "fa fa-info-circle" : icono) + '" aria-hidden="true"></i>' + notificacion + '</a></li>');
                    $("#menu_notificaciones").append('<li style="cursor:pointer;"><a onclick="LeerNotificacion(\'' + usuario + '\',\'' + (url == null ? "#" : url) + '\');"><i class="' + (icono == null ? "fa fa-info-circle" : icono) + '" aria-hidden="true"></i>' + notificacion + '</a></li>');
                }
               // $("#menu_notificaciones").append('<li style="cursor:pointer;"><a onclick="LeerNotificacion(\'' + usuario + '\',\'' + (url == null ? "#" : url) + '\');"><i class="' + (icono == null ? "fa fa-info-circle" : icono) + '" aria-hidden="true"></i>' + notificacion + '</a></li>');

                $('#btn_not').attr('onClick', 'LeerNotificacion(\'' + usuario + '\',\'' + "" + '\');');
            }
        },
        error: function (result, status, err) {
            console.log("error", result.responseText);
        }
    });
}

function LeerNotificacion(usuario, url) {
    if (url != '') {
        window.location.href = url;
    } else {
        $.ajax({
            url: 'Service.asmx/LeerNotificacion',
            contentType: "application/json; charset=utf-8",
            type: "POST",
            dataType: "json",
            data: "{usuario:'" + usuario + "'}",
            success: function (response) {
                var mensaje = response.d;
                if (mensaje != "") {
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
                    Command: toastr["danger"](mensaje, "Error en el sistema")
                }
            },
            error: function (result, status, err) {
                console.log("error", result.responseText);
            }
        });
    }
}