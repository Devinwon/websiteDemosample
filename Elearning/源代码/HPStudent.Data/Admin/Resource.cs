using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Business.Admin
{
    public class Resource
    {
        public static List<HPStudent.Entity.Major> GetMajorList()
        {
            return Data.Admin.Resource.GetMajorList();
        }

        public static HPStudent.ViewModel.Common.Datatable<Entity.ResourceBook> GetResourceBookList(int mid, int start, int length)
        {

            int TotalRows = 0;
            HPStudent.ViewModel.Common.Datatable<Entity.ResourceBook> ResourceTable = new ViewModel.Common.Datatable<Entity.ResourceBook>();


            //初始化返回Datatable行数

            ResourceTable.data = Data.Admin.Resource.GetResourceBookList(mid, start, length, out TotalRows);

            ResourceTable.recordsTotal = TotalRows;
            ResourceTable.recordsFiltered = TotalRows;
            return ResourceTable;
            //return Data.Admin.Projects.GetProjectBookList(mid, start, length);
        }

        public static ViewModel.Common.RequestResult AddResource(Entity.ResourceBook resource)
        {
            ViewModel.Common.RequestResult result = new ViewModel.Common.RequestResult();
            if (resource.ResourcePic == "")
            {
                result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                result.ResultMsg = string.Format("添加失败,图片无法上传！");
                return result;
            }
            int iResult = Data.Admin.Resource.AddResource(resource);
            switch (iResult)
            {
                case 0:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.success;
                    result.ResultMsg = string.Format("资源【{0}】添加成功", resource.ResourceName);
                    break;
                case 1:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("资源【{0}】添加失败！", resource.ResourceName);
                    break;
                default:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("出现异常错误，添加失败！");
                    break;

            }
            return result;
        }
        public static Entity.ResourceBook GetResourceBookByRID(int RID)
        {
            return Data.Admin.Resource.GetResourceBookByRID(RID);
        }

         public static List<Entity.ResourceItem> GetResourceItemListByRID (int RID)
        {
            return Data.Admin.Resource.GetResourceItemListByRID(RID);
        }
    }
}
