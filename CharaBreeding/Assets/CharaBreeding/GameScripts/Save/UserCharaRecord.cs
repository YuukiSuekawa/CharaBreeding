using System;

namespace CharaBreeding
{
    [Serializable]
    public class UserCharaRecord
    {
        public int userId;
        public int charaId;
        public CharaStatus status;
    }
}