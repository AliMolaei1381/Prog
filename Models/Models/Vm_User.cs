using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prog.Models.Models
{
    public class Vm_User 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public int Age { get; set; }
        public string Password { get; set; }
        public string Re_Password { get; set; }
        public string Avatar { get; set; }
        public IFormFile Img { get; set; }
        public bool Status { get; set; }
    }
}