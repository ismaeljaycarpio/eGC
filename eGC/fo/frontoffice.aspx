<%@ Page Title="Guest Arrival Monitoring" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frontoffice.aspx.cs" Inherits="eGC.fo.frontoffice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearch">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h5>GC</h5>
                    </div>

                    <div class="panel-body">
                        <asp:UpdatePanel ID="upApproval" runat="server">
                            <ContentTemplate>
                                <div class="form-inline">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search..."></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlGCStatus" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="0" Text="-- Select GC Status --"></asp:ListItem>
                                            <asp:ListItem Value="Waiting" Text="Waiting"></asp:ListItem>
                                            <asp:ListItem Value="Used" Text="Used"></asp:ListItem>
                                            <asp:ListItem Value="Completed" Text="Completed"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlCompanyName" runat="server" CssClass="form-control"></asp:DropDownList>
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
                                        EnablePersistedSelection="True"
                                        AllowSorting="True"
                                        EmptyDataText="No Record(s) found"
                                        ShowHeaderWhenEmpty="True"
                                        DataKeyNames="Id"
                                        OnRowCommand="gvGC_RowCommand"
                                        OnRowDataBound="gvGC_RowDataBound"
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

                                            <asp:TemplateField HeaderText="ID" SortExpression="GuestIdName" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnGuestIdName" runat="server" Text='<%# Eval("GuestIdName") %>' CommandName="redirectGuest" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="GC Number" SortExpression="GCnumber">
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

                                            <asp:BoundField DataField="Number" HeaderText="Contact No" SortExpression="Number" />
                                            <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" />

                                            <asp:TemplateField HeaderText="Status" SortExpression="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGCStatus"
                                                        runat="server"
                                                        CssClass="badge"
                                                        Text='<%# Eval("Status") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="ExpiryDate" HeaderText="Expiration Date" DataFormatString="{0:d}" SortExpression="ExpiryDate" />

                                            <%--<asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnUsed"
                                                        runat="server"
                                                        CommandName="usedRecord"
                                                        Text="Use"
                                                        CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'
                                                        CssClass="btn btn-success btn-sm" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnCancelled"
                                                        runat="server"
                                                        Text="Cancel"
                                                        CommandName="cancelledRecord"
                                                        CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'
                                                        CssClass="btn btn-danger btn-sm" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                        </Columns>
                                        <PagerStyle CssClass="pagination-ys" />
                                    </asp:GridView>
                                    <asp:Button ID="btnExport"
                                        runat="server"
                                        Text="Export to Excel"
                                        OnClick="btnExport_Click"
                                        CssClass="btn btn-default btn-sm pull-right" />
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
    </asp:Panel>

    <!-- Used Modal -->
    <div id="usedModal" class="modal fade" tabindex="-1" aria-labelledby="deleteModalLabel" aria-hidden="true" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">
                                <asp:Label ID="lblForUseGCTitle" runat="server">Use GC</asp:Label></h4>
                        </div>
                        <div class="modal-body">
                            <asp:Label ID="lblForUseGCContent" runat="server">Are you sure you want to use this Gift Check ?</asp:Label>
                            <asp:HiddenField ID="hfUsedGCId" runat="server" />
                            <asp:HiddenField ID="hfBtnUsedStatus" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnConfirmUseGC"
                                runat="server"
                                CssClass="btn btn-success"
                                Text="Save"
                                OnClick="btnConfirmUseGC_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnConfirmUseGC" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- Cancelled Modal -->
    <div id="cancelledModal" class="modal fade" tabindex="-1" aria-labelledby="deleteModalLabel" aria-hidden="true" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Cancel GC</h4>
                        </div>
                        <div class="modal-body">
                            Reason for cancellation:
                            <asp:TextBox ID="txtCancellationReason"
                                runat="server"
                                TextMode="MultiLine"
                                CssClass="form-control"
                                Columns="40"
                                Rows="5">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                runat="server"
                                CssClass="label label-danger"
                                ControlToValidate="txtCancellationReason"
                                Display="Dynamic"
                                ValidationGroup="vgCancellation"
                                ErrorMessage="Reason for Cancellation is required"></asp:RequiredFieldValidator>
                            <asp:HiddenField ID="hfCancellationId" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnConfirmCancellation"
                                runat="server"
                                CssClass="btn btn-danger"
                                Text="Save"
                                ValidationGroup="vgCancellation"
                                OnClick="btnConfirmCancellation_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnConfirmCancellation" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <asp:GridView ID="GridView1" runat="server"></asp:GridView>

    <asp:LinqDataSource ID="LinqDataSource1"
        runat="server"
        OnSelecting="LinqDataSource1_Selecting">
    </asp:LinqDataSource>
</asp:Content>
