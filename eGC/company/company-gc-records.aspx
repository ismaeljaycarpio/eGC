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
                                        <asp:TextBox ID="txtSearch"
                                            runat="server"
                                            CssClass="form-control"
                                            placeholder="Search..."></asp:TextBox>
                                    </div>

                                    <%--<div class="form-group">
                                        <asp:TextBox ID="txtDateFrom"
                                            runat="server"
                                            placeholder="Date From"
                                            CssClass="form-control"
                                            data-provide="datepicker"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <asp:TextBox ID="txtDateTo"
                                            runat="server"
                                            placeholder="Date To"
                                            CssClass="form-control"
                                            data-provide="datepicker"></asp:TextBox>
                                    </div>--%>

                                    <asp:Button ID="btnSearch"
                                        runat="server"
                                        CssClass="btn btn-primary"
                                        Text="Go"
                                        OnClick="btnSearch_Click" />
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
                                                    <asp:Label ID="lbtnGuestId" runat="server" Text='<%# Eval("GuestId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="FullName" HeaderText="Name" SortExpression="FullName" />
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company" SortExpression="CompanyName" />
                                            <asp:BoundField DataField="ExpiryDate" HeaderText="Expiration Date" DataFormatString="{0:d}" SortExpression="ExpiryDate" />

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

                                            <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" />

                                            <asp:BoundField DataField="CancelledDate" HeaderText="Date Cancelled" DataFormatString="{0:d}" SortExpression="CancelledDate" />
                                            <asp:TemplateField HeaderText="Cancellation Reason" SortExpression="CancellationReason">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCancellationReason" runat="server" Text='<%# Eval("CancellationReason") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <PagerStyle CssClass="pagination-ys" />
                                    </asp:GridView>

                                    <asp:Button ID="btnExport"
                                        runat="server"
                                        Text="Export to Excel"
                                        OnClick="btnExport_Click"
                                        CssClass="btn btn-default btn-sm pull-right" />

                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnExport" />
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
