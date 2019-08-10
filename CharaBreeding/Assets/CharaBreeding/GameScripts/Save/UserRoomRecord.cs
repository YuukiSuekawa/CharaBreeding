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
    }
}