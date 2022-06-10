using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PhotoGame
{
    public class PhotoCollect : MonoBehaviour
    {
        public static Controller cc;

        IEnumerator Start()
        {
            yield return new WaitForSeconds(1.5f);
            Vector3 startSize = transform.localScale;
            Vector3 endSize = Vector3.one * 0.2f; 

            Vector3 startPos = transform.position;
            Vector3 endPos = cc.album.transform.position;

            float t = 0;

            while(t < 1f)
            {
                transform.position = Vector3.Lerp(startPos, endPos, t);
                transform.localScale = Vector3.Lerp(startSize, endSize, t);

                t += Time.deltaTime;
                yield return null;
            }

            gameObject.SetActive(false);
            cc.album.GetComponent<Animator>().Play("Album");

        }



    }
}
