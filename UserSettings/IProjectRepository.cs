using System;

namespace NDbUnit.Utility
{
    public interface IProjectRepository
    {
        void SaveProject(NdbUnitEditorProject settings, string settingsFileName);
        NdbUnitEditorProject LoadProject(string settingsFilePath);
    }
}
