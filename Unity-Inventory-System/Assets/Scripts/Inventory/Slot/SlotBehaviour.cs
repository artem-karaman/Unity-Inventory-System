using System.Collections.Generic;
using Zenject;

namespace UnityInventorySystem.Inventory
{
	public class SlotBehaviour : IInitializable
	{
		private readonly Stack<Item> _itemsInSlot = new Stack<Item>();
		public void AddItem(Item item) => _itemsInSlot.Push(item);
		public Item RemoveItem() => _itemsInSlot.Pop();
		public int ItemsCount => _itemsInSlot.Count;
		public bool Empty => _itemsInSlot.Count == 0;

		public void Initialize()
		{
			
		}
	}
}