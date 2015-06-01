/* UrlPager source code
This file is part of UrlPager.
Copyright 2003-2015 Webdiyer(http://en.webdiyer.com)
Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
    http://www.apache.org/licenses/LICENSE-2.0
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
using System;
using System.IO;
using System.Web.UI;

namespace Webdiyer.WebControls
{
    public class UrlPagerDesigner:System.Web.UI.Design.ControlDesigner
    {

        private UrlPager pager;

        public override string GetEditableDesignerRegionContent(System.Web.UI.Design.EditableDesignerRegion region)
        {
            region.Selectable = false;
            return null;
        }

        public override string GetDesignTimeHtml()
        {
            pager = (UrlPager)Component;
            pager.TotalItemCount = 115;
            var sw = new StringWriter();
            var writer = new HtmlTextWriter(sw);
            pager.RenderControl(writer);
            return sw.ToString();
        }

        protected override string GetErrorDesignTimeHtml(Exception e)
        {
            string errorstr = "Error creating control：" + e.StackTrace;
            return CreatePlaceHolderDesignTimeHtml(errorstr);
        }

    }
}
