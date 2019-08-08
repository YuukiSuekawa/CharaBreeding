using System;

namespace CharaBreeding.Scripts.Chara
{
    [Serializable]
    public class CharaMaster
    {
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