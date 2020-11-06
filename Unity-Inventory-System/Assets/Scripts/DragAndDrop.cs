using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityInventorySystem
{
    public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private Canvas _canvas;
    
        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;

        void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("Pointer down event");
        }

        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log("On drag");
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("On begin drag");
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.alpha = .6f;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("On end drag");
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.alpha = 1f;
        }
    }
}
