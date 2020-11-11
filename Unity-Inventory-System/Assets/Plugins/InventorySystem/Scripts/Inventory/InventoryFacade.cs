using System.Collections.Generic;
using Zenject;

namespace UnityInventorySystem.Inventory
{
	public class InventoryFacade
	{
		private readonly InventoryBehaviour _inventory;
		public InventoryFacade(InventoryBehaviour inventory)
		{
			_inventory = inventory;
		}

		public void AddItem(IItem item) => _inventory.AddItem(item);
		public void AddItems(IEnumerable<IItem> items) => _inventory.AddItems(items);
		public void FilterItems<T>() 
			where T : IItem 
			=> _inventory.FilterItems<T>();

		public class Factory : PlaceholderFactory<int, InventoryFacade>{}
	}
}