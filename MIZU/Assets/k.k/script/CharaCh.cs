using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChararCh : MonoBehaviour
{
    public bool Mode;

    GameObject[] characters;
    GameObject currentChar;

    int _currentCharNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        // ���̃I�u�W�F�N�g�̎q�I�u�W�F�N�g�����ׂĎ擾
        List<GameObject> characterList = new List<GameObject>();
        foreach (Transform child in transform)
        {
            characterList.Add(child.gameObject);
        }
        characters = characterList.ToArray();

        Debug.Log("I have " + characters.Length + " Changable Characters");

        ChangeCharacter(_currentCharNum);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mode) // Player 1
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (_currentCharNum == characters.Length - 1)
                {
                    _currentCharNum = 0;
                }
                else
                {
                    _currentCharNum++;
                }
                Debug.Log("Character " + _currentCharNum + " Selected");
                ChangeCharacter(_currentCharNum);
            }
        }
        else // Player 2
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (_currentCharNum == characters.Length - 1)
                {
                    _currentCharNum = 0;
                }
                else
                {
                    _currentCharNum++;
                }
                Debug.Log("Character " + _currentCharNum + " Selected");
                ChangeCharacter(_currentCharNum);
            }
        }
    }
    void ChangeCharacter(int characterNum)
    {
        foreach (GameObject gObj in characters)         // ��������S�L�����N�^�[���A�N�e�B�u�ɂ���
        {
            gObj.SetActive(false);
        }

        currentChar = characters[characterNum];         // �w�肵���ԍ��̃L�����N�^�[�������A�N�e�B�u�ɂ���
        currentChar.SetActive(true);
    }
}
