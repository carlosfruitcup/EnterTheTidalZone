using System.Collections;
using UnityEngine;
using UnityEngine.Events;
/// <summary>This is used for enemies to share code with each other, it is not an enemy type.
/// <para>This is a MonoBehaviour class. </para>
/// <seealso cref="SpongeBob"/> 
/// </summary>
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
