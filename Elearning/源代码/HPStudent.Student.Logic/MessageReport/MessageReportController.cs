using System;
using System.Web.Mvc;
using HPStudent.Core;
using System.Collections.Generic;
using HPStudent.Entity;

namespace HPStudent.Student.Logic.MessageReport
{
    public class MessageReportController : Controller
    {
        //加载信息举报界面
        public ActionResult MessageReport(int Tid)
        {
            ViewBag.Tid = Tid;
            return View();
        }
        //举报消息发送
        public ActionResult MessageReportSend(HPStudent.Entity.MessageReport messageReport)
        {
            messageReport.IP = HttpHelper.IPAddress();
            HPStudent.Student.ViewModel.Common.RequestResult res = HPStudent.Student.Business.MessageReport.MessagesAdd(messageReport);
            return Json(res);
        }
        //加载举报信息列表
        public ActionResult MessageReportIndex()
        {
            return View();
        }
        //加载举报信息数据
        public ActionResult GetMessageReportData(int draw, int start, int length, HPStudent.Entity.MessageReport keyWord)
        {
            //收件箱条件封装
            ViewModel.Common.Datatable<HPStudent.Entity.MessageReport> Res = HPStudent.Student.Business.MessageReport.QueryMessageReportList(keyWord, length, start);
            Res.draw = draw;
            return Json(Res);
        }
    }
}
