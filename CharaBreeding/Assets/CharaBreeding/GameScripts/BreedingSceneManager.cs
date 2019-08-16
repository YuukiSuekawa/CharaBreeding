﻿using System.Collections;
using System.Linq;
using System.Runtime.InteropServices;
using CharaBreeding.GameScripts.Room;
using CharaBreeding.GameScripts.UI;
using UnityEngine;
using UnityEngine.Networking;

namespace CharaBreeding.GameScripts
{
    // TODO 役割
    // TODO シーンの総括管理。
    // TODO 各マネージャクラスにセーブデータやマスターデータを振り分ける。
    // TODO 各マネージャクラスらを紐付ける。
    public class BreedingSceneManager : SceneManagerBase
    {
        private CharaBreedingManager m_charaMng;
        private RoomManagerBase m_roomMng;
        private UIBreedingManager m_uiMng;

        private ScriptableCharaMaster[] m_charaMasters;

        private enum SceneStatus
        {
            none = 0,
            masterLoad,
            saveLoad,
            init,
            playGame,
            end
        }

        
        protected void Awake()
        {
            base.Awake();
            LoadMaster();
            LoadSaveData();
        }

        void LoadMaster()
        {
            m_charaMasters = Resources.LoadAll("Master/Chara/").Cast<ScriptableCharaMaster>().ToArray();
            // TODO Roomも作ったらここでマスタロード
        }

        void LoadSaveData()
        {
            if (SaveManager.Instance.SaveDataCheck())
            {
                
            }
            else
            {
                Debug.LogError("SaveData error.");
            }
        }

        public void Init()
        {
            Debug.Log("SceneManager Init");
            SearchMng();
            SetUIEvent();
            SetCharaData();

        }

        private void SearchMng()
        {
            foreach (var list in m_gameObject)
            {
                if (list is CharaBreedingManager charaMng)
                    m_charaMng = charaMng;

                if (list is RoomManagerBase roomMng)
                    m_roomMng = roomMng;

                if (list is UIBreedingManager uiMng)
                    m_uiMng = uiMng;
            }
            
            #if DEBUG
            Debug.Log("Seach Mng chara:" + m_charaMng);
            Debug.Log("Seach Mng room:" + m_roomMng);
            Debug.Log("Seach Mng ui:" + m_uiMng);
            #endif
        }

        private void SetUIEvent()
        {
            m_uiMng.SetFoodEvent(m_charaMng.EatFoodRequest);
        }

        public delegate void OnCharaSave(UserCharaRecord _record);

        private void SetCharaData()
        {
            UserCharaRecord selectedCharaRecord = SaveManager.Instance.save.GetSelectUserCharaRecord();
            CharaMaster charaMaster = SearchCharaMaster(selectedCharaRecord.charaId);
            OnCharaSave callback = (_record) =>
            {
                CharaSave(_record);
            };
            m_charaMng.SetCharaData(charaMaster,selectedCharaRecord,callback);
        }

        private CharaMaster SearchCharaMaster(int _charaId)
        {
            foreach (var master in m_charaMasters)
            {
                if (master.Master.charaId == _charaId)
                {
                    return master.Master;
                }
            }

            return null;
        }

        private void CharaSave(UserCharaRecord _record)
        {
            int i = 0;
            foreach (var charaRecord in SaveManager.Instance.save.userChara)
            {
                if (charaRecord.charaId == _record.charaId)
                {
                    break;
                }
                i++;
            }
            SaveManager.Instance.save.userChara[i] = _record;
            SaveManager.Instance.Save(SaveCategory.charaInfo);
        }
    }
}