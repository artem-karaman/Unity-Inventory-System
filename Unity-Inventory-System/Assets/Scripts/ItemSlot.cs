using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityInventorySystem
{
    public class ItemSlot : MonoBehaviour , IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log("On Drop");

            if (eventData.pointerDrag != null)
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition =
                    GetComponent<RectTransform>().anchoredPosition;
            }
        }
    }
}
