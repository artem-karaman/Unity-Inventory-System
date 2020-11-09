using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace UnityInventorySystem
{
	public class ItemDragBehaviour : MonoBehaviour
	{
		private UIManager _uiManager;
		private Canvas _canvas; 
		private RectTransform _rectTransform;
		private CanvasGroup _canvasGroup;

		private Vector2 _savedPosition;
		private RectTransform _savedTransform;
		
		[Inject]
		void Construct(UIManager uiManager)
		{
			_uiManager = uiManager;
		}

		void Start()
		{
			_rectTransform = GetComponent<RectTransform>();
			_canvasGroup = GetComponent<CanvasGroup>();

			_canvas = _uiManager.Canvas;
			_savedPosition = _rectTransform.anchoredPosition;
			_savedTransform = _rectTransform;

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
			
			transform.SetParent(_uiManager.DragingItem.transform);
		}

		private void OnEndDrag(PointerEventData eventData)
		{
			Debug.Log("On end drag");
			_canvasGroup.blocksRaycasts = true;
			_canvasGroup.alpha = 1f;
		}
	}
}
