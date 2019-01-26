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
}