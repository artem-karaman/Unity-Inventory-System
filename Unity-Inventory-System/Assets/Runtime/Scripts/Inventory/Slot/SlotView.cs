using Assets.Scripts.Core.ViewModels;
using Zenject;

namespace UnityInventorySystem.Inventory
{
	public class SlotView : IInitializable
	{
		private readonly SlotViewModel _slotViewModel;

		public SlotView(SlotViewModel slotViewModel)
		{
			_slotViewModel = slotViewModel;
		}
		
		public void Initialize()
		{
			
		}
	}
}