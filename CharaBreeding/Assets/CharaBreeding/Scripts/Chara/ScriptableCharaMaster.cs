using System;
using UnityEngine;

namespace CharaBreeding.Scripts.Chara
{
    [Serializable]
    public class ScriptableCharaMaster : ScriptableObject
    {
        [SerializeField]
        private CharaMaster _master;
        public CharaMaster Master => _master;

        public void InitMaster(CharaMaster master)
        {
            _master = master;
        }
    }
}