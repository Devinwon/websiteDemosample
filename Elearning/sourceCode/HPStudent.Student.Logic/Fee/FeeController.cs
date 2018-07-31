using System;
using System.Web.Mvc;
using HPStudent.Core;
using HPStudent.Student.Business;
using System.Collections.Generic;
using System.Data;
using System.Web;
namespace HPStudent.Student.Logic
{
    public class FeeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Pop_AddFee()
        {
            return View();

        }
        public ActionResult Pop_ShowFee(string FeeID)
        {
            HPStudent.Student.ViewModel.Fee.FeeInfo Fee = HPStudent.Student.Business.Fee.GetFeeInfoByFeeID(FeeID);
            return View(Fee);

        }

        [HttpPost]
        public JsonResult Upload(string FeeTitle, string Year, string Fee)
        {
            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            string imgPath = "";
            string PhysicalPath = "";
            if (hfc.Count > 0)
            {
                imgPath = "/Upload/Fee/" + DateTime .Now .Year +""+DateTime .Now .Month+"" +DateTime .Now .Day+"" +DateTime .Now .Hour+""+DateTime .Now .Minute+""+DateTime .Now .Second +""+ hfc[0].FileName.Substring(hfc[0].FileName.LastIndexOf("\\") + 1);
                PhysicalPath = Server.MapPath(imgPath);
                hfc[0].SaveAs(PhysicalPath);
            }
            HPStudent.Student.ViewModel.Fee.FeeInfo ViewFee = new ViewModel.Fee.FeeInfo();
            ViewFee.Attachment = imgPath;
            ViewFee.Fee = Fee;
            ViewFee.FeeTitle = FeeTitle;
            ViewFee.FeeDescription = "";
            ViewFee.IsCheck = "0";
            ViewFee.NeedFee = "0";
            ViewFee.Year = Year;
            ViewFee.PaidFee = Fee;
            ViewFee.SID = CookieHelper.GetCookieValue("MID");
            int i = HPStudent.Student.Business.Fee.UploadFeeAttachment(ViewFee);
            return Json(i);
        }
        [HttpPost]
        public JsonResult GetFeeListBySID(string Year, int draw, int start, int length)
        {
            string StudentID = CookieHelper.GetCookieValue("MID");
            HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.Fee.FeeInfo> FeeList = HPStudent.Student.Business.Fee.GetFeeListBySID(StudentID, Year, start, length);
            FeeList.draw = draw;
            return Json(FeeList);

        }
    }
}
