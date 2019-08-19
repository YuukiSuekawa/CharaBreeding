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
            UserRoomRecord roomRecord = GetSelectUserRoomRecord();
            int selectedCharaId = roomRecord.charaId;

            foreach (var charaRecord in userChara)
            {
                if (charaRecord.charaId == selectedCharaId)
                    return charaRecord;
            }
            return null;
        }

        public UserRoomRecord GetSelectUserRoomRecord()
        {
            int selectedRoomId = userInfo.selectRoomId;
            foreach (var roomRecord in userRoom)
            {
                if (roomRecord.roomId == selectedRoomId)
                    return roomRecord;
            }

            return null;
        }
    }
}