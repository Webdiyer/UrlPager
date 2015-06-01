using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Webdiyer.UrlPagerDemo
{
    public partial class Bootstrap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                pager1.TotalItemCount = (int)SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "P_GetOrderNumber");
        }

        protected void PageChanged(object sender, EventArgs e)
        {
            Repeater1.DataSource = SqlHelper.ExecuteReader(CommandType.StoredProcedure, ConfigurationManager.AppSettings["pagedSPName"],
                new SqlParameter("@startIndex", pager1.StartItemIndex),
                new SqlParameter("@endIndex", pager1.EndItemIndex));
            Repeater1.DataBind();
        }
    }
}