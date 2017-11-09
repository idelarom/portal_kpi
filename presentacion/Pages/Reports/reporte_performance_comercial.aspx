<%@ Page Title="Comercial" Language="C#" MasterPageFile="~/Pages/MP/Global.Master" AutoEventWireup="true" CodeBehind="reporte_performance_comercial.aspx.cs" Inherits="presentacion.Pages.Reports.reporte_performance_comercial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .header-tbl {
            background-color: #DE3230;
            color: white;
        }
    </style>
    <script type="text/javascript">
        
        function Carganodfiltros() {
            $("#<%= nkcargandofiltros.ClientID%>").show();
            $("#<%= lnkfiltros.ClientID%>").hide();
            return true;
        }
        
        function ChangedTextLoad2() {
            $("#<%= imgloadempleado.ClientID%>").show();
            $("#<%= lblbemp.ClientID%>").show();
            return true;
        }
        
        function ConfirmwidgetProyectoModal() {
            $("#<%= div_modalbodyfiltros.ClientID%>").hide();
            $("#<%= lnkcargando.ClientID%>").show();
            $("#<%= lnkguardar.ClientID%>").hide();
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h3 class="page-header">Performance Comercial</h3>
            <asp:LinkButton OnClientClick="return false;" ID="nkcargandofiltros" CssClass="btn btn-primary btn-flat" runat="server" Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Cargando filtros
            </asp:LinkButton>
            <asp:LinkButton ID="lnkfiltros" CssClass="btn btn-primary btn-flat" OnClick="lnkfiltros_Click" runat="server"
                OnClientClick="return Carganodfiltros();">
               <i class="fa fa-filter" aria-hidden="true"></i>&nbsp;Filtros
            </asp:LinkButton>
        </div>
    </div>

    <div class="row" runat="server" id="div_reporte" visible="false">
        <div class="col-lg-12">
            <div class="box box-danger box-solid">
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="lblnombrempleado" runat="server" Text="--Nombre del empleado--"></asp:Label></h3>
                </div>
                <div class="box-body" style="">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="box box-danger">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Nuevas Oportunidades</h3>

                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                            <i class="fa fa-minus"></i>
                                        </button>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="table table-responsive">
                                                <table class="" bordercolor="black" cellspacing="0" cellpadding="5" width="100%" border="1">
                                                
                                                    <tbody style="">
                                                        <tr>
                                                            <td style="min-width:50px;"  rowspan="2" class="header-tbl"><b></b></td>
                                                            <td style="min-width:400px;" colspan="3" align="center">
                                                                <b><asp:Label ID="lblañoanterior" runat="server" Text="0"></asp:Label></b></td>
                                                            <td rowspan="7" width="5px" class="header-tbl">
                                                                <br>
                                                            </td>
                                                            <td  style="min-width:400px;"colspan="3" align="center"><b><asp:Label ID="lblañoactual" runat="server" Text="0"></asp:Label></b></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" class="header-tbl">Op's</td>
                                                            <td align="center" class="header-tbl">Monto Original</td>
                                                            <td align="center" class="header-tbl">Margen</td>

                                                            <td align="center" class="header-tbl">Op's</td>
                                                            <td align="center" class="header-tbl">Monto Original</td>
                                                            <td align="center" class="header-tbl">Margen</td>
                                                        </tr>
                                                            <asp:Repeater ID="repeat_nuevas_oportunidades" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td align="left"><%# Eval("tipo") %></td>
                                                                        <td align="center"><%# Eval("numop") %></td>
                                                                        <td align="right"><%# presentacion.funciones.ValueMoneyMil(Convert.ToDecimal(Eval("montoori")),2)  %></td>
                                                                        <td align="right"><%#  presentacion.funciones.ValueMoneyMil(Convert.ToDecimal(Eval("margen")),2)  %></td>
                                                                        <td align="center"><%# Eval("numop2") %></td>
                                                                        <td align="right"><%#  presentacion.funciones.ValueMoneyMil(Convert.ToDecimal(Eval("montoori2")),2)  %></td>
                                                                        <td align="right"><%#  presentacion.funciones.ValueMoneyMil(Convert.ToDecimal(Eval("margen2")),2)  %></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>

                                                            <tr>
                                                                <td align="left"><b>Total</b></td>
                                                                <asp:Repeater ID="repeater_totales_nuevas_oportunidades" runat="server">
                                                                    <ItemTemplate>
                                                                        <td align="center"><b><%# Eval("numop") %></b></td>
                                                                        <td align="right"><b><%# presentacion.funciones.ValueMoneyMil(Convert.ToDecimal(Eval("montoori")),2) %></b></td>
                                                                        <td align="right"><b><%# presentacion.funciones.ValueMoneyMil(Convert.ToDecimal(Eval("margen")),2)  %></b></td>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="col-lg-12">
                            <div class="box box-danger">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Inventario de Oportunidades</h3>

                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                            <i class="fa fa-minus"></i>
                                        </button>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="table table-responsive">

                                                <table bordercolor="black" cellspacing="0" cellpadding="0" width="100%" border="1">
                                                    <tbody>
                                                        <tr>
                                                            <td class="header-tbl" rowspan="2" style="min-width:80px"></td>
                                                            <td style="min-width:310px;" colspan="3" align="center"><b>T o t a l e s</b></td>
                                                              <td rowspan="7" width="5px" class="header-tbl">
                                                                <br>
                                                            </td>
                                                            <td style="min-width:310px;" colspan="4" align="center"><b><asp:Label ID="lblañoanterior2" runat="server" Text="0"></asp:Label></b></td>
                                                            <td  class="header-tbl" rowspan="6" width="5">
                                                                <br>
                                                            </td>
                                                            <td style="min-width:310px;" colspan="3" align="center"><b><asp:Label ID="lblañoactual2" runat="server" Text="0"></asp:Label></b></td>
                                                        </tr>
                                                        <tr>
                                                            <td class="header-tbl" align="center">Op's</td>
                                                            <td class="header-tbl" align="center">Monto Original</td>
                                                            <td class="header-tbl" align="center">Margen</td>
                                                            <td class="header-tbl">
                                                                <br>
                                                            </td>
                                                            <td class="header-tbl" align="center">Op's</td>
                                                            <td class="header-tbl" align="center">Monto Original</td>
                                                            <td class="header-tbl" align="center">Margen</td>
                                                            <td class="header-tbl" align="center">Op's</td>
                                                            <td class="header-tbl" align="center">Monto Original</td>
                                                            <td class="header-tbl" align="center">Margen</td>
                                                        </tr>
                                                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                         <div class="col-lg-12">
                            <div class="box box-danger">
                                <div class="box-header with-border">
                                    <h4 class="box-title">% de Bateo</h4>

                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                            <i class="fa fa-minus"></i>
                                        </button>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="table table-responsive">
                                                <table bordercolor="black"  width="100%" border="1">
                                                    <tbody>
                                                        <tr>
                                                            <td class="header-tbl" rowspan="2" style="min-width:140px"></td>
                                                            <td style="min-width:500px;"  colspan="4" align="center"><b><asp:Label ID="lblañoanterior3" runat="server" Text="0"></asp:Label></b></td>
                                                            <td class="header-tbl" rowspan="7" style="max-width:5px; width:5px;"
                                                                <br>
                                                            </td>
                                                            <td style="min-width:500px;"  colspan="4" align="center"><b><asp:Label ID="lblañoactual3" runat="server" Text="0"></asp:Label></b></td>
                                                        </tr>
                                                        <tr>
                                                            <td class="header-tbl" align="center">Op's</td>
                                                            <td class="header-tbl" align="center">Monto Final</td>
                                                            <td class="header-tbl" align="center">Margen</td>
                                                            <td class="header-tbl" align="center">Días/Cierre</td>

                                                            <td class="header-tbl" align="center">Op's</td>
                                                            <td class="header-tbl" align="center">Monto Final</td>
                                                            <td class="header-tbl" align="center">Margen</td>
                                                            <td class="header-tbl" align="center">Días/Cierre</td>
                                                        </tr>
                                                        <asp:Repeater ID="repeater_baetos_Detalles" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td><%# Eval("desestatus") %></td>
                                                                    <td align="center"><%# Eval("numop") %></td>
                                                                    <td align="right"><%# presentacion.funciones.ValueMoneyMil(Convert.ToDecimal(Eval("montofinal")),0)  %></td>
                                                                    <td align="right"><%# presentacion.funciones.ValueMoneyMil(Convert.ToDecimal(Eval("margen")),0)  %></td>
                                                                    <td align="center"><%# Eval("dias_cierre") %></td>
                                                                    <td align="center"><%# Eval("numop2") %></td>
                                                                    <td align="right"><%# presentacion.funciones.ValueMoneyMil(Convert.ToDecimal(Eval("montofinal2")),0)  %></td>
                                                                    <td align="right"><%# presentacion.funciones.ValueMoneyMil(Convert.ToDecimal(Eval("margen2")),0)  %></td>
                                                                    <td align="center"><%# Eval("dias_cierre2") %></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                         <asp:Repeater ID="REPEATER_TOTAL_BATEO" runat="server">
                                                             <ItemTemplate>
                                                                 <tr>
                                                                    <td><b><%# Eval("descripcion") %></b></td>
                                                                    <td align="center"><b><%# Eval("numop") %></b></td>
                                                                    <td align="right"><b><%# presentacion.funciones.ValueMoneyMil(Convert.ToDecimal(Eval("montofinal")),0)  %></b></td>
                                                                    <td align="right"><b><%# presentacion.funciones.ValueMoneyMil(Convert.ToDecimal(Eval("margen")),0)  %></b></td>
                                                                    <td align="right"><b><%# Eval("dias_cierre") %></b></td>
                                                                    <td align="center"><b><%# Eval("numop2") %></b></td>
                                                                    <td align="right"><b><%# presentacion.funciones.ValueMoneyMil(Convert.ToDecimal(Eval("montofinal2")),0)  %></b></td>
                                                                    <td align="right"><b><%# presentacion.funciones.ValueMoneyMil(Convert.ToDecimal(Eval("margen2")),0)  %></b></td>
                                                                    <td align="right"><b><%# Eval("dias_cierre2") %></b></td>
                                                                 </tr>
                                                             </ItemTemplate>
                                                        </asp:Repeater>    
                                                        <tr>
                                                            <td height="20" colspan="10">
                                                                <br>
                                                            </td>
                                                        </tr>
                                                         <asp:Repeater ID="repetaer_promedio_bateo" runat="server">
                                                             <ItemTemplate>
                                                                 <tr style='<%# "color:white;background-color:"+Eval("bgcolor").ToString() +";"%>'>
                                                                     <td><font color="white"><%# Eval("desestatus") %></font></td>
                                                                     <td align="center"><font color="white"><%# Eval("numop") %>%</font></td>
                                                                     <td align="center"><font color="white"><%# Eval("montofinal") %>%</font></td>
                                                                     <td align="center"><font color="white"><%# Eval("margen") %>%</font></td>
                                                                     <td align="center"><font color="white"><%# Eval("dias_cierre") %></font></td>
                                                                     <td>
                                                                         <br>
                                                                     </td>
                                                                     <td align="center"><font color="white"><%# Eval("numop2") %>%</font></td>
                                                                     <td align="center"><font color="white"><%# Eval("montofinal2") %>%</font></td>
                                                                     <td align="center"><font color="white"><%# Eval("margen2") %>%</font></td>
                                                                     <td align="center"><font color="white"><%# Eval("dias_cierre2") %></font></td>
                                                                    
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
                         <div class="col-lg-12">
                            <div class="box box-danger">
                                <div class="box-header with-border">
                                    <h4 class="box-title">Cumplimiento de Cuota</h4>

                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                            <i class="fa fa-minus"></i>
                                        </button>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="table table-responsive">
                                                <table bordercolor="black" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td valign="top" style="min-width:600px"> 
                                                                <table bordercolor="black" cellspacing="0" cellpadding="0" width="100%" border="1">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td class="header-tbl"  style="min-width:100px" rowspan="2"></td>
                                                                            <td colspan="4"   style="min-width:500px" align="center"><b><asp:Label ID="lblañoanterior4" runat="server" Text="0"></asp:Label></b></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="header-tbl" align="center">Ventas</td>
                                                                            <td class="header-tbl" align="center">Margen</td>
                                                                            <td class="header-tbl" align="center">Cuota</td>
                                                                            <td class="header-tbl" align="center">%</td>
                                                                        </tr>

                                                                        <asp:Repeater ID="repeater_año_anterior_cumpli_cuota" runat="server">
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td><%# Eval("estatus") %></td>
                                                                                    <td align="right">$<%# Eval("Venta") %> K</td>
                                                                                    <td align="right">$<%# Eval("Margen") %> K</td>
                                                                                    <td align="right">$<%# Eval("Cuota") %> K</td>
                                                                                    <td align="right"><%# Eval("PCuota") %> %</td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                       
                                                                        <asp:Repeater ID="repeater_año_anterior_cumpli_cuota_total" runat="server">
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td><b>Total&nbsp;&nbsp;<%# Eval("año") %></b></td>
                                                                                    <td align="right"><b>$<%# Eval("Venta") %> K</b></td>
                                                                                    <td align="right"><b>$<%# Eval("Margen") %> K</b></td>
                                                                                    <td align="right"><b>$<%# Eval("Cuota") %> K</b></td>
                                                                                    
                                                                                    <td align="right"><b><%# Eval("PCuota") %> %</b></td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                       
                                                                        <tr>
                                                                            <td rowspan="2">
                                                                                <br>
                                                                                </B></td>
                                                                            <td colspan="4" align="center"><b><asp:Label ID="lblañoactual4" runat="server" Text="0"></asp:Label></b></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="header-tbl" align="center">Ventas</td>
                                                                            <td class="header-tbl" align="center">Margen</td>
                                                                            <td class="header-tbl" align="center">Cuota</td>
                                                                            <td class="header-tbl" align="center">%</td>
                                                                        </tr>
                                                                        
                                                                        <asp:Repeater ID="repeater_año_actual_cumpli_cuota" runat="server">
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td><%# Eval("nmes") %></td>
                                                                                    <td align="right">$<%# Eval("Venta") %> K</td>
                                                                                    <td align="right">$<%# Eval("Margen") %> K</td>
                                                                                    <td align="right">$<%# Eval("Cuota") %> K</td>
                                                                                    <td align="right"><%# Eval("PCuota") %> %</td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                       
                                                                          <asp:Repeater ID="repeater_año_actual_cumpli_cuota_total" runat="server">
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td><b>Total Actual&nbsp;&nbsp;<%# Eval("año") %></b></td>
                                                                                    <td align="right"><b>$<%# Eval("Venta") %> K</b></td>
                                                                                    <td align="right"><b>$<%# Eval("Margen") %> K</b></td>
                                                                                    <td align="right"><b>$<%# Eval("Cuota") %> K</b></td>
                                                                                    
                                                                                    <td align="right"><b><%# Eval("PCuota") %> %</b></td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                       
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                            <td valign="top" align="right">
                                                                <table bordercolor="black" cellspacing="0" cellpadding="0" style="min-width:200px" border="1">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td class="header-tbl" rowspan="2"><b>Visitas</b></td>
                                                                            <td colspan="4" align="center"><b><asp:Label ID="lblañoanterior5" runat="server" Text="0"></asp:Label></b></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="header-tbl" align="center">Visitas</td>
                                                                        </tr>
                                                                         <asp:Repeater ID="repeater_visitas_añoanterior" runat="server">
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td><%# Eval("estatus") %></td>
                                                                                    <td align="right"><%# Eval("visitas") %></td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                        <asp:Repeater ID="repeater_visitas_añoanterior_total" runat="server">
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td><b>Total&nbsp;&nbsp;<%# Eval("ano") %></b></td>
                                                                                    <td align="right"><b><%# Eval("visitas") %></b></td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                      
                                                                       
                                                                        <tr>
                                                                            <td rowspan="2">
                                                                                <br>
                                                                                </B></td>
                                                                            <td colspan="4" align="center"><b><asp:Label ID="lblañoactual6" runat="server" Text="0"></asp:Label></b></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="header-tbl" align="center">Visitas</td>
                                                                        </tr>
                                                                           <asp:Repeater ID="repeater_visitas_añoactual" runat="server">
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td><%# Eval("nmes") %></td>
                                                                                    <td align="right"><%# Eval("visitas") %></td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                        <asp:Repeater ID="repeater_visitas_añoactual_total" runat="server">
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td><b>Total Actual&nbsp;&nbsp;<%# Eval("ano") %></b></td>
                                                                                    <td align="right"><b><%# Eval("visitas") %></b></td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                      
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                        
                         <div class="col-lg-12">
                            <div class="box box-danger">
                                <div class="box-header with-border">
                                    <h4 class="box-title">Suficiencia de Inventario de Oportunidades</h4>

                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                            <i class="fa fa-minus"></i>
                                        </button>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="table table-responsive">
                                                <table bordercolor="black" cellspacing="0" cellpadding="0" border="1">
                                                    <tbody>
                                                        <asp:Repeater ID="repeater_suficiencia" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td class="header-tbl" height="30" style="min-width:350px;" colspan="2"><b>Suficiencia de Inventario de Oportunidades</b></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Eficacia en Venta</td>
                                                                    <td align="right"><%# Eval("eficacia") %> %</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Cuota Margen Mensual Promedio</td>
                                                                    <td align="right">$<%# Eval("cuotamensualpromedio") %> K</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Tiempo Estándar de Cierre</td>
                                                                    <td align="right"><%# Eval("diaspromcierre") %></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Tiempo Facturación Promedio</td>
                                                                    <td align="right"><%# Eval("diaspromfactur") %></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Margen Requerido en Forecast</td>
                                                                    <td align="right">$<%# Eval("margenforecast") %> K</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Valor Actual Margen Op</td>
                                                                    <td align="right">$<%# Eval("valoractualmargen") %> K</td>
                                                                </tr>
                                                                <tr class="header-tbl">
                                                                    <td><b>Sobrante/Faltante</b></td>
                                                                    <td align="right">$<%# Eval("faltante") %> K</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Valor Realización Inventario Op</td>
                                                                    <td align="right">$<%# Eval("valorinv") %> K</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Meses Cuota</td>
                                                                    <td align="right"><%# Eval("mesescuota") %></td>
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
                         <div class="col-lg-12">
                            <div class="box box-danger">
                                <div class="box-header with-border">
                                    <h4 class="box-title">Detalle de Inventario de Oportunidades 
                                    </h4>

                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                            <i class="fa fa-minus"></i>
                                        </button>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="table table-responsive">
                                                <table bordercolor="black" cellspacing="0" cellpadding="0" border="1">
                                                    <tbody>
                                                        <tr>
                                                            <td class="header-tbl" style="min-width:800px"  height="30"  colspan="3"><b>Detalle de Inventario de Oportunidades</b></td>
                                                        </tr>
                                                        <tr  class="header-tbl">
                                                            <td align="center" style="min-width:400px">Cliente</td>
                                                            <td align="center">Margen</td>
                                                            <td align="center">Op's</td>
                                                        </tr>
                                                        <asp:Repeater ID="repeater_clientes" runat="server">
                                                            <ItemTemplate>

                                                                <tr>
                                                                    <td><%# Eval("nombcli") %></td>
                                                                    <td align="right"><%# presentacion.funciones.ValueMoneyMil(Convert.ToDecimal(Eval("margenop")),0) %></td>
                                                                    <td align="right"><%# Eval("numop") %></td>
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
                </div>
            </div>

        </div>
        
    </div>
       <div class="modal fade bs-example-modal-lg" tabindex="-1" id="myModal" role="dialog" aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-lg" role="document">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="lnkfiltros" EventName="Click" />
                        <asp:PostBackTrigger ControlID="lnkguardar"/>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span></button>
                                <h4 class="modal-title">Filtros</h4>
                            </div>
                            <div class="modal-body" id="div_modalbodyfiltros" runat="server">
                                <div class="row">
                                    <div class="col-lg-12 col-xs-12">
                                        <h6><strong><i class="fa fa-users" aria-hidden="true"></i>&nbsp;Seleccione el empleado a consultar</strong>
                                            &nbsp; 
                                        <asp:CheckBox ID="cbxnoactivo" Text="Ver no Activos" Checked="true" runat="server" />
                                        </h6>
                                        <div class="input-group input-group-sm">
                                            <asp:TextBox
                                                onfocus="this.select();" ID="txtfilterempleado" CssClass=" form-control"
                                                placeholder="Ingrese un filtro" runat="server"></asp:TextBox>
                                            <span class="input-group-btn">
                                                <asp:LinkButton ID="lnksearch" CssClass="btn btn-primary btn-flat"
                                                    OnClientClick="return ChangedTextLoad2();" OnClick="lnksearch_Click" runat="server">
                                                <i class="fa fa-search" aria-hidden="true"></i>
                                                </asp:LinkButton>
                                            </span>
                                        </div>
                                        <asp:Image ID="imgloadempleado" Style="display: none;" ImageUrl="~/img/load.gif" runat="server" />
                                        <label id="lblbemp" runat="server" style="display: none; color: #1565c0">Buscando Empleados</label>
                                        <asp:DropDownList Visible="true" ID="ddlempleado_a_consultar" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                              
                            </div>
                            <div class="modal-footer ">
                                <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Cerrar</button>
                                <asp:LinkButton OnClientClick="return false;" ID="lnkcargando" CssClass="btn btn-primary btn-flat" runat="server" Style="display: none;">
                                            <i class="fa fa-refresh fa-spin fa-fw"></i>
                                            <span class="sr-only">Loading...</span>&nbsp;Generando reporte
                                </asp:LinkButton>                              
                                <asp:LinkButton ID="lnkguardar" CssClass="btn btn-primary btn-flat"
                                    OnClientClick="return ConfirmwidgetProyectoModal();" OnClick="lnkguardar_Click" runat="server">
                                            <i class="fa fa-database" aria-hidden="true"></i>&nbsp;Generar reporte
                                </asp:LinkButton>
                            </div>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
</asp:Content>
