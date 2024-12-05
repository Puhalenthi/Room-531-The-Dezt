using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeztUtil : MonoBehaviour
{
    private static System.Random _rnd = new System.Random();

    public static (List<int> question, List<int> answer) GenDeztQuestion()
    {
        int _noOfRoots = _rnd.Next(2, 6);
        List<int> _roots = new List<int>(_noOfRoots);
        for (int i = 0; i < _noOfRoots; i++)
        {
            _roots.Add(_rnd.Next(22) - 11); //(-10, 10)
        }
        List<int> _polynomial = new List<int> { 1 };
        int[] _pastPolynomial;
        foreach (int _root in _roots)
        {
            _pastPolynomial = new int[_polynomial.Count];
            _polynomial.CopyTo(_pastPolynomial);
            _polynomial.Add(1);
            _polynomial[0] *= -_root;
            for (int _degree = 1; _degree < _polynomial.Count - 1; _degree++)
            {
                _polynomial[_degree] = _pastPolynomial[_degree - 1] - _root * _polynomial[_degree];
            }
        }
        return (_polynomial, _roots);
    }

    public static string FormatDeztQuestion(List<int> _deztPolynomial) //Formats the polynomial for input into TextMeshPro
    {
        string _formattedQuestion = "";
        string _term;

        _term = "x<sup>" + (_deztPolynomial.Count - 1) + "</sup>";
        switch (_deztPolynomial[_deztPolynomial.Count - 1])
        {
            case -1:
                _formattedQuestion += "-" + _term;
                break;
            case 1:
                _formattedQuestion += _term;
                break;
            default:
                _formattedQuestion += _deztPolynomial[_deztPolynomial.Count - 1] + _term;
                break;
        }
        for (int i = _deztPolynomial.Count - 2; i > 0; i--)
        {
            if (i == 1)
            {
                _term = "x";
            }
            else
            {
                _term = "x<sup>" + i + "</sup>";
            }
            switch (_deztPolynomial[i])
            {
                case 0:
                    break;
                case -1:
                    _formattedQuestion += " - " + _term;
                    break;
                case 1:
                    _formattedQuestion += " + " + _term;
                    break;
                case < 0:
                    _formattedQuestion += " - " + (-_deztPolynomial[i]) + _term;
                    break;
                default: //coeff is positive
                    _formattedQuestion += " + " + _deztPolynomial[i] + _term;
                    break;
            }
        }
        switch (_deztPolynomial[0])
        {
            case < 0:
                _formattedQuestion += " - " + (-_deztPolynomial[0]);
                break;
            case 0:
                break;
            case > 0:
                _formattedQuestion += " + " + _deztPolynomial[0];
                break;
        }
        return _formattedQuestion;
    }

    public static List<string> FormatDeztQuestion(List<int> _deztPolynomial, List<int> _roots)
    {
        string _formattedQuestion = FormatDeztQuestion(_deztPolynomial);
        string _formattedAnswer = String.Join(", ", _roots.ToArray());
        return new List<string> { _formattedQuestion, _formattedAnswer };
    }

    public static Boolean CheckDezt(List<string> _userAnswers, List<string> _actualAnswers)
    {
        HashSet<string> _singleUserAnswer;
        HashSet<string> _singleActualAnswer;
        for (int i = 0; i < _userAnswers.Count; i++)
        {
            _singleUserAnswer = new HashSet<string>(_userAnswers[i].Split(", "));
            _singleActualAnswer = new HashSet<string>(_actualAnswers[i].Split(", "));
            if (!_singleUserAnswer.SetEquals(_singleActualAnswer)) { return false; }
        }
        return true;
    }
}

