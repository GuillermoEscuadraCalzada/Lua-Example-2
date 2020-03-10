
using System.Collections;
using System.IO;
using MoonSharp.Interpreter;
using System.Collections.Generic;
using UnityEngine;

public class LuaExample : MonoBehaviour
{
    const string luaCode = @"
    -- Lua Code
    num = 1 + 1
    print(num)
    ";

    string assetPath;
    string fileName;
    string path;
    string code;

    LuaVM virtualM;
    // Start is called before the first frame update
    void Start()
    {
        virtualM = new LuaVM();
        virtualM.ExecuteString(luaCode);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            code = LoadFile();
            virtualM.ExecuteString(code);

            // Get table to iterate
            Table fruitTable = virtualM.GetGlobalTable("fruits");
            foreach (DynValue fruit in fruitTable.Values) {
                Debug.Log(fruit.String); // Prints "apple" then "banana"
            }

            // Or get a lua function and call it
            DynValue fruitFunction = virtualM.GetGlobal("GetRandomFruit");
            Debug.Log(virtualM.Call(fruitFunction).String); // Prints return of GetRandomFruit
        }
    }
    public string LoadFile () {


        assetPath = Application.streamingAssetsPath;
        fileName = "lua_example.lua";
        path = assetPath + "/" + fileName;
        print(path);
        StreamReader streamR = new StreamReader(path);
        string rawText = streamR.ReadToEnd();
        streamR.Close();

        print(rawText);
        return rawText;
    }

    

}
