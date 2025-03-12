using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class Portal : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Camera portalView;
    [SerializeField] PortalPair pair;

    private void OnEnable()
    {
        if (pair.getPartner(this) == null)
        {

        }
        else
        {
            GetComponent<MeshRenderer>().material.SetTexture("_PortalCam", portalView.targetTexture);
            RenderPipelineManager.beginCameraRendering += UpdateCamera;
        }
        
    }
    private void OnDisable()
    {
        RenderPipelineManager.beginCameraRendering -= UpdateCamera;
    }

    void UpdateCamera(ScriptableRenderContext context, Camera camera)
    {
        if ((camera.cameraType == CameraType.Game || camera.cameraType == CameraType.SceneView)&&!camera.CompareTag("PortalCamera"))
        {
            portalView.projectionMatrix = camera.projectionMatrix;

            var relativePos = transform.InverseTransformPoint(camera.transform.position);
            relativePos = Vector3.Scale(relativePos, new Vector3(1, 1, 1));
            portalView.transform.position = pair.getPartner(this).transform.TransformPoint(relativePos);

            var relativeRot = transform.InverseTransformDirection(camera.transform.forward);
            relativeRot = Vector3.Scale(relativeRot, new Vector3(-1, 1, -1));
            portalView.transform.forward = pair.getPartner(this).transform.TransformDirection(relativeRot);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var relativeposition = transform.InverseTransformPoint(other.transform.position);
            relativeposition = Vector3.Scale(relativeposition, new Vector3(1,1,1));
            other.transform.position = pair.getPartner(this).transform.TransformPoint(relativeposition);

            var relativeRot = transform.InverseTransformDirection(other.transform.forward);
            relativeRot = Vector3.Scale(relativeRot, new Vector3(-1, 1, -1));
            other.transform.forward = pair.getPartner(this).transform.TransformDirection(relativeRot);
        }
    }
}
