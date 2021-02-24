using System;
using InventorySystem.Runtime.Scripts.Installers.Inventory;
using InventorySystem.Runtime.Scripts.Inventory;
using InventorySystem.Samples.Scripts.Presenters.HorizontalInventoryScene;
using UnityEngine;
using Zenject;

namespace InventorySystem.Samples.Scripts.Installers.HorizontalInventoryScene
{
	public class HorizontalInventoryMonoInstaller : MonoInstaller<HorizontalInventoryMonoInstaller>
	{
		#pragma warning disable 0649
		[SerializeField] 
		private Transform _mainBackgroundPanelTransform;
		#pragma warning disable 0649
		
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
				.ByNewPrefabInstaller<InventoryInstaller>(_settings.HorizontalInventoryPrefab)
				.UnderTransform(_mainBackgroundPanelTransform);

			Container
				.BindFactory<int, HotBarFacade, HotBarFacade.Factory>()
				.FromSubContainerResolve()
				.ByNewPrefabInstaller<HotBarInstaller>(_settings.HotBarInventoryPrefab)
				.UnderTransform(_mainBackgroundPanelTransform);

			Container
				.BindInterfacesAndSelfTo<HorizontalInventoryPresenter>()
				.AsSingle();
		}

		[Serializable]
		public class Settings
		{
			public GameObject HorizontalInventoryPrefab;
			public GameObject HotBarInventoryPrefab;
		}
	}
}