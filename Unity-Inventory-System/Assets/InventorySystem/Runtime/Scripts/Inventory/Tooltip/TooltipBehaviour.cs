using InventorySystem.Runtime.Scripts.Core.Models.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace InventorySystem.Runtime.Scripts.Inventory.Tooltip
{
	public class TooltipBehaviour : MonoBehaviour
	{
		private IItem _item;
		
		[Inject]
		void Construct(IItem item)
		{
			_item = item;
		}

		void Start()
		{
			gameObject.GetComponent<Image>().color = _item?.Color ?? Color.white;
		}
		
		
		public class Factory : PlaceholderFactory<IItem, TooltipBehaviour>{}
	}
}