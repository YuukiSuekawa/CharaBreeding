using System.Collections;
using System.Collections.Generic;
using CharaBreeding;
using UnityEngine;
using UnityEngine.Events;

public class CharaView : MonoBehaviour
{
    [SerializeField] private GameObject m_FoodRoot;
    [SerializeField] private GameObject m_FoodPrefab;
    [SerializeField] private GameObject m_SickPrefab;
    [SerializeField] private GameObject m_SleepingPrefab;
    
    static int AnimatorWalk = Animator.StringToHash("Walk");
    static int AnimatorAttack = Animator.StringToHash("Attack");

    Animator m_animator;
    private Vector3 m_initPos;
    private GameObject m_charaObject;
    private GameObject m_sickObject;
    private GameObject m_sleepingObject;

    private bool nowAnimFlg = false;

    public bool NowAnimFlg => nowAnimFlg;

    void Awake()
    {
        m_animator = GetComponentInChildren<Animator>();
        m_charaObject = GetComponent<Transform>().GetChild(0).gameObject;
        m_initPos = m_charaObject.GetComponent<Transform>().localPosition;
    }

    public IEnumerator AnimWalk(UnityAction _callback)
    {
        Vector3 startPos = m_charaObject.transform.localPosition;
        RectTransform trans = gameObject.GetComponent<RectTransform>();

        int moveX = 10;
        int moveCountMax = 10;
        
        m_animator.SetBool(AnimatorWalk,true);
        AnimatorStateInfo animStateInfo = m_animator.GetCurrentAnimatorStateInfo(0);
        int randNum = Random.Range(0, 10);
        bool rightTrg = false;

        if (randNum > 5)
        {
            m_charaObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            rightTrg = true;
        }
        else
        {
            m_charaObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        
        
        if ((trans.rect.width/2) < (startPos.x + (moveX * moveCountMax)))
        {
            // 強制左
            m_charaObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            rightTrg = false;
        }
        else if (-(trans.rect.width/2) > (startPos.x - (moveX * moveCountMax)))
        {
            // 強制右
            m_charaObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            rightTrg = true;
        }
        
        for (int i = 0; i < moveCountMax; i++)
        {
            if (rightTrg)
            {
                startPos.x += moveX;
            }
            else
            {
                startPos.x -= moveX;
            }
            m_charaObject.transform.localPosition = startPos;
            yield return new WaitForSeconds(animStateInfo.length / moveCountMax);
        }
        m_animator.SetBool(AnimatorWalk,false);
        _callback();
    }
    
    public IEnumerator AnimFood(UnityAction _callback)
    {
        
        int AnimatorFood01 = Animator.StringToHash("Eat01");
        int AnimatorFood02 = Animator.StringToHash("Eat02");
        int AnimatorFood03 = Animator.StringToHash("Eat03");

        GameObject foodObj = Instantiate(m_FoodPrefab);
        foodObj.transform.SetParent(m_FoodRoot.transform);
        foodObj.transform.localPosition = Vector3.zero;
        foodObj.transform.localScale = Vector3.one;

        Animator foodAnimator = foodObj.GetComponent<Animator>();
        
        nowAnimFlg = true;
        m_charaObject.transform.localPosition = m_initPos;
        m_charaObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        foodAnimator.SetTrigger(AnimatorFood01);
        m_animator.SetTrigger(AnimatorAttack);
        yield return new WaitForSeconds(1.5f);
        foodAnimator.SetTrigger(AnimatorFood02);
        m_animator.SetTrigger(AnimatorAttack);
        yield return new WaitForSeconds(1.5f);
        foodAnimator.SetTrigger(AnimatorFood03);
        m_animator.SetTrigger(AnimatorAttack);
        yield return new WaitForSeconds(1.5f);
        Destroy(foodObj);
        nowAnimFlg = false;
        _callback();
    }

    /***
     * 嫌がり
     */
    public IEnumerator AnimDecline(UnityAction _callback)
    {
        nowAnimFlg = true;
        float time = 0.5f;
        int rotateCount = 3;
        m_charaObject.transform.localPosition = m_initPos;        
        for (int i = 0; i < rotateCount; i++)
        {
            m_charaObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            yield return new WaitForSeconds(time);
            m_charaObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            yield return new WaitForSeconds(time);            
        }
        nowAnimFlg = false;
        _callback();
    }

    public void SetCharaData(UserCharaRecord _record)
    {
        SetSick(_record.status.sick);
        SetSleep(_record.status.sleeping);
    }

    private void SetSick(bool _sick)
    {
        if (_sick)
        {
            if (m_sickObject == null)
            {
                m_sickObject = Instantiate(m_SickPrefab);
                m_sickObject.transform.SetParent(m_charaObject.transform);
                m_sickObject.transform.localPosition = new Vector3(0,Common.CHARA_SIZE);
                m_sickObject.transform.localScale = Vector3.one;
            }
        }
        else
        {
            if (m_sickObject != null)
            {
                Destroy(m_sickObject);
                m_sickObject = null;
            }
        }
    }

    private void SetSleep(bool _sleeping)
    {
        if (_sleeping)
        {
            if (m_sleepingObject == null)
            {
                m_sleepingObject = Instantiate(m_SleepingPrefab);
                m_sleepingObject.transform.SetParent(m_charaObject.transform);
                m_sleepingObject.transform.localPosition = new Vector3(Common.CHARA_SIZE,Common.CHARA_SIZE);
                m_sleepingObject.transform.localScale = Vector3.one;
            }
        }
        else
        {
            if (m_sleepingObject != null)
            {
                Destroy(m_sleepingObject);
                m_sleepingObject = null;
            }
        }
    }
}
