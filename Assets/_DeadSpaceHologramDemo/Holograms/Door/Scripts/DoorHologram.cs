using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHologram : MonoBehaviour
{
    public GameObject Trigger;
    public Transform hologram;

    public Vector3 startScale;
    //bool isInTrigger;
    [SerializeField] Vector3 minScale;
    [SerializeField] Vector3 maxScale;
    [SerializeField] float speed = 10f;
    [SerializeField] float duration = 1.5f;

    void Awake()
    {
        startScale = hologram.localScale;
    }

    public IEnumerator Lerp(Vector3 a, Vector3 b, float time)
    {
        float i = 0.0f;
        float rate = (1.0f / time) * speed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.localScale = Vector3.Lerp(a, b, i);
            yield return null;
        }
    }

    public IEnumerator ExpandHolo()
    {
        FindObjectOfType<AudioManager>().Play("HoloExpand");
        yield return Lerp(transform.localScale, maxScale, duration);
        Debug.Log("Expand");
    }
    public IEnumerator CollapseHolo()
    {
        FindObjectOfType<AudioManager>().Play("HoloCollapse");
        yield return Lerp(transform.localScale, startScale, duration);
        Debug.Log("Collapse");
    }
    public IEnumerator RemoveHolo()
    {
        yield return Lerp(transform.localScale, minScale, duration);
        Debug.Log("Removed");
    }
}
