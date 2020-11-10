using System.Collections.Generic;

namespace UnityInventorySystem.Inventory
{
	public class SlotBehaviour
	{
		private readonly Stack<Item> _itemsInSlot = new Stack<Item>();
		public void AddItem(Item item) => _itemsInSlot.Push(item);
		public Item RemoveItem() => _itemsInSlot.Pop();
		public int ItemsCount => _itemsInSlot.Count;
		public bool Empty => _itemsInSlot.Count == 0;
	}
}