using System.Collections.Generic;
using InventorySystem.Runtime.Scripts.Core.Models.Interfaces;
using InventorySystem.Runtime.Scripts.Core.ViewModels.Inventory;
using InventorySystem.Runtime.Scripts.Inventory.Item;
using InventorySystem.Runtime.Scripts.Presenters.Base;
using UniRx;
using Zenject;

namespace InventorySystem.Runtime.Scripts.Inventory.Slot
{
	public class SlotBehaviour : BasePresenter, IInitializable
	{
		private readonly ItemFacadesPoolBehaviour _itemFacadesPoolBehaviour;
		private readonly SlotViewModel _slotViewModel;

		public SlotBehaviour(
			ItemFacadesPoolBehaviour itemFacadesPoolBehaviour,
			SlotViewModel slotViewModel)
		{
			_itemFacadesPoolBehaviour = itemFacadesPoolBehaviour;
			_slotViewModel = slotViewModel;
		}
		public void Initialize() { }

		public void AddItem(IItemFacade item) => _slotViewModel.AddItem(item);
		public void ClearItemsInSlot() => _slotViewModel.ClearItemsInSlot();
		public bool Empty => _slotViewModel.Empty;
		public bool Selected => _slotViewModel.Selected.Value;
		public IReactiveCollection<IItemFacade> GetAllItemsInSlot => _slotViewModel.ItemsInSlot;
		public void SetSelected(bool value) => _slotViewModel.SetSelected(value);
		public void ClearStack() => _slotViewModel.ClearItems();
		public void AddItems(IEnumerable<IItemFacade> items) => _slotViewModel.AddItems(items);
	}
}