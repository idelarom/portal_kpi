<%@ Page Title="Configuracion del Portal" Language="C#" MasterPageFile="~/Pages/MP/Global.Master" AutoEventWireup="true" CodeBehind="configuracion_portal.aspx.cs" Inherits="presentacion.Pages.Common.configuracion_portal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
           function ConfirmEmpleadoProyectoModal(msg) {
            if (confirm(msg)) {
                $("#<%= lnkactualizaractualizar.ClientID%>").show();
               $("#<%= lnkactualizar.ClientID%>").hide();
               return true;
           } else {
               return false;
           }
           return false;
       }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
       <asp:UpdatePanel UpdateMode="Always" ID="aaa" runat="server">
           <ContentTemplate>
                <div class="col-lg-12">
            <h4><strong><i class="fa fa-address-book" aria-hidden="true"></i>&nbsp;Actualizar fotografias de los usuarios</strong></h4>

            <asp:LinkButton OnClientClick="return false;" ID="lnkactualizaractualizar" CssClass="btn btn-primary btn-flat" runat="server"
                 Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Actualizando
            </asp:LinkButton>
            <asp:LinkButton ID="lnkactualizar" OnClick="lnkactualizar_Click"
                CssClass="btn btn-primary btn-flat" OnClientClick="return ConfirmEmpleadoProyectoModal('¿Desea actualizar todas las imagenes de los empleados activos?');" runat="server">
                Actualizar
            </asp:LinkButton>
        </div>
           </ContentTemplate>
       </asp:UpdatePanel>
    </div>
</asp:Content>
