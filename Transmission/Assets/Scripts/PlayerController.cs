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
	public bool holdingBomb = false;
	public GameObject destroyGameObject;
	public float horizontalDirection;
	public float verticalDirection;

	public bool faceFront;
	public bool faceRight;
	public bool faceRear;
	public bool faceLeft;
	public GameObject bombBlastPS;
	private AudioSource myAudio;
	public AudioClip blastBomb;
	public AudioClip pickBomb;
	public AudioClip startInfection;
	public Canvas myCanvas;
	public GameObject gameOverScreen;



	private Rigidbody2D rb2d;       
	SpriteRenderer m_SpriteRenderer;
	private Animator anim;


	void Start()
	{
		myAudio = GetComponent<AudioSource> ();
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
	}

	void Update ()
	{

		if ((Input.GetButtonDown ("HoldBomb")) & (healingBomb)) 
		{
			myAudio.PlayOneShot (pickBomb);
			DestroyBombOnStage ();

			//playanimation
		}
		if ((Input.GetButtonDown ("DropBomb")) & (holdingBomb)) 
		{
			myAudio.PlayOneShot (blastBomb);
			StartCoroutine ("RevertTransmission");
			Debug.Log ("DropBomb");
			anim.SetBool ("holdingBomb", false);
			bombBlastPS.SetActive (true);
			Instantiate (bombBlastPS,transform.position, transform.rotation);
		}


			
	}

	void FixedUpdate()
	{
		anim.SetFloat ("hDirection", horizontalDirection);
		anim.SetFloat ("vDirection", verticalDirection);
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		horizontalDirection = Input.GetAxis ("Horizontal");
		verticalDirection = Input.GetAxis ("Vertical");
		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
		rb2d.AddForce (movement * speed);

		if (moveVertical > 0)
		{
			faceFront = false;
			faceRear = true;
			faceLeft = false;
			faceRight = false;
			anim.SetBool ("faceRear", true);
			anim.SetBool ("faceFront", false);
			anim.SetBool ("faceLeft", false);
			anim.SetBool ("faceRight", false);
		}
		
		if (moveVertical < 0) 
		{
			faceFront = true;
			faceRear = false;
			faceLeft = false;
			faceRight = false;
			anim.SetBool ("faceRear", false);
			anim.SetBool ("faceFront", true);
			anim.SetBool ("faceLeft", false);
			anim.SetBool ("faceRight", false);
		}
		if (moveHorizontal > 0) 
		{
			faceFront = false;
			faceRear = false;
			faceLeft = false;
			faceRight = true;
			anim.SetBool ("faceRear", false);
			anim.SetBool ("faceFront", false);
			anim.SetBool ("faceLeft", false);
			anim.SetBool ("faceRight", true);
		}
		if (moveHorizontal < 0)
		{
			faceFront = false;
			faceRear = false;
			faceLeft = true;
			faceRight = false;
			anim.SetBool ("faceRear", false);
			anim.SetBool ("faceFront", false);
			anim.SetBool ("faceLeft", true);
			anim.SetBool ("faceRight", false);
		}
	}

	void OnTriggerEnter2D(Collider2D coll) 
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
			destroyGameObject = coll.gameObject;

		} 

	}
	 
	void DestroyBombOnStage ()
	{
		holdingBomb = true;
		Destroy(destroyGameObject);
		anim.SetBool ("holdingBomb", true);
	}

	void OnTriggerExit2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "BombaCurativa") 
		{
			healingBomb = false;
		} 
	}



	public IEnumerator ChangeStatus()

	{
		myAudio.PlayOneShot (startInfection);
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
		holdingBomb = false;
	}

	public void GameOver()
	{
		myCanvas.GetComponent<CanvasGroup> ().alpha = 1;
		gameOverScreen.SetActive (true);
		
	}






}