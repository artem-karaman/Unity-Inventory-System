using UnityEngine;
using UnityInventorySystem.Inventory;
using UnityInventorySystem.Presenters;
using Zenject;

namespace UnityInventorySystem.Installers.HorizontalInventoryScene
{
	public class HorizontalInventoryMonoInstaller : MonoInstaller<HorizontalInventoryMonoInstaller>
	{
		[SerializeField] private GameObject _mainBackgroundPanel;
		
		public override void InstallBindings()
		{
			InventoryInstaller.Install(Container);
			
			Container
				.BindFactory<InventoryBehaviour, InventoryBehaviour.Factory>()
				//TODO: replace with addressables
				.FromNewComponentOnNewPrefabResource("Prefabs/HorizontalInventory")
				.UnderTransform(_mainBackgroundPanel.transform);

			Container
				.BindInterfacesAndSelfTo<HorizontalInventoryPresenter>()
				.AsSingle();
		}
	}
}