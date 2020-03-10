using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : LuaAPIBase
{
    List<GameObject> walls;
    GameObject wall;
    int rowns, cols;

    public DungeonManager() : base("Dungeon")
    {
        walls = new List<GameObject>();
        LoadContent prefabs = GameObject.Find("Prefabs").GetComponent<LoadContent>();
        wall = prefabs.wall;
    }

    protected override void InitialiseAPITable()
    {
        m_ApiTable["LoadMap"] = (System.Action<string>)(Lua_LoadMap);
    }

    /*Carga un mapa dentro de todos los archivos en el Streaming assets, busca un nombre con el argumento y al encontrarlo lee
     todos sus elementos*/
    void Lua_LoadMap(string map)
    {
        DestroyWalls();

        string[] str = map.Split('\n');
        int posX = 0;
        int posZ = 0;
        //int tileSize = 1;
        foreach(string line in str)
        {
            foreach(char letter in line)
            {
                if (letter.Equals('#'))
                {
                    CreateWall(posX, posZ);
                }
                posX++;
            }
            posX = 0;
            posZ++;
        }

    }

    void CreateWall(int x, int z)
    {
        GameObject newWall = GameObject.Instantiate(wall, new Vector3(x, 0.5f, z), Quaternion.identity);
        walls.Add(newWall);
    }

    /*Destruir todas las paredes que se encuentran guardadas al leer el archivo de textp*/
    void DestroyWalls()
    {
        foreach(GameObject obj in walls)
        {
            GameObject.Destroy(obj);
        }
        walls.Clear();
    }

}
