using System.Linq;
using InventorySystem.Runtime.Scripts.Core.Messages;
using InventorySystem.Runtime.Scripts.Core.Models.Interfaces;
using InventorySystem.Runtime.Scripts.Core.ViewModels.Inventory;
using InventorySystem.Runtime.Scripts.Inventory.Item;
using InventorySystem.Runtime.Scripts.Models;
using UniRx;
using UnityEngine;

namespace InventorySystem.Runtime.Scripts.Inventory
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
			_itemDragData.CanvasGroup.blocksRaycasts = true;
			_itemDragData.CanvasGroup.alpha = 1f;
			
			if(!FindSlot()) return;
			if (PointerNull()) return;
			if (GetOldSlot(out var oldSlot)) return;
			if (CanAddItemToSlotWithItem(oldSlot)) return;
			if (PointerNotItemSlot()) return;

			MoveItemToEmptySlot(oldSlot);
			PublishNewSlotSelectedMessage();
		}

		private bool GetOldSlot(out ISlotFacade oldSlot)
		{
			oldSlot = _itemDragData?.OldSlot?.GetComponent<ISlotFacade>();

			if (oldSlot != null) return false;
			MoveToOriginalSlot();
			return true;
		}

		private bool CanAddItemToSlotWithItem(ISlotFacade oldSlot)
		{
			if (!_itemDragData.EventData.pointerEnter.CompareTag("Item"))
			{
				MoveToOriginalSlot();
				return false;
			}

			if (!PointerEnterItemEqualToDraggingItem())
			{
				MoveToOriginalSlot();
				return false;
			}

			if (_itemDragData.Item.Item.MaxStack
			    <= 1
			    || _slotFacade.AllItemsInSlot.Count
			    >= _itemDragData.Item.Item.MaxStack)
			{
				MoveToOriginalSlot();
				return false;
			}
			
			AddItemToStack(oldSlot);
			
			return true;
		}

		private bool PointerNotItemSlot()
		{
			if (_itemDragData.EventData.pointerEnter.CompareTag("ItemSlot")) return false;
			MoveToOriginalSlot();
			return true;
		}

		private bool PointerEnterItemEqualToDraggingItem()
		{
			var existingItem = _itemDragData.EventData.pointerEnter.GetComponent<IItemFacade>();

			var existingItemType = existingItem.Item.GetType();
			var dragingItemType = _itemDragData.Item.Item.GetType();
			
			return existingItemType == dragingItemType;
		}

		private bool PointerNull()
		{
			var result = false;
			
			if (_itemDragData.EventData.pointerEnter == null)
			{
				MoveToOriginalSlot();
				result = true;
			}
			
			return result;
		}

		private void MoveItemToEmptySlot(ISlotFacade oldSlot)
		{
			_itemDragData.SelectedItem.transform.SetParent(_itemDragData.EventData.pointerEnter.transform);
			
			_itemDragData.SelectedItem.GetComponent<RectTransform>().localPosition = Vector3.zero;

			if (!_slotFacade.Equals(oldSlot) && _slotFacade.Empty)
			{
				_inventoryViewModel.MoveItem(oldSlot, _slotFacade);

				oldSlot.ClearItems();
			}
			else
			{
				MoveToOriginalSlot();
			}
		}

		private void AddItemToStack(ISlotFacade oldSlot)
		{
			_itemFacadesPoolBehaviour.RemoveItem(oldSlot.AllItemsInSlot?.First());
			_inventoryViewModel.MoveItem(oldSlot, _slotFacade);
			oldSlot.ClearItems();

			MessageBroker.Default.Publish(new NewSlotSelectedMessage(null));
		}

		private bool FindSlot()
		{
			_slotFacade =
				_itemDragData.EventData?.pointerEnter?.GetComponent<ISlotFacade>()
				?? _itemDragData.EventData?.pointerEnter?.GetComponentInParent<ISlotFacade>();

			if (_slotFacade == null)
			{
				MoveToOriginalSlot();
			}

			return _slotFacade != null;
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