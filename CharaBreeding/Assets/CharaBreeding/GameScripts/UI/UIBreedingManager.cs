using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

namespace CharaBreeding.GameScripts.UI
{
    public class UIBreedingManager : UIManagerBase
    {
        [SerializeField] private Button button;

        private UnityAction m_eat;

        public void SetEatEvent(UnityAction _eat)
        {
            m_eat = _eat;
            button.onClick.AddListener(m_eat);
        }

        

    }
}