using System;
using System.Web.Mvc;
using System.Collections.Generic;
using HPStudent.Core;
using HPStudent.Student.Business;
using HPStudent.Entity;
using StuVModel = HPStudent.Student.ViewModel;


namespace HPStudent.Student.Logic
{
    public class ProjectsController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Detail(int id)
        {
            StuVModel.Project.DetailProject myDetailProject = HPStudent.Student.Business.Projects.GetDetailProjectByPID(id);
            return View(myDetailProject);
        }

        [HttpPost]
        public JsonResult GetAllMajorList()
        {
            List<Entity.Major> MajorList = HPStudent.Student.Business.Projects.GetMajorList();

            return Json(MajorList);
        }
        
        

        [HttpGet]
        public ActionResult Pop_ShowPDF(String TID)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Pop_ShowVideo(String TID)
        {
            return View();
        }

        /// <summary>
        /// 获得所有短训推荐项目
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public JsonResult GetProjectListByShort(int CID, int start, int length)
        {
            StuVModel.Common.GridView<StuVModel.Project.MainProject> ProjectList = HPStudent.Student.Business.Projects.GetProjectListByShort(CID, start, length);
            return Json(ProjectList);
        }


        [HttpPost]
        public JsonResult GetPorjectList(int CID,int start, int length)
        {
            StuVModel.Common.GridView<StuVModel.Project.MainProject> ProjectList = HPStudent.Student.Business.Projects.GetStudentProjectList(CID, start, length);
            return Json(ProjectList);
            

            //var jResult = new
            //{
            //    recordsTotal = 200,
            //    data = new[] { 
            //        new {                        
            //             yoyotui_title =  "标题" + (start*CID).ToString(),
            //             yoyotui_img =  "http://img.lanrentuku.com/img/allimg/1301/46-1301141K234.gif",
            //             yoyotui_url = "##1"
            //        },
            //        new {                        
            //             yoyotui_title =  "标题2",
            //             yoyotui_img =  "http://img.lanrentuku.com/img/allimg/1301/46-1301141K235-50.gif",
            //             yoyotui_url = "##2"
            //        },
            //        new {                        
            //             yoyotui_title =  "标题3",
            //             yoyotui_img =  "http://img.lanrentuku.com/img/allimg/1301/46-1301141K236-52.gif",
            //             yoyotui_url = "##3"
            //        },
            //        new {                        
            //             yoyotui_title =  "标题4",
            //             yoyotui_img =  "http://img.lanrentuku.com/img/allimg/1301/46-1301141K239-50.gif",
            //             yoyotui_url = "##3"
            //        },
            //        new {                        
            //             yoyotui_title =  "标题5",
            //             yoyotui_img =  "http://img.lanrentuku.com/img/allimg/1301/46-1301141K238-50.gif",
            //             yoyotui_url = "##3"
            //        },
            //        new {                        
            //             yoyotui_title =  "标题6",
            //             yoyotui_img =  "http://img.lanrentuku.com/img/allimg/1301/46-1301141K240-50.gif",
            //             yoyotui_url = "##3"
            //        },
            //        new {                        
            //             yoyotui_title =  "标题7",
            //             yoyotui_img =  "http://img.lanrentuku.com/img/allimg/1301/46-1301141K242-51.gif",
            //             yoyotui_url = "##3"
            //        },
            //        new {                        
            //             yoyotui_title =  "标题8",
            //             yoyotui_img =  "http://img.lanrentuku.com/img/allimg/1301/46-1301141K244-53.gif",
            //             yoyotui_url = "##3"
            //        }
            //    }
            //};
            //return Json(jResult);
        }
    }
}
