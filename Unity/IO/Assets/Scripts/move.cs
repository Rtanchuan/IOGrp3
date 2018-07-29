using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {


    private Rigidbody2D rgdbdy2d;
    public float acceleration=5;

	// Use this for initialization
	void Start () {
        rgdbdy2d = GetComponent<Rigidbody2D>();

	}
	
	// Update is called once per frame
	void Update () {
		if(acceleration>10)
			acceleration = 10;
        float vertical = Input.GetAxis("Vertical") + acceleration;
        float horizontal = Input.GetAxis("Horizontal") * acceleration;
        rgdbdy2d.AddForce(new Vector2(horizontal, vertical));

    }
}
