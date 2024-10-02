using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace MyBird
{
    public class CameraCon : MonoBehaviour
    {
        public Transform target;
        [SerializeField] private float offset = 1.5f;

        private void LateUpdate()
        {
            float posX = target.position.x;

            Vector3 pos = new Vector3(posX + offset, transform.position.y, transform.position.z);

            transform.position = pos;

        }
    }
}