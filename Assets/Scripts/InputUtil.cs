using UnityEngine;
using UnityEngine.InputSystem;

public class InputUtil : MonoBehaviour
{
    private Vector3 cursorInWorldSpace;
    private Vector3 utilCursor;
    public Vector3 CursorInWorldSpace
    {
        get
        {
            utilCursor = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            return new Vector3(utilCursor.x, utilCursor.y, 0);
        }
    }
}
