<%@ Page Title="Comercial" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="reporte_performance_comercial.aspx.cs" Inherits="presentacion.reporte_performance_comercial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .header-tbl {
            background-color: #DE3230;
            color: white;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h4>Performance Comercial</h4>
        </div>
    </div>

    <div class="row">
        <div class="col-lg-12">
            <div class="box box-danger box-solid">
                <div class="box-header with-border">
                    <h3 class="box-title">--Nombre del empleado--</h3>
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
                                                            <td class="header-tbl" rowspan="6" width="5">
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
                                                       <%-- <tr>
                                                            <td>Anteriores a 2016</td>
                                                            <td align="center">0</td>
                                                            <td align="right">$0 K</td>
                                                            <td align="right">$0 K</td>
                                                            <td align="center">Q1</td>
                                                            <td align="center">0</td>
                                                            <td align="right">$0 K</td>
                                                            <td align="right">$0 K</td>
                                                            <td align="center">0</td>
                                                            <td align="right">$0 K</td>
                                                            <td align="right">$0 K</td>
                                                        </tr>
                                                        <tr>
                                                            <td>2016</td>
                                                            <td align="center">0</td>
                                                            <td align="right">$0 K</td>
                                                            <td align="right">$0 K</td>
                                                            <td align="center">Q2</td>
                                                            <td align="center">0</td>
                                                            <td align="right">$0 K</td>
                                                            <td align="right">$0 K</td>
                                                            <td align="center">0</td>
                                                            <td align="right">$0 K</td>
                                                            <td align="right">$0 K</td>
                                                        </tr>
                                                        <tr>
                                                            <td>2017</td>
                                                            <td align="center">0</td>
                                                            <td align="right">$0 K</td>
                                                            <td align="right">$0 K</td>
                                                            <td align="center">Q3</td>
                                                            <td align="center">0</td>
                                                            <td align="right">$0 K</td>
                                                            <td align="right">$0 K</td>
                                                            <td align="center">0</td>
                                                            <td align="right">$0 K</td>
                                                            <td align="right">$0 K</td>
                                                        </tr>
                                                        <tr>
                                                            <td><b>Total</b></td>
                                                            <td align="center"><b>0</b></td>
                                                            <td align="right"><b>$0 K</b></td>
                                                            <td align="right"><b>$0 K</b></td>
                                                            <td align="center">Q4</td>
                                                            <td align="center">0</td>
                                                            <td align="right">$0 K</td>
                                                            <td align="right">$0 K</td>
                                                            <td align="center">0</td>
                                                            <td align="right">$0 K</td>
                                                            <td align="right">$0 K</td>
                                                        </tr>--%>
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
</asp:Content>
