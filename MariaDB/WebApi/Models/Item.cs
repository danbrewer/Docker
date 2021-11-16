using System;

namespace WebApi
{
    public class Item : AbstractEntity
    {
        public Item()
        {
            Id = Guid.NewGuid().ToString("N");
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}