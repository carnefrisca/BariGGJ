using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Manager : MonoBehaviour
{
    public SpriteRenderer mSpriteRenderer;
    Texture2D mColorSwapTex;
    Color[] mSpriteColors;

    void Start ()
    {
        InitColorSwapTex();
        SwapColor(SwapIndex.Body, Color.blue);
        mColorSwapTex.Apply();

    }
	
	void Update ()
    {
		
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
        //mSpriteColors[(int)index] = color;
        for (int i=0; i<mSpriteColors.Length; i++)
        {
            mSpriteColors[i] = color;
        }

        mColorSwapTex.SetPixel((int)index, 0, color);
    }
}
