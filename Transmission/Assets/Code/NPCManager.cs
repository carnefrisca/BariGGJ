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

    public Transform target;
    public float speed = 10f;
    public string moveTo;

    MovementState movement;

    public int HealtLevel
    {
        get { return _healtLevel; }
        set { _healtLevel = value; }
    }


    void Start ()
    {
        //InitColorSwapTex();
        //SwapColor(SwapIndex.Body, Color.red);
        //mColorSwapTex.Apply();
    }
	
	void Update ()
    {
        float step = speed * Time.deltaTime;
        int x_rand = Random.Range(0, 1088);
        int y_rand = Random.Range(-736, 0);


        if (gameObject.transform.position.x < x_rand)
            movement = MovementState.walkLeft;
        if (gameObject.transform.position.x > x_rand)
            movement = MovementState.walkRight;



        //if (gameObject.transform.position.x > 544)
        //    movement = MovementState.walkRight;
        //if (gameObject.transform.position.x < 544)
        //    movement = MovementState.walkLeft;

        moveTo = movement.ToString();

        transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(x_rand, y_rand, 0), step);


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
