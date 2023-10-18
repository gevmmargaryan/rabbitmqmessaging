using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hash.DAL.Entities
{
    public class Hash : BaseEntity
    {

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string Sha1 { get; set; }

    }
}
