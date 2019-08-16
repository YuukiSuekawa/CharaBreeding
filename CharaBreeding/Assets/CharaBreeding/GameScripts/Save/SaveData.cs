using System;
using System.Collections.Generic;
using UnityEngine;

namespace CharaBreeding
{
    [Serializable]
    public class SaveData
    {
        public UserInfoRecord userInfo;
        public UserRoomRecord[] userRoom;
        public UserCharaRecord[] userChara;
        
        public UserCharaRecord GetSelectUserCharaRecord()
        {
            int selectedRoomId = userInfo.selectRoomId;
            int selectedCharaId = -1;
            foreach (var roomRecord in userRoom)
            {
                if (roomRecord.roomId == selectedRoomId)
                    selectedCharaId = roomRecord.charaId;
            }

            foreach (var charaRecord in userChara)
            {
                if (charaRecord.charaId == selectedCharaId)
                    return charaRecord;
            }
            return null;
        }
    }
}