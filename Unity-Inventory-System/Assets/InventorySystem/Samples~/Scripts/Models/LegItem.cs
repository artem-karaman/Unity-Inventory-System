using InventorySystem.Runtime.Scripts.Core.Models;
using InventorySystem.Samples.Scripts.Models.Interfaces;
using UnityEngine;

namespace InventorySystem.Samples.Scripts.Models
{
	public class LegItem : Item, ILegItem
	{
		public LegItem() 
			: base(Color.red)
		{
			
		}
	}
}