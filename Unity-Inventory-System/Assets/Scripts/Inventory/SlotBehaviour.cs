using System;
using UnityEngine;
using Zenject;

namespace UnityInventorySystem.Inventory
{
	public class SlotBehaviour : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable
	{
		private IMemoryPool _memoryPool;

		#region MemoryPoolMethods
		public void OnDespawned() => _memoryPool = null;
		public void OnSpawned(IMemoryPool memoryPool) => _memoryPool = memoryPool;
		public void Dispose() => _memoryPool.Despawn(this);

		#endregion

		public class Factory : PlaceholderFactory<SlotBehaviour>{}
	}
}