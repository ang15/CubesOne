using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cube : MonoBehaviour
{
    [SerializeField]
    private float screenWidth;
    [SerializeField]
    private float screenHieght;
    public int numberMaterial = 1;
    public int x;
    public int y;
    // Start is called before the first frame update
    void Start()
    {
       setNewMaterial();
    }

    void OnMouseDown()
    {
        GameManager.instance.OnCubeClick(this);

    }

    public void setNewMaterial()
    {
        setMateral(Random.Range(0, MaterialManager.instanse.materialCount-1));
    }
    public void setNewMaterialNow(int index)
    {
        int rnd = Random.Range(0, MaterialManager.instanse.materialCount - 1);
        while (rnd == index)
        {
           rnd = Random.Range(0, MaterialManager.instanse.materialCount - 1);
        }
        setMateral(rnd);
    }

    public bool isSame(Cube cube)
    {
        return numberMaterial == cube.numberMaterial;
    }



    public void setMateral(int materialIndex)
    {
        numberMaterial = materialIndex;
        GetComponent<Image>().sprite= MaterialManager.instanse.materials[numberMaterial];
    }

    public void mergeCube(Cube cube)
    {
        setMateral(cube.numberMaterial);
    }


}
