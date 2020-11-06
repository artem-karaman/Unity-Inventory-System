using Zenject;

namespace UnityInventorySystem.Installers
{
	public class MainInventoryMonoInstaller : MonoInstaller<MainInventoryMonoInstaller>
	{
		public override void InstallBindings()
		{
			MainInventoryInstaller.Install(Container);
		}
	}
}