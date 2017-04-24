namespace Eggore
{
    using UnityEngine;

    public class CameraFocus : MonoBehaviour
    {

        public Vector3 offset;
        public GameObject target;


        protected void Update()
        {
            transform.position = target.transform.position + offset;
        }

    }

}
