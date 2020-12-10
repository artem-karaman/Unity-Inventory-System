using UnityEngine;

namespace InventorySystem.Runtime.Scripts.Managers
{
	public class SharedUIManager : MonoBehaviour
	{
		#pragma warning disable 0649
		[SerializeField]
		private Canvas _canvas;

		[SerializeField]
		private GameObject _dragingItem;
		#pragma warning restore 0649

		public Canvas Canvas => _canvas;
		public GameObject DragingItem => _dragingItem;

	}
}


