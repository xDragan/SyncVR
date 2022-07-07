using UnityEngine;
using System.Collections;
using System.IO;

namespace PhotoGame
{
	public class SafariCapture : MonoBehaviour
	{
        public static Controller cc;

        Camera cam;
        int ammount;

        private void Awake()
        {
            cam = GetComponent<Camera>();
            ammount = 0;
        }

        public void SetPhoto()
        {

            var cam = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cam.z = -10;
            cc.safariCam.transform.position = cam;
            cc.safariCam.transform.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            //FixPos();
            StartCoroutine(Photo(cam));
            Capture("SafariPhoto_" + ammount++.ToString() + ".png", this.cam);
            cc.SetAnimalToShoot();
        }

        IEnumerator Photo(Vector3 pos)
        {
            yield return new WaitForSeconds(1f);

            //Sound 
            //Particles
        }

        void FixPos()
        {
            if (transform.position.y > 3f)
            {
                var tmp = transform.position;
                tmp.y = 3f;
                transform.position = tmp;
            }
            if (transform.position.y < -3f)
            {
                var tmp = transform.position;
                tmp.y = -3f;
                transform.position = tmp;
            }
            if (transform.position.x < -9f)
            {
                var tmp = transform.position;
                tmp.x = -9f;
                transform.position = tmp;
            }
            if (transform.position.x > 9f)
            {
                var tmp = transform.position;
                tmp.x = 9f;
                transform.position = tmp;
            }
        }

        public static void Capture(string name, Camera camera)
        {

            RenderTexture activeRenderTexture = RenderTexture.active;
            RenderTexture.active = camera.targetTexture;

            camera.Render();

            Texture2D image = new Texture2D(camera.targetTexture.width, camera.targetTexture.height);
            image.ReadPixels(new Rect(0, 0, camera.targetTexture.width, camera.targetTexture.height), 0, 0);
            image.Apply();
            RenderTexture.active = activeRenderTexture;

            byte[] bytes = image.EncodeToPNG();

            string path = Application.persistentDataPath + Path.DirectorySeparatorChar + name;
            File.WriteAllBytes(path, bytes);

            cc.AnimalPhoto(image);
        }
    }
}