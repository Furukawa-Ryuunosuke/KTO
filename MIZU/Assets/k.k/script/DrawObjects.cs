using UnityEngine;
using System.Collections.Generic;

public class DrawObjects : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject drawObjectPrefab;  // �u�������I�u�W�F�N�g��Prefab
    public float minDistance = 0.1f;     // �I�u�W�F�N�g��z�u����ŏ�����
    private List<GameObject> drawObjects;
    private Vector3 lastPosition;
    public GameObject parentObject; // �w�肳�ꂽ�e�I�u�W�F�N�g

    void Start()
    {
        drawObjects = new List<GameObject>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10f; // �J��������̓K�؂ȋ�����ݒ�
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
            worldPosition.z = 0f;

            if (drawObjects.Count == 0 || Vector3.Distance(worldPosition, lastPosition) > minDistance)
            {
                GameObject drawObject = Instantiate(drawObjectPrefab, worldPosition, Quaternion.identity);
                drawObjects.Add(drawObject);
                lastPosition = worldPosition;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            // ���ׂẴ��b�V������ɂ܂Ƃ߂�
            CombineMeshes(parentObject);
        }
    }

    void CombineMeshes(GameObject parent)
    {
        // �V�����I�u�W�F�N�g���쐬
        GameObject combinedObject = new GameObject("CombinedObject");

        // ���b�V���t�B���^�[�ƃ����_���[��ǉ�
        MeshFilter combinedMeshFilter = combinedObject.AddComponent<MeshFilter>();
        MeshRenderer combinedMeshRenderer = combinedObject.AddComponent<MeshRenderer>();

        // ���b�V���̓���
        MeshFilter[] meshFilters = new MeshFilter[drawObjects.Count];
        for (int i = 0; i < drawObjects.Count; i++)
        {
            meshFilters[i] = drawObjects[i].GetComponent<MeshFilter>();
        }

        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        for (int i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
        }

        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combine);
        combinedMeshFilter.mesh = combinedMesh;

        // �}�e���A���̐ݒ�
        combinedMeshRenderer.material = drawObjects[0].GetComponent<MeshRenderer>().sharedMaterial;

        // �R���C�_�[�̓���
        MeshCollider combinedCollider = combinedObject.AddComponent<MeshCollider>();
        combinedCollider.sharedMesh = combinedMesh;
        combinedCollider.convex = true;  // Convex�ɐݒ�

        // �e�I�u�W�F�N�g���w�肳��Ă���ꍇ
        if (parent != null)
        {
            // �e�I�u�W�F�N�g�ɐݒ肵�A���[���h���W����v������
            combinedObject.transform.SetParent(parent.transform, false);
            combinedObject.transform.position = parent.transform.position;
            combinedObject.transform.rotation = parent.transform.rotation;
        }
        else
        {
            Debug.LogWarning("Parent object is not specified. Combined object will be placed in the scene root.");
        }

        // �ȑO�̃I�u�W�F�N�g���폜
        foreach (var obj in drawObjects)
        {
            Destroy(obj);
        }
        drawObjects.Clear();

        // �V�����I�u�W�F�N�g�����X�g�ɒǉ�
        drawObjects.Add(combinedObject);
    }
}
