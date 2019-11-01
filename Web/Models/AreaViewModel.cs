using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class CreateAreaViewModel
    {
        public string Name { get; set; }
    }

    public class GetAreaViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class EditAreaViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
       
    }
}
