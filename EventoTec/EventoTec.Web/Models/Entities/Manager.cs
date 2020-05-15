using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventoTec.Web.Models.Entities
{
    public class Manager
    {
        public int Id { get; set; }
        public User User { get; set; }
    }
}
