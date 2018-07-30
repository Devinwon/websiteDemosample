using System;
using HPStudent.Entity;
using HPStudent.ViewModel;
using HPStudent.Data;
using System.Collections.Generic;
using HPStudent.Core;

namespace HPStudent.Business.Admin
{
    public class SysManage
    {
        /// <summary>
        /// 查询所有的管理员信息
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static ViewModel.Common.Datatable<ViewModel.SysManage.ManagerTable> GetManagerDatatable(int start, int length)
        {
            int TotalRows = 0;
            List<ManagerInfo> managerList = Data.Admin.SysManage.GetManagerList(start, length,out TotalRows);
            ViewModel.Common.Datatable<ViewModel.SysManage.ManagerTable> ManagerDatetable = new ViewModel.Common.Datatable<ViewModel.SysManage.ManagerTable>();

            ManagerDatetable.data = new List<ViewModel.SysManage.ManagerTable>();

            //初始化返回Datatable行数
            ManagerDatetable.recordsTotal = TotalRows;
            ManagerDatetable.recordsFiltered = TotalRows;

            foreach (ManagerInfo item in managerList)
            {
                ViewModel.SysManage.ManagerTable table = new ViewModel.SysManage.ManagerTable();
                table.MID = item.MID;
                table.ManagerName = item.ManagerName;
                table.Level = item.Level == 0 ? "超级管理员":"普通管理员";
                table.LastLoginTime = item.LastLoginTime.ToString();
                table.Operation = "<button class=\"btn btn-primary btn-sm\" data-id=\"" + item.MID + "\" data-action=\"edit\">"
                    +"<span class=\"fa fa-pencil\"></span>编辑</button> "
                    + "<button class=\"btn btn-primary btn-sm disabled\" data-id=\"" + item.MID + "\" data-action=\"delete\">"
                    +"<span class=\"fa fa-times\"></span>删除</button>";

                ManagerDatetable.data.Add(table);
            }


            return ManagerDatetable;


        }

        /// <summary>
        /// 添加管理员信息
        /// </summary>
        /// <param name="manager">管理员信息</param>
        /// <returns>
        /// 0:添加成功
        /// 1：管理员名称已存在，添加失败
        /// </returns>
        public static ViewModel.Common.RequestResult AddManager(ManagerInfo manager)
        {
            int iResult = Data.Admin.SysManage.AddManager(manager);
            ViewModel.Common.RequestResult result = new ViewModel.Common.RequestResult();

            switch(iResult)
            {
                case 0:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.success;
                    result.ResultMsg = string.Format("管理员【{0}】添加成功", manager.ManagerName);
                    break;
                case 1:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("管理员名称【{0}】已存在，添加失败！", manager.ManagerName);
                    break;
                default:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("出现异常错误，添加失败！");
                    break;

            }

            return result;
        }

        public static ViewModel.Common.RequestResult EditManager(ManagerInfo manager)
        {
            int iResult = Data.Admin.SysManage.EditManager(manager);
            ViewModel.Common.RequestResult result = new ViewModel.Common.RequestResult();

            switch (iResult)
            {
                case 1:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.success;
                    result.ResultMsg = string.Format("管理员【{0}】修改成功", manager.ManagerName);
                    break;
                case 0:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("修改失败，没有找到【{0}】的信息！", manager.ManagerName);
                    break;
                case -1:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("修改失败，这是最后一个超级管理员帐号，不允许删除！", manager.ManagerName);
                    break;
                default:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("出现异常错误，添加失败！");
                    break;

            }

            return result;
        }

        public static ManagerInfo GetManagerByMid(int MID)
        {
            ManagerInfo manager = new ManagerInfo();
            manager = Data.Admin.SysManage.GetManagerByMid(MID);
            return manager;
        }

        public static ViewModel.Common.RequestResult DelManager(int MID)
        {
            int iResult = Data.Admin.SysManage.DelManager(MID);
            ViewModel.Common.RequestResult result = new ViewModel.Common.RequestResult();

            switch (iResult)
            {
                case 0:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("删除失败，可能信息已被删除！", MID);
                    break;
                case 1:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.success;
                    result.ResultMsg = string.Format("编号为【{0}】的管理员信息删除成功！", MID);
                    break;
                case -1:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("删除失败,此帐号是最后一个超级管理员帐号！", MID);
                    break;
                default:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("出现异常错误，删除失败！");
                    break;

            }

            return result;
        }

    }
}
