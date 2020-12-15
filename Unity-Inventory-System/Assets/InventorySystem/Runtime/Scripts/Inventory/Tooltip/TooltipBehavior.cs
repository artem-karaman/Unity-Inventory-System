using InventorySystem.Runtime.Scripts.Core.Models.Interfaces;

namespace InventorySystem.Runtime.Scripts.Inventory.Tooltip
{
	public class TooltipBehavior
	{
		private readonly TooltipView.Factory _factory;
		private TooltipView _tooltipView;
		
		public TooltipBehavior(TooltipView.Factory factory)
		{
			_factory = factory;
		}

		public void ShowToolTip(IItem item)
		{
			Show(item);
		}

		public void HideToolTip()
		{
			_tooltipView.gameObject.SetActive(false);
		}

		private void Show(IItem item)
		{
			if (_tooltipView == null)
			{
				_tooltipView = _factory.Create(item);
			}
			else
			{
				_tooltipView.Prepare(item);
			}
			
			_tooltipView.gameObject.SetActive(true);
			
		}
	}
}