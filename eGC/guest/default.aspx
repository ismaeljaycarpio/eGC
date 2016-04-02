<%@ Page Title="Guest" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="eGC.guest._default" %>

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
                                <div class="col-md-12">
                                    <div class="input-group">
                                        <span class="input-group-btn">
                                            <asp:Button ID="btnSearch"
                                                runat="server"
                                                CssClass="btn btn-primary"
                                                Text="Go"
                                                OnClick="btnSearch_Click" />
                                        </span>
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search..."></asp:TextBox>
                                    <div class="pull-right">
                                <asp:Button ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_Click" CssClass="btn btn-default btn-sm"/>
                            </div>
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
                                                    <asp:LinkButton ID="lbtnGuestId" runat="server" Text='<%# Eval("GuestId") %>' CommandName="editRecord" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="FullName" HeaderText="Name" SortExpression="FullName" />
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company" SortExpression="CompanyName" />
                                            <asp:BoundField DataField="Number" HeaderText="Contact No" SortExpression="Number" />

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnShowDelete" runat="server" Text="Delete" CommandName="deleteRecord" CssClass="btn btn-danger" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnAddGC" runat="server" Text="Add GC" CommandName="addGC" CssClass="btn btn-success" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <PagerStyle CssClass="pagination-ys" />
                                    </asp:GridView>
                                    <asp:LinkButton ID="lbtnGuestProfile" runat="server" CssClass="btn btn-default" PostBackUrl="~/guest/createguest.aspx">Create Guest Profile</asp:LinkButton>
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

    <!-- Delete Modal -->
    <div id="deleteModal" class="modal fade" tabindex="-1" aria-labelledby="deleteModalLabel" aria-hidden="true" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Delete Record</h4>
                        </div>
                        <div class="modal-body">
                            Are you sure you want to delete this record ?
                            <asp:HiddenField ID="hfDeleteId" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Delete" OnClick="btnDelete_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnDelete" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

</asp:Content>
