using System;
using System.Linq;
using Cysharp.Threading.Tasks;
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

		private bool _click = false;
		private float _time;
		private bool tooltipIsShown;
		private readonly TooltipBehaviour.Factory _toolTipFactory;
		private TooltipBehaviour _tooltipBehaviour;

		public ItemBehaviour(
			SharedUIManager sharedUIManager,
			ItemFacade item,
			ItemEndDragBehaviour.Factory itemEndDragBehaviourFactory,
			TooltipBehaviour.Factory toolTipFactory)
		{
			_sharedUIManager = sharedUIManager;
			_item = item;
			_itemEndDragBehaviourFactory = itemEndDragBehaviourFactory;
			_toolTipFactory = toolTipFactory;
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
				.Subscribe(_ =>
				{
					if (_click && (Time.time - _time) > .5f)
					{
						ShowTooltip();
					}
				})
				.AddTo(Disposables);
			
		}

		private void OnPointerUp(PointerEventData eventData)
		{
			_time = 0;
			_click = false;
			
			HideTooltip();
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

		private void ShowTooltip()
		{
			if (!tooltipIsShown)
			{
				if (_tooltipBehaviour == null)
				{
					_tooltipBehaviour = _toolTipFactory.Create(_item.Item);
				}
				else
				{
					_tooltipBehaviour.gameObject.SetActive(true);
				}
				
				tooltipIsShown = true;
			}
		}

		private void HideTooltip()
		{
			if (!tooltipIsShown) return;
			_tooltipBehaviour.gameObject.SetActive(false);
			tooltipIsShown = false;
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