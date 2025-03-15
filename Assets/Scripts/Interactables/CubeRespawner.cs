using UnityEngine;

public class CubeRespawner : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private GameObject cube;   //set this to cube in scene if it starts in scene
    [SerializeField] private Transform spawnPos;

    public void Respawn(bool input)
    {
        if(input)
        {
            DestroyCube();
        }
    }

    private void DestroyCube()
    {
        if(cube != null)
        {
            if(InteractController.instance)
            {
                InteractController.instance.DropObj();
            }
            else
            {
                Debug.Log("No Interact Controller instance!");
            }
                Destroy(cube);
            Invoke(nameof(RespawnCube), 0.5f);
        }
        else
        {
            RespawnCube();
        }
        
    }

    private void RespawnCube()
    {
        cube = Instantiate(cubePrefab, spawnPos);
    }
}
