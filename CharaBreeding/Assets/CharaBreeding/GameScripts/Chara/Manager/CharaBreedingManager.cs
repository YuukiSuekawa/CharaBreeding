using System.Collections;
using System.Collections.Generic;
using CharaBreeding;
using CharaBreeding.GameScripts.Interface;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CharaBreedingManager : CharaManagerBase
{

    // TODO 役割
    // TODO Manger：キャラの内部的な状態の管理。アニメーション中であれば操作受け付けないとかはこいつに。
    // TODO Controller：ViewとModelへの通達。Modelを更新して、Viewを実行する。
    // TODO Model：ゲーム中の各コンテンツステータス保持場所。Viewはこの値を参照する。
    // TODO View：表示系。アニメーション命令はこいつに。

    private CharaController m_controller;
    
    private CharaStatus m_status = CharaStatus.idle;
    
    private enum CharaStatus
    {
        idle = 0,
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

    // TODO まずはご飯

    public void EatFood()
    {
        m_status = CharaStatus.eat;
        m_controller.ExeEat(() =>
        {
            Debug.Log("オワタ");
            m_status = CharaStatus.idle;
        });
        
    }

    public bool isAction()
    {
        return (m_status == CharaStatus.idle ||
                m_status == CharaStatus.walk);
    }

}
