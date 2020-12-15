using UnityEngine;
using Zenject;

namespace InventorySystem.Runtime.Scripts.Inventory.Tooltip
{
	public class TooltipBehaviour : MonoBehaviour
	{
		
		public class Factory : PlaceholderFactory<TooltipBehaviour>{}
	}
}