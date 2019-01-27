using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		var allImages = GetComponentsInChildren<Image>();
		
		foreach(var image in allImages)
		{
			if (image.name == "UpgradeSlot_Background")
			{
				//enable
				var parentName = image.transform.parent.name;
			}
			else
			{
				image.enabled = false;
			}
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void SetUpgrade(int slot, PlayerScript.UpgradeEnum upgrade)
	{
		var allImages = GetComponentsInChildren<Image>();

		foreach (var image in allImages)
		{
			var parentName = image.transform.parent.name;

			if (parentName == "Slot_1" && slot == 0)
			{
				SetUpgradeInternal(image, upgrade);
			}
			else if (parentName == "Slot_2" && slot == 1)
			{
				SetUpgradeInternal(image, upgrade);
			}
			else if (parentName == "Slot_3" && slot == 2)
			{
				SetUpgradeInternal(image, upgrade);
			}
		}
	}

	private void SetUpgradeInternal(Image image, PlayerScript.UpgradeEnum upgrade)
	{
		//Only disable upgrade images, not the background or highlight
		if (upgrade == PlayerScript.UpgradeEnum.Climb)
		{
			if(image.name == "Climb")
			{
				image.enabled = true;
			}
			else if(image.name == "Dash")
			{
				image.enabled = false;
			}
			else if(image.name == "Jump")
			{
				image.enabled = false;
			}
		}
		else if (upgrade == PlayerScript.UpgradeEnum.Dash)
		{
			if (image.name == "Climb")
			{
				image.enabled = false;
			}
			else if (image.name == "Dash")
			{
				image.enabled = true;
			}
			else if (image.name == "Jump")
			{
				image.enabled = false;
			}
		}
		else if (upgrade == PlayerScript.UpgradeEnum.Jump)
		{
			if (image.name == "Climb")
			{
				image.enabled = false;
			}
			else if (image.name == "Dash")
			{
				image.enabled = false;
			}
			else if (image.name == "Jump")
			{
				image.enabled = true;
			}
		}
	}

	public void SetActive(int slot)
	{
		var allImages = GetComponentsInChildren<Image>();

		foreach(var image in allImages)
		{
			var parentName = image.transform.parent.name;

			if (parentName == "Slot_1" && slot == 1)
			{
				SetActiveInternal(image, true);
			}
			else if (parentName == "Slot_2" && slot == 2)
			{
				SetActiveInternal(image, true);
			}
			else if (parentName == "Slot_3" && slot == 3)
			{
				SetActiveInternal(image, true);
			}
			else if(parentName == "Slot_1" && maxCount > 0)
			{
				SetActiveInternal(image, false);
			}
			else if (parentName == "Slot_2" && maxCount > 1)
			{
				SetActiveInternal(image, false);
			}
			else if (parentName == "Slot_3" && maxCount > 2)
			{
				SetActiveInternal(image, false);
			}
			else
			{
				//do nothing
			}
		}
	}

	private void SetActiveInternal(Image image, bool active)
	{
		if (image.name == "UpgradeSlot_Background")
		{
			image.enabled = !active;
		}
		else if(image.name == "UpgradeSlot_Selected")
		{
			image.enabled = active;
		}
	}

	int maxCount = 0;
	public void SetMaxUpgrades(int count)
	{
		var allImages = GetComponentsInChildren<Image>();
		maxCount = count;

		Dictionary<int, string> enabledSlots = new Dictionary<int, string>();
		enabledSlots.Add(0, "");
		enabledSlots.Add(1, "Slot_1");
		enabledSlots.Add(2, "Slot_1,Slot_2");
		enabledSlots.Add(3, "Slot_1,Slot_2,Slot_3");

		foreach (var image in allImages)
		{
			if (image.name == "UpgradeSlot_Background")
			{
				//enable
				var parentName = image.transform.parent.name;

				if (!enabledSlots[count].Contains(parentName))
					image.enabled = false;
				else
					image.enabled = true;
			}
			else
			{
				image.enabled = false;
			}
		}
	}
}
