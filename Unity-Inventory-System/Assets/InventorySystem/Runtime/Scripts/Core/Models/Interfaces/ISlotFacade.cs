using System;
using UnityEngine;

namespace UnityInventorySystem
{
	public interface ISlotFacade : IDisposable
	{
		Transform Transform { get; }
		
		bool Empty { get; }

		void SetEmpty();

		void AddItemToSlot(IItemFacade item);

		void SetSelected(bool value);
		
		void RemoveItem();
	}
}