<%@ Page Title="GC Approval" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="eGC.gcapproval._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearch">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <h5>GC Approval</h5>
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
                                                <asp:TextBox ID="txtSearch"
                                                    runat="server"
                                                    CssClass="form-control"
                                                    placeholder="Search..."></asp:TextBox>
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
                                        AllowSorting="True"
                                        EmptyDataText="No Record(s) found"
                                        ShowHeaderWhenEmpty="True"
                                        DataKeyNames="Id"
                                        OnRowDataBound="gvGC_RowDataBound"
                                        OnPageIndexChanging="gvGC_PageIndexChanging"
                                        OnRowCommand="gvGC_RowCommand"
                                        OnSorting="gvGC_Sorting"
                                        DataSourceID="LinqDataSource1">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Row Id" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="GC Number" SortExpression="GCNumber">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblGCNo" runat="server" Text='<%# Eval("GCNumber") %>' CommandName="redirectGC" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Guest ID" SortExpression="GuestId">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnGuestId" runat="server" Text='<%# Eval("GuestId") %>' CommandName="redirectGuest" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="FullName" HeaderText="Name" SortExpression="FullName" />
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company" SortExpression="CompanyName" />
                                            <asp:BoundField DataField="ArrivalDate" HeaderText="Arrival Date" DataFormatString="{0:d}" SortExpression="ArrivalDate" />
                                            <asp:BoundField DataField="CheckoutDate" HeaderText="Checkout Date" DataFormatString="{0:d}" SortExpression="CheckoutDate" />
                                            <asp:BoundField DataField="TotalValue" HeaderText="Grand Total" SortExpression="TotalValue" />
                                            <asp:BoundField DataField="Status" HeaderText="Approval Status" SortExpression="Status" />
                                        </Columns>
                                        <PagerStyle CssClass="pagination-ys" />
                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:LinqDataSource ID="LinqDataSource1"
        runat="server"
        OnSelecting="LinqDataSource1_Selecting">
    </asp:LinqDataSource>
</asp:Content>
