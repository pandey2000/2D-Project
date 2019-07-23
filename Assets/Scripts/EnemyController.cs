using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	//AudioSource audiosource;
    //public AudioClip roboFixedClip;
	public float speed = 3.0f;
    public bool vertical;
    public float changeTime = 3.0f;
	public ParticleSystem smokeEffect;
    Rigidbody2D rigidbody2D;
    float timer;
    int direction = 1;
    bool broken = true;
	Animator animator;
    // Start is called before the first frame update
    void Start()
    {	
		//audiosource = GetComponent<AudioSource>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
		animator = GetComponent<Animator>();
		smokeEffect.Play();
    }

	public void Fix()
	{
		animator.SetTrigger("Fixed");
		broken = false;
		//rigidbody2D.simulated = false;
		rigidbody2D.bodyType= RigidbodyType2D.Static;
		smokeEffect.Stop();
		//audiosource.PlayOneShot(roboFixedClip);
		//Destroy(smokeEffect.gameObject);
	}

    // Update is called once per frame
    void Update()
    {
		if(!broken)
			return;
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
        
        
        Vector2 position = rigidbody2D.position;

        if (vertical)
        {
			animator.SetFloat("MoveX", 0);
			animator.SetFloat("MoveY", direction);
            position.y = position.y + Time.deltaTime * speed * direction;
        }
        else
        {
			animator.SetFloat("MoveX", direction);
			animator.SetFloat("MoveY", 0);
            position.x = position.x + Time.deltaTime * speed * direction;
        }
 
        rigidbody2D.MovePosition(position);
    }

	void OnCollisionStay2D(Collision2D other)
	{
		RubyController player = other.gameObject.GetComponent<RubyController >();

		if (player != null && broken== true)
		{
			 player.ChangeHealth(-1);
		}
	}
}