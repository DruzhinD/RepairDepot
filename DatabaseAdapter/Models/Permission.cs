using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable
namespace DatabaseAdapter.Models
{
    [Table("permission")]
    public class Permission : BaseModel
    {
        [Column("name")]
        public string Name { get; set; }

        [DisplayName("Регистрация")]
        [Column("register_user")]
        public bool RegisterUser { get; set; }


        [Browsable(false)]
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
