using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class MyCustomEvent : UnityEvent<string>
{ }
    
public class EventManager : MonoBehaviour
{
    Dictionary<string, MyCustomEvent> eventDictionary;
    static EventManager eventManager; //Se crea un singleton de esta clase

    public static EventManager instance { //Se crea una instancia de este objeto

        get {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;
                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManager script on a GameObject");
                }
                else
                {
                    eventManager.Init();
                }
            }
            return eventManager;
        }
    }

    private void Init()
    {
        if(eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, MyCustomEvent>();
        }   
    }


    public static void StartListening(string eventName, UnityAction<string> listener)
    { //Empieza a escuchar los eventos que ocurren dentro del juego
        MyCustomEvent thisEvent = null; //Crea un costum event vacío
        if(instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        { //Inicializa el dictionario y busca obtener el evento. Si ya tiene eventos, nada más lo añade
            thisEvent.AddListener(listener);
        }
        else
        { //Si no tiene eventos, crea un nuevo evento y lo añade
            thisEvent = new MyCustomEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction<string> listener)
    {
        if (eventName == null) return;
        MyCustomEvent thisEvent = null;
        if(instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName, string data)
    {
        MyCustomEvent thisEvent = null;
        if(instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(data);
        }
    }

}
