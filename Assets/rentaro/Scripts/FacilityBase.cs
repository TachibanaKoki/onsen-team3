using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilityBase
{
    public Facility facility;
	// Use this for initialization
	public virtual void Start (Facility fac) {
        facility = fac;
            }
	
	// Update is called once per frame
	public virtual void Update () {
		
	}
}
