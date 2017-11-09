<%@ Page Title="Ayuda" Language="C#" MasterPageFile="~/Pages/MP/Global.Master" AutoEventWireup="true" CodeBehind="ayuda.aspx.cs" Inherits="presentacion.Pages.Common.ayuda" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">

        .img-center {
            margin-left: auto;
            margin-right: auto;
            display: block;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div class="col-lg-12">
                <h4 class="page-header">Manual de ayuda</h4>
                
            </div>
        </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12">
                    <div class="box box-danger">
                        <div class="box-header with-border">
                            <h3 class="box-title">Módulos de Ayuda</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body no-padding">
                            <div style="text-align: right; display: none;">
                                <label>Buscar</label>
                                <telerik:RadTextBox ID="rtxtxsearchtarea" AutoPostBack="true" Width="200px" runat="server" Skin="Bootstrap"></telerik:RadTextBox>
                            </div>
                            <telerik:RadTreeView RenderMode="Lightweight" ID="rtrvProyectWorks" runat="server" Width="100%"
                                Style="background-color: white;" Skin="Bootstrap" OnNodeClick="rtrvProyectWorks_NodeClick">
                                <DataBindings>
                                    <telerik:RadTreeNodeBinding Expanded="False"></telerik:RadTreeNodeBinding>
                                </DataBindings>
                            </telerik:RadTreeView>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade bs-example-modal-lg" tabindex="-1" id="ModalAyudas" role="dialog" aria-labelledby="mySmallModalLabel">
                <div class="modal-dialog modal-lg" role="document">

                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">
                                <asp:Label ID="lbltituloayuda" runat="server" Text="Label"></asp:Label></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <p><asp:Label ID="lbldesc" runat="server" Text="No hay descripción disponible"></asp:Label></p>
                                    </div>
                                <div class="col-lg-12">
                                    <asp:Image ID="image" CssClass="img img-responsive img-center" style="max-height:450px" runat="server" />
                                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
