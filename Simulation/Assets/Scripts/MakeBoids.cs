using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeBoids : MonoBehaviour
{
    public Camera cam;
    public GameObject fish;
    private GameObject instantiatedFish;
    public Color RandomColor;
    public GameObject parentFish;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Instatiate new fish
            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            mousePos = new Vector3(mousePos.x, mousePos.y, 0f);
            RandomColor = new Color(Random.Range(0.6f,1f), Random.Range(0.6f, 1f), Random.Range(0.6f,1), 1);
            instantiatedFish = Instantiate(fish, mousePos, Quaternion.identity);
            instantiatedFish.GetComponent<SpriteRenderer>().color = RandomColor;
            instantiatedFish.transform.parent = parentFish.transform;
        }
    }
}
