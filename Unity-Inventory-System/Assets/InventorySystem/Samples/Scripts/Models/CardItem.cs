using InventorySystem.Runtime.Scripts.Core.Models;
using UnityEngine;

namespace UnityInventorySystem
{
	public class CardItem : Item, ICardItem
	{
		public CardItem()
			: base(Color.yellow)
		{
			
		}
	}
}