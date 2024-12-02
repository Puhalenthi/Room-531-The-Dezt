using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DeztController : MonoBehaviour
{
    [SerializeField] private Boolean isUser;
    private enum textColor { Typed, NormalWriting, TaintedWriting, BloodyWriting };

    private List<string> _deztQuestions;
    private List<string> _deztAnswers;
    public List<GameObject> questionsList { get; private set; }
    private List<GameObject> answerFields;

    private GameObject QuestionPrefab; //Prefab used
    private GameObject question; //Main body
    private TextMeshProUGUI questionHeader; //Question
    private GameObject answerField; //Answer Box
    private TextMeshProUGUI answer; //Answer Text

    // Start is called before the first frame update
    void Start()
    {
        //Setup
        _deztQuestions = PlayerDezt.Instance.DeztQuestions;
        _deztAnswers = PlayerDezt.Instance.DeztAnswers;
        questionsList = new List<GameObject>(_deztQuestions.Count);
        answerFields = new List<GameObject>(_deztQuestions.Count);

        if (isUser) //If the dezt is of the user
        {
            for (int index = 0; index < _deztQuestions.Count; index++)
            {
                //Generate the text and get the answer fields
                question = Instantiate(QuestionPrefab);
                questionsList.Add(question);

                questionHeader = question.GetComponentInChildren<TextMeshProUGUI>();

                /**answerFields.Add(question.GetComponentInChildren<TMP_InputField>());**/

                questionHeader.text = _deztQuestions[index];
            }
        }
        else
        {
            for (int index = 0; index < _deztQuestions.Count; index++)
            {
                //Here, the Question Prefab is composed of the question itself and a text representing the answer (rather than an answerfield)
                question = Instantiate(QuestionPrefab);
                questionsList.Add(question);

                questionHeader = question.transform.Find("QuestionHeader").GetComponent<TextMeshProUGUI>();
                answer = question.transform.Find("Answer").GetComponent<TextMeshProUGUI>();

                //Write the 

            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        /**if (Input.GetKeyDown(KeyCode.S) && isUser)//User submits his answers
        {
            List<string> userAnswers = new List<string>();
            //Getting the answers from the input fields
            for (int i = 0; i < _deztAnswers.Count; i++)
            {
                userAnswers.Add(answerFields[i].text);
            }
            if (DeztGen.CheckDezt(userAnswers, _deztAnswers))
            {
                return;
                //Add win condition
            }
            else
            {
                return;
                //Add lose condition
            }
        }**/
    }
}
