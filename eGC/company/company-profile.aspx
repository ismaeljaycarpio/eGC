<%@ Page Title="Company" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="company-profile.aspx.cs" Inherits="eGC.company.company_profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearch" CssClass="row">
        <div class="col-md-12">
            <div class="panel panel-danger">
                <div class="panel-heading">
                    <h5>Companies</h5>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-md-12">
                                <div class="input-group">
                                    <asp:TextBox ID="txtSearch"
                                        runat="server"
                                        CssClass="form-control"
                                        placeholder="Search..."></asp:TextBox>
                                    <span class="input-group-btn">
                                        <asp:Button ID="btnSearch"
                                            runat="server"
                                            CssClass="btn btn-primary"
                                            Text="Go"
                                            OnClick="btnSearch_Click" />
                                    </span>

                                    <div class="pull-right">
                                        <asp:Button ID="btnExport"
                                            runat="server"
                                            Text="Export to Excel"
                                            OnClick="btnExport_Click"
                                            CssClass="btn btn-default btn-sm" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="upGuests" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvCompany"
                                    runat="server"
                                    CssClass="table table-striped table-hover dataTable"
                                    GridLines="None"
                                    AutoGenerateColumns="False"
                                    AllowPaging="True"
                                    AllowSorting="True"
                                    EmptyDataText="No Record(s) found"
                                    ShowHeaderWhenEmpty="True"
                                    DataKeyNames="Id"
                                    DataSourceID="CompanyDataSource"
                                    OnRowCommand="gvCompany_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Company ID" SortExpression="CompanyId">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnCompanyId"
                                                    runat="server"
                                                    Text='<%# Eval("GuestId") %>'
                                                    CommandName="editRecord"
                                                    CommandArgument='<%#((GridViewRow)Container).RowIndex %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="CompanyName" HeaderText="Company" SortExpression="CompanyName" />
                                        <asp:BoundField DataField="ContactNumber" HeaderText="Number" SortExpression="Number" />
                                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnAddGC" 
                                                    runat="server" 
                                                    Text="Add GC" 
                                                    CommandName="addGC" 
                                                    CssClass="btn btn-success" 
                                                    CommandArgument='<%#((GridViewRow) Container).RowIndex %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnDelete" 
                                                    runat="server" 
                                                    Text="Delete" 
                                                    CommandName="deleteRecord" 
                                                    CommandArgument='<%#((GridViewRow)Container).RowIndex %>' 
                                                    CssClass="btn btn-danger"></asp:Button>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <PagerStyle CssClass="pagination-ys" />
                                </asp:GridView>
                                <asp:LinkButton ID="lbtnCreateCompany"
                                    runat="server"
                                    CssClass="btn btn-default"
                                    PostBackUrl="~/company/add-company.aspx">Create Company Profile</asp:LinkButton>
                            </ContentTemplate>
                            <Triggers>
                            </Triggers>
                        </asp:UpdatePanel>
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

    <asp:LinqDataSource ID="CompanyDataSource"
        OnSelecting="CompanyDataSource_Selecting"
        runat="server">
    </asp:LinqDataSource>
</asp:Content>
