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
        m_view.SetCharaData(_record);
    }

    #region UPDATE_STATUS
    public void UpdateStatusRequest(BreedingSceneManager.OnCharaSave _saveCallback)
    {
        if (m_model.UpdateStatus())
        {
            _saveCallback(m_model.MCharaRecord);
        }
    }
    #endregion UPDATE_STATUS

    
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
            StopFreeAnim();
            StartCoroutine(m_view.AnimDecline(_endCallback));
            return false;
        }
    }
    #endregion FOOD

    #region POOP
    public void ExePoopRequest(BreedingSceneManager.OnCharaSave _saveCallback,BreedingSceneManager.OnPoop _callback)
    {
        if (m_model.ExePoop(_callback))
        {
            _saveCallback(m_model.MCharaRecord);
        }
    }
    #endregion POOP
    
    #region SICK

    public void ExeSickRequest(BreedingSceneManager.OnCharaSave _saveCallback,int _poopNum)
    {
        if (m_model.ExeSick(_poopNum))
        {
            _saveCallback(m_model.MCharaRecord);
            m_view.SetCharaData(m_model.MCharaRecord);
        }
    }
    #endregion SICK
    
    #region FREE_ANIM
    public void UpdateFreeAnimRequest()
    {
        // 他のアニメ中は通さない
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
    #endregion FREE_ANIM
    
}
