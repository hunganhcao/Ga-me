using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject[] theGoodies;


    //Time it takes to spawn theGoodies
   
    public float waitingForNextSpawn = 5;
   
    public float theCountdown = 5;
    
    public float waitingForChange = 15;

    

    // the range of X
    [Header("X Spawn Range")]
    public float xMin;
    public float xMax;

    // the range of y
    [Header("Y Spawn Range")]
    public float yMin;
    public float yMax;


    void Start()
    {
        
    }

    public void Update()
    {
        theCountdown -= Time.deltaTime;
        waitingForChange -= Time.deltaTime;
        if (waitingForChange <= 0 )
        {

            if (waitingForNextSpawn <= 2)
            {
                waitingForNextSpawn = 2;
            }
            else waitingForNextSpawn = waitingForNextSpawn-(float)0.5;
            Debug.Log(waitingForNextSpawn);
            waitingForChange = 15;
        }
        
        if (theCountdown <= 0)
        {
            SpawnGoodies();
            theCountdown = waitingForNextSpawn;
        }
        
            
        // timer to spawn the next goodie Object
    }


    void SpawnGoodies()
    {
        // Defines the min and max ranges for x and y
        Vector2 pos = new Vector2(Random.Range(Camera.main.transform.position.x- 30, Camera.main.transform.position.x ), Random.Range(-10, -5));
        

        // Choose a new goods to spawn from the array (note I specifically call it a 'prefab' to avoid confusing myself!)
        GameObject goodsPrefab = theGoodies[Random.Range(0, theGoodies.Length)];

        // Creates the random object at the random 2D position.
        Instantiate(goodsPrefab, pos,Quaternion.identity);

        // If I wanted to get the result of instantiate and fiddle with it, I might do t$$anonymous$$s instead:
        //GameObject newGoods = (GameObject)Instantiate(goodsPrefab, pos)
        //newgoods.somet$$anonymous$$ng = somet$$anonymous$$ngelse;
    }
}
