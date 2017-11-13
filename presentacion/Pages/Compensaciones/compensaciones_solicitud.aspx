<%@ Page Title="Solicitud" Language="C#" MasterPageFile="~/Pages/MP/Global.Master" AutoEventWireup="true" CodeBehind="compensaciones_solicitud.aspx.cs" Inherits="presentacion.Pages.Compensaciones.compensaciones_solicitud" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h5 class="page-header">Solicitud de bonos</h5>
        </div>
    </div>
     <div class="row">
        <div class="col-lg-12">
            <div class="box box-default">
                <div class="box-body">
                    <h5><strong>Tipo de bono</strong></h5>
                    <asp:DropDownList ID="cbBonds_Types" AutoPostBack="true" CssClass=" form-control" runat="server"></asp:DropDownList>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
