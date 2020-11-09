using UnityEngine;

public class UIManager : MonoBehaviour
{
	[Tooltip("Это поле для Canvas")] 
	[SerializeField]
	private Canvas _canvas;
	public Canvas Canvas => _canvas;

	[Tooltip("Это поле для DragingItem")] [SerializeField]
	private GameObject _dragingItem;

	public GameObject DragingItem => _dragingItem;

}
