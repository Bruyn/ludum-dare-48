using System.Collections.Generic;
using UnityEngine;

public class SeeThroughWalls : MonoBehaviour
{
    private List<GameObjectWithAlpha> _gameObjectWithAlphaList = new List<GameObjectWithAlpha>();

    public float sphereCastRadius = 0.5f;
    public float alpha = 0.2f;
    void Update()
    {
        foreach (GameObjectWithAlpha gameObjectWithAlpha in _gameObjectWithAlphaList)
        {
            ChangeAlpha(gameObjectWithAlpha.GameObject, gameObjectWithAlpha.Alpha);
        }

        _gameObjectWithAlphaList.Clear();

        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 diff = (transform.position - cameraPosition);
        RaycastHit[] hits = Physics.SphereCastAll(cameraPosition, sphereCastRadius, diff.normalized, diff.magnitude - sphereCastRadius);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject.tag == "Wall")
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