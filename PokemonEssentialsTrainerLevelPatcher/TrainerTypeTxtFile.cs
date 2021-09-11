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

        /// <summary>
        /// Reads File from <see cref="Path"/>, and writes updated file to <paramref name="newPath"/>
        /// </summary>
        /// <param name="newPath">path for new file</param>
        public void WriteUpdatedTrainerTypeTextFile(string newPath)
        {
            string[] fileLines = File.ReadAllLines(Path);
            ChangeTrainerTypeMoney(fileLines);
            File.Delete(Path);
            File.WriteAllLines(newPath, fileLines);
        }

        /// <summary>
        /// Reads File from <see cref="Path"/>, updates it and overides it with the updated data
        /// </summary>
        public void UpdateTrainerTypeTextFile()
        {
            WriteUpdatedTrainerTypeTextFile(Path);
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
            //Split TrainerTypeData into the diffrent fields
            string[] TrainerTypeData = TrainerTypeDataLine.Split(',');
            //get base money
            int baseMoney = int.Parse(TrainerTypeData[3]);
            //multipy base money
            baseMoney = Math.Max(Convert.ToInt32(moneyMult*baseMoney),0);
            //put basemoney back into TrainerTypeData
            TrainerTypeData[3] = baseMoney.ToString();
            //retrun updatedTrainerTypeData
            return string.Join(',', TrainerTypeData);
        }
    }
}
