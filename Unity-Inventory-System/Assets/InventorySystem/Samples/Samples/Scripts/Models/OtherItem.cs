using InventorySystem.Runtime.Scripts.Core.Models;
using InventorySystem.Samples.Scripts.Models.Interfaces;
using UnityEngine;

namespace InventorySystem.Samples.Scripts.Models
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