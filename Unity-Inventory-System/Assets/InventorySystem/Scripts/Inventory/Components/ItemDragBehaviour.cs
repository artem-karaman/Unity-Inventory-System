using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace UnityInventorySystem
{
	public class ItemDragBehaviour : MonoBehaviour
	{
		private SharedUIManager _sharedUIManager;
		private Canvas _canvas; 
		private RectTransform _rectTransform;
		private CanvasGroup _canvasGroup;

		private GameObject _selectedItem;
		private GameObject _oldSlot;
		
		[SerializeField]
		[Tooltip("How long must pointer be down on this object to trigger a long press")]
		private float _holdTime = 1f;
		
		[Inject]
		void Construct(SharedUIManager sharedUIManager)
		{
			_sharedUIManager = sharedUIManager;
		}

		void Start()
		{
			_rectTransform = GetComponent<RectTransform>();
			_canvasGroup = GetComponent<CanvasGroup>();
			_selectedItem = gameObject;

			_canvas = _sharedUIManager.Canvas;

			gameObject
				.AddComponent<ObservableDragTrigger>()
				.OnDragAsObservable()
				.Subscribe(OnDrag)
				.AddTo(this);

			gameObject
				.AddComponent<ObservablePointerDownTrigger>()
				.OnPointerDownAsObservable()
				.Subscribe(OnPointerDown)
				.AddTo(this);

			gameObject
				.AddComponent<ObservableBeginDragTrigger>()
				.OnBeginDragAsObservable()
				.Subscribe(OnBeginDrag)
				.AddTo(this);

			gameObject
				.AddComponent<ObservableEndDragTrigger>()
				.OnEndDragAsObservable()
				.Subscribe(OnEndDrag)
				.AddTo(this);
		}

		private void OnPointerDown(PointerEventData eventData)
		{
			Debug.Log("Pointer down event");

			_oldSlot = transform.parent.gameObject;
			
			Invoke("OnLongPress", _holdTime);
		}

		private void OnLongPress()
		{
			Debug.Log("Log press detected ");
		}

		private void OnDrag(PointerEventData eventData)
		{
			Debug.Log("On drag");
			_rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
		}

		private void OnBeginDrag(PointerEventData eventData)
		{
			Debug.Log("On begin drag");
			_canvasGroup.blocksRaycasts = false;
			_canvasGroup.alpha = .6f;
			
			transform.SetParent(_sharedUIManager.DragingItem.transform);
		}

		private void OnEndDrag(PointerEventData eventData)
		{
			Debug.Log("On end drag");
			_canvasGroup.blocksRaycasts = true;
			_canvasGroup.alpha = 1f;

			if (eventData.pointerEnter == null)
			{
				MoveToOriginalSlot();
				
                return;
            }
			
			if (!eventData.pointerEnter.CompareTag("ItemSlot"))
			{
				MoveToOriginalSlot();
				
                return;
            }

            if (eventData.pointerEnter.CompareTag("ItemSlot"))
            {
	            _selectedItem.transform.SetParent(eventData.pointerEnter.transform);
	            
	            // if (eventData.pointerEnter.GetComponent<SlotInteractor>().Empty())
	            // {
		           //  _selectedItem.transform.SetParent(eventData.pointerEnter.transform);
	            // }
	            // else
	            // {
		           //  MoveToOriginalSlot();
	            // }
            }
		}

		public void MoveToOriginalSlot()
		{
			_selectedItem.transform.SetParent(_oldSlot.transform);
		}
	}
}
