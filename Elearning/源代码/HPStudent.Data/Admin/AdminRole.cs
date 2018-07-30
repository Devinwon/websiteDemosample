using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Business.Admin
{
    public class AdminRole
    {
        //获得管理员角色列表（分页）
        public static HPStudent.ViewModel.Common.Datatable<HPStudent.Entity.AdminRole> GetAdminRoleList(int start, int length, HPStudent.Entity.AdminRole KeyWords)
        {
            int TotalRows = 0;
            HPStudent.ViewModel.Common.Datatable<HPStudent.Entity.AdminRole> ProjectTable = new ViewModel.Common.Datatable<HPStudent.Entity.AdminRole>();
            //初始化返回Datatable行数
            ProjectTable.data = Data.Admin.AdminRole.GetAdminRoleList(start, length, out TotalRows, KeyWords);
            ProjectTable.recordsTotal = TotalRows;
            ProjectTable.recordsFiltered = TotalRows;
            return ProjectTable;
        }
        //得到单个管理员角色实体
        public static HPStudent.Entity.AdminRole GetAdminRoleByID(int id)
        {
            return HPStudent.Data.Admin.AdminRole.GetAdminRoleByID(id);
        }
        //管理员角色添加事件
        public static HPStudent.ViewModel.Common.RequestResult AdminRoleAdd(HPStudent.Entity.AdminRole ur)
        {
            ViewModel.Common.RequestResult result = new ViewModel.Common.RequestResult();
            //获得最大序号和ID
            HPStudent.Entity.AdminRole maxAdminRole = HPStudent.Data.Admin.AdminRole.GetMaxIDAndSort();
            ur.RID = maxAdminRole.RID + 1;
            ur.SortCode = maxAdminRole.SortCode + 1;
            int iResult = HPStudent.Data.Admin.AdminRole.AddAdminRoleItem(ur);
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
        //管理员角色修改事件
        public static HPStudent.ViewModel.Common.RequestResult AdminRoleUpdate(HPStudent.Entity.AdminRole ur)
        {
            ViewModel.Common.RequestResult result = new ViewModel.Common.RequestResult();
            int iResult = HPStudent.Data.Admin.AdminRole.UpdateAdminRoleItem(ur);
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
        //通过RID获得菜单权限
        public static HPStudent.Entity.AdminRole_NavPowrer GetAdminRoleNavPowrerByID(int id)
        {
            return HPStudent.Data.Admin.AdminRole_NavPowrer.GetAdminRoleNavPowrerByID(id);
        }
        //管理员导航权限编辑事件
        public static HPStudent.ViewModel.Common.RequestResult AdminMenuRoleEdit(HPStudent.Entity.AdminRole_NavPowrer roleNavPowrer)
        {
            int iResult = 0;
            ViewModel.Common.RequestResult result = new ViewModel.Common.RequestResult();
            //保存前先验证是否存在，若存在修改，不存在新增
            HPStudent.Entity.AdminRole_NavPowrer adminrole = HPStudent.Data.Admin.AdminRole_NavPowrer.GetAdminRoleNavPowrerByID(roleNavPowrer.RID);
            if (adminrole.RID != 0)
            {
                iResult = HPStudent.Data.Admin.AdminRole_NavPowrer.UpdateAdminRoleNavPowrerItem(roleNavPowrer);
            }
            else
            {
                iResult = HPStudent.Data.Admin.AdminRole_NavPowrer.AddAdminRoleNavPowrerItem(roleNavPowrer);
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
        //获得管理员角色列表（非分页）
        public static List<HPStudent.Entity.AdminRole> GetAdminRoleListNotPage()
        {
            return HPStudent.Data.Admin.AdminRole.GetAdminRoleListNotPage();
        }
        //获得权限关系
        public static List<HPStudent.Entity.AdminRoleRelation> GetAdminRoleRelationList(HPStudent.Entity.AdminRoleRelation KeyWords)
        {
            return HPStudent.Data.Admin.AdminRoleRelation.GetAdminRoleRelationList(KeyWords);
        }
        //重置菜单关系表
        public static int ResetdminRoleRelation(HPStudent.ViewModel.SysManage.EditManagerInfo editManagerInfo)
        {
            return HPStudent.Data.Admin.AdminRoleRelation.ResetdminRoleRelation(editManagerInfo);
        }
    }
}
