﻿/*
    Kingdom Save Editor
    Copyright (C) 2020 Luciano Ciccariello

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using KHSave.LibFf7Remake.Models;
using System;
using System.Globalization;
using Xe.Tools;

namespace KHSave.SaveEditor.Ff7Remake.Models
{
    public class ChapterObjectEntry : BaseNotifyPropertyChanged
    {
        private readonly ChapterObject _chapterObject;

        public ChapterObjectEntry(ChapterObject chapterObject)
        {
            _chapterObject = chapterObject;
        }

        public string Name => ToString();

        public int Index { get => _chapterObject.Index; set { _chapterObject.Index = value; OnPropertyChanged(nameof(Name)); } }
        
        public string Unknown04
        {
            get => _chapterObject.Unknown04.ToString("X08");
            set
            {
                if (!uint.TryParse(value, NumberStyles.HexNumber, null, out var actualValue))
                    throw new FormatException("Not a valid hexadecimal digit");

                _chapterObject.Unknown04 = actualValue;
                OnPropertyChanged((nameof(Name)));
            }
        }

        public int Unknown08 { get => _chapterObject.Unknown08; set { _chapterObject.Unknown08 = value; OnPropertyChanged((nameof(Name))); } }
        public float Unknown0c { get => _chapterObject.Unknown0c; set { _chapterObject.Unknown0c = value; OnPropertyChanged((nameof(Name))); } }
        public float PositionX { get => _chapterObject.PositionX; set { _chapterObject.PositionX = value; OnPropertyChanged((nameof(Name))); } }
        public float PositionY { get => _chapterObject.PositionY; set { _chapterObject.PositionY = value; OnPropertyChanged((nameof(Name))); } }
        public float PositionZ { get => _chapterObject.PositionZ; set { _chapterObject.PositionZ = value; OnPropertyChanged((nameof(Name))); } }
        public float Rotation { get => _chapterObject.Rotation; set { _chapterObject.Rotation = value; OnPropertyChanged((nameof(Name))); } }

        public override string ToString() => _chapterObject.ToString();
    }
}
