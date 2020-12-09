using UnityEngine;
using Zenject;

namespace InventorySystem.Samples.Scripts.Installers.HorizontalInventoryScene
{
	[CreateAssetMenu(fileName = "HorizontalInventorySOInstaller", menuName = "SO/Installers/HorizontalInventorySOInstaller")]
	public class HorizontalInventorySOInstaller : ScriptableObjectInstaller<HorizontalInventorySOInstaller>
	{
		[SerializeField] private HorizontalInventoryMonoInstaller.Settings _horizontalInventoryMonoInstallerSettings;
		public override void InstallBindings()
		{
			Container.BindInstance(_horizontalInventoryMonoInstallerSettings).IfNotBound();
		}
	}
}

