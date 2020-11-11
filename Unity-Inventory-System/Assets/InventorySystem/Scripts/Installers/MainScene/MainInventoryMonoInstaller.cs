using System;
using UnityEngine;
using UnityInventorySystem.Inventory;
using UnityInventorySystem.Presenters;
using Zenject;

namespace UnityInventorySystem.Installers
{
	public class MainInventoryMonoInstaller : MonoInstaller<MainInventoryMonoInstaller>
	{
		[SerializeField] 
		private Transform _mainBackgroundPanelTransform;

		private Settings _settings;
		
		[Inject]
		void Construct(Settings settings)
		{
			_settings = settings;
		}
		
		public override void InstallBindings()
		{
			Container
				.BindFactory<int, InventoryFacade, InventoryFacade.Factory>()
				.FromSubContainerResolve()
				.ByNewPrefabInstaller<InventoryInstaller>(_settings.VerticalInventoryPrefab)
				.UnderTransform(_mainBackgroundPanelTransform);

			Container
				.BindInterfacesAndSelfTo<MainInventoryPresenter>()
				.AsSingle();
		}

		[Serializable]
		public class Settings
		{
			public GameObject VerticalInventoryPrefab;
		}
	}
}