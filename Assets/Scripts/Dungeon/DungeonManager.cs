using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : LuaAPIBase
{
    public List<GameObject> walls, objects, enemies;
    GameObject wall, enemy, door, key;
    Player player;
    int rowns, cols;

    public DungeonManager() : base("Dungeon")
    {
        walls = new List<GameObject>();
        objects = new List<GameObject>();
        enemies = new List<GameObject>();
        LoadContent prefabs = GameObject.Find("Prefabs").GetComponent<LoadContent>();
        wall = prefabs.wall;
        enemy = prefabs.enemy;
        door = prefabs.door;
        key = prefabs.key;
    }

    protected override void InitialiseAPITable()
    {
        
        m_ApiTable["LoadMap"] = (System.Action<string>)(Lua_LoadMap);
        m_ApiTable["CreateEnemy"] = (System.Func<string, string, LuaProxyGameObject>)(Lua_CreateGameObject);
        m_ApiTable["DestroyObject"] = (System.Action<GameObject>)(DestroyGameObject);
        m_ApiTable["DestroyEnemy"] = (System.Action<GameObject>)(DestroyEnemy);

    }

    /*Carga un mapa dentro de todos los archivos en el Streaming assets, busca un nombre con el argumento y al encontrarlo lee
     todos sus elementos*/
    void Lua_LoadMap(string map)
    {
        DestroyWalls();
        DestroyObjects();
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

    /// <summary>
    /// Crea una pared en las coordenadas indicadas instanciando el prefab de puerta 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    void CreateWall(int x, int z)
    {
        GameObject newWall = GameObject.Instantiate(wall, new Vector3(x, 0.5f, z), Quaternion.identity);
        walls.Add(newWall);
    }

    /// <summary>
    /// Crea un objeto, el primer parámetro es el nombre que el objeto tendrá y el segundo parámetro es el tipo de objeto dque se quiere crear
    /// </summary>
    /// <param name="id"></param>
    /// <param name="tipodDeObjeto"></param>
    /// <returns></returns>
    LuaProxyGameObject Lua_CreateGameObject(string id, string tipodDeObjeto)
    {
        LuaProxyGameObject luaObj = null;
        if (tipodDeObjeto.Equals("Enemy"))
        {  ///El tipo de objeto es un enemigo
            GameObject obj = GameObject.Instantiate(enemy);
            obj.name = id;
            enemies.Add(obj);
            luaObj = new LuaProxyGameObject(obj);
        }else if (tipodDeObjeto.Equals("Door"))
        { ///El tipo de objeto es una puerta
            GameObject obj = GameObject.Instantiate(door);
            obj.name = id;
            objects.Add(obj);
            luaObj = new LuaProxyGameObject(obj);
        }
        else if (tipodDeObjeto.Equals("Key"))
        { ///El tipo de objeto es una llave
            GameObject obj = GameObject.Instantiate(key);
            obj.name = id;
            objects.Add(obj);
            luaObj = new LuaProxyGameObject(obj);
        }
        return luaObj;
    }

    void DestroyGameObject( GameObject obj)
    {
        objects.Remove(obj);
        GameObject.Destroy(obj);
    }


    void DestroyEnemy(GameObject obj)
    {
        objects.Remove(obj);
        GameObject.Destroy(obj);
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

    /// <summary>
    /// Destruye todos los objetis en escena y limpia la lista de objetos
    /// </summary>
    void DestroyObjects()
    {
        foreach (GameObject obj in objects)
        {
            GameObject.Destroy(obj);
        }
        objects.Clear();
    }

}
