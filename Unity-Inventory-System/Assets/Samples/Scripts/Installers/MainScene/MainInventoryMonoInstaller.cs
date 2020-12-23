using System;
using InventorySystem.Runtime.Scripts.Installers.Inventory;
using InventorySystem.Runtime.Scripts.Inventory;
using InventorySystem.Samples.Scripts.Presenters.MainInventoryScene;
using UnityEngine;
using Zenject;

namespace InventorySystem.Samples.Scripts.Installers.MainScene
{
	public class MainInventoryMonoInstaller : MonoInstaller<MainInventoryMonoInstaller>
	{
		#pragma warning disable 0649
		[SerializeField] 
		private Transform _mainBackgroundPanelTransform;
		#pragma warning restore 0649
		
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