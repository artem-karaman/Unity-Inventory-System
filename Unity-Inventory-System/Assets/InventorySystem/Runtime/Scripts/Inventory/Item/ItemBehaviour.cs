using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityInventorySystem.Presenters.Base;
using Zenject;

namespace UnityInventorySystem.Inventory
{
	public class ItemBehaviour : BasePresenter, IInitializable
	{
		private readonly SharedUIManager _sharedUIManager;
		private readonly ItemFacade _item;
		private readonly InventoryFacade _inventoryFacade;

		private Canvas _canvas;
		private RectTransform _rectTransform;
		private CanvasGroup _canvasGroup;

		private GameObject _selectedItem;
		private GameObject _oldSlot;

		public ItemBehaviour(
			SharedUIManager sharedUIManager,
			ItemFacade item,
			InventoryFacade inventoryFacade)
		{
			_sharedUIManager = sharedUIManager;
			_item = item;
			_inventoryFacade = inventoryFacade;
		}
		
		public void Initialize()
		{
			_rectTransform = _item.GetComponent<RectTransform>();
			_canvasGroup = _item.GetComponent<CanvasGroup>();
			_selectedItem = _item.gameObject;

			PrepareComponents();
			SubscribeComponents();
		}

		private void PrepareComponents()
		{
			_canvas = _sharedUIManager.Canvas;
		}

		private void SubscribeComponents()
		{
			_item.gameObject
				.AddComponent<ObservablePointerDownTrigger>()
				.OnPointerDownAsObservable()
				.Subscribe(OnPointerDown)
				.AddTo(Disposables);
			
			_item.gameObject
				.AddComponent<ObservableDragTrigger>()
				.OnDragAsObservable()
				.Subscribe(OnDrag)
				.AddTo(Disposables);

			_item.gameObject
				.AddComponent<ObservableBeginDragTrigger>()
				.OnBeginDragAsObservable()
				.Subscribe(OnBeginDrag)
				.AddTo(Disposables);

			_item.gameObject
				.AddComponent<ObservableEndDragTrigger>()
				.OnEndDragAsObservable()
				.Subscribe(OnEndDrag)
				.AddTo(Disposables);
		}

		private void OnPointerDown(PointerEventData eventData)
		{
			_oldSlot = _item.transform.parent.gameObject;
			
			OnPress();
			
			_item.Invoke("OnLongPress", 1f);
		}

		private void OnPress()
		{
			_oldSlot.GetComponent<SlotFacade>().SetSelected(true);
		}
		
		private void OnLongPress()
		{
		}

		private void OnDrag(PointerEventData eventData)
		{
			_rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
		}

		private void OnBeginDrag(PointerEventData eventData)
		{
			_canvasGroup.blocksRaycasts = false;
			_canvasGroup.alpha = .6f;
			
			_item.transform.SetParent(_sharedUIManager.DragingItem.transform);
		}

		private void OnEndDrag(PointerEventData eventData)
		{
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

			if (!eventData.pointerEnter.CompareTag("ItemSlot")) return;
			
			_selectedItem.transform.SetParent(eventData.pointerEnter.transform);
	        eventData.pointerEnter.GetComponent<SlotFacade>().AddItemToSlot(_item);
		}

		private void MoveToOriginalSlot()
		{
			_selectedItem.transform.SetParent(_oldSlot.transform);
		}
	}
}