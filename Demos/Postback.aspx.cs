using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Webdiyer.UrlPagerDemo
{
    public partial class Postback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void TestClick(object sender, EventArgs e)
        {
            lb_btn.Text = "Button clicked and page postback event fired";
        }

        protected void PageChanged(object sender, EventArgs e)
        {
            lb_txt.Text = "PageChanged event fired, current page index is " + pager1.CurrentPageIndex;
        }
    }
}