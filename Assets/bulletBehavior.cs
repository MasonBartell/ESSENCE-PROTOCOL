using UnityEngine;
using System.Collections;

public class bulletBehavior : MonoBehaviour
{
    [SerializeField] private float lifetime = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime);
    }
}
