
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cubes : MonoBehaviour
{
    [SerializeField]
    private float screenWidth;
    [SerializeField]
    private float screenHieght;
    [SerializeField]
    private Cube prefabBoxs;
    public bool CanClick = true;
    [SerializeField]
    private Cube[,] pozitionBox;
    public int kol;
    private bool canGame = false;
    [SerializeField]
    private Text textMonets;
    private int monets = 0;
    [SerializeField]
    private Text textMoves;
    public int moves = 31;
    [SerializeField]
    private RectTransform rt;
    [SerializeField]
    private float Spacing;
    void Start()
    {

        //screenHieght = (Screen.height / 1000) + ((Screen.height / 100) * 3);
        //screenWidth = (Screen.width / 1000) + ((Screen.width / 100) * 3);
        //int x = (int)(screenWidth / 4);
        //int y = (int)(screenHieght / 4);

        //kol = 6;
        //int v = 3;
     
        //for (int i = 0; i < kol - 1; i++) {
        //    for (int j = 0; j < kol; j++)
        //    {
        //        pozitionBox[i, j] = Instantiate(prefabBoxs, new Vector3(-screenHieght + i, -screenWidth + j, 1), Quaternion.identity, transform);
        //        pozitionBox[i, j].x = i;
        //        pozitionBox[i, j].y = j;
        //        v++;
        //    }
        //}
        CreateField();
        StartCoroutine("DownBoxs");
    }

    public void CreateField()
    {
        pozitionBox = new Cube[kol - 1, kol];
        float fieldWidth = Screen.width-30 ;
        var cellSize = (fieldWidth - (Spacing * (kol-1))- Spacing)/ (kol - 1);

        float fieldHeight =(cellSize * (kol)) + (Spacing * (kol))+Spacing;

         rt.sizeDelta = new Vector2(fieldWidth, fieldHeight);

        float startX = -(fieldWidth /2) + (cellSize/2 ) + Spacing;
        float startY = ((fieldHeight/2 ) - (cellSize/2) - Spacing);
        
     //   var index = 0;
        for (int x = 0; x < kol-1; x++)
        {
            for (int y = 0; y < kol; y++)
            {
                pozitionBox[x,  y] = Instantiate(prefabBoxs, transform,false);
                pozitionBox[x,  y].transform.localPosition = new Vector2((startX + (x * cellSize))* Spacing,  (startY- ((kol-1-y) * (cellSize )) )* Spacing);
                var col = pozitionBox[x,y].GetComponent<BoxCollider2D>();
                pozitionBox[x,  y].GetComponent<RectTransform>().sizeDelta = new Vector2(cellSize, cellSize);
                col.size = new Vector2(cellSize, cellSize);
                pozitionBox[x,  y].setNewMaterial();
                pozitionBox[x, y].x =x;
                pozitionBox[x,y].y = y;
            }
        }
    }

    void Update()
    {
        DownBoxs();

    }


    private void DownBoxs()
    {
        textMoves.text ="" + moves;
        textMonets.text = "" + monets;
      
        canGame = false;
        for (int i = 0; i < kol - 1; i++)
        {
            for (int j = 0; j < kol; j++)
            {
                //Debug.Log("z" + z + "x" + 0 + " y " + j);
                if ((j + 3 < kol && pozitionBox[i, j].numberMaterial == pozitionBox[i, j + 2].numberMaterial
                    && pozitionBox[i, j].numberMaterial == pozitionBox[i, j + 3].numberMaterial)
                     || (j - 3 >= 0 && pozitionBox[i, j].numberMaterial == pozitionBox[i, j - 2].numberMaterial
                    && pozitionBox[i, j].numberMaterial == pozitionBox[i, j - 3].numberMaterial)
                  )
                {
                    canGame = true;
                    Debug.Log("canGame " + canGame + " x " + i + " y " + j + " по x ");
                    break;
                }
                Debug.Log(" i " + i + " j " + j);
                if (
                    (i + 1 < kol - 1
                    &&
                      ((j - 1 >= 0 && j + 1 < kol && pozitionBox[i, j].numberMaterial == pozitionBox[i + 1, j + 1].numberMaterial
                  && pozitionBox[i, j].numberMaterial == pozitionBox[i + 1, j - 1].numberMaterial)
                   || (j + 2 < kol && pozitionBox[i, j].numberMaterial == pozitionBox[i + 1, j + 1].numberMaterial
                  && pozitionBox[i, j].numberMaterial == pozitionBox[i + 1, j + 2].numberMaterial)
                  || (j - 2 >= 0 && pozitionBox[i, j].numberMaterial == pozitionBox[i + 1, j - 1].numberMaterial
                  && pozitionBox[i, j].numberMaterial == pozitionBox[i + 1, j - 2].numberMaterial)
                  )
                  )
                   || (i - 1 >= 0 &&
                      ((j - 1 >= 0 && j + 1 < kol && pozitionBox[i, j].numberMaterial == pozitionBox[i - 1, j + 1].numberMaterial
                   && pozitionBox[i, j].numberMaterial == pozitionBox[i - 1, j - 1].numberMaterial)
                    || (j + 2 < kol && pozitionBox[i, j].numberMaterial == pozitionBox[i - 1, j + 1].numberMaterial
                   && pozitionBox[i, j].numberMaterial == pozitionBox[i - 1, j + 2].numberMaterial)
                   || (j - 2 >= 0 && pozitionBox[i, j].numberMaterial == pozitionBox[i - 1, j - 1].numberMaterial
                   && pozitionBox[i, j].numberMaterial == pozitionBox[i - 1, j - 2].numberMaterial)))
                   )
                {
                    canGame = true;
                    Debug.Log("canGame " + canGame + " x " + i + " y " + j + " по i y ");
                    break;
                }


                if ((i + 3 < kol - 1 && pozitionBox[i, j].numberMaterial == pozitionBox[i + 2, j].numberMaterial
                               && pozitionBox[i, j].numberMaterial == pozitionBox[i + 3, j].numberMaterial)
                                || (i - 3 >= 0 && pozitionBox[i, j].numberMaterial == pozitionBox[i - 2, j].numberMaterial
                               && pozitionBox[i, j].numberMaterial == pozitionBox[i - 3, j].numberMaterial)
                               )
                {
                    canGame = true;
                    Debug.Log("canGame " + canGame + " x " + i + " y " + j + " по y ");
                    break;

                }
                if ((j + 1 < kol &&
                      ((i - 1 >= 0 && i + 1 < kol - 1 && pozitionBox[i, j].numberMaterial == pozitionBox[i + 1, j + 1].numberMaterial
                  && pozitionBox[i, j].numberMaterial == pozitionBox[i - 1, j + 1].numberMaterial)
                   || (i + 2 < kol - 1 && pozitionBox[i, j].numberMaterial == pozitionBox[i + 1, j + 1].numberMaterial
                  && pozitionBox[i, j].numberMaterial == pozitionBox[i + 2, j + 1].numberMaterial)
                  || (i - 2 >= 0 && pozitionBox[i, j].numberMaterial == pozitionBox[i - 1, j + 1].numberMaterial
                  && pozitionBox[i, j].numberMaterial == pozitionBox[i - 2, j + 1].numberMaterial))
                  )

                  || (j - 1 >= 0 &&
                    ((i - 1 >= 0 && i + 1 < kol - 1 && pozitionBox[i, j].numberMaterial == pozitionBox[i + 1, j - 1].numberMaterial
                  && pozitionBox[i, j].numberMaterial == pozitionBox[i - 1, j - 1].numberMaterial)
                   || (i + 2 < kol - 1 && pozitionBox[i, j].numberMaterial == pozitionBox[i + 1, j - 1].numberMaterial
                  && pozitionBox[i, j].numberMaterial == pozitionBox[i + 2, j - 1].numberMaterial)
                  || (i - 2 >= 0 && pozitionBox[i, j].numberMaterial == pozitionBox[i - 1, j - 1].numberMaterial
                  && pozitionBox[i, j].numberMaterial == pozitionBox[i - 2, j - 1].numberMaterial))
                  )
                   )
                {
                    canGame = true;
                    Debug.Log("canGame " + canGame + " x " + i + " y " + j + " по xj ");
                    break;
                }
            }
            if (canGame == true)
            {
                // CanClick = true;
                Debug.Log("break2");
                break;
            }
        }
        if (canGame == false)
        {
            // CanClick = false;
            ReplaceColor();
            Debug.Log("ReplaceColor");
        }
        if (moves == 0)
        {
            Debug.Log("the end");
            CanClick = false;
        }
        X();
      

        Y();
    }

    private void X()
    {
        if (CanClick == true)
        {
            for (int i = 0; i < kol - 3; i++)
            {
                for (int j = 0; j < kol; j++)
                {
                    MoveHorizontal(i, j);
                    if (CanClick == false)
                    {
                        break;
                    }
                }

                if (CanClick == false)
                {
                    break;
                }
            }
        }
    }
    private void MoveHorizontal(int i, int j)
    {
        if (pozitionBox[i, j].isSame(pozitionBox[i + 1, j]) && pozitionBox[i + 1, j].isSame(pozitionBox[i + 2, j]))
        {
            StartMoving(i, j);
        }

    }
    private void StartMoving(int startXPosition, int yPositition)
    {
        CanClick = false;

        WhiteHorizontal(startXPosition, yPositition);

        int first = pozitionBox[startXPosition, yPositition].numberMaterial;
        for (int z = startXPosition; z < kol - 1; z++)
        {
            if (first == pozitionBox[z, yPositition].numberMaterial)
            {

                int l = yPositition + 1;
                for (int i = yPositition + 1; i < kol; i++)
                {
                    monets += 1;
                    //   Debug.Log("i = " + i + "y = " + yPositition);
                    pozitionBox[z, i - 1].mergeCube(pozitionBox[z, i]);
                    l = i;

                }
                bool trueL = false;
                for (int j = l; j < kol; j++)
                {
                    trueL = true;
                    pozitionBox[z, j].setNewMaterial();

                }
                if (trueL == false)
                {

                    pozitionBox[z, kol - 1].setNewMaterial();
                }


            }
            else
            {
                break;
            }
        }
        CanClick = true;
    }
    public void WhiteHorizontal(int startXPosition, int yPositition)
    {

        for (int z = startXPosition + 1; z < kol - 1; z++)
        {
            if (pozitionBox[startXPosition, yPositition].isSame(pozitionBox[z, yPositition]))
            {
                OnMoveVertical(pozitionBox[z, yPositition]);
                //  Debug.Log("z = " + z + "y = " + yPositition);
                pozitionBox[z, yPositition].setMateral(5);

            }
            else
            {
                break;
            }
        }
        OnMoveVertical(pozitionBox[startXPosition, yPositition]);
        pozitionBox[startXPosition, yPositition].setMateral(5);

    }


    private void Y()
    {
        if (CanClick == true)
        {
            bool trueTree = false;
            for (int i = 0; i < kol - 1; i++)
            {
                for (int j = 0; j < kol; j++)
                {
                    MoveVertical(i, j, trueTree);
                }
            }
        }
    }

    private void MoveVertical(int i, int j, bool trueTree)
    {
        Debug.Log("MoveVertical" + i + " " + j);
        int k = 0;
        for (int v = j + 1; v < kol; v++)
        {
            Debug.Log("v " + v);
            if (pozitionBox[i, j].isSame(pozitionBox[i, v]))
            {
                k++;
            }
            else
            {
                if (trueTree == false && k >= 2)
                {
                    Debug.Log("k " + k);
                    trueTree = true;
                    MoveDownVertical(i, j, k);
                    break;
                }
                else
                {
                    k = 0;
                    Debug.Log("k " + k);
                }
            }
            if (trueTree != true && k >= 2)
            {
                Debug.Log("k " + k);
                trueTree = true;
                MoveDownVertical(i, j, k);

            }
        }
    }

    public void WhiteVertical(int x, int y, int k)
    {
        for (int l = y; l <= y + k; l++)
        {
            pozitionBox[x, l].setMateral(5);

        }
    }

    private void MoveDownVertical(int x, int y, int k)
    {
        Debug.Log(" MoveDownVertical " + x + y);
        CanClick = false;
        WhiteVertical(x, y, k);

        int i = y;
        for (int l = y + k + 1; l < kol; l++)
        {
            Debug.Log(" l " + l);
            for (int z = y + k; z >= i; z--)
            {
                Debug.Log("call l " + l);
                Debug.Log("call z " + z);
                monets += 1;
                pozitionBox[x, z].mergeCube(pozitionBox[x, l]);

            }
            i++;
        }

        for (int j = i; j < kol; j++)
        {
            pozitionBox[x, j].setNewMaterialNow(pozitionBox[x, j].numberMaterial);
            //  yield return new WaitForSeconds(0.5f);
        }
        CanClick = true;

    }

    public bool OnCheck(Cube first, Cube second, int SecondNumber)
    {
        if ((first.x == (second.x - 1) && first.y == second.y)
            || (first.x == (second.x + 1) && first.y == second.y)
            || (first.y == (second.y - 1) && first.x == second.x)
           || (first.y == (second.y + 1) && first.x == second.x))
        {
            Debug.Log("x");
            if (
               (second.y + 1 < kol &&
                pozitionBox[second.x, second.y + 1].numberMaterial == SecondNumber
               && second.y - 1 >= 0 && pozitionBox[second.x, second.y - 1].numberMaterial == SecondNumber)
               ||
               (second.y + 2 < kol && pozitionBox[second.x, second.y + 1].numberMaterial == SecondNumber
                && pozitionBox[second.x, second.y + 2].numberMaterial == SecondNumber)

                || (second.y - 2 >= 0 && pozitionBox[second.x, second.y - 1].numberMaterial == SecondNumber
                && pozitionBox[second.x, second.y - 2].numberMaterial == SecondNumber)

            || (second.x + 1 < kol - 1 && pozitionBox[second.x + 1, second.y].numberMaterial == SecondNumber
                && second.x - 1 >= 0 && pozitionBox[second.x - 1, second.y].numberMaterial == SecondNumber)

                || (second.x + 2 < kol - 1 && pozitionBox[second.x + 1, second.y].numberMaterial == SecondNumber
                 && pozitionBox[second.x + 2, second.y].numberMaterial == SecondNumber)

                 || (second.x - 2 >= 0 && pozitionBox[second.x - 1, second.y].numberMaterial == SecondNumber
                 && pozitionBox[second.x - 2, second.y].numberMaterial == SecondNumber))
            {
                Debug.Log("true1");
                return true;
            }
        }
        Debug.Log("false");
        return false;

    }
    private void OnMoveVertical(Cube first)
    {
        Debug.Log("first.y" + first.y);
        Debug.Log("first.x" + first.x);
        if (first.y + 1 < kol && first.y - 1 >= 0 && first.isSame(pozitionBox[first.x, first.y - 1]) && first.isSame(pozitionBox[first.x, first.y + 1]))
        {
            monets += 5;
            Debug.Log("между");
            MoveVertical(pozitionBox[first.x, first.y].x, pozitionBox[first.x, first.y].y - 1, false);
        }
        else
      if (first.y + 2 < kol && first.isSame(pozitionBox[first.x, first.y + 1]) && first.isSame(pozitionBox[first.x, first.y + 2]))
        {
            Debug.Log("снизу");
            MoveVertical(pozitionBox[first.x, first.y].x, pozitionBox[first.x, first.y].y, false);
        }
        else
       if (first.y - 2 >= 0 && first.isSame(pozitionBox[first.x, first.y - 1]) && first.isSame(pozitionBox[first.x, first.y - 2]))
        {
            Debug.Log("сверху");
            MoveVertical(pozitionBox[first.x, first.y].x, pozitionBox[first.x, first.y].y - 2, false);
        }
    }

    private void ReplaceColor() 
    { 
        for (int i = 0; i < kol - 1; i++)
        {
            for (int j = 0; j < kol; j++)
            {
                pozitionBox[i, j].setNewMaterial();
            }
        }
    }
}
 