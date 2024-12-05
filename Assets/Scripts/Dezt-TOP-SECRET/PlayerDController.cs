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

    private List<GameObject> _problemsList;

    public GameObject ProblemPrefab; //Prefab used
    public GameObject TestPaperImage; //The image of the paper itself
    public GameObject Instructions;
    public GameObject NameField; //This is the name field on the dezt icon
    private string _playerNameFieldText; //Name field on the actual dezt

    private GameObject _problem; //Main body
    private int _count; //count of problems in dezt

    
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
            _problemsList.Add(_problem);

            //Positioning
            //575
            _problem.transform.parent = this.transform.parent;
            _nextPos += 10.0f;
            _problem.transform.localPosition = new Vector3(_problem.transform.parent.transform.localPosition.x - _xOffset, _nextPos, _problem.transform.localPosition.z);
            _nextPos += _heightPerLine * 3; //Each problem is given 40 (units) or about 2 lines

        }

    }

    // Update is called once per frame
    void Update()
    {
       _playerNameFieldText = GameObject.Find("PlayerNameField").GetComponent<TMP_InputField>().text;
       if (_playerNameFieldText != "")
       {
           NameField.GetComponent<TextMeshProUGUI>().text = _playerNameFieldText;
       }
    }

    //Checks all the players answers against the answer key and sends them to the pass/fail scene accordingly
    public void Submit()
    {
        StartCoroutine("submitCoroutine");
    }

    IEnumerator submitCoroutine()
    {
        yield return new WaitUntil(() => _problemsList.Count == _deztAnswers.Count);
        List<string> _playerInputs = new List<string>();
        for (int i = 0; i < _deztAnswers.Count; i++)
        {
            _playerInputs.Add(_problemsList[i].transform.GetChild(1).gameObject.GetComponent<TMP_InputField>().text);
        }

        if (DeztUtil.CheckDezt(_playerInputs, _deztAnswers))
        {
            SceneManager.LoadScene("WinScreen");
        }
        SceneManager.LoadScene("LoseScreen");
    }
}
