using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidBoundary : MonoBehaviour
{
    public string boundaryOrientation;
    public float speedChanger = 100;
    private Rigidbody2D rb;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            GameObject fishObj = collision.gameObject.transform.parent.gameObject;
            rb = fishObj.GetComponent<Rigidbody2D>();
            Vector2 fishPos = fishObj.transform.position;
            switch (boundaryOrientation)
            {
                case "R":
                    fishPos.x = -9.5f;
                    break;
                case "L":
                    fishPos.x = 9.5f;
                    break;
                case "U":
                    fishPos.y = -5f;
                    break;
                case "D":
                    fishPos.y = 5f;
                    break;
            }
            fishObj.transform.position = fishPos;
        }
    }
}
