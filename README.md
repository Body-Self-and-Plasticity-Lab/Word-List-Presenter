# Word List Presenter
## Presents a list of words read from a CSV file. Presents one word at the time with a time interval between them, either randomly or not. Allows for manual activation of presented word.

- Set word list settings on WordPresenter in the Manager GameObject. Number of repetitions, random/ordered, and whether to play a sound at the beginning.

- Reads wordlist file from /Lists/WordList.csv

- Set word list from CsvRead in the Manager GameObject.
- The "w" key starts the word list and plays n repetitions of all the available words in the csv file.

- To play each word on trigger, use the ShowWordsOnTrigger() function of the WordPresenter class.
