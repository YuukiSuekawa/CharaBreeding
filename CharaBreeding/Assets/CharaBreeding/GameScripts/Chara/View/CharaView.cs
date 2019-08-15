using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharaView : MonoBehaviour
{
    static int AnimatorWalk = Animator.StringToHash("Walk");
    static int AnimatorAttack = Animator.StringToHash("Attack");
    Animator m_animator;
    private Vector3 initPos;
    private GameObject charaObject;

    void Awake()
    {
        m_animator = GetComponentInChildren<Animator>();
        charaObject = GetComponent<Transform>().GetChild(0).gameObject;
        initPos = charaObject.GetComponent<Transform>().localPosition;
    }
    
    public IEnumerator AnimEat(UnityAction _callback)
    {
        // todo コールバックで返したい
        // todo 位置を指定の位置に持っていきたいところ
        
        
        m_animator.SetTrigger(AnimatorAttack);
        yield return new WaitForSeconds(1f);
        m_animator.SetTrigger(AnimatorAttack);
        yield return new WaitForSeconds(1f);
        m_animator.SetTrigger(AnimatorAttack);
        yield return new WaitForSeconds(1f);
        _callback();
    }
}
