using InventorySystem.Runtime.Scripts.Core.Models.Interfaces;

namespace InventorySystem.Runtime.Scripts.Core.Messages
{
	public class NewSlotSelectedMessage
	{
		public NewSlotSelectedMessage(ISlotFacade selectedSlot)
		{
			SelectedSlot = selectedSlot;
		}
		
		public ISlotFacade SelectedSlot { get; }
	}
}