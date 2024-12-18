using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDezt : MonoBehaviour //This will be a singleton
{
    public static PlayerDezt Instance {get; private set;}
    
    [SerializeField]
    private static int _noQuestions = 10;
    public string StudentName {get; set;}
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
    
    private void OnEnable()
    {
        StartCoroutine(GetQuestions());
    }
    
    private IEnumerator GetQuestions() //Gets the questions and answers for the tests
    {
        yield return new WaitUntil(() => PlayerDezt.Instance != null); //Waits until this instance is instantiated
        
        for (int i = 0; i < _noQuestions; i ++) 
        {
            (List<int> _lQuestion, List<int> _lAnswer) = DeztUtil.GenDeztQuestion();
            List<string> _formattedQuestion = DeztUtil.FormatDeztQuestion(_lQuestion, _lAnswer);
            Instance.DeztQuestions.Add(_formattedQuestion[0]);
            Instance.DeztAnswers.Add(_formattedQuestion[1]);
        }
    }
}
