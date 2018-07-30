using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Data.Admin
{
    public class AdminRoleRelation
    {
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<HPStudent.Entity.AdminRoleRelation> GetAdminRoleRelationList(HPStudent.Entity.AdminRoleRelation KeyWords)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select MID,RoleID ");
            strSql.Append(" FROM AdminRoleRelation where 1=1");
            if (KeyWords.MID != null && KeyWords.MID != 0)
            {
                strSql.Append(string.Format(" and MID={0}", KeyWords.MID));
            }
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, strSql.ToString());
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                List<HPStudent.Entity.AdminRoleRelation> lstRes = new List<HPStudent.Entity.AdminRoleRelation>();
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    HPStudent.Entity.AdminRoleRelation objTemp = new HPStudent.Entity.AdminRoleRelation();
                    objTemp.MID = Convert.ToInt32(item["MID"]);
                    objTemp.RoleID = Convert.ToInt32(item["RoleID"]);
                    lstRes.Add(objTemp);
                }
                return lstRes;
            }
            return new List<HPStudent.Entity.AdminRoleRelation>();
        }
        //重置菜单关系表
        public static int ResetdminRoleRelation(HPStudent.ViewModel.SysManage.EditManagerInfo editManagerInfo)
        {
            StringBuilder sb = new StringBuilder();
            string sqlstr = "";
            sb.Append("BEGIN tran ");
            sb.Append(" BEGIN TRY");
            //删除关系表
            sb.Append(string.Format(" DELETE from dbo.AdminRoleRelation where mid={0};", editManagerInfo.MID));
            if (!string.IsNullOrEmpty(editManagerInfo.AdminRoleRelation))
            {
                string[] arrRole = editManagerInfo.AdminRoleRelation.TrimEnd(',').Split(',');
                if (arrRole.Length > 0)
                {
                    //重新插入关系
                    sb.Append(" INSERT INTO [dbo].[AdminRoleRelation]");
                    sb.Append(" ([MID],[RoleID])  VALUES");
                }
                for (int i = 0; i < arrRole.Length; i++)
                {
                    sqlstr += string.Format(" ({0},{1}),", editManagerInfo.MID, arrRole[i]);
                }
            }
            sqlstr = sqlstr.TrimEnd(',');
            sb.Append(sqlstr);
            sb.Append(" COMMIT Tran");
            sb.Append(" END TRY");
            sb.Append(" BEGIN catch");
            sb.Append(" ROLLBACK Tran");
            sb.Append(" end catch");
            sb.Append(" select @@error");
            int result = (int)SqlHelper.ExecuteScalar(CommandType.Text, sb.ToString());
            return result;
        }
    }
}
