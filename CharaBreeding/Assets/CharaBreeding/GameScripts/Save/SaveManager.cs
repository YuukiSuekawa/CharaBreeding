using System;
using System.IO;
using CharaBreeding.GameScripts;
using CharaBreeding.GameScripts.Interface;
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
    
    public class SaveManager : SingletonMonoBehaviour<SaveManager>,IMasterObj,IUpdateByFrame
    {
        private string fileRootPath;
        public SaveData save;
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
                Load();
            }
            else
            {
                Init();
            }


        }

        private void Init()
        {
            int initUserId = 1;
            int initCharaId = 0;
            int initRoomId = 0;
            
            UserInfoRecord userInfo = new UserInfoRecord();
            userInfo.Init(initUserId,initRoomId);
            save.userInfo = userInfo;
         
            UserCharaRecord charaInfo = new UserCharaRecord();
            charaInfo.Init(userInfo.userId,initCharaId);
            save.userChara = new UserCharaRecord[1]; // TODO この配列数どうするか・・・後でResizeかます？
            save.userChara[0] = charaInfo;
            
            UserRoomRecord roomInfo = new UserRoomRecord();
            roomInfo.Init(initUserId,initRoomId,initCharaId);
            save.userRoom = new UserRoomRecord[1]; // TODO この配列数どうするか・・・後でResizeかます？
            save.userRoom[0] = roomInfo;

            Save(SaveCategory.all);
        }

        public void UpdateByFrame()
        {
            
        }

        public void Save(SaveCategory _category)
        {
            save.userInfo.lastUpdate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
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
                    json = JsonUtility.ToJson(save.userInfo);
                    fileName = userSaveFile;
                    break;
                case SaveCategory.roomInfo:
                    json = JsonHelper.ToJson(save.userRoom);
                    fileName = roomSaveFile;
                    break;
                case SaveCategory.charaInfo:
                    json = JsonHelper.ToJson(save.userChara);
                    fileName = charaSaveFile;
                    break;
                
            }
            StreamWriter streamWriter = new StreamWriter(fileRootPath + fileName);
            Debug.Log("saveFilePath:" + fileRootPath + fileName);
            streamWriter.Write(json);
            streamWriter.Flush();
            streamWriter.Close();
        }

        public void Load()
        {
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
                save.userRoom = JsonHelper.FromJson<UserRoomRecord>(data);
            }
        }
        
        private void UserCharaLoad()
        {
            if (File.Exists(fileRootPath + charaSaveFile))
            {
                StreamReader streamReader = new StreamReader(fileRootPath + charaSaveFile);
                string data = streamReader.ReadToEnd();
                streamReader.Close();
                save.userChara = JsonHelper.FromJson<UserCharaRecord>(data);
            }
        }

        public bool SaveDataCheck()
        {
#if UNITY_EDITOR
            Debug.Log("save file check");
            Debug.Log("filePath " + fileRootPath);
            if(save.userInfo != null)
                Debug.Log("userInfo " + save.userInfo);
            if(save.userChara != null)
                Debug.Log("userChara len " + save.userChara.Length);
            if(save.userRoom != null)
                Debug.Log("userRoom len " + save.userRoom.Length);
#endif
            return (save.userInfo != null &&
                save.userRoom != null &&
                save.userChara != null);
        }
    }
}