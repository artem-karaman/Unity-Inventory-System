using System;
using System.Collections.Generic;
using Assets.Scripts.Core.ViewModels;
using UniRx;
using UnityInventorySystem;
using Zenject;

namespace InventorySystem.Runtime.Scripts.Core.ViewModels.Inventory
{
	public class SlotViewModel : IInitializable, ILateDisposable
	{
		private CompositeDisposable _disposables = new CompositeDisposable();
		private readonly InventoryViewModel _inventoryViewModel;
		public SlotViewModel(InventoryViewModel inventoryViewModel)
		{
			_inventoryViewModel = inventoryViewModel;

			Selected = new ReactiveProperty<bool>(false);
			ItemsInSlot = new ReactiveCollection<IItemFacade>();
			ItemsCount = new ReactiveProperty<int>(0);
			Empty = true;
		}

		public IReactiveProperty<bool> Selected { get; }

		public IReactiveCollection<IItemFacade> ItemsInSlot { get; }

		public bool Empty { get; private set; }
		
		public IReactiveProperty<int> ItemsCount { get; private set; }
		
		public ISlotFacade CurrentSlot { get; private set; }

		public void Initialize()
		{
			ItemsInSlot
				.ObserveCountChanged(true)
				.Subscribe(ChangeItemsInSlot)
				.AddTo(_disposables);

			Empty = ItemsInSlot.Count == 0;
		}
			

		private void ChangeItemsInSlot(int count)
		{
			Empty = count == 0;
			ItemsCount.Value = count;
		}

		public void LateDispose() => _disposables?.Dispose();

		public void AddItem(IItemFacade item)
		{
			ItemsInSlot.Add(item);
		}

		public void AddItems(IEnumerable<IItemFacade> items)
		{
			_ = items ?? throw new ArgumentNullException(nameof(items));

			foreach (var item in items)
			{
				ItemsInSlot.Add(item);
			}
		}

		public void RemoveItem()
		{
			if (Empty) return;
			
			ItemsInSlot?.Clear();
		}

		public void SetSelected(bool value)
		{
			if (!Empty && value)
			{
				Selected.Value = true;
				
				_inventoryViewModel.SetSelectedSlot(CurrentSlot);
			}
			else
			{
				Selected.Value = false;
			}
		}

		public void SetCurrentSlot(ISlotFacade slot) => CurrentSlot = slot;

		public void ClearItems() => ItemsInSlot.Clear();
	}
}