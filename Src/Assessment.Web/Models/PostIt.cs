using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Web.Models
{
    public class PostIt
    {
        public int Id { get; set; }

        public string PostName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string PostInfo { get; set; }
    }
}
