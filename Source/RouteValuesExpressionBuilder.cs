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
using System.CodeDom;
using System.ComponentModel;
using System.Web.Compilation;
using System.Web.Routing;
using System.Web.UI;

namespace Webdiyer.WebControls {

    ///<include file='Documentation.xml' path='UrlPagerDoc/Class[@name="RouteValuesExpressionBuilder"]'/>
    [ExpressionPrefix("RouteValues")]
    public class RouteValuesExpressionBuilder : ExpressionBuilder
    {
        public override CodeExpression GetCodeExpression(BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
        {
            Type type = entry.DeclaringType;
            PropertyDescriptor descriptor =
                TypeDescriptor.GetProperties(type)
                    [entry.PropertyInfo.Name];
            CodeExpression[] expressionArray =
                new CodeExpression[3];
            expressionArray[0] = new
                CodePrimitiveExpression(entry.Expression.Trim());
            expressionArray[1] = new
                CodeTypeOfExpression(type);
            expressionArray[2] = new
                CodePrimitiveExpression(entry.Name);

            return new CodeCastExpression(descriptor.PropertyType
                                          , new CodeMethodInvokeExpression(
                                                new CodeTypeReferenceExpression(GetType())
                                                , "GetRouteValues"
                                                , expressionArray));

        }

        public static object GetRouteValues(string expression, Type target, string entry)
        {
            var values = new RouteValueDictionary();
            string[] rarr = expression.Split(',');
            foreach(string s in rarr)
            {
                string[] varr = s.Trim().Split('=');
                if(varr.Length==2)
                values.Add(varr[0].Trim(), varr[1].Trim(new char[]{'\"','\'',' '}));
            }
            return values;
        }
    }
}
