using System.Collections;
using System.Collections.Generic;
using CharaBreeding.GameScripts.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CharaBreeding
{
    public class UITitleManager : UIManagerBase
    {
        [SerializeField] private Button m_button; // todo 分類するならば別途クラス作成

        public void SetStartEvent(UnityAction _callback)
        {
            m_button.onClick.AddListener(_callback);
        }
    }
}

