using UnityEngine;


public class Ball : MonoBehaviour
{
    // config
    [SerializeField] AudioClip[] bouncySounds;
    [SerializeField] Vector2 launchVector;
    [SerializeField] private Paddle paddle;
    [SerializeField] private float randomBounceFactor = 0.2f;

    // state
    private Vector2 paddleToBallVector;
    private bool launched = false;
    private bool frozen = false;

    // cached components
    private AudioSource audioSource;
    private Rigidbody2D myRigidBody;


    // Start is called before the first frame update
    void Start() {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        paddleToBallVector = this.transform.position - this.paddle.transform.position;
        this.audioSource = GetComponent<AudioSource>();
        this.myRigidBody = GetComponent<Rigidbody2D>();
    }

    public void Freeze()
    {
        myRigidBody.bodyType = RigidbodyType2D.Static;
        this.frozen = true;
    }

    private void LockBallToPaddle()
    {
        this.transform.position = new Vector2(this.paddle.transform.position.x, this.paddle.transform.position.y) + this.paddleToBallVector;
    }

    private void LaunchOnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.launched = true;
            myRigidBody.velocity = launchVector;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!launched)
        {
            LockBallToPaddle();
            LaunchOnClick();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2(Random.Range(0, randomBounceFactor), Random.Range(0, randomBounceFactor));

        if (launched)
        {
            myRigidBody.velocity += velocityTweak;

            var clip = bouncySounds[Random.Range(0, bouncySounds.Length - 1)];
            audioSource.PlayOneShot(clip);
        }
    }
}
