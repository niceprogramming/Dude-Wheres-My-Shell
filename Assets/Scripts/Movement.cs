using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;
    //[SerializeField]
    public float rotationSpeed = 50;

	public float selfRightingTorque = 1.0f;

	//public GameObject colliderObject;

	private void Start()
	{
		//this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
	}

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

		//transform.rotation = Quaternion.LookRotation(transform.forward);
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


		

		var angle = Vector3.Angle(transform.up, Vector3.up);

		if(angle > 0.001)
		{
			var axis = Vector3.Cross(transform.up, Vector3.up);
			this.GetComponent<Rigidbody>().AddTorque(axis * angle * selfRightingTorque);
		}

		//this.GetComponent<Rigidbody>().angularDrag = 100000000;

		//colliderObject.transform.Translate(0, 0, translation);
		//colliderObject.transform.Rotate(0, rotation, 0);

		//transform.Translate(colliderObject.transform.position);

		//var force = 5f;
		//var offset = 1f;
		//var point = transform.TransformPoint(offset * Vector3.forward);
		//this.GetComponent<Rigidbody>().AddForceAtPosition(force * Vector3.forward, point);

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