using InventorySystem.Runtime.Scripts.Inventory.Slot;
using InventorySystem.Runtime.Scripts.Managers;
using InventorySystem.Runtime.Scripts.Models;
using InventorySystem.Runtime.Scripts.Presenters.Base;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace InventorySystem.Runtime.Scripts.Inventory.Item
{
	public class ItemBehaviour : BasePresenter, IInitializable
	{
		private readonly SharedUIManager _sharedUIManager;
		private readonly ItemFacade _item;
		private readonly ItemEndDragBehaviour.Factory _itemEndDragBehaviourFactory;

		private Canvas _canvas;
		private RectTransform _rectTransform;
		private CanvasGroup _canvasGroup;

		private GameObject _selectedItem;
		private GameObject _oldSlot;

		private ItemEndDragBehaviour _itemEndDragBehaviour;

		public ItemBehaviour(
			SharedUIManager sharedUIManager,
			ItemFacade item,
			ItemEndDragBehaviour.Factory itemEndDragBehaviourFactory)
		{
			_sharedUIManager = sharedUIManager;
			_item = item;
			_itemEndDragBehaviourFactory = itemEndDragBehaviourFactory;
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
			_item
				.gameObject
				.AddComponent<ObservablePointerDownTrigger>()
				.OnPointerDownAsObservable()
				.Subscribe(OnPointerDown)
				.AddTo(Disposables);
			
			_item
				.gameObject
				.AddComponent<ObservableDragTrigger>()
				.OnDragAsObservable()
				.Subscribe(OnDrag)
				.AddTo(Disposables);

			_item
				.gameObject
				.AddComponent<ObservableBeginDragTrigger>()
				.OnBeginDragAsObservable()
				.Subscribe(OnBeginDrag)
				.AddTo(Disposables);

			_item
				.gameObject
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

		private void SelectOldSlot(bool value)
		{
			_oldSlot
				.GetComponent<SlotFacade>()
				.SetSelected(value);
		}
		
		private void OnPress()
		{
			SelectOldSlot(true);
		}
		
		private void OnLongPress()
		{
			//TODO: implement show tooltip about selected element
		}

		private void OnDrag(PointerEventData eventData)
		{
			SelectOldSlot(false);
			
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
			var itemData = new ItemDragData(_selectedItem, _oldSlot, eventData, _canvasGroup, _item);

			PrepareItemEndDragBehaviour(itemData);
			
			_itemEndDragBehaviour.OnEndDrag();
		}

		private void PrepareItemEndDragBehaviour(ItemDragData itemData)
		{
			if (_itemEndDragBehaviour == null)
			{
				_itemEndDragBehaviour = _itemEndDragBehaviourFactory.Create(itemData);
			}
			else
			{
				_itemEndDragBehaviour.Prepare(itemData);
			}
		}
	}
}