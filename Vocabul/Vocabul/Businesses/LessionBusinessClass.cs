using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Vocabul.Enums;
using Vocabul.Models;
using Vocabul.Structs;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace Vocabul.Businesses
{
    
    public class LessionBusinessClass : ILessionBusinessFactory
    {

        public static ILessionBusinessFactory CreateLessionBusinessFactory()
        {
            return new LessionBusinessClass();
        }
        public DisassembledLessions DeconstuctLessions(ObservableCollection<Lession> lessions)
        {
            DisassembledLessions disassembledLessions = new DisassembledLessions();
            List<Vocabulary> DisassembledVobables = new List<Vocabulary>();
            List<Modul> DisassembledModules = new List<Modul>();         
            foreach (Lession lession  in lessions)
            {
                switch (lession.Modul_Name)
                {
                    case "Rename this!":
                        continue;
                    default:
                        Modul modul = new Modul();
                        modul.ModulID = lession.ModulID;
                        modul.Modul_Name = lession.Modul_Name;
                        DisassembledModules.Add(modul);
                        foreach (Vocabulary vocabulary in lession.Vocables)
                        {
                            if (vocabulary.German != "" && vocabulary.English != "")
                            {
                                Vocabulary vocable = new Vocabulary();
                                vocable.German = TrimmVocable(vocabulary.German);
                                vocable.English = TrimmVocable(vocabulary.English);
                                vocable.Counter = vocabulary.Counter;
                                vocable.ModulID = vocabulary.ModulID;
                                vocable.VocableID = vocabulary.VocableID;
                                DisassembledVobables.Add(vocable);
                            }
                            else if (vocabulary.German == "" && vocabulary.English == "" && vocabulary.VocableID != 0)
                            {
                                Vocabulary vocable = new Vocabulary();
                                vocable.German = TrimmVocable(vocabulary.German);
                                vocable.English = TrimmVocable(vocabulary.English);
                                vocable.Counter = vocabulary.Counter;
                                vocable.ModulID = vocabulary.ModulID;
                                vocable.VocableID = vocabulary.VocableID;
                                DisassembledVobables.Add(vocable);
                            }
                        }
                        break;
                }
            }
            disassembledLessions.Moduls = DisassembledModules;
            disassembledLessions.Vocables = DisassembledVobables;
            return disassembledLessions;
        }
        public  List<Lession> OrderVocabulary(List<Modul> AllModuls, List<Vocabulary> AllVocables)
        {
            List<Lession> tempLession = new List<Lession>();
            foreach (var modul in AllModuls)
            {
                Lession lession = new Lession()
                {
                    ModulID = modul.ModulID,
                    Modul_Name = modul.Modul_Name
                };
                foreach (var vocable in AllVocables)
                {
                    if (vocable.ModulID == modul.ModulID)
                    {
                        lession.Vocables.Add(vocable);
                    }
                    else
                    {
                        continue;
                    }
                }
                tempLession.Add(lession);
            }
            return tempLession;
        }
        public DisassembledLession DeconstuctLession(Lession lession)
        {
            DisassembledLession disassembledLession = new DisassembledLession();
            List<Vocabulary> DisassembledVobables = new List<Vocabulary>();
            Modul DisassembledModule = new Modul();
            switch (lession.Modul_Name)
                {
                       case "Rename this!":
                        break;
                    default:
                        Modul modul = new Modul();
                        modul.ModulID = lession.ModulID;
                        modul.Modul_Name = lession.Modul_Name;
                        DisassembledModule = modul;
                        foreach (Vocabulary vocabulary in lession.Vocables)
                        {
                            if (vocabulary.German != "" && vocabulary.English != "")
                            {
                                Vocabulary vocable = new Vocabulary();
                                vocable.German = TrimmVocable(vocabulary.German);
                                vocable.English =TrimmVocable(vocabulary.English);
                                vocable.Counter = vocabulary.Counter;
                                vocable.ModulID = vocabulary.ModulID;
                                vocable.VocableID = vocabulary.VocableID;
                                DisassembledVobables.Add(vocable);
                            }
                        }
                        break;
            }          
            disassembledLession.RawModul = DisassembledModule;
            disassembledLession.RawVocables = DisassembledVobables;
            return disassembledLession;
        }
        public List<Vocabulary> LeftOnlyMode(List<Vocabulary> vocables, SelectedMode selectedMode)
        {
            List<Vocabulary> vocablesForTest = new List<Vocabulary>();
            switch (selectedMode)
            {
                case SelectedMode.Left:
                    try
                    {
                            foreach (var vocable in vocables)
                            {
                            Vocabulary newVocable = new Vocabulary();
                            newVocable.German = vocable.German;
                            newVocable.English = "";                                
                                vocablesForTest.Add(newVocable);
                            }                        
                    }
                    catch (Exception)
                    {
                        throw new Exception("lession is null");
                    }                    
                    break;
                default:
                    throw new Exception("SelectedMode is not SelectedMode.left");
            }
            return vocablesForTest;
        }

        public List<Vocabulary> RandomMode(List<Vocabulary> vocables, SelectedMode selectedMode)
        {
            Random r = new Random();
            List<Vocabulary> vocablesForTest = new List<Vocabulary>();
            Vocabulary newVocable;
            switch (selectedMode)
            {
                case SelectedMode.Random:
                    try
                    {
                        foreach (var vocable in vocables)
                        {
                            int randomNumber = r.Next(0, 2);
                            switch (randomNumber)
                            {
                                case 0:
                                    newVocable = new Vocabulary();
                                    newVocable.English = vocable.English;
                                    newVocable.German = "";
                                    vocablesForTest.Add(newVocable);

                                    continue;
                                case 1:
                                    newVocable = new Vocabulary();
                                    newVocable.German = vocable.German;
                                    newVocable.English = "";
                                    vocablesForTest.Add(newVocable);
                                    continue;
                            }      
                        }
                    }
                    catch (Exception)
                    {
                        throw new Exception("lession is null");
                    }
                    break;
                default:
                    throw new Exception("SelectedMode is not SelectedMode.left");
            }
            return vocablesForTest;
        }
        public List<Vocabulary> RightOnlyMode(List<Vocabulary> vocables, SelectedMode selectedMode)
        {
            List<Vocabulary> vocablesForTest = new List<Vocabulary>();
            switch (selectedMode)
            {
                case SelectedMode.Right:
                    try
                    {
                        foreach (var vocable in vocables)
                        {
                            Vocabulary newVocable = new Vocabulary();
                            newVocable.English = vocable.English;
                            newVocable.German = "";
                            vocablesForTest.Add(newVocable);
                        }
                    }
                    catch (Exception)
                    {
                        throw new Exception("lession is null");
                    }
                    break;
                default:
                    throw new Exception("SelectedMode is not SelectedMode.left");
            }
            return vocablesForTest;
        }
        public string TrimmVocable(string vocable)
        {
            string cleanVocable = "";
            try
            {
                char chartoCheck = vocable[vocable.Length - 1];
                switch (chartoCheck)
                {
                    case ' ':
                        cleanVocable = vocable.Remove(vocable.Length - 1);
                        break;
                    default:
                        cleanVocable = vocable;
                        break;
                }
            }
            catch (Exception)
            {
                return cleanVocable;
            }

            return cleanVocable;
        }
    }
}
