namespace CharaBreeding.GameScripts.Room
{
    public class RoomBreedingManager : RoomManagerBase
    {

        private RoomController m_controller;
        private BreedingSceneManager.OnRoomSave m_saveCallback;

        private void Awake()
        {
            m_controller = GetComponent<RoomController>();
        }

        public void SetRoomData(UserRoomRecord _record,BreedingSceneManager.OnRoomSave _saveCallback)
        {
            m_controller.SetRoomData(_record);
            m_saveCallback = _saveCallback;
        }

        public void CleanToiletReuest()
        {
            
        }

        public bool CreatePoopRequest(int _poopNum)
        {
            return m_controller.CreatePoopRequest(_poopNum,m_saveCallback, () =>
            {
                
            });
        }
    }
}