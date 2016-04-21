﻿<%@ Page Title="GC Approval" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="eGC.gcapproval._default" %>

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
                                <div class="form-inline">
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtSearch"
                                                    runat="server"
                                                    CssClass="form-control"
                                                    placeholder="Search..."></asp:TextBox>

                                                <span class="input-group-btn">
                                                    <asp:DropDownList ID="ddlCompanyName"
                                                         CssClass="form-control"
                                                         runat="server"></asp:DropDownList>
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

                                            <asp:TemplateField HeaderText="Guest/Company ID" SortExpression="GuestId">
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
                                                    <asp:Label ID="lblApproval" runat="server" Text='<%# Eval("Approval") %>' ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="GC Status" SortExpression="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGCStatus" runat="server" Text='<%# Eval("Status") %>' ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cancellation Reason" SortExpression="CancellationReason">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCancellationReason" runat="server" Text='<%# Eval("CancellationReason") %>' ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnApprove" 
                                                            runat="server"
                                                            CommandName="approveRecord"
                                                            Text="Approve"
                                                            CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'
                                                            CssClass="btn btn-success" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnDisapprove" 
                                                            runat="server"
                                                            Text="Disapprove"
                                                            CommandName="disapproveRecord"
                                                            CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'
                                                            CssClass="btn btn-danger" />
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
                            <h4 class="modal-title"><asp:Label ID="lblApproveTitle" runat="server">Approve Gift Check</asp:Label></h4>
                        </div>
                        <div class="modal-body">
                            <asp:Label ID="lblApproveContent" runat="server">Are you sure you want to approve this Gift Check ?</asp:Label>
                            <asp:HiddenField ID="hfApproveGCId" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnConfirmApproveGC" 
                                runat="server" 
                                CssClass="btn btn-success" 
                                Text="Save" 
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
                            <h4 class="modal-title">Disapprove Gift Check</h4>
                        </div>
                        <div class="modal-body">
                            Are you sure you want to disapprove this Gift Check ?
                            <asp:HiddenField ID="hfDisapproveGCId" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnConfirmDisapproveGC" 
                                runat="server" 
                                CssClass="btn btn-danger" 
                                Text="Save" 
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

    <asp:LinqDataSource ID="LinqDataSource1"
        runat="server"
        OnSelecting="LinqDataSource1_Selecting">
    </asp:LinqDataSource>
</asp:Content>
