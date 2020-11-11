using UnityEngine;
using Zenject;

namespace UnityInventorySystem.Inventory
{
	public class SlotInteractor : MonoBehaviour
	{
		private SlotFacade _slotFacade;
		
		[Inject]
		public void Construct(SlotFacade slotFacade)
		{
			_slotFacade = slotFacade;
		}

		public bool Empty()
		{
			return _slotFacade.Empty;
		}
	}
}