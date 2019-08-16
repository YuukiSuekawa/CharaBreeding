using System;

namespace CharaBreeding
{
    [Serializable]
    public class UserCharaRecord
    {
        public int userId;
        public int charaId;
        public CharaStatus status;
        public CharaStatusUpdateTime time;

        public void Init(int _userId,int _charaId)
        {
            userId = _userId;
            charaId = _charaId;
            if(status == null) status = new CharaStatus();
            status.Init();
            if (time == null) time = new CharaStatusUpdateTime();
            time.Init();
        }
    }
}