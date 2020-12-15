using System.Linq;
using InventorySystem.Runtime.Scripts.Core.ViewModels.Inventory;
using InventorySystem.Runtime.Scripts.Presenters.Base;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace InventorySystem.Runtime.Scripts.Inventory.Slot
{
	public class SlotView : BasePresenter, IInitializable
	{
		private readonly SlotViewModel _slotViewModel;
		private readonly SlotFacade _slot;

		private TextMeshProUGUI _itemCount;
		private Image _image;

		public SlotView(
			SlotViewModel slotViewModel,
			SlotFacade slot)
		{
			_slotViewModel = slotViewModel;
			_slot = slot;
		}

		public void Initialize()
		{
			PrepareComponents();
			SubscribeComponents();
		}

		private void PrepareComponents()
		{
			_itemCount = _slot.GetComponentInChildren<TextMeshProUGUI>();
			_image = _slot.GetComponent<Image>();

			_itemCount.text = string.Empty;
			
			FillSlot();
		}

		private void SubscribeComponents()
		{
			_slotViewModel
				.Selected
				.AsObservable()
				.Subscribe(PrepareSelectedSlot)
				.AddTo(Disposables);

			_slotViewModel
				.ItemsInSlot
				.ObserveCountChanged()
				.Subscribe(ChangeItemCount)
				.AddTo(Disposables);


			_slotViewModel
				.ItemsInSlot
				.ObserveEveryValueChanged(x => x.Count)
				.AsObservable()
				.Subscribe(value => FillSlot(value))
				.AddTo(Disposables);
		}

		private void FillSlot()
		{
			Color color = Color.white;

			if (_slotViewModel.ItemsInSlot.Any())
			{
				var c = _slotViewModel.ItemsInSlot?.First()?.Item?.Color;
				color = c.HasValue ? c.Value : Color.white;
			}
			
			_image.color = color;
		}

		private void FillSlot(int value)
		{
			_image.color = value == 0 ? Color.white 
				: _slotViewModel.ItemsInSlot.First().Item.Color;
		}

		private void PrepareSelectedSlot(bool value) =>
			_slot.gameObject.GetComponent<Image>().color
				= value
					? Color.cyan
					: Color.white;

		private void ChangeItemCount(int count) => 
			_itemCount.text = count == 0 || count == 1 ? string.Empty : count.ToString();

		public void FillSlotBackground()
		{
			FillSlot();
		}
	}
}