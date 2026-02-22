using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private void Update()
    {
        if (Keyboard.current.sKey.wasPressedThisFrame)
        { 
            ObjectSpawner.Instance.Spawn(GameDatabase.Instance.GetProductByName("Wood"));
        }
    }
}
