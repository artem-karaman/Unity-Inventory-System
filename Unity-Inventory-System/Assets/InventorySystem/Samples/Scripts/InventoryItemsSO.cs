using Malee.List;
using UnityEngine;

namespace UnityInventorySystem
{
	[CreateAssetMenu(fileName = "InventorySlots", menuName = "SO/InventoryItemsSO")]
	public class InventoryItemsSo : ScriptableObject
	{
		[Reorderable]
		public ItemsList Items;
	}
}
