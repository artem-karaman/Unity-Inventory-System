using System;
using UnityEngine;
using Zenject;

namespace UnityInventorySystem.Inventory
{
	public class ItemFacade : MonoBehaviour, IPoolable<IMemoryPool>, IItemFacade
	{
		private IMemoryPool _memoryPool;
		private ItemView _itemView;
		private IItem _item;

		public Transform Transform => transform;

		[Inject]
		void Construct(
			ItemView itemView)
		{
			_itemView = itemView;
		}

		public void Dispose()
		{
			_memoryPool?.Despawn(this);
		}

		public void OnDespawned()
		{
			_memoryPool = null;
		}

		public void OnSpawned(IMemoryPool p1)
		{
			_memoryPool = p1;
		}

		public void SetItem(IItem item)
		{
			_itemView.PrepareItem(item);
		}

		public class Factory : PlaceholderFactory<ItemFacade>{}
	}
}