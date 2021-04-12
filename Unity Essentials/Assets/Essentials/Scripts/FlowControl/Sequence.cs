using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace UnityEngine
{
    [Serializable]
    public class Sequence
    {
        [SerializeField] public bool randomizeOrder;
        private RandomEssentials random = new RandomEssentials();
        [SerializeField] public UnityEvent[] events;
        [NonSerialized] public UnityEvent nextEvent;
        [NonSerialized] public int nextEventIndex = 0;

        public Sequence(UnityAction[] actions, bool randomizeOrder = false, int randomizationSeed = -1) 
        {
            List<UnityEvent> events = new List<UnityEvent>();
           
            foreach (UnityAction unityAction in actions)
            {
                UnityEvent unityEvent = new UnityEvent();
                unityEvent.AddListener(unityAction);
                events.Add(unityEvent);
            }
            
            this.events = events.ToArray();
            this.randomizeOrder = randomizeOrder;
            
            if (randomizationSeed == -1)
                random = new RandomEssentials();
            else
                random = new RandomEssentials(randomizationSeed);
        }
        
        public Sequence(UnityEvent[] events, bool randomizeOrder = false, int randomizationSeed = -1)
        {
            this.events = events;
            this.randomizeOrder = randomizeOrder;
            
            if (randomizationSeed == -1)
                random = new RandomEssentials();
            else
                random = new RandomEssentials(randomizationSeed);
        }

        public void Invoke()
        {
            if (randomizeOrder && random == null)
                random = new RandomEssentials();
            
            if (nextEvent == null || nextEvent.GetPersistentEventCount() <= 0)
            {
                if (!randomizeOrder)
                {
                    nextEvent = events[0];
                    nextEventIndex = nextEventIndex.GetLooped(events.Length);
                }
                else
                {
                    nextEventIndex = random.GetRandomInt(events.Length);
                    nextEvent = events[nextEventIndex];
                }
            }

            nextEvent?.Invoke();
            
            if (!randomizeOrder)
                nextEventIndex = nextEventIndex.GetLooped(events.Length);
            else
                nextEventIndex = random.GetRandomInt(events.Length);
            
            nextEvent = events[nextEventIndex];
        }
        
    }
    
}