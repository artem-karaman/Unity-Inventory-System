using InventorySystem.Runtime.Scripts.Core.Models.Interfaces;
using InventorySystem.Runtime.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace InventorySystem.Runtime.Scripts.Inventory.Tooltip
{
	public class TooltipView : MonoBehaviour
	{
		private SharedUIManager _uiManager;
		private IItemFacade _item;
		private RectTransform _toolTipRect;

		private TextMeshProUGUI _topPanelText;
		private TextMeshProUGUI _middlePanelText;
		
		[Inject]
		void Construct(
			IItemFacade item,
			SharedUIManager sharedUIManager)
		{
			_item = item;
			_uiManager = sharedUIManager;
		}

		public void Prepare(IItemFacade item)
		{
			_item = item;
			
			Start();
		}

		void Start()
		{
			gameObject.GetComponent<Image>().color = _item?.Item.Color ?? Color.white;
			
			_topPanelText = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
			_topPanelText.text = _item?.Item?.Title;

			_middlePanelText = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
			_middlePanelText.text = _item?.Item?.Description;

			_toolTipRect = GetComponent<RectTransform>();
			
			SetPosition();
		}

		void SetPosition()
		{
			var padding = 50f;
			var cellSize = 400f * _uiManager.Canvas.scaleFactor;

			Vector3 newPos = _item.Transform.position;
			newPos.z = 0;

			newPos = CalculateXPosition(newPos, padding, cellSize);
			newPos = CalculateYPosition(newPos, cellSize, padding);

			transform.position = newPos;
		}

		private Vector3 CalculateXPosition(Vector3 newPos, float padding, float cellSize)
		{
			float rightEdgeToScreenEdgeDistance =
				Screen.width - (newPos.x + _toolTipRect.rect.width * _uiManager.Canvas.scaleFactor / 2) - padding;

			if (rightEdgeToScreenEdgeDistance > _toolTipRect.rect.width * _uiManager.Canvas.scaleFactor / 2)
			{
				newPos.x += cellSize / 2 + padding;
			}
			else
			{
				newPos.x += -cellSize / 2 - _toolTipRect.rect.width * _uiManager.Canvas.scaleFactor - padding;
			}

			return newPos;
		}

		private Vector3 CalculateYPosition(Vector3 newPos, float cellSize, float padding)
		{
			float bottomEdgeToScreenEdgeDistance = newPos.y;

			//force update tooltip table layout to get actual height
			LayoutRebuilder.ForceRebuildLayoutImmediate(_toolTipRect);

			var tooltipHeight = _toolTipRect.rect.height;

			if (bottomEdgeToScreenEdgeDistance < tooltipHeight)
			{
				_toolTipRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, padding, 0);
				
				newPos.y += tooltipHeight - cellSize;
			}
			else
			{
				newPos.y += cellSize / 2;
			}

			return newPos;
		}


		public class Factory : PlaceholderFactory<IItemFacade, TooltipView>{}
	}
}