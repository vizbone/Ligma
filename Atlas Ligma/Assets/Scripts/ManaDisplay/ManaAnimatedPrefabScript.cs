    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaAnimatedPrefabScript : MonoBehaviour {
    private Transform startPos;
    private Transform endPos;
    // Movement speed in units/sec.
    public float speed = 0.5F;

    public ResourceManager.ResourceType manaType;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    public ResourceManager resourceManager;
	// Use this for initialization
	void Start ()
    {
        resourceManager = FindObjectOfType<ResourceManager>();
        if (manaType == ResourceManager.ResourceType.mana)
        {
            
            startPos = gameObject.transform;                //setting the start spawn position of the prefab 
            endPos = resourceManager.townhall.transform;    //Where the prefab will end at 
            startTime = Time.time;
            journeyLength = Vector3.Distance(startPos.transform.localScale, endPos.transform.localScale); // distance of the startpoint minus the end point
            print("Built");
        }
        else if(manaType == ResourceManager.ResourceType.prebuildMana)
        {
            startPos = gameObject.transform;                //setting the start spawn position of the prefab 
            endPos = FindObjectOfType<Cannon>().transform;      //Where the prefab will end at 
            startTime = Time.time;
            journeyLength = Vector3.Distance(startPos.transform.localScale, endPos.transform.localScale); // distance of the start point minus the end point
            print("prebuilt");
        }
    }

    // Follows the target position like with a spring
    void Update()
    {
        // Distance moved = time * speed.
        float distCovered = (Time.time - startTime) * speed;
        

        // Fraction of journey completed = current distance divided by total distance.
        float fracJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
        transform.position = Vector3.Lerp(startPos.position, endPos.position, fracJourney);
    }
}
