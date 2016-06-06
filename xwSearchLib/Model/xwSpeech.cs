using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xwSearchLib.Model
{
    public class xwSpeech
    {
        public class word
        {
            public Dictionary<string,word> nextWord { get; set; }
            public bool hasValue { get; set; }
        }

        public word listOfPhrases { get; set; }
        public Dictionary<string, int> listOfWords { get; set; }
        public List<string> justWordList { get; set; }
        private List<Pilot> pilots { get; set; }
        private List<Upgrade> upgrades { get; set; }
        
        public xwSpeech()
        {
            this.listOfWords = new Dictionary<string, int>();
            this.listOfPhrases = new word();
            this.pilots = new List<Pilot>();
            this.upgrades = new List<Upgrade>();
            this.justWordList = new List<string>();

        }

        public xwSpeech(List<Pilot> pilots, List<Upgrade> upgrades)
        {
            this.pilots = pilots;
            this.upgrades = upgrades;
            this.listOfWords = new Dictionary<string, int>();
            this.listOfPhrases = new word();
            this.justWordList = new List<string>();
        }

        public xwSpeech.word getWord(string word)
        {
            xwSpeech.word output = new xwSpeech.word();

            this.listOfPhrases.nextWord.TryGetValue(word, out output);

            return output;
        }

        public void splitIntoPhrases()
        {
            foreach (Pilot pilot in this.pilots)
            {
                string abilityText = cleanPunctuation(pilot.pilotAbility.ToLower());

                if (String.IsNullOrEmpty(abilityText) || abilityText == "")
                    continue;

                List<string> words = abilityText.ToLower().Split(' ').ToList();
                
                words.RemoveAll(x=>x.ToString() == "");
                
                recursiveBuild(words, 0, this.listOfPhrases);
            }
            foreach (Upgrade upgrade in this.upgrades)
            {
                string abilityText = cleanPunctuation(upgrade.ability.ToLower());

                if (String.IsNullOrEmpty(abilityText) || abilityText == "")
                    continue;

                List<string> words = abilityText.Split(' ').ToList();

                words.RemoveAll(x => x.ToString() == "");

                recursiveBuild(words, 0, this.listOfPhrases);
            }
        }

        public void splitIntoWords()
        {
            foreach (Pilot pilot in this.pilots)
            {
                string abilityText = cleanPunctuation(pilot.pilotAbility);

                if (String.IsNullOrEmpty(abilityText) || abilityText == "")
                    continue;

                List<string> words = abilityText.ToLower().Split(' ').ToList();

                foreach (string word in words)
                {
                    if (this.listOfWords.ContainsKey(word))
                    {
                        this.listOfWords[word]++;
                    }
                    else
                    {
                        this.justWordList.Add(word);
                        this.listOfWords.Add(word, 1);
                    }
                }
            }

            foreach (Upgrade upgrade in this.upgrades)
            {
                string abilityText = cleanPunctuation(upgrade.ability);


                if (String.IsNullOrEmpty(abilityText) || abilityText == "")
                    continue;

                List<string> words = abilityText.Split(' ').ToList();

                foreach (string word in words)
                {
                    if (this.listOfWords.ContainsKey(word))
                    {
                        this.listOfWords[word]++;
                    }
                    else
                    {
                        this.justWordList.Add(word);
                        this.listOfWords.Add(word, 1);
                    }
                }
            }
        }

        private void recursiveBuild(List<string> words, int count, word wordHash)
        {
            if (count >= words.Count)
                return;

            if (!wordHash.hasValue)
            {
                wordHash.hasValue = true;
                wordHash.nextWord = new Dictionary<string, word>();
            }

            if(!wordHash.nextWord.ContainsKey(words[count]))
            {
                wordHash.nextWord.Add(words[count], new word() { nextWord = new Dictionary<string, word>(), hasValue = false });
            }
            
            wordHash = wordHash.nextWord[words[count]];

            recursiveBuild(words, count + 1, wordHash);
        }

        private string cleanPunctuation(string text)
        {
            string output = text;

            output = output.Replace("Attack(focus)", "");
            output = output.Replace("Attack(target lock)", "");

            output = output.Replace(".", "");
            output = output.Replace(",", "");
            output = output.Replace(";", "");
            output = output.Replace(":", "");
            output = output.Replace("(", "");
            output = output.Replace(")", "");
            output = output.Replace("\"", "");
            output = output.Replace("”", "");
            output = output.Replace("“", "");
            output = output.Replace("[", "");
            output = output.Replace("]", "");

            return output;
        }

    }
}
