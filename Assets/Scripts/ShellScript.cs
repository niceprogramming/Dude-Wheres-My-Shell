using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellScript : MonoBehaviour
{
	public int Upgrades = 1;
	public bool DontCollide = false;
	
	// Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if(DontCollide)
		{
			StartCoroutine(ResetCollision());
				
		}
    }

	private IEnumerator ResetCollision()
	{
		yield return new WaitForSeconds(3);

		DontCollide = false;
	}
}
