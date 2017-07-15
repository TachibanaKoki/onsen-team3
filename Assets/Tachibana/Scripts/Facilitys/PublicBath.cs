using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicBath : FacilityTypeInterface
{
    public int SalesPow = 10;
    public int SalseHelth = 1;

    bool isStarting = true;
    
	// Update is called once per frame
	public void Update ()
    {
		if(isStarting)
        {
            Sales();
        }
	}

    public IEnumerator Sales()
    {
        WaitForSeconds wait = new WaitForSeconds(Mathf.Max(0.1f,SalseHelth));

        while(true)
        {
            if (!isStarting) break;
            GameParame.I.Money += 10;
            yield return wait;
        }
    }
}
