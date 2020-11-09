using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace UnityInventorySystem.Inventory
{
	public class SlotBehaviour : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable
	{
		private IMemoryPool _memoryPool;

		private readonly Stack<Item> _itemsInSlot = new Stack<Item>();
		public void AddItem(Item item) => _itemsInSlot.Push(item);
		public Item RemoveItem() => _itemsInSlot.Pop();
		public int ItemsCount => _itemsInSlot.Count;
		public bool Empty => _itemsInSlot.Count == 0;

		#region MemoryPoolMethods
		public void OnDespawned() => _memoryPool = null;
		public void OnSpawned(IMemoryPool memoryPool) => _memoryPool = memoryPool;
		public void Dispose() => _memoryPool.Despawn(this);

		#endregion

		public class Factory : PlaceholderFactory<SlotBehaviour>{}
	}
}