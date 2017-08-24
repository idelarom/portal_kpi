<%@ Page Title="Catalogo Usuarios" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="catalogo_usuarios.aspx.cs" Inherits="presentacion.catalogo_usuarios" %>
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
        function EditarClick(usuario) {
            var hdfusuario = document.getElementById('<%= hdfusuario.ClientID %>');
            hdfusuario.value = usuario;
              document.getElementById('<%= btnver.ClientID%>').click();
              return false;
        }
        function ChangedTextLoad3()
        {
            $("#<%= imgmenu.ClientID%>").show();
            $("#<%= lblmenu.ClientID%>").show();
            return true;
        }
        function ChangedTextLoad2()
        {
            $("#<%= imgperfil.ClientID%>").show();
            $("#<%= lblperfilb.ClientID%>").show();
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="box box-primary">
                <div class="box-body">
                    <div class="table-responsive">
                        <table class="dvv table no-margin table-condensed">
                            <thead>
                                <tr style="font-size: 11px;">
                                    <th style="min-width: 30px; text-align: center;" scope="col"></th>
                                    <th style="min-width: 60px; text-align: left;" scope="col">Usuario</th>
                                    <th style="min-width: 60px; text-align: left;" scope="col">No. Empleado</th>
                                    <th style="min-width: 200px; text-align: left;" scope="col">Nombre</th>
                                    <th style="min-width: 200px; text-align: left;" scope="col">Puesto</th>
                                    <th style="min-width: 200px; text-align: left;" scope="col">Perfil</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="repeat_usuarios" runat="server">
                                    <ItemTemplate>
                                        <tr style="font-size: 11px">
                                            <td style="text-align: center;">
                                                <a style="cursor: pointer;" 
                                                    onclick='<%# "return EditarClick("+@""""+Eval("usuario")+@"""" +");" %>'>
                                                    <i class="fa fa-pencil fa-2x" aria-hidden="true"></i>
                                                </a>
                                            </td>
                                            <td style="text-align: left;"><%# Eval("Usuario") %></td>
                                            <td style="text-align: left;"><%# Eval("num_empleado") %></td>
                                            <td style="text-align: left;"><%# Eval("Nombre") %></td>
                                            <td style="text-align: left;"><%# Eval("Puesto") %></td>
                                            <td style="text-align: left;"><%# Eval("Perfil") %></td>
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
    <div class="modal fade bs-example-modal-lg" tabindex="-1" id="ModalEmpleado" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnver" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Detalles del empleado</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <div class="box box-widget widget-user">
                                        <!-- Add the bg color to the header using any of the bg-* classes -->
                                        <div class="widget-user-header bg-aqua-active">
                                            <h3 class="widget-user-username">
                                                <asp:Label ID="lblnombre" runat="server" Text=""></asp:Label></h3>
                                            <h5 class="widget-user-desc">
                                                <asp:Label ID="lblusuario" runat="server" Text=""></asp:Label></h5>
                                        </div>
                                        <div class="widget-user-image">
                                            <asp:Image ID="img_employee" runat="server" ImageUrl="~/img/user.png"
                                                CssClass="img-responsive img-circle" Style="max-height: 100px" />
                                        </div>
                                        <div class="box-footer">
                                            <div class="row">
                                                <div class="col-sm-4 border-right">
                                                    <div class="description-block">
                                                        <h5 class="description-header">Puesto</h5>
                                                        <span class="description-text">
                                                            <asp:Label ID="lblpuesto" runat="server" Text=""></asp:Label></span>
                                                    </div>
                                                    <!-- /.description-block -->
                                                </div>
                                                <!-- /.col -->
                                                <div class="col-sm-4">
                                                    <div class="description-block">
                                                        <h5 class="description-header">Perfil</h5>
                                                        <span class="description-text">
                                                            <asp:Label ID="lblperfil" runat="server" Text=""></asp:Label>
                                                            <br />
                                                            <asp:LinkButton CssClass="btn btn-danger btn-sm" OnClick="lnkaddperfil_Click"
                                                                ID="lnkaddperfil" runat="server">Cambiar perfil</asp:LinkButton>
                                                            </span>
                                                    </div>
                                                    <!-- /.description-block -->
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="description-block">
                                                        <h5 class="description-header">Menus</h5>
                                                        <span class="description-text">
                                                            Menus disponibles para el empleado
                                                            <br />
                                                            <asp:LinkButton ID="lnkaddmenus" OnClick="lnkaddmenus_Click" CssClass="btn btn-danger  btn-sm"
                                                                runat="server">Ver menus
                                                            </asp:LinkButton>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" id="div_menus" runat="server" visible="false">
                                                <div class="col-lg-6 col-md-6 col-sm-12">
                                                    <h5><strong>Agregar menu</strong></h5>
                                                    <div style="text-align: left;" class="input-group input-group-sm">
                                                        <asp:TextBox ID="txtbuscarmenu" CssClass="form-control" placeholder="Buscar" runat="server"></asp:TextBox>
                                                        <span class="input-group-btn">
                                                            <asp:LinkButton ID="lnkbuscarmenu" CssClass="btn btn-primary btn-flat" runat="server"
                                                                OnClientClick="return ChangedTextLoad3();" OnClick="lnkbuscarmenu_Click">
                                                                        <i class="fa fa-search" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                        </span>
                                                    </div>

                                                    <asp:Image ID="imgmenu" Style="display: none;" ImageUrl="~/img/load.gif" runat="server" />
                                                    <label id="lblmenu" runat="server" style="display: none; color: #1565c0">Buscando Menus</label>

                                                    <asp:DropDownList ID="ddlmenus" CssClass="form-control" runat="server"></asp:DropDownList>
                                                    <br />
                                                    <div style="text-align: right">
                                                        <asp:LinkButton ID="lnkaddmenu" CssClass="btn btn-primary btn-flat btn-sm"
                                                            OnClick="lnkaddmenu_Click" OnClientClick="return confirm('¿Desea asignar el menu al usuario?');"
                                                            runat="server">Agregar menu&nbsp;<i class="fa fa-plus-circle" aria-hidden="true"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-sm-12">
                                                    <h5><strong>Menus disponibles para este usuario</strong></h5>
                                                    <div style="width: 100%; height: 110px; overflow: scroll;">
                                                        <ul style="font-size: 10px">
                                                            <asp:Repeater ID="repeater_menu" runat="server">
                                                                <ItemTemplate>
                                                                    <li>
                                                                        <%# Eval("nombre") %>
                                                                    </li>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" id="div_addperfil" runat="server" visible="false">
                                                <div class="col-lg-12">
                                                    <div style="text-align: left;" class="input-group input-group-sm">
                                                        <asp:TextBox ID="txtbuscarperfil" CssClass="form-control" placeholder="Buscar" runat="server"></asp:TextBox>
                                                        <span class="input-group-btn">
                                                            <asp:LinkButton ID="LinkButton1" CssClass="btn btn-primary btn-flat" runat="server"
                                                                OnClientClick="return ChangedTextLoad2();" OnClick="lnkbuscarperfil_Click">
                                                                        <i class="fa fa-search" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                        </span>
                                                    </div>

                                                    <asp:Image ID="imgperfil" Style="display: none;" ImageUrl="~/img/load.gif" runat="server" />
                                                    <label id="lblperfilb" runat="server" style="display: none; color: #1565c0">Buscando perfiles</label>

                                                    <asp:DropDownList ID="ddlperfiles" CssClass="form-control" runat="server"></asp:DropDownList>
                                                     <br />
                                                    <div style="text-align: right">
                                                        <asp:LinkButton ID="lnksaveperfil" CssClass="btn btn-primary btn-flat btn-sm"
                                                            OnClick="lnkasaveperfil_Click" OnClientClick="return confirm('¿Desea asignar el perfil al usuario?');"
                                                            runat="server">Guardar perfil&nbsp;<i class="fa fa-save" aria-hidden="true"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- /.row -->
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="modal-footer ">
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:Button ID="btnver" runat="server" Text="" style="display:none" OnClick="btnver_Click" />
    <asp:HiddenField ID="hdfusuario" runat="server" />
</asp:Content>
