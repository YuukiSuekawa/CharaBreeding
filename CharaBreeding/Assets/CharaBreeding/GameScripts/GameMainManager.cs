using UnityEngine;

namespace CharaBreeding.GameScripts
{
    public class GameMainManager : SingletonMonoBehaviour<GameMainManager>
    {

        private void Start()
        {
            Debug.Log("saveMan " + SaveManager.Instance);
            #if UNITY_EDITOR
            SaveManager.Instance.SaveDataCheck();
            #endif
        }
    }
}