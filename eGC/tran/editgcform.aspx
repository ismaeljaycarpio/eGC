<%@ Page Title="Update GC Form" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="editgcform.aspx.cs" Inherits="eGC.tran.editgcform" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h5>Update GC
                        <asp:HyperLink ID="hlPrintForm"
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
                            <label for="txtName">Recommending Approval</label>
                            <asp:TextBox ID="txtRecommendingApproval" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

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
                            <label for="txtRequestedBy">Requested By</label>
                            <asp:TextBox ID="txtRequestedBy" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="panel-body">
                    <div role="form">
                        <div class="col-md-4">
                            <label for="txtRemarks">Remarks</label>
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" Columns="25"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label for="ddlGCType">GC Type</label>
                            <asp:DropDownList ID="ddlGCType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlGCType_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="0">-- Select One --</asp:ListItem>
                                <asp:ListItem Value="Representation">Representation</asp:ListItem>
                                <asp:ListItem Value="Sold">Sold</asp:ListItem>
                                <asp:ListItem Value="Barter">Barter</asp:ListItem>
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

                <div class="panel-body">
                    <div role="form">
                        <div class="col-md-4">
                            <label for="txtName">GC Number</label>
                            <asp:TextBox ID="txtGCNumber" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                runat="server"
                                Display="Dynamic"
                                ValidationGroup="vgPrimaryAdd"
                                ControlToValidate="txtGCNumber"
                                CssClass="label label-danger"
                                Enabled="false"
                                ErrorMessage="GC Number is required"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>

                <asp:Panel ID="pnlRoom" runat="server" CssClass="panel-body" Visible="false">
                    <div class="col-md-4">
                        <label for="ddlRooms">Room:</label>
                        <asp:DropDownList ID="ddlRooms" runat="server" CssClass="form-control"></asp:DropDownList>
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
                        <asp:RadioButtonList ID="rblRoomBreakfast" runat="server" RepeatDirection="Horizontal" CssClass="form-control">
                            <asp:ListItem Value="True">With Breakfast</asp:ListItem>
                            <asp:ListItem Value="False">Without Breakfast</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-4">
                        <label for="txtRoomHeadCount">Head Count:</label>
                        <asp:TextBox ID="txtRoomHeadCount" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
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
                        <asp:DropDownList ID="ddlDining" runat="server" CssClass="form-control">
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
                        <asp:DropDownList ID="ddlDiningType" runat="server" CssClass="form-control"></asp:DropDownList>
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
                        <asp:TextBox ID="txtDiningHeadCount" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="txtDiningHeadCount"
                            CssClass="label label-danger"
                            ValidationGroup="vgPrimaryAdd"
                            ErrorMessage="Head Count is required"></asp:RequiredFieldValidator>
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnlCancellation" runat="server" Visible="false" CssClass="panel-body">
                    <div class="col-md-4">
                        <label for="txtDateCancelled">Date Cancelled:</label>
                        <asp:TextBox ID="txtDateCancelled" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-8">
                        <label for="txtCancellationReason">Reason for Cancellation</label>
                        <asp:TextBox ID="txtCancellationReason" runat="server" Enabled="false" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </asp:Panel>

                <div class="panel-footer text-center">
                    <asp:Button ID="btnSave" runat="server" Text="Update" OnClick="btnSave_Click" CssClass="btn btn-primary" CausesValidation="true" ValidationGroup="vgPrimaryAdd" />
                    <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="btn btn-success" OnClick="btnApprove_Click" CausesValidation="true" ValidationGroup="vgPrimaryAdd" />
                    <asp:Button ID="btnDisapprove" runat="server" Text="Dispprove" CssClass="btn btn-warning" OnClick="btnDisapprove_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-default" />
                </div>

            </div>
        </div>
        <asp:HiddenField ID="TabName" runat="server" />
    </div>

    <!-- Approve Cancelled Modal -->
    <div id="cancelledGCModal" class="modal fade" tabindex="-1" aria-labelledby="deleteModalLabel" aria-hidden="true" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">
                                <asp:Label ID="lblApproveTitle" runat="server">Approve Cancelled GC</asp:Label></h4>
                        </div>
                        <div class="modal-body">
                            <asp:Label ID="lblApproveContent" runat="server">Are you sure you want to approve this <b>Cancelled</b> GC and move it to history?</asp:Label>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnConfirmCancelledGC"
                                runat="server"
                                CssClass="btn btn-danger"
                                Text="Approve"
                                OnClick="btnConfirmCancelledGC_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnApprove" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnConfirmCancelledGC" EventName="Click" />
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

    <asp:HiddenField ID="hfTransactionId" runat="server" />
    <asp:HiddenField ID="hfGCNumber" runat="server" />
</asp:Content>
