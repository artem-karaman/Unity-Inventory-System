using InventorySystem.Runtime.Scripts.Models;
using UnityEngine;
using Zenject;

namespace UnityInventorySystem.Inventory
{
	public class ItemEndDragBehaviour
	{
		private readonly ItemFacadesPoolBehaviour _itemFacadesPoolBehaviour;
		
		private ItemDragData _itemDragData;
		private SlotFacade _slotFacade;

		public ItemEndDragBehaviour(ItemFacadesPoolBehaviour itemFacadesPoolBehaviour,
			ItemDragData itemDragData)
		{
			_itemFacadesPoolBehaviour = itemFacadesPoolBehaviour;
			_itemDragData = itemDragData;
		}

		public void Prepare(ItemDragData itemDragData)
		{
			_itemDragData = itemDragData;
		}

		//TODO: Need to clear item from old slot when move item from it
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
				if (_itemDragData.Item.Item.MaxStack > 1 &&
				    _slotFacade.ItemsCount() < _itemDragData.Item.Item.MaxStack) 
				{
					_itemFacadesPoolBehaviour.RemoveItem(_itemDragData.SelectedItem.GetComponent<IItemFacade>());
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
			// set item to the center of slot
			_itemDragData.SelectedItem.GetComponent<RectTransform>().localPosition = Vector3.zero;
			
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
			_itemDragData.SelectedItem.GetComponent<RectTransform>().localPosition = Vector3.zero;
		}
		
		public class Factory : PlaceholderFactory<ItemDragData, ItemEndDragBehaviour>{}
	}
}