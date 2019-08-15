using CharaBreeding.GameScripts.Room;
using CharaBreeding.GameScripts.UI;
using UnityEngine;

namespace CharaBreeding.GameScripts
{
    public class BreedingSceneManager : SceneManagerBase
    {
        private CharaBreedingManager m_charaMng;
        private RoomManagerBase m_roomMng;
        private UIBreedingManager m_uiMng;

        protected void Awake()
        {
            base.Awake();
            Init();

        }

        private void Init()
        {
            SearchMng();
            SetUIEvent();
        }

        private void SearchMng()
        {
            foreach (var list in m_gameObject)
            {
                if (list is CharaBreedingManager charaMng)
                    m_charaMng = charaMng;

                if (list is RoomManagerBase roomMng)
                    m_roomMng = roomMng;

                if (list is UIBreedingManager uiMng)
                    m_uiMng = uiMng;
            }
            
            #if DEBUG
            Debug.Log("Seach Mng chara:" + m_charaMng);
            Debug.Log("Seach Mng room:" + m_roomMng);
            Debug.Log("Seach Mng ui:" + m_uiMng);
            #endif
            // TODO これ見る前にBreedingの継承クラスちゃんと作ってあげんといかん
        }

        private void SetUIEvent()
        {
            m_uiMng.SetEatEvent(m_charaMng.EatFood);

        }
    }
}