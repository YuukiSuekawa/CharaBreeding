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

    private Coroutine freeAnim;
    
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
    public bool ExeFoodRequest(BreedingSceneManager.OnCharaSave _startCallback,UnityAction _endCallback)
    {
        if (m_model.ExeFood())
        {
            _startCallback(m_model.MCharaRecord);
            // ビュー反映
            StopFreeAnim();
            StartCoroutine(m_view.AnimFood(_endCallback));
            return true;
        }
        else
        {
            // TODO できればいやがる動作をさせたい
            return false;
        }
    }
    #endregion FOOD

    public void UpdateStatusRequest(BreedingSceneManager.OnCharaSave _saveCallback)
    {
        if (m_model.UpdateStatus())
        {
            _saveCallback(m_model.MCharaRecord);
        }
    }

    public void UpdateFreeAnimRequest()
    {
        if (freeAnim != null) return;
        if (m_view.NowAnimFlg) return;
        int randNum = Random.Range(0, 10);
        if (randNum > 5)
        {
            freeAnim = StartCoroutine(m_view.AnimWalk(() => { freeAnim = null; }));
        }
    }

    private void StopFreeAnim()
    {
        if (freeAnim != null) StopCoroutine(freeAnim);
        freeAnim = null;
    }
    
}
