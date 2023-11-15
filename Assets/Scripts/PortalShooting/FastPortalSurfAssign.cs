using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastPortalSurfAssign : MonoBehaviour
{

    public List<Transform> Boxes;
    public bool Top, Bottom, Front, Back, Left, Right;


    void Awake()
    {
		foreach (Transform box in Boxes) {
            PortalBoxSurface surf = gameObject.AddComponent<PortalBoxSurface>(); 
            surf.Up = Top;
            surf.Down = Bottom;
            surf.Front = Front;
            surf.Back = Back;
            surf.Left = Left;
            surf.Right = Right;
            surf.AttachedBox = box;

        }
    }

}
