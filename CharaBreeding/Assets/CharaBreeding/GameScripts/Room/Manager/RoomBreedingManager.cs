namespace CharaBreeding.GameScripts.Room
{
    public class RoomBreedingManager : RoomManagerBase
    {

        private RoomController m_controller;
        private BreedingSceneManager.OnRoomSave m_saveCallback;

        private RoomActionState m_status = RoomActionState.idle;
        
        public enum RoomActionState
        {
            none = 0,
            idle,
            poop,
            clean,
        }
        
        private void Awake()
        {
            m_controller = GetComponent<RoomController>();
        }

        public void SetRoomData(UserRoomRecord _record,BreedingSceneManager.OnRoomSave _saveCallback)
        {
            m_controller.SetRoomData(_record);
            m_saveCallback = _saveCallback;
        }

        public int GetRoomPoopNum()
        {
            return m_controller.GetRoomPoopNum();
        }

        public bool IsActionPossible()
        {
            return (m_status == RoomActionState.none ||
                    m_status == RoomActionState.idle);
        }

        public void CleanToiletReuest()
        {
            m_status = RoomActionState.clean;
            bool cleanFlg = m_controller.CleenToiletRequest(m_saveCallback, () =>
            {
                // endCallback
                m_status = RoomActionState.idle;
            });
            if(!cleanFlg) 
                m_status = RoomActionState.idle;
        }

        public bool CreatePoopRequest(int _poopNum)
        {
            m_status = RoomActionState.poop;            
            bool createFlg = m_controller.CreatePoopRequest(_poopNum,m_saveCallback, () =>
            {
                // endCallback
                m_status = RoomActionState.idle;
            });

            if (!createFlg)
                m_status = RoomActionState.idle;

            return createFlg;
        }
    }
}