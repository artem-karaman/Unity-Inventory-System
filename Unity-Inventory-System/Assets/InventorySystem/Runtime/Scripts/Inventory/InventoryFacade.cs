using System.Collections.Generic;
using InventorySystem.Runtime.Scripts.Core.Models.Interfaces;
using InventorySystem.Runtime.Scripts.Core.ViewModels.Inventory;
using Zenject;

namespace UnityInventorySystem.Inventory
{
	public class InventoryFacade
	{
		private readonly InventoryViewModel _inventoryViewModel;
		public InventoryFacade(InventoryViewModel inventoryViewModel) => _inventoryViewModel = inventoryViewModel;
		public void AddItem(IItem item) => _inventoryViewModel.AddItem(item);
		public void AddItems(IEnumerable<IItem> items) => _inventoryViewModel.AddItems(items);
		public void FilterItems<T>() where T : IItem => _inventoryViewModel.FilterItems<T>();
		public void RemoveSelectedItem() => _inventoryViewModel.RemoveSelectedItem();
		public class Factory : PlaceholderFactory<int, InventoryFacade>{}
	}
}