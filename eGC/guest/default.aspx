<%@ Page Title="Guest List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="eGC.guest._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearch">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <h5>Guests</h5>
                    </div>

                    <div class="panel-body">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-sm-10">
                                    <div class="input-group">
                                        <span class="input-group-btn">
                                            <asp:Button ID="btnSearch"
                                                runat="server"
                                                CssClass="btn btn-primary"
                                                Text="Go"
                                                OnClick="btnSearch_Click" />
                                        </span>
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search..."></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="table-responsive">
                            <asp:UpdatePanel ID="upGuests" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="gvGuests"
                                        runat="server"
                                        CssClass="table table-striped table-hover dataTable"
                                        GridLines="None"
                                        AutoGenerateColumns="false"
                                        AllowPaging="true"
                                        AllowSorting="true"
                                        EmptyDataText="No Record(s) found"
                                        ShowHeaderWhenEmpty="true"
                                        DataKeyNames="Id"
                                        OnRowDataBound="gvGuests_RowDataBound"
                                        OnPageIndexChanging="gvGuests_PageIndexChanging"
                                        OnRowCommand="gvGuests_RowCommand"
                                        OnSorting="gvGuests_Sorting"
                                        PageSize="10">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Row Id" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbEdit" runat="server" Text="Edit" CommandName="editRecord" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="GuestId" HeaderText="ID" SortExpression="GuestId" />
                                            <asp:BoundField DataField="FullName" HeaderText="Name" SortExpression="FullName" />
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company" SortExpression="CompanyName" />
                                            <asp:BoundField DataField="Number" HeaderText="Contact No" SortExpression="Number" />

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnShowDelete" runat="server" Text="Delete" CommandName="deleteRecord" CssClass="btn btn-danger" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' />
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
    </asp:Panel>
</asp:Content>
