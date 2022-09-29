<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BookSpot.aspx.cs" Inherits="H1___Campingpladsen.BookSpot" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:DataList ID="datalist_spotToBook" runat="server">
        <ItemTemplate>
            <%# Eval("spot_name") %>
            <%# Eval("spot_type") %>
            <%# Eval("spot_price_high") %>
            <%# Eval("spot_price_low") %>
        </ItemTemplate>
    </asp:DataList>
</asp:Content>
