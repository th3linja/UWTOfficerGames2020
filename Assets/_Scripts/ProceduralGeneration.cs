using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProceduralGeneration : MonoBehaviour
{
    public int xRendDistance;
    public int yRendDistance;
    public int xGenDist;
    public int yGenDist;
    public int caveHeight;
    public int groundDepth;
    public int caveAltitude;
    public int height;
    public GameObject player;
    public Tile ground;
    Tilemap tileMap;
    int heightRefresh;
    int xMax;
    Vector3 pos;
    List<List<int>> map = new List<List<int>>();
    // Start is called before the first frame update
    void Start()
    {
        pos = player.transform.position;
        xMax = xGenDist;
        heightRefresh = xGenDist;
        tileMap = transform.GetComponent<Tilemap>();
        for(int x = 0; x < 5; x++)
        {
            List<int> temp = new List<int>();
            //int y = -Mathf.RoundToInt(yGenDist/2);y< Mathf.RoundToInt(yGenDist / 2);y++
            for (int y = -Mathf.RoundToInt(yGenDist / 2); y < Mathf.RoundToInt(yGenDist / 2); y++)
            {
                if (y <= 0)
                {
                    temp.Add(1);
                }
                else
                {
                    temp.Add(0);
                }
            }
            map.Add(temp);
        }
        RefreshMap();
    }

    // Update is called once per frame
    void Update()
    {
        pos = player.transform.position;
        print(height);
        if (heightRefresh < pos.x)
        {
            if (Random.Range(0, 10) == 0)
            {
                height += 1;
                heightRefresh = (int)pos.x + 10;
            }
            else if (Random.Range(0, 10) == 1)
            {
                height -= 1;
                heightRefresh = (int)pos.x + 10;
            }
        }
        if (xMax < pos.x)
        {
            xMax = (int)pos.x;
        }
        //print(map.Count + " " + xMax);
        for(int x = map.Count; x < xMax+xGenDist; x++)
        {
            List<int> temp = new List<int>();
            //int y = height-groundDepth; y<=height+caveAltitude; y++
            for (int y = -Mathf.RoundToInt(yGenDist / 2); y < Mathf.RoundToInt(yGenDist / 2); y++)
            {
                //print(height);
                //print(Random.Range(0, 2));
                if (y <= height)
                {
                    temp.Add(1);
                }
                else
                {
                    temp.Add(0);
                }
            }
            map.Add(temp);
        }
        RefreshMap();
    }

    private void RefreshMap()
    {
        tileMap.ClearAllTiles();
        for (int x = (int)pos.x - xRendDistance; x < (int)pos.x + xRendDistance; x++)
        {
            for(int y = (int)pos.y - yRendDistance; y<(int)pos.y+yRendDistance; y++)
            {
                if (x >= 0 && y >= 0 && map.Count > x)
                {
                    if(map[x].Count > y)
                    {
                        if (map[x][y] == 1)
                        {
                            tileMap.SetTile(new Vector3Int(x, y, 0), ground);
                        }
                    }
                }
            }
        }
    }
}
