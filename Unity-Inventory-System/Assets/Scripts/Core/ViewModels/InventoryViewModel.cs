using System;
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
		}
		
		public void Initialize()
		{
		}

		[Serializable]
		public class Settings
		{
		}
	}
}
