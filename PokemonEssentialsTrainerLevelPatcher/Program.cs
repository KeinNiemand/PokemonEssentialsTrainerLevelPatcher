using System;

namespace PokemonEssentialsTrainerLevelPatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            double levelMult = 0.8;
            if (args.Length > 0)
                levelMult = Convert.ToDouble(args[0]);
            Console.WriteLine("Please Enter Path to trainers.txt");
            string path = Console.ReadLine().Replace("\"", "");
            if (path != "")
            {
                TrainerTxtFile trainerTxtFile = new TrainerTxtFile(path, levelMult);
                trainerTxtFile.UpdateTrainerTxtFile();
            }
            

            Console.WriteLine("Please Enter Path to trainertypes.txt");
            path = Console.ReadLine().Replace("\"", "");
            if (path != "")
            {

                TrainerTypeTxtFile trainerTypeTxtFile = new TrainerTypeTxtFile(path, 1 / levelMult); //Actual money reward from trainer battles is based on highest pokemon level this increases base money reward for all trainer classes to compensate for lowering pokemon level
                trainerTypeTxtFile.UpdateTrainerTypeTextFile();
            }
            
        }
    }
}
