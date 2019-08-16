using UnityEngine;
using UnityEngine.UI;

namespace CharaBreeding.Util
{
    public class MenuButton : Button
    {
        public enum ButtonType
        {
            none = 0,
            roomChange,
            sick,
            call,
            discipline,
            food,
            play,
            toilet,
            sleep
        }

        [SerializeField] private ButtonType buttonType = ButtonType.none;

        public ButtonType GetButtonType()
        {
            return buttonType;
        }
    }
}