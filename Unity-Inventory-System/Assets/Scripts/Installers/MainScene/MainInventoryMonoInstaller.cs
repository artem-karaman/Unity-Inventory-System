using UnityEngine;
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
			InventoryInstaller.Install(Container);
			
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