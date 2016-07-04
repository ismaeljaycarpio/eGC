<%@ Page Title="Audit Trail" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="audit-trail.aspx.cs" Inherits="eGC.admin.audit_trail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearch" CssClass="row">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h5>Audit Trail</h5>
                </div>
                <div class="panel-body">
                    <div class="form-inline">
                        <div class="form-group">
                            <asp:TextBox ID="txtSearch"
                                runat="server"
                                CssClass="form-control"
                                placeholder="Search..."></asp:TextBox>
                            <asp:Button ID="btnSearch"
                                runat="server"
                                CssClass="btn btn-primary"
                                Text="Go"
                                OnClick="btnSearch_Click" />
                        </div>
                    </div>
                </div>

                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="upUsers" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvAuditTrail"
                                    runat="server"
                                    CssClass="table table-striped table-hover dataTable"
                                    GridLines="None"
                                    AutoGenerateColumns="False"
                                    AllowPaging="True"
                                    ShowHeader="true"
                                    ShowFooter="True"
                                    AllowSorting="True"
                                    EmptyDataText="No Record(s) found"
                                    ShowHeaderWhenEmpty="True"
                                    DataKeyNames="Id"
                                    OnRowDataBound="gvAuditTrail_RowDataBound"
                                    DataSourceID="AuditTrailDataSource">
                                    <Columns>
                                        <asp:BoundField DataField="User" HeaderText="User" SortExpression="User" />
                                        <asp:BoundField DataField="Action" HeaderText="Action" SortExpression="Action" />
                                        <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                                        <asp:BoundField DataField="AssociatedId" HeaderText="Associated ID" SortExpression="AssociatedId" />
                                        <asp:BoundField DataField="ActionDate" HeaderText="Date" SortExpression="ActionDate" />
                                    </Columns>
                                    <PagerStyle CssClass="pagination-ys" />
                                </asp:GridView>
                                <asp:Label ID="lblRowCount" runat="server"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:LinqDataSource ID="AuditTrailDataSource"
        OnSelecting="AuditTrailDataSource_Selecting"
        OnSelected="AuditTrailDataSource_Selected"
        runat="server">
    </asp:LinqDataSource>

</asp:Content>
