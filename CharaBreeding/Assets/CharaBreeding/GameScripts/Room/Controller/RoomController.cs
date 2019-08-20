using CharaBreeding;
using CharaBreeding.GameScripts;
using UnityEngine;
using UnityEngine.Events;

public class RoomController : MonoBehaviour
{
    private RoomView m_view;
    private RoomModel m_model;

    private void Awake()
    {
        m_view = GetComponent<RoomView>();
        m_model = GetComponent<RoomModel>();
    }

    public void SetRoomData(UserRoomRecord _record)
    {
        m_model.SetRoomData(_record);
        if (_record.dirty > 0)
        {
            m_view.ExePoop(_record.dirty);
        }
    }

    public bool CleenToiletRequest(BreedingSceneManager.OnRoomSave _startCallback, UnityAction _endCallback)
    {
        if (m_model.ExeCleanToilet())
        {
            _startCallback(m_model.MRoomRecord);
            StartCoroutine(m_view.ExeCreanToilet(_endCallback));
            return true;
        }

        return false;
    }

    public bool CreatePoopRequest(int _poopNum,BreedingSceneManager.OnRoomSave _startCallback,UnityAction _endCallback)
    {
        if (m_model.ExeCreatePoop(_poopNum))
        {
            _startCallback(m_model.MRoomRecord);
            m_view.ExePoop(_poopNum);
            _endCallback();
            return true;
        }

        return false;
    }
}
