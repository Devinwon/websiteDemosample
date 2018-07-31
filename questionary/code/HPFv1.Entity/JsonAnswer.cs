using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPFv1.Entity
{
   public class JsonAnswer
    {
       public string Number { get; set; }

       public string Type {get;set;}


       public string Title { get; set; }


       public List<SelAnswer> SelAnswer { get; set; }

       public List<TextAnswer> TxtAnswer { get; set; }
    }

    public class SelAnswer
    {
        public string SelectItem { get; set; }

        public decimal SelectCount { get; set; }

        public string SelectContent { get; set; }
    
    
    }


    public class TextAnswer 
    {
        public string Content { get; set; }
    
    }

}
