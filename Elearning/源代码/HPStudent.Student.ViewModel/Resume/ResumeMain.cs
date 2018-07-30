using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.ViewModel.Resume
{
  public  class ResumeMain
    {
      public ResumeMain()
      {
          this.ResumeBasic = new ResumeBasic();
          this.EducationRecord = new List<Entity.SchoolDetail>();
          this.WorkExperience = new List<Entity.StudentWorkExp>();
          this.ProjectExperience = new List<Entity.StudentProjectExp>();
      }
      public int MID;

      public  HPStudent.Student.ViewModel.Resume.ResumeBasic ResumeBasic;
      public List<HPStudent.Entity.SchoolDetail> EducationRecord;
      public List<HPStudent.Entity.StudentWorkExp> WorkExperience;
      public List<HPStudent.Entity.StudentProjectExp> ProjectExperience;
      }
}
