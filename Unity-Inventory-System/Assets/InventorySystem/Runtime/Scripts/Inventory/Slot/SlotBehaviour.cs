using System.Collections.Generic;

namespace UnityInventorySystem.Inventory
{
	public class SlotBehaviour
	{
		private readonly ItemFacadesPoolBehaviour _itemFacadesPoolBehaviour;

		public SlotBehaviour(ItemFacadesPoolBehaviour itemFacadesPoolBehaviour)
		{
			_itemFacadesPoolBehaviour = itemFacadesPoolBehaviour;
		}
		
		private readonly Stack<ItemFacade> _itemsInSlot = new Stack<ItemFacade>();
		public void AddItem(ItemFacade item) => _itemsInSlot.Push(item);
		public ItemFacade RemoveItem() => _itemsInSlot.Pop();
		public int ItemsCount => _itemsInSlot.Count;
		public bool Empty => _itemsInSlot.Count == 0;

		public void ClearStack()
		{
			foreach (var item in _itemsInSlot)
			{
				_itemFacadesPoolBehaviour.RemoveItem(item);
			}
			
			_itemsInSlot.Clear();
		}
	}
}