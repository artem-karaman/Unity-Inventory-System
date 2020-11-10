using UnityEngine;
using UnityEngine.UI;

namespace UnityInventorySystem.Managers
{
	public class MainSceneUIManager : MonoBehaviour
	{
		[Tooltip("Это поле для ClearInventoryButton")] [SerializeField]
		private Button _clearInventoryButton;

		public Button ClearInventoryButton => _clearInventoryButton;
	}
}