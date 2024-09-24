using System.Text.Json;

namespace InventoryService
{
    public class InventoryStorage : IInventoryStorage
    {
        private readonly List<Item> Items;
        public InventoryStorage()
        {
            Items =
            [
                new Item { Id = 1, Name = "Item1", Quantity = 10 },
                new Item { Id = 2, Name = "Item2", Quantity = 20 },
                new Item { Id = 3, Name = "Item3", Quantity = 30 }
            ];
        }

        public Item GetItemById(int id)
        {
            return Items.FirstOrDefault(x => x.Id == id);
        }

        public void UpdateItemQuantity(int id, int quantity)
        {
            var item = Items.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                item.Quantity -= quantity;
            }
        }

        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        // get all items
        public List<Item> GetAllItems()
        {
            return Items;
        }
    }
}

public class Item
{
    public Item(int id, string name, int quantity)
    {
        Id = id;
        Name = name;
        Quantity = quantity;
    }

    public Item()
    {
        
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }

    // to json
    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }

    // from json
    public static Item FromJson(string json)
    {
        return JsonSerializer.Deserialize<Item>(json);
    }



}
