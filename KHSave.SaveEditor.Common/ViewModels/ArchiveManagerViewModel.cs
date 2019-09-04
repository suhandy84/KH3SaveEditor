﻿using KHSave.Archives;
using KHSave.SaveEditor.Common.Models;
using System.Linq;

namespace KHSave.SaveEditor.Common.ViewModels
{
    public class ArchiveManagerViewModel
    {
        public ArchiveManagerViewModel(IArchive archive)
        {
            Archive = archive;
            Entries = new GenericListModel<ArchiveEntryViewModel, ArchiveEntryViewModel>(
                archive.Entries.Select(x => new ArchiveEntryViewModel(x)),
                Getter, Setter);
        }

        private ArchiveEntryViewModel Getter() => null;

        private void Setter(ArchiveEntryViewModel obj) { }

        public IArchive Archive { get; }

        public GenericListModel<ArchiveEntryViewModel, ArchiveEntryViewModel> Entries { get; }

        public IArchiveEntry SelectedEntry { get; set; }
    }
}
