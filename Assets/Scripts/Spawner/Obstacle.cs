using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

	[HideInInspector]public bool movingLeft;
	[HideInInspector]public bool movingRight;

    [Header("Behavior")]
	public float velocity;
	public string boundaryTag;

    [Header("Visual Attributes")]
    public bool shouldSpin;
    public float spinVelocity;

    



	private void OnEnable()
	{
		movingLeft = false;
		movingRight = false;
	}

	void Start ()
	{
        
	}
	
	void Update ()
	{
        if(shouldSpin)
        {
            transform.localRotation *= Quaternion.Euler(new Vector3(0, 0, spinVelocity * Time.deltaTime));
        }

		if(movingLeft)
		{
            transform.Translate(new Vector3(-velocity * Time.deltaTime, 0), Space.World);

		}
		else if(movingRight)
		{
            transform.Translate(new Vector3(velocity * Time.deltaTime, 0), Space.World);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.CompareTag(boundaryTag))
		{
			gameObject.SetActive(false);
		}
	}
}
