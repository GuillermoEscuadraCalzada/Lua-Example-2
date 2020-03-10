using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class ReadTextFile : MonoBehaviour
{
    // Start is called before the first frame update
    string assetPath;
    string fileName;
    string path;
   
    void Start()
    {
        LoadFile();
    }

    public void LoadFile()
    {
        assetPath = Application.streamingAssetsPath;
        fileName = "lua_example.txt";
        path = assetPath + "/" + fileName;
        print(path);
        StreamReader streamR = new StreamReader(path);
        string rawText = streamR.ReadToEnd();
        streamR.Close();
        print(rawText);       
    }

}
