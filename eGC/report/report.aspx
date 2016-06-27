<%@ Page Title="Report" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="report.aspx.cs" Inherits="eGC.report.report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearch">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-success">
                    <div class="panel-heading">
                        <h5>GC Records</h5>
                    </div>
                    <div class="panel-body">
                        <asp:UpdatePanel ID="upGCRecords" runat="server">
                            <ContentTemplate>
                                <div class="form-inline">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtSearch"
                                            runat="server"
                                            CssClass="form-control"
                                            placeholder="Search..."></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlApproval" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="0" Text="-- Select Approval --"></asp:ListItem>
                                            <asp:ListItem Value="Pending" Text="Pending"></asp:ListItem>
                                            <asp:ListItem Value="Approved" Text="Approved"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlGCStatus" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="0" Text="-- Select GC Status --"></asp:ListItem>
                                            <asp:ListItem Value="Waiting" Text="Waiting"></asp:ListItem>
                                            <asp:ListItem Value="Used" Text="Used"></asp:ListItem>
                                            <asp:ListItem Value="Completed" Text="Completed"></asp:ListItem>
                                            <asp:ListItem Value="Cancelled" Text="Cancelled"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlCompanyName"
                                            runat="server"
                                            CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>

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

                                            <asp:TemplateField HeaderText="ID" SortExpression="GuestId" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnGuestId" runat="server" Text='<%# Eval("GuestId") %>' CommandName="redirectGuest" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ID" SortExpression="GuestIdName" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnGuestIdName" runat="server" Text='<%# Eval("GuestIdName") %>' CommandName="redirectGuest" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="GC Number" SortExpression="GCNumber">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblGCNo" runat="server" Text='<%# Eval("GCNumber") %>' CommandName="redirectGC" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Name" SortExpression="FullName">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnFullName" runat="server" Text='<%# Eval("FullName") %>' CommandName="redirectGuest" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Company" SortExpression="CompanyName">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnCompany" runat="server" Text='<%# Eval("CompanyName") %>' CommandName="redirectCompany" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
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

                                            <asp:BoundField DataField="DateIssued" HeaderText="Date Issued" DataFormatString="{0:d}" SortExpression="DateIssued" />
                                            <asp:BoundField DataField="ExpiryDate" HeaderText="Expiration Date" DataFormatString="{0:d}" SortExpression="ExpiryDate" />

                                            <asp:BoundField DataField="CancelledDate" HeaderText="Date Cancelled" DataFormatString="{0:d}" SortExpression="CancelledDate" />
                                            <asp:TemplateField HeaderText="Cancellation Reason" SortExpression="CancellationReason">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCancellationReason" runat="server" Text='<%# Eval("CancellationReason") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <PagerStyle CssClass="pagination-ys" />
                                    </asp:GridView>

                                    <div class="pull-right">
                                        <asp:LinkButton ID="lbtnExport"
                                            OnClick="btnExport_Click"
                                            CssClass="btn btn-default btn-sm"
                                            runat="server">
                                            <span aria-hidden="true" class="glyphicon glyphicon-list"></span>Export to Excel
                                        </asp:LinkButton>
                                    </div>

                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="lbtnExport" />
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
