using HPStudent.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HPStudent.Student.Logic.Student
{
    public class StudentController : Controller
    {
        public ActionResult EnterpriseSearchStudentList()
        {
            //获取城市联动
            ViewBag.Province = Business.Common.SchoolCommon.GetComAreaByParentAID("0");
            return View();
        }
        //人才信息列表
        [HttpPost]
        public ActionResult QuerySimilarStudentInfoJson(int draw, int start, int length, HPStudent.Student.ViewModel.Student.EnterpriseSearchViewModel KeyWords)
        {
            ViewModel.Common.Datatable<HPStudent.Student.ViewModel.Student.EnterpriseSearchBase> Res = HPStudent.Student.Business.Student.QueryEnterpriseReviewList(length, start, KeyWords);
            Res.draw = draw;
            return Json(Res);
        }
        //发送面试邀请界面
        public ActionResult SendInvitation(int StudentID, string StudentName)
        {
            ViewBag.StudentID = StudentID;
            ViewBag.StudentName = StudentName;
            //获取当前登入公司的ID
            int Sid = Convert.ToInt16(HPStudent.Core.CookieHelper.GetCookieValue("StudentID"));
            //加载本公司岗位信息
            ViewBag.Job = HPStudent.Student.Business.Job.GetJobInfoSID(Sid);
            return View();
        }
        //发送面试邀请
        public ActionResult SendInvitationInfo(HPStudent.Student.ViewModel.Student.InterviewInvitationBase invitation)
        {
            bool falg = false;
            //获取当前登入公司的ID
            int Sid = Convert.ToInt16(HPStudent.Core.CookieHelper.GetCookieValue("StudentID"));
            List<HPStudent.Entity.InterviewInvitation> interList = HPStudent.Student.Business.InterviewInvitation.getInvitationList(Sid, invitation.SID);
            if (interList.Count > 0 && interList != null)
            {
                if (interList[0].SendDate.AddDays(30) < DateTime.Now)
                {
                    falg = true;
                }
            }
            else
            {
                falg = true;
            }
            HPStudent.Student.ViewModel.Common.RequestResult res = new HPStudent.Student.ViewModel.Common.RequestResult();
            if (falg == true)
            {
                //面试邀请函封装数据
                invitation.SendDate = DateTime.Now;
                invitation.IsRead = "0";
                invitation.SenderID = Sid;
                res = HPStudent.Student.Business.Enterprise.SendInvitationInfo(invitation);
                if (res.ResultState == 0)
                {
                    HPStudent.Student.ViewModel.Enterprise.Enterprise tempEnter = new ViewModel.Enterprise.Enterprise();
                    tempEnter.EID = Sid;
                    //获取公司名称
                    HPStudent.Student.ViewModel.Enterprise.Enterprise enterprise = HPStudent.Student.Business.Enterprise.GetEnterpriseByID(tempEnter);
                    //站内消息封装数据
                    HPStudent.Entity.Messages messages = new Entity.Messages();
                    messages.Body = invitation.Content;
                    messages.DateCreated = DateTime.Now;
                    messages.IP = HttpHelper.IPAddress();
                    messages.IsRead = 0;
                    messages.Receiver = invitation.Receiver;
                    messages.ReceiverID = invitation.SID;
                    messages.Sender = enterprise.Profile.CompanyName;
                    messages.SenderID = Sid;
                    messages.Title = enterprise.Profile.CompanyName + "面试邀请";
                    messages.Type = 0;
                    HPStudent.Student.Business.Messages.MessagesAdd(messages);
                }
            }
            else
            {
                res.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                res.ResultMsg = "发送面试邀请失败,30天内不可重复发送邀请";
            }

            return Json(res);
        }
    }
}
