using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDI.Data.Entities
{
    public class TeamModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Leader { get; set; }
        public string FullName { get; set; }
        public string Status { get; set; }
    }
}
