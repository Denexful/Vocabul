using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Vocabul.Businesses;
using Vocabul.Models;
using Vocabul.Structs;

namespace Vocabul.Service
{
    public class LessionService
    {
        private ILessionBusinessFactory lessionFactory;
        private DatabaseService _databaseService;
        public LessionService()
        {
            _databaseService= new DatabaseService();
            lessionFactory = LessionBusinessClass.CreateLessionBusinessFactory();
        }
        public async Task InsertIntoDatabase(ObservableCollection<Lession> lessions)
        {
            for (int i = 0; i < lessions.Count; i++)
            {
                switch (lessions[i].Vocables.Count)
                {
                    case 0:
                        lessions.RemoveAt(i);
                        continue;
                    case 1:
                        if(lessions[i].Vocables[0].English == "" && lessions[i].Vocables[0].German == "")
                        {
                            lessions.RemoveAt(i);
                        }
                        continue;
                    default:
                        continue;
                }
            }
            DisassembledLessions disassembledLessions = await Task.Run(() => lessionFactory.DeconstuctLessions(lessions));
            await _databaseService.InsertModules(disassembledLessions.Moduls);
            await _databaseService.InsertOrUpdateOrDeleteVocablesAsync(disassembledLessions.Vocables);
        }

        public async Task DeleteEntryInDatabase(Lession lessionToDelete) 
        {
            DisassembledLession disassembledLessions = await Task.Run(() => lessionFactory.DeconstuctLession(lessionToDelete));
            if (disassembledLessions.RawModul.ModulID == 0 && disassembledLessions.RawVocables.Count == 0)
            {

            }
            else
            {
                await _databaseService.DeleteVocablesByFKModukID(lessionToDelete.ModulID);
                await _databaseService.DeleteModul(disassembledLessions.RawModul);
            }          
        }
    }
}
