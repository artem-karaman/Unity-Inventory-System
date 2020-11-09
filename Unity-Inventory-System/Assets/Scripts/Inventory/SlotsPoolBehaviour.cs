using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityInventorySystem.Inventory
{
	public class SlotsPoolBehaviour
	{
		private readonly SlotBehaviour.Factory _slotBehaviourFactory;
		private readonly List<SlotBehaviour> _slots = new List<SlotBehaviour>();

		private Transform _parent;
		
		public SlotsPoolBehaviour(
			SlotBehaviour.Factory slotBehaviourFactory)
		{
			_slotBehaviourFactory = slotBehaviourFactory;
		}

		public List<SlotBehaviour> SlotList => _slots;

		public void SetParent(Transform parent)
		{
			_ = parent ? parent : throw new ArgumentNullException(nameof(parent));
			_parent = parent;
		}

		public void AddSlot()
		{
			var slot = _slotBehaviourFactory.Create();
			slot.transform.SetParent(_parent, false);
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