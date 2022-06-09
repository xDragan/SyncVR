using UnityEngine;
using System.Collections;


namespace PhotoGame
{
	public class PhotoProp : MonoBehaviour
	{
		public enum Type {None = -1,Crane, Dog, Dragon, Goat, Horse, Monkey, Ox, Pig, Rabbit, Rat, Rooster, Snake, Tiger};

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
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;

            if(Physics.Raycast(ray,out hit))
            {
                if(hit.transform.gameObject.CompareTag("Animal"))
                {
                    Debug.Log("PhotoAnimal");
                    cc.safariCam.GetComponent<SafariCapture>().SetPhoto();
                }
            }
        }

        protected virtual void Appear() { }


        
    }
}