using System.Runtime.InteropServices.ComTypes;
using CharaBreeding.GameScripts.Interface;
using UnityEngine;

namespace CharaBreeding.GameScripts
{
    public class GameMainManager : SingletonMonoBehaviour<GameMainManager>
    {

        private IMasterObj[] m_masterObject;
        
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

            m_masterObject = InterfaceUtils.FindObjectOfInterfaces<IMasterObj>();
        }

        private void Start()
        {
            if(SaveManager.Instance.SaveDataCheck())
            {
                // TODO タイトル表示
                
                // TODO 仮でBreedingInitする
                foreach (var masterObj in m_masterObject)
                {
                    if(masterObj is BreedingSceneManager sceneManager)
                        sceneManager.Init();
                }
            }
            else
            {
                Debug.LogError("saveData error.");
            }
        }

        private void Update()
        {
            foreach (var list in m_masterObject)
            {
                if (list is IUpdateByFrame update)
                {
                    update.UpdateByFrame();
                }
            }
        }
    }
}