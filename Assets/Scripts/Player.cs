using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Player : MonoBehaviour
{
    public GameObject tailPrefab;
    public int[] tail;
    List<Transform> snakeTail = new List<Transform>();
    public float moveSpeed = 1f;
    public float time;

    [SerializeField]
    private Vector2 moveDirection;
    private Rigidbody2D rb2d;
    private Vector2 temp;
    private AudioSource audioSource;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        moveDirection = Vector3.up;
        InvokeRepeating("Translate", 0f, moveSpeed);
    }

    private void Translate()
    {
        //Temporary for tail movement
        temp = transform.position;

        transform.Translate(moveDirection);

        if (snakeTail.Count > 0)
        {
            snakeTail.Last().position = temp;
            snakeTail.Insert(0, snakeTail.Last());
            snakeTail.RemoveAt(snakeTail.Count - 1);
        }

    }

    private void Update()
    {
        //Read direction change
        if (Input.GetKeyDown(KeyCode.W)) { moveDirection = Vector2.up; }
        else if (Input.GetKeyDown(KeyCode.S)) { moveDirection = -Vector2.up; }
        else if (Input.GetKeyDown(KeyCode.A)) { moveDirection = -Vector2.right; }
        else if (Input.GetKeyDown(KeyCode.D)) { moveDirection = Vector2.right; }

        time += Time.deltaTime;
        if (time > 60f)
        {
            time = 0f;
            if (moveSpeed > .2f)
            {
                moveSpeed -= .1f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check trigger entering
        //and perform specific actions
        Debug.Log("Trigger enter: " + collision.name);
        if (collision.transform.tag == "Wall")
        {
            gameObject.SetActive(false);
            GameManager.instance.EndGame();
        }
        else if (collision.transform.name.StartsWith("Food"))
        {
            audioSource.Play();
            AddTail();
        }
        else if (collision.transform.name.StartsWith("Tail"))
        {
            Destroy(gameObject);
            GameManager.instance.EndGame();
        }
    }

    public void AddTail()
    {
        if (tailPrefab == null) { Debug.LogError("Tail prefab is not assigned!"); }
        //Adding tail gameobject
        else
        {
            GameObject go = (GameObject)Instantiate(tailPrefab, temp, Quaternion.identity);
            snakeTail.Insert(0, go.transform);
        }
    }

}
