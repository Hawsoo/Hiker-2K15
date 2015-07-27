using UnityEngine;
using System.Collections;

namespace EnemyCrawlerLib
{
    public class NewEnemyEntry
    {
        public Vector3 position;
        public Vector3 velocity;

        public NewEnemyEntry(Vector3 position, Vector3 velocity)
        {
            this.position = position;
            this.velocity = velocity;
        }

        // Creates a gameobject as such
        public void CreateObjectWithProperties(GameObject original, bool active)
        {
            GameObject obj = GameObject.Instantiate(original) as GameObject;
            obj.SetActive(active);

            // Set up with properties
            obj.transform.position = position;

            EnemyMovement em = obj.GetComponent<EnemyMovement>();
            if (em != null)
            {
                em.commitStartingVelocity = true;
                em.startVelocity = velocity;
            }
        }
    }
}

public class EnemyCrawlerTESTCreateEvenCoords : MonoBehaviour
{
    public int objectsToCreate;
    private int objectsCreated = 0;

    private float totalTime = 0;
    private float cumTime = 0;

    private bool writeIteration = false;

    private EnemyCrawlerLib.NewEnemyEntry[] enemyEntries;

    // Init
    void Start()
    {
        enemyEntries = new EnemyCrawlerLib.NewEnemyEntry[objectsToCreate];
    }

	// Update
	void Update()
    {
        if (!writeIteration)
        {
            // Find total time
            totalTime += Time.deltaTime;
        }
        else
        {
            if (cumTime >= (totalTime / objectsToCreate) * objectsCreated)
            {
	            // Create new object
                Debug.Log("Position: [" +
                    transform.position +
                    "],\tVelocity: [" +
                    GetComponent<EnemyMovement>().GetVelocity() + "];");

                // Add entry
                enemyEntries[objectsCreated] =
                    new EnemyCrawlerLib.NewEnemyEntry(transform.position, GetComponent<EnemyMovement>().GetVelocity());

                // Increment
                objectsCreated++;
            }

            // Add to time
            cumTime += Time.deltaTime;
        }
	}

    // Flag cycle switch
    public void ResetFlag()
    {
        if (writeIteration && enabled)
        {
            // Create objects
            foreach (var entry in enemyEntries)
            {
                entry.CreateObjectWithProperties(gameObject, false);
            }

            // Checkout
            enabled = false;
        }

        writeIteration = true;
    }
}
