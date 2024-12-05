using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StudentDController : MonoBehaviour
{
    private System.Random _rnd = new System.Random();
    
    private List<string> _deztQuestions;
    private List<string> _deztAnswers;

    private List<GameObject> _problemsList;
    private enum _colorText { Blue, Tarnished, Bloody };
    private _colorText _currColor;
    private int _randNum;

    public GameObject ProblemPrefab; //Prefab used
    public GameObject TestPaperImage; //The image of the paper itself
    public GameObject Instructions;

    private GameObject _problem; //The Whole problem
    private int _count;//Count of problems in dezt

    private float _heightPerLine = 20f;
    private float _lengthOfPaper;
    private float _instructionsPosition;
    private float _nextPos;
    private float _xOffset = 80f;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitUntil(() => PlayerDezt.Instance.DeztQuestions.Count != 0);
        //Setup
        _deztQuestions = PlayerDezt.Instance.DeztQuestions;
        _deztAnswers = PlayerDezt.Instance.DeztAnswers;
        _count = _deztQuestions.Count;
        _problemsList = new List<GameObject>(_count);

        _lengthOfPaper = TestPaperImage.transform.localScale.y;
        _instructionsPosition = (_lengthOfPaper / 2 - Instructions.transform.localPosition.y + Instructions.transform.localScale.y / 2);
        _nextPos = _instructionsPosition;

        for (int i = 0; i < _count; i++) 
        {
            _problem = Instantiate(ProblemPrefab);
            _problem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _deztQuestions[i];
            _problem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = GenWrongAnswer(_deztAnswers[i]);
            _problemsList.Add(_problem);

            //Positioning
            _problem.transform.parent = this.transform.parent;
            _nextPos += 10.0f;
            _problem.transform.localPosition = new Vector3(_problem.transform.parent.transform.localPosition.x - _xOffset, _nextPos, _problem.transform.localPosition.z);
            _nextPos += _heightPerLine * 3; //Each problem is given 40 (units) or about 2 lines

            //Coloring (is reversed because order of polynomials is reversed)
            _randNum = _rnd.Next(0, 3);
            if (_currColor == _colorText.Blue)
            {
                _problem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = new Color32(120, 6, 6, 255); //Color of blood
                if (_randNum == 0) //33% chance
                {
                    _currColor = _colorText.Tarnished;
                }
            }
            else if (_currColor == _colorText.Tarnished)
            {
                _problem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = new Color32(102, 2, 60, 255); //Color of Tyrian Purple (Blue + Blood Red)
                if (_randNum == 0) //33% chance
                {
                    _currColor = _colorText.Bloody;
                }
            }
            else
            {
                _problem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = new Color32(24, 3, 255, 255); //Color of a Bic ballpoint blue pen
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
            _randNum = _rnd.Next(1, 4); 
            if (_randNum == 1) //40% probability
            {
                newAnswerList.Add(answerList[i]);
            }
        }
        return String.Join(", ", newAnswerList);
    }
}
