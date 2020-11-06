using Malee.List;
using UnityEngine;

namespace UnityInventorySystem
{
	[CreateAssetMenu(fileName = "InventoryItems", menuName = "SO/InventoryItemsSO")]
	public class InventoryItemsSo : ScriptableObject
	{
		[Reorderable(paginate = true)]
		public ItemsList Items;
	}
}
