using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MyBird
{
    public class GroundCon : MonoBehaviour
    {
        [SerializeField] private float distacne = -8.4f;
        [SerializeField] private float moveSpeed = 5f;

        // Update is called once per frame
        void Update()
        {
            if (GameManager.instance.IsDeath == true)
            {
                return;
            }

            GroundMove();
        }

        void GroundMove()
        {
            if (transform.localPosition.x <= distacne)
            {

                transform.localPosition = Vector2.zero;
            }

            transform.Translate(Vector2.left * Time.deltaTime * moveSpeed);


        }
    }
}