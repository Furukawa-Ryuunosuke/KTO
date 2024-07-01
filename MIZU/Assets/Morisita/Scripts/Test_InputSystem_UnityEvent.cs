using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class Test_InputSystem_UnityEvent : MonoBehaviour
{
    [SerializeField]
    private float _gravity;
    [SerializeField]
    private float _JumpHeight;
    [SerializeField]
    private Material[] _playerMaterials = new Material[2];

    bool isOnGround = false;

    Rigidbody rb;
    PlayerInput _playerInput;
    MeshRenderer _meshRenderer;
    PlayerPhaseState.State pState;

    private Vector3 _velocity;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
        _meshRenderer = GetComponent<MeshRenderer>();
        pState = GetComponent<PlayerPhaseState.State>();

        if (_playerInput.user.index == 0)
            _meshRenderer.material = _playerMaterials[0];
        else
            _meshRenderer.material = _playerMaterials[1];

        pState = PlayerPhaseState.State.Liquid;
    }

    private void Update()
    {
        transform.position += _velocity * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Gravity();
    }

    void Gravity()
    {
        rb.AddForce(new Vector3(0, -_gravity, 0), ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;
    }
    private void OnCollisionStay(Collision collision)
    {
        isOnGround = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        isOnGround = false;
    }


    // ���\�b�h���͉��ł�OK
    // public�ɂ���K�v������
    public void OnMove(InputAction.CallbackContext context)
    {
        // MoveAction�̓��͒l���擾
        var axis = context.ReadValue<Vector2>();

        // 2D�Ȃ̂ŉ��ړ�����
        _velocity = new Vector3(axis.x, 0, 0);
    }
    int callOnJumpCount = 0;
    public void OnJump(InputAction.CallbackContext context)
    {
        // �������u�Ԃ�����������
        if (!context.performed) return;

        // �������ł���������if���̒��ɂȂ��Č��Â炭�Ȃ�
        //if (context.performed)

        // �n�ʂɂ��Ȃ��Ȃ璵�ׂȂ�
        if (!isOnGround) return;


        rb.AddForce(new Vector3(0, _JumpHeight, 0), ForceMode.VelocityChange);

        print(callOnJumpCount++ + ":Jump��������܂���");
    }

    /// <summary>
    /// �C�̂֕ω�
    /// </summary>
    public void OnStateChangeGas(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        // ������Ȃ�������󂯕t���Ȃ�
        if (pState != PlayerPhaseState.State.Liquid) return;
        print("GAS");
    }

    /// <summary>
    /// �ő̂֕ω�
    /// </summary>
    public void OnStateChangeSolid(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        // ������Ȃ�������󂯕t���Ȃ�
        if (pState != PlayerPhaseState.State.Liquid) return;

        print("SOLID");
    }
    /// <summary>
    /// �t�̂֕ω�
    /// </summary>
    public void OnStateChangeLiquid(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        // �ő́E�C�́E�X���C������Ȃ�������󂯕t���Ȃ�
        if (pState != PlayerPhaseState.State.Solid) return;
        if (pState != PlayerPhaseState.State.Gas) return;
        if (pState != PlayerPhaseState.State.Slime) return;


        print("LIQUID");
    }

    /// <summary>
    /// �X���C���֕ω�
    /// </summary>
    public void OnStateChangeSlime(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        // ������Ȃ�������󂯕t���Ȃ�
        if (pState != PlayerPhaseState.State.Liquid) return;

        print("SLIME");

    }
}
