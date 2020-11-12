using UnityEngine;
using UnityEngine.UI;

namespace UnityInventorySystem.Managers
{
	public class MainSceneUIManager : MonoBehaviour
	{
		[Tooltip("Это поле для HandItemsButton")]
		[SerializeField]
		private Button _handItemsButton;

		[Tooltip("Это поле для BodyItemsButton")] 
		[SerializeField]
		private Button _bodyItemsButton;

		[Tooltip("Это поле для LegItemsButton")] 
		[SerializeField]
		private Button _legItemsButton;

		[Tooltip("Это поле для CardItemsButton")] 
		[SerializeField]
		private Button _cardItemsButton;

		[Tooltip("Это поле для OtherItemsButton")] 
		[SerializeField]
		private Button _otherItemsButton;

		[Tooltip("Это поле для SeparateItems")] [SerializeField]
		private Button separateItemsButton;

		[Tooltip("Это поле для DeleteSelectedItem")] [SerializeField]
		private Button deleteSelectedItemButton;

		public Button HandItemsButton => _handItemsButton;

		public Button BodyItemsButton => _bodyItemsButton;

		public Button LegItemsButton => _legItemsButton;

		public Button CardItemsButton => _cardItemsButton;

		public Button OtherItemsButton => _otherItemsButton;

		public Button SeparateItemsButton => separateItemsButton;

		public Button DeleteSelectedItemButton => deleteSelectedItemButton;


		
	}
}