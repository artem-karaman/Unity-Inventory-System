using InventorySystem.Runtime.Scripts.Core.Models;
using InventorySystem.Samples.Scripts.Models.Interfaces;
using UnityEngine;

namespace InventorySystem.Samples.Scripts.Models
{
	public class BodyItem : Item, IBodyItem
	{
		public BodyItem()
			: base(Color.green)
		{
			
		}
	}
}