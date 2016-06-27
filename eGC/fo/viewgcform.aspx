<%@ Page Title="View GC Form" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="viewgcform.aspx.cs" Inherits="eGC.fo.viewgcform" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h5>View GC
                        <asp:HyperLink ID="hlPrintForm"
                            ToolTip="Print GC Form"
                            ForeColor="Black"
                            runat="server">
                            <span class="glyphicon glyphicon-print pull-right"></span>
                        </asp:HyperLink>
                    </h5>
                </div>
                <div class="panel-body">
                    <div role="form">
                        <div class="col-md-4" id="panelName" runat="server">
                            <label for="txtName">Name</label>
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label for="txtName" id="lblForGuestId" runat="server">Guest ID</label>
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
                        <div class="col-md-4">
                            <label for="txtContactPerson">Contact Person</label>
                            <asp:TextBox ID="txtContactPerson" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="panel-body">
                    <div role="form">
                        <asp:Panel ID="pnlApprovedBy" runat="server" CssClass="col-md-4">
                            <label for="txtApprovedBy">Approved By</label>
                            <asp:TextBox ID="txtApprovedBy" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </asp:Panel>
                        <div class="col-md-4">
                            <label for="txtCreatedBy">Created By</label>
                            <asp:TextBox ID="txtCreatedBy" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label for="txtDateIssued">Date Issued</label>
                            <asp:TextBox ID="txtDateIssued"
                                runat="server"
                                Enabled="false"
                                CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11"
                                runat="server"
                                Display="Dynamic"
                                ValidationGroup="vgPrimaryAdd"
                                ControlToValidate="txtDateIssued"
                                CssClass="label label-danger"
                                ErrorMessage="Date Issued is required"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>

                <div class="panel-body">
                    <div role="form">
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
                            <label for="ddlGCType">GC Type</label>
                            <asp:DropDownList ID="ddlGCType" runat="server" CssClass="form-control" Enabled="false" OnSelectedIndexChanged="ddlGCType_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Selected="True" Value="0">-- Select One --</asp:ListItem>
                                <asp:ListItem Value="Representation">Representation</asp:ListItem>
                                <asp:ListItem Value="Sold">Sold</asp:ListItem>
                                <asp:ListItem Value="Barter">Barter</asp:ListItem>
                                <asp:ListItem Value="Raffle">Raffle / Prize</asp:ListItem>
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
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16"
                                runat="server"
                                Display="Dynamic"
                                ValidationGroup="vgPrimaryAdd"
                                ControlToValidate="txtExpirationDate"
                                CssClass="label label-danger"
                                ErrorMessage="Expiration Date is required"></asp:RequiredFieldValidator>
                        </div>

                        <div class="col-md-4">
                            <label for="txtGCNumber">GC Number</label>
                            <asp:TextBox ID="txtGCNumber"
                                runat="server"
                                Enabled="false"
                                CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <asp:Panel ID="pnlRoom" runat="server" CssClass="panel-body" Visible="false">
                    <div class="col-md-4">
                        <label for="ddlRooms">Room:</label>
                        <asp:DropDownList ID="ddlRooms" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="ddlRooms"
                            CssClass="label label-danger"
                            ValidationGroup="vgPrimaryAdd"
                            InitialValue="0"
                            ErrorMessage="Room is required"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-4">
                        <label for="rblRoomBreakfast">Includes:</label>
                        <asp:RadioButtonList ID="rblRoomBreakfast" runat="server" RepeatDirection="Horizontal" CssClass="form-control" Enabled="false">
                            <asp:ListItem Value="True">With Breakfast</asp:ListItem>
                            <asp:ListItem Value="False">Without Breakfast</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-4">
                        <label for="txtRoomHeadCount">Head Count:</label>
                        <asp:TextBox ID="txtRoomHeadCount" runat="server" CssClass="form-control" TextMode="Number" Enabled="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="txtRoomHeadCount"
                            CssClass="label label-danger"
                            ValidationGroup="vgPrimaryAdd"
                            ErrorMessage="Head Count is required"></asp:RequiredFieldValidator>
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnlDining" runat="server" CssClass="panel-body" Visible="false">
                    <div class="col-md-4">
                        <label for="ddlDining">Dining:</label>
                        <asp:DropDownList ID="ddlDining" runat="server" CssClass="form-control" Enabled="false">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="ddlDining"
                            CssClass="label label-danger"
                            ValidationGroup="vgPrimaryAdd"
                            InitialValue="0"
                            ErrorMessage="Dining is required"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-4">
                        <label for="ddlDiningType">Dining Type:</label>
                        <asp:DropDownList ID="ddlDiningType" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="ddlDiningType"
                            CssClass="label label-danger"
                            ValidationGroup="vgPrimaryAdd"
                            InitialValue="0"
                            ErrorMessage="Dining Type is required"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-4">
                        <label for="txtDiningHeadCount">Head Count:</label>
                        <asp:TextBox ID="txtDiningHeadCount" runat="server" CssClass="form-control" TextMode="Number" Enabled="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="txtDiningHeadCount"
                            CssClass="label label-danger"
                            ValidationGroup="vgPrimaryAdd"
                            ErrorMessage="Head Count is required"></asp:RequiredFieldValidator>
                    </div>
                </asp:Panel>


                <asp:Panel ID="pnlCheckDate" runat="server" CssClass="panel-body">
                    <div class="col-md-4">
                        <label for="txtCheckin">Check-in:</label>
                        <asp:TextBox ID="txtCheckin" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label for="txtCheckout">Check-out:</label>
                        <asp:TextBox ID="txtCheckout" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </asp:Panel>

                <div class="panel-body">
                    <div class="col-md-4">
                        <label for="lblCurrentGCStatus">Current GC Status: </label>
                        <asp:Label ID="lblCurrentGCStatus" runat="server"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <label for="ddlGCStatus">Set GC Status:</label>
                        <asp:DropDownList ID="ddlGCStatus" runat="server" CssClass="form-control">
                            <asp:ListItem Value="">-- Select Status --</asp:ListItem>
                            <asp:ListItem Value="Used">Use</asp:ListItem>
                            <asp:ListItem Value="Completed">Complete</asp:ListItem>
                            <asp:ListItem Value="Cancelled">Cancel</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="panel-footer text-center">
                    <asp:Button ID="btnUpdate" runat="server" Text="Save" OnClick="btnUpdate_Click" CssClass="btn btn-primary" />
                    <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
        <asp:HiddenField ID="TabName" runat="server" />
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
                        <asp:AsyncPostBackTrigger ControlID="btnUpdate" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnConfirmCancellation" EventName="Click" />
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
        });
    </script>

    <script type="text/javascript">
        $(function () {
            $('#<%=txtCheckin.ClientID%>').datetimepicker();
            $('#<%=txtCheckout.ClientID%>').datetimepicker();
        });
    </script>

    <asp:HiddenField ID="hfTransactionId" runat="server" />

</asp:Content>
