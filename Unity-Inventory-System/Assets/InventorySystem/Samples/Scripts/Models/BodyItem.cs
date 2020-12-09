using InventorySystem.Runtime.Scripts.Core.Models;
using UnityEngine;

namespace UnityInventorySystem
{
	public class BodyItem : Item, IBodyItem
	{
		public BodyItem()
			: base(Color.green)
		{
			
		}
	}
}