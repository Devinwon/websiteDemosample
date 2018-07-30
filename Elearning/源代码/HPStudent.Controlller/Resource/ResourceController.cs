using System;
using System.Web.Mvc;
using System.Collections.Generic;
using HPStudent.Core;
using HPStudent.Business;
using HPStudent.Entity;
using System.Web;

namespace HPStudent.Logic.Resource
{
    public class ResourceController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public JsonResult GetAllMajorList()
        {
            List<Entity.Major> MajorList = Business.Admin.Resource.GetMajorList();

            return Json(MajorList);
        }

        [HttpPost]
        public JsonResult GetResourceBookList(int MID, int draw, int start, int length)
        {
            HPStudent.ViewModel.Common.Datatable<Entity.ResourceBook> pbList = Business.Admin.Resource.GetResourceBookList(MID, start, length);
            pbList.draw = draw; 
            return Json(pbList);
        }

        [HttpGet]
        public ActionResult Pop_Resource_Add()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Pop_Resource_Add(Entity.ResourceBook resource)
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
                    //创建目录
                    HPStudent.Core.FileHelper.CreateDir(Server.MapPath(imgPath));
                    //imgPath = "/Library/Image/" + hfc[0].FileName.Substring(hfc[0].FileName.LastIndexOf("\\") + 1);
                    hfc[0].SaveAs(PhysicalPath);

                    resource.ResourcePic = imgPath + filename;
                }
                catch (Exception ex)
                {
                    resource.ResourcePic = "";
                }

            }
            else
            {
                resource.ResourcePic = "";
            }

            ViewModel.Common.RequestResult result = Business.Admin.Resource.AddResource(resource);
            return Json(result,"text/html");
        }

        [HttpGet]
        public ActionResult Pop_Project_Edit(int RID)
        {
            //HPStudent.ViewModel.Projects.ProjectBook myProject = new ViewModel.Projects.ProjectBook();
            Entity.ResourceBook myResource = new Entity.ResourceBook();

            return View(myResource);
        }

        public ActionResult Detail(int RID)
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetResourceDetailByRID(int RID)
        {
            HPStudent.Entity.ResourceBook myResource= Business.Admin.Resource.GetResourceBookByRID(RID);

            return Json(myResource);
        }

        [HttpPost]
        public JsonResult GetResourceItemListByRID(int RID, int draw, int start, int length)
        {
            HPStudent.ViewModel.Common.Datatable<Entity.ResourceItem> myItemList = new ViewModel.Common.Datatable<Entity.ResourceItem>();
            myItemList.data = Business.Admin.Resource.GetResourceItemListByRID(RID);
            myItemList.draw = draw;
            myItemList.recordsFiltered = 0;
            myItemList.recordsTotal = 0;
            return Json(myItemList);
        }

        public ActionResult Pop_ProjectItem_Add()
        {
            return View();
        }
        public ActionResult Pop_ProjectItem_Edit()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddProjectItem(Entity.ProjectItem myProjectItem)
        {
            ViewModel.Common.RequestResult result = Business.Admin.Projects.AddProjectItem(myProjectItem);
            return Json(result, "text/html");
        }
    }
}
