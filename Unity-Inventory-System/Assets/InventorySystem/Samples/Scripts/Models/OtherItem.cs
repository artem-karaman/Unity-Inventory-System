using InventorySystem.Runtime.Scripts.Core.Models;
using UnityEngine;

namespace UnityInventorySystem
{
	public class OtherItem : Item, IOtherItem
	{
		private static int maxItemStack = 20;
		
		public OtherItem()
			: base(Color.gray, maxItemStack)
		{
			
		}
	}
}