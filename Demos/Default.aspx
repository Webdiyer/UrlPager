<%@ Page Title="Basic features" MetaDescription="This sample demonstrates the besic features of the UrlPager control." Language="C#" MasterPageFile="UrlPager.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Webdiyer.UrlPagerDemo._Default" %>
<%@ Register TagPrefix="webdiyer" Namespace="Webdiyer.WebControls" Assembly="Webdiyer.UrlPager" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
 <div><asp:Label runat="server" ID="lb_txt" EnableViewState="false"></asp:Label>
<asp:Label runat="server" ID="lb_btn" EnableViewState="false"></asp:Label>
</div>
<webdiyer:UrlPager  runat="server"  id="pager1" RouteName="UrlPager_Basic"  PageIndexParameterName="id" InheritsRouteValues="true" PagerItemTemplate="&nbsp;{0}"
   TotalItemCount="198"  OnPageChanged="PageChanged"></webdiyer:UrlPager> 

    </asp:Content>
