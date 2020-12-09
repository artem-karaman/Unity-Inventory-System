using System.Collections.Generic;
using InventorySystem.Runtime.Scripts.Core.ViewModels.Inventory;
using UniRx;
using UnityInventorySystem;
using UnityInventorySystem.Inventory;
using UnityInventorySystem.Presenters.Base;
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

		public void Initialize()
		{
			_slotViewModel
				.ItemsInSlot
				.ObserveRemove()
				.Subscribe(value =>
				{
					_itemFacadesPoolBehaviour.RemoveItem(value.Value);
				})
				.AddTo(Disposables);
		}

		public void AddItem(IItemFacade item) => _slotViewModel.AddItem(item);

		public void RemoveItem() => _slotViewModel.RemoveItem();

		public int ItemsCount => _slotViewModel.ItemsCount.Value;

		public bool Empty => _slotViewModel.Empty;
		
		public bool Selected => _slotViewModel.Selected.Value;
		public IReactiveCollection<IItemFacade> GetAllItemsInSlot => _slotViewModel.ItemsInSlot;

		public void SetSelected(bool value) => _slotViewModel.SetSelected(value);

		public void ClearStack()
		{
			foreach (var item in _slotViewModel.ItemsInSlot)
			{
				_itemFacadesPoolBehaviour.RemoveItem(item);
			}

			_slotViewModel.ClearItems();
		}

		public void AddItems(IEnumerable<IItemFacade> items)
		{
			_slotViewModel.AddItems(items);
		}
	}
}