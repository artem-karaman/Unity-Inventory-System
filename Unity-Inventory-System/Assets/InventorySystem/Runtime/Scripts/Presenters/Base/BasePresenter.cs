using UniRx;
using Zenject;

namespace InventorySystem.Runtime.Scripts.Presenters.Base
{
	public class BasePresenter : ILateDisposable
	{
		protected readonly CompositeDisposable Disposables = new CompositeDisposable();
		
		public void LateDispose()
		{
			Disposables.Dispose();
		}
	}
}