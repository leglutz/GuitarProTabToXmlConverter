using System;
using System.IO;
using alphatab.importer;
using System.Xml.Serialization;
using alphatab.audio;

namespace GuitarProTabToXmlConverter
{
    /// <summary>
    /// Class Program.
    /// </summary>
    public class Program
    {
        #region Fields

        /// <summary>
        /// The GP file.
        /// </summary>
        private const string gpFile = "Dream Theater - In The Presence Of Enemies (Pro).gp5";

        /// <summary>
        /// The xml file.
        /// </summary>
        private const string xmlFile = "Dream Theater - In The Presence Of Enemies (Pro).xml";

        #endregion

        #region Methods

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            if (!string.IsNullOrWhiteSpace(gpFile) && File.Exists(gpFile))
            {
                try
                {
                    // Load the score from the GuitarPro file
                    var gpScore = ScoreLoader.loadScore(gpFile);
                    var score = CreateScore(gpScore);

                    // Serialize the score
                    SerializeScore(score);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.ReadKey();
            }
        }

        /// <summary>
        /// Creates the score.
        /// </summary>
        /// <param name="gpScore">The gp score.</param>
        /// <returns>
        /// TabLib.Score.
        /// </returns>
        private static TabLib.Score CreateScore(alphatab.model.Score gpScore)
        {
            var score = new TabLib.Score();

            score.Album = gpScore.album;
            score.Artist = gpScore.artist;
            score.Title = gpScore.title;
            score.Tempo = gpScore.tempo;
            score.MasterBars = CreateMasterBars(gpScore);
            score.Tracks = CreateTracks(gpScore, score.MasterBars);

            return score;
        }

        /// <summary>
        /// Creates the master bars.
        /// </summary>
        /// <param name="gpScore">The gp score.</param>
        /// <returns></returns>
        private static TabLib.MasterBar[] CreateMasterBars(alphatab.model.Score gpScore)
        {
            var masterBars = new TabLib.MasterBar[gpScore.masterBars.length];
            for (var i = 0; i < gpScore.masterBars.length; i++)
            {
                var gpMasterBar = gpScore.masterBars[i] as alphatab.model.MasterBar;
                if (gpMasterBar != null)
                {
                    var masterBar = new TabLib.MasterBar();

                    masterBar.Index = i;
                    // Get the master bar informations
                    if (gpMasterBar.tempoAutomation != null && gpMasterBar.tempoAutomation.type == alphatab.model.AutomationType.Tempo)
                    {
                        masterBar.Tempo = (int)gpMasterBar.tempoAutomation.value;
                    }
                    else if (masterBar.Index == 0)
                    {
                        // Take the score tempo
                        masterBar.Tempo = gpMasterBar.score.tempo;
                    }
                    else
                    {
                        // Take the previous tempo
                        masterBar.Tempo = (int)masterBars[masterBar.Index - 1].Tempo;
                    }

                    masterBar.TimeSignatureNumerator = gpMasterBar.timeSignatureNumerator;
                    masterBar.TimeSignatureDenominator = gpMasterBar.timeSignatureDenominator;
                    
                    masterBars[i] = masterBar;
                }
            }

            return masterBars;
        }

        /// <summary>
        /// Creates the tracks.
        /// </summary>
        /// <param name="gpScore">The gp score.</param>
        /// <param name="masterBars">The master bars.</param>
        /// <returns></returns>
        private static TabLib.Track[] CreateTracks(alphatab.model.Score gpScore, TabLib.MasterBar[] masterBars)
        {
            var tracks = new TabLib.Track[gpScore.tracks.length];
            for (var i = 0; i < gpScore.tracks.length; i++)
            {
                var gpTrack = gpScore.tracks[i] as alphatab.model.Track;
                if (gpTrack != null)
                {
                    var track = new TabLib.Track();

                    track.Index = gpTrack.index;
                    track.Name = gpTrack.name;
                    track.Bars = CreateBars(gpTrack, masterBars);

                    tracks[i] = track;
                }
            }

            return tracks;
        }

        /// <summary>
        /// Creates the bars.
        /// </summary>
        /// <param name="gpTrack">The gp track.</param>
        /// <param name="masterBars">The master bars.</param>
        /// <returns></returns>
        private static TabLib.Bar[] CreateBars(alphatab.model.Track gpTrack, TabLib.MasterBar[] masterBars)
        {
            var bars = new TabLib.Bar[gpTrack.bars.length];
            for (var i = 0; i < gpTrack.bars.length; i++)
            {
                var gpBar = gpTrack.bars[i] as alphatab.model.Bar;
                if (gpBar != null && !gpBar.isEmpty())
                {
                    var bar = new TabLib.Bar();

                    bar.Index = gpBar.index;
                    bar.Beats = CreateBeats(gpBar, masterBars[i]);

                    bars[i] = bar;
                }
            }

            return bars;
        }

        /// <summary>
        /// Creates the beats.
        /// </summary>
        /// <param name="gpVoice">The gp voice.</param>
        /// <param name="masterBar">The master bar.</param>
        /// <returns></returns>
        private static TabLib.Beat[] CreateBeats(alphatab.model.Bar gpBar, TabLib.MasterBar masterBar)
        {
            var gpVoice = gpBar.voices[0] as alphatab.model.Voice;
            var beats = new TabLib.Beat[gpVoice.beats.length];
            for (var i = 0; i < gpVoice.beats.length; i++)
            {
                var gpBeat = gpVoice.beats[i] as alphatab.model.Beat;
                if (gpBeat != null && !gpBeat.isEmpty)
                {
                    var beat = new TabLib.Beat();

                    beat.Index = gpBeat.index;
                    beat.Duration = GetDuration(gpBeat.duration);
                    beat.Notes = CreateNotes(gpBeat);
                    beat.Text = gpBeat.text;
                    beat.Dots = gpBeat.dots;
                    beat.TupletDenominator = gpBeat.tupletDenominator;
                    beat.TupletNumerator= gpBeat.tupletNumerator;
                    beat.CalculateDuration(masterBar.Tempo);

                    beats[i] = beat;
                }
            }

            return beats;
        }

        /// <summary>
        /// Creates the notes.
        /// </summary>
        /// <param name="gpBeat">The gp beat.</param>
        /// <returns></returns>
        private static TabLib.Note[] CreateNotes(alphatab.model.Beat gpBeat)
        {
            var notes = new TabLib.Note[gpBeat.notes.length];
            for (var i = 0; i < gpBeat.notes.length; i++)
            {
                var gpNote = gpBeat.notes[i] as alphatab.model.Note;
                if (gpNote != null)
                {
                    var note = new TabLib.Note();

                    note.@String = gpNote.@string;
                    note.Fret = gpNote.fret;
                    
                    notes[i] = note;
                }
            }

            return notes;
        }

        /// <summary>
        /// Serializes the score.
        /// </summary>
        /// <param name="score">The score.</param>
        private static void SerializeScore(TabLib.Score score)
        {
            using (StreamWriter writer = File.CreateText(xmlFile))
            {
                var serializer = new XmlSerializer(typeof(TabLib.Score));
                serializer.Serialize(writer, score);
            }
        }

        /// <summary>
        /// Gets the duration.
        /// </summary>
        /// <param name="duration">The duration.</param>
        /// <returns></returns>
        private static TabLib.Duration GetDuration(alphatab.model.Duration duration)
        {
            switch(duration)
            {
                case alphatab.model.Duration.Whole:
                    return TabLib.Duration.Whole;
                case alphatab.model.Duration.Half:
                    return TabLib.Duration.Half;
                case alphatab.model.Duration.Quarter:
                    return TabLib.Duration.Quarter;
                case alphatab.model.Duration.Eighth:
                    return TabLib.Duration.Eighth;
                case alphatab.model.Duration.Sixteenth:
                    return TabLib.Duration.Sixteenth;
                case alphatab.model.Duration.ThirtySecond:
                    return TabLib.Duration.ThirtySecond;
                case alphatab.model.Duration.SixtyFourth:
                    return TabLib.Duration.SixtyFourth;
                default:
                    return TabLib.Duration.Quarter;
            }
        }

        #endregion
    }
}
