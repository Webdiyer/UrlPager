using System;
using System.Web.Routing;

namespace Webdiyer.UrlPagerDemo
{
    public class Global : System.Web.HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
        }
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("UrlPager_Basic","{controller}/{action}/{id}","~/Default.aspx",false,new RouteValueDictionary{{"controller","Basic"},{"action","page"},{"id",null}},new RouteValueDictionary{{"controller","Basic"},{"action","page"}});
            routes.MapPageRoute("UrlPager_Repeater",
                "{controller}/{action}/{id}",
                "~/Repeater.aspx", false, new RouteValueDictionary { { "controller", "Repeater" }, { "action", "Page" } ,{"id",null}}, new RouteValueDictionary { { "controller", "Repeater" }, { "action", "Page" } });
            routes.MapPageRoute("UrlPager_ReversePageIndex",
                "{controller}/{action}/{pageIndex}",
                "~/ReversePageIndex.aspx", false, new RouteValueDictionary { { "controller", "ReversePageIndex" }, { "action", "List" }, { "pageIndex", null } }, new RouteValueDictionary { { "controller", "ReversePageIndex" }, { "action", "List" } });
            routes.MapPageRoute("UrlPager_RouteValues",
                "{controller}/{action}/{id}",
                "~/RouteValues.aspx", false, new RouteValueDictionary { { "id", null }, { "action", "ShowPage" } }, new RouteValueDictionary { { "controller", "RouteValues" } });
            routes.MapPageRoute("UrlPager_Bootstrap",
                "{controller}/{pageIndex}",
                "~/Bootstrap.aspx", false, new RouteValueDictionary { { "controller", "Bootstrap" }, { "pageIndex", null } }, new RouteValueDictionary { { "controller", "Bootstrap" } });
            routes.MapPageRoute("UrlPager_Postback",
                "{controller}/{pageIndex}",
                "~/Postback.aspx", false, new RouteValueDictionary { { "controller", "Postback" }, { "pageIndex",null } }, new RouteValueDictionary { { "controller", "Postback" } });
        }
    }
}
