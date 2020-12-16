using InventorySystem.Runtime.Scripts.Core.Models;
using InventorySystem.Samples.Scripts.Models.Interfaces;
using UnityEngine;

namespace InventorySystem.Samples.Scripts.Models
{
	public class CardItem : Item, ICardItem
	{
		public CardItem()
			: base(Color.yellow)
		{
			
		}
	}
}