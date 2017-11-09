<%@ Page Title="Sesiones" Language="C#" MasterPageFile="~/Pages/MP/Global.Master" AutoEventWireup="true" CodeBehind="catalogo_sesiones_usuario.aspx.cs" Inherits="presentacion.Pages.Catalogs.catalogo_sesiones_usuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function CloseDeviceMP(id_usuario_sesion, command) {
            var myHidden = document.getElementById('<%= hdfid_usuario_sesion2.ClientID%>');
            myHidden.value = id_usuario_sesion;
            var commando = document.getElementById('<%= hdfcommand2.ClientID%>');
            commando.value = command;
            var msg = command == "cerrar"
                ? "¿Desea cerrar sesión en este dispositivo. Tenga en cuenta que podria perderse información no guardada?" :
               "¿Desea bloquear/desbloquear el inicio de sesión en este dispositivo. Tenga en cuenta que podria perderse información no guardada?";
            if (confirm(msg)) {
                document.getElementById('<%= btncerrarsesion2.ClientID%>').click();
            }

            return false;
        }

        function CargarNuevosDispositivos() {
            $.ajax({
                url: 'Service.asmx/checaItem',
                contentType: "application/json; charset=utf-8",
                type: "POST",
                dataType: "json",
                success: function (response) {
                    var mensaje = response.d;
                    if (mensaje != "") {
                        document.getElementById('<%= lnkactualizar2.ClientID%>').click();
                        ShowNewDevice();
                    }
                },
                error: function (result, status, err) {
                    console.log("error", result.responseText);
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            
    <div class="row">
        <div class="col-lg-12">
            <h4 class="page-header">Sesiones por Usuario</h4>
        </div>
        <div class="col-lg-12">
            <asp:LinkButton ID="lnkactualizar2" CssClass="btn btn-primary btn-flat"
                OnClick="lnkactualizar2_Click" runat="server">
                                         <i class="fa fa-refresh" aria-hidden="true"></i>
            </asp:LinkButton>
        </div>
        <div class="col-lg-12">
            <div>
                <ul class="products-list product-list-in-box" style="font-size: 12px;">
                    <asp:Repeater ID="repeat_devices2" runat="server">
                        <ItemTemplate>
                            <li class="item">
                                <div class="product-img" style="padding-left: 10px;">
                                    <%# Eval("icono") %>
                                </div>
                                <div class="product-info">
                                    <span class="product-title">
                                        <%# Eval("name_device") %>
                                                                &nbsp;-&nbsp;
                                                                <%# Eval("sistema_operativo") %>
                                        <span class="pull-right">
                                            <div class="dropdown">
                                                <button class="btn btn-link dropdown-toggle" style="cursor: pointer;" type="button" id="dropdownMenu1"
                                                    data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                                                    <i class="fa fa-th"></i>
                                                </button>
                                                <ul class="dropdown-menu  dropdown-menu-right" aria-labelledby="dropdownMenu1">
                                                    <li style="cursor: pointer;">
                                                        <a data-toggle="tooltip" title='<%# Eval("region") %>' style="cursor: pointer;" id="rutaurl" runat="server" visible='<%# Eval("latitud").ToString()!="" &&  Eval("longitud").ToString()!=""%>'
                                                            onclick='<%# "window.open("+@""""+"localizador.aspx?lat="+presentacion.funciones.deTextoa64( Eval("latitud").ToString())+
                                                                            "&lon="+presentacion.funciones.deTextoa64(Eval("longitud").ToString())+
                                                                            "&desc="+presentacion.funciones.deTextoa64("Ubicación de "+Eval("name_device").ToString()+" "+Eval("sistema_operativo").ToString()+
                                                                            " en "+Eval("region").ToString()+", con la IP: "+Eval("ip").ToString()+" de "+Eval("proveedor").ToString())+@""""+");" %>'>
                                                            <i class="fa fa-map-marker" aria-hidden="true"></i>&nbsp;Ver Ubicación
                                                        </a>
                                                    </li>
                                                    <li style="cursor: pointer;" id="li_cs" runat="server" visible='<%# Convert.ToBoolean(Eval("activo")) %>'>
                                                        <a <%# Convert.ToBoolean(Eval("activo"))?"block":"Desconectado" %> onclick='<%# "return CloseDeviceMP("+Eval("id_usuario_Sesion")+@",""" +"cerrar"+@""""+");" %>'>
                                                            <i class="fa fa-sign-out" aria-hidden="true"></i>&nbsp;Cerrar sesión
                                                        </a>
                                                    </li>
                                                    <li style="cursor: pointer;" id="li_dp" runat="server" visible='<%# !Convert.ToBoolean(Eval("bloqueado")) %>'>
                                                        <a onclick='<%# "return CloseDeviceMP("+Eval("id_usuario_Sesion")+@",""" +"bloquear"+@""""+");" %>'>
                                                            <i class="fa fa-lock" aria-hidden="true"></i>&nbsp;Bloquear este dispositivo
                                                        </a>
                                                    </li>
                                                    <li style="cursor: pointer;" id="li1" runat="server" visible='<%# Convert.ToBoolean(Eval("bloqueado")) %>'>
                                                        <a onclick='<%# "return CloseDeviceMP("+Eval("id_usuario_Sesion")+@",""" +"desbloquear"+@""""+");" %>'>
                                                            <i class="fa fa-unlock" aria-hidden="true"></i>&nbsp;Desbloquear este dispositivo
                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </span>
                                    </span>
                                    <span class="product-description">
                                        <%# Eval("navegador") %>
                                        &nbsp;-&nbsp;
                                        <%#Convert.ToBoolean(Eval("bloqueado"))?"<label class='label label-danger'>Bloqueado</label>":
                                                (Convert.ToBoolean(Eval("activo"))?"<label class='label label-success'>Conectado</label>":"<label class='label label-default'>Desconectado</label>") %>
                                    </span>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
    </div>

    <asp:Button ID="btncerrarsesion2" OnClick="btncerrarsesion_Click" runat="server" Text="Button" Style="display: none" />
    <asp:HiddenField ID="hdfid_usuario_sesion2" runat="server" />
    <asp:HiddenField ID="hdfcommand2" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
