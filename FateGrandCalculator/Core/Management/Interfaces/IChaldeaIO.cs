using FateGrandCalculator.Models;

namespace FateGrandCalculator.Core.Management.Interfaces
{
    public interface IChaldeaIO
    {
        void Save(MasterChaldeaInfo masterChaldeaInfo);
        MasterChaldeaInfo Load();
    }
}
