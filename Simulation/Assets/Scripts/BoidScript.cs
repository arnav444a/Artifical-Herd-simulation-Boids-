using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidScript : MonoBehaviour
{
    public List<GameObject> fishes = new List<GameObject>();
    private bool inList = false;
    private Rigidbody2D rb,rbNeighbour;
    private Vector2 totalVelocity, averageVelocity, difference,centerOfFlock,vectorToCenter,directionalVelocity;
    private Vector3 totalPositionsOfFlock;
    public Color AverageColor;
    private SpriteRenderer fishRenderer;
    private float t,k;

    public float speed, seperationDistance,cohesionMultiplier,alignmentMultiplier,maxSpeed,perlinNoiseMagnitude;
    private void Start()
    {
        Vector2 velocity = new Vector2(Random.Range(-3, 3), Random.Range(-3, 3));

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = velocity * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            //Debug.Log("fish detected");
            fishes.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            inList = false;
            for (int i = 0; i < fishes.Count; i++)
            {
                if (fishes[i] == collision.gameObject)
                {
                    inList = true;
                }
            }
            if (inList)
            {
                fishes.Remove(collision.gameObject);
            }
        }
    }
    void Update()
    {
        k = 0;
        
        // Allignment
        totalVelocity = Vector2.zero;
        averageVelocity = Vector2.zero;
       foreach(GameObject fish in fishes)
        {
            rbNeighbour = fish.GetComponent<Rigidbody2D>();
            if (rbNeighbour.velocity != Vector2.zero)
            {
                totalVelocity += rbNeighbour.velocity;
            }
            else
            {
                k++;
            }
        }
        if (fishes.Count > 2)
        {
            fishRenderer = GetComponent<SpriteRenderer>();
            //new Color((255-(fishes.Count*10))/255, ((fishes.Count*15)+30)/255,((fishes.Count*23)+20)/255, 1)
            AverageColor = new Color((177+(-fishes.Count*5))/3, ((fishes.Count*5)+130)/255,((fishes.Count*23)+50)/305, 1);
            Color lerpColor = Color.Lerp(fishRenderer.color, AverageColor, t);
            fishRenderer.color = lerpColor;
            if (fishRenderer.color != AverageColor)
            {
                t += Time.deltaTime*0.05f;
            }
            else
            {
                t = 0f;
            }
        }
        
        totalVelocity += rb.velocity;
        averageVelocity = totalVelocity / (fishes.Count+1-k);

        rb.velocity = averageVelocity * alignmentMultiplier;

        //Seperation
        foreach(GameObject fish in fishes)
        {
            if(Vector2.Distance(fish.transform.position,transform.position) < seperationDistance)
            {
                difference = transform.position - fish.transform.position;
                directionalVelocity += difference.normalized;
                rb.velocity += difference.normalized;
            }
        }

        //Cohesion
        if (fishes.Count > 0)
        {
            totalPositionsOfFlock = Vector3.zero;
            foreach (GameObject fish in fishes)
            {
                if (fish.gameObject.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
                {
                    totalPositionsOfFlock += fish.transform.position;
                }
            }
            totalPositionsOfFlock = totalPositionsOfFlock / (fishes.Count-k);
            centerOfFlock = new Vector2(totalPositionsOfFlock.x, totalPositionsOfFlock.y);

            vectorToCenter = centerOfFlock - new Vector2(transform.position.x, transform.position.y);
            rb.velocity += vectorToCenter.normalized * cohesionMultiplier;
            rb.velocity = new Vector2(rb.velocity.x + (Random.Range(-1,2)*(Mathf.PerlinNoise(transform.position.x, transform.position.y)*perlinNoiseMagnitude))
                , rb.velocity.y + (Random.Range(-1,2)*(Mathf.PerlinNoise(transform.position.x, transform.position.y) * perlinNoiseMagnitude)));
        }
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
       float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg - 90;
       transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}