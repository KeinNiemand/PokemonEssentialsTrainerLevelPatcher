using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PokemonEssentialsTrainerLevelPatcher
{
    
    public class TrainerTypeTxtFile
    {
        private readonly double moneyMult = 0.8;

        /// <summary>
        /// Path to trainertypes.txt file
        /// </summary>
        public string Path { get; private set; }



        public TrainerTypeTxtFile(string path, double moneyMult)
        {
            this.Path = path;
            if (!File.Exists(Path))
                throw new FileNotFoundException(Path);
            this.moneyMult = moneyMult;
        }


        public void UpdateTrainerTypeTextFile()
        {
            string[] fileLines = File.ReadAllLines(Path);
            ChangeTrainerTypeMoney(fileLines);
            File.Delete(Path);
            File.WriteAllLines(Path, fileLines);
        }

        private void ChangeTrainerTypeMoney(string[] allLines)
        {
            for (int i = 1; i < allLines.Length; i++)
            {
                allLines[i] =  ChangeTrainerTypeBaseMoney(allLines[i]);
            }
        }

        private string ChangeTrainerTypeBaseMoney(string TrainerTypeDataLine)
        {
            //Split PokémonData into the diffrent fields
            string[] TrainerTypeDate = TrainerTypeDataLine.Split(',');
            //get level
            int baseMoney = int.Parse(TrainerTypeDate[3]);
            //multipy level
            baseMoney = Math.Max(Convert.ToInt32(moneyMult*baseMoney),0);

            TrainerTypeDate[3] = baseMoney.ToString();

            return string.Join(',', TrainerTypeDate);
        }
    }
}
