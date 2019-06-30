using UnityEngine;

public class Food : MonoBehaviour
{
    public int reward = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided with " + collision.transform.name);
        if (collision.transform.tag == "Player")
        {
            //Change location
            gameObject.transform.SetPositionAndRotation(new Vector2(Random.Range(-49, 49), Random.Range(-49, 49)),
            Quaternion.identity);
            //Add score
            GameManager.instance.AddScore(reward);
        }
    }
}
