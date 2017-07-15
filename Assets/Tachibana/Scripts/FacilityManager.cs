using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilityManager : MonoBehaviour
{
    [SerializeField]
    int MapSize = 5;

    [SerializeField]
    GameObject m_facilityPrefab;

    Facility[][] m_facility;

	void Start ()
    {
        m_facility = new Facility[MapSize][];
        for(int i=0;i<  m_facility.Length; i++)
        {
            m_facility[i] = new Facility[MapSize];
        }
        CreateMap();
	}

    void CreateMap()
    {
        Vector2 vec = new Vector2(3,30);
        vec.Normalize();
        float dis = 50;

        Vector2 offset = new Vector2(-100,-100);

        for(int i=0;i<m_facility.Length;i++)
        {
            for(int j= 0;j< m_facility[i].Length;j++)
            {
                m_facility[i][j] = GameObject.Instantiate(m_facilityPrefab).GetComponent<Facility>();
                m_facility[i][j].transform.SetParent(transform);
                m_facility[i][j].GetRectTransform();
                Vector2 v = vec*i*dis;
                m_facility[i][j].rectTransform.anchoredPosition = offset+ v +new Vector2(vec.y,0- vec.x) * j*dis;
            }
        }
    }
	
}
