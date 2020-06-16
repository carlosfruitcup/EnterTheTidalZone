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
    public TimeBody timeBody;
    public bool ready = false;
    private bool dying = false;

    void Awake()
    {
        timeBody = GetComponent<TimeBody>();
    }

    void Update()
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
    
    IEnumerator Death()
    {
        dying = true;
        yield return null;
    }
}
