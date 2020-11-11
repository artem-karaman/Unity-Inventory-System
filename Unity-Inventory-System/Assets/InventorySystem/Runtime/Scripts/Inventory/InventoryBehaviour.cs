using System;
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

		private List<IItem> _savedOrigianlItemsList = new List<IItem>();

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
		}

		public void AddItem(IItem item, bool saveItem = false)
		{
			AddItem(item);
			
			_savedOrigianlItemsList.Add(item);
		}

		public void AddItems(IEnumerable<IItem> items)
		{
			_ = items ?? throw new ArgumentNullException(nameof(items));
			
			foreach (var item in items)
			{
				AddItem(item);
			}
		}

		public void Update()
		{
			
		}

		public void ClearItems() => _slotsFacadePoolBehaviour.ClearSlots();

		public void FilterItems<T>() 
			where T : IItem
		{
			ClearItems();

			var list = _savedOrigianlItemsList.Where(i => i is T).ToList();

			if (!list.Any()) return;
			
			AddItems(list);
		}

		private void AddItem(IItem item)
		{
			var slot = _slotsFacadePoolBehaviour.FindEmptySlot();
			
			slot.AddItemToSlot(_itemPoolBehaviour.AddItem(slot.SlotTransform, item));
		}
	}
}
