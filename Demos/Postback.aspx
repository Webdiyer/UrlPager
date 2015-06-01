<%@ Page Title="Postback" MetaDescription="This sample demonstrates how UrlPager can retain current page index after post back." Language="C#" MasterPageFile="UrlPager.master" AutoEventWireup="true" CodeBehind="Postback.aspx.cs" Inherits="Webdiyer.UrlPagerDemo.Postback" %>
<%@ Register TagPrefix="webdiyer" Namespace="Webdiyer.WebControls" Assembly="Webdiyer.UrlPager" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
   
<div><asp:Label runat="server" ID="lb_txt" EnableViewState="false"></asp:Label>
<asp:Label runat="server" ID="lb_btn" EnableViewState="false"></asp:Label>
</div>
<webdiyer:UrlPager  runat="server"  id="pager1" RouteName="UrlPager_Postback"  InheritsRouteValues="true" PagerItemTemplate="&nbsp;{0}"
   TotalItemCount="198"  OnPageChanged="PageChanged"></webdiyer:UrlPager> 
   <asp:Button runat="server" ID="btn_test" Text="Post back test" OnClick="TestClick" />

    </asp:Content>