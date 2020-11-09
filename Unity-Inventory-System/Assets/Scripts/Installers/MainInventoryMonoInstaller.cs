using Assets.Scripts.Core.ViewModels;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityInventorySystem.Inventory;
using UnityInventorySystem.Presenters;
using Zenject;

namespace UnityInventorySystem.Installers
{
	public class MainInventoryMonoInstaller : MonoInstaller<MainInventoryMonoInstaller>
	{
		[SerializeField] private GameObject _mainBackgroundPanel;
		
		public override void InstallBindings()
		{
			MainInventoryInstaller.Install(Container);
			
			Container
				.BindFactory<InventoryBehaviour, InventoryBehaviour.Factory>()
				//TODO: replace with addressables
				.FromNewComponentOnNewPrefabResource("Prefabs/VerticalInventory")
				.UnderTransform(_mainBackgroundPanel.transform);

			Container
				.BindInterfacesAndSelfTo<MainInventoryPresenter>()
				.AsSingle();
		}
	}
}