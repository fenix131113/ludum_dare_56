using System;

namespace Interactive
{
	public interface IInteractableProgress
	{
		public float Progress { get; }
		
		public event Action OnProgressChanged;
	}
}