using Assets.Scripts.Core.ViewModels;
using UnityEngine;
using Zenject;

namespace UnityInventorySystem.Inventory
{
	public class SlotFacade : MonoBehaviour, IPoolable<IMemoryPool>, ISlotFacade
	{
		private SlotBehaviour _slotBehaviour;
		private SlotViewModel _slotViewModel;
		private IMemoryPool _memoryPool;

		[Inject]
		void Construct(
			SlotBehaviour slotBehaviour,
			SlotViewModel slotViewModel)
		{
			_slotBehaviour = slotBehaviour;
			_slotViewModel = slotViewModel;
		}

		void Start() => _slotViewModel.SetCurrentSlot(this);

		public Transform Transform => transform;

		public void Dispose() => _memoryPool?.Despawn(this);

		public void OnDespawned() => _memoryPool = null;

		public void OnSpawned(IMemoryPool memoryPool) => _memoryPool = memoryPool;

		public void AddItemToSlot(IItemFacade item) => _slotBehaviour.AddItem(item);

		public bool Empty => _slotBehaviour.Empty;
		public bool Selected => _slotBehaviour.Selected;

		public void SetEmpty() => _slotBehaviour.ClearStack();

		public int ItemsCount() => _slotBehaviour.ItemsCount;

		public void SetSelected(bool value) => _slotBehaviour.SetSelected(value);

		public void RemoveItem() => _slotBehaviour.RemoveItem();

		public class Factory : PlaceholderFactory<SlotFacade> { }
	}
}