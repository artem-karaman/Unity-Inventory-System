using System;
using UniRx;
using Zenject;

namespace Assets.Scripts.Core.ViewModels
{
	public class InventoryViewModel : IInitializable
	{
		private readonly Settings _settings;
		
		public InventoryViewModel(
			Settings settings)
		{
			_settings = settings;

			SlotsCount = new ReactiveProperty<int>(0);
		}

		public IReactiveProperty<int> SlotsCount;
		
		public void Initialize()
		{
			SlotsCount.Value = CalculateSlotsCountWithFullRow();
		}

		private int CalculateSlotsCountWithFullRow()
		{
			var slotsCount = _settings.slotsCount;

			var modulo = slotsCount % 4;

			return slotsCount % 4 == 0 ? slotsCount : slotsCount + (4 - modulo);
		}

		[Serializable]
		public class Settings
		{
			public int slotsCount;
		}
	}
}
