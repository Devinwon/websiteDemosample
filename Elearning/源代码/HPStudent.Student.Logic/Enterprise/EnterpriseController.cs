using HPStudent.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HPStudent.Student.Logic.Enterprise
{
    public class EnterpriseController : Controller
    {

        public ActionResult Index()
        {
            HPStudent.Student.ViewModel.Enterprise.Enterprise modelTemp = new ViewModel.Enterprise.Enterprise();
            modelTemp.EID = Convert.ToInt16(HPStudent.Core.CookieHelper.GetCookieValue("StudentID"));
            HPStudent.Student.ViewModel.Enterprise.Enterprise model = HPStudent.Student.Business.Enterprise.GetEnterpriseByID(modelTemp);
            ViewBag.JobCount = HPStudent.Student.Business.Enterprise.GetJobCountBySID(model.Profile.SID);
            return View(model);
        }
        public ActionResult EnterpriseInfoDetail()
        {
            HPStudent.Student.ViewModel.Enterprise.Enterprise modelTemp = new ViewModel.Enterprise.Enterprise();
            modelTemp.EID = Convert.ToInt32(string.IsNullOrEmpty(Request.QueryString["NowSid"].ToString()) == true ? "-1" : Request.QueryString["NowSid"]);
            HPStudent.Student.ViewModel.Enterprise.Enterprise model = HPStudent.Student.Business.Enterprise.GetEnterpriseByID(modelTemp);
            ViewBag.JobCount = HPStudent.Student.Business.Enterprise.GetJobCountBySID(model.Profile.SID);
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult RegisterEnterpriseInfo()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult RegisterNewEnterprise()
        {
            string strAccount = Request.Params["EmailName"];
            string strPassword = Request.Params["Password"];
            HPStudent.Student.ViewModel.Common.RequestResult res = HPStudent.Student.Business.Enterprise.InsertNewEnterprise(strAccount, strPassword);
            return Json(res);
        }

        public ActionResult UpdateEnterpriseInfo()
        {
            HPStudent.Student.ViewModel.Enterprise.Enterprise modelTemp = new ViewModel.Enterprise.Enterprise();
            modelTemp.EID = Convert.ToInt16(HPStudent.Core.CookieHelper.GetCookieValue("StudentID"));
            HPStudent.Student.ViewModel.Enterprise.Enterprise model = HPStudent.Student.Business.Enterprise.GetEnterpriseByID(modelTemp);
            ViewBag.EnterpriseScaleList = HPStudent.Student.Business.Common.StudentCommon.GetOptionListByCode("CompanyScale");
            return View(model);
        }

        public ActionResult UpdateEnterpriseAction(HPStudent.Entity.CompanyInfo model)
        {
            HPStudent.Student.ViewModel.Common.RequestResult res = HPStudent.Student.Business.Enterprise.UpdateEnterprise(model);
            return Json(res);
        }

        public ActionResult QuerySimilarEnterpriseJson(int draw, int start, int length, HPStudent.Student.ViewModel.Enterprise.CompanyInfoViewModel keywords)
        {
            ViewModel.Common.Datatable<HPStudent.Student.ViewModel.Enterprise.EnterpriseItem> Res = HPStudent.Student.Business.Enterprise.QuerySimilarEnterprise(keywords, length, start);
            Res.draw = draw;
            return Json(Res);
        }

        public ActionResult QueryInitialData(int draw, int start, int length)
        {
            HPStudent.Student.ViewModel.Enterprise.CompanyInfoViewModel keywords = new ViewModel.Enterprise.CompanyInfoViewModel();
            return this.QuerySimilarEnterpriseJson(draw, start, length, keywords);
        }

        public ActionResult EnterpriseInfoList()
        {
            return View();
        }

        public ActionResult QueryEnterprisePage(int EID)
        {
            HPStudent.Student.ViewModel.Enterprise.Enterprise modelTemp = new ViewModel.Enterprise.Enterprise();
            modelTemp.EID = EID;
            HPStudent.Student.ViewModel.Enterprise.Enterprise model = HPStudent.Student.Business.Enterprise.GetEnterpriseByID(modelTemp);
            return View(model);
        }
        public ActionResult UpLoadEnterpriseInfo()
        {
            return View();
        }
        public ActionResult UpLoadInfo()
        {
            #region 获取文件测试
            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            string imgPath = "";
            string PhysicalPath = "";
            string DatePre = DateTime.Now.ToString("yyyyMMdd_HHmmss_ffff_") + HPStudent.Core.RandCode.Number(4) + "_";
            if (hfc.Count > 0)
            {
                imgPath = "/Upload/Enterprise/" + DatePre + hfc[0].FileName.Substring(hfc[0].FileName.LastIndexOf("\\") + 1);
                PhysicalPath = Server.MapPath(imgPath);
                hfc[0].SaveAs(PhysicalPath);
            }

            string[] SheetNames = HPStudent.Core.ExcelHelper.GetExcelSheetNames(PhysicalPath);

            bool version = HPStudent.Core.ExcelHelper.IsExcel2007(imgPath);
            System.Data.DataSet ds = HPStudent.Core.ExcelHelper.GetExcelToDataSet(PhysicalPath, true, "Sheet1$", "A1", "N5000");

            System.Data.DataTable getDt = ds.Tables[0];
            List<HPStudent.Student.ViewModel.Enterprise.EnterpriseWithJobs> model = new List<HPStudent.Student.ViewModel.Enterprise.EnterpriseWithJobs>();

            int iTempIndex = -1;
            foreach (System.Data.DataRow item in getDt.Rows)
            {
                if (string.IsNullOrEmpty(Convert.ToString(item[0])))
                {
                    continue;
                }

                if (item[0].ToString() == "企业全称")
                {
                    //如果开始了一个企业信息航，创建一个实例对象
                    HPStudent.Student.ViewModel.Enterprise.EnterpriseWithJobs temp = new ViewModel.Enterprise.EnterpriseWithJobs();
                    temp.Basic.CompanyName = Convert.ToString(item[1]);
                    temp.Basic.Address = Convert.ToString(item[3]);
                    temp.Basic.CompanyProfile = Convert.ToString(item[5]);
                    temp.Basic.Scale = Convert.ToString(item[7]);
                    temp.Basic.WebSite = Convert.ToString(item[9]);
                    temp.Basic.TelPhone = Convert.ToString(item[11]);
                    temp.Basic.Email = Convert.ToString(item[13]);
                    model.Add(temp);
                    iTempIndex = model.Count - 1;
                }

                if (item[0].ToString() == "岗位名称")
                {
                    HPStudent.Entity.JobTittle tempJob = new Entity.JobTittle();
                    tempJob.Name = Convert.ToString(item[1]);
                    tempJob.City = Convert.ToString(item[3]);
                    tempJob.SalaryRange = Convert.ToString(item[5]);
                    tempJob.WorkType = Convert.ToString(item[7]);
                    tempJob.DegreeRequired = Convert.ToString(item[9]);
                    tempJob.ExperienceRequired = Convert.ToString(item[11]);
                    tempJob.JobDescription = Convert.ToString(item[13]);
                    model[iTempIndex].CompanyJobTittle.Add(tempJob);
                }
            }
            List<string> getResult = HPStudent.Student.Business.Enterprise.ImportEnterpriseAndJobTittles(model);
            return Json(new { total = model.Count, names = getResult });
            #endregion
        }
        // 就业协议上传页面加载
        public ActionResult UploadEnterpriseAgreement()
        {
            return View();
        }
        // 就业协议上传
        public ActionResult UploadAgreement(int SID)
        {
            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            string imgPath = "";
            string PhysicalPath = "";
            string DatePre = DateTime.Now.ToString("yyyyMMdd_HHmmss_ffff_") + HPStudent.Core.RandCode.Number(4) + "_";
            if (hfc.Count > 0)
            {
                imgPath = "/Upload/Agreement/" + DatePre + hfc[0].FileName.Substring(hfc[0].FileName.LastIndexOf("\\") + 1);
                PhysicalPath = Server.MapPath(imgPath);
                hfc[0].SaveAs(PhysicalPath);
            }
            //数据库就业协议修改
            HPStudent.Entity.CompanyInfo model = new Entity.CompanyInfo();
            model.SID = SID;
            model.Agreement = imgPath;
            HPStudent.Student.ViewModel.Common.RequestResult res = HPStudent.Student.Business.Enterprise.UploadAgreement(model);
            return Json(res);
        }
        //企业职位维护
        public ActionResult JobIndex()
        {
            //加载数据
            //薪资范围
            ViewBag.SalaryRange = HPStudent.Student.Business.Common.StudentCommon.GetOptionListByCode("SalaryRange");
            return View();
        }
        //企业职位查询
        [HttpPost]
        public ActionResult QuerySimilarJobJson(int draw, int start, int length, HPStudent.Student.ViewModel.Job.JobListParameter keyWord)
        {
            keyWord.Sid = Convert.ToInt32(Core.CookieHelper.GetCookieValue("StudentID"));
            ViewModel.Common.Datatable<HPStudent.Student.ViewModel.Job.JobList> Res = HPStudent.Student.Business.Job.QueryJobList(keyWord, length, start);
            Res.draw = draw;
            return Json(Res);
        }
        //职位详情
        public ActionResult QueryJobDetails(int JId)
        {
            HPStudent.Student.ViewModel.Job.JobList model = HPStudent.Student.Business.Job.GetJobInfoByID(JId);
            return View(model);
        }
        //职位编辑
        public ActionResult JobEdit(int JID)
        {
            //加载数据
            ViewBag.Province = HPStudent.Student.Business.Common.SchoolCommon.GetComAreaByParentAID("0");
            List<HPStudent.Entity.OptionList> liOptionList = HPStudent.Student.Business.Common.StudentCommon.GetOptionListInCode("'SalaryRange','DegreeRequired','ExperienceRequired','WorkType'");
            ViewBag.SalaryRangeEdit = liOptionList.Where(x => x.ListTypeCode == "SalaryRange").ToList();
            ViewBag.DegreeRequiredEdit = liOptionList.Where(x => x.ListTypeCode == "DegreeRequired").ToList();
            ViewBag.ExperienceRequiredEdit = liOptionList.Where(x => x.ListTypeCode == "ExperienceRequired").ToList();
            ViewBag.WorkTypeEdit = liOptionList.Where(x => x.ListTypeCode == "WorkType").ToList();
            HPStudent.Student.ViewModel.Job.JobList model = new ViewModel.Job.JobList();
            ViewBag.IsCheckProvince = "";
            if (JID != 0)
            {
                model = HPStudent.Student.Business.Job.GetJobInfoByID(JID);
                ViewBag.IsCheckProvince = HPStudent.Student.Business.Common.SchoolCommon.GetComAreaByAreaName(model.City).AreaID;
            }
            return View(model);
        }
        //本公司职位新增
        public ActionResult JobAdd(HPStudent.Entity.JobTittle jobtitle)
        {
            jobtitle.SID = Convert.ToInt32(Core.CookieHelper.GetCookieValue("StudentID"));
            HPStudent.Student.ViewModel.Common.RequestResult res = HPStudent.Student.Business.Job.EditJobItem(jobtitle);
            return Json(res);
        }
        //本公司职位修改
        public ActionResult JobUpdate(HPStudent.Entity.JobTittle jobtitle)
        {
            jobtitle.SID = Convert.ToInt32(Core.CookieHelper.GetCookieValue("StudentID"));
            HPStudent.Student.ViewModel.Common.RequestResult res = HPStudent.Student.Business.Job.EditJobItem(jobtitle);
            return Json(res);
        }
        //获取城市子项
        [HttpPost]
        public JsonResult GetCityListByParentAID(string ParentAID)
        {
            return Json(Business.Common.SchoolCommon.GetComAreaByParentAID(ParentAID));
        }
        //编辑企业信息
        public ActionResult Pop_EditCompany(int SID)
        {
            HPStudent.Student.ViewModel.Enterprise.CompanyInfoResult result = new ViewModel.Enterprise.CompanyInfoResult();
            if (SID != 0)
            {
                result = Business.Enterprise.GetCompanyInfoByID(SID);
            }
            //从参数表中加载公司范围下拉框
            result.ScaleList = Business.Common.StudentCommon.GetOptionListByCode("CompanyScale");
            return View(result);
        }
        //保存公司信息
        public JsonResult SaveCompanyInfo(Entity.CompanyInfo KeyWords)
        {
            HPStudent.Student.ViewModel.Common.RequestResult res = Business.Enterprise.SaveCompanyInfo(KeyWords);
            return Json(res);
        }
        //职位弹窗
        public ActionResult Pop_JobShow()
        {
            return View();
        }
        //职位编辑弹窗
        public ActionResult Pop_JobEdit(int JID)
        {
            //加载数据
            ViewBag.Province = HPStudent.Student.Business.Common.SchoolCommon.GetComAreaByParentAID("0");
            List<HPStudent.Entity.OptionList> liOptionList = HPStudent.Student.Business.Common.StudentCommon.GetOptionListInCode("'SalaryRange','DegreeRequired','ExperienceRequired','WorkType'");
            ViewBag.SalaryRangeEdit = liOptionList.Where(x => x.ListTypeCode == "SalaryRange").ToList();
            ViewBag.DegreeRequiredEdit = liOptionList.Where(x => x.ListTypeCode == "DegreeRequired").ToList();
            ViewBag.ExperienceRequiredEdit = liOptionList.Where(x => x.ListTypeCode == "ExperienceRequired").ToList();
            ViewBag.WorkTypeEdit = liOptionList.Where(x => x.ListTypeCode == "WorkType").ToList();
            HPStudent.Student.ViewModel.Job.JobList model = new ViewModel.Job.JobList();
            ViewBag.IsCheckProvince = "";
            if (JID != 0)
            {
                model = HPStudent.Student.Business.Job.GetJobInfoByID(JID);
                ViewBag.IsCheckProvince = HPStudent.Student.Business.Common.SchoolCommon.GetComAreaByAreaName(model.City).AreaID;
            }
            return View(model);
        }
        //职位新增
        public ActionResult pop_JobAdd(HPStudent.Entity.JobTittle jobtitle)
        {
            HPStudent.Student.ViewModel.Common.RequestResult res = HPStudent.Student.Business.Job.EditJobItem(jobtitle);
            return Json(res);
        }
        //职位修改
        public ActionResult pop_JobUpdate(HPStudent.Entity.JobTittle jobtitle)
        {
            HPStudent.Student.ViewModel.Common.RequestResult res = HPStudent.Student.Business.Job.EditJobItem(jobtitle);
            return Json(res);
        }
    }
}
