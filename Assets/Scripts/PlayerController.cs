using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[System.Serializable]
public class Boundary
{
	public float xMax, xMin, zMax, zMin;
}

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
	public float speed;
	public float tilt;
	public Boundary boundary;
	private Rigidbody rig;
	
	[Header("Shooting")]
	public GameObject shot;
	public Transform shotSpawn;
	public Transform shotSpawn1;
	public float fireRate;
	private float nextFire;
	
	private AudioSource audioSource;
	
	void Awake(){
		rig = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}
	
	void Start()
	{
		UpdateBoundary();
	}
	
	void UpdateBoundary()
	{
		Vector2 half = Utils.GetHalfDimensionsInWorldUnits();
		
		boundary.xMax = half.x - 13.4f;
		boundary.xMin = - half.x + 13.4f;
		boundary.zMin = - half.y + 41f;
		boundary.zMax =	half.y - (-10f);

	
	}
	
	void Update()
	{
		if(CrossPlatformInputManager.GetButton("Fire1") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, Quaternion.identity);
			Instantiate(shot, shotSpawn1.position, Quaternion.identity);
			audioSource.Play();
		}
	}
	
	void FixedUpdate(){
		float moveHorizontal = CrossPlatformInputManager.GetAxis("Horizontal");
		float moveVertical = CrossPlatformInputManager.GetAxis("Vertical");
		
		Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
		rig.velocity = movement * speed;
		rig.position = new Vector3(Mathf.Clamp(rig.position.x,boundary.xMin,boundary.xMax ), 0f,Mathf.Clamp(rig.position.z,boundary.zMin,boundary.zMax));
		rig.rotation = Quaternion.Euler(-90f, 0f, rig.velocity.x * -tilt);
	}
}
