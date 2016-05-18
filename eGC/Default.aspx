<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="eGC.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script src="jquery.bxslider/jquery.bxslider.js" type="text/javascript"></script>
    <link href="jquery.bxslider/jquery.bxslider.css" type="text/css" rel="stylesheet" />

    <%--<div class="row">
        <div class="col-md-12">
            <ul class="bxslider">
                <li>
                    <img src="jquery.bxslider/images/azalea.jpg" />
                </li>
                <li>
                    <img src="jquery.bxslider/images/tradisyon.jpg" alt="Alternate Text" />
                </li>
                <li>
                    <img src="jquery.bxslider/images/azalea-baguio-tent-event-venue.jpg" alt="Alternate Text" />
                </li>
            </ul>
        </div>
    </div>--%>

    <script>
        $(document).ready(function () {
            $('.bxslider').bxSlider({
                adaptiveHeight: true,
                slideWidth: 1100
            });
        });
    </script>

</asp:Content>
