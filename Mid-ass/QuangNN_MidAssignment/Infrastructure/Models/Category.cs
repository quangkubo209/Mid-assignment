using System.Collections.Generic;
using Infrastructure.GenericModel;

namespace Infrastructure.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
