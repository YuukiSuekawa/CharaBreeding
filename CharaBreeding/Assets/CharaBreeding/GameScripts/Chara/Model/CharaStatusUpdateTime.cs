﻿using System;

namespace CharaBreeding
{
    [Serializable]
    public class CharaStatusUpdateTime
    {
        public string satietyAdd;
        public string satietySub;
        public string sleepnessAdd;
        public string sleepnessSub;
        public string humorAdd;
        public string humorSub;
        public string sickCheck;
        public string toiletAdd;
        public string toiletSub;

        public void Init()
        {
            string now = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            satietyAdd = now;
            satietySub = now;
            sleepnessAdd = now;
            sleepnessSub = now;
            humorAdd = now;
            humorSub = now;
            sickCheck = now;
            toiletAdd = now;
            toiletSub = now;            
        }
    }
}