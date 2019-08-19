﻿using System.Collections;
using System.Collections.Generic;
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
    }

    public bool CreatePoopRequest(int _poopNum,BreedingSceneManager.OnRoomSave _startCallback,UnityAction _endCallback)
    {
        if (m_model.ExeCreatePoop(_poopNum))
        {
            _startCallback(m_model.MRoomRecord);
            m_view.ExePoop(_poopNum);
            return true;
        }

        return false;
    }
}
