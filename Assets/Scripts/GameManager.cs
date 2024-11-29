using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject TeacherPrefab;
    public GameObject DoorWay;

    public AudioSource audioSource;
    public AudioClip[] audioClipList;
    public AudioClip JumpScareClip;
    public float VolumeLevel;

    public float StartingTeacherSpawnDelay;
    public float AfterWarningSoundDelay;
    public float WarningSoundDuration;

    private float teacherSpawnDelay;
    public float MinSpawnDelay;
    private int teacherSpawnCount;

    // Start is called before the first frame update
    void Start()
    {
        teacherSpawnDelay = StartingTeacherSpawnDelay;
        teacherSpawnCount = 0;
        StartCoroutine("teacherSpawn");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator teacherSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(teacherSpawnDelay);
            
            // Warning sound
            audioSource.PlayOneShot(GetRandomClip(), VolumeLevel);
            yield return new WaitForSeconds(WarningSoundDuration);
            audioSource.Stop();
            yield return new WaitForSeconds(AfterWarningSoundDelay);

            // Teacher spawns in
            GameObject newTeacher = Instantiate(TeacherPrefab, DoorWay.transform.position, Quaternion.Euler(0, 90, 0));
            TeacherMovement newTeacherScript = newTeacher.GetComponent<TeacherMovement>();
            newTeacherScript.Player = Player;
            newTeacherScript.DoorWay = DoorWay;
            
            teacherSpawnCount++;
            if (teacherSpawnCount % 5 == 0 && teacherSpawnDelay > MinSpawnDelay)
            {
                teacherSpawnDelay -= 0.2f;
            }

            //Jump scare sound
            audioSource.PlayOneShot(JumpScareClip);
        }
    }

    private AudioClip GetRandomClip()
    {
        return audioClipList[Random.Range(0, audioClipList.Length)];
    }
}
