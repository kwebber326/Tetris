using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Objects.Utilities
{
    public static class FileIOUtilities
    {
        private const string HIGH_SCORES_FOLDER = "HighScores";
        private const string HIGH_SCORES_FILE_NAME = "Scores";
        public const int HIGH_SCORE_COUNT = 10;
        public static void WriteScore(HighScore score)
        {
            try
            {
                string filePath = System.Environment.CurrentDirectory + @"/" + HIGH_SCORES_FOLDER + $@"/{HIGH_SCORES_FILE_NAME}.txt";
                using (FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write))
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    string text = $"{score.Score},{score.Initials},{score.Date}";
                    writer.WriteLine(text);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error writing score: {ex.Message}");
            }
        }

        public static List<HighScore> ReadScoreList()
        {
            CreateHighScoreDirectoryIfNeeded();
            string filePath = System.Environment.CurrentDirectory + @"/" + HIGH_SCORES_FOLDER + $@"/{HIGH_SCORES_FILE_NAME}.txt";
            List<HighScore> scores = new List<HighScore>();
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read))
                using (StreamReader reader = new StreamReader(fs))
                {
                    while (!reader.EndOfStream)
                    {
                        string rawText = reader.ReadLine();
                        string[] rawProperties = rawText.Split(',');
                        HighScore score = new HighScore()
                        {
                            Score = Convert.ToInt32(rawProperties[0]),
                            Initials = rawProperties[1],
                            Date = DateTime.Parse(rawProperties[2])
                        };
                        scores.Add(score);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error reading scores: {ex.Message}");
            }
            List<HighScore> highScores = scores.OrderByDescending(s => s.Score).Take(10).ToList();
            return highScores;
        }

        private static void CreateHighScoreDirectoryIfNeeded()
        {
            string path = System.Environment.CurrentDirectory + @"/" + HIGH_SCORES_FOLDER;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
