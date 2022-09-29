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
                    <asp:Calendar runat="server" type="date" id="calendar_dateStart" />
                </div>
                <div class="col-xs-6">
                    Hjemrejse
                    <asp:Calendar runat="server" type="date" id="calendar_dateEnd" />
                </div>
            </div>
            <!-- Dropdown menu to choose the types of spots you're looking to book -->
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

    <!-- Datalist displaying all the available spots for the selected dates -->
    <asp:DataList ID="datalist_spots" runat="server">
        <ItemTemplate>
                    <div class="list-group">
                        <a href="/BookSpot?id=<%# Eval("spot_id") %>&date=<%=calendar_dateStart.SelectedDate %>" class="list-group-item">
                            <%# Eval("spot_name") %><br />
                            <%# Eval("spot_type") %>
                        </a>
                    </div>
        </ItemTemplate>
    </asp:DataList>
</asp:Content>
