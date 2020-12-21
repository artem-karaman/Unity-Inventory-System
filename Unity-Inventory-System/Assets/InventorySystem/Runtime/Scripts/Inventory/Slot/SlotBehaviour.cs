using System.Collections.Generic;
using InventorySystem.Runtime.Scripts.Core.Models.Interfaces;
using InventorySystem.Runtime.Scripts.Core.ViewModels.Inventory;
using InventorySystem.Runtime.Scripts.Presenters.Base;
using UniRx;

namespace InventorySystem.Runtime.Scripts.Inventory.Slot
{
	public class SlotBehaviour : BasePresenter
	{
		private readonly SlotViewModel _slotViewModel;

		public SlotBehaviour(
			SlotViewModel slotViewModel)
		{
			_slotViewModel = slotViewModel;
		}
		public void AddItem(IItemFacade item) => _slotViewModel.AddItem(item);
		public void ClearItemsInSlot() => _slotViewModel.ClearItemsInSlot();
		public bool Empty => _slotViewModel.Empty.Value;
		public bool Selected => _slotViewModel.Selected.Value;
		public IReactiveCollection<IItemFacade> GetAllItemsInSlot => _slotViewModel.ItemsInSlot;
		public void SetSelected(bool value) => _slotViewModel.SetSelected(value);
		public void ClearStack() => _slotViewModel.ClearItems();
		public void AddItems(IEnumerable<IItemFacade> items) => _slotViewModel.AddItems(items);
	}
}