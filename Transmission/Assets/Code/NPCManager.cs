using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public SpriteRenderer mSpriteRenderer;
    Texture2D mColorSwapTex;
    Color[] mSpriteColors;

    private int _healtLevel = 100;

    MovementState movement;

    private Vector3 endpoint;
    private Vector3 vDirection;
    public float speed;


    private float timer = 0.0f;
    public float waitingTime = 2.0f;
    public float Range;

    public int HealtLevel
    {
        get { return _healtLevel; }
        set { _healtLevel = value; }
    }
	
	void Update ()
    {
        print(movement + " " + vDirection);
        print("Current dir " + transform.TransformDirection(vDirection) * speed * Time.deltaTime);

        gameObject.transform.position += transform.TransformDirection(vDirection) * speed * Time.deltaTime;

        print("vector Vs vector " + Vector3.Distance(gameObject.transform.position, endpoint));
        if ((gameObject.transform.position - endpoint).magnitude < Range)
        {
            print("IN TIMER " + timer);
            timer += Time.deltaTime;
            if (timer > waitingTime)
            {
                print("RESET TIMER");
                timer = 0f;
                Reset();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Reset();
    }

    void Reset()
    {
        print("RESET");
        endpoint = gameObject.transform.position; //new Vector3(Random.Range(gameObject.transform.position.x, gameObject.transform.position.x + Range), 1, 0);
        gameObject.transform.LookAt(endpoint);
        RandomPosition();
        RandomDirection();
        print("RESET " + endpoint + " " + vDirection);
    }

    void RandomPosition()
    {
        float x = Random.Range(42f, 1045f);
        float y = Random.Range(-672f, 64f);
        endpoint = new Vector2(x, y);
    }

    void RandomDirection()
    {
        int direction = Random.Range(0,5);
        movement = (MovementState)direction;

        switch (movement)
        {
            case MovementState.idleFront:
                vDirection = Vector3.zero;
                break;
            case MovementState.idleBack:
                vDirection = Vector3.zero;
                break;
            case MovementState.walkLeft:
                vDirection = Vector3.left;
                break;
            case MovementState.walkRight:
                vDirection = Vector3.right;
                break;
            case MovementState.walkTop:
                vDirection = Vector3.up;
                break;
            case MovementState.walkBottom:
                vDirection = Vector3.down;
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
