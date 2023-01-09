using Soli.Stack;
using Soli.Utils;
using UnityEngine;

namespace Soli.Border
{
    public class BorderControl : MonoBehaviour
    {
        [SerializeField] private GameManager m_gameManager;

        private CardStack[] m_workStacks;
        private CardStack largestStack = null;

        private void OnEnable()
        {
            CardStack.OnCardModified += UpdateDictionary;
        }

        private void OnDestroy()
        {
            CardStack.OnCardModified -= UpdateDictionary;
        }

        private void Start()
        {
            m_workStacks = m_gameManager.GetCardsStacks();
        }

        private void UpdateDictionary(CardStack cardStack)
        {
            int maxCards = -1;
            bool shouldUpdate = false;

            foreach(var ele in m_workStacks)
            {
                if(ele.GetCardsCount() > maxCards)
                {
                    largestStack = ele;
                    maxCards = ele.GetCardsCount();
                    shouldUpdate = true;
                }
            }
            if(shouldUpdate)
            {
                UpdateBorderPos(largestStack.LastCardPos());
            }
            
        }

        private void UpdateBorderPos(Vector3 newPos)
        {
           transform.position = newPos;
        }
    }

}
