<%@ Page Title="User Config" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserConfig.aspx.cs" Inherits="eGC.admin.UserConfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearch" CssClass="row">
        <div class="col-md-12">
            <div class="panel panel-danger">
                <div class="panel-heading">
                    <h5>User Access</h5>
                </div>
                <div class="panel-body">
                    <div class="form-inline">
                        <div class="form-group">
                            <asp:TextBox ID="txtSearch"
                                runat="server"
                                CssClass="form-control"
                                placeholder="Search..."></asp:TextBox>
                            <asp:Button ID="btnSearch"
                                runat="server"
                                CssClass="btn btn-primary"
                                Text="Go"
                                OnClick="btnSearch_Click" />
                        </div>
                    </div>
                </div>

                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="upUsers" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvUsers"
                                    runat="server"
                                    CssClass="table table-striped table-hover dataTable"
                                    GridLines="None"
                                    AutoGenerateColumns="False"
                                    AllowPaging="True"
                                    ShowHeader="true"
                                    ShowFooter="True"
                                    AllowSorting="True"
                                    EmptyDataText="No Record(s) found"
                                    ShowHeaderWhenEmpty="True"
                                    DataKeyNames="UserId"
                                    OnRowDataBound="gvUsers_RowDataBound"
                                    OnRowCommand="gvUsers_RowCommand"
                                    DataSourceID="UserDataSource">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ID" SortExpression="EmpId">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblUsername" runat="server" Text='<%# Eval("Username") %>' CommandName="editRole" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="FullName" HeaderText="Name" SortExpression="FullName" />

                                        <asp:BoundField DataField="RoleName" HeaderText="Role" SortExpression="RoleName" />

                                        <asp:TemplateField HeaderText="Account Status" SortExpression="IsApproved">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblStatus"
                                                    runat="server"
                                                    OnClick="lblStatus_Click"
                                                    Text='<%# (Boolean.Parse(Eval("IsApproved").ToString())) ? "Active" : "Inactive" %>'>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Locked Out" SortExpression="IsLockedOut">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnLockedOut"
                                                    runat="server"
                                                    OnClick="lbtnLockedOut_Click"
                                                    Text='<%# (Boolean.Parse(Eval("IsLockedOut").ToString())) ? "Yes" : "No" %>'>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Reset Password">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblReset"
                                                    runat="server"
                                                    OnClick="lblReset_Click"
                                                    Text="Reset Password">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <PagerStyle CssClass="pagination-ys" />
                                </asp:GridView>

                                <asp:Button ID="openCreateAccount"
                                    runat="server"
                                    CssClass="btn btn-default btn-sm pull-right"
                                    Text="Create User"
                                    OnClick="openCreateAccount_Click" />

                                <asp:Label ID="lblRowCount" runat="server"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>


    <%--Create user --%>
    <div id="createUser" class="modal fade" tabindex="-1" aria-labelledby="addModalLabel" aria-hidden="true" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Create User</h4>
                        </div>

                        <div class="modal-body">

                            <div class="form-group">
                                <label for="txtCreateUsername">Username</label>
                                <asp:TextBox ID="txtCreateUsername" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                    runat="server"
                                    CssClass="label label-danger"
                                    Display="Dynamic"
                                    ControlToValidate="txtCreateUsername"
                                    ValidationGroup="vgAddUser"                                  
                                    ErrorMessage="Username is required"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator 
                                    Display="Dynamic" 
                                    ControlToValidate="txtCreateUsername" 
                                    CssClass="label label-danger"
                                    ID="RegularExpressionValidator3" 
                                    ValidationExpression="^[\s\S]{6,15}$" 
                                    runat="server" 
                                    ValidationGroup="vgAddUser"
                                    ErrorMessage="Minimum 6 and Maximum 15 characters required."></asp:RegularExpressionValidator>
                            </div>

                            <div class="form-group">
                                <label for="txtCreateFirstName">First Name</label>
                                <asp:TextBox ID="txtCreateFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                                    runat="server"
                                    CssClass="label label-danger"
                                    Display="Dynamic"
                                    ControlToValidate="txtCreateFirstName"
                                    ValidationGroup="vgAddUser"                                  
                                    ErrorMessage="First Name is required"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group">
                                <label for="txtCreateMiddleName">Middle Name</label>
                                <asp:TextBox ID="txtCreateMiddleName" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                                    runat="server"
                                    CssClass="label label-danger"
                                    Display="Dynamic"
                                    ControlToValidate="txtCreateMiddleName"
                                    ValidationGroup="vgAddUser"                                  
                                    ErrorMessage="Middle Name is required"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group">
                                <label for="txtCreateLastName">Last Name</label>
                                <asp:TextBox ID="txtCreateLastName" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6"
                                    runat="server"
                                    CssClass="label label-danger"
                                    Display="Dynamic"
                                    ControlToValidate="txtCreateLastName"
                                    ValidationGroup="vgAddUser"                                  
                                    ErrorMessage="Last Name is required"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group">
                                <label for="ddlCreateRoles">Role</label>
                                <asp:DropDownList ID="ddlCreateRoles" runat="server" CssClass="form-control"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                    runat="server"
                                    ForeColor="Red"
                                    Display="Dynamic"
                                    ControlToValidate="ddlCreateRoles"
                                    InitialValue="0"
                                    ValidationGroup="vgAddUser"
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group">
                                <asp:Label ID="lblErrorMsg" runat="server" CssClass="label label-danger"></asp:Label>
                            </div>

                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnAddUser"
                                runat="server"
                                CssClass="btn btn-primary"
                                Text="Save"
                                ValidationGroup="vgAddUser"
                                OnClick="btnAddUser_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cancel</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvUsers" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="btnAddUser" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>



    <%--Update Role--%>
    <div id="editRole" class="modal fade" tabindex="-1" aria-labelledby="addModalLabel" aria-hidden="true" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Edit Role</h4>
                        </div>

                        <div class="modal-body">
                            <div class="form-group">
                                <asp:Label ID="lblUserId" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblUserName" runat="server" Visible="false"></asp:Label>
                            </div>
                            <div class="form-group">
                                <label for="ddlRoles">Role</label>
                                <asp:DropDownList ID="ddlRoles" runat="server" CssClass="form-control"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                    runat="server"
                                    ForeColor="Red"
                                    Display="Dynamic"
                                    ControlToValidate="ddlRoles"
                                    InitialValue="0"
                                    ValidationGroup="gvEditRole"
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group">
                                <asp:CheckBox ID="chkDelete" runat="server" Text="Remove all access" />
                            </div>

                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnUpdate"
                                runat="server"
                                CssClass="btn btn-primary"
                                Text="Update"
                                ValidationGroup="gvEditRole"
                                OnClick="btnUpdate_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cancel</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvUsers" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="btnUpdate" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <asp:LinqDataSource ID="UserDataSource"
        OnSelecting="UserDataSource_Selecting"
        runat="server">
    </asp:LinqDataSource>

</asp:Content>
