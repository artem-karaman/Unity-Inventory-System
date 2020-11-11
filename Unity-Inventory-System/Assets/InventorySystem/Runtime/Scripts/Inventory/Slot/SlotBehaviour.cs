using System.Collections.Generic;

namespace UnityInventorySystem.Inventory
{
	public class SlotBehaviour
	{
		private readonly ItemPoolBehaviour _itemPoolBehaviour;

		public SlotBehaviour(ItemPoolBehaviour itemPoolBehaviour)
		{
			_itemPoolBehaviour = itemPoolBehaviour;
		}
		
		private readonly Stack<ItemBehaviour> _itemsInSlot = new Stack<ItemBehaviour>();
		public void AddItem(ItemBehaviour item) => _itemsInSlot.Push(item);
		public ItemBehaviour RemoveItem() => _itemsInSlot.Pop();
		public int ItemsCount => _itemsInSlot.Count;
		public bool Empty => _itemsInSlot.Count == 0;

		public void ClearStack()
		{
			foreach (var item in _itemsInSlot)
			{
				_itemPoolBehaviour.RemoveItem(item);
			}
			
			_itemsInSlot.Clear();
		}
	}
}