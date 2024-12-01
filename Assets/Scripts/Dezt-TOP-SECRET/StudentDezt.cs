using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentDezt : MonoBehaviour //This will be a singleton
{
    public static StudentDezt Instance {get; private set;}
    
    [SerializeField]
    private static int _noQuestions = 10;
    private string _studentName;
    public List<string> DeztQuestions  {get; private set;} = new List<string> (_noQuestions);
    public List<string> DeztAnswers  {get; private set;} = new List<string> (_noQuestions);
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    
    void OnEnable()
    {
        StartCoroutine(GetQuestions());
    }
    
    private IEnumerator GetQuestions() //Gets the questions and answers for the tests
    {
        yield return new WaitUntil(() => StudentDezt.Instance != null); //Waits until this instance is instantiated
        
        for (int i = 0; i < _noQuestions; i ++) 
        {
            (List<int> lQuestion, List<int> lAnswer) = DeztGen.GenDeztQuestion();
            List<string> formattedQuestion = DeztGen.FormatDeztQuestion(lQuestion, lAnswer);
            if (i == _noQuestions - 1)
            {
                Debug.Log(formattedQuestion[0]);
            }
            DeztQuestions[i] = formattedQuestion[0];
            DeztAnswers[i] = formattedQuestion[1];
        }
    }
}
