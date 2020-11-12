using System;
using UnityEngine;
using Zenject;

namespace UnityInventorySystem.Inventory
{
	public class SlotFacade : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable
	{
		private SlotBehaviour _slotBehaviour;
		private IMemoryPool _memoryPool;

		[Inject]
		void Construct(
			SlotBehaviour slotBehaviour)
		{
			_slotBehaviour = slotBehaviour;
		}

		public void Dispose()
		{
			_memoryPool?.Despawn(this);
		}

		public void OnDespawned()
		{
			_memoryPool = null;
		}

		public void OnSpawned(IMemoryPool memoryPool)
		{
			_memoryPool = memoryPool;
		}

		public void AddItemToSlot(ItemFacade item)
		{
			_slotBehaviour.AddItem(item);
		}

		public bool Empty => _slotBehaviour.Empty;

		public void SetEmpty()
		{
			_slotBehaviour.ClearStack();
		}

		public int ItemsCount()
		{
			return _slotBehaviour.ItemsCount;
		}

		public class Factory : PlaceholderFactory<SlotFacade> { }
	}
}