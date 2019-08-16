using System;

namespace CharaBreeding
{
    [Serializable]
    public class UserRoomRecord
    {
        public int userId;
        public int roomId;
        public int charaId;
        public int dirty;

        private const int INIT_MIN_VALUE = 0;
        
        public void Init(int _userId,int _roomId,int _charaId)
        {
            userId = _userId;
            roomId = _roomId;
            charaId = _charaId;
            dirty = INIT_MIN_VALUE;
        }
    }
}