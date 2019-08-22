using System;
using System.Collections;
using System.Collections.Generic;
using CharaBreeding;
using CharaBreeding.GameScripts;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class CharaModel : MonoBehaviour
{
    private CharaMaster m_master;

    private UserCharaRecord m_charaRecord;
    public UserCharaRecord MCharaRecord => m_charaRecord;
    
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
    private const int CHECK_SICK_SEC = 300 / UPDATE_SPEED;//21600 / UPDATE_SPEED;
    private const int SUB_SLEEPING_TOILET_SEC = 3600 / UPDATE_SPEED;
    private const int UPDATE_SUB_SLEEPING_TOILET = 10;
    private const int CHECK_TOILET_SEC = 2400 / UPDATE_SPEED;

    private const int FOOD_ADD_SATIETY = 30;
    private const int FOOD_ADD_HUMOR = 10;
    private const int FOOD_SUB_TOILET = 10;
    
    private const int POOP_SATIETY_BORDER = 50;
    private const int POOP_SUB_SATIETY = 15;
    private const int POOP_ADD_TOILET = 10;

    private const int SICK_POOP_MAG = 5;
    
    public void SetCharaData(CharaMaster _master,UserCharaRecord _record)
    {
        m_master = _master;
        m_charaRecord = _record;
    }

    private int CulcExeCount(string _oldDate,int _baseSec)
    {
        DateTime oldDate = DateTime.Parse(_oldDate);
        DateTime nowDate = DateTime.Now;
        double msec = (nowDate - oldDate).TotalMilliseconds;
        double exeCount = (msec / 1000) / _baseSec;
        return (int)Math.Floor(exeCount);

    }

    private bool IsFood()
    {
        if(m_master.maxSatiety <= m_charaRecord.status.satiety)
        {
            // 満腹状態
            return false;
        }else if (IsSick())
        {
            // 病気中
            return false;
        }
        
        return true;
    }

    public bool ExeFood()
    {
        if (IsFood())
        {
            m_charaRecord.status.satiety += FOOD_ADD_SATIETY;
            if (m_charaRecord.status.satiety > m_master.maxSatiety) m_charaRecord.status.satiety = m_master.maxSatiety;
            m_charaRecord.status.humor += FOOD_ADD_HUMOR;
            if (m_charaRecord.status.humor > m_master.maxHumor) m_charaRecord.status.humor = m_master.maxHumor;
            m_charaRecord.status.toilet -= FOOD_SUB_TOILET;
            if (m_charaRecord.status.toilet < 0) m_charaRecord.status.toilet = 0;
            m_charaRecord.time.satietyAdd = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            return true;
        }
        return false;
    }

    #region UPDATE_STATUS
    public bool UpdateStatus()
    {
        bool updateFlg;
        updateFlg = false;

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

    private int CheckSubSatiety()
    {
        if (m_charaRecord.status.satiety <= 0) return 0;
        return CulcExeCount(m_charaRecord.time.satietySub, SUB_SATIETY_SEC);
    }
    
    private bool ExeSubSatiety()
    {
        int exeCount = CheckSubSatiety();
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

    private int CheckSubSleepiness()
    {
        if (m_charaRecord.status.sleepiness <= 0) return 0;
        return CulcExeCount(m_charaRecord.time.sleepnessSub, SUB_SLEEPINESS_SEC);
    }

    private bool ExeSubSleepiness()
    {
        int exeCount = CheckSubSleepiness();
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

    private int CheckSubHumor()
    {
        if (m_charaRecord.status.humor <= 0) return 0;
        return CulcExeCount(m_charaRecord.time.humorSub, SUB_HUMOR_SEC);
    }

    private bool ExeSubHumor()
    {
        int exeCount = CheckSubHumor();
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

    private int CheckSubSleepingToilet()
    {
        if (m_charaRecord.status.toilet <= 0) return 0;
        return CulcExeCount(m_charaRecord.time.toiletSub, SUB_SLEEPING_TOILET_SEC);
    }

    private bool ExeSleepingToilet()
    {
        int exeCount = CheckSubSleepingToilet();
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
    #region CHECK_STATUS

    private int CheckPoopTiming()
    {
        if (m_charaRecord.status.toilet >= m_master.maxToilet) return 0;
        return CulcExeCount(m_charaRecord.time.toiletAdd, CHECK_TOILET_SEC);

    }

    public bool ExePoop(BreedingSceneManager.OnPoop _callback)
    {
        int exeCount = CheckPoopTiming();
        if (exeCount > 0)
        {
            Debug.Log("ExePoop");
            int poopSum = 0;
            for (int i = 0; i < exeCount; i++)
            {
                if (m_charaRecord.status.toilet >= m_master.maxToilet) break;
                
                int poopNum = (m_charaRecord.status.satiety > POOP_SATIETY_BORDER) ? 2 : 1;
                m_charaRecord.status.satiety -= POOP_SUB_SATIETY * poopNum;
                if (m_charaRecord.status.satiety < 0) m_charaRecord.status.satiety = 0;
                m_charaRecord.status.toilet += POOP_ADD_TOILET * poopNum;
                if (m_charaRecord.status.toilet > m_master.maxToilet) m_charaRecord.status.toilet = m_master.maxToilet;
                poopSum += poopNum;
            }
            _callback(poopSum);
            m_charaRecord.time.toiletAdd = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            return true;
        }

        return false;
    }

    public bool IsSick()
    {
        return m_charaRecord.status.sick;
    }

    private int CheckSickTiming()
    {
        return CulcExeCount(m_charaRecord.time.sickCheck, CHECK_SICK_SEC);
    }

    public bool ExeSick(int _roomPoopNum)
    {
        int exeCount = CheckSickTiming();
        if (exeCount > 0)
        {
            Debug.Log("ExeSick");
            int sickBase = m_master.sickParcent + (_roomPoopNum * SICK_POOP_MAG);
            int randNum = Random.Range(1, 100);
            Debug.Log("sickBase:" + sickBase + "  randNum :" + randNum);
            bool sick = (sickBase > randNum);
            if (sick) m_charaRecord.status.sick = true;
            m_charaRecord.time.sickCheck = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            return true;
        }
        return false;
    }

    
    public bool ExeInjection()
    {
        if (IsSick())
        {
            m_charaRecord.status.sick = false;
            return true;
        }
        return false;
    }
    
    
    #endregion CHECK_STATUS
}
