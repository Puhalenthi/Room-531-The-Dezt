using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu ()]
public class FastTeacher : Teacher
{
    public FastTeacher()
    {
        movementSpeed = 1f;
        scale = 1f;
    }
    public override void KillPlayerAction(AudioSource audioSource)
    {
        Debug.Log("You've been killed by the fast teacher!");
        audioSource.PlayOneShot(deathMessage);
    }
}
