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
    private float m_updatedTime;

    private CharaController m_controller;
    
    private CharaActionState m_status = CharaActionState.idle;

    private BreedingSceneManager.OnCharaSave m_saveCallback;
    
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
        m_updatedTime += Time.deltaTime;
        if (m_updatedTime >= UPDATE_STATUS_SEC)
        {
            m_updatedTime = 0;
            UpdateStatusRequest(m_saveCallback);
        }
    }

    public void SetCharaData(CharaMaster _master,UserCharaRecord _record,BreedingSceneManager.OnCharaSave _saveCallback)
    {
        m_controller.SetCharaData(_master,_record);
        m_saveCallback = _saveCallback;
        m_updatedTime = Time.deltaTime;
    }

    public bool IsAction()
    {
        Debug.Log("state : " + m_status);
        return (m_status == CharaActionState.idle ||
                m_status == CharaActionState.walk);
    }
    

    #region REQUEST
    
    private void UpdateStatusRequest(BreedingSceneManager.OnCharaSave _saveCallback)
    {
        Debug.Log("UpdateStatus");
        m_controller.UpdateStatusRequest(_saveCallback);
    }
    
    public void EatFoodRequest()
    {
        Debug.Log("EatFood");

        if (!IsAction()) return;

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
    #endregion REQUEST
}
