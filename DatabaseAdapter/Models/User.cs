using System.ComponentModel.DataAnnotations.Schema;
#nullable disable

namespace DatabaseAdapter.Models
{
    [Table("user")]
    public class User
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("login")]
        public string Login { get; set; }
        [Column("password")]
        public string Password { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }
        [Column("last_name")]
        public string LastName { get; set; }
        [Column("middle_name")]
        public string MiddleName { get; set; }

        [Column("permission_id")]
        public int PermissionId {  get; set; }

        public virtual Permission Permission { get; set; }
    }
}
