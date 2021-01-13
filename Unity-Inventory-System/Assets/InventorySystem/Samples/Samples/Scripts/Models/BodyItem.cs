using InventorySystem.Runtime.Scripts.Core.Models;
using InventorySystem.Samples.Scripts.Models.Interfaces;
using UnityEngine;

namespace Samples.Scripts.Models
{
	public class BodyItem : Item, IBodyItem
	{
		public BodyItem()
			: base(Color.green)
		{
			
		}
		
		public BodyItem(string title, string description)
			: base(title, description, Color.green){}
	}
}