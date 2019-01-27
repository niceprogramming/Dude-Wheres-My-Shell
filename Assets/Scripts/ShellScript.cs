using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellScript : MonoBehaviour
{
	public int MaxUpgrades = 1;
	public bool DontCollide = false;

	public Vector3 JumpAttachpoint;
	public Vector3 DashAttachpoint;
	public Vector3 ClimbAttachpoint;

	public GameObject JumpPrefab;
	public GameObject DashPrefab;
	public GameObject ClimbPrefab;

	PlayerScript.UpgradeEnum[] upgrades = { PlayerScript.UpgradeEnum.None, PlayerScript.UpgradeEnum.None, PlayerScript.UpgradeEnum.None };

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

	public void SetUpgrade(int slot, PlayerScript.UpgradeEnum upgrade)
	{
		upgrades[slot] = upgrade;

		//Move prefab to slot
	}

	public List<PlayerScript.UpgradeEnum> GetUpgrades()
	{
		List<PlayerScript.UpgradeEnum> results = new List<PlayerScript.UpgradeEnum>();

		for(int x = 0; x < upgrades.Length; x++)
		{
			if (upgrades[x] != PlayerScript.UpgradeEnum.None)
				results.Add(upgrades[x]);
		}

		return results;
	}

	private IEnumerator ResetCollision()
	{
		yield return new WaitForSeconds(3);

		DontCollide = false;
	}
}
