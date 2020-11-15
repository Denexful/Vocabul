using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vocabul.Models;
using Vocabul.Service;
using Vocabul.Businesses;
using System.Collections.ObjectModel;
using Vocabul.Helpers;

namespace Vocabul.Service
{
    class StoreList
    {
        private List<Modul> AllModuls;
        private  List<Vocabulary> AllVocables;
        private ILessionBusinessFactory lessionBusiness;
        public static List<Lession> Lessions;
        static DatabaseService _database;
        public static DatabaseService Database
        {
            get
            {
                if (_database == null)
                {
                    _database = new DatabaseService();
                }
                return _database;
            }
        }
        public StoreList()
        {
            GetModulsVocablesFromDB();
            RefreshDataHelper.DataChanged += RefreshLessions;
        }

        private void GetModulsVocablesFromDB()
        {
            AllModuls = Task.Run(GetAllModuls).Result;
            AllVocables = Task.Run(GetAllVocables).Result;
            lessionBusiness = LessionBusinessClass.CreateLessionBusinessFactory();
            Lessions = lessionBusiness.OrderVocabulary(AllModuls, AllVocables);
        }

        public async Task<List<Modul>> GetAllModuls()
        {

            var modul = await Database.GetModulesAsync();
            return modul;
        }
        public async Task<List<Vocabulary>> GetAllVocables()
        {
            var vocabularies = await Database.GetVocablesAsync();
            return vocabularies;
        }
        private void RefreshLessions(object sender, EventArgs e)
        {
            GetModulsVocablesFromDB();
        }
    }
}
