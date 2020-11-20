using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainGeneration : MonoBehaviour
{
    public Tile tile;
    public GameObject player;
    public int rendDistance;
    public float scale;
    Tilemap tileMap;
    Vector3 pos;
    int[,] map;
    Vector3 tempPos;
    // Start is called before the first frame update
    void Start()
    {
        tileMap = transform.GetComponent<Tilemap>();
        pos = player.transform.position;
        tempPos = pos;
        map = new int[rendDistance, rendDistance];
        //tileMap.SetTile(new Vector3Int(0, 0, 0), tile);
    }

    // Update is called once per frame
    void Update()
    {
        //pos = new Vector3(player.transform.position.x-rendDistance/2, player.transform.position.y-rendDistance/2, player.transform.position.z);
        pos = player.transform.position;
        //refreshMap();
        if (Mathf.Abs(pos.x-tempPos.x)>1)
        {
            refreshMap();
        }
        /*
        for(int x=0; x<rendDistance; x++)
        {
            for(int y=0; y<rendDistance; y++)
            {
                map[x, y] = (int)Mathf.Round(Mathf.PerlinNoise(Mathf.Round(x + pos.x)*scale, Mathf.Round(y + pos.y)*scale));
                print(Mathf.Round(x + pos.x));
                //map[x, y] = (int)Mathf.Round(Mathf.PerlinNoise((x + pos.x), (y + pos.y)));
            }
        }
        */
    }
    
    private void refreshMap()
    {
        tileMap.ClearAllTiles();
        for (int x = 0; x < rendDistance; x++)
        {
            for (int y = 0; y < rendDistance; y++)
            {
                //print(pos.x + " " + pos.y);
                /*
                if (map[x, y] > 0)
                {
                    tileMap.SetTile(new Vector3Int((int)(x + pos.x), (int)(y + pos.y), 0), tile);
                }
                */
                if (Mathf.PerlinNoise(pos.x+x, pos.y+y) > .5)
                {
                    tileMap.SetTile(new Vector3Int((int)(x + pos.x), (int)(y + pos.y), 0), tile);
                }
            }
        }
    }
}
