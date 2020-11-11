using System.Collections.Generic;
using UniRx;

namespace Assets.Scripts.Core.ViewModels
{
	public class InventoryViewModel
	{
		private readonly int _slotsCount;
		
		private int _totalItemsInRowOrColumn = 4;
		
		public InventoryViewModel(int slotsCount)
		{
			_slotsCount = slotsCount;

			SlotsCount = new ReactiveProperty<int>(slotsCount);
		}

		public IReactiveProperty<int> SlotsCount;

		private int CalculateSlotsCountWithFullRow()
		{
			var modulo = _slotsCount % _totalItemsInRowOrColumn;

			return _slotsCount % 4 == 0 ? _slotsCount : _slotsCount + (_totalItemsInRowOrColumn - modulo);
		}
	}
}
