using System;
using System.Collections.Generic;
using HPStudent.Entity;
using HPStudent.ViewModel;
using HPStudent.Data;
using HPStudent.Core;

namespace HPStudent.Business.Admin
{
    public class Teacher
    {
        public static List<Entity.TeacherInfo> GetTeacherList(){
            List<Entity.TeacherInfo> myTeacherList = new List<TeacherInfo>();
            return Data.Admin.Teacher.GetTeacherList();
        }
    }
}
