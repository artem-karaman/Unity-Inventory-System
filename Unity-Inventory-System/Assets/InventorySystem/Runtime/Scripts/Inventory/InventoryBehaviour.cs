using InventorySystem.Runtime.Scripts.Core.Models.Interfaces;
using InventorySystem.Runtime.Scripts.Core.ViewModels.Inventory;
using InventorySystem.Runtime.Scripts.Inventory.Item;
using InventorySystem.Runtime.Scripts.Inventory.Slot;
using InventorySystem.Runtime.Scripts.Presenters.Base;
using UniRx;
using UnityEngine;
using Zenject;

namespace InventorySystem.Runtime.Scripts.Inventory
{
	public class InventoryBehaviour : BasePresenter, IInitializable
	{
		private readonly InventoryViewModel _inventoryViewModel;
		private readonly SlotFacadesPoolBehaviour _slotFacadesPoolBehaviour;
		private readonly ItemFacadesPoolBehaviour _itemFacadesPoolBehaviour;
		private readonly Transform _transform;
		
		public InventoryBehaviour(
			InventoryViewModel inventoryViewModel,
			SlotFacadesPoolBehaviour slotFacadesPoolBehaviour,
			ItemFacadesPoolBehaviour itemFacadesPoolBehaviour,
			Transform transform)
		{
			_inventoryViewModel = inventoryViewModel;
			_slotFacadesPoolBehaviour = slotFacadesPoolBehaviour;
			_itemFacadesPoolBehaviour = itemFacadesPoolBehaviour;
			_transform = transform;
		}

		public void Initialize()
		{
			PrepareComponents();
			SubscribeComponents();
		}

		private void PrepareComponents()
		{
			var content = _transform.GetChild(0).GetChild(0);
			_slotFacadesPoolBehaviour.SetParent(content);
		}

		private void SubscribeComponents()
		{
			_inventoryViewModel
				.SlotsCount
				.AsObservable()
				.Subscribe(AddSlots)
				.AddTo(Disposables);

			_inventoryViewModel
				.Items
				.ObserveAdd()
				.Subscribe(item =>
				{
					AddItemToSlot(item.Value);
				})
				.AddTo(Disposables);
		}

		private void AddSlots(int value)
		{
			for (var i = 0; i < value; i++)
			{
				_slotFacadesPoolBehaviour.AddSlot();
			}
		}

		private void AddItemToSlot(IItem item)
		{
			var slot = _slotFacadesPoolBehaviour.FindEmptySlot();
			
			slot.AddItemToSlot(_itemFacadesPoolBehaviour.AddItem(slot.Transform, item));
		}

		public void RemoveSelectedItem() => _inventoryViewModel.RemoveSelectedItem();
	}
}
