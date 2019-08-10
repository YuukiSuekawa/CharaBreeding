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
        
    }
}