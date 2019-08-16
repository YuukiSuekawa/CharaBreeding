using System;
using UnityEngine;

namespace CharaBreeding
{
    [Serializable]
    public class CharaStatus
    {
        public int satiety;
        public int sleepiness;
        public int humor;
        public bool sick;
        public int toilet;

        private const int INIT_MAX_VALUE = 100;
        private const int INIT_MIN_VALUE = 0;
        
        public void Init()
        {
            satiety = INIT_MAX_VALUE;
            sleepiness = INIT_MIN_VALUE;
            humor = INIT_MAX_VALUE;
            sick = false;
            toilet = INIT_MIN_VALUE;
        }
    }
}