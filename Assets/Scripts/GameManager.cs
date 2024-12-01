using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public GameObject Player;
    private PlayerController playerController;
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

    public Teacher[] teacherTypes;
    

    // Start is called before the first frame update
    void Start()
    {
        Player = Instantiate(PlayerPrefab, new Vector3(10.0f, 1.0f, 10.0f), Quaternion.identity);
        playerController = Player.GetComponent<PlayerController>();

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
            newTeacherScript.TeacherType = teacherTypes[Random.Range(0, teacherTypes.Length)];
            
            teacherSpawnCount++;
            if (teacherSpawnCount % 5 == 0 && teacherSpawnDelay > MinSpawnDelay)
            {
                teacherSpawnDelay -= 0.2f;
            }

            //Jump scare sound
            audioSource.PlayOneShot(JumpScareClip);

            //Checks if the player is hiding/safe (won't chase the player in that instance)
            if (playerController.IsHiding || playerController.IsSitting)
            {
                Debug.Log("test");
                newTeacherScript.followPlayer = false;
                newTeacherScript.Despawn(8);
            }
        }
    }

    private AudioClip GetRandomClip()
    {
        return audioClipList[Random.Range(0, audioClipList.Length)];
    }
}
