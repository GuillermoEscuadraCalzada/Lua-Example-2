using UnityEngine;
using System.Collections;
using System;
using MoonSharp.Interpreter;
public class LuaProxyGameObject 
{
    GameObject obj;
    string id;
    public event EventHandler<string> LuaTriggerEnter, LuaCollitionEnter;

  [MoonSharpHidden]
  public LuaProxyGameObject(GameObject objecT)
  {
      obj = objecT;
      id = obj.name;        
      string trigger_enter_name = "TriggerEntered_" + obj.name;
      string collition_enter_name = "Collition_Enter_" + obj.name;
      EventManager.StartListening(trigger_enter_name, TriggerEnterEventHandler);
  }

    public void SetPosition(float x, float y, float z)
    {
        obj.transform.position = new Vector3(x, y, z);
    }

    public void TriggerEnterEventHandler(string data)
    {
        LuaOnTriggerEnter(data);
    }



    public void CollitionEnterEventHandler(string data)
    {
        LuaOnCollitionEnter(data);
    }

    /// <summary>
    /// Una función que cualquiera puede llamar y sirve para el TriggerEnter
    /// </summary>
    /// <param name="data"></param>
    public void LuaOnTriggerEnter(string data)
    {
        if (LuaTriggerEnter != null)
        {
            LuaTriggerEnter(this, data);
        }

    }

    /// <summary>
    /// Funcion para cualquier objeto que sirve para su Collition Enter
    /// </summary>
    /// <param name="data"></param>
    public void LuaOnCollitionEnter(string data)
    {
        if (LuaCollitionEnter != null)
        {
            LuaCollitionEnter(this, data);
        }

    }

}
