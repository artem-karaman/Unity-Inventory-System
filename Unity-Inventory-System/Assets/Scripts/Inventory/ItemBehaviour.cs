using System;
using UnityEngine;
using Zenject;

namespace UnityInventorySystem.Inventory
{
	public class ItemBehaviour : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable
	{
		private IMemoryPool _memoryPool;

		public void OnDespawned() => _memoryPool = null;

		public void OnSpawned(IMemoryPool memoryPool) => _memoryPool = memoryPool;

		public void Dispose() => _memoryPool.Despawn(this);

		public class Factory : PlaceholderFactory<ItemBehaviour>{}
	}
}