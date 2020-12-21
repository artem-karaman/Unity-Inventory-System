using System;

namespace InventorySystem.Runtime.Scripts.Core.Helpers
{
	public interface ICloneable<T> : ICloneable 
		where T : ICloneable<T>
	{
		new T Clone();
	}
}