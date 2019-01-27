using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellScript : MonoBehaviour
{
	public int MaxUpgrades = 1;
	public bool DontCollide = false;

	public Vector3 JumpAttachpoint = new Vector3(0,0,0);
	public Vector3 DashAttachpoint = new Vector3(0,0,-0.5f);
	public Vector3 ClimbAttachpoint = new Vector3(-0.2f,0.1f,-0.9f);

	public GameObject JumpPrefab;
	public GameObject DashPrefab;
	public GameObject ClimbPrefab;

	PlayerScript.UpgradeEnum[] upgrades = { PlayerScript.UpgradeEnum.None, PlayerScript.UpgradeEnum.None, PlayerScript.UpgradeEnum.None };

	private GameObject attachedJump;
	private GameObject attachedDash;
	private GameObject attachedClimb;

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
		if(upgrades[slot] != PlayerScript.UpgradeEnum.None)
		{
			if(upgrades[slot] == PlayerScript.UpgradeEnum.Climb && attachedClimb != null)
			{
				Destroy(attachedClimb);
				attachedClimb = null;
			}
			else if(upgrades[slot] == PlayerScript.UpgradeEnum.Dash && attachedDash != null)
			{
				Destroy(attachedDash);
				attachedDash = null;
			}
			else if(upgrades[slot] == PlayerScript.UpgradeEnum.Jump && attachedJump != null)
			{
				Destroy(attachedJump);
				attachedJump = null;
			}
		}

		upgrades[slot] = upgrade;

		//Move prefab to slot
		if(upgrade == PlayerScript.UpgradeEnum.Dash)
		{
			attachedDash = Instantiate(DashPrefab, this.gameObject.transform);
			Destroy(attachedDash.GetComponent<Rigidbody>());
			if(attachedDash.GetComponent<BoxCollider>() != null)
				attachedDash.GetComponent<BoxCollider>().enabled = false;
			attachedDash.transform.localPosition = DashAttachpoint;
		}
		else if(upgrade == PlayerScript.UpgradeEnum.Jump)
		{
			attachedJump = Instantiate(JumpPrefab, this.gameObject.transform);
			Destroy(attachedJump.GetComponent<Rigidbody>());
			if(attachedJump.GetComponent<BoxCollider>() != null)
				attachedJump.GetComponent<BoxCollider>().enabled = false;
			attachedJump.transform.localPosition = JumpAttachpoint;
		}
		else if(upgrade == PlayerScript.UpgradeEnum.Climb)
		{
			attachedClimb = Instantiate(ClimbPrefab, this.gameObject.transform);
			Destroy(attachedClimb.GetComponent<Rigidbody>());
			if(attachedClimb.GetComponent<BoxCollider>() != null)
				attachedClimb.GetComponent<BoxCollider>().enabled = false;
			attachedClimb.transform.localPosition = ClimbAttachpoint;
		}
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
