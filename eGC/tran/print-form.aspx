<%@ Page Title="Print GC" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="print-form.aspx.cs" Inherits="eGC.tran.print_form" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%--hide export to excel and word in dropdown--%>
    <script>
        $(document).ready(function()
        {
            $("a[title='Excel']").parent().hide();
            $("a[title='Word']").parent().hide();
        });
    </script>

    <div class="row">
        <div class="col-md-12 col-md-offset-2">
            <rsweb:ReportViewer ID="ReportViewer1"
                runat="server"
                Height="800"
                SizeToReportContent="true">
            </rsweb:ReportViewer>
        </div>
    </div>
</asp:Content>
