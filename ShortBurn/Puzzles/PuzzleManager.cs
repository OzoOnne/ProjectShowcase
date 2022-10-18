using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace puzzle
{
    public class PuzzleManager : MonoBehaviour
    {
        public TreeGrowing growingTree;
        
         private bool cushion1, cushion2, cushion3, cushion4;

         private bool latern1, latern2;

         private bool done = true;
         private bool lanternDone = true;

         public SoundPuzzle2 _soundPuzzle2;
        
        [SerializeField] private GameObject basementKey;
        [SerializeField] private GameObject gardenKey;

        [Header("Sound")] 
        [SerializeField] private SoundManager soundManager;
        [SerializeField] private string cushionSoundName1;
        [SerializeField] private string cushionSoundName2;
        [SerializeField] private string cushionSoundName3;
        [SerializeField] private string cushionSoundName4;
        [SerializeField] private string puzzleCompleteSoundName;

        void Update()
        {
            if (cushion1 && cushion2 && cushion3 && cushion4 && done)
            {
                soundManager.Play(puzzleCompleteSoundName);
                var basementPos = basementKey.transform.position;
                basementKey.transform.position = new Vector3(basementPos.x, 1.5f, basementPos.z);
                _soundPuzzle2.doingPuzzle = false;
                done = false;
            }

            if (latern1 && latern2 && lanternDone)
            {
                soundManager.Play(puzzleCompleteSoundName);
                growingTree.allowedToGrow = true;
                lanternDone = false;
            }
            
        }

        public void CushionActivation(int number)
        {
            switch (number)
            {
                case 1:
                    cushion1 = true;
                    soundManager.Play(cushionSoundName1);
                    break;
                case 2 when cushion1:
                    cushion2 = true;
                    soundManager.Play(cushionSoundName2);
                    break;
                case 3 when cushion2:
                    cushion3 = true;
                    soundManager.Play(cushionSoundName3);
                    break;
                case 4 when cushion3:
                    cushion4 = true;
                    soundManager.Play(cushionSoundName4);
                    break;
            }
        }

        public void LaternActivation(int number)
        {
            switch (number)
            {
                case 1:
                    latern1 = true;
                    break;
                case 2:
                    latern2 = true;
                    break;
            }
        }

        public void GardenKeySpawn()
        {
            var gardenPos = gardenKey.transform.position;
            gardenKey.transform.position = new Vector3(-7.88f, 3.227f, -6.681f);
        }
    }
}