using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using CharaBreeding.GameScripts;
using CharaBreeding.Util;
using UnityEngine;

namespace CharaBreeding
{
    public enum SaveCategory
    {
        none = 0,
        all,
        userInfo,
        roomInfo,
        charaInfo
    }
    
    public class SaveManager : SingletonMonoBehaviour<SaveManager>
    {
        private string fileRootPath;
        [SerializeField] public SaveData save;
        private GameMainManager mainManager;

        private const string userSaveFile = "userSave.json";
        private const string roomSaveFile = "roomSave.json";
        private const string charaSaveFile = "charaSave.json";

        private void Awake()
        {
            if (this != Instance)
            {
                Destroy(gameObject);
                return;
            }
            
            DontDestroyOnLoad(gameObject);

            fileRootPath = Application.persistentDataPath + "/";
            save = new SaveData();
            save.userInfo = new UserInfoRecord();

            mainManager = GetComponent<GameMainManager>();

            if (File.Exists(fileRootPath + userSaveFile))
            {
                Debug.Log("Loadへ");
//                Load();
                Init();
            }
            else
            {
                Debug.Log("Initへ");
                Init();
            }


        }

        private void Init()
        {
            UserInfoRecord userInfo = new UserInfoRecord();
            userInfo.userId = 1; // TODO ローカルなので一旦固定
            save.userInfo = userInfo;
         
            UserCharaRecord charaInfo = new UserCharaRecord();
            charaInfo.userId = userInfo.userId;
            charaInfo.charaId = 0;
            charaInfo.status = new CharaStatus();
            Debug.Log(save);
            Debug.Log(charaInfo);
            save.userChara = new UserCharaRecord[2];
            save.userChara[0] = charaInfo;
            save.userChara[1] = charaInfo; // todo テスト
            
            UserRoomRecord roomInfo = new UserRoomRecord();
            roomInfo.userId = userInfo.userId;
            roomInfo.roomId = 0;
            roomInfo.charaId = charaInfo.charaId;
            roomInfo.dirty = 0;
            save.userRoom = new UserRoomRecord[1];
            save.userRoom[0] = roomInfo;

            Save(SaveCategory.all);
        }

        public void Save(SaveCategory _category)
        {
            save.userInfo.lastUpdate = DateTime.Now.ToString("yyyyMMddHHmmss");
            SaveLogic(SaveCategory.userInfo);
            switch (_category)
            {
                case SaveCategory.all:
                    SaveLogic(SaveCategory.charaInfo);
                    SaveLogic(SaveCategory.roomInfo);
                    break;
                case SaveCategory.charaInfo:
                    SaveLogic(_category);
                    break;
                case SaveCategory.roomInfo:
                    SaveLogic(_category);
                    break;
            }
        }

        private void SaveLogic(SaveCategory _category)
        {
            string json = "";
            string fileName = "";
            switch (_category)
            {
                case SaveCategory.userInfo:
                    Debug.Log("Json前Info " + save.userInfo);                    
                    json = JsonUtility.ToJson(save.userInfo);
                    fileName = userSaveFile;
                    Debug.Log("Json後 " + json);
                    break;
                case SaveCategory.roomInfo:
                    Debug.Log("Json前Room " + save.userRoom);
                    json = JsonHelper.ToJson(save.userRoom);
                    Debug.Log("Json後 " + json);
                    fileName = roomSaveFile;
                    break;
                case SaveCategory.charaInfo:
                    Debug.Log("Json前Chara " + save.userChara);
                    json = JsonHelper.ToJson(save.userChara);
                    Debug.Log("Json後 " + json);
                    fileName = charaSaveFile;
                    break;
                
            }
            StreamWriter streamWriter = new StreamWriter(fileRootPath + fileName);
            streamWriter.Write(json);
            streamWriter.Flush();
            streamWriter.Close();
        }

        public void Load()
        {
            // TODO 動作テストまだ
            UserInfoLoad();
            UserRoomLoad();
            UserCharaLoad();
        }

        private void UserInfoLoad()
        {
            if (File.Exists(fileRootPath + userSaveFile))
            {
                StreamReader streamReader = new StreamReader(fileRootPath + userSaveFile);
                string data = streamReader.ReadToEnd();
                streamReader.Close();
                Debug.Log("UserInfo " + data);
                save.userInfo = JsonUtility.FromJson<UserInfoRecord>(data);
            }
        }
        
        private void UserRoomLoad()
        {
            if (File.Exists(fileRootPath + roomSaveFile))
            {
                StreamReader streamReader = new StreamReader(fileRootPath + roomSaveFile);
                string data = streamReader.ReadToEnd();
                streamReader.Close();
                Debug.Log("UserRoom " + data);

                save.userRoom = JsonUtility.FromJson<UserRoomRecord[]>(data);
            }
        }
        
        private void UserCharaLoad()
        {
            if (File.Exists(fileRootPath + charaSaveFile))
            {
                StreamReader streamReader = new StreamReader(fileRootPath + charaSaveFile);
                string data = streamReader.ReadToEnd();
                streamReader.Close();
                Debug.Log("UserChara " + data);

                save.userChara = JsonUtility.FromJson<UserCharaRecord[]>(data);
            }
        }

        #if UNITY_EDITOR
        public void SaveDataCheck()
        {
            Debug.Log(save.userInfo);
            Debug.Log(save.userChara.Length);
            Debug.Log(save.userRoom.Length);
        }
        #endif
    }
}