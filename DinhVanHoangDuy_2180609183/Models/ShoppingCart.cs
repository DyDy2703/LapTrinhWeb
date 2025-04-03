namespace DinhVanHoangDuy_2180609183.Models
{
    public class ShoppingCart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public void AddItem(CartItem item)
        {
            var existingItem = Items.FirstOrDefault(i => i.CourseId == item.CourseId);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                Items.Add(item);
            }
        }
        public void RemoveItem(int courseId)
        {
            Items.RemoveAll(i => i.CourseId == courseId);
        }

    }
}
