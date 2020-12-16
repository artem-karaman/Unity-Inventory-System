using Malee.List;
using UnityEngine;

namespace InventorySystem.Samples.Scripts
{
	[CreateAssetMenu(fileName = "InventorySlots", menuName = "SO/InventoryItemsSO")]
	public class InventoryItemsSo : ScriptableObject
	{
		[Reorderable]
		public ItemsList Items;
	}
}
