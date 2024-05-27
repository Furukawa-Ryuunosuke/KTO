using UnityEngine;
using System.Collections;

public class SwitchPlatform : MonoBehaviour
{
    public GameObject platform; // �傫������������I�u�W�F�N�g�������ɃA�T�C�����܂�
    private Vector3 originalScale;
    public Vector3 enlargedScale = new Vector3(2, 2, 2); // �傫���������T�C�Y��ݒ肵�܂�
    public float shrinkDelay = 2.0f; // ���ꂪ���̃T�C�Y�ɖ߂�܂ł̒x������

    private Coroutine shrinkCoroutine;

    void Start()
    {
        if (platform != null)
        {
            originalScale = platform.transform.localScale;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (shrinkCoroutine != null)
            {
                StopCoroutine(shrinkCoroutine);
            }
            platform.transform.localScale = enlargedScale;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            shrinkCoroutine = StartCoroutine(ShrinkPlatformAfterDelay());
        }
    }

    IEnumerator ShrinkPlatformAfterDelay()
    {
        yield return new WaitForSeconds(shrinkDelay);
        platform.transform.localScale = originalScale;
    }
}
