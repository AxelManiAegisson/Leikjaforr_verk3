using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 moveVector;
    public AudioSource triggerSound;
    private float speed = 5.0f;
    private float verticalVelocity = 0.0f;
    private float gravity = 12.0f;
    private bool isDead = false;
    public Text Score;
    private int counter = 0;
    private float animDuration = 3.0f;
    private float startTime;
    public Animator Anim;
    public DeathMenu deathMenu;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        triggerSound = GetComponent<AudioSource>();
        counter = 0;
        SetScore();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead)
        {
            return;
        }

        //Gerir animation í nokkrar sek og ekki er hægt að gera neitt fyrr en það er búið
        if(Time.time - startTime < animDuration)
        {
            controller.Move(Vector3.forward * speed * Time.deltaTime);
            return;
        }

        if(moveVector.y < -10)
        {
            Death();
        }

        //Kíkir ef spilarinn er grounded ef ekki þá byrjar hann að detta
        if (controller.isGrounded)
        {
            verticalVelocity = -0.5f;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        moveVector = Vector3.zero;

        // x Lætur character ná að hlaupa til hægri og vinstri
        moveVector.x = Input.GetAxisRaw("Horizontal") * speed;
        // y ÞyngdarAf
        moveVector.y = verticalVelocity;
        // z Lætur character hlaupa í sömu áttina
        moveVector.z = speed;

        controller.Move((moveVector) * Time.deltaTime);
        //Anim.Play("RUN00f");
    }
    //Ef spilarinn fer yfir pening þá fær hann stig
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            triggerSound.Play();
            other.gameObject.SetActive(false);
            counter = counter + 1;
            SetScore();
        }
    }
    //Birtir textann
    void SetScore()
    {
        Score.text = "Stig: " + counter.ToString();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.point.z > transform.position.z + controller.radius/2)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        deathMenu.ToggleEndMenu(counter);
    }


}
