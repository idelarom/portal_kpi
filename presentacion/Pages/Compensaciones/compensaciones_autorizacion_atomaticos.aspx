<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MP/Global.Master" AutoEventWireup="true" CodeBehind="compensaciones_autorizacion_atomaticos.aspx.cs" Inherits="presentacion.Pages.Compensaciones.compensaciones_autorizacion_atomaticos" %>
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
                    "lengthChange": true,
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
        function ConfirmLoadSave(msg) {
            if (confirm(msg)) {

                $("#<%= load_modal.ClientID%>").show();
                var target = document.getElementById('<%= load_modal.ClientID %>');
                var spinner = new Spinner(opts).spin(target);
                return true;
            }
            else {
                return false;
            }
        }
        function Load2() {
           $("#<%= load2.ClientID%>").show();
            var target = document.getElementById('<%= load2.ClientID %>');
            var spinner = new Spinner(opts).spin(target)
            BlockUI();
            return true;
        }
        function ViewBond(id_bond) {
            var hdnIdRequestBond = document.getElementById('<%= hdnIdRequestBond.ClientID %>');
            hdnIdRequestBond.value = id_bond;
            document.getElementById('<%= btnviewrequest.ClientID%>').click();
            Load2();
        }
        function ProcessBond(id_bond, id_tipo_bono) {
            var hdnIdRequestBond = document.getElementById('<%= hdnIdRequestBond.ClientID %>');
            hdnIdRequestBond.value = id_bond;
            var hdnid_tipo_bono = document.getElementById('<%= hdnid_tipo_bono.ClientID %>');
            hdnid_tipo_bono.value = id_tipo_bono;
            document.getElementById('<%= btnprocessrequest.ClientID%>').click();
            Load2();
        }
        <%--function Download(id_bond, path) {
            var hdnIdRequestBond = document.getElementById('<%= hdnIdRequestBond.ClientID %>');
            hdnIdRequestBond.value = id_bond;
            var hdfpath = document.getElementById('<%= hdfpath.ClientID %>');
            hdfpath.value = path;
            console.log(path);
            document.getElementById('<%= lnkdescargas.ClientID%>').click();
        }--%>

       <%-- function ConfirmLoadResultados(msg) {
            if (confirm(msg)) {
                $("#<%= lnkguardaresultadosload.ClientID%>").show();
                $("#<%= lnkguardaresultados.ClientID%>").hide();
                return true;
            }
            else {
                return false;
            }
        }--%>
        function LoadAmount() {
            $("#<%= amount_correct.ClientID%>").hide();
            $("#<%= amount_error.ClientID%>").hide();
            $("#<%= amount_load.ClientID%>").show();
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="uusus" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <h3>Autorización de bonos automaticos</h3>
                </div>
             <%--   <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="box box-danger">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                    <h5><strong><i class="fa fa-pencil-square-o" aria-hidden="true"></i>&nbsp;Tipo de bono</strong></h5>
                                    <asp:DropDownList ID="cbBonds_Types" AutoPostBack="true" onchange="BlockUI();"
                                        OnSelectedIndexChanged="cbBonds_Types_SelectedIndexChanged" CssClass=" form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>--%>

                
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="box box-danger" id="trGridRequisitions" runat="server" visible="true">
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
                                 <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                    <asp:LinkButton ID="lnkAutorizartodos" CssClass="btn btn-primary btn-flat" runat="server" OnClick="lnkAutorizartodos_Click">
                                          Autorizar todos&nbsp;<i class="fa fa-plus" aria-hidden="true"></i>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <div class=" table table-responsive">

                                        <div id="load2" runat="server" style="display: none;"></div>
                                        <table id="table_bonos" class=" table table-responsive table-bordered table-condensed"
                                            style="font-size: 12px">
                                            <thead>
                                                <tr>
                                                    <th style="max-width: 20px; text-align: left;" scope="col"></th>
                                                    <th style="min-width: 30px; text-align: center;" scope="col"># Sol.</th>
                                                    <th style="min-width: 90px; text-align: center;" scope="col">Fecha</th>
                                                    <th style="min-width: 50px; text-align: center;" scope="col">Tipo</th>
                                                    <th style="min-width: 180px; text-align: left;" scope="col">Empleado</th>
                                                    <th style="min-width: 60px; text-align: center;" scope="col">Monto</th>
                                                    <th style="min-width: 150px; text-align: left;" scope="col">CC Cargo</th>
                                                    <th style="min-width: 180px; text-align: left;" scope="col">Solicitante</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="gridBondsRequisitions" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <a class="btn btn-primary btn-flat btn-xs"
                                                                    onclick='<%# "return ProcessBond("+ Eval("id_request_bond").ToString()+","+Eval("id_bond_type").ToString()+");" %>'>Autorizar
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
                                                            <td style="font-size:10px">
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
           
            <asp:HiddenField ID="hdfguid" runat="server" />
            <input id="hdnMontoOriginal" runat="server" type="Hidden" />
            <input id="hdnauthorization_total_amount" runat="server" type="Hidden" />
            <input id="hdnauthorization_total_bonds" runat="server" type="Hidden" />
            <input id="hdnEmployeeNumber" runat="server" type="hidden" />
            <input id="hdnid_tipo_bono" runat="server" type="hidden" />
            <input id="hdnIdRequestBond" runat="server" type="hidden" />
            <asp:Button ID="btnprocessrequest" Style="display: none;" OnClick="btnprocessrequest_Click" runat="server" Text="Button" />
            <asp:Button ID="btnviewrequest" Style="display: none;" runat="server" Text="Button" />
            <asp:Button ID="lnkdescargas" Style="display: none;" runat="server" Text="Button" />
        </ContentTemplate>
    </asp:UpdatePanel>
     
    <div class="modal fade bs-example-modal-lg" tabindex="-1" id="modal_bonos" role="dialog"
        aria-labelledby="mySmallModalLabel">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="txtAuthorizationAmount" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="btnprocessrequest"  EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Detalles de la solicitud</h4>
                        </div>
                        <div class="modal-body">
                            <div id="load_modal" runat="server" style="display:none;"></div>
                            <div class="row">
                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-5">
                                    <h5><strong><i class="fa fa-pencil-square-o" aria-hidden="true"></i>&nbsp;Solicitud</strong></h5>
                                    <asp:Label ID="txtRequisitionNumber" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-7">
                                    <h5><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha</strong></h5>
                                    <asp:Label ID="txtRequisitionDate" runat="server" Text=""></asp:Label>
                                </div>

                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                    <h5><strong><i class="fa fa-pencil-square-o" aria-hidden="true"></i>&nbsp;Tipo de bono</strong></h5>
                                    <asp:Label ID="txtBondType" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Empleado</strong>
                                    </h5>
                                    <asp:Label ID="txtEmployeeName" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-calendar-o" aria-hidden="true"></i>&nbsp;Periodo del bono</strong>
                                    </h5>
                                    <div class="row">
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-6">
                                            <telerik:RadDatePicker ID="txtPeriodDateOf" Enabled="false" Width="100%" runat="server" Skin="Bootstrap"></telerik:RadDatePicker>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-6">
                                            <telerik:RadDatePicker ID="txtPeriodDateTo" Enabled="false" Width="100%" runat="server" Skin="Bootstrap"></telerik:RadDatePicker>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12" id="trAuthorizationAmount" runat="server">
                                    <h5><strong><i class="fa fa-money" aria-hidden="true"></i>&nbsp;Monto autorizado</strong></h5>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtAuthorizationAmount" onfocus="control_clear(this);" onchange="LoadAmount();" AutoPostBack="true"
                                            CssClass=" form-control" runat="server"></asp:TextBox>
                                        <span id="amount_correct" visible="false" style="border-color: Green; color: Green;" runat="server" class="input-group-addon">
                                            <i class="fa fa-check"></i></span>
                                        <span id="amount_error" visible="false" runat="server" style="border-color: Red; color: red;" class="input-group-addon">
                                            <i class="fa fa-times"></i></span>
                                        <span id="amount_load" runat="server" style="display: none; border-color: Blue; color: Blue;" class="input-group-addon">
                                            <i class="fa fa-spinner fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>

                                        </span>
                                    </div>

                                </div>
                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                                    <h5><strong><i class="fa fa-briefcase" aria-hidden="true"></i>&nbsp;CC Empleado</strong></h5>
                                    <asp:TextBox ID="txtCC_Emp" ReadOnly="true" CssClass=" form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                                    <h5><strong><i class="fa fa-briefcase" aria-hidden="true"></i>&nbsp;CC Cargo</strong></h5>
                                    <asp:TextBox ID="txtCC_Cargo" ReadOnly="true" CssClass=" form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-comments" aria-hidden="true"></i>&nbsp;Comentarios de la solicitud</strong></h5>
                                    <asp:TextBox ID="txtComments" ReadOnly="true" TextMode="MultiLine" Rows="2" CssClass=" form-control" runat="server"></asp:TextBox>
                                </div>

                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-comments-o" aria-hidden="true"></i>&nbsp;Comentarios de autorización</strong></h5>
                                    <asp:TextBox ID="tdAuthorizationComments" TextMode="MultiLine" Rows="2" CssClass=" form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer" style="text-align:right   ">
                            <asp:LinkButton ID="lnkrechazarsolicitud" CssClass="btn btn-flat btn-danger"
                                OnClientClick="return ConfirmLoadSave('¿Desea rechazar esta solicitud?');" OnClick="lnkrechazarsolicitud_Click"  
                                runat="server"><i class="fa fa-times" aria-hidden="true"></i>&nbsp;Rechazar</asp:LinkButton>
                            <asp:LinkButton ID="lnkautorizar" CssClass="btn btn-flat btn-primary"
                                OnClientClick="return ConfirmLoadSave('¿Desea autorizar esta solicitud?');" OnClick="lnkautorizar_Click"  
                                runat="server"><i class="fa fa-check" aria-hidden="true"></i>&nbsp;Autorizar</asp:LinkButton>

                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="modal fade bs-example-modal-lg" tabindex="-1" id="modal_bonos_todos" role="dialog"
        aria-labelledby="mySmallModalLabel">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers>
                   <%-- <asp:AsyncPostBackTrigger ControlID="txtAuthorizationAmount" EventName="TextChanged" />--%>
                    <asp:AsyncPostBackTrigger ControlID="lnkautorizartodoss"  EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Detalles de la solicitud</h4>
                        </div>
                        <div class="modal-body">
                            <div id="Div1" runat="server" style="display: none;"></div>
                            <div class="row">                                
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <h5><strong><i class="fa fa-comments-o" aria-hidden="true"></i>&nbsp;Comentarios de autorización</strong></h5>
                                    <asp:TextBox ID="txtCommentstodos" TextMode="MultiLine" Rows="2" CssClass=" form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer" style="text-align:right   ">
                            <asp:LinkButton ID="lnkrechazartodos" CssClass="btn btn-flat btn-danger"
                                OnClientClick="return ConfirmLoadSave('¿Desea rechazar esta solicitud?');" OnClick="lnkrechazartodos_Click"  
                                runat="server"><i class="fa fa-times" aria-hidden="true"></i>&nbsp;Rechazar todos</asp:LinkButton>
                            <asp:LinkButton ID="lnkautorizartodoss" CssClass="btn btn-flat btn-primary"
                                OnClientClick="return ConfirmLoadSave('¿Desea autorizar esta solicitud?');" OnClick="lnkautorizartodoss_Click"
                                runat="server"><i class="fa fa-check" aria-hidden="true"></i>&nbsp;Autorizar todos</asp:LinkButton>

                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

  <%--  <div class="modal fade bs-example-modal-lg" tabindex="-1" id="modal_archivos" role="dialog"
        aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnviewrequest" EventName="Click" />
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
                                    <asp:LinkButton OnClientClick="return false;" ID="lnkguardaresultadosload" CssClass="btn btn-primary btn-flat pull-right" runat="server" Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Guardando
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lnkguardaresultados"
                                        OnClientClick="return ConfirmLoadResultados('¿Desea guardar el resultado?');"
                                        OnClick="lnkguardaresultados_Click" CssClass="btn btn-primary btn-flat pull-right" runat="server">
                                            Guardar documento&nbsp;<i class="fa fa-floppy-o" aria-hidden="true"></i>
                                    </asp:LinkButton>
                                </div>
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <div class="table table-responsive">
                                        <table id="table_archivos" class=" table table-responsive table-bordered table-condensed"
                                            style="font-size: 12px;">
                                            <thead>
                                                <tr>
                                                    <th style="max-width: 10px; text-align: center;" scope="col"></th>
                                                    <th style="max-width: 10px; text-align: center;" scope="col"></th>
                                                    <th style="min-width: 200px; text-align: left;" scope="col">Documento</th>
                                                    <th style="min-width: 90px; text-align: center;" scope="col">Tamaño</th>
                                                    <th style="min-width: 240px; text-align: left;" scope="col">Fecha de carga</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="repeater_archivos" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td style="text-align: center;">
                                                                <asp:LinkButton
                                                                    OnClientClick="return confirm('¿Desea eliminar este archivo de la solicitud?');"
                                                                    OnClick="lnkdeletefile_Click" class="btn btn-primary btn-flat btn-xs"
                                                                    ID="lnkdeletefile" runat="server"
                                                                    file_name='<%# Eval("file_name").ToString().Trim() %>'>
                                                                  Eliminar
                                                                </asp:LinkButton>
                                                            </td>
                                                            <td style="text-align: center;">
                                                                <a style="cursor: pointer;" class="btn btn-success btn-flat btn-xs"
                                                                    onclick='<%#"return Download("+Eval("id_request_bond").ToString()+@""""+ Eval("file_name").ToString().Replace(@"\","/").Trim()+@""""+");" %>'>Descargar</a>

                                                            </td>
                                                            <td>
                                                                <%# Eval("file_name").ToString().Trim() %>
                                                            </td>
                                                            <td style="text-align: center;">
                                                                <%# Math.Round((Convert.ToDecimal(Eval("size"))/1000000),2).ToString()+" mb" %>
                                                            </td>
                                                            <td>
                                                                <%# Convert.ToDateTime(Eval("date_attach")).ToString("dddd dd MMMM, yyyy h:mm:ss tt", System.Globalization.CultureInfo.CreateSpecificCulture("es-MX")) %>
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
    </div>--%>
</asp:Content>