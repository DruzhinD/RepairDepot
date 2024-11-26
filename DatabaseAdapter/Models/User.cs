using System.ComponentModel.DataAnnotations.Schema;
#nullable disable

namespace DatabaseAdapter.Models;

/// <summary>
/// пользователи приложения
/// </summary>
public partial class User : BaseModel
{
    public string Login { get; set; }

    public string Password { get; set; }

    public int PermissionId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string MiddleName { get; set; }

    public virtual Permission Permission { get; set; }
}
