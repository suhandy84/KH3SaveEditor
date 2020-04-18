﻿using KHSave.LibFf7Remake.Chunks;
using KHSave.LibFf7Remake.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Xe.Tools.Wpf.Commands;
using Xe.Tools.Wpf.Dialogs;

namespace KHSave.SaveEditor.Ff7Remake.Models
{
    public class ChunkEntryModel
    {
        private const string MessageBoxTextDifferentSizeWarning = "You are importing a chunk that have a different size.\nThis will potentially lead to unexpected game behaviour.\n\nDo you want to continue?";
        private readonly Chunk _chunk;
        private readonly int _index;
        private readonly IEnumerable<FileDialogFilter> Filter = FileDialogFilterComposer
            .Compose()
            .AddExtensions("Final Fantasy VII Remake CHUNK", "ff7rchunk")
            .AddAllFiles();

        public ChunkEntryModel(Chunk chunk, int index)
        {
            _chunk = chunk;
            _index = index;
            ImportCommand = new RelayCommand(_ => FileDialog.OnOpen(Import, Filter, $"chunk{_index}"));
            ExportCommand = new RelayCommand(_ => FileDialog.OnSave(Export, Filter, $"chunk{_index}"));
        }

        public RelayCommand ImportCommand { get; }
        public RelayCommand ExportCommand { get; }

        public byte ChunkType { get => _chunk.Header.Unknown00; set => _chunk.Header.Unknown00 = value; }
        public byte ChapterId { get => _chunk.Header.Unknown01; set => _chunk.Header.Unknown01 = value; }
        public byte Unknown { get => _chunk.Header.Unknown02; set => _chunk.Header.Unknown02 = value; }

        public string MagicCode { get => _chunk.Content?.MagicCode; set => _chunk.Content.MagicCode = value; }
        public int Unknown10 { get => _chunk.Content?.Unknown10 ?? -1; set => _chunk.Content.Unknown10 = value; }
        public int DataSize
        {
            get
            {
                if (_chunk.Content == null)
                    return -1;
                return Math.Max(0, _chunk.Content.ChunkLength - 0x20);
            }

            private set => _chunk.Content.ChunkLength = value + 0x20;
        }
        public int Unknown18 { get => _chunk.Content?.Unknown18 ?? -1; set => _chunk.Content.Unknown18 = value; }
        public int Unknown1c { get => _chunk.Content?.Unknown1c ?? -1; set => _chunk.Content.Unknown1c = value; }

        private void Import(string fileName)
        {
            using (var stream = File.OpenRead(fileName))
            {
                if (stream.Length != DataSize)
                {
                    if (MessageBox.Show(MessageBoxTextDifferentSizeWarning, "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes)
                        return;
                }

                _chunk.Content.RawData = stream.ReadAllBytes();
                DataSize = _chunk.Content.RawData.Length;
            }
        }

        private void Export(string fileName)
        {
            using (var stream = File.Create(fileName))
                stream.Write(_chunk.Content.RawData, 0, _chunk.Content.RawData.Length);
        }

        public override string ToString() =>
            $"Chunk ID #{_index}: {ChunkType}, {ChapterId}, {Unknown} | {MagicCode}";
    }
}
