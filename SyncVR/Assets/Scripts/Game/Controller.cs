using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        public GameObject photoPrefab, canvas, hexAnimal, photos;

        [Header("Animal to photo")]
        public GameObject targetAnimal;

        private void Awake()
        {
            SafariCapture.cc = this;
        }

        private void Start()
        {
            SetAnimalToShoot();
            StartCoroutine(SpawnAnimal());
        }

        private void Update()
        {
            Debug.Log("animal length: " + animals.Length);
        }

        private void SetAnimalToShoot()
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
            if(animCount == animals.Length)
            {
                //END
            }
        }

        private void SetChildsInvisible() //SetTarget animals invisible
        {
            for (int i = 0; i < targetAnimal.transform.childCount; i++)
            {
                if (targetAnimal.transform.GetChild(i).gameObject.CompareTag("Player"))
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

