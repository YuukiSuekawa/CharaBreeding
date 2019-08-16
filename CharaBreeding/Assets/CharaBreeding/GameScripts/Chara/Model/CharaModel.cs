using System.Collections;
using System.Collections.Generic;
using CharaBreeding;
using UnityEngine;

public class CharaModel : MonoBehaviour
{
    private CharaMaster m_master;

    private UserCharaRecord m_charaRecord;
    public UserCharaRecord MCharaRecord => m_charaRecord;

    private const int foodAddSatiety = 30;
    
    public void SetCharaData(CharaMaster _master,UserCharaRecord _record)
    {
        m_master = _master;
        m_charaRecord = _record;
    }


    private bool isFood()
    {
        if(m_master.maxSatiety <= m_charaRecord.status.satiety)
        {
            // 満腹状態
            return false;
        }
        
        return true;
    }

    public bool ExeFood()
    {
        if (isFood())
        {
            m_charaRecord.status.satiety += foodAddSatiety;
            return true;
        }
        else
        {
            return false;
        }
    }
}
