<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MP/Global.Master" AutoEventWireup="true" CodeBehind="compensaciones_empleados.aspx.cs" Inherits="presentacion.Pages.Compensaciones.compensaciones_empleados" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            Init('.dvv');
        });
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
            UnBlockUI();
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
        function Load() {
            BlockUI();
            return true;
        }
        function Load2() {
            BlockUI();
            return true;
        }
        function Load3() {
            BlockUI();
            return true;
        }
       function Empleado_configuracionClick(numEmpleado) {
            var hdfusuario = document.getElementById('<%= hdfnumEmpleado.ClientID %>');
           hdfusuario.value = numEmpleado;
           Load();
            document.getElementById('<%= btnConfiguracion.ClientID%>').click();
            return true;
        }
         function ValuesEmpleado(no_) {
            var hdnEmployeeNumber = document.getElementById('<%= hdfnumEmpleado.ClientID %>');
            hdnEmployeeNumber.value = no_;
            Load();
            document.getElementById('<%= btnConfiguracion.ClientID%>').click();
            ModalCloseGlobal("#modal_empleados");
         }
        function OpenBondsRequisitionsAutomatic() {
            document.getElementById('<%= btnConfiguracionAuto.ClientID%>').click();
        }
        function ConfirmEntregableDelete(Id_request_bond, bond_name, IdBonds, Monto, Periodo, Ocurrencias, OcurrenciasPend, CC_Cargo, Estatus, Comentarios, Fecha_Inicial, Fecha_Final) {
            if (confirm('¿Desea Cancelar esta solicitud?')) {
            var hdfId_request_bond_automatic = document.getElementById('<%= hdfId_request_bond_automatic.ClientID %>');
            var hdfbond_name = document.getElementById('<%= hdfbond_name.ClientID %>');
            var hdfIdBonds = document.getElementById('<%= hdfIdBonds.ClientID %>');
            var hdfMonto = document.getElementById('<%= hdfMonto.ClientID %>');
            var hdfPeriodo = document.getElementById('<%= hdfPeriodo.ClientID %>');
            var hdfOcurrencias = document.getElementById('<%= hdfOcurrencias.ClientID %>');
            var hdfOcurrenciasPend = document.getElementById('<%= hdfOcurrenciasPend.ClientID %>');
            var hdfCC_Cargo = document.getElementById('<%= hdfCC_Cargo.ClientID %>');
            var hdfEstatus = document.getElementById('<%= hdfEstatus.ClientID %>');
            var hdfComentarios = document.getElementById('<%= hdfComentarios.ClientID %>');
            var hdfFecha_Inicial = document.getElementById('<%= hdfFecha_Inicial.ClientID %>');
            var hdfFecha_Final = document.getElementById('<%= hdfFecha_Final.ClientID %>');

            hdfId_request_bond_automatic.value = Id_request_bond;
            hdfbond_name.value = bond_name;
            hdfIdBonds.value = IdBonds;
            hdfMonto.value = Monto;
            hdfPeriodo.value = Periodo;
            hdfOcurrencias.value = Ocurrencias;
            hdfOcurrenciasPend.value = OcurrenciasPend;
            hdfCC_Cargo.value = CC_Cargo;
            hdfEstatus.value = Estatus;
            hdfComentarios.value = Comentarios;
            hdfFecha_Inicial.value = Fecha_Inicial;
            hdfFecha_Final.value = Fecha_Final;
            document.getElementById('<%= btneliminar.ClientID%>').click();
                return true;
            } else {
                return false;
            }
        }
        function EditarClick(Id_request_bond,bond_name,IdBonds,Monto,Periodo,Ocurrencias,OcurrenciasPend,CC_Cargo,Estatus,Comentarios,Fecha_Inicial,Fecha_Final) {
            var hdfId_request_bond_automatic = document.getElementById('<%= hdfId_request_bond_automatic.ClientID %>');
            var hdfbond_name = document.getElementById('<%= hdfbond_name.ClientID %>');
            var hdfIdBonds = document.getElementById('<%= hdfIdBonds.ClientID %>');
            var hdfMonto = document.getElementById('<%= hdfMonto.ClientID %>');
            var hdfPeriodo = document.getElementById('<%= hdfPeriodo.ClientID %>');
            var hdfOcurrencias = document.getElementById('<%= hdfOcurrencias.ClientID %>');
            var hdfOcurrenciasPend = document.getElementById('<%= hdfOcurrenciasPend.ClientID %>');
            var hdfCC_Cargo = document.getElementById('<%= hdfCC_Cargo.ClientID %>');
            var hdfEstatus = document.getElementById('<%= hdfEstatus.ClientID %>');
            var hdfComentarios = document.getElementById('<%= hdfComentarios.ClientID %>');
            var hdfFecha_Inicial = document.getElementById('<%= hdfFecha_Inicial.ClientID %>');
            var hdfFecha_Final = document.getElementById('<%= hdfFecha_Final.ClientID %>');

            hdfId_request_bond_automatic.value = Id_request_bond;
            hdfbond_name.value = bond_name;
            hdfIdBonds.value = IdBonds;
            hdfMonto.value = Monto;
            hdfPeriodo.value = Periodo;
            hdfOcurrencias.value = Ocurrencias;
            hdfOcurrenciasPend.value = OcurrenciasPend;
            hdfCC_Cargo.value = CC_Cargo;
            hdfEstatus.value = Estatus;
            hdfComentarios.value = Comentarios;
            hdfFecha_Inicial.value = Fecha_Inicial;
            hdfFecha_Final.value = Fecha_Final;
            document.getElementById('<%= btneventgrid.ClientID%>').click();
            return false;
        }
        function OnSelect() {
             document.getElementById('<%= btnValidachinchkSelected.ClientID%>').click();
        }
        function ValuesCC(CC, DESC, RESP) {
            var txtCCCAuto = document.getElementById('<%= txtCCCAuto.ClientID %>');
            txtCCCAuto.innerText = CC + " - " + DESC;
            var txtRCCC = document.getElementById('<%= txtRCCC.ClientID %>');
            txtRCCC.innerText = RESP;
            ModalCloseGlobal("#modal_CC");
        }
        function DetalleClick(Id_request_bond) {
            var hdfId_request_bond_automatic = document.getElementById('<%= hdfId_request_bond_automatic.ClientID %>');
            hdfId_request_bond_automatic.value = Id_request_bond;
            document.getElementById('<%= btnDetalle.ClientID%>').click();
            return false;
        }
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="uusus" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <h3>Empleados Compensasiones</h3>
                </div>

                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">

                    <div id="load2" runat="server" style="display: none;"></div>
                   <%-- <div class="box box-danger">--%>
                    <div class="box box-danger" id="tblemployees" runat="server" visible="true">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                    <asp:LinkButton ID="lnknuevoEmpleado" CssClass="btn btn-primary btn-flat" runat="server" OnClick="lnknuevoEmpleado_Click">
                                          Nuevo empleado&nbsp;<i class="fa fa-plus" aria-hidden="true"></i>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="table table-responsive">
                                        <table id="table_empleadosCompensaciones"  class="dvv table table-responsive table-condensed">
                                            <thead>
                                                <tr style="font-size: 12px;">
                                                    <th style="width: 20px; text-align: center;" scope="col"></th>
                                                    <th style="min-width: 15px; text-align: left;" scope="col">Numero de empleado</th>
                                                    <th style="min-width: 200px; text-align: left;" scope="col">Nombre</th>
                                                    <th style="min-width: 200px; text-align: left;" scope="col">Jefe inmediato</th>
                                                    <th style="min-width: 12px; text-align: center;" scope="col">Activo</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="repeat_employees_compensations" runat="server">
                                                    <ItemTemplate>
                                                        <tr style="font-size: 12px">
                                                            <td style="text-align: center;">
                                                                <a style="cursor: pointer;"
                                                                    onclick='<%# "return Empleado_configuracionClick("+Eval("NumEmpleado")+");" %>'>
                                                                    <i class="fa fa-check fa-2x" aria-hidden="true"></i>
                                                                </a>
                                                            </td>
                                                            <td style="text-align: left;"><%# Eval("NumEmpleado") %></td>
                                                            <td style="text-align: left;"><%# Eval("NomFull") %></td>
                                                            <td style="text-align: left;"><%# Eval("JefeInmediato") %></td> 
                                                            <td style="text-align:center;">                                                                  
                                                                 <asp:CheckBox ID="chkIncludePdf" runat="server" Checked='<%# Convert.ToBoolean(Eval("Enabled"))%>' Enabled="false" />
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

                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div id="load" runat="server" style="display: none;"></div>
                    <div class="box box-danger" id="tblInformationEmployeeBonds" runat="server" visible="false">
                        <div class="box-header with-border">
                            <h3 class="box-title">Solicitud de registro de empleado</h3>

                            <div class="box-tools pull-right">
                                <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                    <i class="fa fa-minus"></i>
                                </button>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="row">

                                <div class="col-lg-3 col-md-3 col-sm-4 col-xs-12" id="trCustomerName" runat="server">
                                    <h5><strong><i class="fa fa-address-card-o" aria-hidden="true"></i>&nbsp;Numero de empleado</strong></h5>
                                    <asp:TextBox ID="txtNumEmpleado" CssClass=" form-control" runat="server" ReadOnly="True"></asp:TextBox>
                                </div>
                                <div class="col-lg-4 col-md-5 col-sm-8 col-xs-12">
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
                                
                                <div class="col-lg-3 col-md-3 col-sm-4 col-xs-12" id="Div2" runat="server">
                                    <h5><strong><i class="fa fa-cc" aria-hidden="true"></i>&nbsp;Centro de costos</strong></h5>
                                    <asp:TextBox ID="txtCC" CssClass=" form-control" runat="server" ReadOnly="True"></asp:TextBox>
                                </div>
                                <div class="col-lg-4 col-md-5 col-sm-8 col-xs-12" id="Div1" runat="server">
                                    <h5><strong><i class="fa fa-user-circle-o" aria-hidden="true"></i>&nbsp;Jefe Inmediato</strong></h5>
                                    <asp:TextBox ID="txtJefe" CssClass=" form-control" runat="server" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-3 col-md-3 col-sm-4 col-xs-12" id="Div3" runat="server">
                                    <h5><strong><i class="fa fa-universal-access" aria-hidden="true"></i>&nbsp;Usuario de red</strong></h5>
                                    <asp:TextBox ID="txtUser" CssClass=" form-control" runat="server" ReadOnly="True"></asp:TextBox>
                                </div>
                                <div class="col-lg-4 col-md-5 col-sm-8 col-xs-12" id="Div4" runat="server">
                                    <h5><strong><i class="fa fa-envelope" aria-hidden="true"></i>&nbsp;Email</strong></h5>
                                    <asp:TextBox ID="txtEmail" CssClass=" form-control" runat="server" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-4 col-md-6 col-sm-8 col-xs-12" id="Div5" runat="server">
                                    <h5><strong><i class="fa fa-check-square" aria-hidden="true"></i>&nbsp;Estatus</strong></h5>
                                    <asp:CheckBox ID="chkActivo" CssClass=" form-control" runat="server" Enabled="false" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <br />
                                    <div class="table table-responsive">
                                        <table class="dvv table table-responsive table-condensed table-responsive table-bordered">
                                            <thead>
                                                <tr style="font-size: 12px;">
                                                    <th style="width: 20px; text-align: center;" scope="col"></th>
                                                    <th style="min-width: 15px; text-align: left; display: none" scope="col">id_bond_type</th>
                                                    <th style="min-width: 15px; text-align: left;" scope="col">Tipo de bono</th>
                                                    <th style="min-width: 100px; text-align: left;" scope="col">Monto</th>
                                                    <th style="min-width: 100px; text-align: left;" scope="col">Porcentaje maximo para pago</th>
                                                    <th style="min-width: 12px; text-align: left;" scope="col">Monto maximo</th>
                                                    <th style="min-width: 12px; text-align: left;" scope="col">Periodicidad</th>
                                                    <th style="min-width: 15px; text-align: left; display: none" scope="col" visible="false">amount_required</th>
                                                    <th style="min-width: 15px; text-align: left; display: none" scope="col" visible="false">periodicity_required</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="repeater_Bonds_Definition" runat="server" OnItemDataBound="repeater_Bonds_Definition_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr style="font-size: 12px">
                                                            <td>
                                                                <asp:CheckBox ID="chkSelected" onclick="OnSelect()" runat="server" />
                                                            </td>

                                                            <td style="display: none">
                                                                <asp:Label ID="lblid_bond_type" runat="server" Text='<%# Eval("id_bond_type") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Literal ID="lbtnAuto" runat="server"></asp:Literal>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtPorcentaje" runat="server" OnTextChanged="txtPorcentaje_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtAmountMax" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPeriodicity" runat="server"></asp:DropDownList>
                                                            </td>
                                                            <td style="display: none">
                                                                <asp:Label ID="lblamount_required" runat="server" Text='<%# Eval("amount_required") %>'></asp:Label>
                                                            </td>
                                                            <td style="display: none">
                                                                <asp:Label ID="lblperiodicity_required" runat="server" Text='<%# Eval("periodicity_required") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <asp:LinkButton ID="lnkguardarconfigbond" CssClass="btn btn-primary btn-flat" runat="server" OnClientClick="return confirm('¿Desea guardar esta solicitud?');" OnClick="lnkguardarconfigbond_Click">
                                         <i class="fa fa-bookmark" aria-hidden="true">
                                         </i>&nbsp;Guardar</asp:LinkButton>
                                    <asp:LinkButton ID="lnkcancelarconfigbond" CssClass="btn btn-danger btn-flat" runat="server" OnClick="lnkcancelarconfigbond_Click">
                                          <i class="fa fa-times" aria-hidden="true"></i>&nbsp;Cancelar</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div id="load3" runat="server" style="display: none;"></div>
                    <div class="box box-danger" id="tblInformationEmployeeBondsAuto" runat="server" visible="false">
                        <div class="box-header with-border">
                            <h3 class="box-title">solicitud de registro de empleado en Bonos automaticos</h3>

                            <div class="box-tools pull-right">
                                <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                    <i class="fa fa-minus"></i>
                                </button>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-4 col-md-6 col-sm-8 col-xs-12" id="Div6" runat="server">
                                    <h5><strong><i class="fa fa-user-circle-o" aria-hidden="true"></i>&nbsp;Tipo de Bono Automatico</strong></h5>
                                    <asp:DropDownList ID="ddlBondsType"  CssClass=" form-control" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6col-md-6 col-sm-8 col-xs-12">
                                    <div class="row">
                                        <div class="col-lg-6col-md-6 col-sm-8 col-xs-12">
                                            <div class="row">
                                                <div class="col-lg-6 col-md-6 col-sm-8 col-xs-12">
                                                   <h5><strong><i class="fa fa-address-card-o" aria-hidden="true"></i>&nbsp;Numero de empleado</strong></h5>
                                                    <asp:TextBox ID="txtNumEmpleadoAuto" CssClass=" form-control" runat="server" ReadOnly="True"></asp:TextBox>
                                                </div>  
                                                <div class="col-lg-6 col-md-6 col-sm-8 col-xs-12">
                                                     <h5><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Empleado</strong></h5>
                                                    <asp:TextBox ID="txtNombEmpleadoAuto" CssClass=" form-control" runat="server" ReadOnly="True"></asp:TextBox>
                                                </div>                                              
                                            </div>
                                        </div>
                                    </div>                                    
                                </div>
                             </div>                             
                             <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-8 col-xs-12">
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-sm-8 col-xs-12">
                                            <div class="row">
                                                <div class="col-lg-12 col-md-6 col-sm-8 col-xs-12">
                                                    <h5><strong><i class="fa fa-usd" aria-hidden="true"></i>&nbsp;Monto Maximo a Solicitar</strong></h5>
                                                    <asp:TextBox ID="txtMonto" CssClass=" form-control" runat="server" OnTextChanged="txtMonto_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-8 col-xs-12">
                                            <div class="row">
                                                <div class="col-lg-6 col-md-6 col-sm-8 col-xs-12">
                                                    <h5><strong><i class="fa fa-calendar-minus-o" aria-hidden="true"></i>&nbsp;Periodicidad</strong></h5>
                                                    <asp:DropDownList ID="ddlPeriodicidad" CssClass=" form-control" runat="server" Enabled="false"></asp:DropDownList>
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-sm-8 col-xs-12">
                                                    <h5><strong><i class="fa fa-sort-numeric-asc" aria-hidden="true"></i>&nbsp;Ocurrencias</strong></h5>
                                                    <asp:DropDownList ID="ddlOcurrencias" CssClass=" form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlOcurrencias_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>                                    
                                </div>
                             </div>
                             <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-8 col-xs-12">
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-sm-8 col-xs-12">
                                              <h5><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Inicial</strong></h5>
                                            <div class="row">
                                                <div class="col-lg-6 col-md-6 col-sm-8 col-xs-12">
                                                    <asp:DropDownList ID="ddlMesInicial"  CssClass=" form-control" runat="server" Enabled="false"></asp:DropDownList>
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-sm-8 col-xs-12">
                                                     <asp:DropDownList ID="ddlAnoInicial"  CssClass=" form-control" runat="server" Enabled="false"></asp:DropDownList>
                                                </div>
                                            </div>
                                         </div>
                                         <div class="col-lg-6 col-md-6 col-sm-8 col-xs-12">
                                              <h5><strong><i class="fa fa-calendar" aria-hidden="true"></i>&nbsp;Fecha Final</strong></h5>
                                             <div class="row">
                                                <div class="col-lg-6 col-md-6 col-sm-8 col-xs-12">
                                                    <asp:DropDownList ID="ddlMesFinal"  CssClass=" form-control" runat="server" Enabled="false"></asp:DropDownList>
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-sm-8 col-xs-12">
                                                    <asp:DropDownList ID="ddlAnoFinal"  CssClass=" form-control" runat="server" Enabled="false"></asp:DropDownList>
                                                </div>
                                            </div>
                                         </div>   
                                    </div>                          
                                </div>
                            </div>
                             <div class="row">
                                 <div class="col-lg-6 col-md-6 col-sm-8 col-xs-12">
                                     <div class="row">
                                          <div class="col-lg-6 col-md-6 col-sm-8 col-xs-12">
                                            <h5><strong><i class="fa fa-cc" aria-hidden="true"></i>&nbsp;Centro de costos</strong></h5>
                                            <asp:TextBox ID="txtCCAuto" CssClass=" form-control" runat="server" ReadOnly="True"></asp:TextBox>
                                        </div>
                                         <div class="col-lg-6 col-md-6 col-sm-8 col-xs-12">
                                             <h5><strong><i class="fa fa-cc" aria-hidden="true"></i>&nbsp;Centro de costos del cargo</strong></h5>
                                             <div class="input-group" runat="server" id="div8">
                                                 <asp:TextBox ID="txtCCCAuto" CssClass=" form-control" ReadOnly="true"
                                                     runat="server"></asp:TextBox>
                                                 <span class="input-group-btn">
                                                     <asp:LinkButton ID="lnksearchCCC" CssClass="btn btn-primary btn-flat"
                                                         OnClientClick="return Load();" OnClick="lnksearchCCC_Click" runat="server">
                                                <i class="fa fa-search" aria-hidden="true"></i>
                                                     </asp:LinkButton>
                                                 </span>
                                             </div>
                                         </div>
                                         <div class="col-lg-12 col-md-6 col-sm-8 col-xs-12">
                                             <h5><strong><i class="fa fa-cc" aria-hidden="true"></i>&nbsp;Responsable del Centro de costos del cargo</strong></h5>
                                             <asp:TextBox ID="txtRCCC" CssClass=" form-control" runat="server" ReadOnly="True"></asp:TextBox>
                                         </div>
                                     </div>
                                     
                               </div>
                            </div>
                             <div class="row">
                                <div class="col-lg-4 col-md-6 col-sm-8 col-xs-12">
                                    <h5><strong><i class="fa fa-sticky-note" aria-hidden="true"></i>&nbsp;Comentarios</strong></h5>
                                    <asp:TextBox ID="txtComtarios" CssClass=" form-control" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                </div>
                            </div>
                              <div class="row">
                                <div class="col-lg-4 col-md-6 col-sm-8 col-xs-12" id="div_comentscancel" runat="server" visible="false">
                                    <h5><strong><i class="fa fa-sticky-note" aria-hidden="true"></i>&nbsp;Comentarios de la cancelacion</strong></h5>
                                    <asp:TextBox ID="txtcomentarioscancela" CssClass=" form-control" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <asp:LinkButton ID="lnksolicitaryguardar" CssClass="btn btn-primary btn-flat" runat="server" OnClientClick="return confirm('¿Desea guardar esta solicitud?');"
                                        OnClick="lnksolicitaryguardar_Click">
                                         <i class="fa fa-bookmark" aria-hidden="true">
                                         </i>&nbsp;Solicitar</asp:LinkButton>
                                    <asp:LinkButton ID="lnkcancelar" OnClick="lnkcancelar_Click" OnClientClick="return confirm('¿Desea cancelar la solicitud?');" CssClass="btn btn-warning btn-flat" runat="server">
                                          <i class="fa fa-times" aria-hidden="true"></i>&nbsp;Cancelar edicion</asp:LinkButton>
                                    <asp:LinkButton ID="lnkcancelarSol" OnClick="lnkcancelarSol_Click" OnClientClick="return confirm('¿Desea cancelar la solicitud?');" CssClass="btn btn-danger btn-flat" runat="server" Visible="False">
                                          <i class="fa fa-times" aria-hidden="true"></i>&nbsp;Cancelar Solicitud</asp:LinkButton>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="table table-responsive">
                                        <table class="dvv table table-responsive table-condensed table-bordered">
                                            <thead>
                                                <tr style="font-size: 12px;">
                                                    <th style="width: 20px; text-align: center;" scope="col"></th>
                                                    <th style="width: 20px; text-align: center;" scope="col"></th>
                                                    <th style="text-align: center;" scope="col" class="auto-style1"></th>
                                                    <th style="min-width: 15px; text-align: left;" scope="col">Solicitud</th>
                                                    <th style="min-width: 15px; text-align: left;" scope="col">Tipo de Bono</th>
                                                    <th style="min-width: 15px; text-align: left;" scope="col">Monto</th>
                                                    <th style="min-width: 15px; text-align: left;" scope="col">Periodo</th>
                                                    <th style="min-width: 15px; text-align: left;" scope="col">Total de ocurrencias</th>
                                                    <th style="min-width: 15px; text-align: left;" scope="col">Ocurrencias pendientes</th>
                                                    <th style="min-width: 15px; text-align: left;" scope="col">Centro de costo del cargo</th>
                                                    <th style="min-width: 15px; text-align: left;" scope="col">Estatus</th>
                                                    <th style="min-width: 15px; text-align: left;" scope="col">Fecha inicial</th>
                                                    <th style="min-width: 15px; text-align: left;" scope="col">Fecha final</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="repeater_solicitud_auto" runat="server">
                                                    <ItemTemplate>
                                                        <tr style="font-size: 12px">
                                                             <td style="text-align: center;">
                                                                <a style="cursor: pointer;"
                                                                    onclick='<%# "return DetalleClick("+@""""+ Eval("Id_request_bond").ToString()+@""""+");" %>'>
                                                                    <i class="fa fa-arrows fa-2x" aria-hidden="true"></i>
                                                                </a>
                                                            </td>
                                                            <td style="text-align: center;">
                                                                <a style="cursor: pointer;"
                                                                    onclick='<%# "return EditarClick("+@""""+ Eval("Id_request_bond").ToString()+@""""+@","""+ Eval("bond_name").ToString()+@""""+@","""+ Eval("IdBonds").ToString()+@""""+@","""+ Eval("Monto").ToString()+@""""+@","""+ Eval("Periodo").ToString()+@""""+@","""+ Eval("Ocurrencias").ToString()+@""""+@","""+ Eval("OcurrenciasPend").ToString()+@""""+@","""+ Eval("CC_Cargo").ToString()+@""""+@","""+ Eval("Estatus").ToString()+@""""+@","""+ Eval("Comentarios").ToString()+@""""+@","""+ Eval("Fecha_Inicial").ToString()+@""""+@","""+ Eval("Fecha_Final").ToString()+@""""+");" %>'>
                                                                    <i class="fa fa-pencil fa-2x" aria-hidden="true"></i>
                                                                </a>
                                                            </td>
                                                            <td style="text-align: center;">
                                                                <a style="cursor: pointer;"
                                                                    onclick='<%# "return ConfirmEntregableDelete("+@""""+ Eval("Id_request_bond").ToString()+@""""+@","""+ Eval("bond_name").ToString()+@""""+@","""+ Eval("IdBonds").ToString()+@""""+@","""+ Eval("Monto").ToString()+@""""+@","""+ Eval("Periodo").ToString()+@""""+@","""+ Eval("Ocurrencias").ToString()+@""""+@","""+ Eval("OcurrenciasPend").ToString()+@""""+@","""+ Eval("CC_Cargo").ToString()+@""""+@","""+ Eval("Estatus").ToString()+@""""+@","""+ Eval("Comentarios").ToString()+@""""+@","""+ Eval("Fecha_Inicial").ToString()+@""""+@","""+ Eval("Fecha_Final").ToString()+@""""+");" %>'>
                                                                    <i class="fa fa-trash fa-2x" aria-hidden="true"></i>
                                                                </a>
                                                            </td>
                                                            <td style="text-align: left;"><%# Eval("Id_request_bond") %></td>
                                                            <td style="text-align: left;"><%# Eval("bond_name") %></td>
                                                            <td style="text-align: left;"><%# Eval("Monto") %></td> 
                                                            <td style="text-align: left;"><%# Eval("Periodo") %></td>
                                                            <td style="text-align: left;"><%# Eval("Ocurrencias") %></td>
                                                            <td style="text-align: left;"><%# Eval("OcurrenciasPend") %></td> 
                                                            <td style="text-align: left;"><%# Eval("CC_Cargo") %></td>
                                                            <td style="text-align: left;"><%# Eval("Estatus") %></td>
                                                            <td style="text-align: left;"><%# Eval("Fecha_Inicial") %></td> 
                                                            <td style="text-align: left;"><%# Eval("Fecha_Final") %></td>
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
                                                    <th style="min-width: 50px;max-width: 50px; text-align: left;" scope="col"></th>
                                                    <th style="min-width: 350px; text-align: left;" scope="col">Empleado</th>
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
                                                                    onclick='<%# "return ValuesEmpleado("+@""""+ Eval("NumEmpleado").ToString()+@""""+");" %>'>Seleccionar</a>
                                                            </td>
                                                            <td>
                                                                <%# Eval("NomFull") %>
                                                            </td>
                                                            <td style="min-width: 60px; text-align: center;">
                                                                <%# Eval("NumEmpleado") %>
                                                            </td>
                                                            <td style="min-width: 50px; text-align: center;">
                                                                <%# Eval("cc") %>
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

            <div class="modal fade bs-example-modal-lg" tabindex="-1" id="modal_CC" role="dialog"
                aria-labelledby="mySmallModalLabel">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Seleccione un centro de costos</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class=" table table-responsive">
                                        <table id="table_cc" class=" table table-responsive table-bordered table-condensed" style="font-size: 12px">
                                            <thead>
                                                <tr>
                                                    <th style="min-width: 50px;max-width: 50px; text-align: left;" scope="col"></th>
                                                    <th style="min-width: 350px; text-align: left;" scope="col">Codigo</th>
                                                    <th style="min-width: 60px; text-align: center;" scope="col">Descripcion</th>
                                                    <th style="min-width: 60px; text-align: center; display:none" scope="col">Responsable</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="repeater_cc" runat="server">
                                                    <ItemTemplate>
                                                        <tr>                                                           
                                                            <td>
                                                                <a class="btn btn-primary btn-flat btn-xs"
                                                                    onclick='<%# "return ValuesCC("+@""""+ Eval("CC").ToString()+@""""+@","""+ Eval("Descripcion").ToString()+@""""+@","""+ Eval("ResponsableCC").ToString()+@""""+");" %>'>Seleccionar</a>
                                                            </td>
                                                            <td>
                                                                <%# Eval("CC") %>
                                                            </td>
                                                            <td style="min-width: 60px; text-align: center;">
                                                                <%# Eval("Descripcion") %>
                                                            </td>
                                                            <td style="min-width: 50px; text-align: center; display:none">
                                                                <%# Eval("ResponsableCC") %>
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

            <div class="modal fade bs-example-modal-lg" tabindex="-1" id="modal_Detalle_sol" role="dialog"
                aria-labelledby="mySmallModalLabel">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Detalles del la solicitud seleccionada</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class=" table table-responsive">
                                        <table id="table_detalle_sol" class=" table table-responsive table-bordered table-condensed" style="font-size: 12px">
                                            <thead>
                                                <tr>
                                                    <th style="min-width: 350px; text-align: left;" scope="col">Realizado por</th>
                                                    <th style="min-width: 60px; text-align: center;" scope="col">Fecha del estatus</th>
                                                    <th style="min-width: 60px; text-align: center;" scope="col">Estatus</th>
                                                    <th style="min-width: 60px; text-align: center;"" scope="col">Comentarios</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="repeater_Detalle_sol" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td style="min-width: 60px; text-align: center;">
                                                                <%# Eval("Solicitante") %>
                                                            </td>
                                                            <td style="min-width: 50px; text-align: center;">
                                                                <%# Eval("Fecha") %>
                                                            </td>
                                                            <td style="min-width: 60px; text-align: center;">
                                                                <%# Eval("Estatus") %>
                                                            </td>
                                                            <td style="min-width: 50px; text-align: center;">
                                                                <%# Eval("Comentarios") %>
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
            
            <asp:HiddenField ID="hdfnumEmpleado" runat="server" />
            <asp:Button ID="btnConfiguracion" OnClick="btnConfiguracion_Click" runat="server" Text="Button" Style="display: none;" />
             <asp:Button ID="btnConfiguracionAuto" OnClick="btnConfiguracionAuto_Click" runat="server" Text="Button" Style="display: none;" />
             <asp:HiddenField ID="hdfCantBondsAuto" runat="server" />
             <asp:Button ID="btnDetalle" OnClick="btnDetalle_Click" runat="server" Text="Button" Style="display: none;" />
             <asp:Button ID="btneventgrid" OnClick="btneventgrid_Click" runat="server" Text="Button" Style="display: none;" />
             <asp:Button ID="btneliminar" OnClick="btneliminar_Click" runat="server" Text="Button" Style="display: none;" />             
             <asp:Button ID="btnValidachinchkSelected" OnClick="btnValidachinchkSelected_Click" runat="server" Text="Button" Style="display: none;" />
             <asp:HiddenField ID="hdfComandAutomaticos" runat="server" />
            <asp:HiddenField ID="hdfId_request_bond_automatic" runat="server" />
            <asp:HiddenField ID="hdfFechaFinalAuto" runat="server" />
            <asp:HiddenField ID="hdfbond_name" runat="server" />
            <asp:HiddenField ID="hdfIdBonds" runat="server" />
            <asp:HiddenField ID="hdfMonto" runat="server" />
            <asp:HiddenField ID="hdfPeriodo" runat="server" />
            <asp:HiddenField ID="hdfOcurrencias" runat="server" />
            <asp:HiddenField ID="hdfOcurrenciasPend" runat="server" />
            <asp:HiddenField ID="hdfCC_Cargo" runat="server" />
            <asp:HiddenField ID="hdfEstatus" runat="server" />
            <asp:HiddenField ID="hdfComentarios" runat="server" />
            <asp:HiddenField ID="hdfFecha_Inicial" runat="server" />
            <asp:HiddenField ID="hdfFecha_Final" runat="server" />

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
