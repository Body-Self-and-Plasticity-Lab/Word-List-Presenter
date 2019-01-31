//Body, Self and Plasticity Lab, University of Zurich
//Programmed by Marte Roel - https://github.com/marteroel
//TODO 
//Integrate ShowWordsAutomaticallyNotRandom and ShowWordsAutomaticallyRandom into single coroutine.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WordList
{
public class WordPresenter : MonoBehaviour {

		public CsvRead csvReader;
		public Text wordText;
		public int repetitions;
		public bool setRandomly, logCount, playSoundAtBeginning;
		public float wordDuration, pauseDuration;

		private int currentWord, currentRepetition;
		private static List<int> possibleItems = new List<int>();
		private int presentedWordCount;

	// Use this for initialization
	void Start () {
			
	}
	
	// Update is called once per frame
	void Update () {

			//to play word by word
		//	if (Input.GetKeyDown ("space"))
		//		ShowWordsOnTrigger ();

			//Starts the word series.
			if (Input.GetKeyDown ("w")) {
				StartWordRoutine ();
			} 


	}

		public void StartWordRoutine(){
			if (!playSoundAtBeginning) {
				if (setRandomly) {
					SetRandomList ();
					StartCoroutine (ShowWordsAutomaticallyRandom ());
				} 
				else
					StartCoroutine (ShowWordsAutomaticallyNotRandom ());
			}

			else
				StartCoroutine (PrepareForWordList ());
		}

		private IEnumerator PrepareForWordList(){

			if (this.GetComponentInChildren<AudioSource> () != null)
				this.GetComponentInChildren<AudioSource> ().Play ();
			
			yield return new WaitForSeconds (2f);

			if (setRandomly) {
				SetRandomList ();
				StartCoroutine (ShowWordsAutomaticallyRandom ());
			} else
				StartCoroutine (ShowWordsAutomaticallyNotRandom ());


		}

		//For showing words in order
		private IEnumerator ShowWordsAutomaticallyNotRandom(){

			//to log how many words have been presented so far
			if (logCount) {
				presentedWordCount++;
				Debug.Log (presentedWordCount + " words have been presented");
			}

			wordText.text = csvReader.wordList [currentWord];
			yield return new WaitForSeconds (wordDuration);

			wordText.text = "";
			yield return new WaitForSeconds (pauseDuration);

			//While there are still items to be read from the list, go to next and repeat coroutine 
			if (currentWord < csvReader.wordList.Count-1) {
				currentWord++;
				StartCoroutine (ShowWordsAutomaticallyNotRandom ());
			} 

			//While there are still repetitions of the list to be read, restarts list and coroutine 
			else if (currentRepetition < repetitions-1) {
				currentWord = 0;
				currentRepetition++;
				StartCoroutine(ShowWordsAutomaticallyNotRandom());
			}
			
			//When all the repetitions have been exhausted
			else
				wordText.text = "Thank you for your participation, \n you may now proceed to the next task";
		}





		//For showing words in random order
		private IEnumerator ShowWordsAutomaticallyRandom(){

			//to log how many words have been presented so far
			if (logCount) {
				presentedWordCount++;
				Debug.Log (presentedWordCount + " words have been presented");
			}


			int currentRandomIndex = Random.Range(0, possibleItems.Count);
			int currentRandomItem = possibleItems[currentRandomIndex];

			wordText.text = csvReader.wordList [currentRandomItem];
			yield return new WaitForSeconds (wordDuration);

			wordText.text = "";
			yield return new WaitForSeconds (pauseDuration);

			//While there are still items to be read from the list, go to next and repeat coroutine 
			if (currentWord < csvReader.wordList.Count-1) {
				currentWord++;
				possibleItems.RemoveAt (currentRandomIndex);
				StartCoroutine (ShowWordsAutomaticallyRandom ());
			} 

			//While there are still repetitions of the list to be read, restarts list and coroutine 
			else if (currentRepetition < repetitions-1) {
				currentWord = 0;
				currentRepetition++;
				possibleItems.RemoveAt (currentRandomIndex);
				SetRandomList ();
				StartCoroutine(ShowWordsAutomaticallyRandom());
			}

			//When all the repetitions have been exhausted
			else
				wordText.text = "Thank you for your participation, \n you may now proceed to the next task";
		}





		public void SetRandomList(){
			for (int i = 0; i < csvReader.wordList.Count; i++) {
				possibleItems.Add (i);
			}

		}



		//to be called from elsewhere
		public void ShowWordsOnTrigger(){
			wordText.text = csvReader.wordList [currentWord];

			//While there are still items to be read from the list, go to next and repeat coroutine 
			if (currentWord < csvReader.wordList.Count-1 && currentRepetition <= repetitions-1) 
				currentWord++;

			//While there are still repetitions of the list to be read, restarts list and coroutine 
			else if (currentRepetition <= repetitions-1) {
				currentWord = 0;
				currentRepetition++;
				Debug.Log (currentRepetition);
			}

			//When all the repetitions have been exhausted
			else //(currentRepetition >= repetitions-1)
				wordText.text = "Thank you for your participation, \n you may now proceed to the next task";
		}
	}
}