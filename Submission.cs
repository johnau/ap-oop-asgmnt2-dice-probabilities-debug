namespace DiceProbabilitiesDebug
{
    internal class Submission
    {
        protected readonly TableLogger RcLog = new TableLogger();
        protected readonly int _diceCount;
        protected readonly int _faces;
        protected readonly int _totalCombos;

        public Submission(int numberOfDice, int faces)
        {
            _diceCount = numberOfDice;
            _faces = faces;
            _totalCombos = (int)Math.Pow(faces, numberOfDice);
        }
        public virtual Dictionary<int, double> CalculateProbabilitiesForNumberOfDice()
        {
            var dice = Enumerable.Repeat(1, _diceCount).ToArray();
            var combinations = Enumerable.Range(_diceCount, _diceCount * _faces - _diceCount + 1).ToDictionary(key => key, value => 0);

            combinations[_diceCount] = 1;
            var total = _diceCount;
            
            // For pretty printing
            RcLog.AddResultRow(total, combinations.Values.Select(v => v).ToArray());
            while (total < _diceCount * _faces)
            {
                for (int d = 0; d < _diceCount; d++)
                {
                    if (dice[d] == _faces)
                    {
                        dice[d] = 1;
                    }
                    else
                    {
                        dice[d]++;
                        break;
                    }
                }
                total = dice.Sum();
                combinations[total]++;
                
                // For pretty printing
                RcLog.AddResultRow(total, combinations.Values.Select(v => v).ToArray()); 
            }

            var probabilities = combinations.ToDictionary(
                combo => combo.Key,
                combo => (double)combo.Value / _totalCombos
            );

            // Pretty print combinations dict to console
            RcLog.Log(); 
            return probabilities;
        }
    }
}
