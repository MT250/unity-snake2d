using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject spawnObject;
    public int spawnAmount = 10;

    void Start()
    {
        if (spawnObject == null)
        {
            Debug.LogError("Spawn object is not assigned!");
        }
        else
        {
            //Spawn 10 spawnObjects
            for (int i = 0; i < spawnAmount; i++)
            {
                Instantiate(spawnObject, new Vector2((int)Random.Range(-49, 49), (int)Random.Range(-49, 49)),
                            Quaternion.identity);
            }
            gameObject.SetActive(false);
        }
    }
}
