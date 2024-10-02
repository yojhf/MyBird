using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;


namespace MyBird
{
    public class Player : MonoBehaviour
    {
        Rigidbody2D rb;

        [SerializeField] private float jumpPower = 5f;
        private bool isJump = false;

        [SerializeField] private float rotSpeed = 5f;
        private Vector3 birdRot = Vector3.zero;

        [SerializeField] private float moveSpeed = 5f;

        [SerializeField] private float readyPower = 1f;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            InputBird();
            ReadyBird();
            PlayerRot();
            Move();
        }

        private void FixedUpdate()
        {
            Jump();
        }

        void Jump()
        {
            if(isJump == true)
            {
                //rb.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);

                rb.velocity = Vector2.up * jumpPower;

                isJump = false;
            }

        }
        void InputBird()
        {

            if (isJump == true && GameManager.instance.IsStart == false)
            {
                GameManager.instance.IsStart = true;
            }

            if (isJump == false)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    isJump = true;
                }
            }
        }

        void Move()
        {
            if(GameManager.instance.IsStart == false)
            {
                return;
            }

            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed, Space.World);         
        }

        void PlayerRot()
        {
            Vector2 pos = rb.velocity;

            float rotUp = 30f;
            float rotDown = -90f;

            float degree = 0f;

            if (pos.y > 0f)
            {
                degree = rotSpeed;

                //transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotUp * Time.deltaTime * rotSpeed);
            }
            else
            {
                degree = -rotSpeed;
                //transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotDown * Time.deltaTime * rotSpeed);
            }

            float rotZ = Mathf.Clamp(birdRot.z + degree, rotDown, rotUp);

            birdRot = new Vector3(0f, 0f, rotZ);

            transform.eulerAngles = birdRot;
        }

        void ReadyBird()
        {
            if (GameManager.instance.IsStart == true)
            {
                return;
            }

            Vector2 pos = rb.velocity;

            if (pos.y < 0f)
            {
                rb.velocity = Vector2.up * readyPower;
            }
        }




    }
}
