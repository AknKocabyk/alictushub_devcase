using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb; //Karakterimizin Rigidbody bileşeni
    [SerializeField] private FixedJoystick joystick; //Fixed Joystick bileşeni
    [SerializeField] private Animator animator; //Karakterimizin Animator bileşeni

    [SerializeField] private float moveSpeed; //Karakterimizin hareket hızı değeri

    
    private void FixedUpdate()
    {
        rb.velocity = new Vector3(joystick.Horizontal * moveSpeed, 
            rb.velocity.y, joystick.Vertical * moveSpeed);//, joystick ile alınayatay ve dikey girişleri kullanarak karakterin hareketini kontrol et

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);//Karakterimizin rotasyonunu joystick input değerine göre değiştir
            animator.SetBool("isRunning", true); // Koşma animasyon parametresini true yap
        }
        else
            animator.SetBool("isRunning", false);// Koşma animasyon parametresini false yap

    }
}
