using System;
using UnityEngine;
using UnityInventorySystem.Installers;
using UnityInventorySystem.Inventory;
using UnityInventorySystem.Presenters;
using Zenject;

namespace InventorySystem.Samples.Scripts.Installers.HorizontalInventoryScene
{
	public class HorizontalInventoryMonoInstaller : MonoInstaller<HorizontalInventoryMonoInstaller>
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