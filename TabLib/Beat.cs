namespace TabLib
{
    #region Public enums

    /// <summary>
    /// Lists all durations of a beat.
    /// </summary>
    public enum Duration
    {
        Whole,
        Half,
        Quarter,
        Eighth,
        Sixteenth,
        ThirtySecond,
        SixtyFourth
    }

    #endregion

    /// <summary>
    /// Class Beat.
    /// </summary>
    public class Beat
    {
        #region Properties

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>The index.</value>
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        public Duration Duration { get; set; }

        /// <summary>
        /// Gets or sets the duration value.
        /// </summary>
        /// <value>
        /// The duration value.
        /// </value>
        public float DurationValue { get; set; }

        /// <summary>
        /// Gets or sets the dots.
        /// </summary>
        /// <value>
        /// The dots.
        /// </value>
        public int Dots { get; set; }

        /// <summary>
        /// Gets or sets the tuplet numerator.
        /// </summary>
        /// <value>
        /// The tuplet numerator.
        /// </value>
        public int TupletNumerator { get; set; }

        /// <summary>
        /// Gets or sets the tuplet denominator.
        /// </summary>
        /// <value>
        /// The tuplet denominator.
        /// </value>
        public int TupletDenominator { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        public Note[] Notes { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Beat"/> class.
        /// </summary>
        public Beat()
        {
            Index = 0;
            Duration = Duration.Quarter;
            DurationValue = 0;
            Dots = 0;
            TupletNumerator = 0;
            TupletDenominator = 0;
            Notes = new Note[0];
            Text = string.Empty;

            CalculateDuration(120);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Calculates the duration.
        /// </summary>
        /// <returns></returns>
        public void CalculateDuration(int tempo)
        {
            DurationValue = 60f / (tempo * (GetDurationValue() / 4f));
            if (Dots == 2)
            {
                ApplyDot(true);
            }
            else if (Dots == 1)
            {
                ApplyDot(false);
            }

            if (TupletDenominator > 0 && TupletNumerator >= 0)
            {
                ApplyTuplet();
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Applies the dot.
        /// </summary>
        /// <param name="doubleDotted">if set to <c>true</c> [double dotted].</param>
        private void ApplyDot(bool doubleDotted)
        {
            if (doubleDotted)
            {
                DurationValue += ((DurationValue / 4f) * 3f);
            }
            else
            {
                DurationValue += (DurationValue / 2f);
            }
        }

        /// <summary>
        /// Applies the tuplet.
        /// </summary>
        private void ApplyTuplet()
        {
            DurationValue *= TupletDenominator / (float)TupletNumerator;
        }

        /// <summary>
        /// Gets the duration value.
        /// </summary>
        /// <returns></returns>
        private int GetDurationValue()
        {
            switch (Duration)
            {
                case Duration.Whole:
                    return 1;
                case Duration.Half:
                    return 2;
                case Duration.Quarter:
                    return 4;
                case Duration.Eighth:
                    return 8;
                case Duration.Sixteenth:
                    return 16;
                case Duration.ThirtySecond:
                    return 32;
                case Duration.SixtyFourth:
                    return 64;
                default:
                    return 4;
            }
        }

        #endregion
    }
}
