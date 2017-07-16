using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farms : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        m_addUnagiCount = 0;
        m_currntUnagiNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        m_addUnagiCount += Time.deltaTime;
        if (m_addUnagiCount > m_addUnagiTime)
        {
            m_currntUnagiNum += 30;
            GameParame.I.Unagi += m_currntUnagiNum;
            m_addUnagiCount = 0;
        }
    }

    private float m_addUnagiCount;
    private int m_currntUnagiNum;
    private const float m_addUnagiTime = 5.0f;
}
