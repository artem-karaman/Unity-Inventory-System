using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityInventorySystem.Inventory
{
	public class SlotsPoolBehaviour : MonoBehaviour
	{
		private readonly SlotBehaviour.Factory _slotBehaviourFactory;
		private readonly List<SlotBehaviour> _slots = new List<SlotBehaviour>();

		public SlotsPoolBehaviour(
			SlotBehaviour.Factory slotBehaviourFactory)
		{
			_slotBehaviourFactory = slotBehaviourFactory;
		}

		public void AddSlot()
		{
			var slot = _slotBehaviourFactory.Create();
			_slots.Add(slot);
		}

		public void RemoveSlot()
		{
			if (_slots.Any())
			{
				var slot = _slots[0];
				slot.Dispose();
				_slots.Remove(slot);
			}
		}
	}
}