using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

namespace HalliHacks.Pulse
{
    /// <summary>
    /// Pulse allows developers to rapidly create timed actions, which may
    /// repeat indefinitely, may execute after a certain amount of time, or may 
    /// repeat for a given number of times before ending.
    /// </summary>
    public class Pulse : MonoBehaviour
    {
        /// <summary>
        /// A private instance of the Pulse component
        /// </summary>
        private static Pulse _instance;

        /// <summary>
        /// Called during initialisation
        /// </summary>
        void Awake()
        {
            _instance = this;
        }

        /// <summary>
        /// Whether or not to use the realtime variant of WaitForSeconds internally.
        /// Setting this value to true will mean Pulse will use unscaled time
        /// </summary>
        public bool Realtime = true;


        /// <summary>
        /// The current list of timers
        /// </summary>
        List<PulseTimer> Timers = new List<PulseTimer>();

        /// <summary>
        /// Create a Timer that ticks regularly based on the given interval.
        /// The Timer returned by this instance will not execute until Timer.Do is called
        /// </summary>
        /// <param name="interval">The length of time, in seconds, between ticks</param>
        /// <returns>A new Timer instance</returns>
        public static PulseTimer Every(float interval)
        {
            if (_instance != null)
            {
                if (interval >= 0)
                {
                    PulseTimer t = new PulseTimer(interval);
                    _instance.Timers.Add(t);
                    return t;
                }
                else
                {
                    throw new PulseException("Interval must be greater than or equal to 0");
                }
            }
            else
            {
                throw new PulseException("No instance of Pulse could be found. Please make sure you have the Pulse component enabled in the scene.");
            }
        }

        /// <summary>
        /// Create a Timer that ticks for the given number of repetitions.
        /// The Timer returned by this instance will not execute until Timer.Do is called
        /// </summary>
        /// <param name="repetitions">The number of reptitions</param>
        /// <returns>A new Timer instance</returns>
        public static PulseTimer For(int repetitions)
        {
            if (_instance != null)
            {
                if (repetitions >= 0)
                {
                    PulseTimer t = new PulseTimer(null, repetitions);
                    _instance.Timers.Add(t);
                    return t;
                }
                else
                {
                    throw new PulseException("The number of repetitions must be greater than or equal to 0");
                }
            }
            else
            {
                throw new PulseException("No instance of Pulse could be found. Please make sure you have the Pulse component enabled in the scene.");
            }
        }

        /// <summary>
        /// Create a Timer that ticks until some condition is met.
        /// The Timer returned by this instance will not execute until Timer.Do is called
        /// </summary>
        /// <param name="predicate">A function returning True or False - where True signifies that the Timer should end</param>
        /// <returns>A new Timer instance</returns>
        public static PulseTimer Until(PulseTimer.PulseValidator predicate)
        {
            if (_instance != null)
            {
                PulseTimer t = new PulseTimer(null);
                if (predicate != null)
                {
                    t.Until(predicate);
                }
                _instance.Timers.Add(t);
                return t;
            }
            else
            {
                throw new PulseException("No instance of Pulse could be found. Please make sure you have the Pulse component enabled in the scene.");
            }
        }


        /// <summary>
        /// Called each frame
        /// </summary>
        void Update()
        {
            //Remove completed timers
            Timers.RemoveAll(x => x.Done);

            //Execute valid timers
            foreach (PulseTimer t in Timers.Where(x => x.HasSomethingToDo()))
            {
                StartCoroutine(t.Wait(Realtime, () =>
                {
                    t.DoSomething();
                }));
            }
        }
    }

    /// <summary>
    /// An exception signifying an error within Pulse
    /// </summary>
    public class PulseException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PulseException() : base() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">The error message</param>
        public PulseException(string message) : base(message) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="innerException">The inner exception which caused this error to occur</param>
        public PulseException(string message, Exception innerException) : base(message, innerException) { }
    }

}