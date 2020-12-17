using System;
using InventorySystem.Runtime.Scripts.Core.Models;
using Malee.List;

namespace InventorySystem.Samples.Scripts
{
	[Serializable]
	public class ItemsList : ReorderableArray<Item> { }
}