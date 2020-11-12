using Assets.Scripts.Core.ViewModels;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityInventorySystem.Presenters.Base;
using Zenject;

namespace UnityInventorySystem.Inventory
{
	public class SlotView : BasePresenter, IInitializable
	{
		private readonly SlotViewModel _slotViewModel;
		private readonly SlotFacade _slot;
		
		public SlotView(
			SlotViewModel slotViewModel,
			SlotFacade slot)
		{
			_slotViewModel = slotViewModel;
			_slot = slot;
		}
		
		public void Initialize()
		{
			_slotViewModel
				.Selected
				.AsObservable()
				.Subscribe(PrepareSelectedSlot)
				.AddTo(Disposables);
		}

		private void PrepareSelectedSlot(bool value)
		{
			_slot.gameObject.GetComponent<Image>().color = value ? Color.cyan : Color.white;
		}
	}
}