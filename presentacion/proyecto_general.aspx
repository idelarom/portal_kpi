<%@ Page Title="Proyecto" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="proyecto_general.aspx.cs" Inherits="presentacion.proyecto_general" ValidateRequest="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://code.highcharts.com/highcharts.js"></script>
    <script src="https://code.highcharts.com/highcharts-more.js"></script>
    <script src="https://code.highcharts.com/modules/data.js"></script>
    <script src="https://code.highcharts.com/modules/exporting.js"></script>
    <link href="plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css" rel="stylesheet" />
    <script src="plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js"></script>

    <link rel="stylesheet" href="primitive/js/jquery/ui-lightness/jquery-ui-1.10.2.custom.css" />
    <script type="text/javascript" src="primitive/js/jquery/jquery-ui-1.10.2.custom.min.js"></script>

    <script type="text/javascript" src="primitive/js/primitives.min.js"></script>
    <link href="primitive/css/primitives.latest.css" media="screen" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function ModalClose() {
            $('#myModal').modal('hide');
            $('#myModalEntregablle').modal('hide');
            $('#myModalTareas').modal('hide');
            $('#myModalExcel').modal('hide');
            $('#myModalCorreos').modal('hide');
            $('#myModalDocumentos').modal('hide');
            $('#myModalMinutas').modal('hide');
            $('#myModalPendientes').modal('hide');
            $('#myModalLoad').modal('hide');
        }

        function OnClickConfirm() {
            var nombre = $("#<%= rtxtproyecto.ClientID%>").val();
            var fechainicio = $("#<%= rdtpinicio.ClientID%>").val();
            var fechafin = $("#<%= rdtpfin.ClientID%>").val();
            var descripcion = $("#<%= rtxtdescripcion.ClientID%>").val();
            var avance = $("#<%= rtxtavance.ClientID%>").val();
            if (nombre != "" && fechafin != "" && fechainicio != "" && avance != "" && descripcion != "") {

                return confirm('¿Desea Actualizar los datos del Proyecto?');
            }
        }


        $(document).ready(function () {
            IniciarEditor();
            Init();
            // CargarGrafica();
        });

        function ClientNodeClicked(sender, eventArgs) {
        }

        function IniciarEditor() {
            $("#ContentPlaceHolder1_txtbody").wysihtml5();

        }
        function Init() {

            $('.dvv').DataTable({
                "paging": true,
                "lengthChange": true,
                "searching": true,
                "ordering": false,
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

        function ChangedTextLoad()
        {            
            $("#<%= imgloadempleados.ClientID%>").show();
            $("#<%= lblbe.ClientID%>").show();
            return true;
        }
         function ChangedTextLoad2()
        {            
            $("#<%= imgloadempleado_.ClientID%>").show();
            $("#<%= lblbe2.ClientID%>").show();
            return true;
        }
            function ChangedTextLoad3()
        {            
            $("#<%= imgloadcliente.ClientID%>").show();
            $("#<%= lblloadcliente.ClientID%>").show();
            return true;
        }
        function OpenModalEntregableForGraph(id_entregable) {
            // alert('ID ' + id_entregable);
            //ModalShow('#myModalLoad');
            
            var myHidden = document.getElementById('<%= hdfid_entregable.ClientID %>');

            myHidden.value = id_entregable;
            document.getElementById('<%= btneditarentregablegraph.ClientID%>').click();
        }

        function ConfirmExcelModal(msg) {
            if (confirm(msg)) {
                $("#<%= lnkloadingexcel.ClientID%>").show();
                $("#<%= lnksubirexcel.ClientID%>").hide();
                return true;
            } else {
                return false;
            }
        }

        function ConfirmCorreoModal(msg) {
            if (confirm(msg)) {
                $("#<%= lnkenviandocorreo.ClientID%>").show();
                $("#<%= lnkenviarcorreo.ClientID%>").hide();
                return true;
            } else {
                return false;
            }
        }

        function ConfirmDocModal(msg) {
            if (confirm(msg)) {
                $("#<%= lnkcargandodocumento.ClientID%>").show();
                $("#<%= lnkupload.ClientID%>").hide();
                return true;
            } else {
                return false;
            }
        }

        function ConfirmMinutaModal(msg) {
            if (confirm(msg)) {
                $("#<%= lnkcargandoMinuta.ClientID%>").show();
                $("#<%= lnkguardarminuta.ClientID%>").hide();
                return true;
            } else {
                return false;
            }
        }

        function ConfirmCharterModal(msg) {
            if (confirm(msg)) {
                $("#<%= lnkcargandocharter.ClientID%>").show();
                $("#<%= lnkguardarcharter.ClientID%>").hide();
                return true;
            } else {
                return false;
            }
        }

        function ConfirmMilestoneModal(msg) {
            if (confirm(msg)) {
                $("#<%= lnkcargandomilestone.ClientID%>").show();
                $("#<%= lnkGuardarEntregable.ClientID%>").hide();
                return true;
            } else {
                return false;
            }
        }

        function ConfirmTareaModal(msg) {
            if (confirm(msg)) {
                $("#<%= lnkcargandoactividad.ClientID%>").show();
                $("#<%= lnkguardartarea_modal.ClientID%>").hide();
                return true;
            } else {
                return false;
            }
        }

        function ConfirmInvolucradoModal(msg) {
            if (confirm(msg)) {
                $("#<%= lnkcaegandoinvolu.ClientID%>").show();
                $("#<%= lnkguardarinvolucrado.ClientID%>").hide();
                return true;
            } else {
                return false;
            }
        }

        function ConfirmClientesModal(msg) {
            if (confirm(msg)) {
                $("#<%= lnkcargandocliente.ClientID%>").show();
                $("#<%= lnkguardarcliente.ClientID%>").hide();
                return true;
            } else {
                return false;
            }
        }

        function ConfirmEmpleadoProyectoModal(msg) {
            if (confirm(msg)) {
                $("#<%= lnkcargarempleados.ClientID%>").show();
                $("#<%= lnkguardarempleado.ClientID%>").hide();
                return true;
            } else {
                return false;
            }
        }
         function ConfirmTerminarProyecto(msg) {
            if (confirm(msg)) {
                $("#<%= lnkcargandotermina.ClientID%>").show();
                $("#<%= lnkterminaproyecto.ClientID%>").hide();
                return true;
            } else {
                return false;
            }
        }
        function ConfirmEntregableDelete(msg) {
            if (confirm(msg)) {
                return ReturnPrompMsg(msg);
            } else {
                return false;
            }
        }

        function ReturnPrompMsg() {
            var motivo = prompt("Motivo de Eliminación", "");
            if (motivo != null) {
                if (motivo != '') {
                    var myHidden = document.getElementById('<%= hdfmotivos.ClientID %>');
                    myHidden.value = motivo;
                    return true;
                } else {
                    alert('ES NECESARIO EL MOTIVO DE LA ELIMINACIÓN.');
                    ReturnPrompMsg();
                }
            } else {
                return false;
            }

        }

        function ConfirmUploadEncuesta(msg) {
            if (confirm(msg)) {
                $("#<%= lnkcargandoencuesta.ClientID%>").show();
                $("#<%= lnksubirencuestas.ClientID%>").hide();
                return true;
            } else {
                return false;
            }
        }

        
        function ConfirmUploadCierre(msg) {
            if (confirm(msg)) {
                $("#<%= lnkcargandocierre.ClientID%>").show();
                $("#<%= lnkdoccierre.ClientID%>").hide();
                return true;
            } else {
                return false;
            }
        }

        
        function ConfirmUploadKit(msg) {
            if (confirm(msg)) {
                $("#<%= lnkcargandokit.ClientID%>").show();
                $("#<%= lnkkit.ClientID%>").hide();
                return true;
            } else {
                return false;
            }
        }
        function CargarDiagrama() {
            $.ajax({
                type: "POST",
                url: "proyecto_general.aspx/GetInvolucrados",
                contentType: "application/json; charset=utf-8",
                success: OnSuccess
            });
            function OnSuccess(response) {
                var items_post = response.d;
                if (items_post.length > 0) {
                    var options = new primitives.orgdiagram.Config();
                    var items = [];
                    $.each(items_post, function (index, val) {
                        var x = new primitives.orgdiagram.ItemConfig({
                            id: val.idpinvolucrado,
                            parent: val.id_parent,
                            title: val.rol,
                            description: val.nombre,
                            image: "img/user.png",
                            phone: val.telefono,
                            email: val.correo,
                            templateName: "contactTemplate",
                            href: "mailto:" + val.correo,
                        });
                        items.push(x);

                    });
                    var buttons = [];
                    buttons.push(new primitives.orgdiagram.ButtonConfig("delete", "ui-icon-close", "Delete"));
                    buttons.push(new primitives.orgdiagram.ButtonConfig("properties", "ui-icon-gear", "Info"));
                    buttons.push(new primitives.orgdiagram.ButtonConfig("add", "ui-icon-person", "Add"));
                    //  options.buttons = buttons;
                    options.hasButtons = primitives.common.Enabled.True;
                    options.hasSelectorCheckbox = primitives.common.Enabled.False;
                    options.onButtonClick = function (e, data) {
                        var message = "User clicked '" + data.name + "' button for item '" + data.context.title + "'.";
                        alert(message);
                    };

                    options.items = items;
                    options.cursorItem = 0;
                    options.templates = [getContactTemplate()];
                    options.onItemRender = onTemplateRender;
                    options.pageFitMode = primitives.common.PageFitMode.None;
                    options.hasSelectorCheckbox = primitives.common.Enabled.False;
                    options.onMouseClick = function (e, data) {
                        //alert("User clicked on item '" + data.context.title + "'.");
                        //var redi = parseInt(data.context.redirect);
                        var id = data.context.id;
                        var myHidden = document.getElementById('<%= hdfid_involucrado.ClientID %>');
                        
                        $("#<%= div_nievoinvo.ClientID%>").show();
                        $("#<%= div_listempleados.ClientID%>").hide();
                        myHidden.value = id;
                        document.getElementById('<%= btneditarinvol.ClientID%>').click();
                    }
                    jQuery("#basicdiagram").orgDiagram(options);

                    function onTemplateRender(event, data) {
                        var hrefElement = data.element.find("[name=readmore]");
                        var emailElement = data.element.find("[name=email]");
                        switch (data.renderingMode) {
                            case primitives.common.RenderingMode.Create:
                                /* Initialize widgets here */
                                hrefElement.click(function (e) {
                                    /* Block mouse click propogation in order to avoid layout updates before server postback*/
                                    primitives.common.stopPropagation(e);
                                });
                                emailElement.click(function (e) {
                                    /* Block mouse click propogation in order to avoid layout updates before server postback*/
                                    primitives.common.stopPropagation(e);
                                });
                                break;
                            case primitives.common.RenderingMode.Update:
                                /* Update widgets here */
                                break;
                        }

                        var itemConfig = data.context;

                        if (data.templateName == "contactTemplate") {
                            data.element.find("[name=titleBackground]").css({ "background": itemConfig.itemTitleColor });

                            data.element.find("[name=photo]").attr({ "src": itemConfig.image });
                            hrefElement.attr({ "href": itemConfig.href });
                            emailElement.attr({ "href": ("mailto:" + itemConfig.email + "") });

                            var fields = ["title", "description", "phone", "email"];
                            for (var index = 0; index < fields.length; index++) {
                                var field = fields[index];

                                var element = data.element.find("[name=" + field + "]");
                                if (element.text() != itemConfig[field]) {
                                    element.text(itemConfig[field]);
                                }
                            }
                        }
                    }

                    function getContactTemplate() {
                        var result = new primitives.orgdiagram.TemplateConfig();
                        result.name = "contactTemplate";

                        result.itemSize = new primitives.common.Size(220, 90);
                        result.minimizedItemSize = new primitives.common.Size(3, 3);
                        result.highlightPadding = new primitives.common.Thickness(2, 2, 2, 2);

                        var itemTemplate = jQuery(
                          '<div class="bp-item bp-corner-all bt-item-frame">'
                            + '<div name="titleBackground" class="bp-item bp-corner-all bp-title-frame" style="top: 2px; left: 2px; width: 216px; height: 20px;">'
                                + '<div name="title" class="bp-item bp-title" style="top: 3px; left: 6px; width: 208px; height: 18px;">'
                                + '</div>'
                            + '</div>'
                            + '<div name="phone" class="bp-item" style="top: 62px; left: 9px; width: 162px; height: 36px; font-size: 10px;"></div>'
                            + '<div class="bp-item" style="top: 44px; left: 9px; width: 185px; height: 18px; font-size: 12px;"><a name="email"></a></div>'
                            + '<div name="description" class="bp-item" style="top: 26px; left: 9px; width: 162px; height: 18px; font-size: 12px;"></div>'

                        + '</div>'
                        ).css({
                            width: result.itemSize.width + "px",
                            height: result.itemSize.height + "px"
                        }).addClass("bp-item bp-corner-all bt-item-frame");

                        result.itemTemplate = itemTemplate.wrap('<div>').parent().html();

                        return result;
                    }

                }

            }

        }

        function CargarDiagramaEmpleados() {
            $.ajax({
                type: "POST",
                url: "proyecto_general.aspx/GetEmpleadosInvolucrados",
                contentType: "application/json; charset=utf-8",
                success: OnSuccess
            });
            function OnSuccess(response) {
                var items_post = response.d;
                if (items_post.length > 0) {
                    var options = new primitives.orgdiagram.Config();
                    var items = [];
                    $.each(items_post, function (index, val) {
                        var x = new primitives.orgdiagram.ItemConfig({
                            id: val.idpinvolucrado,
                            parent: val.id_parent,
                            title: val.rol,
                            description: val.nombre,
                            image: "img/user.png",
                            phone: val.telefono,
                            email: val.correo,
                            templateName: "contactTemplate",
                            href: "mailto:" + val.correo,
                            itemTitleColor: primitives.common.Colors.Red
                        });
                        items.push(x);

                    });
                    var buttons = [];
                    buttons.push(new primitives.orgdiagram.ButtonConfig("delete", "ui-icon-close", "Delete"));
                    buttons.push(new primitives.orgdiagram.ButtonConfig("properties", "ui-icon-gear", "Info"));
                    buttons.push(new primitives.orgdiagram.ButtonConfig("add", "ui-icon-person", "Add"));
                    //  options.buttons = buttons;
                    options.hasButtons = primitives.common.Enabled.True;
                    options.hasSelectorCheckbox = primitives.common.Enabled.False;
                    options.onButtonClick = function (e, data) {
                        var message = "User clicked '" + data.name + "' button for item '" + data.context.title + "'.";
                        alert(message);
                    };

                    options.items = items;
                    options.cursorItem = 0;
                    options.templates = [getContactTemplate()];
                    options.onItemRender = onTemplateRender;
                    options.pageFitMode = primitives.common.PageFitMode.None;
                    options.hasSelectorCheckbox = primitives.common.Enabled.False;
                    options.onMouseClick = function (e, data) {
                        var id = data.context.id;
                        var myHidden = document.getElementById('<%= hdfid_involucrado.ClientID %>');
                     
                        myHidden.value = id;
                        $("#<%= div_nievoinvo.ClientID%>").show();
                        $("#<%= div_listempleados.ClientID%>").hide();
                        document.getElementById('<%= btneditarinvol.ClientID%>').click();
                    }
                    jQuery("#basicdiagram_employeed").orgDiagram(options);

                    function onTemplateRender(event, data) {
                        var hrefElement = data.element.find("[name=readmore]");
                        var emailElement = data.element.find("[name=email]");
                        switch (data.renderingMode) {
                            case primitives.common.RenderingMode.Create:
                                /* Initialize widgets here */
                                hrefElement.click(function (e) {
                                    /* Block mouse click propogation in order to avoid layout updates before server postback*/
                                    primitives.common.stopPropagation(e);
                                });
                                emailElement.click(function (e) {
                                    /* Block mouse click propogation in order to avoid layout updates before server postback*/
                                    primitives.common.stopPropagation(e);
                                });
                                break;
                            case primitives.common.RenderingMode.Update:
                                /* Update widgets here */
                                break;
                        }

                        var itemConfig = data.context;

                        if (data.templateName == "contactTemplate") {
                            data.element.find("[name=titleBackground]").css({ "background": itemConfig.itemTitleColor });

                            data.element.find("[name=photo]").attr({ "src": itemConfig.image });
                            hrefElement.attr({ "href": itemConfig.href });
                            emailElement.attr({ "href": ("mailto:" + itemConfig.email + "") });

                            var fields = ["title", "description", "phone", "email"];
                            for (var index = 0; index < fields.length; index++) {
                                var field = fields[index];

                                var element = data.element.find("[name=" + field + "]");
                                if (element.text() != itemConfig[field]) {
                                    element.text(itemConfig[field]);
                                }
                            }
                        }
                    }

                    function getContactTemplate() {
                        var result = new primitives.orgdiagram.TemplateConfig();
                        result.name = "contactTemplate";

                        result.itemSize = new primitives.common.Size(220, 90);
                        result.minimizedItemSize = new primitives.common.Size(3, 3);
                        result.highlightPadding = new primitives.common.Thickness(2, 2, 2, 2);

                        var itemTemplate = jQuery(
                          '<div class="bp-item bp-corner-all bt-item-frame">'
                            + '<div name="titleBackground" class="bp-item bp-corner-all bp-title-frame" style="top: 2px; left: 2px; width: 216px; height: 20px;">'
                                + '<div name="title" class="bp-item bp-title" style="top: 3px; left: 6px; width: 208px; height: 18px;">'
                                + '</div>'
                            + '</div>'
                            + '<div name="phone" class="bp-item" style="top: 62px; left: 9px; width: 162px; height: 36px; font-size: 10px;"></div>'
                            + '<div class="bp-item" style="top: 44px; left: 9px; width: 185px; height: 18px; font-size: 12px;"><a name="email"></a></div>'
                            + '<div name="description" class="bp-item" style="top: 26px; left: 9px; width: 162px; height: 18px; font-size: 12px;"></div>'

                        + '</div>'
                        ).css({
                            width: result.itemSize.width + "px",
                            height: result.itemSize.height + "px"
                        }).addClass("bp-item bp-corner-all bt-item-frame");

                        result.itemTemplate = itemTemplate.wrap('<div>').parent().html();

                        return result;
                    }

                }

            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h4 class="page-header">
                <asp:Label ID="lblproyect" runat="server" Text="Mi Proyecto"></asp:Label></h4>
        </div>
        <div class="col-lg-12">

            <div class="nav-tabs-custom">
                <ul class="nav nav-tabs">
                    <li class="" id="tinfo" runat="server">
                        <asp:LinkButton ID="LinkButton5" CommandName="tinfo" runat="server" OnClick="tinfo_Click"><strong>Información General</strong></asp:LinkButton>
                    </li>
                    <li class="" id="tinvo" runat="server">
                        <asp:LinkButton ID="LinkButton6" CommandName="tinvo" runat="server" OnClick="tinfo_Click"><strong>Involucrados</strong></asp:LinkButton>
                    </li>
                    <li class="" id="tmin" runat="server">
                        <asp:LinkButton ID="LinkButton7" CommandName="tmin" runat="server" OnClick="tinfo_Click"><strong>Minutas</strong></asp:LinkButton>
                    </li>
                    <li class="" id="tdoc" runat="server">
                        <asp:LinkButton ID="LinkButton8" CommandName="tdoc" runat="server" OnClick="tinfo_Click"><strong>Documentos</strong></asp:LinkButton>
                    </li>
                    <li class="" id="tcomun" runat="server">
                        <asp:LinkButton ID="LinkButton9" CommandName="tcomun" runat="server" OnClick="tinfo_Click"><strong>Comunicación</strong></asp:LinkButton>
                    </li>
                    <li class="" id="tadmin" runat="server">
                        <asp:LinkButton ID="LinkButton10" CommandName="tadmin" runat="server" OnClick="tinfo_Click"><strong>Administración</strong></asp:LinkButton>
                    </li>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane" id="tab_info" runat="server">
                        <div class="row">
                            <div class="col-lg-6 col-xs-12">
                                <h5><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Inicio</strong></h5>
                                <asp:Label ID="lblfecha_inicio" runat="server" Text="Label"></asp:Label>
                            </div>
                            <div class="col-lg-6 col-xs-12">

                                <h5><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Fin</strong></h5>
                                <asp:Label ID="lblfecha_fin" runat="server" Text="Label"></asp:Label>
                            </div>
                            <div class="col-lg-12">

                                <h5><strong><i class="fa fa-wrench" aria-hidden="true"></i>&nbsp;Avance</strong>&nbsp;<asp:Label ID="lblavancee" runat="server" Text="Label"></asp:Label>&nbsp;%</h5>

                                <div class="progress">
                                    <div class="progress-bar progress-bar-red" role="progressbar" aria-valuemin="0" aria-valuemax="100" style="width: 100%" id="barprog" runat="server">
                                        <span>
                                            <asp:Label ID="lblavance2" runat="server" Text="0"></asp:Label>% Completado</span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <h5><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Resumen del Proyecto</strong></h5>
                                <p>
                                    <asp:Label ID="lblresumen" runat="server" Text="Label"></asp:Label>
                                </p>
                            </div>
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <h5><strong><i class="fa fa-info-circle" aria-hidden="true"></i>&nbsp;Proyect Charter</strong></h5>
                                <div style="text-align: left;">
                                    <asp:LinkButton ID="lnkobjetivos" OnClick="lnkobjetivos_Click"  OnClientClick="LoadPage();" CommandArgument="objetivos" runat="server" CssClass="btn btn-default btn-flat">
                                        <i class="fa fa-bar-chart" aria-hidden="true"></i>&nbsp;Objetivos del Proyecto
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lnkdescsolucion" OnClick="lnkobjetivos_Click" OnClientClick="LoadPage();"  CommandArgument="solucion" runat="server" CssClass="btn btn-default btn-flat">
                                        <i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Descripción de la solución
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lnksupuestos" OnClick="lnkobjetivos_Click" OnClientClick="LoadPage();"  CommandArgument="supuestos" runat="server" CssClass="btn btn-default btn-flat">
                                        <i class="fa fa-lightbulb-o" aria-hidden="true"></i>&nbsp;Supuestos
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lnkfueraalcance" OnClick="lnkobjetivos_Click" OnClientClick="LoadPage();"  CommandArgument="alcance" runat="server" CssClass="btn btn-default btn-flat">
                                        <i class="fa fa-level-down" aria-hidden="true"></i>&nbsp;Fuera de Alcance
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lnkriesgos" OnClick="lnkobjetivos_Click" OnClientClick="LoadPage();"  CommandArgument="riesgos" runat="server" CssClass="btn btn-default btn-flat">
                                        <i class="fa fa-life-ring" aria-hidden="true"></i>&nbsp;Riesgos Alto Nivel
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="text-align: right;">

                                <asp:LinkButton ID="lnkterminacíon" OnClick="lnkterminacíon_Click" OnClientClick="LoadPage();" CssClass="btn btn-danger btn-flat btng"
                                    runat="server">
                                    <i class="fa fa-handshake-o" aria-hidden="true"></i>&nbsp;
                                    Terminación</asp:LinkButton>
                                <asp:LinkButton ID="lnkseleccionarcliente" OnClientClick="LoadPage();" CssClass="btn btn-success btn-flat" runat="server" OnClick="lnkseleccionarcliente_Click">
                                         <i class="fa fa-user" aria-hidden="true"></i>&nbsp;Info. Cliente
                                </asp:LinkButton>
                                <asp:LinkButton ID="lnkactualiza_excel" OnClientClick="return ModalShow('#myModalExcel');" CssClass="btn btn-primary btn-flat btng" runat="server" OnClick="lnkeditar_Click">
                                    <i class="fa fa-pencil" aria-hidden="true"></i>&nbsp;
                                    Editar</asp:LinkButton>
                                <asp:LinkButton ID="lnkeditar" Visible="false" OnClientClick="return ModalShow('#myModal');" CssClass="btn btn-info btn-flat btng" runat="server">
                                    <i class="fa fa-pencil" aria-hidden="true"></i>&nbsp;
                                    Editar Información</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane" id="tab_invo" runat="server">
                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-12">
                                <h5><strong><i class="fa fa-users" aria-hidden="true"></i>&nbsp;Diagrama de Involucrados</strong>
                                </h5>
                                <div style="text-align:right;">
                                     <asp:LinkButton ID="lnkagregarinvolucrado" CssClass="btn btn-primary btn-flat btn-sm" runat="server" OnClientClick="LoadPage();" OnClick="lnkagregarinvolucrado_Click">
                                          Agregar Involucrado del Cliente&nbsp;<i class="fa fa-plus" aria-hidden="true"></i></asp:LinkButton>
                                </div>
                                <div id="basicdiagram" style="border-style: solid; border-width: 1px; width: 100%; height: 450px">
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-12">
                                <h5><strong><i class="fa fa-users" aria-hidden="true"></i>&nbsp;Diagrama de Empleados</strong>
                                 
                                </h5>
                                 <div style="text-align:right;">
                                    <asp:LinkButton ID="lnkagregarempleado" CssClass="btn btn-danger btn-flat btn-sm"  OnClientClick="LoadPage();" runat="server" OnClick="lnkagregarempleado_Click">
                                          Agregar Empleado&nbsp;<i class="fa fa-plus" aria-hidden="true"></i></asp:LinkButton>
                                </div>
                                <div id="basicdiagram_employeed" style="border-style: solid; border-width: 1px; width: 100%; height: 450px">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane" id="tab_minu" runat="server">

                        <div class="row">
                            <div class="col-lg-12">
                                <h5><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Minutas</strong>
                                </h5>
                                 <asp:LinkButton CssClass="btn btn-danger btn-sm btn-flat" ID="lnknuevaminuta" OnClick="lnknuevaminuta_Click" runat="server">
                                            Nueva Minuta&nbsp;<i class="fa fa-plus" aria-hidden="true"></i></asp:LinkButton>
                            </div>
                            <div class="col-lg-12">

                                <div class="table table-responsive">
                                    <table class="table table-responsive table-bordered table-condensed">
                                        <thead>
                                            <tr>
                                                <th style="width: 40px"></th>
                                                <th style="width: 40px"></th>
                                                <th style="width: 40px"></th>
                                                <th>Asunto</th>
                                                <th>Fecha</th>
                                                <th>Lugar</th>
                                                <th style="width: 40px;">Estatus</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="repeater_minutas" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="text-align: center">
                                                            <asp:LinkButton ID="lnkcerrarminuta" runat="server" OnClientClick="return confirm('¿Desea Enviar Esta Minuta?');" OnClick="lnkeditminuta_Click" CommandName="Terminar"
                                                                CssClass="btn btn-success btn-flat" Visible='<%# Convert.ToBoolean(Eval("visible")) %>'
                                                                CommandArgument='<%# Eval("id_minuta") %>'>
                                                                     <i class="fa fa-share" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="lnkeditminuta" runat="server" OnClick="lnkeditminuta_Click" CommandName="Editar" CssClass="btn btn-primary btn-flat"
                                                                CommandArgument='<%# Eval("id_minuta") %>' Visible='<%# Convert.ToBoolean(Eval("visible")) %>'>
                                                                     <i class="fa fa-pencil" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="lnkdeleteminuta" runat="server" OnClick="lnkeditminuta_Click" CommandName="Delete" CssClass="btn btn-danger btn-flat"
                                                                CommandArgument='<%# Eval("id_minuta") %>' Visible='<%# Convert.ToBoolean(Eval("visible")) %>' OnClientClick="return ConfirmEntregableDelete('¿Desea Eliminar la Minuta?');">
                                                                     <i class="fa fa-trash" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                        </td>
                                                        <td><%# Eval("asunto").ToString().ToUpper() %></td>
                                                        <td><%# Convert.ToDateTime(Eval("fecha")).ToString("dddd dd MMMM, yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("es-MX")) %></td>
                                                        <td><%# Eval("lugar") %></td>
                                                        <td><%# Eval("Estatus") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane" id="tab_docs" runat="server">

                        <div class="row">
                            <div class="col-lg-12">
                                <h5><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Documentos Anexos al Proyecto</strong>
                                </h5> 
                                <asp:LinkButton CssClass="btn btn-danger btn-sm btn-flat" ID="lnknuevodoc" OnClientClick="return ModalShow('#myModalDocumentos');" runat="server">
                                            Nuevo Documento&nbsp;<i class="fa fa-plus" aria-hidden="true"></i></asp:LinkButton>
                                <div class="table table-responsive">
                                    <table class="table table table-responsive table-bordered table-condensed">
                                        <thead>
                                            <tr>
                                                <th style="width: 40px"></th>
                                                <th style="width: 40px"></th>
                                                <th>Documento</th>
                                                <th>Tipo de Archivo</th>
                                                <th>Tamaño</th>
                                                <th>Fecha de Subida</th>
                                                <th>Usuario</th>
                                            </tr>
                                        </thead>
                                        <tbody>

                                            <asp:Repeater ID="repeater_documentos" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:LinkButton ID="lnkDownload" runat="server" OnClick="DownloadFile" CommandName="Download" CssClass="btn btn-info btn-flat"
                                                                CommandArgument='<%# Eval("id_documento") %>'>
                                                                     <i class="fa fa-download" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="LinkButton4" runat="server" OnClick="DownloadFile" CommandName="Delete" CssClass="btn btn-danger btn-flat"
                                                                CommandArgument='<%# Eval("id_documento") %>' OnClientClick="return ConfirmEntregableDelete('¿Desea Eliminar el Documento?');">
                                                                     <i class="fa fa-trash" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                        </td>
                                                        <td><%# Eval("nombre").ToString().ToUpper() %></td>
                                                        <td><%# Eval("extension").ToString().ToUpper() %></td>
                                                        <td><%# (Convert.ToDecimal(Eval("tamaño"))/1000000).ToString("#,###.##") %>&nbsp;mb</td>
                                                        <td><%# Convert.ToDateTime(Eval("fecha_registro")).ToString("dddd dd MMMM, yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("es-MX")) %></td>
                                                        <td><%# Eval("usuario") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane" id="tab_com" runat="server">
                        <div class="row">
                            <div class="col-lg-12">
                                <h5><strong><i class="fa fa-envelope-o"></i>&nbsp;Historial de Correos&nbsp;</strong>
                                </h5>
                                 <asp:LinkButton CssClass="btn btn-danger btn-flat btn-sm " ID="lnknuevocorreo" OnClick="lnknuevocorreo_Click" runat="server">
                                            Nuevo Correo&nbsp;<i class="fa fa-plus" aria-hidden="true"></i></asp:LinkButton>
                            </div>
                            <div class="col-lg-12">
                                <div class="table table-responsive">
                                    <telerik:RadGrid ID="rgri_correos" runat="server" Skin="Metro">
                                        <MasterTableView AutoGenerateColumns="false" CssClass="dvv table table-responsive"
                                            HeaderStyle-BackColor="White" HeaderStyle-ForeColor="Black"
                                            Width="100%">
                                            <Columns>

                                                <telerik:GridTemplateColumn HeaderText="Usuario Envio">
                                                    <HeaderStyle Width="200px" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton2" OnClick="LinkButton21_Click" runat="server" CommandName="View" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id_pcorreo").ToString() %>'>
                                               <%# Eval("usuario") %>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Asunto">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton21" OnClick="LinkButton21_Click" runat="server" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id_pcorreo").ToString() %>'>
                                                 <%# Eval("subject") %>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>

                                                <telerik:GridBoundColumn DataField="fecha_envio" HeaderText="Fecha" UniqueName="fecha"
                                                    Visible="true">
                                                    <HeaderStyle Width="200px" />
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane" id="tab_admin" runat="server">
                        <div class="row">
                            <div class="col-lg-12">
                                <h5><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Usuarios Relacionados al Proyectos</strong>

                                </h5>
                                <asp:LinkButton ID="lnkagregarempleadoaproyecto" CssClass="btn btn-danger btn-flat btn-sm" runat="server" 
                                        OnClick="lnkagregarempleadoaproyecto_Click" OnClientClick="LoadPage();">
                                        Agregar PM&nbsp;<i class="fa fa-plus" aria-hidden="true"></i>
                                    </asp:LinkButton>
                                <div class="table table-responsive">
                                    <table class="table table table-responsive table-bordered table-condensed">
                                        <thead>
                                            <tr>
                                                <th style="width: 40px"></th>
                                                <th>Usuario</th>
                                                <th>Empleado</th>
                                                <th style="width: 40px">Creador</th>
                                            </tr>
                                        </thead>
                                        <tbody>

                                            <asp:Repeater ID="repeat_proyectos_empleados" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:LinkButton ID="lnkeliminarempleadoproyecto" OnClick="lnkeliminarempleadoproyecto_Click" runat="server" CommandName="Delete" CssClass="btn btn-danger btn-flat"
                                                                CommandArgument='<%# Eval("id_pempleado") %>' Visible='<%# !Convert.ToBoolean(Eval("creador")) %>' OnClientClick="return ConfirmEntregableDelete('¿Desea Eliminar al PM?');">
                                                                     <i class="fa fa-trash" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                        </td>
                                                        <td><%# Eval("usuario").ToString().ToUpper() %></td>
                                                        <td><%# Eval("nombre").ToString().ToUpper() %></td>
                                                        <td>
                                                            <asp:CheckBox ID="CheckBox1" Checked='<%# Convert.ToBoolean(Eval("creador")) %>' CssClass=" form-control" Enabled="false" runat="server" /></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-6 col-md-6 col-sm-12" style="display: none;">
            <div class="box box-danger">
                <div class="box-header with-border">
                    <h3 class="box-title">Tareas</h3>

                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                            <i class="fa fa-minus"></i>
                        </button>
                        <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                    </div>
                </div>
                <div class="box-body">
                    <div class="table-responsive">
                        <telerik:RadGrid runat="server" ID="grid_tareas" Skin="Metro">
                            <MasterTableView AutoGenerateColumns="false" CssClass="dvv table table-responsive"
                                HeaderStyle-BackColor="White" HeaderStyle-ForeColor="Black"
                                Width="100%">
                                <Columns>
                                    <telerik:GridTemplateColumn>
                                        <HeaderStyle Width="30px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Editar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id_tarea").ToString() %>'>
                                                <i class="fa fa-pencil fa-2x" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn>
                                        <HeaderStyle Width="30px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton2" OnClientClick="return confirm('¿Desea Eliminar esta Tarea?');" runat="server" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id_tarea").ToString() %>'>
                                                <i class="fa fa-trash fa-2x" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="tarea_corta" HeaderText="Tareas" UniqueName="tarea_corta"
                                        Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="avance" HeaderText="% Avance" UniqueName="avance"
                                        Visible="true">
                                        <HeaderStyle Width="70px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="recursos" HeaderText="Recursos" UniqueName="recursos"
                                        Visible="true">
                                        <HeaderStyle Width="130px" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-6 col-md-6 col-sm-12">
            <div class="box box-danger">
                <div class="box-header with-border">
                    <h3 class="box-title">Mapa de Tareas
                    </h3>
                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                            <i class="fa fa-minus"></i>
                        </button>
                    </div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-12">

                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always">
                                <ContentTemplate>

                                    <div style="text-align: right; display: none;">
                                        <label>Buscar</label>
                                        <telerik:RadTextBox ID="rtxtxsearchtarea" AutoPostBack="true" Width="200px" runat="server" OnTextChanged="rtxtxsearchtarea_TextChanged" Skin="Bootstrap"></telerik:RadTextBox>
                                    </div>
                                    <telerik:RadTreeView RenderMode="Lightweight" ID="rtrvProyectWorks" runat="server" Width="100%" OnClientNodeClicked="ClientNodeClicked" Style="background-color: white;" Skin="Bootstrap" OnNodeClick="rtrvProyectWorks_NodeClick">
                                        <DataBindings>
                                            <telerik:RadTreeNodeBinding Expanded="False"></telerik:RadTreeNodeBinding>
                                        </DataBindings>
                                    </telerik:RadTreeView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-lg-6 col-md-6 col-sm-12">

            <div class="box box-danger">
                <div class="box-header with-border">
                    <h3 class="box-title">Documentos</h3>

                    <div class="box-tools">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                            <i class="fa fa-minus"></i>
                        </button>
                    </div>
                </div>
                <div class="box-body">

                    <div style="padding: 10px;">
                        <ul class="nav nav-pills nav-stacked">
                            <asp:Repeater ID="repeater_docs" runat="server">
                                <ItemTemplate>
                                    <li class="active" style="background-color: #e0e0e0">
                                        <asp:LinkButton ID="LinkButton3" runat="server" OnClick="DownloadFile"
                                            CommandArgument='<%# Eval("id_documento") %>'>
                                                                        <i class='<%# Eval("icono").ToString() %>'></i>
                                                                    <%# Eval("nombre").ToString().ToUpperInvariant() %><%# Eval("extension").ToString() %>
                                        </asp:LinkButton>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-12">
            <div class="box box-danger">
                <div class="box-header with-border">
                    <h3 class="box-title">Milestones
                    </h3>
                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                            <i class="fa fa-minus"></i>
                        </button>
                    </div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-12" id="div_chartentregables" runat="server">
                            <div id="container" style="height: 365px; margin: 0 auto"></div>
                            <asp:GridView ID="grid_entregables_hide" runat="server" AutoGenerateColumns="false" Style="display: none;">
                                <Columns>

                                    <asp:BoundField DataField="entregable_name" HeaderText="entregable"></asp:BoundField>
                                    <asp:BoundField DataField="avance" HeaderText="avance"></asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="box-footer clearfix">
                    <asp:LinkButton ID="lnknuevo_entregable" runat="server" OnClick="lnknuevo_entregable_Click" CssClass="btn btn-primary btn-flat  pull-right btn-sm">
                            <i class="fa fa-plus" aria-hidden="true"></i>&nbsp;Nuevo Milestone
                    </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade bs-example-modal-lg" tabindex="-1" id="myModal" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnkeditar" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Información del Proyecto</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <h5><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Proyecto</strong></h5>
                                    <telerik:RadTextBox ID="rtxtproyecto" Width="100%" runat="server" Skin="Bootstrap" placeholder="Proyecto"></telerik:RadTextBox>
                                </div>
                                <div class="col-lg-12">
                                    <h5><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Descripción del Proyecto</strong></h5>
                                    <telerik:RadTextBox ID="rtxtdescripcion" Width="100%" runat="server" Skin="Bootstrap" placeholder="Descripción" TextMode="MultiLine" Rows="3"></telerik:RadTextBox>
                                </div>
                                <div class="col-lg-6 col-sm-6 col-xs-12">
                                    <h5><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Inicio</strong></h5>
                                    <telerik:RadDatePicker ID="rdtpinicio" runat="server" Width="100%" Skin="Bootstrap"></telerik:RadDatePicker>
                                </div>
                                <div class="col-lg-6 col-sm-6 col-xs-12">
                                    <h5><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Fin</strong></h5>
                                    <telerik:RadDatePicker ID="rdtpfin" runat="server" Width="100%" Skin="Bootstrap"></telerik:RadDatePicker>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6 col-sm-6 col-xs-12">
                                    <h5><strong><i class="fa fa-wrench" aria-hidden="true"></i>&nbsp;Avance</strong></h5>
                                    
                                    <asp:TextBox ID="rtxtavance" CssClass=" form-control"  runat="server"  type="number" onkeypress="return validarEnteros(event);"
                                         onpaste="return false;"></asp:TextBox>
                                   <%-- <telerik:RadTextBox ID="rtxtavance" runat="server" Skin="Bootstrap" InputType="Number">
                                        <ClientEvents OnKeyPress="keyPressInteger" />
                                    </telerik:RadTextBox>--%>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer ">
                            <div class="row" id="div_errormodal" runat="server" visible="false">

                                <div class="col-lg-12">
                                    <div class="alert alert-danger alert-dismissible">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <asp:Label ID="lblerrormodal" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                            <asp:LinkButton ID="lnkeditar_cambios" OnClientClick="return OnClickConfirm();" CssClass="btn btn-primary btn-flat" runat="server" OnClick="lnkeditar_cambios_Click">
                            <i class="fa fa-floppy-o" aria-hidden="true"></i>&nbsp;Guardar
                            </asp:LinkButton>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="modal fade bs-example-modal-lg" tabindex="-1" id="myModalExcel" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnkactualiza_excel" EventName="Click" />
                    <asp:PostBackTrigger ControlID="lnksubirexcel" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Modificación del Proyecto</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <p>Puede Actualizar el proyecto mediante un archivo de CSV.</p>
                                    <ul>
                                        <li>La Primer Fila, debe contener el nombre del Proyecto, por lo que las actividades seran tomadas en cuenta, a partir de la segunda fila</li>
                                        <li>Se le recomienda utilizar el asistente de exportación de Microsoft Project para generar el archivo de CSV</li>
                                        <li>Si tiene dudas, contacte al administrador</li>
                                    </ul>
                                    <asp:FileUpload ID="fuparchivos" runat="server"  onchange="return ValidateUF(this,5);" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer ">

                            <div class="row" id="div_errormodal2" runat="server" visible="false" style="text-align: left;">
                                <div class="col-lg-12">
                                    <div class="alert alert-danger alert-dismissible">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <asp:Label ID="lblerrormodal2" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                            <asp:LinkButton OnClientClick="return false;" ID="lnkloadingexcel" CssClass="btn btn-primary btn-flat" runat="server" Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Cargando Archivo...
                            </asp:LinkButton>

                            <asp:LinkButton ID="lnksubirexcel" OnClientClick="return ConfirmExcelModal('¿Desea actualizar el proyecto?');" CssClass="btn btn-primary btn-flat" runat="server"
                                OnClick="lnksubirexcel_Click">
                                            <i class="fa fa-upload" aria-hidden="true"></i>&nbsp;Subir Archivo
                            </asp:LinkButton>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="modal fade bs-example-modal-lg" tabindex="-1" id="myModalEntregablle" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnknuevo_entregable" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btneditarentregablegraph" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Milestones</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <h5><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Nombre del Milestones</strong></h5>
                                    <telerik:RadTextBox ID="rtxtentregable" Width="100%" runat="server" Skin="Bootstrap"></telerik:RadTextBox>
                                </div>
                                <div class="col-lg-12">
                                    <h5><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha</strong></h5>
                                    <telerik:RadDatePicker ID="rdtfechaentregable" runat="server" Width="100%" Skin="Bootstrap"></telerik:RadDatePicker>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-wrench" aria-hidden="true"></i>&nbsp;% Avance</strong></h5>
                                    
                                    <asp:TextBox ID="rtxtavanceentregable" CssClass=" form-control"  runat="server"  type="number" onkeypress="return validarEnteros(event);"
                                         onpaste="return false;"></asp:TextBox>
                                <%--    <telerik:RadTextBox ID="rtxtavanceentregable" runat="server" Skin="Bootstrap" InputType="Number">
                                        <ClientEvents OnKeyPress="keyPressInteger" />
                                    </telerik:RadTextBox>--%>
                                </div>
                                <asp:TextBox ID="txtid_entregable" Visible="false" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="modal-footer ">
                            <div class="row" id="div_comentarios_eliminacionentregable" runat="server" style="display: none;">
                                <label>Comentarios de Borrado</label>
                                <telerik:RadTextBox ID="rtxtxcomentariosentregablesborrado" TextMode="MultiLine" Rows="2" runat="server" Skin="Bootstrap"></telerik:RadTextBox>
                            </div>
                            <div class="row" id="div_error_entregabe" runat="server" visible="false" style="text-align: left;">
                                <div class="col-lg-12">
                                    <div class="alert alert-danger alert-dismissible">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <asp:Label ID="lblerrorentregable" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>

                            <asp:LinkButton ID="lnkeliminarentregable" OnClientClick="return ConfirmEntregableDelete('¿Desea Eliminar el Entregable?');"
                                CssClass="btn btn-danger btn-flat" runat="server" OnClick="lnkeliminarentregable_Click">
                                            <i class="fa fa-trash" aria-hidden="true"></i>&nbsp;Eliminar
                            </asp:LinkButton>
                            <asp:LinkButton OnClientClick="return false;" ID="lnkcargandomilestone" CssClass="btn btn-primary btn-flat" runat="server" Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Guardando Milestone...
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkGuardarEntregable" OnClick="lnkGuardarEntregable_Click" OnClientClick="return ConfirmMilestoneModal('¿Desea Guardar el Entregable?');" CssClass="btn btn-primary btn-flat" runat="server">
                                            <i class="fa fa-floppy-o" aria-hidden="true"></i>&nbsp;Guardar
                            </asp:LinkButton>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="modal fade bs-example-modal-lg" tabindex="-1" id="myModalTareas" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="rtrvProyectWorks" EventName="NodeClick" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Tareas</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <h5><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Nombre de la Tarea</strong></h5>
                                    <telerik:RadTextBox ID="rtxttarea" Width="100%" runat="server" Skin="Bootstrap" placeholder="Tarea"></telerik:RadTextBox>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                    <h5><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Inicio</strong></h5>
                                    <telerik:RadDatePicker ID="rdfechainiciotarea" runat="server" Width="100%" Skin="Bootstrap"></telerik:RadDatePicker>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                    <h5><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Fin</strong></h5>
                                    <telerik:RadDatePicker ID="rdfechafintarea" runat="server" Width="100%" Skin="Bootstrap"></telerik:RadDatePicker>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                    <h5><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Duración</strong></h5>
                                    <telerik:RadTextBox ID="rtxtduracion" Width="100%" runat="server" Skin="Bootstrap">
                                    </telerik:RadTextBox>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                    <h5><strong><i class="fa fa-wrench" aria-hidden="true"></i>&nbsp;% Avance</strong></h5>
                                    
                                    <asp:TextBox ID="rtxtxavancetarea" CssClass=" form-control"  runat="server"  type="number" onkeypress="return validarEnteros(event);"
                                         onpaste="return false;"></asp:TextBox>
                                 <%--   <telerik:RadTextBox ID="rtxtxavancetarea" InputType="Number" runat="server" Skin="Bootstrap">
                                        <ClientEvents OnKeyPress="keyPressInteger" />
                                    </telerik:RadTextBox>--%>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <h5><strong><i class="fa fa-wrench" aria-hidden="true"></i>&nbsp;Recursos</strong></h5>
                                    <telerik:RadTextBox ID="rtxtrecursos" runat="server" Skin="Bootstrap" TextMode="MultiLine" Rows="2" Width="100%">
                                    </telerik:RadTextBox>
                                </div>
                                <div class="col-lg-12" id="tareas_hijas" runat="server" visible="false">
                                    <h5><strong><i class="fa fa-wrench" aria-hidden="true"></i>&nbsp;Tareas</strong></h5>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:TextBox ID="txtid_tarea" Visible="false" runat="server"></asp:TextBox>
                                    <asp:TextBox ID="txtcodigo_tarea" Visible="false" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer ">

                            <div class="row" id="div_error_tareas" runat="server" visible="false" style="text-align: left;">
                                <div class="col-lg-12">
                                    <div class="alert alert-danger alert-dismissible">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <asp:Label ID="lblerror_tareas" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                            <asp:LinkButton ID="lnkeliminartarea_modal" OnClientClick="return ConfirmEntregableDelete('¿Desea Eliminar la Tarea?');" CssClass="btn btn-danger btn-flat" runat="server">
                                            <i class="fa fa-trash" aria-hidden="true"></i>&nbsp;Eliminar
                            </asp:LinkButton>
                            <asp:LinkButton OnClientClick="return false;" ID="lnkcargandoactividad" CssClass="btn btn-primary btn-flat" runat="server" Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Guardando Tarea...
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkguardartarea_modal" OnClientClick="return ConfirmTareaModal('¿Desea Guardar la Tarea?');" CssClass="btn btn-primary btn-flat" runat="server" OnClick="lnkguardartarea_modal_Click">
                                            <i class="fa fa-floppy-o" aria-hidden="true"></i>&nbsp;Guardar
                            </asp:LinkButton>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="modal fade bs-example-modal-lg" tabindex="-1" id="myModalCorreos" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnknuevocorreo" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="lnkenviarcorreo" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Correos</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">

                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <label><strong><i class="fa fa-address-book-o" aria-hidden="true"></i>&nbsp;Destinatario(s):</strong></label>
                                        <br />
                                        <asp:TextBox ID="txtto" CssClass="form-control" placeholder="Para:" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label><strong><i class="fa fa-address-card-o" aria-hidden="true"></i>&nbsp;Asunto:</strong></label>
                                        <br />
                                        <asp:TextBox ID="txtsubject" CssClass="form-control" placeholder="Asunto:" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Mensaje:</strong></label>
                                        <br />
                                        <asp:TextBox ID="txtbody" CssClass="form-control" Height="150px" TextMode="MultiLine" runat="server"></asp:TextBox>
                                        <div style="background-color: #eee; padding: 6px 12px; border: 1px solid #ccc;" id="div_body" runat="server" visible="false">
                                            <asp:PlaceHolder ID="plhbody" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer ">

                            <div class="row" id="div_error_correos" runat="server" visible="false" style="text-align: left;">
                                <div class="col-lg-12">
                                    <div class="alert alert-danger alert-dismissible">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <asp:Label ID="lblerrorcorreos" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                            <asp:LinkButton ID="lnkenviandocorreo" CssClass="btn btn-primary btn-flat" runat="server" OnClientClick="return false;" Style="display: none;">
                                        <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Enviando Correo...</asp:LinkButton>

                            <asp:LinkButton ID="lnkenviarcorreo" CssClass="btn btn-primary btn-flat" runat="server"
                                OnClick="lnkenviarcorreo_Click" OnClientClick="return ConfirmCorreoModal('¿Desea Enviar este Correo?');">
                                        <i class="fa fa-share" aria-hidden="true"></i>&nbsp;Enviar Correo</asp:LinkButton>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="modal fade bs-example-modal-lg" tabindex="-1" id="myModalDocumentos" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnknuevodoc" EventName="Click" />
                    <asp:PostBackTrigger ControlID="lnkupload" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Documentos</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <h5>Puede Subir Documentos como un repositorios para todo el proyecto. También puede indicar si el documento SERA VISIBLE PARA EL CLIENTE.</h5>
                                </div>
                                <div class="col-lg-12">
                                    <h5><strong><i class="fa fa-file-archive-o" aria-hidden="true"></i>&nbsp;Seleccionar Documento</strong></h5>
                                    <asp:FileUpload ID="fupDocumentos" runat="server" CssClass="form-control"  onchange="return ValidateUF(this,15);" />
                                </div>
                                <div class="col-lg-12">
                                    <br />
                                    <asp:CheckBox ID="cbxpublico" Text="Documento Visible para el Cliente" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer ">

                            <div class="row" id="div_error_Documentos" runat="server" visible="false" style="text-align: left;">
                                <div class="col-lg-12">
                                    <div class="alert alert-danger alert-dismissible">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <asp:Label ID="lblerror_documento" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                            <asp:LinkButton ID="lnkcargandodocumento" CssClass="btn btn-primary btn-flat" runat="server" OnClientClick="return false;" Style="display: none;">
                                        <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Cargando Documento...</asp:LinkButton>
                            <asp:LinkButton ID="lnkupload" runat="server" CssClass="btn btn-primary btn-flat" OnClick="btnUpload_Click" OnClientClick="return ConfirmDocModal('¿Desea Agregar Este Documento?');">
                                    <i class="fa fa-upload" aria-hidden="true"></i>&nbsp;Subir Documento
                            </asp:LinkButton>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="modal fade bs-example-modal-lg" tabindex="-1" id="myModalMinutas" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnknuevaminuta" EventName="Click" />
                    <asp:PostBackTrigger ControlID="lnkupload" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Minutas</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <h5><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Asunto</strong></h5>
                                    <telerik:RadTextBox ID="rtxtasuntominuta" Width="100%" runat="server" Skin="Bootstrap"></telerik:RadTextBox>
                                </div>

                                <div class="col-lg-6 col-md-6 col-sm-12">
                                    <h5><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha</strong></h5>
                                    <telerik:RadDatePicker ID="rdpfechaminuta" Width="100%" Skin="Bootstrap" runat="server"></telerik:RadDatePicker>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12">
                                    <h5><strong><i class="fa fa-map-marker" aria-hidden="true"></i>&nbsp;Lugar</strong></h5>
                                    <telerik:RadTextBox ID="rtxtlugarminuta" Width="100%" runat="server" Skin="Bootstrap"></telerik:RadTextBox>
                                </div>
                                <div class="col-lg-12">
                                    <h5><strong><i class="fa fa-wrench" aria-hidden="true"></i>&nbsp;Propósito</strong></h5>
                                    <telerik:RadTextBox ID="rtxtpropositos" Width="100%" runat="server" TextMode="MultiLine" Rows="3" Skin="Bootstrap"></telerik:RadTextBox>
                                </div>
                                <div class="col-lg-12">
                                    <h5><strong><i class="fa fa-line-chart" aria-hidden="true"></i>&nbsp;Resultados</strong></h5>
                                    <telerik:RadTextBox ID="rtxtresultados" Width="100%" runat="server" TextMode="MultiLine" Rows="2" Skin="Bootstrap"></telerik:RadTextBox>
                                </div>
                                <div class="col-lg-12">
                                    <h5><strong><i class="fa fa-hand-paper-o" aria-hidden="true"></i>&nbsp;Acuerdos</strong></h5>
                                    <telerik:RadTextBox ID="rtxtacuerdos" Width="100%" runat="server" TextMode="MultiLine" Rows="2" Skin="Bootstrap"></telerik:RadTextBox>
                                </div>
                                <div class="col-lg-12">
                                    <br />
                                    <div style="text-align: right;">
                                        <asp:LinkButton ID="lnkpendientes" runat="server" CssClass="btn btn-success btn-flat" OnClick="lnkpendientes_Click">
                                           <i class="fa fa-briefcase" aria-hidden="true"></i>&nbsp;Pendientes
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnkparticipantes" runat="server" CssClass="btn btn-danger btn-flat" OnClick="lnkparticipantes_Click">
                                           <i class="fa fa-users"></i>&nbsp;Participantes
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <asp:TextBox ID="txtid_minuta" runat="server" Visible="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="modal-footer ">

                            <div class="row" id="div_errorminuta" runat="server" visible="false" style="text-align: left;">
                                <div class="col-lg-12">
                                    <div class="alert alert-danger alert-dismissible">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <asp:Label ID="lblerrorminuta" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                            <asp:LinkButton ID="lnkcargandoMinuta" CssClass="btn btn-primary btn-flat" runat="server" OnClientClick="return false;" Style="display: none;">
                                        <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Guardando Minuta...</asp:LinkButton>
                            <asp:LinkButton ID="lnkguardarminuta" runat="server" CssClass="btn btn-primary btn-flat" OnClick="lnkguardarminuta_Click" OnClientClick="return ConfirmMinutaModal('¿Desea Agregar Esta Minuta?');">
                                    <i class="fa fa-floppy-o" aria-hidden="true"></i>&nbsp;Guardar
                            </asp:LinkButton>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="modal fade bs-example-modal-lg" tabindex="-1" id="myModalParticipantes" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnkparticipantes" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="lnknuevaminuta" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Participantes</h4>
                        </div>
                        <div class="modal-body" style="width: 100%; height: 450px; overflow: scroll;">
                            <div class="row">
                                <div id="div_addparticipante" runat="server" visible="false">

                                    <div class="col-lg-12">
                                        <h5><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Nombre</strong></h5>
                                        <telerik:RadTextBox ID="rtxtnombreparticipante" Width="100%" runat="server" Skin="Bootstrap"></telerik:RadTextBox>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                        <h5><strong><i class="fa fa-briefcase" aria-hidden="true"></i>&nbsp;Rol</strong></h5>
                                        <telerik:RadTextBox ID="rtxtrol" Width="100%" runat="server" Skin="Bootstrap"></telerik:RadTextBox>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                        <h5><strong><i class="fa fa-address-book" aria-hidden="true"></i>&nbsp;Organización</strong></h5>
                                        <telerik:RadTextBox ID="rtxtorganizacion" Width="100%" runat="server" Skin="Bootstrap"></telerik:RadTextBox>
                                    </div>
                                    <div style="text-align: right;">
                                    </div>
                                    <br />
                                </div>
                                <div class="col-lg-12" id="div_selectedinvo" runat="server">

                                    <h5><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Seleccione un Involucrado al Proyecto</strong>
                                        &nbsp;<asp:LinkButton ID="lnkagregar" OnClick="lnkagregar_Click" CssClass="btn btn-primary btn-flat btn-xs" runat="server">Agregar Persona Externa</asp:LinkButton>
                                    </h5>
                                    <div style="height: 150px; overflow: scroll;">
                                        <telerik:RadListBox RenderMode="Lightweight"   Style="font-size: 11px" runat="server" ID="rdlinvolucrados" Width="100%" SelectionMode="Multiple" Skin="Bootstrap">
                                        </telerik:RadListBox>
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <br />
                                    <div class="table table-responsive">

                                        <telerik:RadGrid ID="rgrid_participantes" runat="server" Skin="Metro">
                                            <MasterTableView AutoGenerateColumns="false" CssClass="table table-responsive"
                                                HeaderStyle-BackColor="White" HeaderStyle-ForeColor="Black"
                                                Width="100%">
                                                <Columns>

                                                    <telerik:GridTemplateColumn>
                                                        <HeaderStyle Width="40px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkeliminarparticipante" OnClientClick="return confirm('¿Desea Eliminar este Participante?');" OnClick="lnkeliminarparticipante_Click" runat="server" CommandName="View"
                                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "nombre").ToString() %>'>
                                                        <i class="fa fa-trash fa-2x" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="nombre" HeaderText="Participante" UniqueName="nombre"
                                                        Visible="true">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="rol" HeaderText="Rol" UniqueName="rol"
                                                        Visible="true">
                                                        <HeaderStyle Width="200px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="organizacion" HeaderText="Organización" UniqueName="organizacion"
                                                        Visible="true">
                                                        <HeaderStyle Width="200px" />
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer ">

                            <div class="row" id="div_participantes" runat="server" visible="false" style="text-align: left;">
                                <div class="col-lg-12">
                                    <div class="alert alert-danger alert-dismissible">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <asp:Label ID="lblparticipantes" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>

                            <asp:LinkButton ID="lnkaddparticipante" CssClass="btn btn-danger btn-flat" OnClick="lnkaddparticipante_Click" OnClientClick="return confirm('¿Desea Agregar Este Participante?');" runat="server">
                                            Agregar Participante&nbsp;<i class="fa fa-plus" aria-hidden="true"></i>
                            </asp:LinkButton>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="modal fade bs-example-modal-lg" tabindex="-1" id="myModalPendientes" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnkpendientes" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Pendientes</h4>
                        </div>
                        <div class="modal-body" style="width: 100%; height: 450px; overflow: scroll;">
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-12">
                                    <h5><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Responsable</strong></h5>
                                    <telerik:RadTextBox ID="rtxtresponsable" Width="100%" runat="server" Skin="Bootstrap"></telerik:RadTextBox>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12">
                                    <h5><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Planeada</strong></h5>
                                    <telerik:RadDatePicker ID="rdtfecha_planeada" Width="100%" Skin="Bootstrap" runat="server"></telerik:RadDatePicker>
                                </div>
                                <div class="col-lg-12">
                                    <h5><strong><i class="fa fa-briefcase" aria-hidden="true"></i>&nbsp;Pendiente</strong></h5>
                                    <telerik:RadTextBox ID="rtxtpendiente" Width="100%" runat="server" Skin="Bootstrap" TextMode="MultiLine" Rows="2"></telerik:RadTextBox>
                                </div>
                                <div class="col-lg-12">
                                    <h5><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Puede Seleccionar un Involucrado del Proyecto</strong></h5>

                                    <div style="height: 150px; overflow: scroll;">
                                        <telerik:RadListBox RenderMode="Lightweight"   Style="font-size: 11px" runat="server" ID="rdlinvopendientes" Width="100%" Skin="Bootstrap" SelectionMode="Multiple">
                                        </telerik:RadListBox>
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <br />
                                    <div class="table table-responsive">
                                        <telerik:RadGrid ID="grid_pendiente" runat="server" Skin="Metro">
                                            <MasterTableView AutoGenerateColumns="false" CssClass="table table-responsive"
                                                HeaderStyle-BackColor="White" HeaderStyle-ForeColor="Black"
                                                Width="100%">
                                                <Columns>
                                                    <telerik:GridTemplateColumn>
                                                        <HeaderStyle Width="40px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkeditarparticipante"
                                                                OnClick="lnkeditarparticipante_Click" runat="server" CommandName='<%# DataBinder.Eval(Container.DataItem, "descripcion").ToString() %>'
                                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "responsable").ToString() %>'>
                                                                <i class="fa fa-pencil fa-2x" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn>
                                                        <HeaderStyle Width="40px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkeliminarparticipante" OnClientClick="return confirm('¿Desea Eliminar este Pendiente?');"
                                                                OnClick="lnkeliminarpendiente_Click" runat="server" CommandName='<%# DataBinder.Eval(Container.DataItem, "descripcion").ToString() %>'
                                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "responsable").ToString() %>'>
                                                                <i class="fa fa-trash fa-2x" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="responsable" HeaderText="Responsable" UniqueName="nombre"
                                                        Visible="true">
                                                        <HeaderStyle Width="250px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="descripcion" HeaderText="Pendiente" UniqueName="rol"
                                                        Visible="true">
                                                    </telerik:GridBoundColumn>
                                                    
                                                    <telerik:GridTemplateColumn HeaderText="Fecha Planeada">
                                                        
                                                        <HeaderStyle Width="150px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblfecha_pendiente" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "Fecha")).ToString("dddd dd MMMM, yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("es-MX")) %>'  runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer ">

                            <div class="row" id="div_pendientes" runat="server" visible="false" style="text-align: left;">
                                <div class="col-lg-12">
                                    <div class="alert alert-danger alert-dismissible">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <asp:Label ID="lblerrorpendientes" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>

                            <asp:LinkButton ID="lnkaddpendientes" CssClass="btn btn-success btn-flat" OnClick="lnkaddpendientes_Click" OnClientClick="return confirm('¿Desea Agregar Este Pendiente?');" runat="server">
                                            Agregar Pendiente&nbsp;<i class="fa fa-plus" aria-hidden="true"></i>
                            </asp:LinkButton>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="modal fade bs-example-modal-lg" tabindex="-1" id="myModalCharter" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnkobjetivos" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="lnkdescsolucion" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="lnksupuestos" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="lnkfueraalcance" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="lnkriesgos" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">
                                <asp:Label ID="lblcharter" runat="server" Text="Label"></asp:Label></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <telerik:RadTextBox ID="rtxtcontent" TextMode="MultiLine" Rows="8" runat="server" Skin="Bootstrap" Width="100%"></telerik:RadTextBox>
                                </div>
                            </div>

                            <asp:TextBox ID="txttipo_charter" runat="server" Visible="false"></asp:TextBox>
                        </div>
                        <div class="modal-footer ">
                            <div class="row" id="div_error_charter" runat="server" visible="false" style="text-align: left;">
                                <div class="col-lg-12">
                                    <div class="alert alert-danger alert-dismissible">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <asp:Label ID="lblerrorcharter" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                            <asp:LinkButton ID="lnkcargandocharter" CssClass="btn btn-primary btn-flat" runat="server" OnClientClick="return false;" Style="display: none;">
                                        <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Guardando Información...</asp:LinkButton>
                            <asp:LinkButton ID="lnkguardarcharter" CssClass="btn btn-primary btn-flat" OnClick="lnkguardarcharter_Click"
                                OnClientClick="return ConfirmCharterModal('¿Desea Guardar los Cambios?');" runat="server">
                                            <i class="fa fa-floppy-o" aria-hidden="true"></i>&nbsp;Guardar
                            </asp:LinkButton>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="modal fade bs-example-modal-lg" tabindex="-1" id="myModalInvolucrados" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnbuscarempleado2" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="lnkagregarinvolucrado" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="lnkagregarempleado" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btneditarinvol" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Involucrados</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row" id="div_nievoinvo" runat="server">
                                <div class="col-lg-12">
                                    <h5><strong><i class="fa fa-user-circle" aria-hidden="true"></i>&nbsp;Nombre</strong></h5>
                                    <telerik:RadTextBox ID="rtxtnombreinvo" runat="server" Skin="Bootstrap" Width="100%"></telerik:RadTextBox>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12">
                                    <h5><strong><i class="fa fa-phone" aria-hidden="true"></i>&nbsp;Telefono</strong></h5>
                                    <telerik:RadTextBox ID="rtxttelefonoinvo" runat="server" Skin="Bootstrap" Width="100%" >
                                    </telerik:RadTextBox>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12">
                                    <h5><strong><i class="fa fa-mobile" aria-hidden="true"></i>&nbsp;Celular</strong></h5>
                                    <asp:TextBox ID="rtxtcelularinvo" CssClass=" form-control"  runat="server"  type="number" onkeypress="return validarEnteros(event);"
                                         onpaste="return false;"></asp:TextBox>
                                   <%-- <telerik:RadTextBox ID="rtxtcelularinvo" runat="server" Skin="Bootstrap" Width="100%" InputType="Number">
                                        <ClientEvents  OnBlur="keyPressInteger" />
                                    </telerik:RadTextBox>--%>
                                </div>
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <h5><strong><i class="fa fa-internet-explorer" aria-hidden="true"></i>&nbsp;Correo</strong></h5>
                                    <telerik:RadTextBox ID="rtxtcorreoinvo" runat="server" Skin="Bootstrap" Width="100%" InputType="Email"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="row" id="div_listempleados" runat="server" visible="false">
                                <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                            <h5><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Seleccione un empleado</strong></h5>
                                            <div style="text-align: left;" class="input-group input-group-sm">
                                                <asp:TextBox ID="txtbuscarempleado" CssClass="form-control" placeholder="Buscar" runat="server"></asp:TextBox>
                                                <span class="input-group-btn">
                                                    <asp:LinkButton ID="btnbuscarempleado2" CssClass="btn btn-primary btn-flat" runat="server" OnClientClick="return ChangedTextLoad2();" OnClick="btnbuscarempleado2_Click">
                                                <i class="fa fa-search" aria-hidden="true"></i>
                                                    </asp:LinkButton>
                                                </span>
                                            </div>

                                            <asp:Image ID="imgloadempleado_" Style="display: none;" ImageUrl="~/img/load.gif" runat="server" />
                                            <label id="lblbe2" runat="server" style="display: none; color: #1565c0">Buscando Empleados</label>
                                        </div>
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">


                                            <div style="height: 150px; overflow: scroll;">
                                                <telerik:RadListBox RenderMode="Lightweight" runat="server" ID="rdllista_empleados" Width="100%"   Style="font-size: 11px"
                                                    Skin="Bootstrap" SelectionMode="Single" OnSelectedIndexChanged="rdllista_empleados_SelectedIndexChanged" AutoPostBack="true">
                                                </telerik:RadListBox>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <h5><strong><i class="fa fa-briefcase" aria-hidden="true"></i>&nbsp;Rol del Involucrado</strong></h5>
                                    <asp:DropDownList ID="ddlrol" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlrol_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-lg-12 col-md-12 col-sm-12">

                                    <h5><strong><i class="fa fa-briefcase" aria-hidden="true"></i>&nbsp;Responsabilidades</strong></h5>
                                    <asp:TextBox ID="txtresponsabilidades" ReadOnly="true" TextMode="MultiLine" Rows="2" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <asp:TextBox ID="txtid_invo" runat="server" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="txtno_empleado" runat="server" Visible="false"></asp:TextBox>
                        </div>
                        <div class="modal-footer ">
                            <div class="row" id="diverror_invo" runat="server" visible="false" style="text-align: left;">
                                <div class="col-lg-12">
                                    <div class="alert alert-danger alert-dismissible">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <asp:Label ID="lblerrorinvo" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <%--<asp:Button ID="btnbuscarempleado2" runat="server" Text="Button" Style="display: none;" OnClick="btnbuscarempleado2_Click"/>--%>
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                            <asp:LinkButton ID="lnkeliminarinvolucrado" Visible="false" CssClass="btn btn-danger btn-flat" OnClick="lnkeliminarinvolucrado_Click"
                                OnClientClick="return ConfirmEntregableDelete('¿Desea Eliminar este Involucrado?');" runat="server">
                                            <i class="fa fa-trash" aria-hidden="true"></i>&nbsp;Eliminar
                            </asp:LinkButton>
                            <asp:LinkButton OnClientClick="return false;" ID="lnkcaegandoinvolu" CssClass="btn btn-primary btn-flat" runat="server" Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Guardando Involucrado...
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkguardarinvolucrado" CssClass="btn btn-primary btn-flat" OnClick="lnkguardarinvolucrado_Click"
                                OnClientClick="return ConfirmInvolucradoModal('¿Desea Guardar este Involucrado?');" runat="server">
                                            <i class="fa fa-floppy-o" aria-hidden="true"></i>&nbsp;Guardar
                            </asp:LinkButton>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="modal fade bs-example-modal-lg" tabindex="-1" id="myModalClientes" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnkseleccionarcliente" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="lnkbuscarcliente" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Clientes</h4>
                        </div>
                        <div class="modal-body">

                            <div class="row" id="div_listaclientes" runat="server" visible="false">
                                <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">

                                    <h5><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Seleccione un Cliente</strong></h5>
                                    <div style="text-align: left;">
                                        <asp:TextBox ID="txtbuscarclientes" CssClass="form-control" placeholder="Buscar"  runat="server"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <asp:LinkButton ID="lnkbuscarcliente" CssClass="btn btn-primary btn-flat" runat="server" OnClientClick="return ChangedTextLoad3();" OnClick="lnkbuscarcliente_Click">
                                                <i class="fa fa-search" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </span>
                                    </div>

                                    <asp:Image ID="imgloadcliente" Style="display: none;" ImageUrl="~/img/load.gif" runat="server" />
                                    <label id="lblloadcliente" runat="server" style="display: none; color: #1565c0">Buscando Clientes</label>
                                </div>
                                <div  class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <div style="height: 100px; overflow: scroll;">
                                    <telerik:RadListBox Style="font-size: 10px" RenderMode="Lightweight" runat="server" ID="rdlclientes" Width="100%"
                                        Skin="Bootstrap" SelectionMode="Single" OnSelectedIndexChanged="rdlclientes_SelectedIndexChanged" AutoPostBack="true">
                                    </telerik:RadListBox>
                                        </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <h5><strong><i class="fa fa-home" aria-hidden="true"></i>&nbsp;Dirección</strong></h5>
                                    <telerik:RadTextBox ID="rtxtcliente_direccion" runat="server" Skin="Bootstrap" Width="100%"></telerik:RadTextBox>
                                </div>
                                <div class="col-lg-12" style="display: none;">
                                    <h5><strong><i class="fa fa-briefcase" aria-hidden="true"></i>&nbsp;Ocupación Cliente</strong></h5>
                                    <telerik:RadTextBox ID="rtxtcliente_ocupacion" runat="server" Skin="Bootstrap" Width="100%"></telerik:RadTextBox>
                                </div>
                            </div>

                            <div class="row" id="div_nombre_cliente" runat="server">
                                <div class="col-lg-12">
                                    <h5><strong><i class="fa fa-address-book-o" aria-hidden="true"></i>&nbsp;Contactos</strong>
                                        <small>Seleccione uno o mas contactos&nbsp;</small>
                                        <span>
                                            <asp:LinkButton ID="lnkagregarnuevocontacto" CssClass="btn btn-primary btn-flat btn-xs" 
                                                runat="server" OnClick="lnkagregarnuevocontacto_Click">
                                                Agregar Nuevo Contacto</asp:LinkButton></span>
                                    </h5>
                                    
                                    <div style="height: 100px; overflow: scroll;">
                                    <telerik:RadListBox Style="font-size: 10px" RenderMode="Lightweight" runat="server" ID="rdlcontacto_clientes" Width="100%"
                                        OnItemDataBound="rdlcontacto_clientes_ItemDataBound" Skin="Bootstrap" SelectionMode="Multiple">
                                    </telerik:RadListBox>
                                        </div>
                                    <div id="div_addnewcontact" runat="server" visible="false">
                                        <div class="col-lg-12">
                                            <h5><strong>Nombre</strong></h5>
                                            <telerik:RadTextBox ID="rtxtnombre_newcontact" Skin="Bootstrap" Width="100%" runat="server"></telerik:RadTextBox>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                            <h5><strong>Telefono</strong></h5>
                                            <telerik:RadTextBox ID="rtxttelefeno_newcontact" Skin="Bootstrap" InputType="Number" Width="100%" runat="server"></telerik:RadTextBox>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-122">
                                            <h5><strong>Correo</strong></h5>
                                            <telerik:RadTextBox ID="rtxtcorreoo_newcontact" Skin="Bootstrap" Width="100%" InputType="Email" runat="server"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" id="div_usuarios" runat="server" visible="false">
                                <div class="col-lg-12">
                                    <h5><strong><i class="fa fa-users" aria-hidden="true"></i>&nbsp;Tipo de Usuario</strong></h5>
                                    <asp:DropDownList ID="ddltipos_usuarios" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12">
                                    <h5><strong><i class="fa fa-user-circle-o" aria-hidden="true"></i>&nbsp;Usuario</strong></h5>
                                    <telerik:RadTextBox ID="rtxtusuario" runat="server" Skin="Bootstrap" Width="100%"></telerik:RadTextBox>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12">
                                    <h5><strong><i class="fa fa-sign-in" aria-hidden="true"></i>&nbsp;Contraseña</strong></h5>
                                    <telerik:RadTextBox ID="rtxtcontraseña" runat="server" Skin="Bootstrap" Width="100%"></telerik:RadTextBox>
                                </div>
                            </div>
                            <asp:TextBox ID="txtid_cliente" runat="server" Visible="false"></asp:TextBox>
                        </div>
                        <div class="modal-footer ">
                            <div class="row" id="div_errorclientes" runat="server" visible="false" style="text-align: left;">
                                <div class="col-lg-12">
                                    <div class="alert alert-danger alert-dismissible">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <asp:Label ID="lblerrorclientes" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                            <asp:LinkButton OnClientClick="return false;" ID="lnkcargandocliente" CssClass="btn btn-success btn-flat" runat="server" Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Guardando Información...
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkguardarcliente" CssClass="btn btn-success btn-flat"
                                OnClientClick="return ConfirmClientesModal('¿Desea Guardar esta información del proyecto?');" runat="server" OnClick="lnkguardarcliente_Click">
                                            <i class="fa fa-floppy-o" aria-hidden="true"></i>&nbsp;Guardar
                            </asp:LinkButton>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="modal fade bs-example-modal-lg" tabindex="-1" id="myModalEmpleados" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnbuscarempleado" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="lnkagregarempleadoaproyecto" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Empleados</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                    <h5><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Seleccione uno o mas empleados</strong></h5>
                                    <div style="text-align: left;" class="input-group input-group-sm">
                                        <asp:TextBox ID="txtbuscarempleadoproyecto" CssClass="form-control" placeholder="Buscar"
                                            runat="server"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <asp:LinkButton ID="btnbuscarempleado" CssClass="btn btn-primary btn-flat" runat="server" OnClientClick="ChangedTextLoad();" OnClick="btnbuscarempleado_Click">
                                                <i class="fa fa-search" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </span>
                                    </div>

                                    <asp:Image ID="imgloadempleados" Style="display: none;" ImageUrl="~/img/load.gif" runat="server" />
                                    <label id="lblbe" runat="server" style="display: none; color: #1565c0">Buscando Empleados</label>
                                </div>
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                   
                                    <div style="height: 200px; overflow: scroll;">

                                        <telerik:RadListBox RenderMode="Lightweight" runat="server" ID="rdlempleadosproyecto"  Style="font-size: 11px" Width="100%"
                                            Skin="Bootstrap" SelectionMode="Multiple">
                                        </telerik:RadListBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer ">
                            <div class="row" id="div_errorempleados" runat="server" visible="false" style="text-align: left;">
                                <div class="col-lg-12">
                                    <div class="alert alert-danger alert-dismissible">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <asp:Label ID="lblerrorempleados" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                           <%-- <asp:Button ID="btnbuscarempleado" runat="server" Text="Button" Style="display: none;" OnClick="btnbuscarempleado_Click" />--%>
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                            <asp:LinkButton OnClientClick="return false;" ID="lnkcargarempleados" CssClass="btn btn-primary btn-flat" runat="server" Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Guardando Configuración...
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkguardarempleado" CssClass="btn btn-primary btn-flat" OnClick="lnkguardarempleado_Click"
                                OnClientClick="return ConfirmEmpleadoProyectoModal('¿Desea Guardar esta configuración del Proyecto?');" runat="server">
                                            <i class="fa fa-floppy-o" aria-hidden="true"></i>&nbsp;Guardar
                            </asp:LinkButton>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="modal fade bs-example-modal-lg" tabindex="-1" id="myModalTerminacion" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnkterminacíon" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="lnkcargdenuevo" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="lnkcargdenuevokit" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="lnkcargdenuevocierre" EventName="Click" />
                    <asp:PostBackTrigger ControlID="lnksubirencuestas" />
                    <asp:PostBackTrigger ControlID="lnkkit" />
                    <asp:PostBackTrigger ControlID="lnkdoccierre" />
                    <asp:PostBackTrigger ControlID="lnkdeescargaencuesta" />
                    <asp:PostBackTrigger ControlID="lnkdeescargakit" />
                    <asp:PostBackTrigger ControlID="lnkdeescargacierre" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Terminar Proyecto</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <h5>Para Terminar el Proyecto, debe subir los documentos solicitados y llenar la información correspondiente</h5>
                                </div>
                                <div class="col-lg-12">
                                    <h5><strong><i class="fa fa-question-circle" aria-hidden="true"></i>&nbsp;Encuestas</strong></h5>
                                    <div class="input-group" id="div_cargaencuesta" runat="server">
                                        <asp:FileUpload ID="fupencuestas" runat="server" CssClass=" form-control"  onchange="return ValidateUF(this,10);" />
                                        <span class="input-group-addon">
                                            <asp:LinkButton CommandName="encuestas" ID="lnksubirencuestas" OnClientClick="return ConfirmUploadEncuesta('¿Desea subir este archivo?')"
                                                OnClick="lnksubirencuestas_Click" runat="server">
                                            <i class="fa fa-upload" aria-hidden="true"></i>&nbsp;Subir
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lnkcargandoencuesta" OnClientClick="return false;" runat="server" Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>&nbsp;Subiendo
                                            </asp:LinkButton>
                                        </span>
                                    </div>
                                    <div class="has-success" runat="server" id="div_okencuesta" style="text-align: right; padding-top: 5px;">
                                        <label class="control-label">
                                            <i class="fa fa-check"></i>Documento cargado 
                                            <span>
                                                <asp:LinkButton ID="lnkdeescargaencuesta" OnClick="DownloadFile" runat="server" CssClass="btn btn-success btn-flat btn-sm"> 
                                                    <i class="fa fa-download" aria-hidden="true"></i>&nbsp;Descargar</asp:LinkButton>
                                                <asp:LinkButton ID="lnkcargdenuevo" CommandArgument="encuesta" OnClick="lnkcargdenuevo_Click" runat="server" CssClass="btn btn-danger btn-flat btn-sm"> 
                                                    <i class="fa fa-upload" aria-hidden="true"></i>&nbsp;Cargar</asp:LinkButton>
                                            </span>
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <h5><strong><i class="fa fa-file" aria-hidden="true"></i>&nbsp;Documento de Cierre</strong></h5>

                                    <div class="input-group" id="div_cargacierre" runat="server">
                                        <asp:FileUpload ID="fupdocierre" runat="server" CssClass=" form-control" onchange="return ValidateUF(this,10);" />
                                        <span class="input-group-addon">
                                            <asp:LinkButton CommandName="cierre" ID="lnkdoccierre" OnClick="lnksubirencuestas_Click" runat="server" OnClientClick="return ConfirmUploadCierre('¿Desea subir este archivo?');">                                        
                                            <i class="fa fa-upload" aria-hidden="true"></i>&nbsp;Subir
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lnkcargandocierre" OnClientClick="return false;" runat="server" Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>&nbsp;Subiendo
                                            </asp:LinkButton>
                                        </span>
                                    </div>
                                    <div class="has-success" runat="server" id="div_okcierre" style="text-align: right; padding-top: 5px;">
                                        <label class="control-label">
                                            <i class="fa fa-check"></i>Documento cargado 
                                            <span>
                                                <asp:LinkButton ID="lnkdeescargacierre" OnClick="DownloadFile" runat="server" CssClass="btn btn-success btn-flat btn-sm"> 
                                                    <i class="fa fa-download" aria-hidden="true"></i>&nbsp;Descargar</asp:LinkButton>
                                            <asp:LinkButton ID="lnkcargdenuevocierre" CommandArgument="cierre" OnClick="lnkcargdenuevo_Click" runat="server" CssClass="btn btn-danger btn-flat btn-sm"> 
                                                    <i class="fa fa-upload" aria-hidden="true"></i>&nbsp;Cargar</asp:LinkButton>
                                            </span>
                                        </label>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <h5><strong><i class="fa fa-briefcase" aria-hidden="true"></i>&nbsp;Kit de Cierre</strong></h5>
                                    <div class="input-group" id="div_cargakit" runat="server">
                                        <asp:FileUpload ID="fupkit" runat="server" CssClass=" form-control" onchange="return ValidateUF(this,10);" />
                                        <span class="input-group-addon">
                                            <asp:LinkButton CommandName="kit" ID="lnkkit" OnClick="lnksubirencuestas_Click" runat="server" OnClientClick="return ConfirmUploadKit('¿Desea subir este archivo?');">                                        
                                            <i class="fa fa-upload" aria-hidden="true"></i>&nbsp;Subir
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lnkcargandokit" OnClientClick="return false;" runat="server" Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>&nbsp;Subiendo
                                            </asp:LinkButton>
                                        </span>
                                    </div>
                                    <div class="has-success" runat="server" id="div_okkit" style="text-align: right; padding-top: 5px;">
                                        <label class="control-label">
                                            <i class="fa fa-check"></i>Documento cargado 
                                            <span>
                                                <asp:LinkButton ID="lnkdeescargakit" OnClick="DownloadFile" runat="server" CssClass="btn btn-success btn-flat btn-sm"> 
                                                    <i class="fa fa-download" aria-hidden="true"></i>&nbsp;Descargar</asp:LinkButton>
                                            <asp:LinkButton  CommandArgument="kit" ID="lnkcargdenuevokit" OnClick="lnkcargdenuevo_Click" runat="server" CssClass="btn btn-danger btn-flat btn-sm"> 
                                                    <i class="fa fa-upload" aria-hidden="true"></i>&nbsp;Cargar</asp:LinkButton>
                                            </span>
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                    <h5><strong><i class="fa fa-usd" aria-hidden="true"></i>&nbsp;Costo Real</strong></h5>
                                    <telerik:RadTextBox ID="rtxtcostoreal" InputType="Number" Width="100%" runat="server" Skin="Bootstrap"></telerik:RadTextBox>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                    <h5><strong><i class="fa fa-usd" aria-hidden="true"></i>&nbsp;Valor Real Ganado</strong></h5>
                                    <telerik:RadTextBox ID="rtxtvalorganado" InputType="Number" Width="100%" runat="server" Skin="Bootstrap"></telerik:RadTextBox>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer ">
                            <div class="row" id="div_errorterminacion" runat="server" visible="false" style="text-align: left;">
                                <div class="col-lg-12">
                                    <div class="alert alert-danger alert-dismissible">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <asp:Label ID="lblerrorterminacion" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                            <asp:LinkButton OnClientClick="return false;" ID="lnkcargandotermina" CssClass="btn btn-danger btn-flat" runat="server" Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Terminando Proyecto...
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkterminaproyecto" CssClass="btn btn-danger btn-flat" OnClick="lnkterminaproyecto_Click"
                                OnClientClick="return ConfirmTerminarProyecto('¿Desea Terminar el Proyecto?');" runat="server">
                                            <i class="fa fa-floppy-o" aria-hidden="true"></i>&nbsp;Terminar
                            </asp:LinkButton>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="modal fade bs-example-modal-xs" tabindex="-1" id="myModalLoad" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-xs" role="document">

            <div class="modal-content">
                <div class="modal-header">
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Image ID="idid" Style="display: block; margin: 0 auto;"
                                runat="server" ImageUrl="~/img/loading_2.gif" />
                            <h3 style="text-align:center;"><strong>Cargando Información...</strong></h3>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hdfid_entregable" runat="server" />
    <asp:HiddenField ID="hdfid_proyecto" runat="server" />
    <asp:HiddenField ID="hdfid_cliente" runat="server" />
    <asp:HiddenField ID="hdfid_involucrado" runat="server" />
    <asp:HiddenField ID="hdfmotivos" runat="server" />
    <asp:Button ID="btneditarentregablegraph" runat="server" Text="Button" Style="display: none;" OnClick="btneditarentregablegraph_Click" />
    <asp:Button ID="btneditarinvol" runat="server" Text="Button" Style="display: none;" OnClick="btneditarinvol_Click" />
</asp:Content>