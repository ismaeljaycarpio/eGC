<%@ Page Title="GC Approval" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="eGC.gcapproval._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearch">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h5>GC Approval</h5>
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

                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlCompanyName"
                                            CssClass="form-control"
                                            runat="server">
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
                                        DataSourceID="LinqDataSource1">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Row Id" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnGuestId" runat="server" Text='<%# Eval("GuestId") %>' CommandName="redirectGuest" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="GC Number" SortExpression="GCNumber">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblGCNo" runat="server" Text='<%# Eval("GCNumber") %>' CommandName="redirectGC" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ID" SortExpression="GuestIdName" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnGuestIdName" runat="server" Text='<%# Eval("GuestIdName") %>' CommandName="redirectGuest" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Name" SortExpression="FullName">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnFullName" runat="server" Text='<%# Eval("FullName") %>' CommandName="redirectGuest" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="CompanyName" HeaderText="Company" SortExpression="CompanyName" />
                                            
                                            <asp:TemplateField HeaderText="Approval" SortExpression="Approval" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblApproval" runat="server" Text='<%# Eval("Approval") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Status" SortExpression="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGCStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" />
                                            <asp:BoundField DataField="ExpiryDate" HeaderText="Expiration Date" DataFormatString="{0:d}" SortExpression="ExpiryDate" />
                                            <asp:BoundField DataField="CancelledDate" HeaderText="Date Cancelled" DataFormatString="{0:d}" SortExpression="CancelledDate" />

                                            <asp:TemplateField HeaderText="Cancellation Reason" SortExpression="CancellationReason">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCancellationReason" runat="server" Text='<%# Eval("CancellationReason") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <%--<asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnApprove"
                                                        runat="server"
                                                        CommandName="approveRecord"
                                                        Text="Approve"
                                                        CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'
                                                        CssClass="btn btn-success btn-sm" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnDisapprove"
                                                        runat="server"
                                                        Text="Disapprove"
                                                        CommandName="disapproveRecord"
                                                        CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'
                                                        CssClass="btn btn-danger btn-sm" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnDelete"
                                                        runat="server"
                                                        Text="Delete"
                                                        CommandName="deleteRecord"
                                                        CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'
                                                        CssClass="btn btn-danger btn-sm" />
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

    <!-- Approve Modal -->
    <div id="approveModal" class="modal fade" tabindex="-1" aria-labelledby="deleteModalLabel" aria-hidden="true" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">
                                <asp:Label ID="lblApproveTitle" runat="server">Approve GC</asp:Label></h4>
                        </div>
                        <div class="modal-body">
                            <asp:Label ID="lblApproveContent" runat="server">Are you sure you want to approve this GC ?</asp:Label>
                            <asp:HiddenField ID="hfApproveGCId" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnConfirmApproveGC"
                                runat="server"
                                CssClass="btn btn-success"
                                Text="Approve"
                                OnClick="btnConfirmApproveGC_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnConfirmApproveGC" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- Disapprove Modal -->
    <div id="disapproveModal" class="modal fade" tabindex="-1" aria-labelledby="deleteModalLabel" aria-hidden="true" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Disapprove GC</h4>
                        </div>
                        <div class="modal-body">
                            Are you sure you want to disapprove this GC ?
                            <asp:HiddenField ID="hfDisapproveGCId" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnConfirmDisapproveGC"
                                runat="server"
                                CssClass="btn btn-danger"
                                Text="Disapprove"
                                OnClick="btnConfirmDisapproveGC_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnConfirmDisapproveGC" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>


    <!-- Delete Modal -->
    <div id="deleteModal" class="modal fade" tabindex="-1" aria-labelledby="deleteModalLabel" aria-hidden="true" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Delete GC</h4>
                        </div>
                        <div class="modal-body">
                            Are you sure you want to delete this GC ?
                            <asp:HiddenField ID="hfDeleteGCId" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnConfirmDelete"
                                runat="server"
                                CssClass="btn btn-danger"
                                Text="Delete"
                                OnClick="btnConfirmDelete_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnConfirmDisapproveGC" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <asp:LinqDataSource ID="LinqDataSource1"
        runat="server"
        OnSelecting="LinqDataSource1_Selecting">
    </asp:LinqDataSource>
</asp:Content>
