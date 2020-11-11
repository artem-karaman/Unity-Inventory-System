using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core.ViewModels;
using UniRx;
using UnityEngine;
using UnityInventorySystem.Presenters.Base;
using Zenject;

namespace UnityInventorySystem.Inventory
{
	public class InventoryBehaviour : BasePresenter, IInitializable
	{
		private readonly InventoryViewModel _inventoryViewModel;
		private readonly SlotsFacadePoolBehaviour _slotsFacadePoolBehaviour;
		private readonly ItemPoolBehaviour _itemPoolBehaviour;
		private readonly Transform _transform;
		
		private List<SlotFacade> _allSlotsFacades;

		public InventoryBehaviour(
			InventoryViewModel inventoryViewModel, 
			SlotsFacadePoolBehaviour slotsFacadePoolBehaviour, 
			ItemPoolBehaviour itemPoolBehaviour,  
			Transform transform)
		{
			_inventoryViewModel = inventoryViewModel;
			_slotsFacadePoolBehaviour = slotsFacadePoolBehaviour;
			_itemPoolBehaviour = itemPoolBehaviour;
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
			_slotsFacadePoolBehaviour.SetParent(content);
		}

		private void SubscribeComponents()
		{
			_inventoryViewModel
				.SlotsCount
				.AsObservable()
				.Subscribe(AddSlots)
				.AddTo(Disposables);
		}

		private void AddSlots(int value)
		{
			for (var i = 0; i < value; i++)
			{
				_slotsFacadePoolBehaviour.AddSlot();
			}

			_allSlotsFacades = _slotsFacadePoolBehaviour.SlotList;
		}

		public void AddItem(Item item)
		{
			var slot = _allSlotsFacades.First(x => x.Empty);
			
			slot.AddItemToSlot(item);
			
			_itemPoolBehaviour.AddItem(slot.SlotTransform, item);
		}

		public void AddItems(IEnumerable<Item> items)
		{
			foreach (var item in items)
			{
				AddItem(item);
			}
		}

		public void Update()
		{
			
		}

		public void ClearItems()
		{
			var itemCount = _itemPoolBehaviour.Items.Count;
			
			for (int i = 0; i < itemCount; i++)
			{
				_itemPoolBehaviour.RemoveItem();
			}
		}
	}
}
