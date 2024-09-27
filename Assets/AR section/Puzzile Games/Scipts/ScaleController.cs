using UnityEngine;

public class ScaleController : MonoBehaviour
{
    public Transform[] topTransforms; // Array to hold the three top transforms
    private Vector3 initialScale; // Initial scale of the object in the x and z axes
    private Vector3[] initialPositionsY; // Initial y positions of the top transforms

    void Start()
    {
        // Store the initial scale of the object in x and z
        initialScale = new Vector3(transform.localScale.x, 0, transform.localScale.z);

        // Store the initial y positions of the top transforms
        initialPositionsY = new Vector3[topTransforms.Length];
        for (int i = 0; i < topTransforms.Length; i++)
        {
            initialPositionsY[i] = new Vector3(0, topTransforms[i].position.y, 0);
        }
    }

    void Update()
    {
        // Continuously adjust position based on current scale changes in x and z
        AdjustScaleAndPosition(transform.localScale.x, transform.localScale.z);
    }

    private void AdjustScaleAndPosition(float newScaleX, float newScaleZ)
    {
        // Calculate the ratio of new scale to old scale
        float ratioX = newScaleX / initialScale.x;
        float ratioZ = newScaleZ / initialScale.z;

        // Calculate the average ratio and apply it to the y position of the top transforms
        float averageRatio = (ratioX + ratioZ) / 2;
        for (int i = 0; i < topTransforms.Length; i++)
        {
            Vector3 newPosition = new Vector3(
                topTransforms[i].position.x,
                initialPositionsY[i].y * averageRatio,
                topTransforms[i].position.z);
            topTransforms[i].position = newPosition;
        }
    }
}
