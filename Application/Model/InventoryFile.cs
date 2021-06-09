using System.Collections.Generic;

namespace Application.Model
{
    public class InventoryFile
    {
        public ICollection<inventory> inventory { get; set; }
    }

    public class inventory
    {
        public string articleId { get; set; }
        public string name { get; set; }
        public int stock { get; set; }
    }
}
