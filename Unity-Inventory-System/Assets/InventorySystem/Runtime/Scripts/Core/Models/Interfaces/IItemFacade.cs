using System;
using UnityEngine;

namespace UnityInventorySystem
{
	public interface IItemFacade : IDisposable
	{
		Transform Transform { get; }
		
		IItem Item { get; }
	}
}