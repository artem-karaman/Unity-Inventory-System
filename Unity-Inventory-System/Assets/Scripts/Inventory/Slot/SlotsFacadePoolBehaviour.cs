using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityInventorySystem.Inventory
{
	public class SlotsFacadePoolBehaviour
	{
		private readonly SlotFacade.Factory _slotFacadeFactory;
		
		private List<SlotFacade> _slots = new List<SlotFacade>();
		private Transform _parent;
		
		public SlotsFacadePoolBehaviour(
			SlotFacade.Factory slotFacadeFactory)
		{
			_slotFacadeFactory = slotFacadeFactory;
		}

		public List<SlotFacade> SlotList => _slots;

		public void SetParent(Transform parent)
		{
			_ = parent ? parent : throw new ArgumentNullException(nameof(parent));
			_parent = parent;
		}

		public void AddSlot()
		{
			var slot = _slotFacadeFactory.Create();
			slot.SlotTransform.SetParent(_parent);
			_slots.Add(slot);
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