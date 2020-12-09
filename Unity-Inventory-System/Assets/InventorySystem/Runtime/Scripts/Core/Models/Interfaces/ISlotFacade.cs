using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace UnityInventorySystem
{
	public interface ISlotFacade : IDisposable
	{
		Transform Transform { get; }
		
		bool Empty { get; }
		
		bool Selected { get;}

		void SetEmpty();

		void AddItemToSlot(IItemFacade item);

		void AddItemsToSlot(IEnumerable<IItemFacade> items);

		void SetSelected(bool value);
		
		void ClearItems();
		
		IItemFacade Item { get; }
		
		IReactiveCollection<IItemFacade> AllItemsInSlot { get; }
	}
}