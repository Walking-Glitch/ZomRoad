using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class GroundCheck : MonoBehaviour
{
    private WheelController player;
    [SerializeField] private bool isResetting;
    [SerializeField] private float cooldownTime;
    [SerializeField] private float dropTime;
    [SerializeField] private float elapseTime;
    private GameManager gameManager;

    void Start()
    {
        player = GetComponentInParent<WheelController>();
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
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
            isResetting = false;
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
        //yield return new WaitForSeconds(cooldownTime);
        elapseTime = 0;
        while (elapseTime <= cooldownTime)
        {
            elapseTime += Time.deltaTime;
            yield return null;
        }

        while (!player.IsGrounded)
        {
           

                player.SetIsInvincible(true);
                player.transform.position = gameManager.roadManager.reAllocationPoint.position;
                player.transform.rotation = Quaternion.Euler(0, 0, 0);

                player.CaRigidbody.isKinematic = true;
                player.CaRigidbody.useGravity = false;

                isResetting = true;

                yield return new WaitForSeconds(dropTime);

                player.CaRigidbody.isKinematic = false;
                player.CaRigidbody.useGravity = true;

                player.SetIsInvincible(false);

                break;
 
        }
    


        //if (!player.IsGrounded)
        //{
               
        //    player.SetIsInvincible(true);
        //    player.transform.position = gameManager.roadManager.reAllocationPoint.position;
        //    player.transform.rotation = Quaternion.Euler(0, 0, 0);
                
        //    player.CaRigidbody.isKinematic = true;
        //    player.CaRigidbody.useGravity = false;

        //    isResetting = true;

        //yield return new WaitForSeconds(cooldownTime);

        //player.CaRigidbody.isKinematic = false;
        //player.CaRigidbody.useGravity = true;

        //player.SetIsInvincible(false);

        //}
            
    }

   
}
