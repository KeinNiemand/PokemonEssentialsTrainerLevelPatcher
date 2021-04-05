using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PokemonEssentialsTrainerLevelPatcher
{
    
    public class TrainerTxtFile
    {
        private readonly double levelMult = 0.8;

        /// <summary>
        /// Path to trainers.txt file
        /// </summary>
        public string Path { get; private set; }


        public TrainerTxtFile(string path, double levelMult)
        {
            this.Path = path;
            if (!File.Exists(Path))
                throw new FileNotFoundException(Path);
            this.levelMult = levelMult;
        }


        public void UpdateTrainerTxtFile()
        {
            string[] fileLines = File.ReadAllLines(Path);
            ChangePokemonLevel(fileLines);
            File.Delete(Path);
            File.WriteAllLines(Path, fileLines);
        }

        private void ChangePokemonLevel(string[] allLines)
        {
            uint trainerDataLineCounter = 0;
            for (int i = 0; i < allLines.Length; i++)
            {
                //New Trainer found Reset counter and continue to the next line
                if (allLines[i].StartsWith("#-"))
                {
                    trainerDataLineCounter = 0;
                    continue;
                }
                //increment counter
                trainerDataLineCounter++;

                //line 4 or later of trainer data is the trainers pokemon
                if (trainerDataLineCounter >= 4)
                {
                    allLines[i] = ChangePokemonLevel(allLines[i]);
                }

            }
        }

        private string ChangePokemonLevel(string PokémonDataLine)
        {
            //Split PokémonData into the diffrent fields
            string[] PokémonData = PokémonDataLine.Split(',');
            //get level
            byte level = byte.Parse(PokémonData[1]);
            //multipy level
            level = Math.Clamp(Convert.ToByte(levelMult*level),(byte)1,(byte)100);

            PokémonData[1] = level.ToString();

            return string.Join(',', PokémonData);
        }
    }
}
