using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Vocabul.Businesses;
using Vocabul.Helpers;
using Vocabul.Models;
using Vocabul.Service;

namespace Vocabul.ViewModels
{
    public class EvaluationPageViewModel
    {
        private ILessionBusinessFactory lessionBusinessFactory;
        private RefreshDataHelper refreshDataHelper;
        private DatabaseService _database;
        public ObservableCollection<string> CorrectVocables { get; set; }
        public ObservableCollection<Vocabulary> WrongVocables { get; set; }
        public ObservableCollection<Vocabulary> GoodVocables { get; set; }
        public EvaluationPageViewModel(List<Vocabulary> originalList, List<Vocabulary> userList)
        {
            lessionBusinessFactory = new LessionBusinessClass();
            _database = new DatabaseService();
            WrongVocables = new ObservableCollection<Vocabulary>();
            GoodVocables = new ObservableCollection<Vocabulary>();
            CorrectVocables = new ObservableCollection<string>();
            refreshDataHelper = new RefreshDataHelper();
            ValidateVoables(originalList,userList);
        }
        private async void ValidateVoables(List<Vocabulary> originalList, List<Vocabulary> userList)
        {
            for (int i = 0; i < userList.Count; i++)
            {
                userList[i].German = lessionBusinessFactory.TrimmVocable(userList[i].German);
                userList[i].English = lessionBusinessFactory.TrimmVocable(userList[i].English);
                if (originalList[i].German.Equals(userList[i].German) && originalList[i].English.Equals(userList[i].English))
                {
                    switch (originalList[i].Counter > 0)
                    {
                        case true:
                            originalList[i].Counter -= 1;
                            GoodVocables.Add(originalList[i]);
                            await _database.UpdateVocable(originalList[i]);
                            continue;
                        default:
                            GoodVocables.Add(originalList[i]);
                            continue;
                    }
                }
                else if (!originalList[i].German.Equals(userList[i].German))
                {
                    originalList[i].Counter += 1;
                    userList[i].Counter += 1;
                    WrongVocables.Add(userList[i]);
                    CorrectVocables.Add(originalList[i].German);
                   await _database.UpdateVocable(originalList[i]);

                }
                else if (!originalList[i].English.Equals(userList[i].English))
                {
                    originalList[i].Counter += 1;
                    userList[i].Counter += 1;
                    WrongVocables.Add(userList[i]);
                    CorrectVocables.Add(originalList[i].English);
                    await _database.UpdateVocable(originalList[i]);

                }
            }
            EditorViewModel.IsRefreshingAfterTest = true;
            refreshDataHelper.CallChange(null, "EvalutationPage");
        }        
    }
}
