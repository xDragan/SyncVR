using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random;

namespace PhotoGame
{
    public class Controller : MonoBehaviour
    {
        [System.Serializable]
        public struct Animal
        {
            public PhotoProp.Type animType;
            public bool inUse;
            public bool appeared;
            public GameObject[] animal;
        }

        public float spawnCooldown;
        public Animal[] animals;
        public int animCount = -1;

        public Camera safariCam;

        public GameObject photoPrefab, canvas, animalFind, album;

        [Header("Animal to photo")]
        public GameObject targetAnimal;

        private void Awake()
        {
            SafariCapture.cc = this;
            PhotoProp.cc = this;
            PhotoCollect.cc = this;
        }

        private void Start()
        {
            SetAnimalToShoot();
            StartCoroutine(SpawnAnimal());
        }

        public bool CanPhoto(PhotoProp.Type anim)
        {
            Debug.Log("CAN PHOTO, ANIMAL: " + anim);
            return (int)anim == animCount; //not the prettiest way
        }

        public void SetAnimalToShoot()
        {
            if (animCount >= animals.Length)
                return;
            SetChildsInvisible();
            animCount++;
            CheckEnd();
            if (animCount < animals.Length)
                targetAnimal.transform.GetChild(animCount).gameObject.SetActive(true);
        }

        private void CheckEnd()
        {
            if(animCount == animals.Length - 1)
            {
                //END
            }
        }

        private void SetChildsInvisible() //SetTarget animals invisible
        {
            for (int i = 0; i < targetAnimal.transform.childCount; i++)
            {
                //if (targetAnimal.transform.GetChild(i).gameObject.CompareTag("Player"))
                    targetAnimal.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        IEnumerator SpawnAnimal()
        {
            yield return new WaitForSeconds(spawnCooldown);
            var tmp = GetAnimal();
            if (tmp.animType != PhotoProp.Type.None)
            {
                var go = Instantiate(tmp.animal[Random.Range(0, 2)]);
            }
            StartCoroutine(SpawnAnimal());
        }

        public void AnimalPhoto(Texture2D img)
        {
            var go = Instantiate(photoPrefab, Input.mousePosition, Quaternion.identity);

            RawImage rImg = go.GetComponentInChildren<RawImage>();
            rImg.texture = img;
        }

        Animal GetAnimal()
        {
            Utility.ShuffleArray<Animal>(animals);

            Animal ret = new Animal();
            ret.animType = PhotoProp.Type.None;

            for (int i = 0; i < animals.Length; i++)
            {
                if (!animals[i].inUse)
                {
                    ret = animals[i];
                    animals[i].inUse = true;
                    break;
                }
            }

            return ret;
        }

    }
}

