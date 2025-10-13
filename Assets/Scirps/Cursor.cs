using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorDefault;
    [SerializeField] private Texture2D cursorShoot;
    [SerializeField] private Texture2D cursorReload;
    private Vector2 hotspot = new Vector2(16, 48);
    void Start()
    {
        Cursor.SetCursor(cursorDefault, hotspot, CursorMode.Auto);
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(cursorShoot, hotspot, CursorMode.Auto);
        }
        else
        {
            if (Input.GetMouseButtonUp(0) )
            {
                Cursor.SetCursor(cursorDefault, hotspot, CursorMode.Auto);
            }
        }     
        if (Input.GetKeyDown(KeyCode.R))
         {
           Cursor.SetCursor(cursorReload, hotspot, CursorMode.Auto);
         } else
        {
            {
                if (Input.GetKeyUp(KeyCode.R))
                {
                    Cursor.SetCursor(cursorDefault, hotspot, CursorMode.Auto);
                }
            }

        }
        
    }
}
