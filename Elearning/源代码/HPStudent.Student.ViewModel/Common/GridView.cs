using System;
using System.Collections.Generic;

namespace HPStudent.Student.ViewModel.Common
{
    public class GridView<T>
    {
        public int recordsTotal { get; set; }
        public List<T> data { get; set; }
    }
}
