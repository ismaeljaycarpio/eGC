﻿<%@ Page Title="Edit Guest" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="editguest.aspx.cs" Inherits="eGC.guest.editguest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Guest Details
                </div>
                <asp:Panel ID="pnlInputForm" runat="server" CssClass="panel-body">
                    <div class="form-horizontal">
                        <asp:Panel ID="pnlSuccess" runat="server" CssClass="alert alert-success" Visible="false">
                            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                            <strong>Success!</strong> Guest successfully updated.
                        </asp:Panel>

                        <div class="form-group">
                            <label for="imgProfile" class="col-sm-3 control-label">&nbsp;</label>
                            <div class="col-sm-6">
                                <asp:Image ID="imgProfile" runat="server" AlternateText="Profile Image" Height="200" Width="200" />
                                <asp:Image ID="IDPic" runat="server" AlternateText="ID Image" Height="200" Width="200" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="imgProfile" class="col-sm-3 control-label">Profile Picture: </label>
                            <div class="col-sm-6">
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="imgProfile" class="col-sm-3 control-label">Valid ID Picture: </label>
                            <div class="col-sm-6">
                                <asp:FileUpload ID="FileUpload2" runat="server" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtEmpId" class="col-sm-3 control-label">ID: </label>
                            <div class="col-sm-6">
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-user"></span></span>
                                    <asp:TextBox ID="txtGuestId" runat="server" CssClass="form-control" placeholder="ID" Enabled="false"></asp:TextBox>
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
                            <label for="ddlCompanyName" class="col-sm-3 control-label">Company Name: </label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddlCompanyName"
                                    runat="server"
                                    CssClass="form-control">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11"
                                    runat="server"
                                    Display="Dynamic"
                                    InitialValue="0"
                                    ControlToValidate="ddlCompanyName"
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
                                <asp:Button ID="btnUpdate"
                                    runat="server"
                                    CssClass="btn btn-primary"
                                    Text="Update"
                                    CausesValidation="true"
                                    OnClick="btnUpdate_Click" />
                                <asp:LinkButton ID="lbtnClose" 
                                    runat="server"
                                    CausesValidation="false"
                                    PostBackUrl="~/guest/default.aspx"
                                    CssClass="btn btn-default">Close</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfUserId" runat="server" />
</asp:Content>
