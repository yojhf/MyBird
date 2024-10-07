using UnityEngine;


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

        AudioSource audiosource;
        [SerializeField] private AudioClip getPoint;
        [SerializeField] private AudioClip die;
        [SerializeField] private AudioClip wing;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            audiosource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            PlayerRot();

            if (GameManager.instance.IsDeath == true)
            {
                return;
            }

            StartGameSet();
            InputBird();
            ReadyBird();
            Move();
        }

        private void FixedUpdate()
        {
            Jump();
        }
        void StartGameSet()
        {

#if UNITY_EDITOR
            if (GameManager.instance.IsStart == false && Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                GameManager.instance.IsStart = true;
                UIManager.instance.readyUI.SetActive(false);
            }

#elif UNITY_ANDROID
            if (GameManager.instance.IsStart == false && Input.touchCount > 0)
            {
                GameManager.instance.IsStart = true;
                UIManager.instance.readyUI.SetActive(false);             
            }

#endif



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
#if UNITY_EDITOR

            if (isJump == false)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    isJump = true;
                    PlayAudio(wing);
                }
            }
#elif UNITY_ANDROID

   if (isJump == false)
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);

                    if (touch.phase == TouchPhase.Began)
                    {
                        isJump = true;
                        PlayAudio(wing);
                    }

                }
            }
        
#endif




        }


        void Move()
        {
            if(GameManager.instance.IsStart == false || GameManager.instance.IsDeath == true)
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

        void Die()
        {
            if (GameManager.instance.IsDeath == true)
            {
                return;
            }


            GameManager.instance.IsDeath = true;
            UIManager.instance.gameoverUI.SetActive(true);
            GameManager.instance.GetBestScore();
            PlayAudio(die);

        }

        void GetPoint()
        {
            if (GameManager.instance.IsDeath == true)
            {
                return;
            }

            PlayAudio(getPoint);
            GameManager.instance.Score++;

            SpawnTimeCon();
        }

        void SpawnTimeCon()
        {
            if (GameManager.instance.Score % 10 == 0)
            {
                SpawnManager.levelTime += 0.05f;
            }
        }



        void PlayAudio(AudioClip audio)
        {
            audiosource.clip = audio;

            audiosource.Play();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.tag == "Ground")
            {
                Die();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Pipe"))
            {
                Die();
            }
            else if(collision.CompareTag("Point"))
            {
                GetPoint();
            }

        }


    }
}
