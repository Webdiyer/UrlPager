<%@ Page Title="Paging for repeater" MetaDescription="This sample demonstrates how to use UrlPager control to add paging functionality to the Repeater control." Language="C#" MasterPageFile="UrlPager.Master" AutoEventWireup="true" CodeBehind="Repeater.aspx.cs" Inherits="Webdiyer.UrlPagerDemo.Repeater" %>
<%@Register Namespace="Webdiyer.WebControls" Assembly="Webdiyer.UrlPager" TagPrefix="webdiyer"%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label runat="server" ID="lb_msg"></asp:Label>
<asp:Repeater ID="Repeater1" runat="server">
        <HeaderTemplate>
        <table class="table table-bordered table-hover">
        <tr><th>Order ID</th><th>Order Date</th><th>Company Name</th><th>Customer ID</th><th>Employee Name</th></tr>
        </HeaderTemplate>
        <ItemTemplate>
        <tr>
        <td><%#DataBinder.Eval(Container.DataItem,"orderid")%></td>
        <td><%#DataBinder.Eval(Container.DataItem,"orderdate","{0:d}")%></td>
        <td><%#DataBinder.Eval(Container.DataItem, "companyname")%></td>
        <td><%#DataBinder.Eval(Container.DataItem,"customerid")%></td>
        <td><%#DataBinder.Eval(Container.DataItem,"employeename")%></td>
        </tr>
        </ItemTemplate>
        <FooterTemplate>
        </table>
        </FooterTemplate>
        </asp:Repeater>

    <webdiyer:UrlPager runat="server" id="pager1" RouteName="UrlPager_Repeater" CssClass="pull-right" PageIndexParameterName="id" PagerItemTemplate="&nbsp;{0}"
      OnPageChanged="PageChanged"></webdiyer:UrlPager> 
     <p>
</asp:Content>
