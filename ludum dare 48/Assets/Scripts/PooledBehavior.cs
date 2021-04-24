using UnityEngine;

public class PooledBehavior : MonoBehaviour
{
    public virtual void SetUp()
    {

    }

    public virtual void PickFromPool()
    {
        SetUp();
        gameObject.SetActive(true);
    }

    public virtual void ReturnToPool()
    {
        gameObject.SetActive(false);
    }

    public virtual bool IsActive()
    {
        return gameObject.activeInHierarchy;
    }
}
