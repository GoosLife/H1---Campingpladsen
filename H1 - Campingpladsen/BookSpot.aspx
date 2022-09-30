<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BookSpot.aspx.cs" Inherits="H1___Campingpladsen.BookSpot" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Demonstration that I can get and display all data from this booking -->
    <asp:DataList ID="datalist_spotToBook" runat="server">
        <ItemTemplate>
            <%# Eval("spot_name") %>
            <%# Eval("spot_type") %>
            <%# Eval("spot_price") %>
        </ItemTemplate>
    </asp:DataList>

    Voksne:
    <input type="number" min="0" max="6" value="0" runat="server" id="number_adults"/>
    Børn:
    <input type="number" min="0" max="6" value="0" runat="server" id="number_children" />
    Hunde:
    <input type="number" min="0" value="0" runat="server" id="number_dogs" />
    Sengelinned:
    <input type="checkbox" runat="server" id="cb_bedLinen" />
    Slutrengøring:
    <input type="checkbox" runat="server" id="cb_exitCleaning"/>
    Cykelleje antal:
    <input type="number" min="0" max="12" value="0" runat="server" id="number_bikes" />
    Cykelleje dage:
    <input type="number" min="0" max="7" value="0" runat="server" id="number_bikeDays" />
    Adgang til badeland, voksne:
    <input type="number" min="0" max="6" value="0" runat="server" id="number_waterParkPassAdults" />
    Adgang til badeland, børn:
    <input type="number" min="0" max="6" value="0" runat="server" id="number_waterParkPassChildren" />

    <button class="btn btn-success" runat="server">Book now</button>
</asp:Content>
