using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeztUtil : MonoBehaviour
{
    private static System.Random rnd = new System.Random();

    public static (List<int> question, List<int> answer) GenDeztQuestion()
    {
        int noOfRoots = rnd.Next(2, 6);
        List<int> roots = new List<int>(noOfRoots);
        for (int i = 0; i < noOfRoots; i++)
        {
            roots.Add(rnd.Next(22) - 11); //(-10, 10)
        }
        List<int> polynomial = new List<int> { 1 };
        int[] pastPolynomial;
        foreach (int root in roots)
        {
            pastPolynomial = new int[polynomial.Count];
            polynomial.CopyTo(pastPolynomial);
            polynomial.Add(1);
            polynomial[0] *= -root;
            for (int degree = 1; degree < polynomial.Count - 1; degree++)
            {
                polynomial[degree] = pastPolynomial[degree - 1] - root * polynomial[degree];
            }
        }
        return (polynomial, roots);
    }

    public static string FormatDeztQuestion(List<int> deztPolynomial) //Formats the polynomial for input into TextMeshPro
    {
        string formattedQuestion = "";
        string term;

        term = "x<sup>" + (deztPolynomial.Count - 1) + "</sup>";
        switch (deztPolynomial[deztPolynomial.Count - 1])
        {
            case -1:
                formattedQuestion += "-" + term;
                break;
            case 1:
                formattedQuestion += term;
                break;
            default:
                formattedQuestion += deztPolynomial[deztPolynomial.Count - 1] + term;
                break;
        }
        for (int i = deztPolynomial.Count - 2; i > 0; i--)
        {
            if (i == 1)
            {
                term = "x";
            }
            else
            {
                term = "x<sup>" + i + "</sup>";
            }
            switch (deztPolynomial[i])
            {
                case 0:
                    break;
                case -1:
                    formattedQuestion += " - " + term;
                    break;
                case 1:
                    formattedQuestion += " + " + term;
                    break;
                case < 0:
                    formattedQuestion += " - " + (-deztPolynomial[i]) + term;
                    break;
                default: //coeff is positive
                    formattedQuestion += " + " + deztPolynomial[i] + term;
                    break;
            }
        }
        switch (deztPolynomial[0])
        {
            case < 0:
                formattedQuestion += " - " + (-deztPolynomial[0]);
                break;
            case 0:
                break;
            case > 0:
                formattedQuestion += " + " + deztPolynomial[0];
                break;
        }
        return formattedQuestion;
    }

    public static List<string> FormatDeztQuestion(List<int> deztPolynomial, List<int> roots)
    {
        string formattedQuestion = FormatDeztQuestion(deztPolynomial);
        string formattedAnswer = String.Join(", ", roots.ToArray());
        return new List<string> { formattedQuestion, formattedAnswer };
    }

    public static Boolean CheckDezt(List<string> userAnswers, List<string> actualAnswers)
    {
        HashSet<string> singleUserAnswer;
        HashSet<string> singleActualAnswer;
        for (int i = 0; i < userAnswers.Count; i++)
        {
            singleUserAnswer = new HashSet<string>(userAnswers[i].Split(", "));
            singleActualAnswer = new HashSet<string>(actualAnswers[i].Split(", "));
            if (!singleUserAnswer.SetEquals(singleActualAnswer)) { return false; }
        }
        return true;
    }
}
