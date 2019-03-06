using UnityEngine;

public class GemController : MonoBehaviour
{
    private void Start()
    {
        Init();
    }

    public void Init()
    {

    }

    private void OnTriggerEnter(Collider collider)
    {
        // Debug.Log("OnTriggerEnter");

        if (collider.tag == "Human")
        {
            HumanController human = collider.gameObject.GetComponent<HumanController>();
            human.Die();
        }
    }
}
