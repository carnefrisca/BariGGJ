using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tiled2Unity;
using System.Linq;
using System.Reflection;

public class Create_NPC : MonoBehaviour
{
    public int PercentageNPC = 1;
    public Texture2D tex;
    private Sprite mySprite;
    private SpriteRenderer sr;

    void Awake()
    {
        sr = gameObject.AddComponent<SpriteRenderer>() as SpriteRenderer;
        sr.color = new Color(0.9f, 0.9f, 0.9f, 1.0f);

        transform.position = new Vector3(1.5f, 1.5f, 0.0f);
    }

    // Use this for initialization
    void Start ()
    {
        TiledMap tileMap = GetComponent<TiledMap>();
        GameObject[] Map = GameObject.FindGameObjectsWithTag("Map");

        int tileWidth = tileMap.NumTilesWide;
        int tileHeight = tileMap.NumTilesHigh;

        RectangleObject[] boxColliders = Map[0].GetComponentsInChildren<RectangleObject>();
        //Vector2[] tileCollision = new Vector2[tileWidth*tileHeight];
        List<Vector2> tileCollision = new List<Vector2>();


        foreach (TmxObject tmo in boxColliders)
        {
            int x_end = (int)(tmo.TmxPosition.x + tmo.TmxSize.x) / 32;
            int y_end = (int)(tmo.TmxPosition.y + tmo.TmxSize.y) / 32;

            for (int x = (int)tmo.TmxPosition.x / 32; x < x_end; x++)
            {
                for (int y = (int)tmo.TmxPosition.y / 32; y < y_end; y++)
                {
                    // Here NO NPC
                    tileCollision.Add(new Vector2(x, y));
                }
            }
        }


        // Add NPC
        for (int n=0;n<PercentageNPC;n++)
        {
            int x = Random.Range(1, tileWidth - 1);
            int y = Random.Range(1, tileHeight - 1);

            //mySprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
            GameObject obj = new GameObject("npc");
            sr = obj.AddComponent(typeof(SpriteRenderer)) as SpriteRenderer;
        }

        sr.sprite = mySprite;


    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
