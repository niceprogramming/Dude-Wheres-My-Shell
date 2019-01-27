using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;
    //[SerializeField]
    public float rotationSpeed = 50;
    public Animator animator;
	public float selfRightingTorque = 1.0f;
    private bool isWalking;

	private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    } 

	private void Update()
	{
		
	}

	void FixedUpdate()
    {
        float translation = Mathf.RoundToInt(Input.GetAxis("Vertical")) * speed;
        float rotation = Mathf.RoundToInt(Input.GetAxis("Horizontal")) * rotationSpeed;
        if (translation != 0f || rotation != 0f)
        {
            translation *= Time.deltaTime;
            rotation *= Time.deltaTime;

            
            animator.Play("Walk");
            
            transform.Translate(0, 0, translation);

            transform.Rotate(0, rotation, 0);

            
        }
        else
        {
            
                animator.Play("Idle");
           
        }

     
        var angle = Vector3.Angle(transform.up, Vector3.up);
        if (angle > 0.001)
        {
            var axis = Vector3.Cross(transform.up, Vector3.up);
            this.GetComponent<Rigidbody>().AddTorque(axis * angle * selfRightingTorque);
        }
    }
}