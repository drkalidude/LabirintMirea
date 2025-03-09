using UnityEngine;
using UnityEngine.UI;

public class TransformController : MonoBehaviour
{
    public GameObject objectToModify; 
    public Button scaleIncreaseButton;
    public Button scaleDecreaseButton;
    public Button rotateLeftButton;
    public Button rotateRightButton;
    public float scaleStep = 1.5f;
    public float rotationStep = 15f;

    public Rigidbody rb; 

    void Start()
    {
        scaleIncreaseButton.onClick.AddListener(IncreaseScale);
        scaleDecreaseButton.onClick.AddListener(DecreaseScale);
        rotateLeftButton.onClick.AddListener(() => RotateObject(-rotationStep));
        rotateRightButton.onClick.AddListener(() => RotateObject(rotationStep));

        if (objectToModify == null)
        {
            objectToModify = GameObject.FindWithTag("SpawnedObject");
        }

        if (objectToModify != null)
        {
            rb = objectToModify.GetComponent<Rigidbody>();
        }
    }

    public void IncreaseScale()
    {
        if (objectToModify == null) return;
        ChangeScale(scaleStep);
    }

    public void DecreaseScale()
    {
        if (objectToModify == null) return;
        ChangeScale(-scaleStep);
    }

    private void ChangeScale(float step)
    {
        if (objectToModify == null) return;

        if (rb != null)
        {
            rb.isKinematic = true;
        }

        float newScale = objectToModify.transform.localScale.x + step;
        objectToModify.transform.localScale = new Vector3(newScale, newScale, newScale);

        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }

    public void RotateObject(float angle)
    {
        if (objectToModify == null) return;

        if (rb != null)
        {
            rb.isKinematic = true;
        }

        objectToModify.transform.Rotate(0, angle, 0);

        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }
}
