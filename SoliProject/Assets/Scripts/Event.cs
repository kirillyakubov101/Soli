using Soli.Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Soli.Stack;

namespace Soli.Events
{
    public class Event
    {
        public IStackable prevStack;
        public IStackable newStack;
        public CardWrapper currentCard;

        public Event(IStackable prevStack, IStackable newStack, CardWrapper currentCard)
        {
            this.prevStack = prevStack;
            this.newStack = newStack;
            this.currentCard = currentCard;
        }

        public void InitEvent()
        {
            prevStack.AddCardToPileFromEvent(currentCard);
            currentCard.ResetCardPosition();
        }
    }

}
