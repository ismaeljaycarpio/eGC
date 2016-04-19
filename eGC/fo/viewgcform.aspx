<%@ Page Title="View GC Form" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="viewgcform.aspx.cs" Inherits="eGC.fo.viewgcform" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h5>View Gift Check</h5>
                </div>
                <div class="panel-body">
                    <div role="form">
                        <div class="col-md-4">
                            <label for="txtName">Name</label>
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label for="txtName">Guest ID</label>
                            <asp:TextBox ID="txtGuestId" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label for="txtName">Company</label>
                            <asp:TextBox ID="txtCompany" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label for="txtName">Email</label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label for="txtName">Contact No</label>
                            <asp:TextBox ID="txtContactNo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="panel-body">
                    <div role="form">
                        <div class="col-md-4">
                            <label for="txtName">Recommending Approval</label>
                            <asp:TextBox ID="txtRecommendingApproval" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11"
                                runat="server"
                                Display="Dynamic"
                                ValidationGroup="vgPrimaryAdd"
                                ControlToValidate="txtRecommendingApproval"
                                CssClass="label label-danger"
                                ErrorMessage="Recommending Approval is required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <label for="txtApprovedBy">Approved By</label>
                            <asp:TextBox ID="txtApprovedBy" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12"
                                runat="server"
                                Display="Dynamic"
                                ValidationGroup="vgPrimaryAdd"
                                ControlToValidate="txtApprovedBy"
                                CssClass="label label-danger"
                                ErrorMessage="Approved By is required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <label for="txtAccountNo">Account No</label>
                            <asp:TextBox ID="txtAccountNo" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13"
                                runat="server"
                                Display="Dynamic"
                                ValidationGroup="vgPrimaryAdd"
                                ControlToValidate="txtAccountNo"
                                CssClass="label label-danger"
                                ErrorMessage="Account No is required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <label for="txtRemarks">Remarks</label>
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" Columns="25"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14"
                                runat="server"
                                Display="Dynamic"
                                ValidationGroup="vgPrimaryAdd"
                                ControlToValidate="txtRemarks"
                                CssClass="label label-danger"
                                ErrorMessage="Remarks is required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <label for="txtReason">Reason</label>
                            <asp:TextBox ID="txtReason" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" Columns="25"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15"
                                runat="server"
                                Display="Dynamic"
                                ValidationGroup="vgPrimaryAdd"
                                ControlToValidate="txtReason"
                                CssClass="label label-danger"
                                ErrorMessage="Reason is required"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>

                <div class="panel-body">
                    <div role="form">
                        <div class="col-md-4">
                            <label for="txtGCNumber">GC Number</label>
                            <asp:TextBox ID="txtGCNumber" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label for="txtArrivalDate">Arrival Date</label>
                            <asp:TextBox ID="txtArrivalDate"
                                runat="server"
                                CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                runat="server"
                                Display="Dynamic"
                                ValidationGroup="vgPrimaryAdd"
                                ControlToValidate="txtArrivalDate"
                                CssClass="label label-danger"
                                ErrorMessage="Arrival Date is required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <label for="txtCheckoutDate">Check-out Date</label>
                            <asp:TextBox ID="txtCheckoutDate"
                                runat="server"
                                CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                runat="server"
                                Display="Dynamic"
                                ControlToValidate="txtCheckoutDate"
                                CssClass="label label-danger"
                                ValidationGroup="vgPrimaryAdd"
                                ErrorMessage="Checkout Date is required"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>

                <div class="panel-body">
                    <div class="col-md-6">
                        <label for="lblCurrentGCStatus">Current GC Status: </label>
                        <asp:Label ID="lblCurrentGCStatus" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-md-6">
                        <label for="btnUsed">Set GC Status: </label>
                        <asp:Button ID="btnUsed" runat="server" Text="Used" CssClass="btn btn-success" OnClick="btnUsed_Click"/>
                        <%--<asp:Button ID="btnCancel" runat="server" Text="Cancelled" CssClass="btn btn-danger" OnClick="btnCancel_Click" />
                        <asp:Button ID="btnExpire" runat="server" Text="Expired" CssClass="btn btn-warning"  OnClick="btnExpire_Click"/>
                        <asp:Button ID="btnWaiting" runat="server" Text="Waiting" CssClass="btn btn-default"  OnClick="btnWaiting_Click"/>--%>
                    </div>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" id="myTab">
                        <li><a href="#roomTab" data-toggle="tab">Room</a></li>
                        <li><a href="#diningTab" data-toggle="tab">Dining</a></li>
                    </ul>

                    <div id="myTabContent" class="tab-content">
                        <div class="tab-pane" id="roomTab">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <div class="pull-right">
                                        <button type="button" class="btn btn-default btn-xs" data-toggle="modal" data-target="#addRoom">Add</button>
                                    </div>
                                    <h5 class="panel-title">Room</h5>
                                </div>
                                <div class="panel-body">
                                    <div class="table-responsive">
                                        <asp:UpdatePanel ID="upRoom" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="gvRoom"
                                                    runat="server"
                                                    CssClass="table table-striped table-hover dataTable"
                                                    GridLines="None"
                                                    AutoGenerateColumns="false"
                                                    EmptyDataText="No Record(s) found"
                                                    ShowHeaderWhenEmpty="true"
                                                    DataKeyNames="Id"
                                                    OnRowCommand="gvRoom_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Row Id" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:ButtonField HeaderText="Edit" ButtonType="Link" Text="Edit" CommandName="editRoom" />

                                                        <asp:BoundField DataField="Type" HeaderText="Type" />
                                                        <asp:BoundField DataField="Room" HeaderText="Room" />

                                                        <asp:TemplateField HeaderText="Regular/Peak">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRegularOrPeak" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Nights">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNights" runat="server" Text='<%# Eval("Nights") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Value">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblValue" runat="server" Text='<%# Eval("Value") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Total">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:ButtonField HeaderText="Delete" ButtonType="Link" Text="Delete" CommandName="deleteRoom" />

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

                        <div class="tab-pane" id="diningTab">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <div class="pull-right">
                                        <button type="button" class="btn btn-default btn-xs" data-toggle="modal" data-target="#addDining">Add</button>
                                    </div>
                                    <h5 class="panel-title">Dining</h5>
                                </div>
                                <div class="panel-body">
                                    <div class="table-responsive">
                                        <asp:UpdatePanel ID="upDining" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="gvDining"
                                                    runat="server"
                                                    CssClass="table table-striped table-hover dataTable"
                                                    GridLines="None"
                                                    AutoGenerateColumns="false"
                                                    EmptyDataText="No Record(s) found"
                                                    ShowHeaderWhenEmpty="true"
                                                    DataKeyNames="Id"
                                                    OnRowCommand="gvDining_RowCommand"
                                                    PageSize="10">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Row Id" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDiningId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:ButtonField HeaderText="Edit" ButtonType="Link" Text="Edit" CommandName="editDining" />

                                                        <asp:BoundField DataField="Name" HeaderText="Name" />

                                                        <asp:TemplateField HeaderText="Value">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblValue" runat="server" Text='<%# Eval("Value") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:ButtonField HeaderText="Delete" ButtonType="Link" Text="Delete" CommandName="deleteDining" />

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
                </div>
                <div class="panel-footer">
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" CssClass="btn btn-primary" CausesValidation="true" ValidationGroup="vgPrimaryAdd" />
                    <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
        <asp:HiddenField ID="TabName" runat="server" />
    </div>

    <!-- Add Room Modal -->
    <div id="addRoom" class="modal fade" tabindex="-1" aria-labelledby="addModalLabel" aria-hidden="true" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <asp:UpdatePanel ID="upAddRoom" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Add Room</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form">
                                <div class="form-group">
                                    <label for="ddlAddRoom">Room</label>
                                    <asp:DropDownList ID="ddlAddRoom"
                                        runat="server"
                                        AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlAddRoom_SelectedIndexChanged"
                                        CssClass="form-control">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="ddlAddRoom"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAddRoom"
                                        ErrorMessage="Room is required"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <label for="ddlAddPeakRegular">Peak/Regular</label>
                                    <asp:DropDownList ID="ddlAddPeakRegular" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="Regular">Regular</asp:ListItem>
                                        <asp:ListItem Value="Peak">Peak</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label for="txtAddNight">No of Nights</label>
                                    <asp:TextBox ID="txtAddNight" runat="server" CssClass="form-control" placeholder="No of Nights"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtAddNight"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAddRoom"
                                        ErrorMessage="No of Nights is required"></asp:RequiredFieldValidator>
                                </div>
                                <p>
                                    <b>Regular Rate</b>:
                                    <asp:Label ID="lblAddRoomRegularRate" runat="server"></asp:Label>
                                </p>
                                <p>
                                    <b>Peak Rate</b>:
                                    <asp:Label ID="lblAddRoomPeakRate" runat="server"></asp:Label>
                                </p>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnAddRoom" runat="server" CssClass="btn btn-primary" Text="Save" ValidationGroup="vgAddRoom" OnClick="btnAddRoom_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnAddRoom" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- Update Room Modal content-->
    <div id="editRoom" class="modal fade" tabindex="-1" aria-labelledby="addModalLabel" aria-hidden="true" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="upEditRoom" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Edit Room</h4>
                        </div>

                        <div class="modal-body">
                            <div class="form-group">
                                <asp:Label ID="lblEditRoomId" runat="server" Visible="false"></asp:Label>
                            </div>
                            <div class="form-group">
                                <label for="ddlEditRoom">Room</label>
                                <asp:DropDownList ID="ddlEditRoom"
                                    runat="server"
                                    AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlEditRoom_SelectedIndexChanged"
                                    CssClass="form-control">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="ddlEditRoom"
                                    CssClass="label label-danger"
                                    ValidationGroup="vgEditRoom"
                                    ErrorMessage="Room is required"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label for="ddlEditPeakRegular">Peak/Regular</label>
                                <asp:DropDownList ID="ddlEditPeakRegular" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="Regular">Regular</asp:ListItem>
                                    <asp:ListItem Value="Peak">Peak</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label for="txtEditNight">No of Nights</label>
                                <asp:TextBox ID="txtEditNight"
                                    runat="server"
                                    CssClass="form-control"
                                    placeholder="No of Nights"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtEditNight"
                                    CssClass="label label-danger"
                                    ValidationGroup="vgEditRoom"
                                    ErrorMessage="No of Nights is required"></asp:RequiredFieldValidator>
                            </div>
                            <p>
                                <b>Regular Rate</b>:
                                    <asp:Label ID="lblEditRoomRegularRate" runat="server"></asp:Label>
                            </p>
                            <p>
                                <b>Peak Rate</b>:
                                    <asp:Label ID="lblEditRoomPeakRate" runat="server"></asp:Label>
                            </p>
                            <div class="form-group">
                                <label for="lblEditTotalValue">Total</label>
                                <asp:Label ID="lblEditTotalValue" runat="server" ForeColor="Green"></asp:Label>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnUpdateRoom" runat="server" CssClass="btn btn-primary" Text="Update" ValidationGroup="vgEditRoom" OnClick="btnUpdateRoom_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cancel</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvRoom" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="btnUpdateRoom" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- Delete Room Modal -->
    <div id="deleteRoom" class="modal fade" tabindex="-1" aria-labelledby="deleteModalLabel" aria-hidden="true" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Delete Record</h4>
                        </div>
                        <div class="modal-body">
                            Are you sure you want to delete this record ?
                            <asp:HiddenField ID="hfDeleteRoomId" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnDeleteRoom" runat="server" CssClass="btn btn-danger" Text="Delete" OnClick="btnDeleteRoom_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnDeleteRoom" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>


    <!-- Add Dining Modal -->
    <div id="addDining" class="modal fade" tabindex="-1" aria-labelledby="addModalLabel" aria-hidden="true" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Add Dining</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form">
                                <div class="form-group">
                                    <label for="ddlAddDining">Dining</label>
                                    <asp:DropDownList ID="ddlAddDining"
                                        runat="server"
                                        CssClass="form-control">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="ddlAddDining"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAddDining"
                                        ErrorMessage="Dining is required"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <label for="txtAddValue">Value</label>
                                    <asp:TextBox ID="txtAddValue" runat="server" CssClass="form-control" placeholder="Value"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtAddValue"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAddDining"
                                        ErrorMessage="Value is required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnAddDining" runat="server" CssClass="btn btn-primary" Text="Save" ValidationGroup="vgAddDining" OnClick="btnAddDining_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnAddDining" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- Update Dining Modal content-->
    <div id="editDining" class="modal fade" tabindex="-1" aria-labelledby="addModalLabel" aria-hidden="true" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Edit Dining</h4>
                        </div>

                        <div class="modal-body">
                            <div class="form-group">
                                <asp:Label ID="lblEditDiningId" runat="server" Visible="false"></asp:Label>
                            </div>
                            <div class="form-group">
                                <label for="ddlEditDining">Dining</label>
                                <asp:DropDownList ID="ddlEditDining"
                                    runat="server"
                                    CssClass="form-control">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="ddlEditDining"
                                    CssClass="label label-danger"
                                    ValidationGroup="vgEditDining"
                                    ErrorMessage="Dining is required"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label for="txtEditValue">Value</label>
                                <asp:TextBox ID="txtEditValue" runat="server" CssClass="form-control" placeholder="Value"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtEditValue"
                                    CssClass="label label-danger"
                                    ValidationGroup="vgEditDining"
                                    ErrorMessage="Value is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnEditDining" runat="server" CssClass="btn btn-primary" Text="Update" ValidationGroup="vgEditDining" OnClick="btnEditDining_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cancel</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvDining" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="btnEditDining" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- Delete Dining Modal -->
    <div id="deleteDining" class="modal fade" tabindex="-1" aria-labelledby="deleteModalLabel" aria-hidden="true" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Delete Record</h4>
                        </div>
                        <div class="modal-body">
                            Are you sure you want to delete this record ?
                            <asp:HiddenField ID="hfDeleteDiningId" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnDeleteDining" runat="server" CssClass="btn btn-danger" Text="Delete" OnClick="btnDeleteDining_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnDeleteDining" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(function () {
            var tabName = $("[id*=TabName]").val() != "" ? $("[id*=TabName]").val() : "roomTab";
            $('#myTab a[href="#' + tabName + '"]').tab('show');
            $("#myTab a").click(function () {
                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
            });
        });
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=txtArrivalDate.ClientID%>').datepicker();
            $('#<%=txtCheckoutDate.ClientID%>').datepicker();
        });
    </script>

    <asp:HiddenField ID="hfTransactionId" runat="server" />

</asp:Content>
