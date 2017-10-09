<%@ Page Title="Dashboard Proyectos" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="proyectos_dashboard.aspx.cs" Inherits="presentacion.proyectos_dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function GoRiesgos() {
            document.getElementById('<%= lnkgo_riesgos.ClientID%>').click();
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h4 class="page-header">
                <asp:Label ID="lblproyect" runat="server" Text="Mi Proyecto"></asp:Label></h4>
        </div>
        <div class="col-lg-12">
            <div class="box box-danger">
                <div class="box-header with-border">
                    <h3 class="box-title">Información general</h3>

                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                            <i class="fa fa-minus"></i>
                        </button>
                    </div>
                </div>
                <div class="box-body" style="">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <h5><strong><i class="fa fa-bars" aria-hidden="true"></i>&nbsp;Resumen del Proyecto</strong></h5>
                            <p class="text-muted">
                                <asp:Label ID="lblresumen" runat="server" Text="Label"></asp:Label>
                            </p>
                        </div>   
                        
                        <div class="col-lg-12 col-md-12 col-sm-12  col-xs-12">
                            <h5><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Tecnología</strong></h5>
                            <p class="text-muted">
                                <asp:Label ID="lbltecnologia" runat="server" Text="Label"></asp:Label>
                            </p>
                        </div>                     
                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                            <h5><strong><i class="fa fa-briefcase" aria-hidden="true"></i>&nbsp;Estatus</strong></h5>
                            <p class="text-muted">
                                <asp:Label ID="lblestatus" runat="server" Text="Label"></asp:Label>
                            </p>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-6  col-xs-12">
                            <h5><strong><i class="fa fa-calendar-o" aria-hidden="true"></i>&nbsp;Periodo de evaluación asignado</strong></h5>
                            <p class="text-muted">
                                <asp:Label ID="lblperiodo" runat="server" Text="Label"></asp:Label>
                            </p>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-6  col-xs-12">
                            <h5><strong><i class="fa fa-user" aria-hidden="true"></i>&nbsp;Administrador del proyecto</strong></h5>
                            <p class="text-muted">
                                <asp:Label ID="lblempleado" runat="server" Text="Label"></asp:Label>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
      <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
          <!-- small box -->
          <div class="small-box bg-red" onclick="return GoRiesgos();" style="cursor:pointer;">
            <div class="inner">
              <h3>
                  <asp:Label ID="lblnumriesgos" runat="server" Text="0"></asp:Label></h3>

              <p>Riesgos abiertos</p>
            </div>
            <div class="icon">
              <i class="ion ion-android-warning"></i>
            </div>
              <asp:LinkButton ID="lnkgo_riesgos" OnClick="lnkgo_riesgos_Click" runat="server"   CssClass="small-box-footer">
                  
              Evaluaciones&nbsp;<i class="fa fa-arrow-circle-right"></i>
              </asp:LinkButton>
          </div>
        </div>
    </div>
    <asp:HiddenField ID="hdfid_proyecto" runat="server" />
</asp:Content>
