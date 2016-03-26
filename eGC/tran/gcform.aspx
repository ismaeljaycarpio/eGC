<%@ Page Title="Add Gift Check" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="gcform.aspx.cs" Inherits="eGC.tran.gcform" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h5>Add Gift Check</h5>
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
                        <div class="col-lg-10">
                            <label for="txtName">GC Number</label>
                            <asp:TextBox ID="txtGCNumber" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label for="txtName">Arrival Date</label>
                            <asp:TextBox ID="txtArrivalDate" runat="server" CssClass="form-control" data-provide="datepicker"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                runat="server"
                                Display="Dynamic"
                                ValidationGroup="vgPrimaryAdd"
                                ControlToValidate="txtArrivalDate"
                                CssClass="label label-danger"
                                ErrorMessage="Arrival Date is required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <label for="txtName">Check-out Date</label>
                            <asp:TextBox ID="txtCheckoutDate" runat="server" CssClass="form-control" data-provide="datepicker"></asp:TextBox>
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
                                                    OnPageIndexChanging="gvRoom_PageIndexChanging"
                                                    OnRowCommand="gvRoom_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Row Id" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:ButtonField HeaderText="Edit" ButtonType="Link" Text="Edit" CommandName="editRecord" />

                                                        <asp:BoundField DataField="Type" HeaderText="Type" />
                                                        <asp:BoundField DataField="Room" HeaderText="Leave" SortExpression="LeaveName" />

                                                        <asp:TemplateField HeaderText="Regular/Peak">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRegularOrPeak" runat="server" Text='<%# Eval("RegularPeak") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Nights">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNights" runat="server" Text='<%# Eval("Nights") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Total">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

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
                                                    OnPageIndexChanging="gvDining_PageIndexChanging"
                                                    OnRowCommand="gvDining_RowCommand"
                                                    PageSize="10">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Row Id" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:ButtonField HeaderText="Edit" ButtonType="Link" Text="Edit" CommandName="editRecord" />

                                                        <asp:BoundField DataField="Name" HeaderText="Name" />

                                                        <asp:TemplateField HeaderText="Regular/Peak">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblValue" runat="server" Text='<%# Eval("Value") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
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
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-primary" CausesValidation="true" ValidationGroup="vgPrimaryAdd" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
        <asp:HiddenField ID="TabName" runat="server" />
    </div>

    <!-- Add Modal -->
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
                                        CssClass="form-control"></asp:DropDownList>
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
                                    <b>Regular Rate</b>: <asp:Label ID="lblAddRoomRegularRate" runat="server"></asp:Label>
                                </p>
                                <p>
                                    <b>Peak Rate</b>: <asp:Label ID="lblAddRoomPeakRate" runat="server"></asp:Label>
                                </p>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text="Save" ValidationGroup="vgAddRoom" OnClick="btnSave_Click" />
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

    <div id="updateModal" class="modal fade" tabindex="-1" aria-labelledby="addModalLabel" aria-hidden="true" role="dialog">
        <div class="modal-dialog">
            <!-- Update Modal content-->
            <div class="modal-content">
                <asp:UpdatePanel ID="upEditRoom" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Edit Room</h4>
                        </div>

                        <div class="modal-body">
                            <div class="form-group">
                                <asp:Label ID="lblRowId" runat="server" Visible="false"></asp:Label>
                            </div>
                            <div class="form-group">
                                <label for="ddlEditRoom">Room</label>
                                <asp:DropDownList ID="ddlEditRoom" runat="server" CssClass="form-control"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="ddlEditRoom"
                                    CssClass="label label-danger"
                                    ValidationGroup="vgAddRoom"
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
                                <asp:TextBox ID="txtEditNight" runat="server" CssClass="form-control" placeholder="No of Nights"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtEditNight"
                                    CssClass="label label-danger"
                                    ValidationGroup="vgAddRoom"
                                    ErrorMessage="No of Nights is required"></asp:RequiredFieldValidator>
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

    <script type="text/javascript">
        $(function () {
            var tabName = $("[id*=TabName]").val() != "" ? $("[id*=TabName]").val() : "roomTab";
            $('#myTab a[href="#' + tabName + '"]').tab('show');
            $("#myTab a").click(function () {
                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
            });
        });
    </script>

</asp:Content>
