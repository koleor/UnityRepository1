using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public   float speed = 0.9f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      

    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody my_rigidbody = gameObject.GetComponent<Rigidbody>();
        Debug.Log(my_rigidbody.linearVelocity);
    }
}
