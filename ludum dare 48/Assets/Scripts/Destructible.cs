using UnityEngine;

public class Destructible : MonoBehaviour
{
    public GameObject[] cells;

    private Health _health;

    private void Start()
    {
        _health = GetComponent<Health>();
        _health.OnDeath.AddOnce(HandleOnDeath);
    }

    private void OnDestroy()
    {
        _health.OnDeath.RemoveOnce(HandleOnDeath);
    }

    private void HandleOnDeath(Damage deathDamage)
    {
        Vector3 velocity = (transform.position - deathDamage.Damager.transform.position);
        velocity.Normalize();
        velocity *= 4f;

        bool isDestroyed = true;

        foreach (var cell in cells)
        {
            cell.SetActive(isDestroyed);
            Vector3 rndVector = Random(-1, 1);
            cell.GetComponent<Rigidbody>().velocity = velocity + rndVector;
        }

        GetComponent<MeshRenderer>().enabled = !isDestroyed;
        GetComponent<MeshCollider>().enabled = !isDestroyed;
    }

    public Vector3 Random(float min, float max)
    {
        return new Vector3(UnityEngine.Random.Range(min, max), UnityEngine.Random.Range(min, max),
            UnityEngine.Random.Range(min, max));
    }

    // Update is called once per frame
    void Update()
    {
    }
}