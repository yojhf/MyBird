using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyBird
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private GameObject Pipe;
        [SerializeField] private Transform PipePar;
        [SerializeField] private float coolTime = 1f;
        [SerializeField] private float minPos = 1.5f;
        [SerializeField] private float maxPos = 3.5f;

        [SerializeField] private float minSpawnTime = 0.95f;
        [SerializeField] private float maxSpawnTime = 1.05f;

        public static float levelTime = 0f;
        private GameObject tmp_Pipe;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(SpawnPipe());
        }

        // Update is called once per frame
        void Update()
        {
            if (GameManager.instance.IsDeath == true)
            {
                return;
            }
        }

        IEnumerator SpawnPipe()
        {
            while (GameManager.instance.IsDeath == false)
            {
                float posY = Random.Range(minPos, maxPos);

                coolTime = Random.Range((minSpawnTime - levelTime), maxSpawnTime);

                Vector2 pos = new Vector2(PipePar.position.x, posY);

                if (GameManager.instance.IsStart == true)
                {
                    tmp_Pipe = Instantiate(Pipe, pos, Quaternion.identity);
   
                }

                yield return new WaitForSeconds(coolTime);
            }
        }



    }
}