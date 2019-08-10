using UnityEngine;

namespace CharaBreeding.GameScripts
{
    public class GameMainManager : SingletonMonoBehaviour<GameMainManager>
    {

        public enum GameMainState
        {
            none = 0,
            title,
            breeding
        }
        
        private void Awake()
        {
            if (this != Instance)
            {
                Destroy(gameObject);
                return;
            }
            
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            if(SaveManager.Instance.SaveDataCheck())
            {
                // TODO タイトル表示
                
            }
            else
            {
                Debug.LogError("saveData error.");
            }
        }
        
        
    }
}