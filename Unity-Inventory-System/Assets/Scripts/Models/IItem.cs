using UnityEngine;

namespace UnityInventorySystem
{
	public interface IItem
	{
		string Title { get; }
		string Description { get; }
		Sprite ItemSprite { get; }
		Color Color { get;  }
		int MaxStack { get; }
	}
}