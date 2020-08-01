using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    private GameObject selectedObject;
    public GameObject markOverHead;
    public GameObject markUnderFeet;

    private void Start()
    {
        markOverHead.SetActive(false);
        markUnderFeet.SetActive(false);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if(Physics.Raycast( ray, out hitInfo))
            {
                if(hitInfo.transform.root.CompareTag("Mob") && selectedObject == null)
                {
                    selectedObject = hitInfo.transform.root.gameObject;                    
                    PlaceTheIndicators();
                }
                else if(hitInfo.transform.root.CompareTag("Mob") && selectedObject != null)
                {
                    HideTheIndicators();
                    selectedObject = hitInfo.transform.root.gameObject;
                    PlaceTheIndicators();
                }
                else if (hitInfo.transform.root.CompareTag("Ground"))
                {
                    HideTheIndicators();
                }

            }
        }
    }

    private void PlaceTheIndicators()
    {
        markOverHead.SetActive(true);
        markUnderFeet.SetActive(true);
        Bounds outterBounds = selectedObject.GetComponentInChildren<SkinnedMeshRenderer>().bounds;       
        float diameter = Mathf.Max(outterBounds.size.x, outterBounds.size.z);
        diameter *= 1.05f;
        markUnderFeet.transform.localScale = new Vector3(diameter, diameter, diameter);
        markUnderFeet.transform.position = new Vector3(selectedObject.transform.position.x - .165f, 0.01f, selectedObject.transform.position.z);
        markUnderFeet.transform.parent = selectedObject.transform;
        markOverHead.transform.position = new Vector3(selectedObject.transform.position.x , (outterBounds.size.y + 1f), selectedObject.transform.position.z);
        markOverHead.transform.parent = selectedObject.transform;


    }

    private void HideTheIndicators()
    {
        selectedObject = null;
        markOverHead.SetActive(false);
        markUnderFeet.SetActive(false);
    }
}
