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

		public void AddItem(Item item) => _inventory.AddItem(item);
		public void AddItems(IEnumerable<Item> items) => _inventory.AddItems(items);
		public void UpdateInventory() => _inventory.Update();
		public void ClearItems() => _inventory.ClearItems();

		public class Factory : PlaceholderFactory<int, InventoryFacade>{}
	}
}