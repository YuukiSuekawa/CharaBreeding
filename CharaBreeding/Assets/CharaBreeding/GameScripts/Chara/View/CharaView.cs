using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharaView : MonoBehaviour
{
    [SerializeField] private GameObject m_FoodRoot;
    [SerializeField] private GameObject m_FoodPrefab;
    
    static int AnimatorWalk = Animator.StringToHash("Walk");
    static int AnimatorAttack = Animator.StringToHash("Attack");

    Animator m_animator;
    private Vector3 initPos;
    private GameObject charaObject;

    private bool nowAnimFlg = false;

    public bool NowAnimFlg => nowAnimFlg;

    void Awake()
    {
        m_animator = GetComponentInChildren<Animator>();
        charaObject = GetComponent<Transform>().GetChild(0).gameObject;
        initPos = charaObject.GetComponent<Transform>().localPosition;
    }

    public IEnumerator AnimWalk(UnityAction _callback)
    {
        Vector3 startPos = charaObject.transform.localPosition;
        RectTransform trans = gameObject.GetComponent<RectTransform>();

        int moveX = 10;
        int moveCountMax = 10;
        
        m_animator.SetBool(AnimatorWalk,true);
        AnimatorStateInfo animStateInfo = m_animator.GetCurrentAnimatorStateInfo(0);
        int randNum = Random.Range(0, 10);
        bool rightTrg = false;

        if (randNum > 5)
        {
            charaObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            rightTrg = true;
        }
        else
        {
            charaObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        
        
        if ((trans.rect.width/2) < (startPos.x + (moveX * moveCountMax)))
        {
            // 強制左
            charaObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            rightTrg = false;
        }
        else if (-(trans.rect.width/2) > (startPos.x - (moveX * moveCountMax)))
        {
            // 強制右
            charaObject.transform.rotation = Quaternion.Euler(0, 180, 0);
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
            charaObject.transform.localPosition = startPos;
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

        Animator foodAnimator = foodObj.GetComponent<Animator>();
        
        nowAnimFlg = true;
        charaObject.transform.localPosition = initPos;
        charaObject.transform.rotation = Quaternion.Euler(0, 0, 0);
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
        charaObject.transform.localPosition = initPos;
        for (int i = 0; i < rotateCount; i++)
        {
            charaObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            yield return new WaitForSeconds(time);
            charaObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            yield return new WaitForSeconds(time);            
        }
        nowAnimFlg = false;
        _callback();
    }
}
