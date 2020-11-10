using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UnityInventorySystem.Inventory
{
	public class ItemBehaviour : MonoBehaviour, IPoolable<Item, IMemoryPool>, IDisposable
	{
		private IMemoryPool _memoryPool;
		private Item _item;
		
		public void OnDespawned()
		{
			_memoryPool = null;
			_item = null;
		}

		public void OnSpawned(Item item, IMemoryPool memoryPool)
		{
			_memoryPool = memoryPool;
			_item = item;

			PrepareItem();
		}

		private void PrepareItem()
		{
			var image = gameObject.GetComponent<Image>();
			image.color = _item.Color;
		}

		public void Dispose() => _memoryPool.Despawn(this);

		public class Factory : PlaceholderFactory<Item, ItemBehaviour>{}
	}
}