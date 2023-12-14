using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 4f; // Mermi hızı

    void FixedUpdate()
    {
        // Mermiyi ileri doğru hareket ettir
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

}
