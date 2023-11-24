using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Objects
{
    public class TetrisGameLogic
    {
        private const int MAX_LEVELS = 29;
        public const int BASE_SINGLE_MATCH = 40;
        public const int BASE_DOUBLE_MATCH = 100;
        public const int BASE_TRIPLE_MATCH = 300;
        public const int BASE_TETRIS_MATCH = 1200;
        public const int BASE_COMBO_SCORE = 50;

        public const int BASE_MINI_T_SPIN_0 = 100;
        public const int BASE_MINI_T_SPIN_1 = 200;
        public const int BASE_MINI_T_SPIN_2 = 300;

        public const int BASE_T_SPIN_0 = 400;
        public const int BASE_T_SPIN_1 = 800;
        public const int BASE_T_SPIN_2 = 1200;
        public const int BASE_T_SPIN_3 = 1600;

        private Dictionary<int, int> _lineScoreMapping = new Dictionary<int, int>()
        {
            { 1, BASE_SINGLE_MATCH },
            { 2, BASE_DOUBLE_MATCH },
            { 3, BASE_TRIPLE_MATCH },
            { 4, BASE_TETRIS_MATCH }
        };

        private Dictionary<int, int> _gravitySpeedByLevel = new Dictionary<int, int>()
        {
            { 1, 800 },
            { 2, 715 },
            { 3, 632 },
            { 4, 549 },
            { 5, 466 },
            { 6, 383 },
            { 7, 300 },
            { 8, 216 },
            { 9, 133 },
            { 10, 100 },
            { 11, 83 },
            { 12, 83 },
            { 13, 83 },
            { 14, 67 },
            { 15, 67 },
            { 16, 67 },
            { 17, 50 },
            { 18, 50 },
            { 19, 50 },
            { 20, 33 },
            { 21, 33 },
            { 22, 33 },
            { 23, 33 },
            { 24, 33 },
            { 25, 33 },
            { 26, 33 },
            { 27, 33 },
            { 28, 33 },
            { 29, 33 },
            { 30, 17 }
        };

        private static Dictionary<int, int> _linesMiniTSpinScoreMapping = new Dictionary<int, int>()
        {
            { 0, BASE_MINI_T_SPIN_0 },
            { 1, BASE_MINI_T_SPIN_1 },
            { 2, BASE_MINI_T_SPIN_2 }
        };

        private static Dictionary<int, int> _linesTSpinScoreMapping = new Dictionary<int, int>()
        {
            { 0, BASE_T_SPIN_0 },
            { 1, BASE_T_SPIN_1 },
            { 2, BASE_T_SPIN_2 },
            { 3, BASE_T_SPIN_3 }
        };
        public TetrisGameLogic()
        {
            this.Level = 1;
            CalculateMovementInterval();
            CalculateNewMatchThreshold();
        }

        private void CalculateMovementInterval()
        {
            this.MovementInterval = _gravitySpeedByLevel[this.Level];
        }

        private void CalculateNewMatchThreshold()
        {
            this.MatchesToNextLevel = (this.Level) * 10;
        }

        public int GetComboScore(int comboAmount)
        {
            return this.Level * BASE_COMBO_SCORE * comboAmount;
        }

        public event EventHandler GameChanged;
        public int Level { get; private set; }

        public int Score { get; private set; }

        public int MovementInterval { get; private set; }

        public int MatchCount { get; private set; }

        public int MatchesToNextLevel { get; private set; }

        protected void OnGameChanged()
        {
            GameChanged?.Invoke(this, EventArgs.Empty);
        }


        public void ScoreMatch(int lines, int comboCount, int TSpinMultiplier)
        {
            if (lines < 1 || lines > 4)
                return;
            int scoreForMatch = GetScoreForMatch(lines, comboCount, TSpinMultiplier);
            this.Score += scoreForMatch;
            this.MatchCount += lines;
            if (this.Level < MAX_LEVELS && this.MatchCount >= this.MatchesToNextLevel)
            {
                this.Level++;
                CalculateMovementInterval();
                CalculateNewMatchThreshold();
            }

            OnGameChanged();
        }

        public void IncrementScore(int amount)
        {
            this.Score += amount;
            OnGameChanged();
        }

        public static int GetTSpinScore(int lines, int tSpinMultiplier, int level)
        {
            int tSpinScore = 0;
            
            if (tSpinMultiplier == 1 && _linesMiniTSpinScoreMapping.TryGetValue(lines, out int score))
            {
                    tSpinScore = score * level;
            }
            else if (tSpinMultiplier > 1 && _linesTSpinScoreMapping.TryGetValue(lines, out int scoreTSpin))
            {
                tSpinScore = scoreTSpin * level;
            }
            return tSpinScore;
        }

        private int GetScoreForMatch(int lines, int comboCount, int tSpinMultiplier)
        {
            int tSpinScore = GetTSpinScore(lines, tSpinMultiplier, this.Level);
            return ((this.Level) * _lineScoreMapping[lines]) + GetComboScore(comboCount) + tSpinScore;
        }
    }
}
