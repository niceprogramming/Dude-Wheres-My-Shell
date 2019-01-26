using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;
    //[SerializeField]
    public float rotationSpeed = 50;

	//public GameObject colliderObject;

	private void Awake()
	{
		//var boxCollider = gameObject.GetComponent<BoxCollider>();
		//boxCollider.isTrigger = true;
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.LeftShift))
		{
			if(shellBoy != null)
			{
				//shellBoy.transform.SetParent(null);
				this.transform.DetachChildren();
				//shellBoy.transform.position += new Vector3(0, 4, 0);
				shellBoy.DontCollide = true;
				shellBoy.gameObject.AddComponent<Rigidbody>();
				shellBoy = null;
				upgrades = 0;
				
			}
		}
	}

	void FixedUpdate()
    {

        float translation = Mathf.RoundToInt(Input.GetAxis("Vertical")) * speed;
        float rotation = Mathf.RoundToInt(Input.GetAxis("Horizontal")) * rotationSpeed;

        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

		//translation 

        transform.Translate(0, 0, translation);


        transform.Rotate(0, rotation, 0);

		//colliderObject.transform.Translate(0, 0, translation);
		//colliderObject.transform.Rotate(0, rotation, 0);

		//transform.Translate(colliderObject.transform.position);
    }

	int upgrades = 0;
	ShellScript shellBoy = null;
	private void OnCollisionEnter(Collision collision)
	{
		var shell = collision.gameObject.GetComponent<ShellScript>();

		if (shell != null && !shell.DontCollide)
		{
			shellBoy = shell;
			upgrades = shell.Upgrades;
			//shell.GetComponent<Collider>().enabled = false;
			Destroy(shell.GetComponent<Rigidbody>());
			shell.transform.SetParent(this.transform);
			shell.transform.localPosition = new Vector3(0, 0.5f, -0.5f);

			
		}
	}

	//private void OnTriggerEnter(Collider other)
	//{
	//	Debug.Log("Test");
	//}
}