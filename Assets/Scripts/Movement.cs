using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float rotationSpeed = 50;


    void Update()
    {

        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;


        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;


        transform.Translate(0, 0, translation);


        transform.Rotate(0, rotation, 0);
    }
}