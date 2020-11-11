using System;
using UnityEngine;
using Zenject;

namespace UnityInventorySystem.Inventory
{
	public class SlotFacade : IPoolable<IMemoryPool>, IDisposable
	{
		private readonly Transform _slotTransform;
		private readonly SlotBehaviour _slotBehaviour;
		
		private IMemoryPool _memoryPool;

		public SlotFacade(
			Transform slotTransform,
			SlotBehaviour slotBehaviour)
		{
			_slotTransform = slotTransform;
			_slotBehaviour = slotBehaviour;
		}
		
		public Transform SlotTransform => _slotTransform;
		
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

		public void AddItemToSlot(ItemBehaviour item)
		{
			_slotBehaviour.AddItem(item);
		}

		public bool Empty => _slotBehaviour.Empty;

		public void SetEmpty()
		{
			_slotBehaviour.ClearStack();
		}

		public class Factory : PlaceholderFactory<SlotFacade> { }
	}
}