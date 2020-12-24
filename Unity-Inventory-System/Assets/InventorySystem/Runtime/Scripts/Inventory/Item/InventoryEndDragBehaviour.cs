using System.Linq;
using InventorySystem.Runtime.Scripts.Core.Messages;
using InventorySystem.Runtime.Scripts.Core.Models.Interfaces;
using InventorySystem.Runtime.Scripts.Core.ViewModels.Inventory;
using InventorySystem.Runtime.Scripts.Models;
using UniRx;
using UnityEngine;

namespace InventorySystem.Runtime.Scripts.Inventory.Item
{
	public class InventoryEndDragBehaviour
	{
		private readonly ItemFacadesPoolBehaviour _itemFacadesPoolBehaviour;
		private readonly InventoryViewModel _inventoryViewModel;
		
		private ItemDragData _itemDragData;
		private ISlotFacade _slotFacade;

		public InventoryEndDragBehaviour(
			ItemFacadesPoolBehaviour itemFacadesPoolBehaviour,
			InventoryViewModel inventoryViewModel)
		{
			_itemFacadesPoolBehaviour = itemFacadesPoolBehaviour;
			_inventoryViewModel = inventoryViewModel;
		}

		public void Prepare(ItemDragData itemDragData) => _itemDragData = itemDragData;

		public void OnEndDrag()
		{
			FindSlot();
			
			_itemDragData.CanvasGroup.blocksRaycasts = true;
			_itemDragData.CanvasGroup.alpha = 1f;

			if (PointerNull()) return;
			
			var oldSlot = _itemDragData.OldSlot.GetComponent<ISlotFacade>();

			if (CanAddItemToSlotWithItem(oldSlot)) return;
			if (PointerNotItemSlot()) return;

			MoveItemToEmptySlot(oldSlot);
			PublishNewSlotSelectedMessage();
		}

		private bool PointerNotItemSlot()
		{
			if (_itemDragData.EventData.pointerEnter.CompareTag("ItemSlot")) return false;
			MoveToOriginalSlot();
			return true;
		}

		private bool CanAddItemToSlotWithItem(ISlotFacade oldSlot)
		{
			if (!_itemDragData.EventData.pointerEnter.CompareTag("Item")) return false;
			
			if (_itemDragData.Item.Item.MaxStack 
			    <= 1 
			    || _slotFacade.AllItemsInSlot.Count 
			    >= _itemDragData.Item.Item.MaxStack) return false;
			
			AddItemToStack(oldSlot);
			
			return true;
		}

		private bool PointerNull()
		{
			if (_itemDragData.EventData.pointerEnter != null) return false;
			
			MoveToOriginalSlot();
				
			return true;
		}

		private void MoveItemToEmptySlot(ISlotFacade oldSlot)
		{
			_itemDragData.SelectedItem.transform.SetParent(_itemDragData.EventData.pointerEnter.transform);
			// set item to the center of slot
			_itemDragData.SelectedItem.GetComponent<RectTransform>().localPosition = Vector3.zero;

			if (!_slotFacade.Equals(oldSlot) && _slotFacade.Empty)
			{
				_inventoryViewModel.MoveItem(oldSlot, _slotFacade);

				oldSlot?.ClearItems();
			}
		}

		private void AddItemToStack(ISlotFacade oldSlot)
		{
			_itemFacadesPoolBehaviour.RemoveItem(oldSlot.AllItemsInSlot?.First());
			_inventoryViewModel.MoveItem(oldSlot, _slotFacade);
			oldSlot.ClearItems();

			MessageBroker.Default.Publish(new NewSlotSelectedMessage(null));
		}

		private void FindSlot()
		{
			_slotFacade = _itemDragData.EventData.pointerEnter.GetComponent<ISlotFacade>();

			if (_slotFacade == null)
			{
				_slotFacade = _itemDragData.EventData.pointerEnter.GetComponentInParent<ISlotFacade>();
			}
		}

		private void MoveToOriginalSlot()
		{
			_itemDragData.SelectedItem.transform.SetParent(_itemDragData.OldSlot.transform);
			_itemDragData.SelectedItem.GetComponent<RectTransform>().localPosition = Vector3.zero;
		}

		private void PublishNewSlotSelectedMessage()
		{
			MessageBroker.Default.Publish(new NewSlotSelectedMessage(null));
		}
	}
}