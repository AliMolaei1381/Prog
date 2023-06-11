using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prog.Models.Entities
{
    public class Tbl_User
    {
       public int Id { get; set; } 
       public string Name { get; set; }
       public string Family { get; set; }
       public int Age { get; set; }
       public string  Password { get; set; }
       public string Avatar { get; set; }
       public bool Status { get; set; }
    }
}