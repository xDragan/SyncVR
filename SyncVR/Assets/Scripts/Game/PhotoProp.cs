using UnityEngine;
using System.Collections;


namespace PhotoGame
{
	public class PhotoProp : MonoBehaviour
	{
		public enum Type {None = -1, Dog, Dragon, Horse, Ox, Rat, Tiger};

		public Type animal;

		public float time;
        protected Collider col;

        public static Controller cc;

        protected void Start()
        {
            col = GetComponent<Collider>();
            col.enabled = false;
        }

        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                ShootRay(Input.mousePosition);
            }
        }

        private void ShootRay(Vector3 mousePos)
        {
            //var camTrans = Camera.main.transform;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;
            
            Debug.DrawRay(ray.origin, ray.direction, Color.black);

            if (Physics.Raycast(ray, out hit))
            {
                if(hit.transform.gameObject.CompareTag("Animal"))
                {
                    Debug.Log("AnimalDetected");
                    if(cc.CanPhoto(animal))
                    {
                        cc.safariCam.GetComponent<SafariCapture>().SetPhoto();
                    }
                }
            }
        }

        protected virtual void Appear() { }

        private void OnDisable()
        {
            cc.UnUse(animal);
        }

    }
}