using UnityEngine;
using UnityEngine.EventSystems;
using UnityInventorySystem.Inventory;

namespace InventorySystem.Runtime.Scripts.Models
{
	public class ItemDragData
	{
		public ItemDragData(
			GameObject selectedItem, 
			GameObject oldSlot, 
			PointerEventData eventData, 
			CanvasGroup canvasGroup,
			ItemFacade item)
		{
			SelectedItem = selectedItem;
			OldSlot = oldSlot;
			EventData = eventData;
			CanvasGroup = canvasGroup;
			Item = item;
		}

		public GameObject SelectedItem { get; }
		public GameObject OldSlot { get; }
		public PointerEventData EventData { get; }
		public CanvasGroup CanvasGroup { get; }
		
		public ItemFacade Item { get; }
		
	}
}