using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KakarazaZoohop.Model
{
    public class Owner : BaseEntity
    {
        public string Fullname { get; set; }

        public List<Patient> Animals { get; set; }
    }
}
