using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject player;
    public GameObject platform;
    public GameObject operationCube;
    public Transform lastPlatform;
    public List<GameObject> platforms;
    Vector3 lastposition;
    Vector3 newPos;
    bool stop;
    int skip = 2;
    int cubeSpawnThreshold = 4; // Minimum platforms between cube spawns
    int platformCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        lastposition = lastPlatform.position;
        StartCoroutine(SpawnPlatforms());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnPlatforms()
    {
        while (!stop)
        {
            if (GameManager.instance.operations.Any())
            {
                newPos = lastposition;
                newPos.z += 2f;

                GameObject platformClone = Instantiate(platform, newPos, Quaternion.identity);
                platforms.Add(platformClone);

                // Set PlatFormClone parent
                platformClone.transform.SetParent(transform);

                // Increment platform counter
                platformCounter++;

                // Spawn cubes only if the threshold is met
                if (platformCounter >= cubeSpawnThreshold)
                {
                    if (Random.Range(0, 5) == 1 && skip != 2) // Random chance
                    {
                        Vector3 leftCubePos = new Vector3(-5.2f, 0, newPos.z);
                        Vector3 rightCubePos = new Vector3(5.2f, 0, newPos.z);

                        var cube1 = Instantiate(operationCube, leftCubePos, Quaternion.identity);
                        var cube2 = Instantiate(operationCube, rightCubePos, Quaternion.identity);

                        // Set PlatFormClone as parent
                        cube1.transform.SetParent(platformClone.transform);
                        cube2.transform.SetParent(platformClone.transform);

                        // Reset counter and skip
                        platformCounter = 0;
                        skip = 2;
                    }
                    else
                    {
                        skip--;
                    }
                }

                lastposition = newPos;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}
