<%@ Page Title="Pagos" Language="C#" MasterPageFile="~/Pages/MP/Global.Master" AutoEventWireup="true" CodeBehind="compensaciones_pagos.aspx.cs" Inherits="presentacion.Pages.Compensaciones.compensaciones_pagos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript">
        function Init(value) {
            if ($.fn.dataTable.isDataTable(value)) {
                table = $(value).DataTable();

            }
            else {
                $(value).DataTable({
                    "paging": true,
                    "pageLength": 100,
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

        function CheckedChanged()
        {
            Load();
            document.getElementById('<%= btncheckedchanged.ClientID%>').click();
            return true;
        }
        function ViewDesgloceBonos(num_empleado)
        {
            Load();            
            var hdf_numemployee = document.getElementById('<%= hdf_numemployee.ClientID %>');
            hdf_numemployee.value = num_empleado;
            document.getElementById('<%= btnview.ClientID%>').click();
        }
        function ConfirmLoadSave(msg) {
            if (confirm(msg)) {
                Load();
                return true;
            }
            else {
                return false;
            }
        }

        function Load() {
            BlockUI();
            return true;
        }
        function printDiv(divName) {
            Load();
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;

            document.body.innerHTML = printContents;

            window.print();

            document.body.innerHTML = originalContents;
            window.location.href = "compensaciones_pagos.aspx";
        }
        function AmountChanged(control, max_amount) {
            // Create our number formatter.
            var formatter = new Intl.NumberFormat('en-US', {
                style: 'currency',
                currency: 'USD',
                minimumFractionDigits: 2,
                // the default value for minimumFractionDigits depends on the currency
                // and is usually already 2
            });

            var amount = control.value;
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
            if (amount=="") {
                control.value = formatter.format(max_amount);

            } else if (amount > max_amount) {
                control.value =  formatter.format(max_amount);
                Command: toastr["error"]("El monto no puede ser mayor a " + formatter.format(max_amount), "Mensaje del sistema");
            } else if (amount == 0) {
                control.value = formatter.format(max_amount);
                Command: toastr["error"]("El monto debe ser mayor a $ 0.00", "Mensaje del sistema");   
            } else {
                control.value = formatter.format(amount);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="uusus" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkexportar" />
            <asp:PostBackTrigger ControlID="lnkimprimir" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <h3>Pago de bonos</h3>
                </div>

                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="box box-danger" id="trGridRequisitions" runat="server" visible="true">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <asp:LinkButton ID="lnkprocesar" CssClass="btn btn-primary btn-flat btn-sm"
                                         OnClientClick="return ConfirmLoadSave('¿Desea procesar todos los bonos seleccionados?');" 
                                        OnClick="lnkprocesar_Click" runat="server">
                                        <i class="fa fa-exclamation" aria-hidden="true"></i>&nbsp;Procesar</asp:LinkButton>

                                    <asp:LinkButton ID="lnkexportar" CssClass="btn btn-success btn-flat btn-sm" runat="server"
                                         OnClick="lnkexportar_Click">
                                        <i class="fa fa-file-excel-o" aria-hidden="true"></i>&nbsp;Exportar</asp:LinkButton>

                                    <asp:LinkButton ID="lnkimprimir" CssClass="btn btn-danger btn-flat btn-sm" runat="server"
                                         OnClientClick="printDiv('div_bonos');">
                                        <i class="fa fa-print" aria-hidden="true"></i>&nbsp;Imprimir</asp:LinkButton>

                                    <div class=" table table-responsive" id="div_bonos">
                                        <table id="table_bonos" class="table table-responsive table-bordered table-condensed"
                                            style=" font-size:12px;">
                                            <thead>
                                                <tr>
                                                    <th style="max-width: 20px; text-align: left;" scope="col"></th>
                                                    <th style="min-width:100px; text-align: center;" scope="col">
                                                        <asp:CheckBox ID="cbxseleccionartodo"
                                                          onclick="return CheckedChanged();" 
                                                            Text="Todo" runat="server" /></th>
                                                    <th style="min-width: 220px; text-align: left;" scope="col">Empleado</th>
                                                    <th style="min-width: 100px; text-align: right;" scope="col">$ Autorizado</th>
                                                    <th style="min-width: 100px; text-align: right;" scope="col">$ Pagado</th>
                                                    <th style="min-width: 100px; text-align: right;" scope="col">$ A pagar</th>
                                                    <th style="min-width: 180px; text-align: center;" scope="col">Comentarios</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="gridBondsPayments" OnItemDataBound="gridBondsPayments_ItemDataBound" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td style="text-align:center;">
                                                                <a class="btn btn-primary btn-flat btn-xs"
                                                                     onclick=' <%# "return ViewDesgloceBonos("+Eval("employee_number").ToString()+");" %>'>
                                                                    Detalles
                                                                </a>
                                                            </td>
                                                            <td style="text-align:center;">
                                                                <asp:CheckBox ID="cbxseleccionar" Text="Seleccionar" runat="server"
                                                                     employee_number='<%# Eval("employee_number") %>' />
                                                            </td>
                                                            <td>
                                                                <%# System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Eval("full_name").ToString().ToLower()) %>
                                                            </td>
                                                            <td style="min-width: 90px; text-align: right;">
                                                                <%# Convert.ToDecimal(Eval("authorization_amount")).ToString("C2") %>
                                                            </td>

                                                            <td style="min-width: 90px; text-align: right;">
                                                                <%# Convert.ToDecimal(Eval("amount_paid")).ToString("C2") %>
                                                            </td>
                                                            <td style="min-width: 90px; text-align: right;">
                                                                <%# Convert.ToDecimal(Eval("outstanding_amount_paid")).ToString("C2") %>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlcomentarios" CssClass="" Width="100%" runat="server">
                                                                </asp:DropDownList>
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
            <asp:HiddenField ID="hdf_numemployee" runat="server" />
            <asp:HiddenField ID="hdfguid" runat="server" />
            <asp:Button ID="btnview" style="display:none;" OnClick="btnview_Click" runat="server" Text="Button" />
            <asp:Button ID="btncheckedchanged" style="display:none;" OnClick="btncheckedchanged_Click" runat="server" Text="Button" />
        </ContentTemplate>
    </asp:UpdatePanel>
      <div class="modal fade bs-example-modal-lg" tabindex="-1" id="modal_detalles" role="dialog"
        aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnview" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Desgloce de bonos</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <div class="box box-solid">
                                        <!-- /.box-header -->
                                        <div class="box-body">
                                            <div class="box-group" id="accordion">
                                                <asp:Repeater ID="repetaer_bonds_details" runat="server">
                                                    <ItemTemplate>
                                                        <div class="panel box box-danger">
                                                            <div class="box-header with-border">
                                                                <h5 class="box-title">
                                                                    <a data-toggle="collapse" data-parent="#accordion" href='<%#"#collapse"+ Eval("id_request_bond").ToString() %>' 
                                                                        aria-expanded="false" class="collapsed">Solicitud:&nbsp;<%# Eval("id_request_bond").ToString() %>
                                                                    </a>
                                                                </h5>
                                                            </div>
                                                            <div id='<%#"collapse"+ Eval("id_request_bond").ToString() %>' class="panel-collapse collapse" 
                                                                aria-expanded="false" style="height: 0px;">
                                                                <div class="box-body">
                                                                    <div class="row">
                                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                                                            <h5><strong><i class="fa fa-pencil-square-o" aria-hidden="true"></i>&nbsp;Tipo de bono</strong></h5>
                                                                            <%# Eval("Tipo de bono").ToString() %>
                                                                        </div>
                                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                                                            <h5><strong><i class="fa fa-pencil-square-o" aria-hidden="true"></i>&nbsp;CC Cargo</strong></h5>
                                                                            <%# Eval("cc_cargo").ToString() %>
                                                                        </div>
                                                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                                                                            <h5><strong><i class="fa fa-money" aria-hidden="true"></i>&nbsp;$ Autorizado</strong></h5>
                                                                            <%# Convert.ToDecimal(Eval("authorization_amount")).ToString("C2") %>
                                                                        </div>
                                                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                                                                            <h5><strong><i class="fa fa-money" aria-hidden="true"></i>&nbsp;$ Pagado</strong></h5>
                                                                            <%# Convert.ToDecimal(Eval("amount_paid")).ToString("C2") %>
                                                                        </div>
                                                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                                            <h5><strong><i class="fa fa-money" aria-hidden="true"></i>&nbsp;$ A Pagar</strong></h5>
                                                                            <asp:UpdatePanel ID="UpdatePanel2341" runat="server" UpdateMode="Always">
                                                                                <Triggers>
                                                                                    <asp:AsyncPostBackTrigger ControlID="txtamount_topaid" EventName="TextChanged" />
                                                                                </Triggers>
                                                                                <ContentTemplate>
                                                                                    <asp:TextBox ID="txtamount_topaid"                                                                                         
                                                                                        onchange='<%# "return AmountChanged(this,"+ Eval("authorization_amount").ToString()+");" %>' 
                                                                                        Text='<%# Convert.ToDecimal(Eval("outstanding_amount_paid")).ToString("C2") %>'
                                                                                        CssClass="form-control" runat="server"
                                                                                        max_amount='<%# Convert.ToDecimal(Eval("authorization_amount")) %>'></asp:TextBox>

                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </div>
                                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                                            <h5><strong><i class="fa fa-comments" aria-hidden="true"></i>&nbsp;Observaciones</strong></h5>
                                                                            <asp:TextBox ID="txtobservations" TextMode="MultiLine" Rows="2" Text='<%# Eval("observations") %>'                                                                                 
                                                                                CssClass="form-control" runat="server"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="text-align: right;">
                                                                            <br />
                                                                            <asp:UpdatePanel ID="UpdatePanel2333" runat="server" UpdateMode="Always">
                                                                                <Triggers>
                                                                                    <asp:AsyncPostBackTrigger ControlID="btnguardar" EventName="Click" />
                                                                                </Triggers>
                                                                                <ContentTemplate>
                                                                                    <asp:Button ID="btnguardar" runat="server" CssClass="btn btn-primary btn-flat"
                                                                                         OnClick="btnguardar_Click" OnClientClick="return confirm('¿Desea aplicar el pago a la solicitud?');" Text="Actualizar"
                                                                                         num_employee='<%# Eval("employee_number").ToString() %>' 
                                                                                        id_request_bond='<%# Eval("id_request_bond").ToString() %>'/>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>
                                        <!-- /.box-body -->
                                    </div>
                                </div>
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
