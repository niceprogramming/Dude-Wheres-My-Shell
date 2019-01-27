using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	int _upgradeLimit = 0;
	ShellScript _shellBoy = null;

	public GameObject CanvasObject;

	public float jumpStrength = 1f;
	public float dashStrength = 1f;
	public float climbForce = 1f;

	public enum UpgradeEnum
	{
		None,
		Jump,
		Dash,
		Climb
	}

	int _upgradeCount = 0;
	const int _upgradeMax = 3;
	UpgradeEnum[] _upgrades = new UpgradeEnum[_upgradeMax];

	int _activeUpgrade = 0;

	UIController uiController = null;

	// Start is called before the first frame update
	void Start()
    {
		_upgradeCount = 0;
		_upgradeLimit = 0;

		uiController = CanvasObject.GetComponent<UIController>();

		ClearUpgrades();

		/*
		 * Test
		 */

		_activeUpgrade = 1;
		_upgrades[0] = UpgradeEnum.Climb;
    }

	bool climbEnabled = false;
    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			if (_shellBoy != null)
			{
				//shellBoy.transform.SetParent(null);
				this.transform.DetachChildren();
				//shellBoy.transform.position += new Vector3(0, 4, 0);
				_shellBoy.DontCollide = true;
				_shellBoy.gameObject.AddComponent<Rigidbody>();
				_shellBoy = null;
				_upgradeLimit = 0;
			}
		}

		if(Input.GetKeyDown(KeyCode.Space))
		{
			//activate selected ability
			if(_upgrades[_activeUpgrade-1] == UpgradeEnum.Climb)
			{
				//If wall climbable, go up
				if(collisionDic.Keys.Count > 0)
					climbEnabled = true;
			}
			else if (_upgrades[_activeUpgrade-1] == UpgradeEnum.Dash)
			{
				this.GetComponent<Rigidbody>().AddForce(transform.forward * dashStrength, ForceMode.Impulse);
			}
			else if (_upgrades[_activeUpgrade-1] == UpgradeEnum.Jump)
			{
				this.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
			}
		}

		if (climbEnabled)
		{
			this.GetComponent<Rigidbody>().AddForce(Vector3.up * climbForce, ForceMode.Force);

		}

		if (Input.GetKeyUp(KeyCode.Space))
		{
			climbEnabled = false;
		}

		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			SetActiveUpgrade(1);
		}

		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			SetActiveUpgrade(2);
		}

		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			SetActiveUpgrade(3);
		}
	}

	Dictionary<string, int> collisionDic = new Dictionary<string, int>();
	string allowedNameCsv = "climb,Climb";
	void OnCollisionEnter(Collision collision)
	{
		var shell = collision.gameObject.GetComponent<ShellScript>();

		if (shell != null && !shell.DontCollide)
		{
			_shellBoy = shell;
			_upgradeLimit = shell.Upgrades;

			Destroy(shell.GetComponent<Rigidbody>());
			shell.transform.SetParent(this.transform);
			shell.transform.localPosition = new Vector3(0, 0.5f, -0.5f);
		}

		bool allowedClimb = false;

		string[] names = allowedNameCsv.Split(',');
		foreach(var name in names)
		{
			if (collision.gameObject.name.Contains(name))
				allowedClimb = true;
		}

		if (allowedClimb)
		{
			if (!collisionDic.ContainsKey(collision.gameObject.name))
				collisionDic.Add(collision.gameObject.name, 1);
			else
				collisionDic[collision.gameObject.name]++;
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		//This won't work right if we're colliding with multiple objects of the same name
		if (collisionDic.ContainsKey(collision.gameObject.name))
		{
			collisionDic[collision.gameObject.name]--;
			if (collisionDic[collision.gameObject.name] <= 0)
				collisionDic.Remove(collision.gameObject.name);
		}

		if (climbEnabled && collisionDic.Keys.Count == 0)
			climbEnabled = false;
	}

	public void SetActiveUpgrade(int slot)
	{
		_activeUpgrade = slot;
		if (_upgradeLimit >= _activeUpgrade)
			_activeUpgrade = _upgradeLimit;

		uiController.SetActive(slot);
	}

	public void ClearUpgrades()
	{
		for (int x = 0; x < _upgradeMax; x++)
		{
			_upgrades[x] = UpgradeEnum.None;
		}
	}

	private bool AddUpgrade(UpgradeEnum upgrade)
	{
		if (_upgradeCount < _upgradeLimit)
		{
			_upgrades[_upgradeCount] = upgrade;

			//Put upgrade onto shell
		}
		else
			return false;

		return true;
	}
}
