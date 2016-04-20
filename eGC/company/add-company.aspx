<%@ Page Title="Create Company Profile" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="add-company.aspx.cs" Inherits="eGC.company.add_company" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Company Details
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <asp:Panel ID="pnlSuccess" runat="server" CssClass="alert alert-success" Visible="false">
                            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                            <strong>Success!</strong> Company successfully created.
                        </asp:Panel>

                        <div class="form-group">
                            <label for="txtEmpId" class="col-sm-3 control-label">Company ID: </label>
                            <div class="col-sm-6">
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-user"></span></span>
                                    <asp:TextBox ID="txtCompanyId" runat="server" CssClass="form-control" placeholder="Company ID"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtCompanyId"
                                        CssClass="label label-danger"
                                        ErrorMessage="Company ID is required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtCompanyName" class="col-sm-3 control-label">Company Name: </label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control" placeholder="Company Name"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtCompanyName"
                                    CssClass="label label-danger"
                                    ErrorMessage="Company Name is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtContactNo" class="col-sm-3 control-label">Contact No: </label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtContactNo" runat="server" CssClass="form-control" placeholder="Contact No"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtContactNo"
                                    CssClass="label label-danger"
                                    ErrorMessage="Contact No is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtEmail" class="col-sm-3 control-label">Email: </label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtEmail"
                                    CssClass="label label-danger"
                                    ErrorMessage="Email is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtIdNumber" class="col-sm-3 control-label">Valid ID Number: </label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtIdNumber" runat="server" CssClass="form-control" placeholder="Valid ID Number"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtIdNumber"
                                    CssClass="label label-danger"
                                    ErrorMessage="Valid ID Number is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtContactPerson" class="col-sm-3 control-label">Contact Person</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtContactPerson" runat="server" CssClass="form-control" placeholder="Contact Person"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtContactPerson"
                                    CssClass="label label-danger"
                                    ErrorMessage="Contact Person is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtContactPersonNumber" class="col-sm-3 control-label">Contact Person Number</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtContactPersonNumber" runat="server" CssClass="form-control" placeholder="Contact Person Number"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtContactPersonNumber"
                                    CssClass="label label-danger"
                                    ErrorMessage="Contact Person Number is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtContactPersonAddress" class="col-sm-3 control-label">Contact Person Address</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtContactPersonAddress" runat="server" CssClass="form-control" placeholder="Contact Person Address"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtContactPersonAddress"
                                    CssClass="label label-danger"
                                    ErrorMessage="Contact Person Address is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="col-sm-offset-3 col-sm-6">
                                <asp:Button ID="btnCreate"
                                    runat="server"
                                    CssClass="btn btn-primary"
                                    Text="Create"
                                    CausesValidation="true"
                                    OnClick="btnCreate_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfUserId" runat="server" />
</asp:Content>
