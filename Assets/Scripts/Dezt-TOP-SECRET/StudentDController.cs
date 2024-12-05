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
    private enum _colorText { Blue, Tarnished, Bloody };
    private _colorText _currColor;
    private int _randNum;

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
            problemsList.Add(problem);

            //Positioning
            problem.transform.parent = this.transform.parent;
            nextPos += 10.0f;
            problem.transform.localPosition = new Vector3(problem.transform.parent.transform.localPosition.x - xOffset, nextPos, problem.transform.localPosition.z);
            nextPos += heightPerLine * 3; //Each problem is given 40 (units) or about 2 lines

            //Coloring (is reversed because order of polynomials is reversed)
            _randNum = rnd.Next(0, 3);
            if (_currColor == _colorText.Blue)
            {
                problem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = new Color32(120, 6, 6, 255); //Color of blood
                if (_randNum == 0) //33% chance
                {
                    _currColor = _colorText.Tarnished;
                }
            }
            else if (_currColor == _colorText.Tarnished)
            {
                problem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = new Color32(102, 2, 60, 255); //Color of Tyrian Purple (Blue + Blood Red)
                if (_randNum == 0) //33% chance
                {
                    _currColor = _colorText.Bloody;
                }
            }
            else
            {
                problem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = new Color32(87, 3, 239, 255); //Color of a Bic ballpoint blue pen
            }
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
        int _randNum;
        for(int i = 0; i < answerList.Count; i ++)
        {
            _randNum = rnd.Next(1, 4); 
            if (_randNum == 1) //40% probability
            {
                newAnswerList.Add(answerList[i]);
            }
        }
        return String.Join(", ", newAnswerList);
    }
}
