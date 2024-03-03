using UnityEngine;

namespace PlayerScripts
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Joystick myJoystick;
        [SerializeField] public float movementSpeed;
        private Vector3 moveDir;
        private Vector3 lookDir;
        private bool isActive = true;
        private bool isRunning = false;

        private Transform playerTransform;
        private Rigidbody playerRigidbody;

        [SerializeField] private Animator myAnimator;

        private void Start()
        {
            playerTransform = GetComponent<Transform>();
            playerRigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (isActive)
            {
                Movement();
                AnimationControl();
            }
        }
    
        private void Movement()
        {
            var position = playerTransform.position;
            moveDir = new Vector3(myJoystick.Horizontal, 0, myJoystick.Vertical);
            lookDir = new Vector3(transform.position.x + myJoystick.Horizontal, position.y, position.z + myJoystick.Vertical);
            transform.LookAt(lookDir);
            playerRigidbody.velocity = moveDir * movementSpeed;
        }
    
        private void AnimationControl()
        {
            if (moveDir.magnitude == 0)
            {
                if (!isRunning) return;
                isRunning = false;
                myAnimator.SetBool("isRunning",false);
            }
            else
            {
                if (isRunning) return;
                isRunning = true;
                myAnimator.SetBool("isRunning",true);
            }
        }
    }
}
