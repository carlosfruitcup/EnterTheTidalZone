using System.Collections;
using UnityEngine;
/// <summary>This is a broken BaseEnemy derivative, supposed to rock back and forth.
/// <para>This is a MonoBehaviour class based on BaseEnemy. </para>
/// <seealso cref="BaseEnemy"/>
/// </summary>
public class RockingEnemy : BaseEnemy
{
    public Vector3 force = Vector3.zero;
    public float time = 2f;
    public Rigidbody rb;
    public bool walking = false;
    public bool direction = false; //true = right
    public bool waiting = false;
    public override void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    public override void Update()
    {
        //BASE
        if(health > 0 && !GlobalVariables.global.busy)
        {
            ready = true;
        }
        else if(health <= 0 && !dying)
        {
            ready = false;
            onDeath.Invoke();
            if(destroyOnDeath)
                Destroy(gameObject);
            else
                StartCoroutine(Death());
        }
        //END BASE
        if(ready && !walking)
        {
            walking = true;
            StartCoroutine(WaitAndWalk());
        }
    }
    IEnumerator WaitAndWalk()
    {
        Vector3 force2 = force;
        if(!direction) force2 = new Vector3(force.x*-1,force.y,force.z);
        waiting = false;
        StartCoroutine(StillWaiting());
        while(!waiting)
        {
            Debug.Log("hello hello");
            rb.AddForce(force2);
            yield return null;
        }
        yield return null;
    }
    IEnumerator StillWaiting()
    {
        yield return new WaitForSeconds(time);
        rb.velocity = Vector3.zero;
        direction = !direction;
        waiting = true;
        StartCoroutine(WaitAndWalk());
    }
}
