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

        public void SetPlayEvent(UnityAction _callback)
        {
            MenuButton button = SearchMenuButton(MenuButton.ButtonType.play);
            button.onClick.AddListener(_callback);            
        }
        
        public void SetToiletEvent(UnityAction _callback)
        {
            MenuButton button = SearchMenuButton(MenuButton.ButtonType.toilet);
            button.onClick.AddListener(_callback);
        }

        public void SetSleepEvent(UnityAction _callback)
        {
            MenuButton button = SearchMenuButton(MenuButton.ButtonType.sleep);
            button.onClick.AddListener(_callback);            
        }
        
        public void SetRoomChangeEvent(UnityAction _callback)
        {
            MenuButton button = SearchMenuButton(MenuButton.ButtonType.roomChange);
            button.onClick.AddListener(_callback);            
        }
        public void SetSickEvent(UnityAction _callback)
        {
            MenuButton button = SearchMenuButton(MenuButton.ButtonType.sick);
            button.onClick.AddListener(_callback);            
        }
        public void SetCallEvent(UnityAction _callback)
        {
            MenuButton button = SearchMenuButton(MenuButton.ButtonType.call);
            button.onClick.AddListener(_callback);            
        }
        public void SetDisciplineEvent(UnityAction _callback)
        {
            MenuButton button = SearchMenuButton(MenuButton.ButtonType.discipline);
            button.onClick.AddListener(_callback);            
        }

    }
}