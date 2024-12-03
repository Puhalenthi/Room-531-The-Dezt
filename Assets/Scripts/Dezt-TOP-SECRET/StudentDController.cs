using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StudentDController : MonoBehaviour
{
    private List<string> _deztQuestions;
    private List<string> _deztAnswers;

    private List<GameObject> problemsList;

    public GameObject ProblemPrefab; //Prefab used
    public GameObject TestPaperImage; //The image of the paper itself

    private GameObject problem; //The Whole Problem
    private int count;//Count of problems in dezt

    // Start is called before the first frame update
    void Start()
    {
        //Setup
        _deztQuestions = PlayerDezt.Instance.DeztQuestions;
        _deztAnswers = PlayerDezt.Instance.DeztAnswers;
        count = _deztQuestions.Count;
        problemsList = new List<GameObject>(count); 

        for (int i = 0; i < count; i++) 
        {
            problem = Instantiate(ProblemPrefab);
            problemsList.Add(problem);

            //Positioning

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
