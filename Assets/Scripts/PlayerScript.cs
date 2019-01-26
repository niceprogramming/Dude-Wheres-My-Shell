using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	int _upgradeLimit = 0;
	ShellScript _shellBoy = null;

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

	// Start is called before the first frame update
	void Start()
    {
		_upgradeCount = 0;
		_upgradeLimit = 0;

		ClearUpgrades();
    }

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
	}

	public void SetActiveUpgrade(int slot)
	{
		_activeUpgrade = slot;
		if (_upgradeLimit >= _activeUpgrade)
			_activeUpgrade = _upgradeLimit;
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
