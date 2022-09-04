using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour
{
    
	public float Speed;
	public float AngularSpeed;
	protected Rigidbody r;
	
	void Start()
	{
		r = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate()
	{
		Speed = r.velocity.magnitude;
		AngularSpeed = r.angularVelocity.magnitude;
		
		
		r.angularVelocity = new Vector3(0,5,0);
	}

}