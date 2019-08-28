using System.Collections;
using System.Collections.Generic;
using CharaBreeding.GameScripts;
using UnityEngine;

namespace CharaBreeding
{
    public class TitleSceneManager : SceneManagerBase
    {
        private UITitleManager m_uiMng;

        private void Awake()
        {
            base.Awake();
        }

        public override void Init(GameMainManager.ChangeSceneRequest _changeSceneCallback)
        {
            base.Init(_changeSceneCallback);
            SearchMng();
            SetUIEvent();
        }

        private void SearchMng()
        {
            foreach (var list in m_gameObject)
            {
                if (list is UITitleManager uiMng)
                    m_uiMng = uiMng;
            }
        }

        private void SetUIEvent()
        {
            m_uiMng.SetStartEvent(() =>
            {
                SceneChangeRequest("BreedingScene");
            });
        }
    }    
}

