using System.Collections;
using UnityEngine;
using System;
/// <summary>This is used as an enemy type known as the 'Bolter'. It slides back and forth and destroys itself upon hurting the player.
/// <para>This is a BaseEnemy class. </para>
/// <seealso cref="BaseEnemy"/> 
/// </summary>
public class MoveEnemy1 : BaseEnemy
{
	public float benefitOfTheDoubtSeconds = 0.1f;
	public float thrust = 1.0f;
    public Rigidbody rb;
	public bool direction = false; //false = left
	private bool benefitOfTheDoubt = false;
    public override void Awake()
    {
		//BASE
		timeBody = GetComponent<TimeBody>();
		//END BASE
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    public void FixedUpdate()
    {
		Vector3 dir;
		if(direction)
		{
			dir = transform.right;
			transform.localScale = new Vector3(0.25f,0.25f,0.25f);
		}
		else
		{
			dir = transform.right*-1;
			transform.localScale = new Vector3(-0.25f,0.25f,0.25f);
		}
		rb.AddForce(dir * thrust);
    }
	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.layer == 10 && !benefitOfTheDoubt) //wall layer
		{
			benefitOfTheDoubt = true;
			direction = !direction;
			StartCoroutine(WaitForIt());
		}
		else if(col.gameObject.name.EndsWith("Spongebob"))
		{
			SpongeBob spongeBob = col.gameObject.GetComponent<SpongeBob>();
			if(!spongeBob.invincible)
            {
				spongeBob.health -= damage;
                onDamage.Invoke();
				health = 0;
            }
		}
    }
	void OnCollisionStay(Collision col) //in case
	{
		if(col.gameObject.name.EndsWith("Spongebob"))
		{
			SpongeBob spongeBob = col.gameObject.GetComponent<SpongeBob>();
			if(!spongeBob.invincible)
            {
                spongeBob.health -= damage;
                onDamage.Invoke();
				health = 0;
            }
		}
	}
	IEnumerator WaitForIt()
	{
		if(benefitOfTheDoubt)
		{
			yield return new WaitForSeconds(benefitOfTheDoubtSeconds);
			benefitOfTheDoubt = false;
		}
	}
}
