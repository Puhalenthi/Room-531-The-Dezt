using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StudentDController : MonoBehaviour
{
    private System.Random rnd = new System.Random();
    
    private List<string> _deztQuestions;
    private List<string> _deztAnswers;

    private List<GameObject> problemsList;

    public GameObject ProblemPrefab; //Prefab used
    public GameObject TestPaperImage; //The image of the paper itself
    public GameObject instructions;

    private GameObject problem; //The Whole Problem
    private GameObject question; //Question
    private GameObject answer; 
    private int _count;//Count of problems in dezt

    private float heightPerLine = 20f;
    private float lengthOfPaper;
    private float instructionsPosition;
    private float nextPos;
    private float xOffset = 80f;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitUntil(() => PlayerDezt.Instance.DeztQuestions.Count != 0);
        //Setup
        _deztQuestions = PlayerDezt.Instance.DeztQuestions;
        _deztAnswers = PlayerDezt.Instance.DeztAnswers;
        _count = _deztQuestions.Count;
        problemsList = new List<GameObject>(_count);

        lengthOfPaper = TestPaperImage.transform.localScale.y;
        instructionsPosition = (lengthOfPaper / 2 - instructions.transform.localPosition.y + instructions.transform.localScale.y / 2);
        nextPos = instructionsPosition;

        for (int i = 0; i < _count; i++) 
        {
            problem = Instantiate(ProblemPrefab);
            problem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _deztQuestions[i];
            problem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = GenWrongAnswer(_deztAnswers[i]);
            Debug.Log(_deztQuestions[i]);
            problemsList.Add(problem);

            //Positioning
            problem.transform.parent = this.transform.parent;
            nextPos += 10.0f;
            problem.transform.localPosition = new Vector3(problem.transform.parent.transform.localPosition.x - xOffset, nextPos, problem.transform.localPosition.z);
            nextPos += heightPerLine * 3; //Each problem is given 40 (units) or about 2 lines
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private string GenWrongAnswer(string correctAnswer)
    {
        List<string> answerList = correctAnswer.Split(", ").ToList();
        List<string> newAnswerList = new List<String> ();
        int randNum;
        for(int i = 0; i < answerList.Count; i ++)
        {
            randNum = rnd.Next(1, 3); //50% probability
            if (randNum == 1)
            {
                newAnswerList.Add(answerList[i]);
            }
        }
        return String.Join(", ", newAnswerList);
    }
}
