using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float forwardForce = 50f;
    public float sidewaysForce = 1000f;
    public float maxSpeed = 10f;
    public float topSpeed = 10f;
    private bool isFalling = false;

    void FixedUpdate()
    {
        rb.AddForce(0, 0, forwardForce * Time.deltaTime);

        if (rb.velocity.magnitude > topSpeed)
            rb.velocity = rb.velocity.normalized * topSpeed;

        Controls();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            StartCoroutine(WaitForSceneLoad());
        }

        if (other.gameObject.tag == "Object")
        {
            Destroy(gameObject);
            SceneManager.LoadScene("GameOver");
        }
    }

    private IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
        SceneManager.LoadScene("GameOver");
    }

    void Controls()
    {
        if (Input.GetKey("a"))
        {
            rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey("d"))
        {
            rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.Space) && isFalling == false)
        {
            Vector3 vertVelocity = rb.velocity;
            vertVelocity.y = 5f;
            rb.velocity = vertVelocity;

            isFalling = true;
        }
    }

    void OnCollisionStay(Collision floor)
    {
        isFalling = false;
    }
}

