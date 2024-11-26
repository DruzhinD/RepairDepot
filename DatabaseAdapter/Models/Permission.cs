using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable
namespace DatabaseAdapter.Models;

public class Permission : BaseModel
{
    public string Name { get; set; }

    public bool Admin { get; set; }

    public bool TechnicalDepartment { get; set; }

    public bool PlaningDepartment { get; set; }

    public bool RepairDepartment { get; set; }

    public bool StaffDepartment { get; set; }


    [Browsable(false)]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
