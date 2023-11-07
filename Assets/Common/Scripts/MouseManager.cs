using UnityEngine;
using UnityEngine.Events;

public class MouseManager : MonoBehaviour
{
    public LayerMask clickableLayer; // layermask used to isolate raycasts against clickable layers

    public Texture2D pointer; // normal mouse pointer
    public Texture2D target; // target mouse pointer
    public Texture2D doorway; // doorway mouse pointer
    public Texture2D sword;

    public UnityEvent<Vector3> OnClickEnvironment;
    //public UnityEvent<CharacterStats> OnClickAttack;
    public UnityEvent<GameObject> OnClickAttackable;

    public GameObject player;

    void Update()
    {
        // Raycast into scene
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50, clickableLayer.value))
        {
            IAttackable attackable = hit.collider.GetComponent<IAttackable>();

            bool door = false;

            if (attackable != null)
            {
                Cursor.SetCursor(sword, new Vector2(16, 16), CursorMode.Auto);
            }

            //bool attack = false;
            else if (hit.collider.gameObject.tag == "Doorway")
            {
                Cursor.SetCursor(doorway, new Vector2(16, 16), CursorMode.Auto);
                door = true;
            }
            else
            {
                Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (attackable != null)
                {
                    OnClickAttackable.Invoke(hit.collider.gameObject);
                }
                else
                {
                    Vector3 destination = hit.point;

                    if (door)
                    {
                        var doorWay = hit.collider.GetComponent<Doorway>();
                        if (doorWay != null || doorWay.exit != null)
                        {
                            destination = doorWay.exit.position;
                        }
                    }
                    OnClickEnvironment.Invoke(destination);
                }
            }
        }
        else
        {
            Cursor.SetCursor(pointer, Vector2.zero, CursorMode.Auto);
        }
    }
}

