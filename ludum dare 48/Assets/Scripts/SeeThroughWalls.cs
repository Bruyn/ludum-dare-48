using System.Collections.Generic;
using UnityEngine;

public class SeeThroughWalls : MonoBehaviour
{
    private List<GameObjectWithAlpha> _gameObjectWithAlphaList = new List<GameObjectWithAlpha>();

    public float sphereCastRadius = 0.3f;
    public float alpha = 0f;

    private RaycastHit[] results = new RaycastHit[10];

    void Update()
    {
        foreach (GameObjectWithAlpha gameObjectWithAlpha in _gameObjectWithAlphaList)
        {
            ChangeAlpha(gameObjectWithAlpha.GameObject, gameObjectWithAlpha.Alpha);
        }

        _gameObjectWithAlphaList.Clear();

        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 diff = ((transform.position) - cameraPosition);

        LayerMask mask = LayerMask.GetMask("Wall");
        var size = Physics.SphereCastNonAlloc(cameraPosition, sphereCastRadius, diff.normalized, results,
            diff.magnitude - sphereCastRadius, mask);
        
        for (int i = 0; i < size; i++)
        {
            RaycastHit hit = results[i];
            if (hit.collider.gameObject.CompareTag("Wall"))
            {
                GameObjectWithAlpha gameObjectWithAlpha = new GameObjectWithAlpha();
                gameObjectWithAlpha.GameObject = hit.collider.gameObject;
                gameObjectWithAlpha.Alpha = ChangeAlpha(gameObjectWithAlpha.GameObject, alpha);
                _gameObjectWithAlphaList.Add(gameObjectWithAlpha);
            }
        }
    }

    float ChangeAlpha(GameObject go, float alpha)
    {
        Material mat = go.GetComponent<Renderer>().material;
        Color color = mat.color;
        float temp = color.a;
        color.a = alpha;
        mat.SetColor("_Color", color);
        return temp;
    }
}

class GameObjectWithAlpha
{
    public GameObject GameObject;
    public float Alpha;
}