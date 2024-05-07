using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Cheats : MonoBehaviour
{
    public UnityEvent cheatEvent;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            cheatEvent.Invoke();
        }
    }
    
    void OnEnable()
    {
        print("Cheats Enabled");
        cheatEvent.AddListener(OnCheat);
    }
    
    void OnDisable()
    {
        print("Cheats Disabled");
        cheatEvent.RemoveListener(OnCheat);
    }
    
    void OnCheat()
    {
        print("onCheat print");
    }
}