using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

	public void Launch(Vector2 direction, float force)
	{
		rigidbody2D.AddForce(direction * force);
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		//we also add a debug log to know what the projectile touch
		EnemyController e = other.collider.GetComponent<EnemyController>();
		if (e != null)
		{
			e.Fix();
		}
		//Debug.Log("Projectile Collision with " + other.gameObject);
		Destroy(gameObject);
	}
    // Update is called once per frame
	void Update()
	{
		if(transform.position.magnitude > 1000.0f)
		{
			Destroy(gameObject);
		}
	}
}
