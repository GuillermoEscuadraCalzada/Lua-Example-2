using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[LuaApi(
    luaName = "SuperMarket",
    description = "This is a test lua api")]
public class SuperMarketAPI : LuaAPIBase {
    private readonly List<string> m_Veggies = new List<string>
    {
    "Aubergine",
    "Broccoli",
    "Cauliflower",
    "Carrot",
    "Kale",
  };

    private readonly List<string> m_Fruits = new List<string>
    {
    "Strawberry",
    "Grape",
    "Lychee",
    "Melon",
    "Apple",
  };

    public SuperMarketAPI ()
      : base("SuperMarket") {
    }

    protected override void InitialiseAPITable () {
        m_ApiTable["GetRandomVeg"] = (System.Func<string>)(Lua_GetRandomVeggies);
        m_ApiTable["GetRandomFruit"] = (System.Func<string>)(Lua_GetRandomFruits);
        m_ApiTable["MaxStock"] = MaxStock;
    }

    [LuaApiEnumValue(description = "The max stock any shop should contain")]
    private const int MaxStock = 10;

    [LuaApiFunction(
      name = "GetRandomVeg",
      description = "Returns a random vegetable that can be stocked by an in-game shop"
      )]
    private string Lua_GetRandomVeggies () {
        int randomIndex = Random.Range(0, m_Veggies.Count - 1);
        return m_Veggies[randomIndex];
    }

    [LuaApiFunction(
      name = "GetRandomFruit",
      description = "Returns a random fruit that can be stocked by an in-game shop"
    )]
    private string Lua_GetRandomFruits () {
        int randomIndex = Random.Range(0, m_Fruits.Count - 1);
        return m_Fruits[randomIndex];
    }
}
