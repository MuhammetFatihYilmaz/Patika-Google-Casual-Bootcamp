using UnityEngine;

public class PlanetInfo : MonoBehaviour
{

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            PlanetManager.Instance.GetPlanetInfoToUIPanel(gameObject.name);
        }
    }
}
