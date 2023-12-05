using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDI.Data.Entities
{
    public class TeamMemberModel
    {

        public int Id { get; set; }
        public int TeamId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }

    }
}
