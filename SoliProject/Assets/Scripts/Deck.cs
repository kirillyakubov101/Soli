using Soli.Card;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Soli.Utils
{
    public class Deck : MonoBehaviour
    {
        [SerializeField] private Queue<CardWrapper> m_cards = new Queue<CardWrapper>();
        [SerializeField] private Transform m_cardShowcase;
        [SerializeField] private CardWrapper m_showcasedCard = null;


        private static Deck instance;
        public static event Action OnDeckEmpty;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
        }

        public static Deck Instance()
        {
            return instance;
        }

        public void ClearLastCardRef()
        {
            m_cards.Dequeue();
            this.m_showcasedCard = null;

            if(m_cards.Count == 0)
            {
                OnDeckEmpty?.Invoke();
            }
        }

        public async Task CreateDeck()
        {
            for (int i = 0; i < 24; i++)
            {
                GameObject newCardGameObject = CardFactory.Instance().TakeOneCard();
                CardWrapper newCard = newCardGameObject.GetComponent<CardWrapper>();
                newCard.IsFromDeck = true;
                m_cards.Enqueue(newCard);
              

                newCard.PutCardFaceDown();
                newCard.transform.parent = transform;
                newCard.transform.position = transform.position;
            }
           

            await Task.Delay(100);

        } 

        public void TakeCardFromDeck()
        {
            if(m_cards.Count == 0) { return; }
            if(m_showcasedCard != null)
            {
                m_showcasedCard.transform.localPosition = Vector3.zero;
                m_showcasedCard.PutCardFaceDown();
                m_showcasedCard = null;
                CardWrapper lastShownCard = m_cards.Dequeue();
                m_cards.Enqueue(lastShownCard); //put the card back in the queue
            }

            CardWrapper cardToShow =  m_cards.Peek();
            AnimationHandler.Instance().AssignAnimationObject(cardToShow, m_cardShowcase.transform.position); //assign item to animate

            m_showcasedCard = cardToShow; //cache preview card

        }

        public bool IsDeckEmpty()
        {
            return m_cards.Count == 0;
        }
    }

}
