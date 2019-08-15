using System.Collections;
using CharaBreeding.GameScripts.Interface;
using UnityEngine;

namespace CharaBreeding
{
    public class CharaManagerBase : MonoBehaviour,IGameObj,IUpdateByFrame
    {
        private CharaMaster _master;
        public CharaMaster Master { get; }

        private CharaStatus _status;
        public CharaStatus Status { get; }

        public virtual void Init(CharaMaster master)
        {
            // TODO ステータスもホシイ。
            // TODO masterのcharaIdからユーザデータをロードさせたい
            
            // TODO セーブデータは一旦JSON(後でサーバに移したい)
            
            // TODO ユーザデータからの検索
            // TODO なければ新規作成
            // TODO あればデータ反映
            StartCoroutine(Load(master.charaId));
        }

        protected virtual IEnumerator Load(int charaId)
        {
            
            
            yield return null;
        }

        public virtual void UpdateByFrame()
        {
//            Debug.Log("UpdateByFrame : " + GetType().Name);
        }
    }
}