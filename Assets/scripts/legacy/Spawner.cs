using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnObj;
    public float spawnTerm;
    public float xRange;
    public float yRange;
    public float yDistance;
    public float termMin;
    public float termMax;

    public bool startSpawning;
    public bool spawn;
    public bool spawnRand;

    // Start is called before the first frame update
    void Start()
    {
        spawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (startSpawning == true)
        {
            if (spawn == true)
            {
                spawn = false;
                float xPos = Random.Range(xRange, -xRange);
                float yPos = Random.Range(yRange, -yRange);
                Instantiate(spawnObj, new Vector3(xPos+transform.position.x, yPos + transform.position.y - yDistance, transform.position.z), Quaternion.identity);
                StartCoroutine(mineSpawn());
            }
        }
    }

    IEnumerator mineSpawn()
    {
        if (spawnRand == true)
        {
            spawnTerm = Random.Range(termMin, termMax);
        }
        yield return new WaitForSeconds(spawnTerm);
        spawn = true;
    }

}
