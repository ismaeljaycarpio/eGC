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
                            <h5>Guest/Company Profile</h5>
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
                                        <label for="txtGuestId">Guest/Company ID</label>
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
                                    <div class="col-xs-3">
                                        <label for="txtGCExpirationDate">GC Expiration Date</label>
                                        <asp:TextBox ID="txtGCExpirationDate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
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
                            <h5>Approved List</h5>
                        </div>
                        <div class="panel-body">
                            <asp:UpdatePanel ID="upApproval" runat="server">
                                <ContentTemplate>
                                    <div class="form-inline">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search..."></asp:TextBox>
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

                                                <asp:TemplateField HeaderText="ID" SortExpression="GuestId">
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
                                                <asp:BoundField DataField="ExpiryDate" HeaderText="Expiration Date" DataFormatString="{0:d}" SortExpression="ExpiryDate" />

                                                <asp:TemplateField HeaderText="Status" SortExpression="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGCStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="TotalValue" HeaderText="Grand Total" DataFormatString="{0:C}" SortExpression="TotalValue" />

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnUsed"
                                                            runat="server"
                                                            CommandName="usedRecord"
                                                            Text="Use"
                                                            CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'
                                                            CssClass="btn btn-success" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnCancelled"
                                                            runat="server"
                                                            Text="Cancel"
                                                            CommandName="cancelledRecord"
                                                            CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'
                                                            CssClass="btn btn-danger" />
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

    <!-- Used Modal -->
    <div id="usedModal" class="modal fade" tabindex="-1" aria-labelledby="deleteModalLabel" aria-hidden="true" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title"><asp:Label ID="lblForUseGCTitle" runat="server">Use GC</asp:Label></h4>
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
