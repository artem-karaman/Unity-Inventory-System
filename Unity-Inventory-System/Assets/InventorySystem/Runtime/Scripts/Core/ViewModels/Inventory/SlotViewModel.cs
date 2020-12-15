using System;
using System.Collections.Generic;
using InventorySystem.Runtime.Scripts.Core.Models.Interfaces;
using UniRx;
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
			Empty = new ReactiveProperty<bool>(true);
			PaintSlot = new ReactiveProperty<bool>(true);
		}
		public IReactiveProperty<bool> Selected { get; }
		public IReactiveCollection<IItemFacade> ItemsInSlot { get; }
		public IReactiveProperty<bool> Empty { get; }
		public IReactiveProperty<bool> PaintSlot { get; }

		public void Initialize()
		{
			ItemsInSlot
				.ObserveCountChanged(true)
				.Subscribe(ChangeItemsInSlot)
				.AddTo(_disposables);

			ItemsInSlot
				.ObserveReset()
				.Subscribe(_ => Empty.Value = true)
				.AddTo(_disposables);

			ItemsInSlot
				.ObserveAdd()
				.Subscribe(_ => Empty.Value = false)
				.AddTo(_disposables);
			

			Empty.Value = ItemsInSlot.Count == 0;
		}

		private void ChangeItemsInSlot(int count) => Empty.Value = count == 0;

		public void LateDispose() => _disposables?.Dispose();

		public void AddItem(IItemFacade item) => ItemsInSlot.Add(item);

		public void AddItems(IEnumerable<IItemFacade> items)
		{
			_ = items ?? throw new ArgumentNullException(nameof(items));

			foreach (var item in items)
			{
				ItemsInSlot.Add(item);
			}
		}

		public void ClearItemsInSlot()
		{
			if (Empty.Value) return;
			
			ClearItems();
		}

		public void SetSelected(bool value)
		{
			if (!Empty.Value)
			{
				Selected.Value = value;
			}
		}

		public void ClearItems()
		{
			ItemsInSlot.Clear();
		}
	}
}