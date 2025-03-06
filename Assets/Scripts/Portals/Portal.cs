using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class Portal : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Camera portalView;
    [SerializeField] PortalPairScriptableObj pair;

    private void OnEnable()
    {
        if (pair.getPartner(this) == null)
        {

        }
        else
        {
            GetComponent<MeshRenderer>().material.SetTexture("_PortalCam", pair.getPartner(this).portalView.targetTexture);
        }
        RenderPipelineManager.beginCameraRendering += UpdateCamera;
    }
    private void OnDisable()
    {
        RenderPipelineManager.beginCameraRendering -= UpdateCamera;
    }

    void UpdateCamera(ScriptableRenderContext context, Camera camera)
    {
        portalView.projectionMatrix = Camera.main.projectionMatrix;

    }
}
