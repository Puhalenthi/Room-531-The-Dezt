using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu ()]
public class BigTeacher : Teacher
{
    public BigTeacher()
    {
        movementSpeed = 0.1f;
        scale = 2f;
    }
    public override void KillPlayerAction(AudioSource audioSource)
    {
        Debug.Log("You've been killed by the big teacher!");
        audioSource.PlayOneShot(deathMessage);
    }
}
