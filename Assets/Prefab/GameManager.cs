using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    private Cubes cubes;
    private Cube FirstCube;
    private Cube SecondCube;
    private void Awake()
    {
        instance = this;
    }

    public void OnCubeClick(Cube cube)
    {
        if (cubes.CanClick == true)
        {
            if (FirstCube == null)
            {
                FirstCube = cube;
                cube.transform.rotation =Quaternion.Euler(cube.transform.rotation.x,cube.transform.rotation.y,-45);
            }
            else if (FirstCube != null && SecondCube == null && FirstCube!=cube)
            {
                cubes.moves -= 1;
                cube.transform.rotation = Quaternion.Euler(cube.transform.rotation.x, cube.transform.rotation.y, -45);
                cubes.CanClick = false;
                SecondCube = cube;
                StartCoroutine("DownBoxs");

          
            }
        }
    }
    IEnumerator DownBoxs()
    {
            int first = FirstCube.numberMaterial;
            int second = SecondCube.numberMaterial;
            FirstCube.setMateral(5);
            SecondCube.setMateral(5);

        if ((FirstCube.x == (SecondCube.x - 1) && FirstCube.y == SecondCube.y)
                || (FirstCube.y == (SecondCube.y - 1) && FirstCube.x == SecondCube.x)
                || (FirstCube.x == (SecondCube.x + 1) && FirstCube.y == SecondCube.y)
                || (FirstCube.y == (SecondCube.y + 1) && FirstCube.x == SecondCube.x)
                )
        {
            yield return new WaitForSeconds(0.3f);
         
                FirstCube.setMateral(second);
                SecondCube.setMateral(first);

            if (cubes.OnCheck(FirstCube, SecondCube, SecondCube.numberMaterial))
            {
              
                FirstCube.transform.rotation = Quaternion.Euler(FirstCube.transform.rotation.x, FirstCube.transform.rotation.y, 0);
                SecondCube.transform.rotation = Quaternion.Euler(SecondCube.transform.rotation.x, SecondCube.transform.rotation.y, 0);
                FirstCube = null;
                SecondCube = null;
                cubes.CanClick = true;
            }
            else  if (cubes.OnCheck(SecondCube, FirstCube, FirstCube.numberMaterial))
            {
                FirstCube.transform.rotation = Quaternion.Euler(FirstCube.transform.rotation.x, FirstCube.transform.rotation.y, 0);
                SecondCube.transform.rotation = Quaternion.Euler(SecondCube.transform.rotation.x, SecondCube.transform.rotation.y, 0);
                FirstCube = null;
                SecondCube = null;
               
                cubes.CanClick = true;
            }
            else 
            {
                Debug.Log("Check 1");
                StartCoroutine("ChangeBoxs");

            }

        }
        else
        {
            yield return new WaitForSeconds(0.3f);
            FirstCube.setMateral(first);
            SecondCube.setMateral(second);
            FirstCube.transform.rotation = Quaternion.Euler(FirstCube.transform.rotation.x, FirstCube.transform.rotation.y, 0);
            SecondCube.transform.rotation = Quaternion.Euler(SecondCube.transform.rotation.x, SecondCube.transform.rotation.y, 0);
            FirstCube = null;
            SecondCube = null;
            cubes.CanClick = true;
        }
    }

    IEnumerator ChangeBoxs()
    {
        yield return new WaitForSeconds(0.3f);
        Debug.Log(FirstCube==null);
        int indext = FirstCube.numberMaterial;
        FirstCube.mergeCube(SecondCube);
        SecondCube.setMateral(indext);
        FirstCube.transform.rotation = Quaternion.Euler(FirstCube.transform.rotation.x, FirstCube.transform.rotation.y, 0);
        SecondCube.transform.rotation = Quaternion.Euler(SecondCube.transform.rotation.x, SecondCube.transform.rotation.y, 0);
        FirstCube = null;
        SecondCube = null;

        yield return new WaitForSeconds(0.3f);
        cubes.CanClick = true;
    }


}
