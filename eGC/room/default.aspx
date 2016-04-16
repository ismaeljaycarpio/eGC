<%@ Page Title="Rooms" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="eGC.room._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-danger">
                <div class="panel-heading">
                    <div class="pull-right">
                        <button type="button" class="btn btn-primary btn-sm" data-toggle="modal" data-target="#addModal">Add</button>
                    </div>
                    <h5>Rooms</h5>
                </div>

                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="upRooms" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvRoom"
                                    runat="server"
                                    CssClass="table table-striped table-hover dataTable"
                                    GridLines="None"
                                    AutoGenerateColumns="false"
                                    AllowPaging="true"
                                    EmptyDataText="No Record(s) found"
                                    ShowHeaderWhenEmpty="true"
                                    DataKeyNames="Id"
                                    OnRowCommand="gvRoom_RowCommand"
                                    OnPageIndexChanging="gvRoom_PageIndexChanging"
                                    PageSize="10">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Row Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:ButtonField HeaderText="" ButtonType="Link" Text="Edit" CommandName="editRecord" />
                                        <asp:BoundField HeaderText="Type" DataField="Type" />
                                        <asp:BoundField HeaderText="Room" DataField="Room1" />
                                        <asp:BoundField HeaderText="Regular" DataField="Regular" />
                                        <asp:BoundField HeaderText="Peak" DataField="Peak" />
                                        <asp:ButtonField HeaderText="" ButtonType="Link" Text="Delete" CommandName="deleteRecord" />

                                    </Columns>
                                    <PagerStyle CssClass="pagination-ys" />
                                </asp:GridView>
                            </ContentTemplate>
                            <Triggers></Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Add Modal -->
    <div id="addModal" class="modal fade" tabindex="-1" aria-labelledby="addModalLabel" aria-hidden="true" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <asp:UpdatePanel ID="upAdd" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Add Room</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form">
                                <div class="form-group">
                                    <label for="txtAddType">Room Type</label>
                                    <asp:TextBox ID="txtAddType" runat="server" CssClass="form-control" placeholder="Room Type"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtAddType"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAdd"
                                        ErrorMessage="Room Type is required"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <label for="txtAddRoom">Room</label>
                                    <asp:TextBox ID="txtAddRoom" runat="server" CssClass="form-control" placeholder="Room"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtAddRoom"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAdd"
                                        ErrorMessage="Room is required"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <label for="txtAddRegular">Regular</label>
                                    <asp:TextBox ID="txtAddRegular" runat="server" CssClass="form-control" placeholder="Regular rate"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtAddRegular"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAdd"
                                        ErrorMessage="Regular rate is required"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <label for="txtAddPeak">Peak</label>
                                    <asp:TextBox ID="txtAddPeak" runat="server" CssClass="form-control" placeholder="Peak rate"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtAddPeak"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAdd"
                                        ErrorMessage="Peak rate is required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" ValidationGroup="vgAdd" OnClick="btnSave_Click" />
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

    <!-- Edit Modal -->
    <div id="updateModal" class="modal fade" tabindex="-1" aria-labelledby="addModalLabel" aria-hidden="true" role="dialog">
        <div class="modal-dialog">

            <!-- Update Modal content-->
            <div class="modal-content">
                <asp:UpdatePanel ID="upEdit" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Edit Leave Type</h4>
                        </div>

                        <div class="modal-body">
                            <div class="form-group">
                                <asp:Label ID="lblRowId" runat="server" Visible="false"></asp:Label>
                            </div>
                            <div class="form-group">
                                <label for="txtEditType">Room Type</label>
                                <asp:TextBox ID="txtEditType" runat="server" CssClass="form-control" placeholder="Room Type"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtEditType"
                                    CssClass="label label-danger"
                                    ValidationGroup="vgEdit"
                                    ErrorMessage="Room Type is required"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label for="txtEditRoom">Room</label>
                                <asp:TextBox ID="txtEditRoom" runat="server" CssClass="form-control" placeholder="Room"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtEditRoom"
                                    CssClass="label label-danger"
                                    ValidationGroup="vgEdit"
                                    ErrorMessage="Room is required"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label for="txtEditRegular">Regular</label>
                                <asp:TextBox ID="txtEditRegular" runat="server" CssClass="form-control" placeholder="Regular rate"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtEditRegular"
                                    CssClass="label label-danger"
                                    ValidationGroup="vgEdit"
                                    ErrorMessage="Regular rate is required"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label for="txtEditPeak">Peak</label>
                                <asp:TextBox ID="txtEditPeak" runat="server" CssClass="form-control" placeholder="Peak rate"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtEditPeak"
                                    CssClass="label label-danger"
                                    ValidationGroup="vgEdit"
                                    ErrorMessage="Peak rate is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" Text="Update" ValidationGroup="vgEdit" OnClick="btnUpdate_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cancel</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvRoom" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="btnUpdate" EventName="Click" />
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
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Delete Record</h4>
                        </div>
                        <div class="modal-body">
                            Are you sure you want to delete this record ?
                            <asp:HiddenField ID="hfDeleteId" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Delete" OnClick="btnDelete_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnDelete" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>


</asp:Content>
