using System;
using UnityEngine;
using Zenject;

namespace UnityInventorySystem.Inventory
{
	public class SlotBehaviour : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable
	{
		private IMemoryPool _memoryPool;
		
		public void OnDespawned()
		{
			
		}

		public void OnSpawned(IMemoryPool memoryPool)
		{
			_memoryPool = memoryPool;
		}

		public void Dispose()
		{
			_memoryPool.Despawn(this);
		}

		public class Factory : PlaceholderFactory<SlotBehaviour>{}
	}
}