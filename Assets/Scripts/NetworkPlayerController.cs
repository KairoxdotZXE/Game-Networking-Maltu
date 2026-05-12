using UnityEngine;
using Unity.Netcode;

public class NetworkPlayerController : NetworkBehaviour
{

    [SerializeField]float moveSpeed = 6.7f;
    [SerializeField]float gravity = -9.81f;
    [SerializeField]float groundedGravity = -2f;

private CharacterController characterController;
private float verticalVelocity;
private void Awake() {
    characterController = GetComponent<CharacterController>();
}

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if(!IsOwner)return;

      float horizontalInput = Input.GetAxis("Horizontal");
      float verticalInput =   Input.GetAxis("Vertical");
      Debug.Log(horizontalInput + "   " + verticalInput);
      Vector2 inputDirection = new Vector2(horizontalInput,verticalInput);
        if (IsServer)
        {
            MovePlayer(inputDirection);
        }
        else{MovePlayerRPC(inputDirection);}
    }
    [Rpc(SendTo.Server)]
    private void MovePlayerRPC(Vector2 movementInput)
    {
        MovePlayer(movementInput);

    }
    private void MovePlayer(Vector2 movementInput)
    {
        Debug.Log("MovingPlayer");
        if(characterController.isGrounded && verticalVelocity < 0){verticalVelocity=groundedGravity;}
        else{verticalVelocity += gravity * Time.deltaTime;}

    
    Vector3 moveDirection = new Vector3(movementInput.x, 0, movementInput.y).normalized;
    Vector3 horizontalMovement = moveDirection * moveSpeed;
    Vector3 verticalMovement = Vector3.up * verticalVelocity;
    Vector3 finalMovement = horizontalMovement = verticalMovement;
    characterController.Move(finalMovement * Time.deltaTime);
    }

}
