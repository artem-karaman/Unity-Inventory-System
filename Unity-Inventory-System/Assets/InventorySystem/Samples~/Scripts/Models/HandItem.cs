using InventorySystem.Runtime.Scripts.Core.Models;
using UnityEngine;

namespace UnityInventorySystem
{
	public class HandItem : Item, IHandItem
	{
		public HandItem() 
			: base(Color.blue)
		{
			
		}
	}
}