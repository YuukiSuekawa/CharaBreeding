using UnityEngine;

namespace CharaBreeding.GameScripts
{
    public class GameMainManager : SingletonMonoBehaviour<GameMainManager>
    {

        private void Start()
        {
            Debug.Log("saveMan " + SaveManager.Instance);
            SaveManager.Instance.SaveDataCheck();
        }
    }
}