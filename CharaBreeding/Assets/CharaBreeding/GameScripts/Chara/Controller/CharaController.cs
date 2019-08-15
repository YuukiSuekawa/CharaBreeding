using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharaController : MonoBehaviour
{
    private CharaView m_view;
    private CharaModel m_model;
    
    private void Awake()
    {
        // 同一階層前提
        m_view = GetComponent<CharaView>();
        m_model = GetComponent<CharaModel>();
    }

    public void ExeFood(UnityAction _callback)
    {
        Debug.Log("ExeEat");
        // TODO モデルにデータ反映
        
        // TODO ビュー反映
        StartCoroutine(m_view.AnimFood(_callback));
    }
}
