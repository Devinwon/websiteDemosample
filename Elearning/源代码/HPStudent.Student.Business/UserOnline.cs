using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HPStudent.Student.Business
{
    public class UserOnline
    {
        #region 用户在线编辑数据
        /// <summary>
        /// 用户在线编辑数据
        /// </summary>
        /// <param name="userOnline"></param>
        /// <returns>新的在线时长</returns>
        public static int EditUserOnline(HPStudent.Entity.UserOnline userOnline, int OnlineTime, DateTime LastLoginTime)
        {
            //查看是否有数据
            List<HPStudent.Entity.UserOnline> liUserOnline = new List<Entity.UserOnline>();
            liUserOnline = HPStudent.Student.Data.UserOnline.UserOnlineListNotPage(userOnline);
            //在线时长统计
            int NewOnlineTime;
            TimeSpan ts = new TimeSpan();
            //如果存在修改否则添加
            if (liUserOnline.Count > 0)
            {
                HPStudent.Student.Data.UserOnline.Update(userOnline);
                ts = userOnline.LastRequestTime - LastLoginTime;
            }
            else
            {
                HPStudent.Student.Data.UserOnline.Add(userOnline);
                ts = DateTime.Now - LastLoginTime;
            }
            NewOnlineTime = OnlineTime + Convert.ToInt32(ts.TotalSeconds);
            //修改在线时长
            HPStudent.Student.Data.UserOnline.UpdateOnlineTime(userOnline.UserID, NewOnlineTime);
            return NewOnlineTime;
        }
        #endregion
    }
}
