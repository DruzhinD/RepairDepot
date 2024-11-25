using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseAdapter.Models
{
    /// <summary>
    /// хранит свойства, общие для всех таблиц в базе данных
    /// </summary>
    public abstract class BaseModel
    {
        
        [Column("id")]
        [ReadOnly(true)]
        [DisplayName("ID")]
        public int Id { get; set; }
    }
}
