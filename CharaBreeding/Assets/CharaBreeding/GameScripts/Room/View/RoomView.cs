using System;
using System.Collections;
using System.Collections.Generic;
using CharaBreeding;
using UnityEngine;
using UnityEngine.Events;

public class RoomView : MonoBehaviour
{
    [SerializeField] private GameObject m_poopRoot;
    [SerializeField] private GameObject m_poopPrefab;
    [SerializeField] private GameObject m_toiletPrefab;

    private List<GameObject> m_poops = new List<GameObject>();

    public void ExePoop(int _poopNum)
    {
        int poopsNum = m_poops.Count;

        if (poopsNum >= Common.POOP_MAX) return;

        int exePoopNum = _poopNum;
        if ((poopsNum + _poopNum)  >= Common.POOP_MAX)
            exePoopNum = Common.POOP_MAX - poopsNum;
            
        CreatePoop(exePoopNum);
    }

    private void CreatePoop(int _poopNum)
    {
        int poopListNum = m_poops.Count;
        for (int i = poopListNum; i < poopListNum + _poopNum; i++)
        {
            GameObject poopObj = Instantiate(m_poopPrefab);
            m_poops.Add(poopObj);
            poopObj.transform.SetParent(m_poopRoot.transform);
            poopObj.transform.localScale = new Vector3(4,4);
                
            float posY = 0f;
            float posX = 0f;
            float posDefault = 100f;
            if (i < (Common.POOP_MAX / 2))
            {
                posY = 0f;
                bool oddNumFlg = ((i % 2) != 0);
                int positionRate = (int)Math.Ceiling(i / 2f);
                posX = posDefault * positionRate;
                if (oddNumFlg) posX = -posX;
            }
            else
            {
                posY = -100f;
                int tmpCnt = i - 5;
                bool oddNumFlg = ((tmpCnt % 2) != 0);
                int positionRate = (int)Math.Ceiling(tmpCnt / 2f);
                posX = posDefault * positionRate;
                if (oddNumFlg) posX = -posX;
            }
                
            poopObj.transform.localPosition = new Vector3(posX,posY);
        }
    }

    public IEnumerator ExeCreanToilet(UnityAction _callback)
    {
        GameObject toiletObj = Instantiate(m_toiletPrefab);
        toiletObj.transform.SetParent(m_poopRoot.transform);
        toiletObj.transform.localPosition = new Vector3(0, 150);
        Animator toiletAnim = toiletObj.GetComponent<Animator>();
        AnimatorStateInfo animStateInfo = toiletAnim.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(animStateInfo.length / 2);
        foreach (var poop in m_poops)
        {
            Destroy(poop);
        }
        m_poops = new List<GameObject>();
        yield return new WaitForSeconds(animStateInfo.length / 2);
        Destroy(toiletObj);
        _callback();
    }
}
