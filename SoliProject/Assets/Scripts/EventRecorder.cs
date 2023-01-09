using Soli.Card;
using System.Collections.Generic;
using UnityEngine;
using Soli.Stack;

namespace Soli.Events
{
    public class EventRecorder : MonoBehaviour
    {
        private static EventRecorder instance;

        private Stack<Event> m_events = new Stack<Event>();
        private Event m_currentEvent = null;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        public static EventRecorder Instance { get { return instance; } }

        //Stack to stack
        public void CreateEvent(IStackable previousStack, IStackable newStack,CardWrapper currentCard)
        {
            m_currentEvent = new Event(previousStack, newStack, currentCard);
            m_events.Push(m_currentEvent);
        }

        public void RevertEvent()
        {
            if(m_events.Count <= 0) { return; }

            Event revertedEvent = m_events.Pop();
            revertedEvent.InitEvent();
        }

       
    }

}
