using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	int _upgradeLimit = 0;
	ShellScript _shellBoy = null;

	public GameObject CanvasObject;

	public float jumpStrength = 5f;
	public float dashStrength = 10f;
	public float climbForce = 10f;

	public Vector3 ShellAttachVector = new Vector3(0.25f, 0.16f,-0.2f);

	public bool DestroyUpgrades = true;

	public float DashDeadlyTime = 1f;

	public int JumpCountMax = 1;
	private int jumpCount = 0;

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
		 
		//_upgradeLimit = 1;
		//_upgrades[0] = UpgradeEnum.Dash;
		uiController.SetMaxUpgrades(0);
    }

	bool climbEnabled = false;
	bool dashEnabled = false;
    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			if (_shellBoy != null)
			{
				//shellBoy.transform.SetParent(null);
#warning Bug, detached the crab mesh
				this.transform.DetachChildren();
				//shellBoy.transform.position += new Vector3(0, 4, 0);
				_shellBoy.DontCollide = true;
				_shellBoy.gameObject.AddComponent<Rigidbody>();
				_shellBoy.gameObject.GetComponent<BoxCollider>().enabled = true;
				_shellBoy = null;

				ClearUpgrades();
				uiController.SetMaxUpgrades(0);
			}
		}

		//Joy4 = LB

		//Debug.Log("JoyButton 1 (b): " + Input.GetButton("bUpgrade"));
		//Debug.Log("JoyButton 2 (x): " + Input.GetButton("xUpgrade"));
		//Debug.Log("JoyButton 3 (y): " + Input.GetButton("yUpgrade"));
		//Debug.Log("JoyButton 0 (a): " + Input.GetButton("aUpgrade"));
		//Debug.Log("JoyButton 6 (select): " + Input.GetButton("select_Reset"));
		//Debug.Log("JoyButton 7 (start): " + Input.GetButton("start_menu"));

		if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("aUpgrade")) && _activeUpgrade > 0)
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

				dashEnabled = true;

				StartCoroutine(DisableDash());
			}
			else if (_upgrades[_activeUpgrade-1] == UpgradeEnum.Jump)
			{
				if (jumpCount > 0)
				{
					this.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
					jumpCount--;
				}
			}
		}

		if (climbEnabled)
		{
			this.GetComponent<Rigidbody>().AddForce(Vector3.up * climbForce, ForceMode.Force);
		}

		if (Input.GetKeyUp(KeyCode.Space) && Input.GetButtonUp("aUpgrade"))
		{
			climbEnabled = false;
		}

		if(Input.GetKeyDown(KeyCode.Alpha1) || Input.GetButtonDown("xUpgrade"))
		{
			SetActiveUpgrade(1);
		}

		if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetButtonDown("yUpgrade"))
		{
			SetActiveUpgrade(2);
		}

		if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetButtonDown("bUpgrade"))
		{
			SetActiveUpgrade(3);
		}
	}

	private IEnumerator DisableDash()
	{
		yield return new WaitForSeconds(DashDeadlyTime);

		dashEnabled = false;
	}

	Dictionary<string, int> collisionDic = new Dictionary<string, int>();
	string allowedClimbNameCsv = "climb,Climb";
	string allowedBreakCsv = "break,Break";
	string dashTag = "Dash";
	string jumpTag = "Jump";
	string climbTag = "Climb";

	void OnCollisionEnter(Collision collision)
	{
		var shell = collision.gameObject.GetComponent<ShellScript>();

		if (shell != null && !shell.DontCollide)
		{
			_shellBoy = shell;
			_upgradeLimit = shell.MaxUpgrades;

			uiController.SetMaxUpgrades(_upgradeLimit);

			//Ask shell what upgrades it has already
			var upgradesOnShell = shell.GetUpgrades();
			foreach(var onShell in upgradesOnShell)
			{
				AddUpgrade(onShell);
			}

			Destroy(shell.GetComponent<Rigidbody>());
			shell.gameObject.GetComponent<BoxCollider>().enabled = false;
			shell.transform.SetParent(this.transform);
			shell.transform.localPosition = ShellAttachVector;
			shell.transform.localRotation = new Quaternion(0f,0f,0f,0f);
		}

		jumpCount = JumpCountMax;

		string cName = collision.gameObject.name;

		bool allowedClimb = false;

		string[] climbNames = allowedClimbNameCsv.Split(',');
		foreach(var name in climbNames)
		{
			if (cName.Contains(name))
				allowedClimb = true;
		}

		if (allowedClimb)
		{
			if (!collisionDic.ContainsKey(cName))
				collisionDic.Add(cName, 1);
			else
				collisionDic[cName]++;
		}

		string[] breakNames = allowedBreakCsv.Split(',');
		foreach(var name in breakNames)
		{
			if(dashEnabled && cName.Contains(name))
			{
				Destroy(collision.gameObject);
			}
		}

		if(collision.gameObject.tag == dashTag)
		{
			int slot = UpgradeInternal(collision.gameObject, UpgradeEnum.Dash);

			if(_shellBoy != null && slot > -1)
			{
				_shellBoy.SetUpgrade(slot, UpgradeEnum.Dash);

				SetActiveUpgrade(slot + 1);
			}
		}
		else if(collision.gameObject.tag == jumpTag)
		{
			int slot = UpgradeInternal(collision.gameObject, UpgradeEnum.Jump);

			if (_shellBoy != null && slot > -1)
			{
				_shellBoy.SetUpgrade(slot, UpgradeEnum.Jump);

				SetActiveUpgrade(slot + 1);
			}
		}
		else if(collision.gameObject.tag == climbTag)
		{
			int slot = UpgradeInternal(collision.gameObject, UpgradeEnum.Climb);

			if (_shellBoy != null && slot > -1)
			{
				_shellBoy.SetUpgrade(slot, UpgradeEnum.Climb);

				SetActiveUpgrade(slot + 1);
			}
		}
	}

	private int UpgradeInternal(GameObject gameObject, UpgradeEnum upgrade)
	{
		if(DestroyUpgrades && _shellBoy != null)
		{
			Destroy(gameObject);
		}
		else if(DestroyUpgrades && _shellBoy == null)
		{
			return -1;
		}
		else
			gameObject.transform.Translate(0, 10, 0);

		return AddUpgrade(upgrade);
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
		if (_activeUpgrade >= _upgradeLimit)
			_activeUpgrade = _upgradeLimit;

		uiController.SetActive(slot);
	}

	public void ClearUpgrades()
	{
		for (int x = 0; x < _upgradeMax; x++)
		{
			_upgrades[x] = UpgradeEnum.None;
		}

		_upgradeCount = 0;
		_upgradeLimit = 0;
	}

	private int AddUpgrade(UpgradeEnum upgrade)
	{
		int result = -1;

		if (_upgradeCount < _upgradeLimit)
		{
			_upgrades[_upgradeCount] = upgrade;

			uiController.SetUpgrade(_upgradeCount, upgrade);

			result = _upgradeCount;
			_upgradeCount++;

			//Put upgrade onto shell
		}
		else if(_upgradeCount == _upgradeLimit && _upgradeLimit > 0)
		{
			_upgradeCount = 0;
			_upgrades[0] = upgrade;
			uiController.SetUpgrade(0, upgrade);

			result = _upgradeCount;
			_upgradeCount++;
		}
		else
			return -1;

		return result;
	}
}
