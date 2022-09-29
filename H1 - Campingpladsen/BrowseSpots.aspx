<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BrowseSpots.aspx.cs" Inherits="H1___Campingpladsen.BrowseSpots" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="panel panel-primary">
            <!-- Panel to customize search criteria -->
            <div class="panel-heading">Søgekriterier</div>
            <!-- Calendar - choose which dates you're planning to book for -->
            <div class="panel-body">
                <div class="col-xs-6">
                    Udrejse
                    <input runat="server" type="date" id="date_start">
                </div>
                <div class="col-xs-6">
                    Hjemrejse
                    <input runat="server" type="date" id="date_end" />
                </div>
            </div>
            <div class="panel-body">
                <div class="col-xs-12">
                <div class="dropdown">
                    Pladstype
                    <button class="btn btn-default dropdown-toggle" type="button" id="dropdown_spotTypes" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                        Typer
                        <span class="caret"></span>
                    </button>
                    <asp:CheckBoxList class="dropdown-menu" id="cbl_spotTypes" runat="server" OnSelectedIndexChanged="cbl_spotTypes_SelectedIndexChanged">
                    </asp:CheckBoxList>
                </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Datalist displaying all the available forms for the dates -->
    <asp:DataList ID="datalist_spots" runat="server">
        <ItemTemplate>
            <table class="table table-hover table-bordered">
                <!-- Table entries change color on hovering, and are bordered to differentiate between spots -->
                <tbody>
                    <tr>
                        <td>
                            <%# Eval("spot_name") %>
                        </td>
                    </tr>
                </tbody>
            </table>
        </ItemTemplate>
    </asp:DataList>
</asp:Content>
