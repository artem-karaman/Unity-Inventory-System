using System;
using UnityEngine;

namespace InventorySystem.Runtime.Scripts.Core.Models.Interfaces
{
	public interface IItemFacade : IDisposable
	{
		Transform Transform { get; }
		
		IItem Item { get; }
	}
}