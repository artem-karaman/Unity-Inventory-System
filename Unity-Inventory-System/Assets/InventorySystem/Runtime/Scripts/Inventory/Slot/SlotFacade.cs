using System.Collections.Generic;
using InventorySystem.Runtime.Scripts.Core.Messages;
using InventorySystem.Runtime.Scripts.Core.Models.Interfaces;
using InventorySystem.Runtime.Scripts.Core.ViewModels.Inventory;
using UniRx;
using UnityEngine;
using Zenject;

namespace InventorySystem.Runtime.Scripts.Inventory.Slot
{
	public class SlotFacade : MonoBehaviour, IPoolable<IMemoryPool>, ISlotFacade
	{
		private SlotBehaviour _slotBehaviour;
		private SlotView _slotView;
		private SlotViewModel _slotViewModel;
		
		private IMemoryPool _memoryPool;

		[Inject]
		void Construct(
			SlotBehaviour slotBehaviour,
			SlotView slotView,
			SlotViewModel slotViewModel)
		{
			_slotBehaviour = slotBehaviour;
			_slotView = slotView;
			_slotViewModel = slotViewModel;
		}

		void Start()
		{
			MessageBroker
				.Default
				.Receive<NewSlotSelectedMessage>()
				.AsObservable()
				.Subscribe(value => SetSelected(Equals(value.SelectedSlot)))
				.AddTo(this);
		}

		public void FillSlotBackground()
		{
			_slotView.FillSlotBackground();
		}
		
		public Transform Transform => transform;

		public void Dispose() => _memoryPool?.Despawn(this);

		public void AddItemToSlot(IItemFacade item) => _slotBehaviour.AddItem(item);

		public void AddItemsToSlot(IEnumerable<IItemFacade> items) => _slotBehaviour.AddItems(items);

		public bool Empty => _slotBehaviour.Empty;
		
		public bool Selected => _slotBehaviour.Selected;

		public void SetEmpty() => _slotBehaviour.ClearStack();

		public void SetSelected(bool value) => _slotBehaviour.SetSelected(value);

		public void ClearItems() => _slotBehaviour.ClearItemsInSlot();
		
		public IReactiveCollection<IItemFacade> AllItemsInSlot => _slotBehaviour.GetAllItemsInSlot;

		#region PoolBehaviour
		public void OnDespawned() => _memoryPool = null;
		public void OnSpawned(IMemoryPool memoryPool) => _memoryPool = memoryPool;

		#endregion

		public class Factory : PlaceholderFactory<SlotFacade> { }
	}
}