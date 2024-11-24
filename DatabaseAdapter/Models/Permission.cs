using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable
namespace DatabaseAdapter.Models
{
    [Table("permission")]
    public class Permission
    {
        //[ReadOnly(true)]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }

        [Column("register_user")]
        public bool RegisterUser { get; set; }

        //[ReadOnly(true)]
        //[Browsable(false)]
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
