using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;
	public int transmissionTime;
	public Color neutroStatusColor;
	public Color firstStatusColor;
	public Color secondStatusColor;
	public Color thirdStatusColor;
	public Color zombieStatus;
	public bool infected;
	public float smoothTime = .3F;
	private float velocity = 1F;
	public bool healingBomb = false;

	private Rigidbody2D rb2d;       
	SpriteRenderer m_SpriteRenderer;



	void Start()
	{

		rb2d = GetComponent<Rigidbody2D> ();
	}

	void Update ()
	{
		if ((Input.GetButtonDown ("Interaction")) & (healingBomb))

			StartCoroutine ("RevertTransmission");
			
	}

	void FixedUpdate()
	{

		float moveHorizontal = Input.GetAxis ("Horizontal");


		float moveVertical = Input.GetAxis ("Vertical");

		//Use the two store floats to create a new Vector2 variable movement.
		 Vector2 movement = new Vector2 (moveHorizontal, moveVertical);

		rb2d.AddForce (movement * speed);
	}

	void OnCollisionEnter2D(Collision2D coll) 
	{
		if (coll.gameObject.tag == "Enemy")
		{
			Debug.Log ("Enemy");
			StartCoroutine ("ChangeStatus");
			infected = true;

		}
		if (coll.gameObject.tag == "BombaCurativa") 
		{
			Debug.Log ("BombaCurativa Collision");
			healingBomb = true;
		} 

	}
	 
	void OnCollisionExit2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "BombaCurativa") 
		{
			healingBomb = false;
		} 
	}



	public IEnumerator ChangeStatus()

	{
		
		yield return new WaitForSeconds(transmissionTime);       
		Debug.Log("Start Virus Transmission");  
		m_SpriteRenderer = GetComponent<SpriteRenderer>();
		//m_SpriteRenderer.color = firstStatusColor;
		m_SpriteRenderer.color = new Color(240.0f, Mathf.SmoothDamp(0.0f, 200.9f, ref velocity, smoothTime),0.0f);

	}
	public IEnumerator RevertTransmission()

	{

		yield return new WaitForSeconds(transmissionTime);       
		Debug.Log("Revert Virus Transmission");  
		m_SpriteRenderer = GetComponent<SpriteRenderer>();
		//m_SpriteRenderer.color = firstStatusColor;
		m_SpriteRenderer.color = neutroStatusColor;
		infected = false;
	}





}