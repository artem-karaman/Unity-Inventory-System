using Samples.Scripts.Installers.HorizontalInventoryScene;
using UnityEngine;
using Zenject;

namespace InventorySystem.Samples.Scripts.Installers.HorizontalInventoryScene
{
	[CreateAssetMenu(fileName = "HorizontalInventorySOInstaller", menuName = "SO/Installers/HorizontalInventorySOInstaller")]
	public class HorizontalInventorySOInstaller : ScriptableObjectInstaller<HorizontalInventorySOInstaller>
	{
		#pragma warning disable 0649
		[SerializeField] private HorizontalInventoryMonoInstaller.Settings _horizontalInventoryMonoInstallerSettings;
		#pragma warning restore 0649
		public override void InstallBindings()
		{
			Container.BindInstance(_horizontalInventoryMonoInstallerSettings).IfNotBound();
		}
	}
}

