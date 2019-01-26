﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
	public GameObject target;
	public float damping = 1;
	Vector3 offset;
	
	// Start is called before the first frame update
    void Start()
    {
		offset = target.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void LateUpdate()
	{
		float currentAngle = transform.eulerAngles.y;
		float desiredAngle = target.transform.eulerAngles.y;
		float angle = Mathf.LerpAngle(currentAngle, desiredAngle, 1);//Time.deltaTime * damping);

		Debug.Log("Current: " + currentAngle);
		Debug.Log("Target: " + desiredAngle);

		Quaternion rotation = Quaternion.Euler(0, angle, 0);
		transform.position = target.transform.position - (rotation * offset);

		transform.LookAt(target.transform);
	}
}
