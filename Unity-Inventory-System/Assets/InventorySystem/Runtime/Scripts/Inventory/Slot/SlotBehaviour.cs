using Assets.Scripts.Core.ViewModels;
using UniRx;
using UnityInventorySystem.Presenters.Base;
using Zenject;

namespace UnityInventorySystem.Inventory
{
	public class SlotBehaviour : BasePresenter, IInitializable

	{
		private readonly ItemFacadesPoolBehaviour _itemFacadesPoolBehaviour;
		private readonly SlotViewModel _slotViewModel;

		public SlotBehaviour(
			ItemFacadesPoolBehaviour itemFacadesPoolBehaviour,
			SlotViewModel slotViewModel)
		{
			_itemFacadesPoolBehaviour = itemFacadesPoolBehaviour;
			_slotViewModel = slotViewModel;
		}

		public void Initialize()
		{
			_slotViewModel
				.ItemsInSlot
				.ObserveRemove()
				.Subscribe(value =>
				{
					_itemFacadesPoolBehaviour.RemoveItem(value.Value);
				})
				.AddTo(Disposables);
		}

		public void AddItem(IItemFacade item) => _slotViewModel.ItemsInSlot.Add(item);

		public void RemoveItem() => _slotViewModel.RemoveItem();

		public int ItemsCount => _slotViewModel.ItemsCount;

		public bool Empty => _slotViewModel.Empty;

		public void SetSelected(bool value) => _slotViewModel.SetSelected(value);

		public void ClearStack()
		{
			foreach (var item in _slotViewModel.ItemsInSlot)
			{
				_itemFacadesPoolBehaviour.RemoveItem(item);
			}

			_slotViewModel.ClearItems();
		}
	}
}