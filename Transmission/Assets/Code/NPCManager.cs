using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public SpriteRenderer mSpriteRenderer;
    Texture2D mColorSwapTex;
    Color[] mSpriteColors;

    MovementState movement;

    private Vector3 endpoint;
    private Vector3 vDirection;
    public float speed;

    private float timer;
    public float waitingTime = 2.0f;
    public float Range;

    private float healtTimer;
    public float healtTiming = 0.1f;

    private Animator anim;

    public int HealtLevel = 100;

    public bool infected = false;

    public bool faceFront;
    public bool faceRight;
    public bool faceRear;
    public bool faceLeft;

    void Start()
    {
        anim = GetComponent<Animator>();
        InitColorSwapTex();
        timer = 0f;
        healtTimer = 0f;
        Reset();
    }

    void Update ()
    {
        if (infected)
        {
            if (HealtLevel == 100)
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            if (Enumerable.Range(99, 85).Contains(HealtLevel))
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            if (Enumerable.Range(84, 65).Contains(HealtLevel))
                gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
            if (Enumerable.Range(64, 45).Contains(HealtLevel))
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            if (Enumerable.Range(44, 1).Contains(HealtLevel))
                gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
            if (HealtLevel <= 0)
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;
                HealtLevel = 0;
            }

            healtTimer += 0.1f;
            if (healtTimer > healtTiming)
            {
                healtTimer = 0f;
                HealtLevel -= 1;
            }
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }

        gameObject.transform.position += transform.TransformDirection(vDirection) * speed * Time.deltaTime;

        if (Vector3.Distance(gameObject.transform.position,endpoint) < Range)//((gameObject.transform.position - endpoint).magnitude < Range)
        {
            timer += Time.deltaTime;
            if (timer > waitingTime)
            {
                timer = 0f;
                Reset();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag== "Enemy")
        { infected = true;
        }
        timer = 0f;
        Reset();
    }

    void Reset()
    {
        RandomDirection();

        RandomPosition();
        endpoint = gameObject.transform.position;
        //gameObject.transform.LookAt(endpoint);
    }

    void RandomPosition()
    {
        float x = Random.Range(0.0f, 1088f);
        float y = Random.Range(0.0f, 736f);
        endpoint = new Vector2(x, y);
    }

    void RandomDirection()
    {
        int normale = Random.Range(0,5);
        movement = (MovementState)normale;
        anim.SetInteger("direction", normale);

        switch (movement)
        {
            case MovementState.idleFront:
                anim.SetBool("faceLeft", false);
                anim.SetBool("faceRight", false);
                anim.SetBool("faceRear", false);
                anim.SetBool("faceFront", true);
                vDirection = Vector3.zero;
                break;
            case MovementState.idleBack:
                anim.SetBool("faceLeft", false);
                anim.SetBool("faceRight", false);
                anim.SetBool("faceRear", true);
                anim.SetBool("faceFront", false);
                vDirection = Vector3.zero;
                break;
            case MovementState.walkLeft:
                vDirection = Vector3.left;
                anim.SetBool("faceFront", false);
                anim.SetBool("faceRear", false);
                anim.SetBool("faceRight", false);
                anim.SetBool("faceLeft", true);
                break;
            case MovementState.walkRight:
                vDirection = Vector3.right;
                anim.SetBool("faceLeft", false);
                anim.SetBool("faceFront", false);
                anim.SetBool("faceRear", false);
                anim.SetBool("faceRight", true);
                break;
            case MovementState.walkTop:
                vDirection = Vector3.up;
                anim.SetBool("faceLeft", false);
                anim.SetBool("faceRight", false);
                anim.SetBool("faceFront", false);
                anim.SetBool("faceRear", true);
                break;
            case MovementState.walkBottom:
                vDirection = Vector3.down;
                anim.SetBool("faceLeft", false);
                anim.SetBool("faceRight", false);
                anim.SetBool("faceRear", false);
                anim.SetBool("faceFront", true);
                break;
        }
    }

    public void InitColorSwapTex()
    {
        Texture2D colorSwapTex = new Texture2D(32, 64, TextureFormat.RGBA32, false, false);
        colorSwapTex.filterMode = FilterMode.Point;

        for (int i = 0; i < colorSwapTex.width; ++i)
            colorSwapTex.SetPixel(i, 0, new Color(0.0f, 0.0f, 0.0f, 0.0f));

        colorSwapTex.Apply();

        mSpriteRenderer.material.SetTexture("_SwapTex", colorSwapTex);

        mSpriteColors = new Color[colorSwapTex.width];
        mColorSwapTex = colorSwapTex;
    }

    public enum SwapIndex
    {
        Body = 0,
    }

    public void SwapColor(SwapIndex index, Color color)
    {
        mSpriteColors[(int)index] = color;
        mColorSwapTex.SetPixel((int)index, 0, color);
    }

    public enum MovementState
    {
        idleFront,
        idleBack,
        walkLeft,
        walkRight,
        walkTop,
        walkBottom
    }
}
