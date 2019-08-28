using System.Collections;
using System.Runtime.InteropServices.ComTypes;
using CharaBreeding.GameScripts.Interface;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CharaBreeding.GameScripts
{
    public class GameMainManager : SingletonMonoBehaviour<GameMainManager>
    {

        private IMasterObj[] m_masterObject;
        private AsyncOperation async;
        
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
            GetMasterObject();
        }

        private void GetMasterObject()
        {
            m_masterObject = InterfaceUtils.FindObjectOfInterfaces<IMasterObj>();
        }

        private void Start()
        {
            if(SaveManager.Instance.SaveDataCheck())
            {
                SceneStart();
            }
            else
            {
                Debug.LogError("saveData error.");
            }
        }

        private void SceneStart()
        {
            foreach (var masterObj in m_masterObject)
            {
                if (masterObj is SceneManagerBase sceneManager)
                {
                    sceneManager.Init(ChangeScene);
                    sceneManager.FadeIn();
                }
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

        public delegate void ChangeSceneRequest(string _sceneName);
       
        // todo シーン遷移によって破棄されるもの & 新規で取得
        private void ChangeScene(string _sceneName)
        {
            SceneManagerBase sceneMng = null;
            foreach (var masterObj in m_masterObject)
            {
                if (masterObj is SceneManagerBase sceneManager)
                {
                    sceneMng = sceneManager;
                    sceneManager.FadeOut();
                    StartCoroutine(LoadScene(_sceneName));
                    break;
                }
            }
        }

        private IEnumerator LoadScene(string _sceneName)
        {
            async = SceneManager.LoadSceneAsync(_sceneName);
            while (!async.isDone)
            {
                yield return null;
            }
            
            // シーンロード完了後、マスタオブジェクト更新
            GetMasterObject();
            
            // fadein & 初期化
            SceneStart();
        }
    }
}