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
                            <label for="txtName">Recommending Approval</label>
                            <asp:TextBox ID="txtRecommendingApproval" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator11"
                                runat="server"
                                Display="Dynamic"
                                ValidationGroup="vgPrimaryAdd"
                                ControlToValidate="txtRecommendingApproval"
                                CssClass="label label-danger"
                                ErrorMessage="Recommending Approval is required"></asp:RequiredFieldValidator>--%>
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
                            <label for="txtRemarks">Remarks</label>
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" Columns="25" Enabled="false"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14"
                                runat="server"
                                Display="Dynamic"
                                ValidationGroup="vgPrimaryAdd"
                                ControlToValidate="txtRemarks"
                                CssClass="label label-danger"
                                ErrorMessage="Remarks is required"></asp:RequiredFieldValidator>
                        </div>

                        <div class="col-md-4">
                            <label for="txtRequestedBy">Requested By</label>
                            <asp:TextBox ID="txtRequestedBy" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="col-md-4">
                            <label for="ddlGCType">GC Type</label>
                            <asp:DropDownList ID="ddlGCType" runat="server" CssClass="form-control" Enabled="false">
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
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16"
                                runat="server"
                                Display="Dynamic"
                                ValidationGroup="vgPrimaryAdd"
                                ControlToValidate="txtExpirationDate"
                                CssClass="label label-danger"
                                ErrorMessage="Expiration Date is required"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>

                <div class="panel-body">
                    <div role="form">
                        <div class="col-md-4">
                            <label for="txtGCNumber">GC Number</label>
                            <asp:TextBox ID="txtGCNumber"
                                runat="server"
                                Enabled="false"
                                CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="panel-body">
                    <div class="col-md-6">
                        <label for="lblCurrentGCStatus">Current GC Status: </label>
                        <asp:Label ID="lblCurrentGCStatus" runat="server"></asp:Label>
                    </div>
                </div>

                <%--<div class="panel-body">
                    <ul class="nav nav-tabs" id="myTab">
                        <li><a href="#roomTab" data-toggle="tab">Room</a></li>
                        <li><a href="#diningTab" data-toggle="tab">Dining</a></li>
                    </ul>

                    <div id="myTabContent" class="tab-content">
                        <div class="tab-pane" id="roomTab">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
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
                                                    DataKeyNames="Id">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Row Id" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:BoundField DataField="Type" HeaderText="Type" />
                                                        <asp:BoundField DataField="Room" HeaderText="Room" />
                                                        <asp:BoundField DataField="WithBreakfast" HeaderText="With Breakfast?" />
                                                        <asp:BoundField DataField="HowManyPerson" HeaderText="Head Count" />
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
                                                    PageSize="10">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Row Id" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDiningId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:BoundField DataField="Name" HeaderText="Name" />
                                                        <asp:BoundField DataField="DiningType" HeaderText="Dining Type" />
                                                        <asp:BoundField DataField="HeadCount" HeaderText="Head Count" />

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
                </div>--%>

                <div class="panel-footer">
                    <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
        <asp:HiddenField ID="TabName" runat="server" />
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

    <asp:HiddenField ID="hfTransactionId" runat="server" />

</asp:Content>
