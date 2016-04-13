<%@ Page Title="Guest Arrival Monitoring" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frontoffice.aspx.cs" Inherits="eGC.fo.frontoffice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearch">
        <div class="row">
            <div class="col-md-12">
                <div class="panel-group">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h5>Guest Profile</h5>
                        </div>

                        <div class="panel-body">
                            <asp:UpdatePanel ID="gProfile" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-5">
                                        <asp:Image ID="imgProfile"
                                            runat="server"
                                            AlternateText="Profile Image"
                                            Height="200"
                                            Width="200" />
                                        <asp:Image ID="IDPic"
                                            runat="server"
                                            AlternateText="ID Image"
                                            Height="200"
                                            Width="200" />
                                    </div>

                                    <div class="col-xs-3">
                                        <label for="txtName">Name</label>
                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="col-xs-4">
                                        <label for="txtGuestId">Guest ID</label>
                                        <asp:TextBox ID="txtGuestId" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="col-xs-3">
                                        <label for="txtArrival">Arrival</label>
                                        <asp:TextBox ID="txtArrival" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="col-xs-3">
                                        <label for="txtCheckout">Checkout Date</label>
                                        <asp:TextBox ID="txtCheckout" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="col-xs-3">
                                        <label for="txtStatus">GC Status</label>
                                        <asp:TextBox ID="txtStatus" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="gvGC" EventName="RowCommand" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h5>Guest List</h5>
                        </div>
                        <div class="panel-body">
                            <asp:UpdatePanel ID="upApproval" runat="server">
                                <ContentTemplate>
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search..."></asp:TextBox>
                                                    <span class="input-group-btn">
                                                        <asp:Button ID="btnSearch"
                                                            runat="server"
                                                            CssClass="btn btn-primary"
                                                            Text="Go"
                                                            OnClick="btnSearch_Click" />
                                                    </span>
                                                    

                                                    <div class="pull-right">
                                                        <asp:Button ID="btnExport"
                                                            runat="server"
                                                            Text="Export to Excel"
                                                            OnClick="btnExport_Click"
                                                            CssClass="btn btn-default btn-sm" />
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="table-responsive">
                                        <asp:GridView ID="gvGC"
                                            runat="server"
                                            CssClass="table table-striped table-hover dataTable"
                                            GridLines="None"
                                            AutoGenerateColumns="False"
                                            AllowPaging="True"
                                            EnablePersistedSelection="True"
                                            AllowSorting="True"
                                            EmptyDataText="No Record(s) found"
                                            ShowHeaderWhenEmpty="True"
                                            DataKeyNames="Id"
                                            OnRowCommand="gvGC_RowCommand"
                                            DataSourceID="LinqDataSource1">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Row Id" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Guest ID" SortExpression="GuestId">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnGuestId" runat="server" Text='<%# Eval("GuestId") %>' CommandName="selectGuest" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="GC Number" SortExpression="GCnumber">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblGCNo" runat="server" Text='<%# Eval("GCNumber") %>' CommandName="redirectGC" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="FullName" HeaderText="Name" SortExpression="FullName" />
                                                <asp:BoundField DataField="CompanyName" HeaderText="Company" SortExpression="CompanyName" />
                                                <asp:BoundField DataField="Number" HeaderText="Contact No" SortExpression="Number" />

                                                <asp:BoundField DataField="ArrivalDate" HeaderText="Arrival Date" DataFormatString="{0:d}" SortExpression="ArrivalDate" />
                                                <asp:BoundField DataField="CheckoutDate" HeaderText="Checkout Date" DataFormatString="{0:d}" SortExpression="CheckoutDate" />
                                                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                                                <asp:BoundField DataField="TotalValue" HeaderText="Grand Total" DataFormatString="{0:C}" SortExpression="TotalValue" />
                                            </Columns>
                                            <PagerStyle CssClass="pagination-ys" />
                                        </asp:GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                                    <asp:PostBackTrigger ControlID="btnExport" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:GridView ID="GridView1" runat="server"></asp:GridView>
    <asp:LinqDataSource ID="LinqDataSource1"
        runat="server"
        OnSelecting="LinqDataSource1_Selecting">
    </asp:LinqDataSource>
</asp:Content>
