<%@ Page Title="Create Guest Profile" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="createguest.aspx.cs" Inherits="eGC.guest.createguest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Guest Details
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <asp:Panel ID="pnlSuccess" runat="server" CssClass="alert alert-success" Visible="false">
                            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                            <strong>Success!</strong> Guest successfully created.
                        </asp:Panel>
                        <div class="form-group">
                            <label for="imgProfile" class="col-sm-3 control-label">Profile Picture: </label>
                            <div class="col-sm-6">
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="FileUpload1"
                                    CssClass="label label-danger"
                                    ErrorMessage="Profile Picture is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="imgProfile" class="col-sm-3 control-label">Valid ID Picture: </label>
                            <div class="col-sm-6">
                                <asp:FileUpload ID="FileUpload2" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="FileUpload2"
                                    CssClass="label label-danger"
                                    ErrorMessage="Valid ID Picture is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtEmpId" class="col-sm-3 control-label">Guest ID: </label>
                            <div class="col-sm-6">
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-user"></span></span>
                                    <asp:TextBox ID="txtGuestId" runat="server" CssClass="form-control" placeholder="Employee ID"></asp:TextBox>
                                    <asp:Button ID="btnGenerateId"
                                        runat="server"
                                        Text="Generate ID"
                                        CausesValidation="false"
                                        OnClick="btnGenerateId_Click"
                                        CssClass="btn btn-default" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtGuestId"
                                        CssClass="label label-danger"
                                        ErrorMessage="Guest ID is required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtFirstName" class="col-sm-3 control-label">First Name :</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" placeholder="First Name"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtFirstName"
                                    CssClass="label label-danger"
                                    ErrorMessage="First Name is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtMiddleName" class="col-sm-3 control-label">Middle Name: </label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtMiddleName" runat="server" CssClass="form-control" placeholder="Middle Name"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtMiddleName"
                                    CssClass="label label-danger"
                                    ErrorMessage="Middle Name is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtLastName" class="col-sm-3 control-label">Last Name: </label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" placeholder="Last Name"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtLastName"
                                    CssClass="label label-danger"
                                    ErrorMessage="Last Name is required"></asp:RequiredFieldValidator>
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
                            <label for="txtLastName" class="col-sm-3 control-label">Contact No: </label>
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
                            <label for="txtLastName" class="col-sm-3 control-label">Email: </label>
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
