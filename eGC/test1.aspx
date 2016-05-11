<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test1.aspx.cs" Inherits="eGC.test1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="txtpass" runat="server"></asp:TextBox>
            <asp:Button ID="btnClick" runat="server" Text="Click" OnClick="btnClick_Click" />
        </div>
    </form>
</body>
</html>
