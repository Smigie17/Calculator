using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;





public class Calculator : MonoBehaviour
{
    private List<string> calcReturns = new List<string>();
    private int curRet = 0;

    public Text mainCalcText;
    public Text secondaryCalcText;

    public static int MAX_CALC_NUMS = 10;



    // Start is called before the first frame update
    void Start()
    {
        calcReturns.Insert(0, "");
    }

    void UpdateCalcText()
    {
        

        mainCalcText.text = calcReturns[curRet];
        if(calcReturns.Count >1 && curRet+1 <calcReturns.Count)
        {
            secondaryCalcText.text = calcReturns[curRet+1];
        }

    }


    public void Number(int num)
    {
        if (calcReturns[curRet].Length >= MAX_CALC_NUMS)
        {
            return;
        }
        calcReturns[curRet] += num;
        UpdateCalcText();
    }
    public void Func(string str)
    {
        if(calcReturns[curRet].Length >= MAX_CALC_NUMS)
        {
            return;
        }
        calcReturns[curRet] += str;
        UpdateCalcText();
    }

    public void Up()
    {
        if(curRet != calcReturns.Count-1)
        {
            curRet++;
        }
        UpdateCalcText();
    }

    public void Down()
    {
        if (curRet != 0)
        {
            curRet--;
        }
        UpdateCalcText();
    }

    public void Solve()
    {
        int indx = 0;
        float total = 0;
        string error = "";

        string curString = calcReturns[0];

        float tempNum = 0;

        string tempFunc = "";


        while (indx < curString.Length)
        {
            if(System.Char.IsDigit(curString[indx]) )
            {
                int startIdx = indx;
                int cnt = 0;
                while (indx < curString.Length ){

                    if(!System.Char.IsDigit(curString[indx]))
                    {

                        break;
                    }
                    indx++;
                    cnt++;

                }
                int endIdx = indx;

 
                //converts string of num chars to a total number
                float.TryParse(curString.Substring(startIdx, cnt), out tempNum);

                if(tempFunc=="")
                {
                    total = tempNum;

                }
                else
                {
                    try
                    {
                        total = runFunc(tempFunc, total, tempNum);
                    }
                    catch (System.Exception e)
                    {
                        error = e.Message;
                        break;
                    }

                    tempFunc = "";
                }
                if(indx == curString.Length-1)
                {
                    break;
                }
                continue;


            }
            else
            {
                if(tempFunc.Length!=0)
                {
                    error = "INVALID";
                    break;
                }
                tempFunc = curString[indx] + "";
                indx++;
            }


        }

        if (error != "")
        {
            calcReturns.Insert(0, error);
        }
        else
        {
            calcReturns.Insert(0, "" + total);
        }
        UpdateCalcText();
    }




    public float runFunc(string function, float n1,float n2)
    {
        switch(function)
        {
            case "+":
                return Add(n1,n2);
               
            case "-":
                return Subtract(n1, n2);
                ;
            case "/":
                return Divide(n1, n2);
                

            case "*":
                return Multiply(n1, n2);

            
        }
        return 0;
    }



    public float Add(float n1, float n2)
    {
        return (n1 + n2);

    }
    public float Subtract(float n1, float n2)
    {
        return (n1 - n2);

    }
    public float Divide(float n1, float n2)
    {
        if(n2 ==0)
        {
            throw new System.Exception("NaN");
        }
        return (n1 / n2);

    }
    public float Multiply(float n1, float n2)
    {
        return (n1 * n2);

    }
    public void Clear()
    {
        calcReturns[curRet] = "";
        calcReturns.RemoveRange(0, curRet);
        curRet = 0;
        UpdateCalcText();
    }
}
