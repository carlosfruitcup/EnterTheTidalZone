using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseEnemy : MonoBehaviour
{
    public bool destroyOnDeath = true;
    public int health = 1;
    public int damage = 1;
    public UnityEvent onDeath;
	public UnityEvent onDamage;
    public TimeBody timeBody;
    public bool ready = false;
    public bool dying = false;

    public virtual void Awake()
    {
        timeBody = GetComponent<TimeBody>();
    }

    public virtual void Update()
    {
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
    }
    
    public IEnumerator Death()
    {
        dying = true;
        yield return null;
    }
}
