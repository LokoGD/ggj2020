using System;
using System.Collections;
using UnityEngine;

namespace HalliHacks.Pulse
{
    /// <summary>
    /// A Timer class. The Timer class provides factory functions to allow developers
    /// to easily chain together calls to produce a full Timer instruction.
    /// </summary>
    public class PulseTimer
    {
        /// <summary>
        /// Delegate for the pulse task
        /// </summary>
        public delegate void PulseTask();

        /// <summary>
        /// The action to call on each tick
        /// </summary>
        private PulseTask TickTask;

        /// <summary>
        /// Delegate for the pulse validation callback
        /// </summary>
        /// <returns></returns>
        public delegate bool PulseValidator();

        /// <summary>
        /// The completion checker function
        /// </summary>
        private PulseValidator CancelChecker;

        /// <summary>
        /// The action to call when this Timer ends
        /// </summary>
        PulseTask completionCallback;

        /// <summary>
        /// The interval at which the Timer should tick
        /// </summary>
        private float? _interval;

        /// <summary>
        /// A costant representing an infinite number of repetitions
        /// </summary>
        public const int Forever = 0;

        /// <summary>
        /// The number of repetitions for this timer.
        /// </summary>
        private int repetitions = Forever;

        /// <summary>
        /// The number of repetitions which have been executed by this timer
        /// </summary>
        private int numReps = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="interval">The timer interval in seconds.</param>
        /// <param name="repetitions">The number of repetitions. If omitted, the timer will tick forever</param>
        internal PulseTimer(float? interval, int repetitions = Forever)
        {
            this._interval = interval;
            this.repetitions = repetitions;
        }

        /// <summary>
        /// Specify the action to perform on each tick
        /// </summary>
        /// <param name="something">The action to perform</param>
        /// <returns>This Timer instance</returns>
        public PulseTimer Do(PulseTask something)
        {
            if (TickTask == null)
            {
                TickTask = something;
                return this;
            }
            else
            {
                throw new PulseException("You cannot call Do() twice on the same Timer.");
            }
        }



        /// <summary>
        /// Specify a predicate function to check whether this timer should end
        /// </summary>
        /// <param name="func">The predicate function</param>
        /// <returns>This Timer instance</returns>
        public PulseTimer Until(PulseValidator func)
        {
            if (CancelChecker == null)
            {
                CancelChecker = func;
                return this;
            }
            else
            {
                throw new PulseException("A predicate function has already been applied.");
            }
        }

        /// <summary>
        /// Specify an action to call when this Timer ends
        /// </summary>
        /// <param name="callback">The action to call when this Timer ends</param>
        public void Then(PulseTask callback)
        {
            if (completionCallback == null)
            {
                completionCallback = callback;
            }
            else
            {
                throw new PulseException("You cannot supply more than one completion callback for a timer.");
            }
        }

        /// <summary>
        /// Determine whether or not this timer is completed.
        /// </summary>
        internal void CheckCompleted()
        {
            if (Done || (CancelChecker != null && CancelChecker.Invoke()))
            {
                End();
            }
        }

        /// <summary>
        /// End this timer and call the completion callback specified by Then
        /// </summary>
        internal void End()
        {
            Cancelled = true;

            if (completionCallback != null)
            {
                completionCallback.Invoke();
            }
        }

        /// <summary>
        /// Run this timer for the given number of repetitions.
        /// You should only call this method if you want the Timer to tick a specific number of times.
        /// </summary>
        /// <param name="repetitions">The number of repetitions. 0 signifies infinity.</param>
        /// <returns>This Timer instance</returns>
        public PulseTimer For(int repetitions)
        {
            if (repetitions >= Forever)
            {
                this.repetitions = repetitions;
                return this;
            }
            else
            {
                throw new PulseException("The number of repetitions must be 0 (Infinite) or more");
            }
        }

        /// <summary>
        /// Run this timer with the given tick interval.
        /// </summary>
        /// <param name="interval">The interval between ticks</param>
        /// <returns>This Timer instance</returns>
        public PulseTimer Every(float interval)
        {
            if (!_interval.HasValue)
            {
                if (interval > 0)
                {
                    this._interval = interval;
                    return this;
                }
                else
                {
                    throw new PulseException("The given interval must be greater than 0");
                }
            }
            else
            {
                throw new PulseException("You cannot call Every() once you have defined an interval");
            }
        }

        /// <summary>
        /// Invoke the tick action
        /// </summary>
        internal void DoSomething()
        {
            if (TickTask != null)
            {
                TickTask.Invoke();
            }
        }

        /// <summary>
        /// Determines whether this timer is capable of being executed
        /// </summary>
        /// <returns>True if this Timer can tick</returns>
        internal bool HasSomethingToDo()
        {
            return TickTask != null && _interval.HasValue && !Busy && !Done;
        }


        /// <summary>
        /// Whether this Timer is currently working. This will be True for most of the Timer's lifetime.
        /// </summary>
        private bool Busy = false;

        /// <summary>
        /// Whether this Timer has been cancelled. If this is set to True, the timer will end
        /// </summary>
        private bool Cancelled = false;

        /// <summary>
        /// Whether this Timer is completed. This will return True if the Timer has been cancelled,
        /// or if the Timer has ticker for the correct number of repetitions
        /// </summary>
        internal bool Done
        {
            get
            {
                return (Cancelled || repetitions != Forever && numReps == repetitions);
            }
        }

        /// <summary>
        /// Wait for the correct interval, and then execute the given callback
        /// </summary>
        /// <param name="callback">The action to call once the interval is up</param>
        /// <returns>Yield instruction</returns>
        public IEnumerator Wait(bool realtime, Action callback)
        {
            Busy = true;

            if (realtime)
            {
                yield return new WaitForSecondsRealtime(_interval.Value);
            }
            else
            {
                yield return new WaitForSeconds(_interval.Value);
            }
            callback.Invoke();
            if (repetitions > Forever)
            {
                numReps++;
            }
            CheckCompleted();

            Busy = false;
        }
    }
}
