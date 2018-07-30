using System;
using System.Collections.Generic;
using HPStudent.Entity;
using HPStudent.Student.ViewModel;
using HPStudent.Student.Data;
using StuVModel = HPStudent.Student.ViewModel;


namespace HPStudent.Student.Business
{
    public class Webpage
    {
        public static int Register(string realname,string mobile,string email){

            return HPStudent.Student.Data.Webpage.Register(realname, mobile, email);
        }
    }
}
