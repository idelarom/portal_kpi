<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MP/Global.Master" AutoEventWireup="true" CodeBehind="catalogo_tipo_bonos.aspx.cs" Inherits="presentacion.Pages.Catalogs.catalogo_tipo_bonos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <script type="text/javascript">
        $(document).ready(function () {
            Init();
        });
        function Init() {
            $('.dvv').DataTable({
                "paging": true,
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
        function ConfirmEntregableDelete(id_permiso) {
            if (confirm('¿Desea eliminar este permiso?')) {
                var hdfusuario = document.getElementById('<%= hdfid_proyecto_tecnologia.ClientID %>');
                hdfusuario.value = id_permiso;
                document.getElementById('<%= btneliminar.ClientID%>').click();
                return true;
            } else {
                return false;
            }
        }
        function EditarClick(id_permiso) {
            var hdfusuario = document.getElementById('<%= hdfid_proyecto_tecnologia.ClientID %>');
            hdfusuario.value = id_permiso;
            document.getElementById('<%= btneventgrid.ClientID%>').click();
            return false;
        }
        function ConfirmwidgetProyectoModal(msg) {
            if (confirm(msg)) {
                $("#<%= lnkcargando.ClientID%>").show();
                $("#<%= lnkguardar.ClientID%>").hide();
                return true;
            } else {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
          <h4 class="page-header">Catálogo tipo de bonos</h4>          
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="box box-danger">
                <div class="box-body"><asp:LinkButton ID="lnknuevotipobono" CssClass="btn btn-primary btn-flat" runat="server" OnClick="lnknuevotipobono_Click">
                Nuevo Tipo de bono&nbsp;<i class="fa fa-plus" aria-hidden="true"></i>
            </asp:LinkButton>
                    <div class="table-responsive">
                        <table class="dvv table no-margin table-condensed">
                            <thead>
                                <tr style="font-size: 12px;">
                                    <th style="width: 20px; text-align: center;" scope="col"></th>
                                    <th style="width: 20px; text-align: center;" scope="col"></th>
                                    <th style=" min-width:400px; text-align: left;" scope="col">Tipo de bono</th>
                                     <th style=" min-width:400px; text-align: left;" scope="col">Descripcion</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="repeat_bonds_type" runat="server">
                                    <ItemTemplate>
                                        <tr style="font-size: 12px">
                                            <td style="text-align: center;">
                                                <a style="cursor: pointer;"
                                                    onclick='<%# "return EditarClick("+Eval("id_proyecto_tecnologia")+");" %>'>
                                                    <i class="fa fa-pencil fa-2x" aria-hidden="true"></i>
                                                </a>
                                            </td>
                                            <td style="text-align: center;">
                                                <a style="cursor: pointer;"
                                                    onclick='<%# "return ConfirmEntregableDelete("+Eval("id_proyecto_tecnologia")+");" %>'>
                                                    <i class="fa fa-trash fa-2x" aria-hidden="true"></i>
                                                </a>
                                            </td>
                                            <td style="text-align: left;"><%# Eval("name") %></td>
                                            <td style="text-align: left;"><%# Eval("description") %></td>
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
       <div class="modal fade bs-example-modal-lg" tabindex="-1" id="ModalTipoBonos" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnknuevotipobono" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btneventgrid" EventName="Click" />
                    <asp:PostBackTrigger ControlID="lnkguardar" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Detalles del tipo de bono</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <h5><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Nombre del bono</strong></h5>
                                    <asp:TextBox ID="txtestatus" MaxLength="250" CssClass=" form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <h5><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Descripcion</strong></h5>
                                    <asp:TextBox ID="TextBox1" MaxLength="250" CssClass=" form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>  

                             <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-12">
                                    <h5><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Monto de configuracion</strong></h5>
                                    <asp:TextBox ID="txtmonto" MaxLength="250" CssClass=" form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12">
                                    <h5><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Porcentaje extra</strong></h5>
                                    <asp:TextBox ID="txtporcentaje" MaxLength="250" CssClass=" form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>  

                             <h5><strong><i class="fa fa-check-square-o" aria-hidden="true"></i>&nbsp;Requerimientos</strong></h5> 
                             <div class="row">
                                 <div class="col-lg-3 col-md-4 col-sm-12">
                                    <asp:CheckBox ID="chkRmonto" runat="server" Checked="false" Visible="true" text="cantidad"/>
                                </div>  
                                <div class="col-lg-3 col-md-4 col-sm-12">
                                    <asp:CheckBox ID="chkperiodicidad" runat="server" Checked="false" Visible="true" text="periodicidad"/>
                                </div>                                
                                <div class="col-lg-3 col-md-4 col-sm-12">
                                    <asp:CheckBox ID="chksemana" runat="server" Checked="false" Visible="true" text="detalle de la semana"/>
                                </div>
                                  <div class="col-lg-3 col-md-4 col-sm-12">
                                    <asp:CheckBox ID="chkfolipm" runat="server" Checked="false" Visible="true" text="folio pmtracker"/>
                                </div>                                  
                            </div>

                            <div class="row">
                                <div class="col-lg-3 col-md-4 col-sm-12">
                                    <asp:CheckBox ID="chkproyecto" runat="server" Checked="false" Visible="true" text="nombre del proyecto"/>
                                </div>
                                <div class="col-lg-3 col-md-4 col-sm-12">
                                    <asp:CheckBox ID="chkcliente" runat="server" Checked="false" Visible="true" text="nombre del cliente"/>
                                </div>                                
                                <div class="col-lg-3 col-md-4 col-sm-12">
                                    <asp:CheckBox ID="chkhoras" runat="server" Checked="false" Visible="true" text="número de horas"/>
                                </div>
                                 <div class="col-lg-3 col-md-4 col-sm-12">
                                    <asp:CheckBox ID="chkcamount" runat="server" Checked="false" Visible="true" text="captura de cantidad de auto."/>
                                </div>
                            </div>
                           <div class="row">                               
                                <div class="col-lg-3 col-md-4 col-sm-12">
                                    <asp:CheckBox ID="chkgrid" runat="server" Checked="false" Visible="true" text="requisiciones de cuadrícula"/>
                                </div> 
                               <div class="col-lg-3 col-md-12 col-sm-12">
                                    <asp:CheckBox ID="chkinperiodo" runat="server" Checked="false" Visible="true" text="fecha inicio período captura"/>
                                </div>
                                <div class="col-lg-3 col-md-4 col-sm-12">
                                   <%-- <h5><strong><i class="fa fa-comment-o" aria-hidden="true"></i>&nbsp;Activo</strong></h5>--%>
                                    <asp:CheckBox ID="chkfinperiodo" runat="server" Checked="false" Visible="true" text="fecha fin período capturar"/>
                                </div> 
                           </div>
                        </div>

                        <div class="modal-footer ">
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                            <asp:LinkButton OnClientClick="return false;" ID="lnkcargando" CssClass="btn btn-primary btn-flat" runat="server" Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Guardando...
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkguardar" CssClass="btn btn-primary btn-flat" OnClick="lnkguardar_Click"
                                OnClientClick="return ConfirmwidgetProyectoModal('¿Desea Guardar este proyecto estatus?');" runat="server">
                                            <i class="fa fa-floppy-o" aria-hidden="true"></i>&nbsp;Guardar
                            </asp:LinkButton>
                        </div>
                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
           </div>  
    <asp:Button ID="btneventgrid" OnClick="btneventgrid_Click" runat="server" Text="Button" Style="display: none;" />
    <asp:Button ID="btneliminar" OnClick="btneliminar_Click" runat="server" Text="Button" Style="display: none;" />
     <asp:HiddenField ID="hdfcommand" runat="server" />
     <asp:HiddenField ID="hdfid_proyecto_tecnologia" runat="server" />
</asp:Content>
