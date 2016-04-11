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
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-md-12">
                                <div class="input-group">
                                    <span class="input-group-btn">
                                        <asp:Button ID="btnSearch"
                                            runat="server"
                                            CssClass="btn btn-primary"
                                            Text="Go"
                                            OnClick="btnSearch_Click" />
                                    </span>
                                    <asp:TextBox ID="txtSearch"
                                        runat="server"
                                        CssClass="form-control" placeholder="Search..."></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="upUsers" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvUsers"
                                    runat="server"
                                    CssClass="table table-striped table-hover dataTable"
                                    GridLines="None"
                                    AutoGenerateColumns="false"
                                    AllowPaging="true"
                                    AllowSorting="true"
                                    EmptyDataText="No Record(s) found"
                                    ShowHeaderWhenEmpty="true"
                                    DataKeyNames="UserId"
                                    OnRowDataBound="gvUsers_RowDataBound"
                                    OnPageIndexChanging="gvUsers_PageIndexChanging"
                                    OnRowCommand="gvUsers_RowCommand"
                                    OnSorting="gvUsers_Sorting"
                                    PageSize="10">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblEmpId" runat="server" Text='<%# Eval("EmpId") %>' CommandName="editRole" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="FullName" HeaderText="Name" />

                                        <asp:BoundField DataField="RoleName" HeaderText="Role" />

                                    </Columns>
                                    <PagerStyle CssClass="pagination-ys" />
                                </asp:GridView>
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

</asp:Content>
