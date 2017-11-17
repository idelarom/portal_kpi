<%@ Page Title="Solicitud" Language="C#" MasterPageFile="~/Pages/MP/Global.Master" AutoEventWireup="true" CodeBehind="compensaciones_solicitud.aspx.cs" Inherits="presentacion.Pages.Compensaciones.compensaciones_solicitud" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Init(value) {
            if ($.fn.dataTable.isDataTable(value)) {
                table = $(value).DataTable();
            }
            else {
                $(value).DataTable({
                    "paging": true,
                    "pageLength": 10,
                    "lengthChange": false,
                    "searching": true,
                    "ordering": true,
                    "info": true,
                    "autoWidth": false,
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

        }
        function InitPagging(value) {
            if ($.fn.dataTable.isDataTable(value)) {
                table = $(value).DataTable();
            }
            else {
                $(value).DataTable({
                    "paging": true,
                    "pageLength": 8,
                    "lengthChange": false,
                    "searching": true,
                    "ordering": true,
                    "info": true,
                    "autoWidth": false,
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

        }
        var opts = {
          lines: 13 // The number of lines to draw
        , length: 28 // The length of each line
        , width: 14 // The line thickness
        , radius: 42 // The radius of the inner circle
        , scale: 1.4 // Scales overall size of the spinner
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
      function ConfirmLoadResultados(msg) {
            if (confirm(msg)) {
                $("#<%= lnkguardaresultadosload.ClientID%>").show();
                $("#<%= lnkguardaresultados.ClientID%>").hide();
                return true;
            } else {
                return false;
            }
        }
        function Load() {
            $("#<%= load.ClientID%>").show();
        	var target = document.getElementById('<%= load.ClientID %>');
            var spinner = new Spinner(opts).spin(target);
            return true;
        }
        function Load2() {
            $("#<%= load2.ClientID%>").show();
            var target = document.getElementById('<%= load2.ClientID %>');
            var spinner = new Spinner(opts).spin(target);
            return true;
        }
        function Load3() {
            $("#<%= load3.ClientID%>").show();
        	var target = document.getElementById('<%= load3.ClientID %>');
            var spinner = new Spinner(opts).spin(target);
            return true;
        }
        function control_clear(control) {
            var valor = control.value;

            if (valor.length > 0) {
                control.value = "";
            }
        }
        function ValuesPMTracker(folio, proyecto, cliente) {
            var hdnfolio = document.getElementById('<%= hdnfolio.ClientID %>');
            hdnfolio.value = folio;
            var hdnproyecto = document.getElementById('<%= hdnproyecto.ClientID %>');
            hdnproyecto.value = proyecto;
            var hdncliente = document.getElementById('<%= hdncliente.ClientID %>');
            hdncliente.value = cliente;
            ModalCloseGlobal("#modal_proyectos");
            Load();
            document.getElementById('<%= lnkproyecto.ClientID%>').click();
        }
        function ValuesCC(CC, DESC) {
            var hdndesc_cc = document.getElementById('<%= hdndesc_cc.ClientID %>');
            hdndesc_cc.value = CC+" - "+DESC;
            var hdnCC_Cargo = document.getElementById('<%= hdnCC_Cargo.ClientID %>');
            hdnCC_Cargo.value = CC;
            ModalCloseGlobal("#modal_cc");
            Load();
            document.getElementById('<%= lnkcc.ClientID%>').click();
        }
        
        function ValuesEmpleado(no_, name) {
            var hdnEmployeeNumber = document.getElementById('<%= hdnEmployeeNumber.ClientID %>');
            hdnEmployeeNumber.value = no_;
            var txtfilterempleado = document.getElementById('<%= txtfilterempleado.ClientID %>');
            txtfilterempleado.value = name;
            Load();
            document.getElementById('<%= btncargarempleado.ClientID%>').click();
            ModalCloseGlobal("#modal_empleados");
        }
        function Download(path) {
            var hdfpath = document.getElementById('<%= hdfpath.ClientID %>');
            hdfpath.value = path;
            console.log(path);
            document.getElementById('<%= lnkdescargas.ClientID%>').click();
        }
        function ViewBond(id_bond) {
            var hdnIdRequestBond = document.getElementById('<%= hdnIdRequestBond.ClientID %>');
            hdnIdRequestBond.value = id_bond;
            document.getElementById('<%= btnviewrequest.ClientID%>').click();
            Load3();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="uusus" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <h3>Solicitud de bonos</h3>
                </div>

                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">

                    <div id="load2" runat="server" style="display: none;"></div>
                    <div class="box box-danger">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                    <h5><strong><i class="fa fa-pencil-square-o" aria-hidden="true"></i>&nbsp;Tipo de bono</strong></h5>
                                    <asp:DropDownList ID="cbBonds_Types" AutoPostBack="true" onchange="Load2();"
                                        OnSelectedIndexChanged="cbBonds_Types_SelectedIndexChanged" CssClass=" form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div id="load" runat="server" style="display: none;"></div>
                    <div class="box box-danger" id="tblInformationRequisitions" runat="server" visible="false">
                        <div class="box-header with-border">
                            <h3 class="box-title">Solicitud</h3>

                            <div class="box-tools pull-right">
                                <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                    <i class="fa fa-minus"></i>
                                </button>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" id="trWeek" runat="server">
                                    <h5><strong><i class="fa fa-calendar-o" aria-hidden="true"></i>&nbsp;Seleccione una fecha de una semana</strong></h5>
                                    <div class="row">
                                        <div class="col-lg-2 col-md-3 col-sm-4 col-xs-12">
                                            <telerik:RadDatePicker OnSelectedDateChanged="calDateSupport_SelectedDateChanged" AutoPostBack="true" ID="calDateSupport" Width="100%" runat="server" Skin="Bootstrap"></telerik:RadDatePicker>

                                        </div>
                                        <div class="col-lg-4 col-md-5 col-sm-8 col-xs-12">
                                            <div class="table table-responsive">
                                                <table id="table_fechas" class="table table-responsive table-bordered table-condensed table-hover table-striped"
                                                    style="font-size: 11px">
                                                    <thead style="background-color: #dd4b39; color: white;">
                                                        <tr>
                                                            <th style="min-width: 30px; text-align: center;" scope="col"></th>
                                                            <th style="min-width: 140px; text-align: left;" scope="col">Dias trabajados</th>
                                                            <th style="min-width: 50px; text-align: center;" scope="col">Monto</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="repeater_fechas_Soporte" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td style="text-align: center;">
                                                                        <asp:CheckBox ID="cbx_checkday" Style="cursor: pointer;" Checked="true" Text="Seleccionar" AutoPostBack="true" runat="server" OnCheckedChanged="cbx_checkday_CheckedChanged"
                                                                            amount='<%# Eval("amount") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <%# Convert.ToDateTime(Eval("date")).ToString("dddd dd MMMM, yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("es-MX")) %>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <%# Convert.ToDecimal(Eval("amount")).ToString("C2") %>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </div>
                                            <a class="btn btn-block btn-social btn-google" style="text-align: right;">
                                                <i class="fa fa-money"></i>Monto total autorizado: <strong>
                                                    <asp:Label ID="lblmonto_total_autorizadp" runat="server" Text="$ 0.00"></asp:Label></strong>
                                            </a>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4 col-md-6 col-sm-8 col-xs-12">
                                    <h5><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Empleado</strong>
                                    </h5>
                                    <div class="input-group" runat="server" id="div_filtro_empleados">
                                        <asp:TextBox ID="txtfilterempleado" CssClass=" form-control" ReadOnly="true"
                                            runat="server"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <asp:LinkButton ID="lnksearch" CssClass="btn btn-primary btn-flat"
                                                OnClientClick="return Load();" OnClick="lnksearch_Click" runat="server">
                                                <i class="fa fa-search" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-2 col-md-3 col-sm-4 col-xs-12" id="trFolioPMTracker" runat="server">
                                    <h5><strong><i class="fa fa-ticket" aria-hidden="true"></i>&nbsp;Folio PM Tracker</strong></h5>
                                    <div class="input-group" runat="server">
                                        <asp:TextBox
                                            onfocus="this.select();" ID="txtPMTrackerNumberImplementations" CssClass=" form-control"
                                            ReadOnly="false" runat="server"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <asp:LinkButton ID="lnksearchpmTracker" CssClass="btn btn-primary btn-flat"
                                                OnClientClick="return Load();"
                                                OnClick="lnksearchpmTracker_Click" runat="server">
                                                <i class="fa fa-search" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-lg-4 col-md-5 col-sm-8 col-xs-12" id="trProjectName" runat="server">
                                    <h5><strong><i class="fa fa-cubes" aria-hidden="true"></i>&nbsp;Nombre proyecto</strong></h5>
                                    <asp:TextBox ID="txtProjectNameImplementations" CssClass=" form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6 col-md-8 col-sm-12 col-xs-12" id="trCustomerName" runat="server">
                                    <h5><strong><i class="fa fa-handshake-o" aria-hidden="true"></i>&nbsp;Nombre cliente</strong></h5>
                                    <asp:TextBox ID="txtCustomerNameImplementations" CssClass=" form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-2 col-md-4 col-sm-4 col-xs-12" id="trNumberHours" runat="server">
                                    <h5><strong><i class="fa fa-clock-o" aria-hidden="true"></i>&nbsp;Cantidad de horas</strong></h5>
                                    <asp:TextBox ID="txtNumberHoursImplementations" CssClass=" form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-2 col-md-4 col-sm-4 col-xs-12" id="trAuthorizationAmount" runat="server">
                                    <h5><strong><i class="fa fa-money" aria-hidden="true"></i>&nbsp;Importe bono</strong></h5>
                                    <asp:TextBox ID="txtAuthorizationAmount" onfocus="$(this).select();" AutoPostBack="true"
                                        OnTextChanged="txtAuthorizationAmount_TextChanged"
                                        CssClass=" form-control" runat="server"></asp:TextBox>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-lg-6 col-md-8 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Periodo del bono</strong></h5>
                                    <div class="row" style="padding: 5px;">
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12" id="tblMonthSelect" runat="server">
                                            <div class="row">
                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                    <h6><strong>
                                                        <asp:Label ID="lblTitleMonth" runat="server" Text="Mes:"></asp:Label></strong></h6>
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                    <asp:DropDownList ID="cbInitialMonth" CssClass="form-control" AutoPostBack="true"
                                                        OnSelectedIndexChanged="cbInitialMonth_SelectedIndexChanged"
                                                        runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                    <asp:DropDownList ID="cbInitialYear" CssClass="form-control" AutoPostBack="true"
                                                        runat="server">
                                                    </asp:DropDownList>
                                                </div>

                                            </div>

                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12" id="trFinalizeMonth" runat="server" visible="false">
                                            <div class="row">
                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                    <h6><strong>Mes final:</strong></h6>
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                    <asp:DropDownList ID="cbFinalizeMonth" CssClass="form-control"
                                                        OnSelectedIndexChanged="cbInitialYear_SelectedIndexChanged" AutoPostBack="true"
                                                        runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                    <asp:DropDownList ID="cbFinalizeYear" CssClass="form-control"
                                                        runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <br />
                                            <div class="row">
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                    <telerik:RadDatePicker ID="txtPeriodDateOf" Width="100%" runat="server" Skin="Bootstrap"></telerik:RadDatePicker>
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                    <telerik:RadDatePicker ID="txtPeriodDateTo" Width="100%" runat="server" Skin="Bootstrap"></telerik:RadDatePicker>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                    <h5><strong><i class="fa fa-briefcase" aria-hidden="true"></i>&nbsp;CC Empleado</strong></h5>
                                    <asp:TextBox ID="txtCC_Emp" CssClass=" form-control" runat="server"></asp:TextBox>
                                </div>

                                <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                    <h5><strong><i class="fa fa-briefcase" aria-hidden="true"></i>&nbsp;CC Cargo</strong></h5>
                                    <div class="input-group" runat="server">

                                        <asp:TextBox ID="txtCC_Cargo" CssClass=" form-control" runat="server"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <asp:LinkButton ID="lnksearchcc" OnClientClick="return Load();" CssClass="btn btn-primary btn-flat"
                                                OnClick="lnksearchcc_Click" runat="server">
                                                <i class="fa fa-search" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-comments" aria-hidden="true"></i>&nbsp;Comentarios</strong></h5>
                                    <asp:TextBox ID="txtComments" TextMode="MultiLine" Rows="3" CssClass=" form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <asp:LinkButton ID="lnkadjuntarfiles" OnClick="lnkadjuntarfiles_Click" CssClass="btn btn-success btn-flat" runat="server">
                                        <i class="fa fa-file-archive-o" aria-hidden="true"></i>&nbsp;Adjuntar archivos</asp:LinkButton>
                                    <asp:LinkButton ID="lnksolicitar" CssClass="btn btn-primary btn-flat" runat="server" OnClientClick="return confirm('¿Desea guardar esta solicitud?');"
                                        OnClick="lnksolicitar_Click">
                                         <i class="fa fa-bookmark" aria-hidden="true">

                                         </i>&nbsp;Solicitar</asp:LinkButton>
                                    <asp:LinkButton ID="lnkcancelar" OnClick="lnkcancelar_Click" OnClientClick="return confirm('¿Desea cancelar la solicitud?');" CssClass="btn btn-danger btn-flat" runat="server">
                                          <i class="fa fa-times" aria-hidden="true"></i>&nbsp;Cancelar</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="box box-danger" id="trGridRequisitions" runat="server" visible="false">
                        <div class="box-header with-border">
                            <h3 class="box-title">Listado de bonos</h3>

                            <div class="box-tools pull-right">
                                <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                    <i class="fa fa-minus"></i>
                                </button>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <div class=" table table-responsive">
                                        <div id="load3" runat="server" style="display: none;"></div>
                                        <table id="table_bonos" class=" table table-responsive table-bordered table-condensed"
                                            style="font-size: 12px">
                                            <thead>
                                                <tr>
                                                    <th style="min-width: 20px; text-align: left;" scope="col"></th>
                                                    <th style="min-width: 30px; text-align: center;" scope="col"># Sol.</th>
                                                    <th style="min-width: 100px; text-align: center;" scope="col">Fecha</th>
                                                    <th style="min-width: 80px; text-align: center;" scope="col">Tipo</th>
                                                    <th style="min-width: 190px; text-align: left;" scope="col">Empleado</th>
                                                    <th style="min-width: 100px; text-align: center;" scope="col">Monto</th>
                                                    <th style="min-width: 200px; text-align: left;" scope="col">CC Cargo</th>
                                                    <th style="min-width: 190px; text-align: left;" scope="col">Solicitante</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="gridBondsRequisitions" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <a class="btn btn-success btn-flat btn-xs"
                                                                    onclick='<%# "return ViewBond("+ Eval("id_request_bond").ToString()+");" %>'>Archivos
                                                                </a>
                                                            </td>
                                                            <td style="min-width: 30px; text-align: center;">
                                                                <%# Eval("id_request_bond") %>
                                                            </td>
                                                            <td style="min-width: 100px; text-align: center;">
                                                                <%# Convert.ToDateTime(Eval("created")).ToString("dd MMMM, yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("es-MX")) %>
                                                            </td>
                                                            <td style="min-width: 100px; text-align: center;">
                                                                <%# System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Eval("bond_name").ToString().ToLower())%>
                                                            </td>
                                                            <td>
                                                                <%# System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Eval("full_name").ToString().ToLower()) %>
                                                            </td>
                                                            <td style="min-width: 100px; text-align: center;">
                                                                <%# Convert.ToDecimal(Eval("authorization_amount")).ToString("C2") %>
                                                            </td>
                                                            <td>
                                                                <%# Eval("CC_Cargo") %>
                                                            </td>
                                                            <td>
                                                                <%# System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Eval("created_by").ToString().ToLower()) %>
                                                            </td>
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

            <div class="modal fade bs-example-modal-lg" tabindex="-1" id="modal_proyectos" role="dialog"
                aria-labelledby="mySmallModalLabel">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Seleccione un proyecto</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class=" table table-responsive">
                                        <table id="table_proyectos" class=" table table-responsive table-bordered table-condensed" style="font-size: 12px">
                                            <thead>
                                                <tr>
                                                    <th style="min-width: 50px; text-align: left;" scope="col"></th>
                                                    <th style="min-width: 80px; text-align: left;" scope="col"># Proyecto</th>
                                                    <th style="min-width: 300px; text-align: left;" scope="col">Nombre Proyecto</th>
                                                    <th style="min-width: 200px; text-align: left;" scope="col">Cliente</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="repeat_proyectos" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <a class="btn btn-primary btn-flat btn-xs"
                                                                    onclick='<%# "return ValuesPMTracker("+@""""+ Eval("folio").ToString()+@""""+@","""+ Eval("nombre_proyecto").ToString()+@""""+@","""+ Eval("nombre_cliente").ToString()+@""""+");" %>'>Seleccionar</a>
                                                            </td>
                                                            <td>
                                                                <%# Eval("folio") %>
                                                            </td>
                                                            <td>
                                                                <%# Eval("nombre_proyecto") %>
                                                            </td>
                                                            <td>
                                                                <%# Eval("nombre_cliente") %>
                                                            </td>
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

            <div class="modal fade bs-example-modal-lg" tabindex="-1" id="modal_empleados" role="dialog"
                aria-labelledby="mySmallModalLabel">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Seleccione un empleado</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class=" table table-responsive">
                                        <table id="table_empleados" class=" table table-responsive table-bordered table-condensed" style="font-size: 12px">
                                            <thead>
                                                <tr>
                                                    <th style="min-width: 50px; text-align: left;" scope="col"></th>
                                                    <th style="min-width: 500px; text-align: left;" scope="col">Empleado</th>
                                                    <th style="min-width: 60px; text-align: center;" scope="col"># Empleado</th>
                                                    <th style="min-width: 50px; text-align: center;" scope="col">CC</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="repeater_empleados" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <a class="btn btn-primary btn-flat btn-xs"
                                                                    onclick='<%# "return ValuesEmpleado("+@""""+ Eval("employee_number").ToString()+@""""+@","""+ Eval("full_name").ToString()+@""""+");" %>'>Seleccionar</a>
                                                            </td>
                                                            <td>
                                                                <%# Eval("full_name") %>
                                                            </td>
                                                            <td style="min-width: 60px; text-align: center;">
                                                                <%# Eval("employee_number") %>
                                                            </td>
                                                            <td style="min-width: 50px; text-align: center;">
                                                                <%# Eval("id_cost_center") %>
                                                            </td>
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

            <div class="modal fade bs-example-modal-lg" tabindex="-1" id="modal_cc" role="dialog"
                aria-labelledby="mySmallModalLabel">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Seleccione un Centro de Costos</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="table table-responsive">

                                        <table id="table_cc" class=" table table-responsive table-bordered table-condensed" style="font-size: 12px">
                                            <thead>
                                                <tr>
                                                    <th style="min-width: 50px; text-align: left;" scope="col"></th>
                                                    <th style="min-width: 700px; text-align: left;" scope="col">Centro de Costos</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="repetaer_cc" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>

                                                                <a class="btn btn-primary btn-flat btn-xs"
                                                                    onclick='<%# "return ValuesCC("+@""""+ Eval("CC").ToString()+@""""+@","""+ Eval("DESC_CC").ToString()+@""""+");" %>'>Seleccionar</a>
                                                            </td>
                                                            <td>
                                                                <%# Eval("CC").ToString().Trim()+" - "+Eval("DESC_CC").ToString().Trim() %>
                                                            </td>
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

            <asp:HiddenField ID="hdfguid" runat="server" />
            <asp:HiddenField ID="hdndesc_cc" runat="server" />
            <asp:HiddenField ID="hdnfolio" runat="server" />
            <asp:HiddenField ID="hdnproyecto" runat="server" />
            <asp:HiddenField ID="hdncliente" runat="server" />
            <input id="hdnAuthorizationAmount" runat="server" type="Hidden" />
            <input id="hdnValidateAmount" runat="server" type="hidden" />
            <input id="hdnMontoOriginal" runat="server" type="Hidden" />
            <input id="hdnauthorization_total_amount" runat="server" type="Hidden" />
            <input id="hdnauthorization_total_bonds" runat="server" type="Hidden" />
            <input id="hdnIdPeriodicity" runat="server" type="hidden" />
            <input id="hdnIdRequestBond" runat="server" type="hidden" />
            <input id="hdnMonto" runat="server" type="hidden" />
            <input id="hdnInitialYear" runat="server" type="hidden" />
            <input id="hdnFinalizeYear" runat="server" type="hidden" />
            <input id="hdnCC_Cargo" runat="server" type="hidden" />
            <input id="hdnIdTypeBonds" runat="server" type="hidden" />
            <input id="hdnFilesRequeried" runat="server" type="hidden" />
            <input id="hdnSubio" runat="server" type="hidden" />
            <input id="hdnEmployeeNumber" runat="server" type="hidden" />
            <asp:Button ID="btncargarempleado" Style="display: none;" OnClick="btncargarempleado_Click" runat="server" Text="Button" />
            <asp:Button ID="lnkcc" Style="display: none;" OnClick="lnkcc_Click" runat="server" Text="Button" />
            <asp:Button ID="lnkproyecto" Style="display: none;" OnClick="lnkproyecto_Click" runat="server" Text="Button" />
            <asp:Button ID="lnkdescargas" Style="display: none;" OnClick="lnkdescargas_Click" runat="server" Text="Button" />
            <asp:Button ID="btnviewrequest" Style="display: none;" OnClick="btnviewrequest_Click" runat="server" Text="Button" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="modal fade bs-example-modal-lg" tabindex="-1" id="modal_archivos" role="dialog"
        aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnviewrequest" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="lnkadjuntarfiles" EventName="Click" />
                    <asp:PostBackTrigger ControlID="lnkdescargas" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Seleccione un archivo</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-file-archive-o" aria-hidden="true"></i>&nbsp;Documento</strong></h5>
                                    <telerik:RadAsyncUpload RenderMode="Lightweight" ID="AsyncUpload1" runat="server"
                                        OnFileUploaded="AsyncUpload1_FileUploaded" PostbackTriggers="lnkguardaresultados"
                                        MaxFileSize="2097152" Width="100%"
                                        AutoAddFileInputs="false" Localization-Select="Seleccionar" Skin="Bootstrap" />
                                </div>
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <br />
                                    <asp:LinkButton OnClientClick="return false;" ID="lnkguardaresultadosload" CssClass=" pull-right btn btn-primary btn-flat btn-sm" runat="server" Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Guardando
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lnkguardaresultados" OnClientClick="return ConfirmLoadResultados('¿Desea guardar el resultado?');"
                                        OnClick="lnkguardaresultados_Click" CssClass="btn btn-primary btn-flat btn-sm pull-right" runat="server">
                                            Guardar documento&nbsp;<i class="fa fa-floppy-o" aria-hidden="true"></i>
                                    </asp:LinkButton>
                                </div>
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <div class="table table-responsive">
                                        <table id="table_archivos" class=" table table-responsive table-bordered table-condensed" 
                                            >
                                            <thead>
                                                <tr>
                                                    <th style="max-width: 10px; text-align: center;" scope="col"></th>
                                                    <th style="max-width: 10px; text-align: center;" scope="col"></th>
                                                    <th style="min-width: 350px; text-align: left;" scope="col">Documento</th>
                                                    <th style="min-width: 150px; text-align: left;" scope="col">Tamaño (megabytes)</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="repeater_archivos" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td style="text-align: center;">
                                                                <asp:LinkButton 
                                                                    OnClientClick="return confirm('¿Desea eliminar este archivo de la solicitud?');" 
                                                                    OnClick="lnkdeletefile_Click"  class="btn btn-primary btn-flat btn-xs"
                                                                     ID="lnkdeletefile" runat="server"
                                                                    file_name='<%# Eval("file_name").ToString().Trim() %>'>
                                                                  Eliminar
                                                                </asp:LinkButton>
                                                            </td>
                                                            <td style="text-align: center;">
                                                                <a style="cursor:pointer;" class="btn btn-success btn-flat btn-xs" 
                                                                    onclick='<%#"return Download("+@""""+ Eval("file_name").ToString().Replace(@"\","/").Trim()+@""""+");" %>'>
                                                                   Descargar</a>
                                                               
                                                            </td>
                                                            <td>
                                                                <%# Eval("file_name").ToString().Trim() %>
                                                            </td>
                                                            <td>
                                                                <%# Math.Round((Convert.ToDecimal(Eval("size"))/1000000),2).ToString()+" mb" %>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
            <asp:HiddenField ID="hdfpath" runat="server" />
                            </div>
                        </div>
                        <div class="modal-footer">

                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
