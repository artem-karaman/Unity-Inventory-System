using InventorySystem.Runtime.Scripts.Inventory;
using Zenject;

namespace InventorySystem.Runtime.Scripts.Installers.Inventory
{
	public class HotBarInstaller : Installer<HotBarInstaller>
	{
		private readonly int _slotsCount;
		
		public HotBarInstaller(
			[InjectOptional]
			int slotsCount)
		{
			_slotsCount = slotsCount;
		}

		public override void InstallBindings()
		{
			Container
				.BindInterfacesAndSelfTo<HotBarFacade>().AsSingle();
			
			InventoryInstaller.Install(Container, _slotsCount);
		}
	}
}