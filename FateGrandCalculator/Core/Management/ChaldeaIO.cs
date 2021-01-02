using System.IO;

using FateGrandCalculator.Core.Management.Interfaces;
using FateGrandCalculator.Models;

using Newtonsoft.Json;

namespace FateGrandCalculator.Core.Management
{
    public class ChaldeaIO : IChaldeaIO
    {
        private readonly string _chaldeaFileLocation;

        public ChaldeaIO(string chaldeaFileLocation)
        {
            _chaldeaFileLocation = chaldeaFileLocation;
        }

        public void Save(MasterChaldeaInfo masterChaldeaInfo)
        {
            File.WriteAllText(_chaldeaFileLocation, JsonConvert.SerializeObject(masterChaldeaInfo));
        }

        public MasterChaldeaInfo Load()
        {
            return JsonConvert.DeserializeObject<MasterChaldeaInfo>(File.ReadAllText(_chaldeaFileLocation));
        }
    }
}
