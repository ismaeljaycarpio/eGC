<%@ Page Title="GC Records" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="company-gc-records.aspx.cs" Inherits="eGC.company.company_gc_records" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearch">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <h5>GC Records</h5>
                    </div>
                    <div class="panel-body">
                        <asp:UpdatePanel ID="upApproval" runat="server">
                            <ContentTemplate>
                                <div class="form-inline">
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtSearch"
                                                    runat="server"
                                                    CssClass="form-control"
                                                    placeholder="Search..."></asp:TextBox>
                                                <span class="input-group-btn">
                                                    <asp:Button ID="btnSearch"
                                                        runat="server"
                                                        CssClass="btn btn-primary"
                                                        Text="Go"
                                                        OnClick="btnSearch_Click" />
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
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
                                        OnRowCommand="gvGC_RowCommand"
                                        DataSourceID="GCRecordDataSource">
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

                                            <asp:TemplateField HeaderText="ID" SortExpression="GuestId">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnGuestId" runat="server" Text='<%# Eval("GuestId") %>' CommandName="redirectGuest" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="FullName" HeaderText="Name" SortExpression="FullName" />
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company" SortExpression="CompanyName" />
                                            <asp:BoundField DataField="ArrivalDate" HeaderText="Arrival Date" DataFormatString="{0:d}" SortExpression="ArrivalDate" />
                                            <asp:BoundField DataField="CheckoutDate" HeaderText="Checkout Date" DataFormatString="{0:d}" SortExpression="CheckoutDate" />
                                            <asp:BoundField DataField="TotalValue" HeaderText="Total" SortExpression="TotalValue" />

                                            <asp:TemplateField HeaderText="Approval" SortExpression="Approval">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblApproval" runat="server" Text='<%# Eval("Approval") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="GC Status" SortExpression="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGCStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="CancelledDate" HeaderText="Date Cancelled" DataFormatString="{0:d}" SortExpression="CancelledDate" />
                                            <asp:TemplateField HeaderText="Cancellation Reason" SortExpression="CancellationReason">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCancellationReason" runat="server" Text='<%# Eval("CancellationReason") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

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

    <asp:LinqDataSource ID="GCRecordDataSource"
        runat="server"
        OnSelecting="GCRecordDataSource_Selecting">
    </asp:LinqDataSource>

</asp:Content>
