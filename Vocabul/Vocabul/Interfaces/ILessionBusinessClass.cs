using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Vocabul.Enums;
using Vocabul.Models;
using Vocabul.Structs;

namespace Vocabul.Businesses
{
    public interface ILessionBusinessFactory
    {
        DisassembledLessions DeconstuctLessions(ObservableCollection<Lession> lessions);
        List<Lession> OrderVocabulary(List<Modul> AllModuls, List<Vocabulary> AllVocables);
        DisassembledLession DeconstuctLession(Lession lession);
        List<Vocabulary> LeftOnlyMode(List<Vocabulary> lessions, SelectedMode selectedMode);
        List<Vocabulary> RandomMode(List<Vocabulary> lessions, SelectedMode selectedMode);
        string TrimmVocable(string vocable);
        List<Vocabulary> RightOnlyMode(List<Vocabulary> lessions, SelectedMode selectedMode);
    }
}