using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Optimization;


namespace System.Web.Mvc.Html
{
    public static class Html
    {
        /// <summary>
        /// 动态捆绑多个脚本
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="scripts">javscript</param>
        /// <returns></returns>
        public static IHtmlString BundleScripts(this HtmlHelper htmlHelper, params Func<object, object>[] scripts)
        {
            if (scripts == null)
            {
                throw new ArgumentNullException("scripts");
            }

            var inputs = new StringBuilder();
            foreach (var script in scripts)
            {
                inputs.AppendLine(script.Invoke(null).ToString().ToLower());
            }

            var applicationPath = htmlHelper.ViewContext.HttpContext.Request.ApplicationPath;
            Func<string, string> fixSrc = (src) => applicationPath == "/" ? "~" + src : src.Replace(applicationPath, "~/");

            var srcs = Matches(inputs.ToString(), @"(?<=src="").+?\.js(?="")").Select(item => fixSrc(item)).ToArray();
            var path = string.Format("~/{0}", Math.Abs(string.Join(string.Empty, srcs).GetHashCode()));

            if (BundleTable.Bundles.GetBundleFor(path) == null)
            {
                BundleTable.Bundles.Add(new ScriptBundle(path).Include(srcs));
            }
            return Scripts.Render(path);
        }
        /// <summary>
        /// 匹配正则表示式
        /// </summary>
        /// <param name="source">源</param>
        /// <param name="pattert">regex参数</param>
        /// <returns></returns>
        public static string[] Matches(this string source, string pattert)
        {
            source = string.IsNullOrEmpty(source) == true ? string.Empty : source;
            pattert = string.IsNullOrEmpty(pattert) == true ? string.Empty : pattert;
            var mc = System.Text.RegularExpressions.Regex.Matches(source, pattert);
            string[] list = new string[mc.Count];
            int i = 0;
            foreach (System.Text.RegularExpressions.Match m in mc)
            {
                list[i] = m.Value;
                i++;
            }
            return list;
        }
    }
}