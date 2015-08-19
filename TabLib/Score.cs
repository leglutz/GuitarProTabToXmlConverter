using System.Collections.Generic;
namespace TabLib
{
    /// <summary>
    /// Class Score.
    /// </summary>
    public class Score
    {
        #region Properties

        /// <summary>
        /// Gets or sets the album.
        /// </summary>
        /// <value>The album.</value>
        public string Album { get; set; }

        /// <summary>
        /// Gets or sets the artist.
        /// </summary>
        /// <value>The artist.</value>
        public string Artist { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the tempo.
        /// </summary>
        /// <value>The tempo.</value>
        public int Tempo { get; set; }

        /// <summary>
        /// Gets or sets the master bars.
        /// </summary>
        /// <value>
        /// The master bars.
        /// </value>
        public MasterBar[] MasterBars { get; set; }

        /// <summary>
        /// Gets or sets the tracks.
        /// </summary>
        /// <value>The tracks.</value>
        public Track[] Tracks { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Score"/> class.
        /// </summary>
        public Score()
        {
            Album = string.Empty;
            Artist = string.Empty;
            Title = string.Empty;
            Tempo = 0;
            MasterBars = new MasterBar[0];
            Tracks = new Track[0];
        }

        #endregion
    }
}
