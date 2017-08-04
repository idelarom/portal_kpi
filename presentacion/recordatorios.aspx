<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="recordatorios.aspx.cs" Inherits="presentacion.recordatorios" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    .day-style
    {
        background-color:#ef5350;
         /*background-image: url("../Images/gplus.png") !important;*/
        background-repeat: no-repeat;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h4 class="page-header">Mis Recordatorios</h4>
        </div>
        <div class="col-lg-12">
            <telerik:RadCalendar RenderMode="Lightweight" ID="rdcalendar" runat="server"
                Style="float: left;" Skin="Bootstrap" Width="100%" EnableMultiSelect="false" 
                  OnSelectionChanged="rdcalendar_SelectionChanged" AutoPostBack="true" >
            </telerik:RadCalendar>
        </div>
    </div>
</asp:Content>
