using System.Collections.Generic;
using InventorySystem.Runtime.Scripts.Core.Models.Interfaces;
using InventorySystem.Runtime.Scripts.Core.ViewModels.Inventory;
using Zenject;

namespace UnityInventorySystem.Inventory
{
	public class InventoryFacade
	{
		private readonly InventoryBehaviour _inventoryBehaviour;
		private readonly InventoryViewModel _inventoryViewModel;
		public InventoryFacade(InventoryBehaviour inventoryBehaviour)
		{
			_inventoryBehaviour = inventoryBehaviour;
		}

		public void AddItem(IItem item)
		{
			_inventoryBehaviour.AddItem(item);
		}

		public void AddItems(IEnumerable<IItem> items)
		{
			_inventoryBehaviour.AddItems(items);
		}

		public void FilterItems<T>() where T : IItem => _inventoryBehaviour.FilterItems<T>();
		public void RemoveSelectedItem() => _inventoryBehaviour.RemoveSelectedItem();
		public class Factory : PlaceholderFactory<int, InventoryFacade>{}
	}
}