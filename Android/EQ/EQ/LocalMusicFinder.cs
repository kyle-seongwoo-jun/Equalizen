﻿//using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Provider;
using Android.Net;

namespace EQ
{
    class LocalMusicFinder
    {
        public List<LocalMusic> FindMusic(ContentResolver resolver)
        {
            var musicList = new List<LocalMusic>();

            Uri uri = MediaStore.Audio.Media.ExternalContentUri;
            string selection = MediaStore.Audio.AudioColumns.IsMusic + " != 0";
            string sortOrder = MediaStore.Audio.AudioColumns.Title + " ASC";
            var cursor = resolver.Query(uri, null, selection, null, sortOrder);

            if (cursor != null)
            {
                int count = cursor.Count;
                if (count > 0)
                {
                    while (cursor.MoveToNext())
                    {
                        string title = cursor.GetString(cursor.GetColumnIndex(MediaStore.Audio.AudioColumns.Title));
                        string artist = cursor.GetString(cursor.GetColumnIndex(MediaStore.Audio.AudioColumns.Artist));
                        long duration = cursor.GetLong(cursor.GetColumnIndex(MediaStore.Audio.AudioColumns.Duration));
                        string name = cursor.GetString(cursor.GetColumnIndex(MediaStore.Audio.AudioColumns.DisplayName));
                        string path = cursor.GetString(cursor.GetColumnIndex(MediaStore.Audio.AudioColumns.Data));
                        
                        musicList.Add(new LocalMusic
                        {
                            Title = title,
                            Artist = artist,
                            Path = path,
                            Duration = System.TimeSpan.FromMilliseconds(duration),
                            Name = name
                        });
                    }
                }
            }

            return musicList;
        }
    }

    class LocalMusic
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public System.TimeSpan Duration { get; set; }
    }
}