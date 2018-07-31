using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Web;
using HPStudent.Core;
using HPStudent.Business;
using HPStudent.Entity;


namespace HPStudent.Logic.Projects
{
    public class ProjectsController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public JsonResult GetAllMajorList()
        {
            List<Entity.Major> MajorList = Business.Admin.Projects.GetMajorList();

            return Json(MajorList);
        }

        [HttpPost]
        public JsonResult DelProject(int PID)
        {
            ViewModel.Common.RequestResult result = new ViewModel.Common.RequestResult();
            result = Business.Admin.Projects.DelProject(PID);
            return Json(result);
        }

        [HttpPost]
        public JsonResult GetProjectBookList(int MID, int draw, int start, int length)
        {
            //HPStudent.ViewModel.Projects.ProjectBookList pbList = new ViewModel.Projects.ProjectBookList() ;

            //pbList.pbList = Business.Admin.Projects.GetProjectBookList(MID, start, length);
            //pbList.draw = draw;

            HPStudent.ViewModel.Common.Datatable<HPStudent.ViewModel.Projects.ProjectBook> pbList = Business.Admin.Projects.GetProjectBookList(MID, start, length);
            pbList.draw = draw;
            return Json(pbList);
        }

        [HttpGet]
        public ActionResult Pop_Project_Add()
        {
            HPStudent.ViewModel.Projects.ProjectBook myProject = new ViewModel.Projects.ProjectBook();
            myProject.TeacherList = Business.Admin.Teacher.GetTeacherList();

            return View(myProject);
        }

        [HttpPost]
        public JsonResult Pop_Project_Add(HPStudent.ViewModel.Projects.ProjectBook project)
        {

            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            

            if (hfc.Count > 0)
            {
                try
                {
                    string trueName = hfc[0].FileName;
                    string fileExtension = System.IO.Path.GetExtension(trueName);
                    string imgPath = "/Library/Image/" + DateTime.Now.ToString("yyyyMM") + "/";
                    string filename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + fileExtension;
                    string PhysicalPath = Server.MapPath(imgPath + filename);
                    //imgPath = "/Library/Image/" + hfc[0].FileName.Substring(hfc[0].FileName.LastIndexOf("\\") + 1);
                    //创建目录
                    HPStudent.Core.FileHelper.CreateDir(Server.MapPath(imgPath));
                    hfc[0].SaveAs(PhysicalPath);

                    project.ProjectPic = imgPath + filename;
                }
                catch (Exception ex)
                {
                    project.ProjectPic = "";
                }

            }
            else
            {
                project.ProjectPic = "";
            }

            ViewModel.Common.RequestResult result = Business.Admin.Projects.AddProject(project);
            return Json(result,"text/html");
        }

        [HttpGet]
        public ActionResult Pop_Project_Edit(int PID)
        {
            HPStudent.ViewModel.Projects.ProjectBook myProject = new ViewModel.Projects.ProjectBook();
            myProject = Business.Admin.Projects.GetProjectBookByPID(PID);
            myProject.TeacherList = Business.Admin.Teacher.GetTeacherList();
            return View(myProject);
        }

        [HttpPost]
        public JsonResult Pop_Project_Edit(HPStudent.ViewModel.Projects.ProjectBook project)
        {

            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;


            if (hfc.Count > 0 && hfc[0].ContentLength>0)
            {
                try
                {
                    string trueName = hfc[0].FileName;
                    string fileExtension = System.IO.Path.GetExtension(trueName);
                    string imgPath = "/Library/Image/" + DateTime.Now.ToString("yyyyMM") + "/";
                    string filename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + fileExtension;
                    string PhysicalPath = Server.MapPath(imgPath + filename);          
                    //创建目录
                    HPStudent.Core.FileHelper.CreateDir(Server.MapPath(imgPath));
                    hfc[0].SaveAs(PhysicalPath);

                    project.ProjectPic = imgPath + filename;
                }
                catch (Exception ex)
                {
                    project.ProjectPic = "";
                }

            }

            ViewModel.Common.RequestResult result = Business.Admin.Projects.EditProject(project);
            return Json(result, "text/html");
        }
        public ActionResult Detail(int PID)
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetProjectDetailByPID(int PID)
        {
            HPStudent.ViewModel.Projects.ProjectDetail myProject = Business.Admin.Projects.GetProjectDetailByPID(PID);

            return Json(myProject);
        }

        [HttpPost]
        public JsonResult GetProjectItemListByPID(int PID, int draw, int start, int length)
        {
            HPStudent.ViewModel.Common.Datatable<HPStudent.Entity.ProjectItem> myItemList = new ViewModel.Common.Datatable<HPStudent.Entity.ProjectItem>();
            myItemList.data = Business.Admin.Projects.GetProjectItemListByPID(PID);
            myItemList.draw = draw;
            myItemList.recordsFiltered = 0;
            myItemList.recordsTotal = 0;
            return Json(myItemList);
        }

        public ActionResult Pop_ProjectItem_Add()
        {
            return View();
        }
        public ActionResult Pop_ProjectItem_Edit(int ID)
        {
            Entity.ProjectItem myProjectItem = new ProjectItem();
            myProjectItem = Business.Admin.Projects.GetProjectItemByID(ID);

            return View(myProjectItem);
        }

        [HttpPost]
        public JsonResult AddProjectItem(Entity.ProjectItem myProjectItem)
        {
            ViewModel.Common.RequestResult result = Business.Admin.Projects.AddProjectItem(myProjectItem);
            return Json(result, "text/html");
        }

        [HttpPost]
        public JsonResult EditProjectItem(Entity.ProjectItem myProjectItem)
        {
            ViewModel.Common.RequestResult result = Business.Admin.Projects.EditProjectItem(myProjectItem);
            return Json(result, "text/html");
        }

        [HttpPost]
        public JsonResult DelProjectItem(int ID)
        {
            ViewModel.Common.RequestResult result = Business.Admin.Projects.DelProjectItem(ID);
            return Json(result, "text/html");
        }
    }
}
