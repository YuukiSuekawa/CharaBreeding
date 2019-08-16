using CharaBreeding.Util;
using UnityEngine;
using UnityEngine.Events;

namespace CharaBreeding.GameScripts.UI
{
    public class UIBreedingManager : UIManagerBase
    {
        [SerializeField] private MenuButton[] m_buttons;


        private MenuButton SearchMenuButton(MenuButton.ButtonType _type)
        {
            foreach (var button in m_buttons)
            {
                if (button.GetButtonType() == _type) return button;
            }

            return null;
        }

        public void SetFoodEvent(UnityAction _callback)
        {
            MenuButton button = SearchMenuButton(MenuButton.ButtonType.food);
            button.onClick.AddListener(_callback);
        }

        

    }
}