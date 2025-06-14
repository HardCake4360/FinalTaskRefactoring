using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    private Player player;
    // Update is called once per frame
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed);
        player = Player.GetInstance();
        Destroy(gameObject, 10f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "Player") return;
        player.GetDemaged(damage);
        Destroy(gameObject,0.5f);
    }
}
