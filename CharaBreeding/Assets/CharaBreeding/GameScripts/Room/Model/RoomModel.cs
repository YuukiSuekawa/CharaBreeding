using System;
using System.Collections;
using System.Collections.Generic;
using CharaBreeding;
using UnityEditor.U2D;
using UnityEngine;

public class RoomModel : MonoBehaviour
{
    private UserRoomRecord m_roomRecord;
    public UserRoomRecord MRoomRecord => m_roomRecord;

    public void SetRoomData(UserRoomRecord _record)
    {
        m_roomRecord = _record;
    }

    private bool isCreatePoop()
    {
        return (Common.POOP_MAX > m_roomRecord.dirty);
    }
    
    public bool ExeCreatePoop(int _poopNum)
    {
        if (isCreatePoop())
        {
            m_roomRecord.dirty += _poopNum;
            if (Common.POOP_MAX < m_roomRecord.dirty)
                m_roomRecord.dirty = Common.POOP_MAX;
            return true;
        }
        return false;
    }

    private bool isCleanToilet()
    {
        return (m_roomRecord.dirty > 0);
    }

    public bool ExeCleanToilet()
    {
        if (isCleanToilet())
        {
            m_roomRecord.dirty = 0;
            return true;
        }

        return false;
    }
}
