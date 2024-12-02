using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerDController : MonoBehaviour
{
    private List<string> _deztQuestions;
    private List<string> _deztAnswers;

    private List<GameObject> problemsList;

    public GameObject ProblemPrefab; //Prefab used
    public GameObject TestPaperImage; //The image of the paper itself
    public GameObject instructions;

    private GameObject problem; //Main body
    private GameObject question; //Question
    private GameObject answerField; //Answer Box
    private int _count; //count of problems in dezt

    private float heightPerLine = 20f;
    private float lengthOfPaper;
    private float instructionsPosition;
    private float nextPos;

    // Start is called before the first frame update
    void Start()
    {
        new WaitForSeconds(2);
        //Setup
        _deztQuestions = PlayerDezt.Instance.DeztQuestions;
        _deztAnswers = PlayerDezt.Instance.DeztAnswers;
        _count = _deztQuestions.Count;
        problemsList = new List<GameObject>(_count);

        lengthOfPaper = TestPaperImage.transform.localScale.y;
        instructionsPosition = (lengthOfPaper / 2 - instructions.transform.localPosition.y + instructions.transform.localScale.y / 2);
        nextPos = instructionsPosition;
        Debug.Log(_count);
        for (int i = 0; i < _count; i++)
        {
            problem = Instantiate(ProblemPrefab);
            problem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _deztQuestions[i];
            Debug.Log(_deztQuestions[i]);
            problemsList.Add(problem);

            //Positioning
            //575
            nextPos += 10.0f;
            problem.transform.localPosition = new Vector3(problem.transform.localPosition.x, nextPos, problem.transform.localPosition.z);
            nextPos += heightPerLine * 2; //Each problem is given 40 (units) or about 2 lines
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
