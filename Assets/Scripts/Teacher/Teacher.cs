using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Teacher : ScriptableObject
{
    public float movementSpeed;
    public float scale;
    public AudioClip deathMessage;
    public abstract void KillPlayerAction(AudioSource audioSource);
}
