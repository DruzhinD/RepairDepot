using System.ComponentModel.DataAnnotations.Schema;
#nullable disable

namespace DatabaseAdapter.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
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
