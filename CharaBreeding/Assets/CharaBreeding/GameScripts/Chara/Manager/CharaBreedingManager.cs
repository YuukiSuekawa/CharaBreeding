using System.Collections;
using System.Collections.Generic;
using CharaBreeding;
using CharaBreeding.GameScripts;
using CharaBreeding.GameScripts.Interface;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]
public class CharaBreedingManager : CharaManagerBase
{

    // TODO 役割
    // TODO Manger：キャラの内部的な状態の管理。アニメーション中であれば操作受け付けないとかはこいつに。
    // TODO Controller：ViewとModelへの通達。Modelを更新して、Viewを実行する。
    // TODO Model：ゲーム中の各コンテンツステータス保持場所。Viewはこの値を参照する。
    // TODO View：表示系。アニメーション命令はこいつに。

    private const float UPDATE_STATUS_SEC = 1.0f; // 必ず60以下で
    private float m_updatedStatusTime;
    private const float UPDATE_ANIM_SEC = 1.0f;
    private float m_updatedAnimTime;

    private CharaController m_controller;
    
    private CharaActionState m_status = CharaActionState.idle;

    private BreedingSceneManager.OnCharaSave m_saveCallback;

    private BreedingSceneManager.OnPoop m_poopCallback;

    private BreedingSceneManager.GetRoomPoopNum _m_getRoomPoop;
    
    public enum CharaActionState
    {
        none = 0,
        idle,
        walk,
        eat,
        play,
        sleep,
        toilet,
        sick
    }


    private void Awake()
    {
        m_controller = GetComponent<CharaController>();
    }

    public override void UpdateByFrame()
    {
        m_updatedStatusTime += Time.deltaTime;
        if (m_updatedStatusTime >= UPDATE_STATUS_SEC)
        {
            m_updatedStatusTime = 0;
            UpdateStatusRequest();
            ExePoopRequest();
            ExeSickRequest();
        }

        m_updatedAnimTime += Time.deltaTime;
        if (m_updatedAnimTime >= UPDATE_ANIM_SEC)
        {
            m_updatedAnimTime = 0;
            UpdateFreeAnimRequest();
        }
    }

    public void SetCharaData(CharaMaster _master,UserCharaRecord _record)
    {
        m_controller.SetCharaData(_master,_record);
        m_updatedStatusTime = Time.deltaTime;
        m_updatedAnimTime = Time.deltaTime;
    }

    public void SetCallback(BreedingSceneManager.OnCharaSave _saveCallback,BreedingSceneManager.OnPoop _poopCallback,BreedingSceneManager.GetRoomPoopNum _getRoomPoopCallback)
    {
        m_saveCallback = _saveCallback;
        m_poopCallback = _poopCallback;
        _m_getRoomPoop = _getRoomPoopCallback;
    }

    public bool IsActionPossible()
    {
        return (m_status == CharaActionState.idle ||
                m_status == CharaActionState.walk);
    }
    

    #region REQUEST
    
    private void UpdateStatusRequest()
    {
        m_controller.UpdateStatusRequest(m_saveCallback);
    }

    private void ExePoopRequest()
    {
        m_controller.ExePoopRequest(m_saveCallback,m_poopCallback);
    }

    private void ExeSickRequest()
    {
        m_controller.ExeSickRequest(m_saveCallback,_m_getRoomPoop());
    }
    
    public void EatFoodRequest()
    {
        if (!IsActionPossible()) return;

        bool success = m_controller.ExeFoodRequest(m_saveCallback,
            () =>
        {
            // endCallback
            Debug.Log("オワタ");
            m_status = CharaActionState.idle;
        });
        
        // status反映
        if(success) m_status = CharaActionState.eat;
    }

    private void UpdateFreeAnimRequest()
    {
        m_controller.UpdateFreeAnimRequest();
    }
    #endregion REQUEST
}
