using UnityEngine;

public class BuildingMaterialGrid : MonoBehaviour
{
    private GameObject _flyingMaterial;
    private Camera _mainCamera;
    public static int CountOfMaterials { get; set; }
    [SerializeField] private GameObject _winText;
    //public MiniGameLauncher GameLauncher;

    private void Awake()
    {
        _mainCamera = Camera.main;
        CountOfMaterials = 4;
    }

    public void StartControlMaterial(GameObject materialPrefab)
    {
        if (_flyingMaterial != null)
            Destroy(_flyingMaterial);
        _flyingMaterial = Instantiate(materialPrefab);
    }

    private void Update()
    {
        if (_flyingMaterial != null)
        {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (groundPlane.Raycast(ray, out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);
                _flyingMaterial.transform.position = worldPosition;
            }
        }
        if (CountOfMaterials == 0)
        {
            _winText.SetActive(true);
            //GameLauncher.LoadMiniGame("MainGameScene");
            CountOfMaterials = -1;
        }
            
    }
}
