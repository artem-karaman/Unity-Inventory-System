using System;
using InventorySystem.Runtime.Scripts.Core.Models;
using Malee.List;

namespace UnityInventorySystem
{
	[Serializable]
	public class ItemsList : ReorderableArray<Item> { }
}