using System;

namespace CharaBreeding
{
    [Serializable]
    public class UserInfoRecord
    {
        public int userId;
        public int selectRoomId;
        public string lastUpdate;

        public void Init(int _userId,int _selectRoomId)
        {
            userId = _userId;
            selectRoomId = _selectRoomId;
            lastUpdate = DateTime.Now.ToString("yyyyMMddHHmmss");
        }
    }
}