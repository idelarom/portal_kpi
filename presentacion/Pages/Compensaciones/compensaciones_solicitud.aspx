<%@ Page Title="Solicitud" Language="C#" MasterPageFile="~/Pages/MP/Global.Master" AutoEventWireup="true" CodeBehind="compensaciones_solicitud.aspx.cs" Inherits="presentacion.Pages.Compensaciones.compensaciones_solicitud" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
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
        , scale: 1 // Scales overall size of the spinner
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
        function ChangedTextLoad2() {
            $("#<%= imgloadempleado.ClientID%>").show();
            $("#<%= lblbemp.ClientID%>").show();
            return true;
        }
        function Load() {
            $("#<%= load.ClientID%>").show();
        	var target = document.getElementById('<%= load.ClientID %>');
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
            var txtPMTrackerNumberImplementations = document.getElementById('<%= txtPMTrackerNumberImplementations.ClientID %>');
            txtPMTrackerNumberImplementations.value = folio;
            var txtProjectNameImplementations = document.getElementById('<%= txtProjectNameImplementations.ClientID %>');
            txtProjectNameImplementations.value = proyecto;
            var txtCustomerNameImplementations = document.getElementById('<%= txtCustomerNameImplementations.ClientID %>');
            txtCustomerNameImplementations.value = cliente;
            ModalCloseGlobal("#modal_proyectos");
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
                    <div class="box box-default">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                    <h5><strong><i class="fa fa-pencil-square-o" aria-hidden="true"></i>&nbsp;Tipo de bono</strong></h5>
                                    <asp:DropDownList ID="cbBonds_Types" AutoPostBack="true" 
                                        OnSelectedIndexChanged="cbBonds_Types_SelectedIndexChanged" CssClass=" form-control" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="box box-default">
                        <div class="box-body" id="tblInformationRequisitions" runat="server" visible="false">
                            <div id="load" runat="server" style="display: none;"></div>
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" id="trWeek" runat="server">
                                    <h5><strong><i class="fa fa-calendar-o" aria-hidden="true"></i>&nbsp;Seleccione una fecha de una semana</strong></h5>
                                </div>
                                <div class="col-lg-4 col-md-6 col-sm-8 col-xs-12">
                                    <h5><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Seleccione el empleado</strong>
                                    </h5>
                                    <div class="input-group input-group-sm" runat="server" id="div_filtro_empleados">
                                        <asp:TextBox
                                            onfocus="this.select();" ID="txtfilterempleado" CssClass=" form-control"
                                            placeholder="Ingrese un filtro" runat="server"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <asp:LinkButton ID="lnksearch" CssClass="btn btn-primary btn-flat"
                                                OnClientClick="return ChangedTextLoad2();" OnClick="lnksearch_Click" runat="server">
                                                <i class="fa fa-search" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </span>
                                    </div>
                                    <asp:Image ID="imgloadempleado" Style="display: none;" ImageUrl="~/img/load.gif" runat="server" />
                                    <label id="lblbemp" runat="server" style="display: none; color: #1565c0">Buscando Empleados</label>
                                    <asp:DropDownList Visible="true" ID="ddlempleado" CssClass="form-control"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlempleado_SelectedIndexChanged" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-2 col-md-3 col-sm-4 col-xs-12" id="trFolioPMTracker" runat="server">
                                    <h5><strong><i class="fa fa-ticket" aria-hidden="true"></i>&nbsp;Folio PM Tracker</strong></h5>
                                    <div class="input-group input-group-sm" runat="server">
                                        <asp:TextBox
                                            onfocus="this.select();" ID="txtPMTrackerNumberImplementations" CssClass=" form-control"
                                            ReadOnly="false" runat="server"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <asp:LinkButton ID="lnksearchpmTracker" CssClass="btn btn-primary btn-flat" OnClientClick="return Load();"
                                                OnClick="lnksearchpmTracker_Click" runat="server">
                                                <i class="fa fa-search" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-lg-5 col-md-9 col-sm-8 col-xs-12" id="trProjectName" runat="server">
                                    <h5><strong><i class="fa fa-cubes" aria-hidden="true"></i>&nbsp;Nombre proyecto</strong></h5>
                                    <asp:TextBox ID="txtProjectNameImplementations" CssClass=" form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-lg-5 col-md-12 col-sm-12 col-xs-12" id="trCustomerName" runat="server">
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
                                    <asp:TextBox ID="txtAuthorizationAmount" onfocus="control_clear(this)" AutoPostBack="true"
                                        OnTextChanged="txtAuthorizationAmount_TextChanged"
                                        CssClass=" form-control" runat="server"></asp:TextBox>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-lg-6 col-md-8 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Periodo del bono</strong></h5>
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" id="tblMonthSelect" runat="server">
                                            <div class="row">
                                                <h6><strong><asp:Label ID="lblTitleMonth" runat="server" Text="Mes:"></asp:Label></strong></h6>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                                    <asp:DropDownList ID="cbInitialMonth" CssClass="form-control" AutoPostBack="true"
                                                        OnSelectedIndexChanged="cbInitialMonth_SelectedIndexChanged"
                                                        runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                                    <asp:DropDownList ID="cbInitialYear" CssClass="form-control" AutoPostBack="true"
                                                        OnSelectedIndexChanged="cbInitialMonth_SelectedIndexChanged"
                                                        runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row" id="trFinalizeMonth" runat="server" visible="false">
                                                <h6><strong>Mes final</strong></h6>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                    <asp:DropDownList ID="cbFinalizeMonth" CssClass="form-control"
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
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                                    <telerik:RadDatePicker ID="txtPeriodDateOf" Width="100%" runat="server" Skin="Bootstrap"></telerik:RadDatePicker>
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
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
                                    <div class="input-group input-group-sm" runat="server">

                                        <asp:TextBox ID="txtCC_Cargo" CssClass=" form-control" runat="server"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <asp:LinkButton ID="lnksearchcc" CssClass="btn btn-primary btn-flat"
                                                OnClick="lnksearchcc_Click" runat="server">
                                                <i class="fa fa-search" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-comments" aria-hidden="true"></i>&nbsp;Comentarios</strong></h5>
                                    <asp:TextBox ID="txtComments" TextMode="MultiLine" Rows="3" CssClass=" form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="box box-default">
                        <div class="box-body" id="trGridRequisitions" runat="server" visible="false">
                            <div class="row">
                                <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
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
                                            <table id="table_proyectos" class=" table no-margin table-condensed" style="font-size: 12px">
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
                                                                         onclick='<%# "return ValuesPMTracker("+@""""+ Eval("folio").ToString()+@""""+@","""+ Eval("nombre_proyecto").ToString()+@""""+@","""+ Eval("nombre_cliente").ToString()+@""""+");" %>' >
                                                                        Seleccionar</a>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
