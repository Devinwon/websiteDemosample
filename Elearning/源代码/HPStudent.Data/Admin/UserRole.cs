using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Business.Admin
{
    public class UserRole
    {
        //获得用户角色列表
        public static HPStudent.ViewModel.Common.Datatable<HPStudent.Entity.UserRole> GetUserRoleList(int start, int length, HPStudent.Entity.UserRole KeyWords)
        {
            int TotalRows = 0;
            HPStudent.ViewModel.Common.Datatable<HPStudent.Entity.UserRole> ProjectTable = new ViewModel.Common.Datatable<HPStudent.Entity.UserRole>();
            //初始化返回Datatable行数
            ProjectTable.data = Data.Admin.UserRole.GetUserRoleList(start, length, out TotalRows, KeyWords);
            ProjectTable.recordsTotal = TotalRows;
            ProjectTable.recordsFiltered = TotalRows;
            return ProjectTable;
        }
        //用户角色添加事件
        public static HPStudent.ViewModel.Common.RequestResult UserRoleAdd(HPStudent.Entity.UserRole ur)
        {
            ViewModel.Common.RequestResult result = new ViewModel.Common.RequestResult();
            //获得最大序号和ID
            HPStudent.Entity.UserRole maxUserRole = HPStudent.Data.Admin.UserRole.GetMaxIDAndSort();
            ur.RID = maxUserRole.RID + 1;
            ur.SortCode = maxUserRole.SortCode + 1;
            int iResult = HPStudent.Data.Admin.UserRole.AddUserRoleItem(ur);
            switch (iResult)
            {
                case 0:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.success;
                    result.ResultMsg = string.Format("添加成功！");
                    break;
                default:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("出现异常错误，添加操作处理失败！");
                    break;
            }
            return result;
        }
        //用户角色修改事件
        public static HPStudent.ViewModel.Common.RequestResult UserRoleUpdate(HPStudent.Entity.UserRole ur)
        {
            ViewModel.Common.RequestResult result = new ViewModel.Common.RequestResult();
            int iResult = HPStudent.Data.Admin.UserRole.UpdateUserRoleItem(ur);
            switch (iResult)
            {
                case 0:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.success;
                    result.ResultMsg = string.Format("修改成功！");
                    break;
                default:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("出现异常错误，添加操作处理失败！");
                    break;
            }
            return result;
        }
        //得到单个用户角色实体
        public static HPStudent.Entity.UserRole GetUserRoleByID(int id)
        {
            return HPStudent.Data.Admin.UserRole.GetUserRoleByID(id);
        }
        //通过RID获得菜单权限
        public static HPStudent.Entity.UserRole_NavPowrer GetUserRoleNavPowrerByID(int id)
        {
            return HPStudent.Data.Admin.UserRole_NavPowrer.GetUserRoleNavPowrerByID(id);
        }
        //用户导航权限编辑事件
        public static HPStudent.ViewModel.Common.RequestResult UserMenuRoleEdit(HPStudent.Entity.UserRole_NavPowrer roleNavPowrer)
        {
            int iResult = 0;
            ViewModel.Common.RequestResult result = new ViewModel.Common.RequestResult();
            //保存前先验证是否存在，若存在修改，不存在新增
            HPStudent.Entity.UserRole_NavPowrer userrole = HPStudent.Data.Admin.UserRole_NavPowrer.GetUserRoleNavPowrerByID(roleNavPowrer.RID);
            if (userrole.RID != 0)
            {
                iResult = HPStudent.Data.Admin.UserRole_NavPowrer.UpdateUserRoleNavPowrerItem(roleNavPowrer);
            }
            else
            {
                iResult = HPStudent.Data.Admin.UserRole_NavPowrer.AddUserRoleNavPowrerItem(roleNavPowrer);
            }
            switch (iResult)
            {
                case 0:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.success;
                    result.ResultMsg = string.Format("保存成功！");
                    break;
                default:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("出现异常错误，添加操作处理失败！");
                    break;
            }
            return result;
        }
        //获得权限列表非分页
        public static List<HPStudent.Entity.UserRole> GetUserRoleListNotPage()
        {
            return HPStudent.Data.Admin.UserRole.GetUserRoleListNotPage();
        }
    }
}
