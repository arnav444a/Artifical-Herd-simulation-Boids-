using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParentFishScript : MonoBehaviour
{
    public Slider[] sliders;
    private float startTime,startTime1;
    BoidScript[] childrenFish;
    
    public float sight;
    
    public float seperation;
    
    public float cohesion;
    
    public float alignment;
    
    public float maxSpeed;
    
    public float noise;
    private void Start()
    {
        childrenFish = GetComponentsInChildren<BoidScript>();
    }
    private void Update()
    {
        cohesion = sliders[0].value;
        alignment = sliders[1].value;
        noise = sliders[2].value;
        sight = sliders[3].value;
        maxSpeed = sliders[4].value;
        seperation = sliders[5].value;

        if (Time.time - startTime >= 2)
        {
            startTime = Time.time;
            childrenFish = GetComponentsInChildren<BoidScript>();
        }
        if (Time.time - startTime1 >= 0.2f)
        {
            startTime1 = Time.time;
            foreach (BoidScript fish in childrenFish)
            {
                fish.cohesionMultiplier = cohesion;
                fish.GetComponent<CircleCollider2D>().radius = sight;
                fish.seperationDistance = seperation;
                fish.alignmentMultiplier = alignment;
                fish.maxSpeed = maxSpeed;
                fish.perlinNoiseMagnitude = noise;
            }
        }
    }
}
