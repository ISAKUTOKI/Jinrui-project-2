using UnityEngine;

public class DebugPanelController : MonoBehaviour
{
    [SerializeField]
    private bool isInTest;

    [SerializeField]
    private GameObject panelDebug;

    // Start is called before the first frame update
    private void Start()
    {
        panelDebug.SetActive(!isInTest);
    }
}