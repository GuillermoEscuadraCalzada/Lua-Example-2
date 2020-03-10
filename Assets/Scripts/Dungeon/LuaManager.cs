using UnityEngine;
using System.Collections;
using System.IO;
using MoonSharp.Interpreter;
public class LuaManager : MonoBehaviour
{
    string assetPath;
    string fileName;
    string path;
    string scriptPath;
    LuaVM vm;
    // Use this for initialization
    void Start()
    {
        vm = new LuaVM();
    }

    public void LoadScript()
    {
        scriptPath = GetLuaScriptPath("dungeon.lua");
        Debug.Log(scriptPath);
        vm.ExecuteScript(scriptPath);
    }
    

    public string GetLuaScriptPath(string path)
    {
        assetPath = Application.streamingAssetsPath;
        fileName = path;
        path = assetPath + "/" + fileName;
        print(path);

        return path;
    }
}
