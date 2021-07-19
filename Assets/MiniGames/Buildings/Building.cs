using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public List<GameObject> Buildings = new List<GameObject>();
    public MiniGameLauncher GameLauncher;
    private void Update()
    {
        if (BuildingMaterialGrid.CountOfMaterials == 0)
        {
            //_winText.SetActive(true);
            GameLauncher.LoadMiniGame("MainGameScene");
            BuildingMaterialGrid.CountOfMaterials = -1;
        }
    }
}
