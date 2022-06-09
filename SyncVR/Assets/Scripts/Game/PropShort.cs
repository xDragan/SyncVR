using UnityEngine;
using System.Collections;

namespace PhotoGame
{
	public class PropShort : PhotoProp
    {
        public float height;
        protected void Start()
        {
            base.Start();
            Appear();

        }

        protected override void Appear()
        {
            StartCoroutine(Pop());
        }

        IEnumerator Pop()
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = startPos + Vector3.up * height;
            col.enabled = true;

            float t = 0;
            while (t < time)
            {
                transform.position = Vector3.Lerp(startPos, endPos, t / time);

                t += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(time);
            StartCoroutine(Dissapear());
        }

        IEnumerator Dissapear()
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = startPos + Vector3.up * -height;

            float t = 0;
            while (t < (time/2))
            {
                transform.position = Vector3.Lerp(startPos, endPos, t / (time/2));

                t += Time.deltaTime;
                yield return null;
            }
            col.enabled = false;
            Destroy(gameObject);
        }
    }
}