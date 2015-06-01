<%@ Page Title="Applying Bootstrap pagination styles" MetaDescription="This sample demonstrates how to apply Bootstrap pagination styles to the UrlPager control." Language="C#" MasterPageFile="UrlPager.master" AutoEventWireup="true" CodeBehind="Bootstrap.aspx.cs" Inherits="Webdiyer.UrlPagerDemo.Bootstrap" %>
<%@ Register TagPrefix="webdiyer" Namespace="Webdiyer.WebControls" Assembly="Webdiyer.UrlPager" %>


<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
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
<webdiyer:UrlPager PageSize="5" NumericPagerItemCount="8" ContainerTag="Ul" PagerItemTemplate="<li>{0}</li>" CssClass="pagination" CurrentPagerItemTemplate="<li class='active'><a href='#'>{0}</a></li>" id="pager1"
   DisabledPagerItemTemplate="<li class='disabled'><a href='#'>{0}</a></li>" runat="server"  RouteName="UrlPager_Bootstrap"  InheritsRouteValues="true"
   OnPageChanged="PageChanged"></webdiyer:UrlPager> 

    </asp:Content>
