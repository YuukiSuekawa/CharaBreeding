using System;

namespace CharaBreeding
{
    [Serializable]
    public class CharaMaster
    {
        public int charaId;
        public string name;
        public string imgName;
        public string assetName;
        public string category;
        public int speed;
        public int maxSatiety;
        public int maxSleepiness;
        public int maxHumor;
        public int sickParcent;
        public int maxToilet;
    }
}