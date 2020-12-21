using UnityEngine;

namespace InventorySystem.Runtime.Scripts.Core.Models.Interfaces
{
	public interface IItem
	{
		string Title { get; }
		string Description { get;  }
		Sprite ItemSprite { get;  }
		Color Color { get; }
		int MaxStack { get; }
		int Count { get; }
	}
}