using CharaBreeding;
using CharaBreeding.GameScripts;
using CharaBreeding.GameScripts.Interface;
using UnityEngine;
using UnityEngine.Events;

public class CharaController : MonoBehaviour
{
    private CharaView m_view;
    private CharaModel m_model;
    
    private const int SUB_SATIETY = 5;

    
    private void Awake()
    {
        // 同一階層前提
        m_view = GetComponent<CharaView>();
        m_model = GetComponent<CharaModel>();
    }


    public void SetCharaData(CharaMaster _master,UserCharaRecord _record)
    {
        m_model.SetCharaData(_master,_record);
    }

    #region FOOD
    public bool ExeFood(BreedingSceneManager.OnCharaSave _startCallback,UnityAction _endCallback)
    {
        Debug.Log("ExeEat");
        if (m_model.ExeFood())
        {
            _startCallback(m_model.MCharaRecord);
            // ビュー反映
            StartCoroutine(m_view.AnimFood(_endCallback));
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ExeSubFood()
    {
        
    }
    #endregion FOOD
}
