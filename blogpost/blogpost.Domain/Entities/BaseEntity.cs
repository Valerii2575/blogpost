
namespace blogpost.Domain.Entities
{
    public class BaseEntity : IBaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
