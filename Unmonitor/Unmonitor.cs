using System;
using System.Threading;

namespace Unmonitor
{

	/// <summary>
	/// Represents a scope that calls monitor.exit 
	/// on a given object when it's created, and monitor.enter
	/// on the object when it's disposed.
	/// 
	/// Used when scopes are undefined well and call
	/// to a certain method should not be inside a lock.
	/// </summary>
	public class Unmonitor : IDisposable
	{

		/// <summary>
		/// Creates an instance of <see cref="Unmonitor"/>
		/// </summary>
		/// <param name='syncObject'>
		/// The sync object to unlock within the block.
		/// </param>
		public static IDisposable Create(object syncObject)
		{
			return new Unmonitor(syncObject);
		}

		private readonly object _syncObject;

		private Unmonitor(object syncObject)
		{
			_syncObject = syncObject;

			Monitor.Exit(_syncObject);
		}

		#region IDisposable implementation

		public void Dispose ()
		{
			Monitor.Enter(_syncObject);
		}

		#endregion
	}
}

