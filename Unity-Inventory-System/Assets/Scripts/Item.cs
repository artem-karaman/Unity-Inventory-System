using System;
using UnityEngine;

namespace UnityInventorySystem
{
	[Serializable]
	public class Item
	{
		[SerializeField] private string _title;

		public string Title => _title;

		[Tooltip("Это поле для Description")]
		[SerializeField]
		[TextArea]
		private string _description;

		public string Description => _description;

	}
}