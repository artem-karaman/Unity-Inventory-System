using System.Linq;
using Assets.Scripts.Core.ViewModels;
using TMPro;
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

			if (!_slotViewModel.Empty)
			{
				_image.color = _slotViewModel.ItemsInSlot.First().Item.Color;
			}
		}

		private void SubscribeComponents()
		{
			_slotViewModel
				.Selected
				.AsObservable()
				.Subscribe(PrepareSelectedSlot)
				.AddTo(Disposables);

			_slotViewModel
				.ItemsCount
				.AsObservable()
				.Subscribe(ChangeItemCount)
				.AddTo(Disposables);
		}

		private void PrepareSelectedSlot(bool value)
		{
			_slot.gameObject.GetComponent<Image>().color = value 
				? Color.cyan 
				: Color.white;
		}

		private void ChangeItemCount(int count)
		{
			if (count == 0 || count == 1)
			{
				_itemCount.text = string.Empty;
				_image.color = Color.white;
			}
			else
			{
				_itemCount.text = count.ToString();
				_image.color = _slotViewModel.ItemsInSlot.First().Item.Color;
			}
		}
	}
}