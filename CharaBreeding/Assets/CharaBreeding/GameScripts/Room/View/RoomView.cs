using System;
using System.Collections.Generic;
using CharaBreeding;
using UnityEngine;

public class RoomView : MonoBehaviour
{
    [SerializeField] private GameObject m_poopRoot;
    [SerializeField] private GameObject m_poopPrefab;

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

    private void CreanPoop()
    {
            
    }
}
