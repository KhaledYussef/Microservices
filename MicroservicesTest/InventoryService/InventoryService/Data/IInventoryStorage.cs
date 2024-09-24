
namespace InventoryService
{
    public interface IInventoryStorage
    {
        void AddItem(Item item);
        List<Item> GetAllItems();
        Item GetItemById(int id);
        void UpdateItemQuantity(int id, int quantity);
    }
}