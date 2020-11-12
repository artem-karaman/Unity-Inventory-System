using UniRx;
using UnityInventorySystem;
using Zenject;

namespace Assets.Scripts.Core.ViewModels
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
			Empty = true;
		}

		public IReactiveProperty<bool> Selected { get; }

		public IReactiveCollection<IItemFacade> ItemsInSlot { get; }

		public bool Empty { get; private set; }
		
		public int ItemsCount { get; private set; }
		
		public ISlotFacade CurrentSlot { get; private set; }
		
		public void Initialize()
		{
			ItemsInSlot
				.ObserveCountChanged()
				.Subscribe(ChangeItemsInSlot)
				.AddTo(_disposables);
		}

		private void ChangeItemsInSlot(int count)
		{
			Empty = count == 0;
			ItemsCount = count;
		}

		public void LateDispose()
		{
			_disposables?.Dispose();
		}

		public void RemoveItem()
		{
			if (Empty) return;
			
			ItemsInSlot.RemoveAt(0);
			CurrentSlot.SetSelected(false);

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

		public void SetCurrentSlot(ISlotFacade slot)
		{
			CurrentSlot = slot;
		}

		public void ClearItems()
		{
			ItemsInSlot.Clear();
		}
	}
}