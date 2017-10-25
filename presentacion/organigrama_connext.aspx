<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="organigrama_connext.aspx.cs" Inherits="presentacion.organigrama_connext" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <link rel="stylesheet" href="primitive/js/jquery/ui-lightness/jquery-ui-1.10.2.custom.css" />
    <script type="text/javascript" src="primitive/js/jquery/jquery-ui-1.10.2.custom.min.js"></script>
    

    <script type="text/javascript" src="primitive/js/primitives.min.js"></script>
    <link href="primitive/css/primitives.latest.css" media="screen" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        
        function CargarDiagramaEmpleados() {
            $.ajax({
                type: "POST",
                url: "organigrama_connext.aspx/GetEmpleadosInvolucrados",
                contentType: "application/json; charset=utf-8",
                success: OnSuccess,
                error: function (result, status, err) {
                    console.log("error", result.responseText);
                }
            });
            function OnSuccess(response) {
                var items_post = response.d;
                if (items_post.length > 0) {
                    var options = new primitives.orgdiagram.Config();
                    var items = [];
                    $.each(items_post, function (index, val) {
                        var x = new primitives.orgdiagram.ItemConfig({
                            id: val.idpinvolucrado,
                            parent: val.id_parent,
                            title: val.rol,
                            description: val.nombre,
                            image: "img/user.png",
                            email: val.correo,
                            templateName: "contactTemplate",
                            href: "mailto:" + val.correo,
                            itemTitleColor: primitives.common.Colors.Red
                        });
                        items.push(x);

                    });
                    var buttons = [];
                    buttons.push(new primitives.orgdiagram.ButtonConfig("delete", "ui-icon-close", "Delete"));
                    buttons.push(new primitives.orgdiagram.ButtonConfig("properties", "ui-icon-gear", "Info"));
                    buttons.push(new primitives.orgdiagram.ButtonConfig("add", "ui-icon-person", "Add"));
                    //  options.buttons = buttons;
                    options.hasButtons = primitives.common.Enabled.True;
                    options.hasSelectorCheckbox = primitives.common.Enabled.False;
                    options.onButtonClick = function (e, data) {
                        var message = "User clicked '" + data.name + "' button for item '" + data.context.title + "'.";
                        alert(message);
                    };

                    options.items = items;
                    options.cursorItem = 0;
                    options.templates = [getContactTemplate()];
                    options.onItemRender = onTemplateRender;
                    options.hasSelectorCheckbox = primitives.common.Enabled.False;
                    options.onMouseClick = function (e, data) {
                        var id = data.context.id;
                        var myHidden = document.getElementById('<%= hdfid_involucrado.ClientID %>');

                        myHidden.value = id;
                       <%-- $("#<%= div_nievoinvo.ClientID%>").show();
                        $("#<%= div_listempleados.ClientID%>").hide();
                        document.getElementById('<%= btneditarinvol.ClientID%>').click();--%>
                    }
                    jQuery("#basicdiagram_employeed").orgDiagram(options);

                    function onTemplateRender(event, data) {
                        var hrefElement = data.element.find("[name=readmore]");
                        var emailElement = data.element.find("[name=email]");
                        switch (data.renderingMode) {
                            case primitives.common.RenderingMode.Create:
                                /* Initialize widgets here */
                                hrefElement.click(function (e) {
                                    /* Block mouse click propogation in order to avoid layout updates before server postback*/
                                    primitives.common.stopPropagation(e);
                                });
                                emailElement.click(function (e) {
                                    /* Block mouse click propogation in order to avoid layout updates before server postback*/
                                    primitives.common.stopPropagation(e);
                                });
                                break;
                            case primitives.common.RenderingMode.Update:
                                /* Update widgets here */
                                break;
                        }

                        var itemConfig = data.context;

                        if (data.templateName == "contactTemplate") {
                            data.element.find("[name=titleBackground]").css({ "background": itemConfig.itemTitleColor });

                            data.element.find("[name=photo]").attr({ "src": itemConfig.image });
                            hrefElement.attr({ "href": itemConfig.href });
                            emailElement.attr({ "href": ("mailto:" + itemConfig.email + "") });

                            var fields = ["title", "description", "phone", "email"];
                            for (var index = 0; index < fields.length; index++) {
                                var field = fields[index];

                                var element = data.element.find("[name=" + field + "]");
                                if (element.text() != itemConfig[field]) {
                                    element.text(itemConfig[field]);
                                }
                            }
                        }
                    }

                    function getContactTemplate() {
                        var result = new primitives.orgdiagram.TemplateConfig();
                        result.name = "contactTemplate";

                        result.itemSize = new primitives.common.Size(220, 90);
                        result.minimizedItemSize = new primitives.common.Size(3, 3);
                        result.highlightPadding = new primitives.common.Thickness(2, 2, 2, 2);

                        var itemTemplate = jQuery(
                          '<div class="bp-item bp-corner-all bt-item-frame">'
                            + '<div name="titleBackground" class="bp-item bp-corner-all bp-title-frame" style="top: 2px; left: 2px; width: 216px; height: 20px;">'
                                + '<div name="title" class="bp-item bp-title" style="top: 3px; left: 6px; width: 208px; height: 18px;">'
                                + '</div>'
                            + '</div>'
                            + '<div name="phone" class="bp-item" style="top: 62px; left: 9px; width: 162px; height: 36px; font-size: 10px;"></div>'
                            + '<div class="bp-item" style="top: 44px; left: 9px; width: 185px; height: 18px; font-size: 12px;"><a name="email"></a></div>'
                            + '<div name="description" class="bp-item" style="top: 26px; left: 9px; width: 162px; height: 18px; font-size: 12px;"></div>'

                        + '</div>'
                        ).css({
                            width: result.itemSize.width + "px",
                            height: result.itemSize.height + "px"
                        }).addClass("bp-item bp-corner-all bt-item-frame");

                        result.itemTemplate = itemTemplate.wrap('<div>').parent().html();

                        return result;
                    }

                }

            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h4 class="page-header">Organigrama General Connext</h4>
        </div>
        <div class="col-lg-12">
            <div id="basicdiagram_employeed" style="border-style: solid; border-width: 1px; width: 100%; height: 450px">
            </div>
        </div>
    </div>

    
    <asp:HiddenField ID="hdfid_involucrado" runat="server" />
</asp:Content>
