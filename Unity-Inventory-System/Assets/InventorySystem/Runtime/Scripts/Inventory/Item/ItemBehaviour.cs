using InventorySystem.Runtime.Scripts.Core.Messages;
using InventorySystem.Runtime.Scripts.Core.Models.Interfaces;
using InventorySystem.Runtime.Scripts.Inventory.Slot;
using InventorySystem.Runtime.Scripts.Inventory.Tooltip;
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

		private bool _click;
		private float _time;
		private readonly TooltipBehavior _tooltipBehavior;

		public ItemBehaviour(
			SharedUIManager sharedUIManager,
			ItemFacade item,
			ItemEndDragBehaviour.Factory itemEndDragBehaviourFactory,
			TooltipBehavior tooltipBehavior)
		{
			_sharedUIManager = sharedUIManager;
			_item = item;
			_itemEndDragBehaviourFactory = itemEndDragBehaviourFactory;
			_tooltipBehavior = tooltipBehavior;
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
				.AddComponent<ObservablePointerUpTrigger>()
				.OnPointerUpAsObservable()
				.Subscribe(OnPointerUp)
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

			Observable
				.EveryUpdate()
				.Subscribe(_ => ShowTooltip())
				.AddTo(Disposables);
		}

		private void ShowTooltip()
		{
			if (_click && Time.time - _time > .5f)
			{
				_tooltipBehavior.ShowToolTip(_item);
			}
		}

		private void OnPointerUp(PointerEventData eventData)
		{
			_time = 0;
			_click = false;
			
			_tooltipBehavior.HideToolTip();
			_oldSlot?.GetComponent<ISlotFacade>()?.FillSlotBackground();
		}

		private void OnPointerDown(PointerEventData eventData)
		{
			_oldSlot = _item.transform.parent.gameObject;

			_click = true;
			_time = Time.time;
			
			OnPress();
		}

		private void SelectOldSlot(bool value)
		{
			var slot = _oldSlot.GetComponent<SlotFacade>();

			if (slot == null) return;
			
			slot.SetSelected(value);
			MessageBroker.Default.Publish(new NewSlotSelectedMessage(slot));
		}

		private void OnPress()
		{
			SelectOldSlot(true);
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