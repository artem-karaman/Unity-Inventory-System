﻿using UnityEngine;

public class SharedUIManager : MonoBehaviour
{
	[SerializeField]
	private Canvas _canvas;

	[SerializeField]
	private GameObject _dragingItem;

	public Canvas Canvas => _canvas;
	public GameObject DragingItem => _dragingItem;

}
