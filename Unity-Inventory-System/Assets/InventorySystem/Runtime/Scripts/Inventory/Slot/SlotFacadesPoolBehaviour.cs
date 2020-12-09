using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core.ViewModels;
using InventorySystem.Runtime.Scripts.Core.ViewModels.Inventory;
using InventorySystem.Runtime.Scripts.Inventory.Slot;
using UnityEngine;

namespace UnityInventorySystem.Inventory
{
	public class SlotFacadesPoolBehaviour
	{
		private readonly SlotFacade.Factory _slotFacadeFactory;
		private readonly InventoryViewModel _inventoryViewModel;
		private List<ISlotFacade> _slots = new List<ISlotFacade>();
		private Transform _parent;
		
		public SlotFacadesPoolBehaviour(
			SlotFacade.Factory slotFacadeFactory,
			InventoryViewModel inventoryViewModel)
		{
			_slotFacadeFactory = slotFacadeFactory;
			_inventoryViewModel = inventoryViewModel;
		}

		public void SetParent(Transform parent)
		{
			_ = parent ? parent : throw new ArgumentNullException(nameof(parent));
			_parent = parent;
		}

		public void AddSlot()
		{
			var slot = _slotFacadeFactory.Create();
			_inventoryViewModel.InventoryItems.Add(slot);
			slot.Transform.SetParent(_parent);
			_slots.Add(slot);
		}

		public ISlotFacade FindEmptySlot()
		{
			return _slots.First(s => s.Empty);
		}

		public void ClearSlots()
		{
			_slots.ForEach(s => s.SetEmpty());
		}

		public void RemoveSlot()
		{
			if (!_slots.Any()) return;
			var slot = _slots[0];
			slot.Dispose();
			_slots.Remove(slot);
		}
	}
}