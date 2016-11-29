<%@ Page Title="Add GC" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="gcform.aspx.cs" Inherits="eGC.tran.gcform" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-info">

                <div class="panel-heading">
                    <h5>Add GC</h5>
                </div>

                <div class="panel-body">
                    <div role="form">
                        <div class="col-md-4" id="panelName" runat="server">
                            <label for="txtName">Name</label>
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label for="txtGuestId" id="lblForGuestId" runat="server">Guest ID</label>
                            <asp:TextBox ID="txtGuestId" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label for="txtCompany">Company</label>
                            <asp:TextBox ID="txtCompany" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label for="txtEmail">Email</label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label for="txtContactNo">Contact No</label>
                            <asp:TextBox ID="txtContactNo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label for="txtContactPerson">Contact Person</label>
                            <asp:TextBox ID="txtContactPerson" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="panel-body">
                    <div role="form">
                        <div class="col-md-4">
                            <label for="txtDateIssued">Date Issued</label>
                            <asp:TextBox ID="txtDateIssued"
                                runat="server"
                                CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11"
                                runat="server"
                                Display="Dynamic"
                                ValidationGroup="vgPrimaryAdd"
                                ControlToValidate="txtDateIssued"
                                CssClass="label label-danger"
                                ErrorMessage="Date Issued is required"></asp:RequiredFieldValidator>
                        </div>

                        <div class="col-md-4">
                            <label for="txtCreatedBy">Created By</label>
                            <asp:TextBox ID="txtCreatedBy" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="panel-body">
                    <div role="form">
                        <div class="col-md-4">
                            <label for="ddlGCType">GC Type</label>
                            <asp:DropDownList ID="ddlGCType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlGCType_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="0">-- Select One --</asp:ListItem>
                                <asp:ListItem Value="Representation">Representation</asp:ListItem>
                                <asp:ListItem Value="Sold">Sold</asp:ListItem>
                                <asp:ListItem Value="Barter">Barter</asp:ListItem>
                                <asp:ListItem Value="Raffle / Prize">Raffle / Prize</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15"
                                runat="server"
                                Display="Dynamic"
                                ValidationGroup="vgPrimaryAdd"
                                ControlToValidate="ddlGCType"
                                InitialValue="0"
                                CssClass="label label-danger"
                                ErrorMessage="GC Type is required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <label for="txtExpirationDate">Expiration Date</label>
                            <asp:TextBox ID="txtExpirationDate"
                                runat="server"
                                Enabled="false"
                                CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                runat="server"
                                Display="Dynamic"
                                ValidationGroup="vgPrimaryAdd"
                                ControlToValidate="txtExpirationDate"
                                CssClass="label label-danger"
                                Enabled="false"
                                ErrorMessage="Expiration Date is required"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>

                <%-- Room and Dining Table --%>
                <div class="panel-body">
                    <ul class="nav nav-tabs" id="myTab">
                        <li><a href="#roomTab" data-toggle="tab">Room</a></li>
                        <li><a href="#diningTab" data-toggle="tab">Dining</a></li>
                    </ul>

                    <div id="myTabContent" class="tab-content">

                        <%-- Rooms Tab --%>
                        <div class="tab-pane" id="roomTab">
                            <asp:UpdatePanel ID="upRoom" runat="server">
                                <ContentTemplate>
                                    <div class="panel panel-primary">
                                        <div class="panel-heading">
                                            <div class="pull-right">
                                                <asp:Button ID="btnOpenRoomModal" runat="server" Text="Add" CssClass="btn btn-default btn-xs" OnClick="btnOpenRoomModal_Click" CausesValidation="false" />
                                            </div>
                                            <h5 class="panel-title">Rooms</h5>
                                        </div>
                                        <div class="panel-body">
                                            <div class="table-responsive">
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

                                                        <asp:BoundField DataField="GCNumber" HeaderText="GC Number" />
                                                        <asp:BoundField DataField="Type" HeaderText="Type" />
                                                        <asp:BoundField DataField="Room" HeaderText="Room" />

                                                        <asp:TemplateField HeaderText="Breakfast Inclusion?">
                                                            <ItemTemplate>
                                                                <%# (Boolean.Parse(Eval("WithBreakfast").ToString())) ? "Yes": "No" %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                        <asp:BoundField DataField="HowManyPerson" HeaderText="Head Count" />
                                                        <asp:BoundField DataField="Remarks" HeaderText="Remarks" />

                                                        <asp:ButtonField HeaderText="Edit" ButtonType="Link" Text="Edit" CommandName="editRoom" />
                                                        <asp:ButtonField HeaderText="Delete" ButtonType="Link" Text="Delete" CommandName="deleteRoom" />

                                                    </Columns>
                                                    <PagerStyle CssClass="pagination-ys" />
                                                </asp:GridView>

                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>

                        <%-- Dining Tab --%>
                        <div class="tab-pane" id="diningTab">
                            <asp:UpdatePanel ID="upDining" runat="server">
                                <ContentTemplate>
                                    <div class="panel panel-primary">
                                        <div class="panel-heading">
                                            <div class="pull-right">
                                                <asp:Button ID="btnOpenDiningModal" runat="server" Text="Add" CssClass="btn btn-default btn-xs" OnClick="btnOpenDiningModal_Click" />
                                            </div>
                                            <h5 class="panel-title">Dinings</h5>
                                        </div>
                                        <div class="panel-body">
                                            <div class="table-responsive">

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

                                                        <asp:BoundField DataField="GCNumber" HeaderText="GC Number" SortExpression="GCNumber" />
                                                        <asp:BoundField DataField="Name" HeaderText="Name" />
                                                        <asp:BoundField DataField="DiningType" HeaderText="Dining Type" />
                                                        <asp:BoundField DataField="HeadCount" HeaderText="Head Count" />
                                                        <asp:BoundField DataField="Remarks" HeaderText="Remarks" />

                                                        <asp:ButtonField HeaderText="Edit" ButtonType="Link" Text="Edit" CommandName="editDining" />
                                                        <asp:ButtonField HeaderText="Delete" ButtonType="Link" Text="Delete" CommandName="deleteDining" />

                                                    </Columns>
                                                    <PagerStyle CssClass="pagination-ys" />
                                                </asp:GridView>

                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>

                <%-- Footer --%>
                <div class="panel-footer">
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-primary" CausesValidation="true" ValidationGroup="vgPrimaryAdd" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-default" />
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
                                    <label for="ddlRoomProperty">Property</label>
                                    <asp:DropDownList ID="ddlRoomProperty"
                                        runat="server"
                                        AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlRoomProperty_SelectedIndexChanged"
                                        CssClass="form-control">
                                        <asp:ListItem Value="0">Select One</asp:ListItem>
                                        <asp:ListItem Value="Baguio">Baguio</asp:ListItem>
                                        <asp:ListItem Value="Boracay">Boracay</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20"
                                        runat="server"
                                        Display="Dynamic"
                                        InitialValue="0"
                                        ControlToValidate="ddlRoomProperty"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAddRoom"
                                        ErrorMessage="Property is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtAddRoomGCNumber">e-GC Request Number</label>
                                    <asp:TextBox ID="txtAddRoomGCNumber"
                                        runat="server"
                                        CssClass="form-control">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtAddRoomGCNumber"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAddRoom"
                                        ErrorMessage="GC Number is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="ddlAddRoom">Room</label>
                                    <asp:DropDownList ID="ddlAddRoom"
                                        runat="server"
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
                                    <label for="rblAddRoomBreakfast">Breakfast Inclusion? </label>
                                    <asp:RadioButtonList ID="rblAddRoomBreakfast" 
                                        runat="server" 
                                        RepeatDirection="Horizontal"
                                        CssClass="form-control">
                                        <asp:ListItem Value="True">Yes</asp:ListItem>
                                        <asp:ListItem Value="False" style="margin-left:20px">No</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="rblAddRoomBreakfast"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAddRoom"
                                        ErrorMessage="Breakfast is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtAddRoomHeadCount">Head Count</label>
                                    <asp:TextBox ID="txtAddRoomHeadCount"
                                        runat="server"
                                        TextMode="Number"
                                        CssClass="form-control">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtAddRoomHeadCount"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAddRoom"
                                        ErrorMessage="Head Count is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtAddRoomRemarks">Remarks</label>
                                    <asp:TextBox ID="txtAddRoomRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" Columns="25"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator24"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtAddRoomRemarks"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAddRoom"
                                        ErrorMessage="Remarks is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <asp:Label ID="lblAddRoomDuplicateGC" runat="server" CssClass="label label-danger"></asp:Label>
                                </div>
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

    <!-- Update Room Modal-->
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
                                <asp:Label ID="lblEditRoomGCNumber_old" runat="server" Visible="false"></asp:Label>
                            </div>

                            <div class="form-group">
                                <label for="ddlEditRoomProperty">Property</label>
                                <asp:DropDownList ID="ddlEditRoomProperty"
                                    runat="server"
                                    AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlEditRoomProperty_SelectedIndexChanged"
                                    CssClass="form-control">
                                    <asp:ListItem Value="0">Select One</asp:ListItem>
                                    <asp:ListItem Value="Baguio">Baguio</asp:ListItem>
                                    <asp:ListItem Value="Boracay">Boracay</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator21"
                                    runat="server"
                                    Display="Dynamic"
                                    InitialValue="0"
                                    ControlToValidate="ddlEditRoomProperty"
                                    CssClass="label label-danger"
                                    ValidationGroup="vgEditRoom"
                                    ErrorMessage="Property is required"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group">
                                <label for="txtEditRoomGCNumber">e-GC Request Number</label>
                                <asp:TextBox ID="txtEditRoomGCNumber"
                                    runat="server"
                                    CssClass="form-control">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtEditRoomGCNumber"
                                    CssClass="label label-danger"
                                    ValidationGroup="vgEditRoom"
                                    ErrorMessage="GC Number is required"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group">
                                <label for="ddlEditRoom">Room</label>
                                <asp:DropDownList ID="ddlEditRoom"
                                    runat="server"
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
                                <label for="rblAddRoomBreakfast">Breakfast Inclusion?</label>
                                <asp:RadioButtonList ID="rblEditRoomBreakfast" runat="server" RepeatDirection="Horizontal"
                                    CssClass="form-control">
                                    <asp:ListItem Value="True">Yes</asp:ListItem>
                                    <asp:ListItem Value="False" style="margin-left:20px;">No</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="rblEditRoomBreakfast"
                                    CssClass="label label-danger"
                                    ValidationGroup="vgEditRoom"
                                    ErrorMessage="Breakfast is required"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group">
                                <label for="txtEditRoomHeadCount">Head Count</label>
                                <asp:TextBox ID="txtEditRoomHeadCount"
                                    runat="server"
                                    CssClass="form-control">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtEditRoomHeadCount"
                                    CssClass="label label-danger"
                                    ValidationGroup="vgEditRoom"
                                    ErrorMessage="Head Count is required"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group">
                                <label for="txtEditRoomRemarks">Remarks</label>
                                <asp:TextBox ID="txtEditRoomRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" Columns="25"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <asp:Label ID="lblEditRoomDuplicateGC" runat="server" CssClass="label label-danger"></asp:Label>
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
                                    <label for="ddlDiningProperty">Property</label>
                                    <asp:DropDownList ID="ddlDiningProperty"
                                        runat="server"
                                        CssClass="form-control">
                                        <asp:ListItem Value="0">Select One</asp:ListItem>
                                        <asp:ListItem Value="Baguio">Baguio</asp:ListItem>
                                        <asp:ListItem Value="Boracay">Boracay</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22"
                                        runat="server"
                                        Display="Dynamic"
                                        InitialValue="0"
                                        ControlToValidate="ddlDiningProperty"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAddDining"
                                        ErrorMessage="Property is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtAddDiningGCNumber">e-GC Request Number</label>
                                    <asp:TextBox ID="txtAddDiningGCNumber"
                                        runat="server"
                                        CssClass="form-control">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtAddDiningGCNumber"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAddDining"
                                        ErrorMessage="GC Number is required"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtAddDiningGCNumber"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAddDining"
                                        ValidationExpression="\d+"
                                        ErrorMessage="GC Number should be numeric"></asp:RegularExpressionValidator>
                                </div>

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
                                    <label for="ddlAddDiningType">Dining Type</label>
                                    <asp:DropDownList ID="ddlAddDiningType"
                                        runat="server"
                                        CssClass="form-control">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16"
                                        runat="server"
                                        Display="Dynamic"
                                        InitialValue="0"
                                        ControlToValidate="ddlAddDiningType"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAddDining"
                                        ErrorMessage="Dining Type is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtAddDiningHeadCount">Head Count</label>
                                    <asp:TextBox ID="txtAddDiningHeadCount" runat="server" CssClass="form-control" placeholder="Value" TextMode="Number"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtAddDiningHeadCount"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAddDining"
                                        ErrorMessage="Head Count is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtAddDiningRemarks">Remarks</label>
                                    <asp:TextBox ID="txtAddDiningRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" Columns="25"></asp:TextBox>
                                </div>


                                <div class="form-group">
                                    <asp:Label ID="lblAddDiningDuplicateGC" runat="server" CssClass="label label-danger"></asp:Label>
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

    <!-- Update Dining -->
    <div id="editDining" class="modal fade" tabindex="-1" aria-labelledby="addModalLabel" aria-hidden="true" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Edit Dining</h4>
                        </div>

                        <div class="form-group">
                            <label for="ddlEditDiningProperty">Property</label>
                            <asp:DropDownList ID="ddlEditDiningProperty"
                                runat="server"
                                CssClass="form-control">
                                <asp:ListItem Value="0">Select One</asp:ListItem>
                                <asp:ListItem Value="Baguio">Baguio</asp:ListItem>
                                <asp:ListItem Value="Boracay">Boracay</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator23"
                                runat="server"
                                Display="Dynamic"
                                InitialValue="0"
                                ControlToValidate="ddlEditDiningProperty"
                                CssClass="label label-danger"
                                ValidationGroup="vgEditDining"
                                ErrorMessage="Property is required"></asp:RequiredFieldValidator>
                        </div>

                        <div class="modal-body">
                            <div class="form-group">
                                <asp:Label ID="lblEditDiningId" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblEditDiningGCNumber_old" runat="server" Visible="false"></asp:Label>
                            </div>

                            <div class="form-group">
                                <label for="txtEditDiningGCNumber">e-GC Request Number</label>
                                <asp:TextBox ID="txtEditDiningGCNumber"
                                    runat="server"
                                    CssClass="form-control">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator19"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtEditDiningGCNumber"
                                    CssClass="label label-danger"
                                    ValidationGroup="vgEditDining"
                                    ErrorMessage="GC Number is required"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtEditDiningGCNumber"
                                    CssClass="label label-danger"
                                    ValidationGroup="vgEditDining"
                                    ValidationExpression="\d+"
                                    ErrorMessage="GC Number should be numeric"></asp:RegularExpressionValidator>
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
                                <label for="ddlEditDiningType">Dining Type</label>
                                <asp:DropDownList ID="ddlEditDiningType"
                                    runat="server"
                                    CssClass="form-control">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10"
                                    runat="server"
                                    Display="Dynamic"
                                    InitialValue="0"
                                    ControlToValidate="ddlEditDiningType"
                                    CssClass="label label-danger"
                                    ValidationGroup="vgEditDining"
                                    ErrorMessage="Dining Type is required"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group">
                                <label for="txtEditDiningHeadCount">Head Count</label>
                                <asp:TextBox ID="txtEditDiningHeadCount" runat="server" CssClass="form-control" placeholder="Value" TextMode="Number"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtEditDiningHeadCount"
                                    CssClass="label label-danger"
                                    ValidationGroup="vgEditDining"
                                    ErrorMessage="Head Count is required"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group">
                                <label for="txtEditDiningRemarks">Remarks</label>
                                <asp:TextBox ID="txtEditDiningRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" Columns="25"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <asp:Label ID="lblEditDiningDuplicateGC" runat="server" CssClass="label label-danger"></asp:Label>
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

    <!-- Duplicate GC Modal -->
    <div id="duplicateGCModal" class="modal fade" tabindex="-1" aria-labelledby="deleteModalLabel" aria-hidden="true" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content panel-warning">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <div class="modal-header panel-heading">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Duplicate GC Number</h4>
                        </div>
                        <div class="modal-body">
                            <p>Duplicate GC Number detected!</p>
                            <p>Modify your GC Number and try again.</p>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
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
            $('#<%=txtExpirationDate.ClientID%>').datepicker();
            $('#<%=txtDateIssued.ClientID%>').datepicker();
        });
    </script>

    <asp:HiddenField ID="hfGCNumber" runat="server" />
</asp:Content>
