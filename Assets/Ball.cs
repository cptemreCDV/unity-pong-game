using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rd2D;
    public float speed;
    public Vector2 velocity;
    public int leftPlayerScore = 0;
    public int rightPlayerScore = 0;
    public TextMeshProUGUI leftPlayerText;
    public TextMeshProUGUI rightPlayerText;

    // Start is called before the first frame update
    void Start()
    {
        rd2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && rd2D.velocity.magnitude < 0.1F)
        {
            ResetAndSetRandomVelocity();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        rd2D.velocity = Vector2.Reflect(velocity, collision.contacts[0].normal);
        velocity = rd2D.velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.position.x > 0)
        {
            leftPlayerScore++;
            leftPlayerText.text = leftPlayerScore.ToString();
        }
        else
        {
            rightPlayerScore++;
            rightPlayerText.text = rightPlayerScore.ToString();

        }
        ResetBall();
    }

    private void ResetBall()
    {
        rd2D.velocity = Vector2.zero;
        transform.position = Vector2.zero;
    }

    private void ResetAndSetRandomVelocity()
    {
        ResetBall();
        rd2D.velocity = GenerateRandomVector2Without0(true) * speed;
        velocity = rd2D.velocity;
    }

    private Vector2 GenerateRandomVector2Without0 (bool returnNormalized)
    {
        Vector2 newRandomVector = new Vector2();

        bool shouldXBeLessThanZero = Random.Range(0, 100) % 2 == 0;
        newRandomVector.x = (shouldXBeLessThanZero) ? Random.Range(-.8f,-.1f) : Random.Range(.1f,.8f);

        bool shouldYBeLessThanZero = Random.Range(0, 100) % 2 == 0;
        newRandomVector.y = (shouldYBeLessThanZero) ? Random.Range(-.8f, -.1f) : Random.Range(.1f, .8f);

        return returnNormalized ? newRandomVector.normalized : newRandomVector;
    }


}
