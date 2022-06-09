using UnityEngine;
using System.Collections;
using System;

namespace PhotoGame
{
	public class PropLong : PhotoProp
    {
        public Vector3 finalPos;

        private void Start()
        {
            base.Start();
            Appear();
        }

        protected override void Appear()
        {
            StartCoroutine(MoveLerp());
        }

        IEnumerator MoveLerp()
        {
            Vector3 initPos = transform.position;

            float t = 0;
            col.enabled = true;
            while(t < time)
            {
                transform.position = Vector3.Lerp(initPos, finalPos, t / time);

                t += Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(0.5f);
            Delete();

        }

        private void Delete()
        {
            Destroy(gameObject);
        }
    }
}