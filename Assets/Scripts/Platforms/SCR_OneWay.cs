using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_OneWay : MonoBehaviour
{
    [SerializeField] Collider PhysicsCollider;

    enum EntrySide
    {
        top,
        bottom
    }

    [SerializeField] EntrySide entrySide;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) { return; }

        if (other.transform.position.y > transform.position.y && entrySide == EntrySide.top)
        {
            StartCoroutine(DisableCollider());
        }

        if (other.transform.position.y < transform.position.y && entrySide == EntrySide.bottom)
        {
            StartCoroutine(DisableCollider());
        }
    }

    IEnumerator DisableCollider()
    {
        Debug.Log("Weee");
        PhysicsCollider.enabled = false;
        yield return new WaitForSeconds(0.5f);
        PhysicsCollider.enabled = true;
    }
}
