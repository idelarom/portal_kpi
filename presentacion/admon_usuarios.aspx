<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="admon_usuarios.aspx.cs" Inherits="presentacion.admon_usuarios" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ModalClose() {
            $('#myModal').modal('hide');
        }
        $(document).ready(function () {
            Init();
        });
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
         function ConfirmEmpleadoProyectoModal(msg) {
            if (confirm(msg)) {
                $("#<%= lnkcargando.ClientID%>").show();
                $("#<%= lnkguardar.ClientID%>").hide();
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

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h4 class="page-header">Administración de Usuarios</h4>
        </div>
        <div class="col-lg-4" id="div_tipos" runat="server" visible="false">
            <h5><strong><i class="fa fa-users" aria-hidden="true"></i>&nbsp;Filtro de Usuarios</strong></h5>
            <asp:DropDownList ID="ddltipousuarios" CssClass="form-control" runat="server" AutoPostBack="true"
                 OnSelectedIndexChanged="ddltipousuarios_SelectedIndexChanged"></asp:DropDownList>
            <br />
        </div>
        <div class="col-lg-12">
            
                    <asp:LinkButton ID="lnkagregar" OnClick="lnkagregar_Click"
                        CssClass="btn btn-danger btn-flat" runat="server">Nuevo Usuario Cliente&nbsp;<i class="fa fa-plus" aria-hidden="true"></i></asp:LinkButton>
            <br />   <br />
            <div class="table table-responsive">

                <telerik:RadGrid ID="grid_usuarios" runat="server" Skin="Metro">
                    <MasterTableView AutoGenerateColumns="false" CssClass="dvv table table-responsive"
                        HeaderStyle-BackColor="White" HeaderStyle-ForeColor="Black"
                        Width="100%">
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="">
                                <HeaderStyle Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkcommand"  runat="server" CommandName="Editar" OnClick="lnkcommand_Click"
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id_usuario").ToString() %>'>
                                              <i class="fa fa-pencil fa-2x" aria-hidden="true"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="">
                                <HeaderStyle Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkcommand2" Visible="true"  OnClick="lnkcommand_Click"  runat="server" CommandName="Eliminar" OnClientClick="return ConfirmEntregableDelete('¿Desea Eliminar este Usuario?')"
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id_usuario").ToString() %>'>
                                         <i class="fa fa-trash fa-2x" aria-hidden="true"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridBoundColumn DataField="usuario" HeaderText="Usuario" UniqueName="usuario"
                                Visible="true">
                                <HeaderStyle Width="150px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="perfil" HeaderText="Perfil" UniqueName="perfil"
                                Visible="true">
                                <HeaderStyle Width="150px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="pertenece a" HeaderText="" UniqueName="pertenece"
                                Visible="true">
                            </telerik:GridBoundColumn>  
                            <telerik:GridTemplateColumn HeaderText="Administrador">
                                <HeaderStyle Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" Checked='<%# Convert.ToBoolean(Eval("administrador")) %>' CssClass=" form-control" Enabled="false" runat="server" /></td>
                                       
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
      
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
        </div>
    </div>
        <div class="modal fade bs-example-modal-lg" tabindex="-1" id="myModal" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnkagregar" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Usuarios de Clientes</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-12">
                                    <h5><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Usuario</strong></h5>
                                    <telerik:RadTextBox ID="rtxtusuario" Width="100%" runat="server" Skin="Bootstrap"></telerik:RadTextBox>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12">
                                    <h5><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Contraseña</strong></h5>
                                    <telerik:RadTextBox ID="rtxtcontra" Width="100%" runat="server" Skin="Bootstrap"></telerik:RadTextBox>
                                </div>
                                <div class="col-lg-12">
                                    <h5><strong><i class="fa fa-users" aria-hidden="true"></i>&nbsp;Tipo de Usuario</strong></h5>
                                    <asp:DropDownList ID="ddltipos_usuarios" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                                <div class="col-lg-12">
                                    <h5><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Seleccione los Proyectos a los que desea Relacionar</strong></h5>
                                    
                                    <telerik:RadListBox Style="font-size: 11px" RenderMode="Lightweight" runat="server" ID="rdl_proyectos" Height="150" Width="100%"
                                        Skin="Bootstrap" SelectionMode="Multiple">
                                    </telerik:RadListBox>
                                </div>
                            </div>
                            <asp:TextBox ID="txtid_usuario" Visible="false" runat="server"></asp:TextBox>
                        </div>
                        <div class="modal-footer ">
                            <div class="row" id="div_error" runat="server" visible="false" style="text-align: left;">
                                <div class="col-lg-12">
                                    <div class="alert alert-danger alert-dismissible">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <asp:Label ID="lblerror" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                            <asp:LinkButton OnClientClick="return false;" ID="lnkcargando" CssClass="btn btn-primary btn-flat" runat="server" Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Guardando...
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkguardar" CssClass="btn btn-primary btn-flat" OnClick="lnkguardar_Click"
                                OnClientClick="return ConfirmEmpleadoProyectoModal('¿Desea Guardar este Usuario?');" runat="server">
                                            <i class="fa fa-floppy-o" aria-hidden="true"></i>&nbsp;Guardar
                            </asp:LinkButton>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:HiddenField ID="hdfmotivos" runat="server" />
</asp:Content>
