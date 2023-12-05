using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDI.Data.Entities
{
    public class CountryModel
    {
        public int Id { get; set; }
        [Required]
        //[Required(ErrorMessage = "First id is required")]
        public string Name { get; set; }
        //[Required(ErrorMessage = "First id is required")]

        public bool Active { get; set; }
        //[Required(ErrorMessage = "First id is required")]

        public string Sapb1db { get; set; }
        //[Required(ErrorMessage = "First id is required")]

        public string HREmail { get; set; }

    }
}
