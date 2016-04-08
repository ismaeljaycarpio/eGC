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
                                    <div class="col-xs-3">
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
                                            <div class="col-sm-10">
                                                <div class="input-group">
                                                    <span class="input-group-btn">
                                                        <asp:Button ID="btnSearch"
                                                            runat="server"
                                                            CssClass="btn btn-primary"
                                                            Text="Go"
                                                            OnClick="btnSearch_Click" />
                                                    </span>
                                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search..."></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="table-responsive">
                                        <asp:GridView ID="gvGC"
                                            runat="server"
                                            CssClass="table table-striped table-hover dataTable"
                                            GridLines="None"
                                            AutoGenerateColumns="false"
                                            AllowPaging="true"
                                            EnablePersistedSelection="true"
                                            AllowSorting="true"
                                            EmptyDataText="No Record(s) found"
                                            ShowHeaderWhenEmpty="true"
                                            DataKeyNames="Id"
                                            OnRowDataBound="gvGC_RowDataBound"
                                            OnPageIndexChanging="gvGC_PageIndexChanging"
                                            OnRowCommand="gvGC_RowCommand"
                                            OnSorting="gvGC_Sorting"
                                            PageSize="10">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Row Id" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Guest ID">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnGuestId" runat="server" Text='<%# Eval("GuestId") %>' CommandName="selectGuest" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="GC Number">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblGCNo" runat="server" Text='<%# Eval("GCNumber") %>' CommandName="redirectGC" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="FullName" HeaderText="Name" />
                                                <asp:BoundField DataField="CompanyName" HeaderText="Company" />
                                                <asp:BoundField DataField="Number" HeaderText="Contact No" />

                                                <asp:BoundField DataField="ArrivalDate" HeaderText="Arrival Date" />
                                                <asp:BoundField DataField="CheckoutDate" HeaderText="Checkout Date" />
                                                <asp:BoundField DataField="Status" HeaderText="Approval" />
                                                <asp:BoundField DataField="TotalValue" HeaderText="Grand Total" DataFormatString="{0:C}" />
                                            </Columns>
                                            <PagerStyle CssClass="pagination-ys" />
                                        </asp:GridView>
                                </ContentTemplate>
                                <Triggers>
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
