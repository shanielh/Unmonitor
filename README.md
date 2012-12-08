Unmonitor
=========

A simple way to exit a monitor lock within a block.

# Usage

  lock(lockObject) 
  {
      // I'm within the lock..
      
      lock (Unmonitor.Create(lockObject))
      {
          // I'm outside the lock..
      }
  
      // I'm back to lock!
  }