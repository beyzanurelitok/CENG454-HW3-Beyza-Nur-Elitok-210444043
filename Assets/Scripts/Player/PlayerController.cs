using UnityEngine;

namespace CoreBreach.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 6f;

        private CharacterController _cc;
        private Camera _mainCam;

        private void Awake()
        {
            _cc = GetComponent<CharacterController>();
            _mainCam = Camera.main;
        }

        private void Update()
        {
            HandleMovement();
            HandleAiming();
        }

        private void HandleMovement()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            Vector3 moveDir = new Vector3(h, 0f, v).normalized;
            _cc.SimpleMove(moveDir * moveSpeed);
        }

        private void HandleAiming()
        {
            Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 lookDir = hit.point - transform.position;
                lookDir.y = 0f;
                if (lookDir.sqrMagnitude > 0.01f)
                    transform.rotation = Quaternion.LookRotation(lookDir);
            }
        }
    }
}