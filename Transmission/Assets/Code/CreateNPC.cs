using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tiled2Unity;
using System.Linq;
using System.Reflection;

public class CreateNPC : MonoBehaviour
{
    public int HowManyNPC = 1;
    public Animator otherAnimator;

    // Use this for initialization
    void Start()
    {
        TiledMap tileMap = GetComponent<TiledMap>();
        GameObject[] Map = GameObject.FindGameObjectsWithTag("Map");

        RectangleObject[] boxColliders = Map[0].GetComponentsInChildren<RectangleObject>();
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

        // Free box
        List<Vector2> tileFree = new List<Vector2>();
        for (int k = 0 ; k < tileMap.NumTilesWide; k++)
        {
            for (int s = 0; s < tileMap.NumTilesHigh; s++)
            {
                if (tileCollision.Contains(new Vector2(k,s)) == false)
                {
                    tileFree.Add(new Vector2(k, s));
                }
            }
        }

        GameObject npcGroup = new GameObject();

        // Add NPC        
        for (int n = 0; n < HowManyNPC; n++)
        {
            var randomIndex = Random.Range(0, tileFree.Count);

            otherAnimator.gameObject.AddComponent(typeof(TmxObject));;
            ((TmxObject)otherAnimator.gameObject.GetComponent(typeof(TmxObject))).TmxPosition = tileFree[randomIndex];
            ((TmxObject)otherAnimator.gameObject.GetComponent(typeof(TmxObject))).TmxSize = new Vector2(32, 64);
            ((TmxObject)otherAnimator.gameObject.GetComponent(typeof(TmxObject))).TmxId = 1000 + n;
            ((SpriteRenderer)otherAnimator.GetComponent(typeof(SpriteRenderer))).sortingOrder = 2;
            otherAnimator.gameObject.name = "npc";
            otherAnimator.gameObject.SetActive(true);
            otherAnimator.transform.position = new Vector3(tileFree[randomIndex].x * 32, -tileFree[randomIndex].y * 32, 0);

            Instantiate(otherAnimator, npcGroup.transform);

            tileFree.Remove(tileFree[randomIndex]);
        }

        npcGroup.transform.parent = tileMap.transform;
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}

//public class AdvRectangleObject : RectangleObject
//{
//    private float _topTile;
//    public float TopTile
//    {
//        get { return _topTile; }
//    }

//    private float _bottomTile;
//    public float BottoTile
//    {
//        get { return _bottomTile; }
//    }

//    private RectangleObject[] _rectangleObject;
//    public RectangleObject[] RectangleObject
//    {
//        get { return _rectangleObject; }
//        set { _rectangleObject = value; }
//    }

//    public AdvRectangleObject()
//    { }

//    public AdvRectangleObject(RectangleObject[] rts)
//    {
//        int i = 0;
//        foreach (RectangleObject rt in rts)
//        {
//            _rectangleObject[i] = rt;

//            _topTile = rt.TmxPosition.x;
//            _bottomTile = rt.TmxSize.y;
//            i++;
//        }
//    }

//}