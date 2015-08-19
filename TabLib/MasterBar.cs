namespace TabLib
{
    /// <summary>
    /// MasterBar class.
    /// </summary>
    public class MasterBar
    {
        #region Properties

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>The index.</value>
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets the tempo.
        /// </summary>
        /// <value>The tempo.</value>
        public int Tempo { get; set; }

        /// <summary>
        /// Gets or sets the time signature numerator.
        /// </summary>
        /// <value>
        /// The time signature numerator.
        /// </value>
        public int TimeSignatureNumerator { get; set; }

        /// <summary>
        /// Gets or sets the time signature denominator.
        /// </summary>
        /// <value>
        /// The time signature denominator.
        /// </value>
        public int TimeSignatureDenominator { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MasterBar"/> class.
        /// </summary>
        public MasterBar()
        {
            Index = 0;
            Tempo = 0;
            TimeSignatureNumerator = 0;
            TimeSignatureDenominator = 0;
        }

        #endregion
    }
}
