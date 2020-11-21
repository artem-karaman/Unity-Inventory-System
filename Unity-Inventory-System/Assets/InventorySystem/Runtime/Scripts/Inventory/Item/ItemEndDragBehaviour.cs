using InventorySystem.Runtime.Scripts.Models;
using UniRx.Triggers;
using UnityEngine;

namespace UnityInventorySystem.Inventory
{
	public class ItemEndDragBehaviour
	{
		private ItemDragData _itemDragData;
		private SlotFacade _slotFacade;

		public ItemEndDragBehaviour(
			ItemDragData itemDragData)
		{
			_itemDragData = itemDragData;
		}

		public void Prepare(ItemDragData itemDragData)
		{
			_itemDragData = itemDragData;
		}

		public void OnEndDrag()
		{
			FindSlot();
			
			_itemDragData.CanvasGroup.blocksRaycasts = true;
			_itemDragData.CanvasGroup.alpha = 1f;

			if (_itemDragData.EventData.pointerEnter == null)
			{
				MoveToOriginalSlot();
				
				return;
			}

			if (_itemDragData.EventData.pointerEnter.CompareTag("Item"))
			{
				if (_itemDragData.Item.Item.MaxStack > 1 && _slotFacade.ItemsCount() < _itemDragData.Item.Item.MaxStack) 
				{
					Object.Destroy(_itemDragData.SelectedItem.gameObject); 
					_slotFacade.AddItemToSlot(_itemDragData.Item);

					return;
				}
			}
			
			if (!_itemDragData.EventData.pointerEnter.CompareTag("ItemSlot"))
			{
				MoveToOriginalSlot();
				
				return;
			}

			_itemDragData.SelectedItem.transform.SetParent(_itemDragData.EventData.pointerEnter.transform);
			_slotFacade.AddItemToSlot(_itemDragData.Item);
		}

		private void FindSlot()
		{
			_slotFacade = _itemDragData.EventData.pointerEnter.GetComponent<SlotFacade>();

			if (_slotFacade == null)
			{
				_slotFacade = _itemDragData.EventData.pointerEnter.GetComponentInParent<SlotFacade>();
			}
		}

		private void MoveToOriginalSlot()
		{
			_itemDragData.SelectedItem.transform.SetParent(_itemDragData.OldSlot.transform);
		}
	}
}