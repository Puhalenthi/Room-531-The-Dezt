using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
        Debug.Log(_count);
        for (int i = 0; i < _count; i++)
        {
            problem = Instantiate(ProblemPrefab);
            problem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _deztQuestions[i];
            Debug.Log(_deztQuestions[i]);
            problemsList.Add(problem);

            //Positioning
            //575
            problem.transform.parent = this.transform.parent;
            nextPos += 10.0f;
            problem.transform.localPosition = new Vector3(problem.transform.parent.transform.localPosition.x - xOffset, nextPos, problem.transform.localPosition.z);
            nextPos += heightPerLine * 3; //Each problem is given 40 (units) or about 2 lines
        }
        Debug.Log(problemsList.Count);

    }

    // Update is called once per frame
    void Update()
    {
        if (_deztQuestions != null && _deztAnswers != null)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log(_deztAnswers.Count);
            }
        }
    }

    //Checks all the players answers against the answer key and sends them to the pass/fail scene accordingly
    public void Submit()
    {
        StartCoroutine("submitCoroutine");
    }

    IEnumerator submitCoroutine()
    {
        yield return new WaitUntil(() => problemsList.Count == _deztAnswers.Count);
        List<string> _playerInputs = new List<string>();
        for (int i = 0; i < _deztAnswers.Count; i++)
        {
            _playerInputs.Add(problemsList[i].transform.GetChild(1).gameObject.GetComponent<TMP_InputField>().text);
        }

        if (DeztUtil.CheckDezt(_playerInputs, _deztAnswers))
        {
            SceneManager.LoadScene("WinScreen");
        }
        SceneManager.LoadScene("LoseScreen");
    }
}
