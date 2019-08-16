using System;
using System.Collections;
using System.Collections.Generic;
using CharaBreeding;
using UnityEngine;

public class CharaModel : MonoBehaviour
{
    private CharaMaster m_master;

    private UserCharaRecord m_charaRecord;
    public UserCharaRecord MCharaRecord => m_charaRecord;

    private const int FOOD_ADD_SATIETY = 30;
    private const int FOOD_ADD_HUMOR = 10;

    private Common.CharaState state;
    
    #if SUEKAWA
    private const int UPDATE_SPEED = 20;
    #else
    private const int UPDATE_SPEED = 1;    
    #endif
    
    private const int SUB_SATIETY_SEC = 300 / UPDATE_SPEED;
    private const int UPDATE_SUB_SATIETY = 10;
    private const int SUB_SLEEPINESS_SEC = 300 / UPDATE_SPEED;
    private const int UPDATE_SUB_SLEEPINESS = 1;
    private const int SUB_HUMOR_SEC = 600 / UPDATE_SPEED;
    private const int UPDATE_SUB_HUMOR = 3;
    private const int CHECK_SICK_SEC = 21600 / UPDATE_SPEED;
    private const int SUB_SLEEPING_TOILET_SEC = 3600 / UPDATE_SPEED;
    private const int UPDATE_SUB_SLEEPING_TOILET = 10;
    private const int CHECK_TOILET_SEC = 2400 / UPDATE_SPEED;
    
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
            m_charaRecord.status.satiety += FOOD_ADD_SATIETY;
            m_charaRecord.status.humor += FOOD_ADD_HUMOR;
            m_charaRecord.time.satietyAdd = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            return true;
        }
        return false;
    }

    #region UPDATE_STATUS
    public bool UpdateStatus()
    {
        bool updateFlg = false;

        // TODO 部屋の状態を引数でもらってきたほうがいいかも

        if (ExeSubSatiety())
        {
            updateFlg = true;
        }

        if (ExeSubSleepiness())
        {
            updateFlg = true;
        }

        if (ExeSubHumor())
        {
            updateFlg = true;
        }

        if (ExeSleepingToilet())
        {
            updateFlg = true;
        }
        
        // TODO 病気チェックとうんこチェックが必要
        
        return updateFlg;
    }

    private int IsSubSatiety()
    {
        if (m_charaRecord.status.satiety <= 0) return 0;
        
        DateTime oldDate = DateTime.Parse(m_charaRecord.time.satietySub);
        DateTime nowDate = DateTime.Now;
        double msec = (nowDate - oldDate).TotalMilliseconds;
        double subSec = (msec / 1000) / SUB_SATIETY_SEC;
        return (int)Math.Floor(subSec);
    }
    
    private bool ExeSubSatiety()
    {
        int exeCount = IsSubSatiety();
        if (exeCount > 0)
        {
            Debug.Log("ExeSubSatiety");
            m_charaRecord.status.satiety -= UPDATE_SUB_SATIETY * exeCount;
            if (m_charaRecord.status.satiety <= 0)
                m_charaRecord.status.satiety = 0;
            m_charaRecord.time.satietySub = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            return true;
        }
        return false;
    }

    private int IsSubSleepiness()
    {
        if (m_charaRecord.status.sleepiness <= 0) return 0;
        
        DateTime oldDate = DateTime.Parse(m_charaRecord.time.sleepnessSub);
        DateTime nowDate = DateTime.Now;
        double msec = (nowDate - oldDate).TotalMilliseconds;
        double subSec = (msec / 1000) / SUB_SLEEPINESS_SEC;
        return (int)Math.Floor(subSec);
    }

    private bool ExeSubSleepiness()
    {
        int exeCount = IsSubSleepiness();
        if (exeCount > 0)
        {
            Debug.Log("ExeSubSleepiness");
            m_charaRecord.status.sleepiness -= UPDATE_SUB_SLEEPINESS * exeCount;
            if (m_charaRecord.status.sleepiness <= 0)
                m_charaRecord.status.sleepiness = 0;
            m_charaRecord.time.sleepnessSub = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            return true;
        }
        return false;
    }

    private int IsSubHumor()
    {
        if (m_charaRecord.status.humor <= 0) return 0;
        
        DateTime oldDate = DateTime.Parse(m_charaRecord.time.humorSub);
        DateTime nowDate = DateTime.Now;
        double msec = (nowDate - oldDate).TotalMilliseconds;
        double subSec = (msec / 1000) / SUB_HUMOR_SEC;
        return (int)Math.Floor(subSec);
    }

    private bool ExeSubHumor()
    {
        int exeCount = IsSubHumor();
        if (exeCount > 0)
        {
            Debug.Log("ExeSubHumor");
            m_charaRecord.status.humor -= UPDATE_SUB_HUMOR * exeCount;
            if (m_charaRecord.status.humor <= 0)
                m_charaRecord.status.humor = 0;
            m_charaRecord.time.humorSub = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            return true;
        }
        return false;
    }

    private int IsSubSleepingToilet()
    {
        if (m_charaRecord.status.toilet <= 0) return 0;
        
        DateTime oldDate = DateTime.Parse(m_charaRecord.time.toiletSub);
        DateTime nowDate = DateTime.Now;
        double msec = (nowDate - oldDate).TotalMilliseconds;
        double subSec = (msec / 1000) / SUB_SLEEPING_TOILET_SEC;
        return (int)Math.Floor(subSec);
    }

    private bool ExeSleepingToilet()
    {
        int exeCount = IsSubSleepingToilet();
        if (exeCount > 0)
        {
            Debug.Log("ExeSleepingToilet");
            m_charaRecord.status.toilet -= UPDATE_SUB_SLEEPING_TOILET * exeCount;
            if (m_charaRecord.status.toilet <= 0)
                m_charaRecord.status.toilet = 0;
            m_charaRecord.time.toiletSub = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            return true;
        }
        return false;
    }
    
    #endregion UPDATE_STATUS
}
