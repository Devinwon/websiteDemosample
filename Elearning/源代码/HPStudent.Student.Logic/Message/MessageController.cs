using System;
using System.Web.Mvc;
using HPStudent.Core;
using System.Collections.Generic;
using HPStudent.Entity;

namespace HPStudent.Student.Logic.Message
{
    public class MessageController : Controller
    {
        //加载收件箱页面
        public ActionResult GetMessageIndex()
        {
            SetMessageNum();
            return View();
        }
        //加载收件箱数据
        public ActionResult GetMessageData(int draw, int start, int length, HPStudent.Entity.Messages keyWord)
        {
            //收件箱条件封装
            keyWord.ReceiverID = Convert.ToInt16(HPStudent.Core.CookieHelper.GetCookieValue("StudentID"));
            ViewModel.Common.Datatable<HPStudent.Entity.Messages> Res = HPStudent.Student.Business.Messages.QueryMessageList(keyWord, length, start);
            Res.draw = draw;
            return Json(Res);
        }
        //加载发送页面
        public ActionResult SendMessageIndex()
        {
            SetMessageNum();
            return View();
        }
        //加载发送数据
        public ActionResult SendMessageData(int draw, int start, int length, HPStudent.Entity.Messages keyWord)
        {
            //发送条件封装
            keyWord.SenderID = Convert.ToInt16(HPStudent.Core.CookieHelper.GetCookieValue("StudentID"));
            ViewModel.Common.Datatable<HPStudent.Entity.Messages> Res = HPStudent.Student.Business.Messages.QueryMessageList(keyWord, length, start);
            Res.draw = draw;
            return Json(Res);
        }
        //加载查看收件详情界面
        public ActionResult ReaderGetMessageDetail(int MID)
        {
            HPStudent.Entity.Messages keyWord = new HPStudent.Entity.Messages();
            keyWord.MID = MID;
            keyWord.ReceiverID = Convert.ToInt16(HPStudent.Core.CookieHelper.GetCookieValue("StudentID"));
            HPStudent.Entity.Messages model = new HPStudent.Entity.Messages();
            List<HPStudent.Entity.Messages> list = HPStudent.Student.Business.Messages.MessagesListNotPage(keyWord);
            if (list.Count > 0)
            {
                model = list[0];
            }
            return View(model);
        }
        //设定收发邮件数量
        public void SetMessageNum()
        {
            int ReceiverID = Convert.ToInt16(HPStudent.Core.CookieHelper.GetCookieValue("StudentID"));
            string notReaderNum = HPStudent.Student.Business.Messages.GetNotReaderMessage(ReceiverID).ToString();
            if (notReaderNum == "0")
            {
                notReaderNum = string.Empty;
            }
            ViewBag.GetMessageNum = notReaderNum;
        }
    }
}
