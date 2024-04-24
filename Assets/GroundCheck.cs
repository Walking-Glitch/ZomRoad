using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class GroundCheck : MonoBehaviour
{
    private WheelController player;
    bool isResetting;
    [SerializeField] private float timeToStartRelocation = 4;
    [SerializeField] private float timeToResetIsResetting = 4;
    [SerializeField] private float timeElapsed;

    void Start()
    {
        player = GetComponentInParent<WheelController>();
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(player.IsInvincible);

        //if (!player.IsGrounded)
        //{
        //    StartCoroutine(FlipCar());
        //}

        //else
        //{
        //    player.SetIsInvincible(false);
        //}

        if (!player.IsGrounded && !isResetting)
        {
            StartCoroutine(RelocateCar());
        }

     
            
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            player.SetIsGrounded(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            player.SetIsGrounded(false);
        }
    }

    private IEnumerator RelocateCar()
    {
         timeElapsed = 0f;

        while (!player.IsGrounded && timeElapsed < timeToStartRelocation)
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        if (!isResetting && !player.IsGrounded)
        {
            isResetting = true;
            player.SetIsInvincible(true);
            Vector3 initialPos = player.transform.position;
            Vector3 newPos = new Vector3(initialPos.x, 5f, initialPos.z);
            Vector3 euler = player.transform.rotation.eulerAngles;
            euler.x = 0;
            euler.z = 0;

            player.transform.position = newPos;
            player.transform.rotation = Quaternion.Euler(euler);

            yield return new WaitForSeconds(timeToResetIsResetting);

            isResetting = false;
            player.SetIsInvincible(false); // You might want to set invincibility back to false here

        }

    }

   
}
