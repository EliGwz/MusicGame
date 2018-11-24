using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;
using SonicBloom.Koreo.Players;

public static class GameStatics{

	public class Song {
        string name;
        string bpm;
        string length;
        string imagePath;
        string playBackName;
        string musicDemoPath;
        //string musicPath;
        //Koreography koreography;
        //SimpleMusicPlayer simpleMusicPlayer;

        public Song(string name, string bpm, string length, string imagePath, string playBackName, string musicDemoPath) {
            this.name = name;
            this.bpm = bpm;
            this.length = length;
            this.imagePath = imagePath;
            this.playBackName = playBackName;
            this.musicDemoPath = musicDemoPath;
            //this.simpleMusicPlayer = simpleMusicPlayer;
        }

        public string Name {
            get {
                return name;
            }

            set {
                name = value;
            }
        }

        public string Bpm {
            get {
                return bpm;
            }

            set {
                bpm = value;
            }
        }

        public string Length {
            get {
                return length;
            }

            set {
                length = value;
            }
        }

        public string ImagePath {
            get {
                return imagePath;
            }

            set {
                imagePath = value;
            }
        }

        public string PlayBackName {
            get {
                return playBackName;
            }

            set {
                playBackName = value;
            }
        }

        public string MusicDemoPath {
            get {
                return musicDemoPath;
            }

            set {
                musicDemoPath = value;
            }
        }
    }

    public static string rankSPath = "RankingImages/S";
    public static string rankAPath = "RankingImages/A";
    public static string rankBPath = "RankingImages/B";
    public static string rankCPath = "RankingImages/C";
    public static string rankDPath = "RankingImages/D";
    public static string cheersPath = "Sounds/Cheers";



    public static Song[] songs = {
        new Song("Blade Dance", "187", "01:30", "SongImages/BladeDance","Music_BladeDance","SongDemos/BladeDance"),
        new Song("Blade Dance2", "1872", "01:302", "SongImages/Astral_Zero", "Music_BladeDance","SongDemos/BladeDance2"),
        new Song("Blade Dance3", "18724", "01:30322", "SongImages/BladeDance", "Music_BladeDance","SongDemos/BladeDance") };

    public static Song testSong = new Song("Blade Dance", "187", "01:30", "SongImages/BladeDance", "Music_BladeDance", "SongDemos/BladeDance");


}
