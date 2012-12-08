using System;
using System.Threading;

namespace Unmonitor.Example
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			object lockObject = new object();
			ManualResetEvent threadStartedEvent = new ManualResetEvent(false);

			Console.WriteLine("#1 : Waiting for lock");

			lock (lockObject)
			{
				Console.WriteLine("#1 : Entered lock, Starting thread #2");

				ThreadPool.QueueUserWorkItem(@null => {

					threadStartedEvent.Set();
					Console.WriteLine("#2 : Waiting for lock");
					lock (lockObject)
					{
						Console.WriteLine("#2 : Entered lock");
					}

				});

				threadStartedEvent.WaitOne();

				Console.WriteLine("#1 : Exiting lock");

				using (Unmonitor.Create(lockObject))
				{
					Console.WriteLine("#1 : Exited lock");
				}

				Console.WriteLine("#1 : Back to lock");

			}
		}
	}
}
